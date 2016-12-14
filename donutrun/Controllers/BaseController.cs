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
            var store = db.Store.FirstOrDefault();
            if (store != null)
            {
                ViewBag.StoreName = store?.DisplayName ?? "";
            }
        }

    }
}

