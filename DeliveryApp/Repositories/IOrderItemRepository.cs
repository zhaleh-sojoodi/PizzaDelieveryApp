using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Repositories
{
    public interface IOrderItemRepository
    {
        public Task AddOrderItem(OrderItem orderItem);
        public Task UpdateOrderItem(OrderItem orderItem);
        public Task DropOrderItem(int orderItemId, int orderId);
        public Task<bool> OrderItemExist(int orderItemId);

    }
}
