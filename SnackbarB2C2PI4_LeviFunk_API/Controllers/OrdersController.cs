using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SnackbarB2C2PI4_LeviFunk_ClassLibrary;
using SnackbarB2C2PI4_LeviFunk_MVC.Data;

namespace SnackbarB2C2PI4_LeviFunk_API
{
    /// <summary>
    /// The controller class
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        /// <summary>
        /// Constructor of the controller
        /// </summary>
        /// <param name="context"></param>
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
                return NotFound();

            return orders;
        }

        // GET: api/Orders
        /// <summary>
        /// Get a list of all orders from a customer
        /// </summary>
        /// <returns>List(Order)</returns>
        [HttpGet("CustomerOrders/{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders(int id)
        {
            List<Order> orders = await _context.Orders.Where(o => o.CustomerId == id).ToListAsync();

            if (orders == null)
                return NotFound();

            return orders;
        }

        // GET: api/Orders/5
        /// <summary>
        /// Get a specific order
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order</returns>
        [HttpGet("SpecificOrder/{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            Order order = await _context.Orders
                .Where(o => o.Id == id)
                .FirstAsync();

            if (order == null)
                return NotFound();

            //Avoid circular reference
            if(order.Customer != null)
            {
                order.CustomerId = order.Customer.Id;
                order.Customer = null;
            }
            if(order.Transaction != null)
            {
                order.TransactionId = order.Transaction.Id;
                order.Transaction = null;
            }
            if(order.Products != null)
            {
                order.Products = null;
            }

            return order;
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
                return BadRequest();

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
        public async Task<ActionResult<Order>> PostOrder([FromBody] Order order) 
        {
            /*if (_context.Orders == null)
                return Problem("Entity set 'SystemDbContext.Orders'  is null.");*/

            // Calculate the cost of the order and add all products to a dictionairy with their amount
            decimal cost = 0;
            Dictionary<int, int> dictProducts = new Dictionary<int, int>();
            if(order.Products != null)
            {
                foreach (Product product in order.Products)
                {
                    cost += product.Price;

                    if (!dictProducts.ContainsKey(product.Id))
                    {
                        dictProducts[product.Id] = 1;
                    }
                    else
                    {
                        dictProducts[product.Id] += 1;
                    }
                }
            }

            //Make sure nothing is null
            if (order.Customer == null)
            { 
                order.Customer = new Customer();
                order.Customer.Id = 0;
            }
                
            if (order.Transaction == null)
            {
                order.Transaction = new Transaction();
                order.Transaction.Id = 0;
            }

            // Create order and add it to the database
            Order o = new Order()
            {
                Cost = cost,
                DateOfOrder = DateTime.Now,
                IsFavorited = false,
                Status = "Has not been ordered",
                CustomerId = order.Customer.Id,
                //Customer = order.Customer,
                //Transaction = order.Transaction,
                //Products = order.Products,
            };

            // Save order
            _context.Orders.Add(o);
            await _context.SaveChangesAsync();

            // Get the list of all order products
            o = _context.Orders.Where(a => a.Id == o.Id).First();

            List<OrderProduct> products = new List<OrderProduct>();
            foreach (KeyValuePair<int, int> kvp in dictProducts)
            {
                OrderProduct orderProduct = new OrderProduct()
                {
                    OrderId = o.Id,
                    ProductId = kvp.Key,
                    Amount = kvp.Value,
                };
                products.Add(orderProduct);
            }

            // Check if the list of products is not empty, then save it
            if (!products.IsNullOrEmpty())
            {
                await PostOrderProduct(products);
            }

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        /// <summary>
        /// Save the list of order products to the database
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        private async Task PostOrderProduct(List<OrderProduct> products)
        {
            if (products.IsNullOrEmpty())
                return;

            foreach(OrderProduct product in products)
            {
                await _context.OrderProducts.AddAsync(product);
            }
            await _context.SaveChangesAsync();            
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
            Order order = await _context.Orders.Where(o => o.Id == id).FirstAsync();

            if (order == null)
                return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Check if the order already exists in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
