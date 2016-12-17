using storeexample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace storeexample.Controllers.Admin
{
    public class BaseAdminController : Controller
    {
        private StoreExampleModel db = new StoreExampleModel();

        public BaseAdminController()
        {
            ViewBag.StoreName = $"{db.Store.First().DisplayName} Admin";            
        }
    }
}