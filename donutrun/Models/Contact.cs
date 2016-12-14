using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace donutrun.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public bool IsFullNameVisible { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ContactType ContactType { get;set;}
    }

    public enum ContactType
    {
        Email,
        Phone,
        EmailAndPhone
    }
}