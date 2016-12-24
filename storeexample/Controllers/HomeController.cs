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
        public ActionResult VerifyZipCode(string zipcode)
        {
            var zipcodes = db.ZipCodes.Where(z => z.IsServiced && z.Zip == zipcode);

            return PartialView("ZipCodeCheck");
        }
    }
}