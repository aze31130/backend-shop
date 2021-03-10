using backend_shop.Data;
using backend_shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly Context _context;
        public OrdersController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders != null)
            {
                return orders;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderByCustomerId(int id)
        {
            List<Order> orders = await _context.Orders.ToListAsync();
            List<Order> filteredOrders = new List<Order>(0) { };

            foreach (Order order in orders)
            {
                if (order.customerId.Equals(id))
                {
                    filteredOrders.Add(order);
                }
            }

            if (orders != null)
            {
                //NEED TO CONVERT LIST<Order> TO ACTIONRESULT
                return null;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAllOrders", new { id = order.id }, order);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

    }
}
