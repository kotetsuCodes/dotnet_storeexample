using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace donutrun.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Phone { get; set; }

        
        List<Order> Orders { get; set; }
    }
}