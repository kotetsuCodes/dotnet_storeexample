using storeexample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace storeexample.Controllers
{
    public class OrderController : Controller
    {
        private StoreExampleModel db = new StoreExampleModel();

        public ActionResult Index(int orderId)
        {
            var order = db.Orders.SingleOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception("Invalid Order");
            }

            var model = new OrderViewModel()
            {
                Order = order,
                Products = db.Products.Where(p => p.IsActive).ToList()
            };

            return View(model);
        }

        [HttpPut]
        public ActionResult AddProductToCart(int orderId, int productId)
        {
            var order = db.Orders.SingleOrDefault(o => o.OrderId == orderId && o.Status == OrderStatus.Initial);
            var product = db.Products.SingleOrDefault(p => p.ProductId == productId);

            if (order == null)
            {
                throw new Exception("Invalid order");
            }
            if (product == null)
            {
                throw new Exception("Invalid product");
            }

            var orderedProduct = order.OrderedProducts?.SingleOrDefault(op => op.ProductId == product.ProductId);

            if (orderedProduct == null)
            {
                orderedProduct = new OrderedProduct()
                {
                    Order = order,
                    Product = product,
                    QuantityOrdered = 1
                };

                order.OrderedProducts.Add(orderedProduct);
            }
            else
            {
                orderedProduct.QuantityOrdered++;
            }

            db.SaveChanges();

            var model = new OrderViewModel();
            model.Order = order;

            return PartialView("_OrderCart", model);
        }
    }
}