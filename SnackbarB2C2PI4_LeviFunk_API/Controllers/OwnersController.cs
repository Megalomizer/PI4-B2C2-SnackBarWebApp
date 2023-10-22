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
    public class OwnersController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        /// <summary>
        /// The constructor of the controller
        /// </summary>
        /// <param name="context"></param>
        public OwnersController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        /// <summary>
        /// Get a list of all owners
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetOwners()
        {
            List<Owner> owners = await _context.Owners.ToListAsync();

            if (owners == null)
            {
                return NotFound();
            }

            return owners;
        }

        // GET: api/Transactions/5
        /// <summary>
        /// Get a specific Owner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetOwner(int id)
        {
            Owner owner = await _context.Owners.Where(p => p.Id == id).FirstAsync();

            if (owner == null)
            {
                return NotFound();
            }

            return owner;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a owner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOwner(int id, Owner owner)
        {
            if (id != owner.Id)
            {
                return BadRequest();
            }

            // Set all properties of the product to the modified state
            _context.Entry(owner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(id))
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
        /// Create an owner
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Owner>> PostOwner(Owner owner)
        {
            if (_context.Owners == null)
            {
                return Problem("Entity set 'SystemDbContext.Transactions'  is null.");
            }
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = owner.Id }, owner);
        }

        // DELETE: api/Transactions/5
        /// <summary>
        /// Delete an owner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            Owner owner = await _context.Owners.Where(t => t.Id == id).FirstAsync();

            if (owner == null)
            {
                return NotFound();
            }

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Check if an owner already exists in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool OwnerExists(int id)
        {
            return (_context.Owners?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
