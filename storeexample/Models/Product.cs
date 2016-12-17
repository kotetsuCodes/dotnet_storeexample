using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace storeexample.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [DataType(DataType.Currency)]
        public decimal BasePrice { get; set; }
        public int BaseQuantity { get; set; }
        public bool IsActive { get; set; }
        public bool IsMultipleItem { get; set; }
        public bool IsAlwaysAvailable { get; set; }

        [ForeignKey("Category")]
        public int ProductCategoryId { get; set; }

        public virtual ProductCategory Category { get; set; }

    }
}