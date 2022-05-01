using System;
using System.Collections.Generic;

#nullable disable

namespace DeliveryApp.Models
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int PizzaId { get; set; }
        public int ItemCount { get; set; }
        public decimal? OrderItemAmount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
