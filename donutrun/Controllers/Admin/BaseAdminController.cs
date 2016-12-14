using donutrun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace donutrun.Controllers.Admin
{
    public class BaseAdminController : Controller
    {
        private DonutRunModel db = new DonutRunModel();

        public BaseAdminController()
        {
            ViewBag.StoreName = $"{db.Store.First().DisplayName} Admin";            
        }
    }
}