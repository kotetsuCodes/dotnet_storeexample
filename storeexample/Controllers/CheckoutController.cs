using storeexample.Models;
using storeexample.Utilities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace storeexample.Controllers
{
    public class CheckoutController : BaseController
    {
        private StoreExampleModel db = new StoreExampleModel();

        // GET: Checkout
        public ActionResult Index(int orderId)
        {
            var order = db.Orders.SingleOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception("Invalid Order");
            }

            decimal orderTotal = order.OrderedProducts.Sum(op => op.QuantityOrdered * op.Product.BasePrice * op.Product.BaseQuantity);
            decimal deliveryFee = db.Store.First().DeliveryFee;
            order.DeliveryCharge = deliveryFee;
            order.SubTotal = orderTotal;
            order.GrandTotal = Math.Round(orderTotal + deliveryFee, 2);

            db.SaveChanges();

            List<string> availableDates = new List<string>();
            List<string> availableHours = new List<string>();

            if (DateTime.Now.Hour < db.Store.First().DeliveryHourEnd)
            {
                availableDates.Add($"{DateTime.Now.ToShortDateString()} {DateTime.Now.DayOfWeek}");

                for (var i = db.Store.First().DeliveryHourStart; i <= db.Store.First().DeliveryHourEnd; i++)
                {
                    if (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i, 0, 0) > DateTime.Now.AddMinutes(15))
                    {
                        availableHours.Add(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i, 0, 0).ToShortTimeString());
                    }
                    if (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i, 30, 0) > DateTime.Now.AddMinutes(15))
                    {
                        availableHours.Add(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i, 30, 0).ToShortTimeString());
                    }
                }
            }
            else
            {
                for (var i = db.Store.First().DeliveryHourStart; i <= db.Store.First().DeliveryHourEnd; i++)
                {
                    availableHours.Add(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i, 0, 0).ToShortTimeString());
                    availableHours.Add(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i, 30, 0).ToShortTimeString());
                }
            }

            for (var i = 1; i < 21; i++)
            {
                availableDates.Add($"{DateTime.Now.AddDays(i).ToShortDateString()} {DateTime.Now.AddDays(i).DayOfWeek}");
            }

            var model = new CheckoutViewModel()
            {
                Order = order,
                DeliveryDay = availableDates.Select(ad => new SelectListItem() { Text = ad, Value = ad }).ToList(),
                DeliveryTime = availableHours.Select(ah => new SelectListItem() { Text = ah, Value = ah }).ToList(),
                CityState = $"{order.City}, {order.State}"
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult GetDeliveryTimes(string deliveryDay)
        {
            var deliveryTimes = new List<string>();

            if (DateTime.Parse(deliveryDay) > DateTime.Today)
            {
                for (var i = db.Store.First().DeliveryHourStart; i <= db.Store.First().DeliveryHourEnd; i++)
                {
                    deliveryTimes.Add($"{i}:00");
                    deliveryTimes.Add($"{i}:30");
                }
            }
            else
            {
                for (var i = db.Store.First().DeliveryHourStart; i <= db.Store.First().DeliveryHourEnd; i++)
                {
                    if (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i, 0, 0) > DateTime.Now.AddMinutes(15))
                    {
                        deliveryTimes.Add($"{i}:00");
                    }
                    if (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i, 30, 0) > DateTime.Now.AddMinutes(15))
                    {
                        deliveryTimes.Add($"{i}:30");
                    }
                }
            }


            return Json(deliveryTimes);
        }

        [HttpPost]
        public ActionResult SubmitPayment(int orderId, CheckoutViewModel model)
        {
            var order = db.Orders.SingleOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception("Invalid Order");
            }

            order.DeliveryAddress1 = model.DeliveryAddress;
            order.IsRecurring = false;
            order.RecurFrequency = RecurFrequency.None;

            order.ScheduledDeliveryDate = DateTime.Parse($"{model.Day} {model.Time}");

            Customer customer = new Customer()
            {
                Address1 = model.DeliveryAddress,
                EmailAddress = model.Email,
                Phone = model.Phone,
                FirstName = model.Name.Substring(0, model.Name.IndexOf(' ')),
                LastName = model.Name.Substring(model.Name.IndexOf(' ') + 1),
                Orders = new List<Order>() { order }
            };

            db.Customers.Add(customer);

            db.SaveChanges();

            //new StripeClient(db.Store.First().StripeKey)
            var stripeClient = new StripeClient(db.Store.First().StripePrivateKey.GetDecryptedString(db.Store.First().StripeIV, db.Store.First().StripeKey));

            dynamic response = stripeClient.CreateChargeWithToken(order.GrandTotal, model.StripeToken);

            order.Status = OrderStatus.Paid;
            db.SaveChanges();

            StringBuilder emailBody = new StringBuilder();

            emailBody.AppendLine("--Order Details--");
            emailBody.AppendLine($"Customer Name: {customer.FirstName} {customer.LastName}");
            emailBody.AppendLine($"Phone: {customer.Phone}");
            
            emailBody.AppendLine($"Delivery Address: {order.DeliveryAddress1} {order.DeliveryAddress2} {order.DeliveryAddress3}");
            emailBody.AppendLine($"Delivery Time: {order.ScheduledDeliveryDate.Value.ToShortDateString()} {order.ScheduledDeliveryDate.Value.ToShortTimeString()}");
            emailBody.AppendLine($"Delivery Charge: ${order.DeliveryCharge}");

            foreach(var orderedProducts in order.OrderedProducts)
            {
                emailBody.AppendLine($"{orderedProducts.Product.DisplayName} x {orderedProducts.QuantityOrdered} @ ${orderedProducts.Product.BasePrice}");
            }

            emailBody.AppendLine();
            emailBody.AppendLine($"Order Total: ${order.GrandTotal}");

            var smtpClient = new SmtpClient()
            {
                Port = 587,
                Host = db.Store.First().EmailHost,
                Credentials = new System.Net.NetworkCredential(db.Store.First().EmailUsername, db.Store.First().EmailPassword.GetDecryptedString(db.Store.First().EmailPasswordIV, db.Store.First().EmailPasswordKey)),
            };

            smtpClient.Send(db.Store.First().OrderConfirmationFromAddress, order.Customer.EmailAddress, $"{db.Store.First().DisplayName} Order Confirmation", emailBody.ToString());

            return View("OrderComplete", model);
        }
    }
}