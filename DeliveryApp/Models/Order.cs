using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DeliveryApp.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public string CustomerEmail { get; set; }
        public decimal? OrderTotal { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual Customer CustomerEmailNavigation { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
