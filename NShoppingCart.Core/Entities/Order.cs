using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NShoppingCart.Core.Enums;
using NShoppingCart.Core.Entities;

namespace NShoppingCart.Core.Entities;

public class Order : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(50)]
    public string OrderNumber { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = OrderStatus.Pending;

    [Required]
    [StringLength(50)]
    public string ShippingAddress { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Notes { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}