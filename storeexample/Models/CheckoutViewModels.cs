using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace storeexample.Models
{
    public class CheckoutViewModel
    {
        public Order Order { get; set; }
        [DataType(DataType.Currency)]
        public decimal OrderTotal { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        [Required]
        public int ZipCode { get; set; }
        [Required]
        public string CityState { get; set; }
        [Display(Name = "Special Instructions")]
        public string SpecialInstructions { get; set; }
        [Display(Name = "Delivery Day")]
        [Required]
        public string DeliveryDay { get; set; }
        [Display(Name = "Delivery Time")]
        [Required]
        public string DeliveryTime { get; set; }
    }
}