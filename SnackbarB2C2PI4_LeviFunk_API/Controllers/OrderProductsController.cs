using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackbarB2C2PI4_LeviFunk_ClassLibrary;
using SnackbarB2C2PI4_LeviFunk_MVC.Data;

namespace SnackbarB2C2PI4_LeviFunk_API
{
    /// <summary>
    /// The controller class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductsController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        /// <summary>
        /// The constructor for the controller
        /// </summary>
        /// <param name="context"></param>
        public OrderProductsController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderProducts
        /// <summary>
        /// Get a list of all products for an order
        /// </summary>
        /// <returns>order.products --> List(OrderProducts)</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProducts(int id)
        {
            List<OrderProduct> orderProducts = await _context.OrderProducts.Where(o => o.OrderId == id).ToListAsync();

            if (orderProducts == null)
            {
                return NotFound();
            }

            return orderProducts;
        }

        // PUT: api/OrderProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update an Orderproduct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderProduct"></param>
        /// <returns></returns>
        [HttpPut("OneOrderProduct/{id}")]
        public async Task<IActionResult> PutOrderProduct(int id, OrderProduct orderProduct)
        {
            if (id != orderProduct.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(orderProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        
        // PUT: api/OrderProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update an Orderproduct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderProducts"></param>
        /// <returns></returns>
        [HttpPut("AllOrderProducts/{id}")]
        public async Task<IActionResult> PutOrderProduct(int id, List<OrderProduct> orderProducts)
        {
            if (id != orderProducts.First().OrderId)
            {
                return BadRequest();
            }

            foreach(OrderProduct op in orderProducts)
            {
                _context.Entry(op).State = EntityState.Modified;
                _context.SaveChanges();
            }

            return NoContent();
        }

        // POST: api/OrderProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create new Orderproduct in the database
        /// </summary>
        /// <param name="orderProduct"></param>
        /// <returns></returns>
        [HttpPost("PostAnOrderProduct")]
        public async Task<ActionResult<OrderProduct>> PostOrderProduct(OrderProduct orderProduct)
        {
            if (_context.OrderProducts == null)
                return Problem("Entity set 'SystemDbContext.OrderProducts'  is null.");

            _context.OrderProducts.Add(orderProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderProduct", new { id = orderProduct.OrderId }, orderProduct);
        }

        // POST: api/OrderProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create new Orderproducts for an order in the database
        /// </summary>
        /// <param name="orderProducts"></param>
        /// <returns></returns>
        [HttpPost("PostAllNewOrderProducts")]
        public async Task<IActionResult> PostOrderProduct(List<OrderProduct> orderProduct)
        {
            if (_context.OrderProducts == null)
                return Problem("Entity set 'SystemDbContext.OrderProducts'  is null.");

            foreach(OrderProduct product in orderProduct)
            {
                _context.OrderProducts.Add(product);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/OrderProducts/5
        /// <summary>
        /// Delete the product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> DeleteOrderProduct(int orderId, int productId)
        {
            OrderProduct orderProduct = await _context.OrderProducts.Where(o => o.OrderId == orderId && o.ProductId == productId).FirstAsync();

            if (orderProduct == null)
            {
                return NotFound();
            }

            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Check if the orderproduct already exists in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool OrderProductExists(int id)
        {
            return (_context.OrderProducts?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
