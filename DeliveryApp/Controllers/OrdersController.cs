using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryApp.Models;
using DeliveryApp.Repositories;
using DeliveryApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DeliveryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderRepository orderRepository, 
            IOrderItemRepository orderItemRepository, ILogger<OrdersController> logger)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _logger = logger;
        }

        //GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderVM>>> GetOrdersByCustomerAndDate(OrderVM order)
        {
            _logger.LogInformation($"Retrive all the order for the date : {order.OrderDate} for the customer : {order.CustomerEmail}");
            try
            {
                if (order.OrderDate.Value.Date > DateTime.Now.Date)
                {
                    var errMsg = $"Fail to retrive orders for with date : {order.OrderDate}. Date is for future";
                    _logger.LogError(errMsg);
                    return StatusCode(StatusCodes.Status400BadRequest, new { status = "Fail", message = errMsg });
                }
                var orders = await _orderRepository.GetOrdersByCustomerAndDate(order);
                return Ok(orders);
            }
            catch (KeyNotFoundException ex)
            {
                var errMsg = $"Fail to retrive order : {ex.Message}";
                _logger.LogError(errMsg);
                return StatusCode(StatusCodes.Status404NotFound, new { status = "Fail", message = errMsg });
            }
            catch (Exception ex)
            {
                var errMsg = $"Fail to retrive order : {ex.Message}";
                _logger.LogError(errMsg);
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "Fail", message = errMsg });
            }
        }

        //GET: api/orders/GetOrderById/7
        [HttpGet]
        [Route("GetOrderById/{id}")]
        public async Task<ActionResult<OrderVM>> GetOrder(int id)
        {
            try
            {
                var order = await _orderRepository.GetOrderById(id);
                if (order == null)
                {
                    var errMsg = $"Order is null. Failed to retrieve order with id : {id}";
                    _logger.LogError(errMsg);
                    return NotFound(new { status = "Fail", message = errMsg });
                }

                _logger.LogDebug($"Successfully Returned the order with id : {id}");
                return Ok(order);

            }
            catch (KeyNotFoundException ex)
            {
                var errMsg = $"Fail to retrive order : {ex.Message}";
                _logger.LogError(errMsg);
                return StatusCode(StatusCodes.Status404NotFound, new { status = "Fail", message = errMsg });
            }
            catch (Exception ex)
            {
                var errMsg = $"Fail to retrive order : {ex.Message}";
                _logger.LogError(errMsg);
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "Fail", message = errMsg });
            }

        }

        // PUT: api/Orders
        [HttpPut]
        public async Task<IActionResult> PutOrder(OrderItem itemDetails)
        {
            _logger.LogInformation($"Update the order with id : {itemDetails.OrderId}");
            try
            {
                if(await _orderItemRepository.OrderItemExist(itemDetails.OrderItemId))
                {
                    _logger.LogInformation($"Updating the existing order item in order with id : {itemDetails.OrderId}");
                    await _orderItemRepository.UpdateOrderItem(itemDetails);
                    return Ok("Successfully updated the order details");
                } else
                {
                    _logger.LogInformation($"Adding the new order item in order with id : {itemDetails.OrderId}");
                    await _orderItemRepository.AddOrderItem(itemDetails);
                    return Ok("Successfully added the order item");
                }
                
            }
            catch (KeyNotFoundException ex)
            {
                var errMsg = $"Fail to update order details : {ex.Message}";
                _logger.LogError(errMsg);
                return StatusCode(StatusCodes.Status400BadRequest, new { status = "Fail", message = errMsg });
            }
            catch (Exception ex)
            {
                var errMsg = $"Fail to retrive order : {ex.Message}";
                _logger.LogError(errMsg);
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "Fail", message = errMsg });
            }
            
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> PostOrder(Order order)
        {
            _logger.LogInformation("Add Order");
            try
            {
                await _orderRepository.AddOrder(order);
                return Ok("Successfully inserted order");
            } 
            catch (KeyNotFoundException ex) 
            {
                var errMsg = $"Fail to add order : {ex.Message}";
                _logger.LogDebug("errMsg");
                return StatusCode(StatusCodes.Status400BadRequest, new { status = "Fail", message = errMsg });
            }
            catch (Exception ex)
            {
                var errMsg = $"Fail to add order : {ex.Message}";
                _logger.LogError(errMsg);
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "Fail", message = errMsg });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderItem(OrderItem itemDetails)
        {
            _logger.LogInformation($"Delete the order item of order with id : {itemDetails.OrderId}");
            try
            {
                await _orderItemRepository.DropOrderItem(itemDetails.OrderItemId, itemDetails.OrderId);
                return Ok("Successfully deleted the order item");
            }
            catch (KeyNotFoundException ex)
            {
                var errMsg = $"Fail to delete order item: {ex.Message}";
                _logger.LogError(errMsg);
                return StatusCode(StatusCodes.Status400BadRequest, new { status = "Fail", message = errMsg });
            }
            catch (Exception ex)
            {
                var errMsg = $"Fail to retrive order : {ex.Message}";
                _logger.LogError(errMsg);
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "Fail", message = errMsg });
            }

        }

        //DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            _logger.LogInformation($"Delete the order with id : {id}");
            try
            {
                await _orderRepository.DropOrder(id);
                return Ok("Successfully deleted the order");
            }
            catch (KeyNotFoundException ex)
            {
                var errMsg = $"Fail to retrive order : {ex.Message}";
                _logger.LogError(errMsg);
                return StatusCode(StatusCodes.Status404NotFound, new { status = "Fail", message = errMsg });
            }
            catch (Exception ex)
            {
                var errMsg = $"Fail to retrive order : {ex.Message}";
                _logger.LogError(errMsg);
                return StatusCode(StatusCodes.Status500InternalServerError, new { status = "Fail", message = errMsg });
            }
        }

    }
}
