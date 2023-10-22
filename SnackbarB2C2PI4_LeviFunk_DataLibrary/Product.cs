using System.ComponentModel.DataAnnotations;

namespace SnackbarB2C2PI4_LeviFunk_ClassLibrary
{
    public class Product
    {
        #region Properties

        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int? Discount { get; set; }
        [Required]
        public int Stock { get; set; }
        public string? ImgPath { get; set; }
        public string? Description { get; set; }

        // Relational Properties
        public virtual ICollection<Owner>? Owner { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Customer>? Customers { get; set; }

        #endregion
    }
}
