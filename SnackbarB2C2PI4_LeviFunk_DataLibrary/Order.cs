using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SnackbarB2C2PI4_LeviFunk_ClassLibrary
{
    public class Order
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Cost {  get; set; }
        [Required]
        public DateTime DateOfOrder { get; set; }
        public bool? IsFavorited { get; set; }
        public string? Status { get; set; }

        // Relational Properties
        public Customer? Customer { get; set; }
        public int? CustomerId { get; set; }

        public Transaction? Transaction { get; set; }
        public int? TransactionId { get; set; }

        public virtual ICollection<Product>? Products { get; set; }

        #endregion
    }
}
