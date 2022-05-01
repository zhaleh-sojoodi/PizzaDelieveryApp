using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.ViewModels
{
    public class OrderItemVM
    {
        public string PizzaName { get; set; }
        public decimal? PizzaAmount { get; set; }
        public int ItemCount { get; set; }
        public decimal? OrderItemAmount { get; set; }
    }
}
