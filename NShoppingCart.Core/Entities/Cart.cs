using System.ComponentModel.DataAnnotations;

namespace NShoppingCart.Core.Entities
{
    public class Cart : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}