using System;
using System.Collections.Generic;

#nullable disable

namespace DeliveryApp.Models.PizzaDelivery
{
    public partial class PizzaOrder
    {
        public int PizzaOrdersId { get; set; }
        public int? OrderId { get; set; }
        public int? PizzaId { get; set; }
        public int? PizzaOrdersCount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
