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

            var model = new CheckoutViewModel()
            {
                Order = order
            };

            return View(model);
        }
    }
}