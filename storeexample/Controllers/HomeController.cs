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
            if(store != null && string.IsNullOrEmpty(store.HomeSplashImageUrl) == false)
            {
                ViewBag.HomeSplashImageUrl = store.HomeSplashImageUrl;
            }

            model.HomePageCrumbs = db.HomePageCrumbs.ToList();

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
    }
}