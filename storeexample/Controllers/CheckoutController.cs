using storeexample.Models;
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

            List<string> availableDates = new List<string>();
            List<string> availableHours = new List<string>();

            for(var i = 1; i < 21; i++)
            {
                availableDates.Add($"{DateTime.Now.AddDays(i).ToShortDateString()} {DateTime.Now.AddDays(i).DayOfWeek}");
            }

            for(var i = 0; i < 24; i++)
            {
                availableHours.Add(DateTime.Now.AddHours(i).ToShortTimeString());
            }

            var model = new CheckoutViewModel()
            {
                Order = order,
                OrderTotal = orderTotal,
                DeliveryDay = availableDates.Select(ad => new SelectListItem() { Text = ad, Value = ad }).ToList(),
                DeliveryTime = availableHours.Select(ah => new SelectListItem() { Text = ah, Value = ah }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ProcessOrder(CheckoutViewModel model)
        {
            //var order = db.Orders.SingleOrDefault(o => o.OrderId == orderId);
            //if (order == null)
            //{
            //    throw new Exception("Invalid Order");
            //}

            if (ModelState.IsValid)
            {
                return View(model);
            }

            return View(model);
        }
    }
}