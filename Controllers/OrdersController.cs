using backend_shop.Data;
using backend_shop.DTO;
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

        [HttpGet("id")]
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


        [HttpGet("customerId")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByCustomerId(int customerId)
        {
            List<Order> orders = await _context.Orders.ToListAsync();
            List<Order> filteredOrders = new List<Order>(0) { };

            foreach (Order order in orders)
            {
                if (order.customerId.Equals(customerId))
                {
                    filteredOrders.Add(order);
                }
            }

            if (orders != null)
            {
                return filteredOrders;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> AddOrder(OrderDTO request)
        {
            foreach (var item in request.productId)
            {
                var order = new Order()
                {
                    customerId = request.customerId,
                    productId = item
                };
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetAllOrders", request);
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateOrder(int id, Order order)
        {
            if (!id.Equals(order.id) || !_context.Orders.Any(x => x.id.Equals(id)))
            {
                return BadRequest();
            }
            else
            {
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetAllOrders", new { id = order.id }, order);
            }
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
