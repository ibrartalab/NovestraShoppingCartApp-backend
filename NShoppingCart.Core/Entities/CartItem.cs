using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NShoppingCart.Core.Entities
{
    public class CartItem : BaseEntity
    {
        [Required]
        public int CartId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; } // Price at the time of adding to cart

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Cart Cart { get; set; } = null!;
        public Product Product { get; set; } = null!;

        // Computed properties
        [NotMapped]
        public decimal TotalPrice => UnitPrice * Quantity;

        [NotMapped]
        public decimal Savings => (Product?.Price - UnitPrice) * Quantity ?? 0;
    }
}