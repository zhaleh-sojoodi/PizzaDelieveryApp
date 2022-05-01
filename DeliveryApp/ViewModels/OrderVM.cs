using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.ViewModels
{
    public class OrderVM
    {
        public int OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public decimal? OrderTotal { get; set; }
        public DateTime? OrderDate { get; set; }
        public virtual ICollection<OrderItemVM> OrderItems { get; set; }

    }
}
