using storeexample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace storeexample.Controllers
{
    public class HomeController : BaseController
    {
        private StoreExampleModel db = new StoreExampleModel();

        public ActionResult Index(HomePageViewModel model)
        {
            var store = db.Store.FirstOrDefault();
            if (store != null && string.IsNullOrEmpty(store.HomeSplashImageUrl) == false)
            {
                ViewBag.HomeSplashImageUrl = store.HomeSplashImageUrl;
            }

            model.HomePageCrumbs = db.HomePageCrumbs.ToList();

            // if our store delivers we'll show the zip code form
            if (store.DeliveryAvailable)
            {
                model.ShowZipCodeForm = true;
            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(ContactViewModel model)
        {
            model.Contacts = db.Contacts.ToList();
            ViewBag.Message = "Your contact page.";

            return View(model);
        }

        [HttpGet]
        public ActionResult VerifyZipCode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyZipCode(HomePageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("_OrderProducts", model);
            }

            model.ShowZipCodeForm = true;

            ViewBag.ZipCode = model.ZipCode;
            int parsedZip = int.Parse(model.ZipCode);

            var zipcode = db.ZipCodes.SingleOrDefault(z => z.Zip == parsedZip);

            if(zipcode == null)
            {
                db.ZipCodes.Add(new ZipCode() { Zip = parsedZip, IsServiced = false });
                db.SaveChanges();
                model.ZipCodeMessage = db.Store.First().ZipCodeNotInServiceMessage;
            }
            else if(zipcode.IsServiced)
            {
                var order = db.Orders.Add(new Order()
                {
                    ZipCode = zipcode,
                    DateOrdered = DateTime.Now,
                    Status = OrderStatus.Initial
                });

                db.SaveChanges();

                return RedirectToAction("Index", "Order", new { orderId = order.OrderId });
            }
            else
            {
                model.ZipCodeMessage = db.Store.First().ZipCodeNotInServiceMessage;
            }

            return View(model);
        }
    }
}