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

        public async Task<CartItem?> GetCartItemByIdAsync(int id)
        {
            return await _dbContext.CartItems.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            return await _dbContext.CartItems.Where(c => c.CartId == cartId).ToListAsync();
        }

        public async Task<CartItem> CreateCartItemAsync(CartItem cartItem)
        {
            await _dbContext.CartItems.AddAsync(cartItem);

            await _dbContext.SaveChangesAsync();

            return cartItem;
        }

        public async Task<bool> DeleteCartItemAsync(int id)
        {
            var cartItem = await _dbContext.CartItems.FirstAsync(c => c.Id == id);

            if (cartItem == null)
            {
                return false;
            }

            _dbContext.CartItems.Remove(cartItem);

            int ch = await _dbContext.SaveChangesAsync();

            return ch > 0;
        }

    }
}