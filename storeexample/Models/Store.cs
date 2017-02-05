using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace storeexample.Models
{
    public class Store
    {
        public int StoreId { get; set; }

        public string DisplayName { get; set; }
        public string Url { get; set; }

        public bool IsOpenOnHolidays { get; set; }
        public string HolidayMessage { get; set; }
        public bool DeliveryAvailable { get; set; }
        public decimal DeliveryFee { get; set; }

        public int DeliveryHourStart { get; set; }
        public int DeliveryHourEnd { get; set; }

        public bool IsMultiNational { get; set; }
        public bool IsMultiProvince { get; set; }
        public bool IsMultiCity { get; set; }

        [DataType(DataType.ImageUrl)]
        public string HomeSplashImageUrl { get; set; }

        public virtual List<Product> Products { get; set; }
        public virtual List<Driver> Drivers { get; set; }
        public virtual List<Contact> ContactInfo { get; set; }
        public string ZipCodeNotInServiceMessage { get; set; }

        //Email Details
        public string EmailHost { get; set; }
        public string EmailUsername { get; set; }
        public string EmailPassword { get; set; }

        public string StripePrivateKey { get; set; }

        public string EmailPasswordIV { get; set; }
        public string EmailPasswordKey { get; set; }

        public string StripeIV { get; set; }
        public string StripeKey {get;set;}

        public string OrderConfirmationFromAddress { get; set; }
    }
}