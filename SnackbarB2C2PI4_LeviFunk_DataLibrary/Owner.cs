using System.ComponentModel.DataAnnotations;

namespace SnackbarB2C2PI4_LeviFunk_ClassLibrary
{
    public class Owner
    {
        #region Properties

        public int Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Phone { get; set; }

        // Relational Properties
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        #endregion
    }
}
