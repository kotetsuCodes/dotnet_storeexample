using donutrun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace storeexample.Models
{
    public class ContactViewModel
    {
        public IList<Contact> Contacts { get; set; }
    }
}