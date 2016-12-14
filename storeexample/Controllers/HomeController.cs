using donutrun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace donutrun.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var store = db.Store.FirstOrDefault();
            if(store != null && string.IsNullOrEmpty(store.HomeSplashImageUrl) == false)
            {
                ViewBag.HomeSplashImageUrl = store.HomeSplashImageUrl;
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}