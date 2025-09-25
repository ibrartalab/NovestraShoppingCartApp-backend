using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NShoppingCart.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductName { get; set; } = string.Empty; // Snapshot of product name

        [StringLength(100)]
        public string? ProductSKU { get; set; } // Snapshot of product SKU

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; } // Price at the time of order

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; } = 0;

        [StringLength(500)]
        public string? ProductImageUrl { get; set; } // Snapshot of product image

        // Navigation properties
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;

        // Computed properties
        [NotMapped]
        public decimal TotalPrice => (UnitPrice * Quantity) - DiscountAmount;

        [NotMapped]
        public decimal OriginalTotalPrice => UnitPrice * Quantity;
    }
}