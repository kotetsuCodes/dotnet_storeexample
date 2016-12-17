using storeexample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace storeexample.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminHomeController : BaseAdminController
    {
        private StoreExampleModel db = new StoreExampleModel();

        private DateTime lastDay = DateTime.Now.AddHours(-24);
        private DateTime lastWeek = DateTime.Now.AddDays(-7);
        private DateTime lastMonth = DateTime.Now.AddDays(-30);

        public ActionResult Index()
        {
            if (db.Store.Any() == false)
            {
                return RedirectToAction("Create", "Stores");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Dashboard(DashboardViewModel model)
        {
            model.Store = db.Store.First();

            model.Drivers = db.Drivers.ToList();
            model.DriversActive = db.Drivers.Where(d => d.IsActive).ToList();

            model.Orders = db.Orders.ToList();
            model.OrdersLast24Hours = db.Orders.Where(o => !o.IsRecurring && o.DateOrdered < DateTime.Now && o.DateOrdered > lastDay).ToList();
            model.OrdersLastWeek = db.Orders.Where(o => !o.IsRecurring && o.DateOrdered < DateTime.Now && o.DateOrdered > lastWeek).ToList();
            model.OrdersLastMonth = db.Orders.Where(o => !o.IsRecurring && o.DateOrdered < DateTime.Now && o.DateOrdered > lastMonth).ToList();

            model.ProductCategories = db.ProductCategories.ToList();

            model.Products = db.Products.ToList();
            model.ProductsActive = db.Products.Where(p => p.IsActive).ToList();

            model.ZipCodes = db.ZipCodes.Where(zc => zc.IsServiced).ToList();

            if (db.Store.Any() == false)
            {
                return RedirectToAction("Create", "Stores");
            }
            else
            {
                return View(model);
            }
        }
    }
}