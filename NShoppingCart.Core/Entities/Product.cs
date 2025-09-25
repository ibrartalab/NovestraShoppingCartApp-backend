using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NShoppingCart.Core.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? ImageUrl { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be greater than or equal to 0")]
        public int Stock { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountPrice { get; set; }

        public DateTime? DiscountValidUntil { get; set; }

        [Range(0, 5)]
        public double AverageRating { get; set; } = 0;

        public int ReviewCount { get; set; } = 0;

        // Foreign Key
        [Required]
        public int CategoryId { get; set; }

        // Navigation properties
        public Category Category { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Computed properties
        public decimal CurrentPrice => DiscountPrice.HasValue && DiscountValidUntil > DateTime.UtcNow
            ? DiscountPrice.Value
            : Price;

        public bool IsInStock => Stock > 0;

        public bool IsOnSale => DiscountPrice.HasValue && DiscountValidUntil > DateTime.UtcNow;
    }

}