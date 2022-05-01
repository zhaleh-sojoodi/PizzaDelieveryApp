using System;
using System.Collections.Generic;

#nullable disable

namespace DeliveryApp.Models
{
    public partial class Pizza
    {
        public Pizza()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public decimal? PizzaAmount { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
