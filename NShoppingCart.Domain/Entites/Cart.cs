using NShoppingCart.Core.Entities;

namespace NShoppingCart
{
    public class Cart:BaseEntity
    {
        public int UserId { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}