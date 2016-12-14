using donutrun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace donutrun.Controllers
{
    public class BaseController : Controller
    {
        protected DonutRunModel db = new DonutRunModel();

        public BaseController()
        {
            ViewBag.StoreName = db.Store.First().DisplayName;
            ViewBag.HomeSplashImageUrl = $"{ db.Store.First().HomeSplashImageUrl}";
        }

    }
}

