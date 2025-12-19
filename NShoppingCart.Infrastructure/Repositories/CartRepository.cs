using Microsoft.EntityFrameworkCore;
using NShoppingCart.Core.Entities;
using NShoppingCart.Infrastructure.Data;

namespace NShoppingCart
{
    public class CartRepository : ICartRepository
    {
        private readonly NShoppingCartDbContext _dbContext;
        public CartRepository(NShoppingCartDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Cart (container) operations
        public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
        {
            return await _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task<Cart> GetOrCreateCartByUserIdAsync(Guid userId)
        {
            var cart = await _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                return cart;
            }

            var newCart = new Cart { UserId = userId };
            _dbContext.Carts.Add(newCart);
            await _dbContext.SaveChangesAsync();
            return newCart;
        }
        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }
        public async Task<bool> DeleteCartAsync(Guid cartId)
        {
            var cart = await _dbContext.Carts.FindAsync(cartId);
            if (cart == null)
            {
                return false;
            }
            _dbContext.Carts.Remove(cart);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ClearCartAsync(Guid cartId)
        {
            var cartItems = _dbContext.CartItems.Where(ci => ci.CartId == cartId);
            _dbContext.CartItems.RemoveRange(cartItems);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Cart> UpdateCartAsync(Cart cart)
        {
            _dbContext.Carts.Update(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }

        // CartItem operations
        public async Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(Guid cartId)
        {
            var cartItems = _dbContext.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.CartId == cartId);
            return await Task.FromResult(cartItems.AsEnumerable());
        }
        public async Task<CartItem?> GetCartItemByIdAsync(int cartItemId)
        {
            return await _dbContext.CartItems
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId);
        }
        public async Task<CartItem?> GetCartItemAsync(Guid cartId, Guid productId)
        {
            return await _dbContext.CartItems
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
        }
        public async Task<CartItem> AddItemToCartAsync(Guid cartId, CartItem cartItem)
        {
            cartItem.CartId = cartId;
            _dbContext.CartItems.Add(cartItem);
            await _dbContext.SaveChangesAsync();
            return cartItem;
        }
        public async Task<bool> UpdateCartItemAsync(CartItem cartItem)
        {
            _dbContext.CartItems.Update(cartItem);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCartItemAsync(Guid cartItemId)
        {
            var cartItem = await _dbContext.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return false;
            }
            _dbContext.CartItems.Remove(cartItem);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}