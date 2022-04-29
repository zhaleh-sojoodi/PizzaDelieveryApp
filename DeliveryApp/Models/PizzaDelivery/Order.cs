using System;
using System.Collections.Generic;

#nullable disable

namespace DeliveryApp.Models.PizzaDelivery
{
    public partial class Order
    {
        public Order()
        {
            PizzaOrders = new HashSet<PizzaOrder>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public decimal? OrderTotal { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<PizzaOrder> PizzaOrders { get; set; }
    }
}
