using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace storeexample.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public IList<Product> Products { get; set; }
    }
}