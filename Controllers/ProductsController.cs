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
    public class ProductsController : ControllerBase
    {
        private readonly Context _context;
        public ProductsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var produts = await _context.Products.FindAsync(id);
            if (produts != null)
            {
                return produts;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAllProducts", new { id = product.id }, product);
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateProducts(int id, Product product)
        {
            if (!id.Equals(product.id) || !_context.Products.Any(x => x.id.Equals(id)))
            {
                return BadRequest();
            }
            else
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetAllProducts", new { id = product.id }, product);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> RemoveProduct(int id)
        {
            var produts = await _context.Products.FindAsync(id);
            if (produts == null)
            {
                return NotFound();
            }

            _context.Products.Remove(produts);
            await _context.SaveChangesAsync();

            return produts;
        }
    }
}
