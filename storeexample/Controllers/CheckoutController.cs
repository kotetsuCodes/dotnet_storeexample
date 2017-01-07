using storeexample.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace storeexample.Controllers
{
    public class CheckoutController : Controller
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

            for (var i = 1; i < 21; i++)
            {
                availableDates.Add($"{DateTime.Now.AddDays(i).ToShortDateString()} {DateTime.Now.AddDays(i).DayOfWeek}");
            }

            for (var i = 0; i < 24; i++)
            {
                availableHours.Add(DateTime.Now.AddHours(i).ToShortTimeString());
            }

            var model = new CheckoutViewModel()
            {
                Order = order,
                DeliveryDay = availableDates.Select(ad => new SelectListItem() { Text = ad, Value = ad }).ToList(),
                DeliveryTime = availableHours.Select(ah => new SelectListItem() { Text = ah, Value = ah }).ToList()
            };

            return View(model);
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
            var stripeClient = new StripeClient("sk_test_");

            dynamic response = stripeClient.CreateChargeWithToken(order.GrandTotal, model.StripeToken);

            order.Status = OrderStatus.Paid;
            db.SaveChanges();

            return View("OrderComplete", model);
        }
    }
}