using System.ComponentModel.DataAnnotations;

namespace NShoppingCart.Core.Entities;

public class CartItem : BaseEntity
{
    [Required]
    public Guid CartId { get; set; }

    [Required]
    public Guid ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }

    // Navigation properties
    public Product Product { get; set; } = null!;
}