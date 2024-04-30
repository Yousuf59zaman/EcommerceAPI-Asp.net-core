using Microsoft.AspNetCore.Http;
using EcommerceAPI.Models;
using EcommerceAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }


        // Place a new order with order details
        [HttpPost("placeorder")]
        public async Task<ActionResult<Order>> PlaceOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
        }

        // Retrieve detailed information about a specific order by OrderID
        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrderById(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }
            return order;
        }






        // Retrieve all orders for a specific user by UserID
        /*
        * This method handles the HTTP GET request to retrieve all orders for a specific user by UserID.
        */

        [HttpGet("user/{userId}")] // Specifies the HTTP GET route for retrieving orders by user ID
        public async Task<ActionResult<List<Order>>> GetOrdersByUserId(int userId) // Method signature with async Task that returns orders based on user ID
        {
            var orders = await _context.Orders
                                .Include(o => o.OrderDetails)
                                .ThenInclude(od => od.Product)
                                .Where(o => o.UserId == userId)
                                .ToListAsync(); // Retrieve orders with details and products for the specific user

            if (orders == null || orders.Count == 0) // Check if no orders are found
            {
                return NotFound("No orders found for this user."); // Return a not found response with a message
            }

            return orders; // Return the list of orders for the user
        }


        /*
        * This method retrieves detailed information about a specific order by OrderID.
        */

        // Specifies the HTTP GET route for retrieving an order by OrderID
        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrderById(int orderId)
        {
            // Retrieve the order with detailed information including OrderDetails and associated Product
            var order = await _context.Orders
                                    .Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                    .FirstOrDefaultAsync(o => o.OrderId == orderId);

            // Check if order is not found
            if (order == null)
            {
                return NotFound("Order not found."); // Return a not found response with a message
            }

            return order; // Return the specific order
        }

        /*
         * Update the status of an order
         */

        // Specifies the HTTP PUT route for updating the status of an order with a specific ID
        [HttpPut("updatestatus/{id}")]
        // Method signature with async Task that updates the status of an order based on ID and new status
        public async Task<IActionResult> UpdateOrderStatus(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id); // Retrieve the order by ID

            if (order == null) // Check if order is not found
            {
                return NotFound(); // Return a not found response
            }

            order.Status = status; // Update the status of the order
            await _context.SaveChangesAsync(); // Save the changes to the database
            return Ok("Updated Status Successfully"); // Return no content response upon successful update
        }   

    }

}
