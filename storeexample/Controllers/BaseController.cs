using storeexample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace storeexample.Controllers
{
    public class BaseController : Controller
    {
        protected StoreExampleModel db = new StoreExampleModel();

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

