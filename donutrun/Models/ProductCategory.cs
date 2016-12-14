using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace donutrun.Models
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<Product> Products { get; set; }
    }
}