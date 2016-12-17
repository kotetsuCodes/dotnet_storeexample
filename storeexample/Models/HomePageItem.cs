using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace storeexample.Models
{
    public class HomePageItem
    {
        public int HomePageItemId { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
    }
}