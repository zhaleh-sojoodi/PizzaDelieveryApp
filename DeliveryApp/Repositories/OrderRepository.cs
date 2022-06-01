using DeliveryApp.Models;
using DeliveryApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DeliveryDBContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(DeliveryDBContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddOrder(Order order)
        {
            if(order.CustomerEmail == null)
            {
                var errMsg = "Customer email is null";
                _logger.LogError(errMsg);
                throw new KeyNotFoundException(errMsg);
                
            }

            if (!await _customerExist(order.CustomerEmail))
            {
                var errMsg = "Customer doesnot exist.First create the customer profile";
                _logger.LogError(errMsg);
                throw new KeyNotFoundException(errMsg);

            }

            if (order.OrderItems.Count() == 0)
            {
                var errMsg = "OrderItem is null";
                _logger.LogError(errMsg);
                throw new KeyNotFoundException(errMsg);
            }

            order.OrderDate = DateTime.Now;
            order.OrderTotal = order.OrderItems.Sum(i => i.OrderItemAmount);
            //foreach (OrderItem o in order.OrderItems.ToList())
            //{
            //    order.OrderItems.Add(o);
            //}
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            _logger.LogDebug("Successfully inserted order");
        }

        public async Task<IEnumerable<OrderVM>> GetOrdersByCustomerAndDate(OrderVM order)
        {
            if (!await _customerExist(order.CustomerEmail))
            {
                var errMsg = "Customer doesnot exist";
                _logger.LogError(errMsg);
                throw new KeyNotFoundException(errMsg);

            }

            var orders= await _context.Orders
                .Where(o => o.CustomerEmail.Equals(order.CustomerEmail) && DateTime.Compare(o.OrderDate.Value.Date, order.OrderDate.Value.Date) == 0)
                .Select(o => new OrderVM
                {
                    OrderId = o.OrderId,
                    CustomerEmail = o.CustomerEmail,
                    OrderTotal = o.OrderTotal,
                    OrderDate = o.OrderDate,
                    OrderItems = o.OrderItems.Select(i => new OrderItemVM
                    {
                        PizzaName = i.Pizza.PizzaName,
                        PizzaAmount = i.Pizza.PizzaAmount,
                        ItemCount = i.ItemCount,
                        OrderItemAmount = i.ItemCount * i.Pizza.PizzaAmount
                    }).ToList()
                }).ToListAsync();
            _logger.LogDebug($"Retrived all the orders for the customet : {order.CustomerEmail} on the date : {order.OrderDate}");
            return orders;
        }

        public async Task<OrderVM> GetOrderById(int id)
        {
            _logger.LogInformation($"Retrieving the order of {id}");

            if(!await _orderExist(id))
            {
                var errMsg = $"Failed to find the order with id : {id}";
                _logger.LogError(errMsg);
                throw new KeyNotFoundException(errMsg);
            }

            var order = _context.Orders.Where(o => o.OrderId.Equals(id)).Select(o => new OrderVM
            {
                OrderId = o.OrderId,
                CustomerEmail = o.CustomerEmail,
                OrderTotal = o.OrderTotal,
                OrderDate = o.OrderDate,
                OrderItems = o.OrderItems.Select(i => new OrderItemVM
                {
                    PizzaName = i.Pizza.PizzaName,
                    PizzaAmount = i.Pizza.PizzaAmount,
                    ItemCount = i.ItemCount,
                    OrderItemAmount = i.ItemCount * i.Pizza.PizzaAmount
                }).ToList()
            }).FirstOrDefault();
            return order;
        }

        public async Task DropOrder(int orderId)
        {
            if (!await _orderExist(orderId))
            {
                var errMsg = $"Failed to find the order with id : {orderId}";
                _logger.LogError(errMsg);
                throw new KeyNotFoundException(errMsg);
            }
            var order = _context.Orders.Find(orderId);
            var details = _context.OrderItems.Where(oi => oi.OrderId == orderId).ToList();
            if(details.Count() != 0)
            {
                _logger.LogInformation("Deleteing the orderItems of the order from OrderItems DB");
                _context.OrderItems.RemoveRange(details);
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Succesfully deleted the order with id : {orderId}");
        }

        private async Task<bool> _orderExist(int orderId)
        {
            if(await _context.Orders.FindAsync(orderId) == null)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> _customerExist(string email)
        {
            if (await _context.Customers.FindAsync(email) == null)
            {
                return false;
            }
            return true;
        }

    }
}
