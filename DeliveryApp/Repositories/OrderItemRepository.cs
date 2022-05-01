using DeliveryApp.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DeliveryDBContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderItemRepository(DeliveryDBContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddOrderItem(OrderItem orderItem)
        {
            if (!await _orderExist(orderItem.OrderId))
            {
                var errMsg = $"Failed to find the order with id : {orderItem.OrderId}";
                _logger.LogError(errMsg);
                throw new KeyNotFoundException(errMsg);
            }

            _context.Orders.FirstOrDefault(o => o.OrderId == orderItem.OrderId).OrderItems.Add(orderItem);
            _logger.LogInformation($"Updating the total amount of order with id  : {orderItem.OrderId}");
            _context.Orders.FirstOrDefault(o => o.OrderId == orderItem.OrderId).OrderTotal += orderItem.OrderItemAmount;
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Successfully added the order item to order with id : {orderItem.OrderId}");

        }

        public async Task UpdateOrderItem(OrderItem orderItem)
        {
            if (!await _orderExist(orderItem.OrderId))
            {
                var errMsg = $"Failed to find the order with id : {orderItem.OrderId}";
                _logger.LogError(errMsg);
                throw new KeyNotFoundException(errMsg);
            }

            var order = _context.Orders.Find(orderItem.OrderId);
            var item = _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == orderItem.OrderItemId);

            _logger.LogInformation($"Changing the count of order item to {orderItem.ItemCount}");
            item.ItemCount = orderItem.ItemCount;
            _logger.LogInformation("Changing the total amount of order");
            order.OrderTotal = order.OrderTotal - item.OrderItemAmount + orderItem.OrderItemAmount;
            _logger.LogInformation("Changing the total amount of order item");
            item.OrderItemAmount = orderItem.OrderItemAmount;

            await _context.SaveChangesAsync();
            _logger.LogDebug($"Succesfully updated the details of order item in order with id : {orderItem.OrderItemId}");
        }

        public async Task DropOrderItem(int orderItemId, int orderId)
        {
            if (!await _orderExist(orderId))
            {
                var errMsg = $"Failed to find the order with id : {orderId}";
                _logger.LogError(errMsg);
                throw new KeyNotFoundException(errMsg);
            }

            var order = _context.Orders.Find(orderId);
            var item = _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == orderItemId);
            if (item == null)
            {
                var errMsg = $"Failed to find the order item with id : {orderItemId}";
                _logger.LogError(errMsg);
                throw new KeyNotFoundException(errMsg);
            }

            _context.OrderItems.Remove(item);
            _logger.LogInformation("Changing the total amount of the order after removing the order item");
            order.OrderTotal -= item.OrderItemAmount;
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Succesfully deleted the order item with id : {orderItemId} from order with id : {orderId}");
        }

        public async Task<bool> OrderItemExist(int orderItemId)
        {
            if (await _context.OrderItems.FindAsync(orderItemId) == null)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> _orderExist(int orderId)
        {
            if (await _context.Orders.FindAsync(orderId) == null)
            {
                return false;
            }
            return true;
        }

    }
}
