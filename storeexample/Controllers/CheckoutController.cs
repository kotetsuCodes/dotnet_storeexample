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

            var model = new CheckoutViewModel()
            {
                Order = order,
                OrderTotal = orderTotal
            };

            return View(model);
        }
    }
}