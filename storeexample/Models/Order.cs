using System;
using System.Collections.Generic;

namespace storeexample.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime DateOrdered { get; set; }
        public DateTime? ScheduledDeliveryDate { get; set; }
        public OrderStatus Status { get; set; }
        public RecurFrequency RecurFrequency { get; set; }
        public DayOfWeek WeeklyRecurDay { get; set; }
        public int MonthlyRecurDay { get; set; }
        public bool IsRecurring { get; set; }        

        //Order Info
        public string DeliveryAddress1 { get; set; }
        public string DeliveryAddress2 { get; set; }
        public string DeliveryAddress3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public ZipCode ZipCode { get; set; }

        public virtual List<OrderedProduct> OrderedProducts { get; set; }
    }

    public enum OrderStatus
    {
        Initial,
        Paid,
        Delivered,
        Canceled
    }

    public enum DayOfWeek
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6
    }

    public enum RecurFrequency
    {
        None,
        Daily,
        Weekly,
        Monthly
    }
}