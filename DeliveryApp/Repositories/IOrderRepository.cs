using DeliveryApp.Models;
using DeliveryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Repositories
{
    public interface IOrderRepository
    {
        public Task AddOrder(Order order);
        public Task<IEnumerable<OrderVM>> GetOrdersByDate(DateTime dt);
        public Task<OrderVM> GetOrderById(int id);
        public Task DropOrder(int orderId);
    }
}
