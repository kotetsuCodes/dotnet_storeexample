using storeexample.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace storeexample.Models
{
    public class ContactViewModel
    {
        public IList<Contact> Contacts { get; set; }
    }

    public class HomePageViewModel
    {
        public IList<HomePageCrumb> HomePageCrumbs { get; set; }
        public bool ShowZipCodeForm { get; set; }
        [DataType(DataType.PostalCode)]
        [Required]
        public string ZipCode { get; set; }
        public string ZipCodeMessage { get; set; }

        public bool ReadyToOrder { get; set; }

        public IList<Product> Products { get; set; }
        public IList<OrderedProduct> OrderedProducts { get; set; }


    }


}