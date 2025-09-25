using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NShoppingCart.Core.Enums;
using NShoppingCart.Core.Entities;

namespace NShoppingCart
{
    public class Order : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; } = string.Empty; // Unique order identifier

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubtotalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingAmount { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = OrderStatus.Pending;

        [StringLength(50)]
        public string PayStatus { get; set; } = PaymentStatus.Pending;

        [StringLength(100)]
        public string? PaymentMethod { get; set; }

        [StringLength(100)]
        public string? PaymentTransactionId { get; set; }

        // Shipping Information
        [Required]
        [StringLength(200)]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ShippingCity { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ShippingState { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string ShippingZipCode { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ShippingCountry { get; set; } = string.Empty;

        [StringLength(20)]
        public string? TrackingNumber { get; set; }

        public DateTime? ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Computed properties
        [NotMapped]
        public bool IsCompleted => Status == OrderStatus.Delivered;

        [NotMapped]
        public bool IsCancelled => Status == OrderStatus.Cancelled;

        [NotMapped]
        public bool CanBeCancelled => Status is OrderStatus.Pending or OrderStatus.Processing;

        [NotMapped]
        public int TotalItems => OrderItems?.Sum(item => item.Quantity) ?? 0;

        [NotMapped]
        public string FormattedOrderNumber => $"ORD-{OrderNumber}";
    }
}