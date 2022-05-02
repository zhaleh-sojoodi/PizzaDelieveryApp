# Delivery App

## Database Schema
![database](https://github.com/zhaleh-sojoodi/PizzaDelieveryApp/blob/main/images/db_design.JPG)

## API Design 
- GetOrdersByCustomerAndDate : get all the orders for the customer on specific date 
  - [HttpGet] api/orders
- GetOrder : get order by order id
  - [HttpGet] api/orders/GetOrderById/:id
- PutOrder : update the count of existing order item of the order or add new order item to order 
  - [HttpPut] api/orders
- PostOrder : add new order
  - [HttpPost] api/orders
- DeleteOrderItem : delete an order item from the order
  - [HttpDelete] api/orders
- DeleteOrder : delete the whole order
  - [HttpDelete] api/orders/:id
