using System.Collections.Generic;

namespace storeexample.Models
{
    public class Driver
    {
        public int DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public int Rating { get; set; }
        public bool IsActive { get; set; }

        public virtual List<ZipCode> ZipCodes {get; set;}
        public virtual List<Order> Orders { get; set; }
    }
}