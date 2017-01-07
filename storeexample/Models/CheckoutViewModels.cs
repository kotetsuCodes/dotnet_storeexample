using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace storeexample.Models
{
    public class CheckoutViewModel
    {
        public Order Order { get; set; }

        public string StripeToken { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        [Required]
        [DataType(DataType.PostalCode)]
        public int ZipCode { get; set; }
        [Required]
        public string CityState { get; set; }
        [Display(Name = "Special Instructions")]
        [DataType(DataType.MultilineText)]
        public string SpecialInstructions { get; set; }
        [Display(Name = "Delivery Day")]
        [Required]
        public List<SelectListItem> DeliveryDay { get; set; }
        [Display(Name = "Delivery Time")]
        [Required]
        public List<SelectListItem> DeliveryTime { get; set; }
    }
}