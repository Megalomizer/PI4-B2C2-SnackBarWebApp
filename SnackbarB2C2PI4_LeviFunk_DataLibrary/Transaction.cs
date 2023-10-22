using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnackbarB2C2PI4_LeviFunk_ClassLibrary
{
    public class Transaction
    {
        #region Properties

        public int Id { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }
        public int? Discount { get; set; }
        [Required]
        public DateTime DateOfTransaction { get; set; }

        // Relational Properties
        public Customer? Customer { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Order? Order { get; set; }
        [Required]
        public int OrderId { get; set; }

        #endregion
    }
}
