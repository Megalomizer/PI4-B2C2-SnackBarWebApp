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
    public class TransactionsController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        /// <summary>
        /// The constructor of the controller
        /// </summary>
        /// <param name="context"></param>
        public TransactionsController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        /// <summary>
        /// Get a list of all transactions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            List<Transaction> transactions = await _context.Transactions.ToListAsync();

            if (transactions == null)
            {
                return NotFound();
            }

            return transactions;
        }

        // GET: api/Transactions/5
        /// <summary>
        /// Get a specific transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            Transaction tranasction = await _context.Transactions.Where(p => p.Id == id).FirstAsync();

            if (tranasction == null)
            {
                return NotFound();
            }

            return tranasction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a transaction
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            // Set all properties of the product to the modified state
            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create a transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'SystemDbContext.Transactions'  is null.");
            }
            // Save transaction
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            // Update Order status
            Order order = _context.Orders.Where(o => o.Id == transaction.Order.Id).First();
            order.Status = "Ordered";
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }

        // DELETE: api/Transactions/5
        /// <summary>
        /// Delete a transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            Transaction transaction = await _context.Transactions.Where(t => t.Id == id).FirstAsync();

            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Check if the transaction already exists in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool TransactionExists(int id)
        {
            return (_context.Transactions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
