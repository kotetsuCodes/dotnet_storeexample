using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace donutrun.Models
{
    public class DashboardViewModel
    {
        public IList<Order> Orders { get; set; }
        public IList<Order> OrdersLast24Hours { get; set; }
        public IList<Order> OrdersLastWeek { get; set; }
        public IList<Order> OrdersLastMonth { get; set; }

        public IList<ProductCategory> ProductCategories { get; set; }

        public IList<Product> Products { get; set; }
        public IList<Product> ProductsActive { get; set; }

        public IList<ZipCode> ZipCodes { get; set; }
        public IList<Driver> Drivers { get; set; }
        public IList<Driver> DriversActive { get; set; }
        public Store Store { get; set; }
    }
}