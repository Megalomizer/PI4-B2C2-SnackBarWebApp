using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SnackbarB2C2PI4_LeviFunk_ClassLibrary
{
    public class Customer
    {
        #region Properties

        public int Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Phone {  get; set; }
        public string? AuthenticationId { get; set; }

        // Relational Properties
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        #endregion
    }
}
