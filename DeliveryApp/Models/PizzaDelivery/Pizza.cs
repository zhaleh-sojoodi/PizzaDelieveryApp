using System;
using System.Collections.Generic;

#nullable disable

namespace DeliveryApp.Models.PizzaDelivery
{
    public partial class Pizza
    {
        public Pizza()
        {
            PizzaOrders = new HashSet<PizzaOrder>();
        }

        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public decimal? PizzaAmount { get; set; }

        public virtual ICollection<PizzaOrder> PizzaOrders { get; set; }
    }
}
