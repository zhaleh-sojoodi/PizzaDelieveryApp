using System;
using System.Collections.Generic;

#nullable disable

namespace DeliveryApp.Models.PizzaDelivery
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerAddress { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
