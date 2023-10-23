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
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public OrdersController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        /// <summary>
        /// Get a list of all orders
        /// </summary>
        /// <returns>List(Order)</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            List<Order> orders = await _context.Orders.ToListAsync();

            if (orders == null)
            {
                return NotFound();
            }

            return orders;
        }

        // GET: api/Orders/5
        /// <summary>
        /// Get a specific order based on orderid
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order</returns>
        [HttpGet("SpecificOrder/{id}")]
        public async Task<ActionResult<Order>> GetOrderByOrderId(int id)
        {
            Order order = await _context.Orders.Where(o => o.Id == id).FirstAsync();

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // GET: api/Orders/5
        /// <summary>
        /// Get a specific order based on customer id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order</returns>
        [HttpGet("CustomerOrders/{id}")]
        public async Task<ActionResult<List<Order>>> GetOrdersByCustomerId(int id)
        { 
            List<Order> orders = await _context.Orders.Where(o => o.CustomerId == id).ToListAsync();

            if (orders == null)
            {
                return NotFound();
            }

            // Get for each order the list of products (its ugly but it works... for now)
            foreach (Order order in orders)
            {
                order.Customer = null;
                order.Transaction = null;
                order.Products = null;

                List<OrderProduct> op = await _context.OrderProducts.Where(o => o.OrderId == order.Id).ToListAsync();
                foreach(OrderProduct opProduct in op)
                {
                    for(int i = 0; i < opProduct.Amount;  i++)
                    {
                        Product product = await _context.Products.Where(a => a.Id == opProduct.ProductId).FirstAsync();
                        product.Orders = null;
                        order.Products.Add(product);
                    }
                }
            }

            return orders;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update an order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            // Set all properties of the product to the modified state
            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create new Order in the database
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderByOrderId", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        /// <summary>
        /// Delete the order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.Where(o => o.Id == id).FirstAsync();

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
