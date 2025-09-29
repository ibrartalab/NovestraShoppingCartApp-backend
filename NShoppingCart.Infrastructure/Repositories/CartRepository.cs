using Microsoft.EntityFrameworkCore;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces;
using NShoppingCart.Infrastructure.Data;

namespace NShoppingCart.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly NShoppingCartDbContext _dbContext;

    public CartRepository(NShoppingCartDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Cart?> GetOrCreateCartByUserIdAsync(int userId)
    {
        var cart = await _dbContext.Carts
                                    .Include(c => c.CartItems)
                                    .FirstOrDefaultAsync(u => u.UserId == userId);


        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _dbContext.Carts.Add(cart);

            await _dbContext.SaveChangesAsync();
        }

        return cart;
    }

    public async Task<string> AddItemToCartAsync(int userId, int productId, int quantity)
    {
        var cart = await GetOrCreateCartByUserIdAsync(userId);

        var existingCartItem = cart?.CartItems.FirstOrDefault(c => c.ProductId == productId);

        if (existingCartItem is not null)
        {
            return "Product alrady in your cart!";
        }

        var newCartItem = new CartItem
        {
            CartId = cart.Id,
            ProductId = productId,
            Quantity = quantity
        };

        _dbContext.CartItems.Add(newCartItem);

        await _dbContext.SaveChangesAsync();

        return "Item has been successfully added to your cart";
    }

    public async Task<CartItem?> UpdateCartItemAsync(int userId, int productId, int quantity)
    {
        var cart = await GetOrCreateCartByUserIdAsync(userId);

        var existingCartItem = cart?.CartItems.FirstOrDefault(c => c.ProductId == productId);

        if (existingCartItem != null)
        {
            existingCartItem.Quantity += quantity;

            if (existingCartItem.Quantity <= 0)
            {
                cart?.CartItems.Remove(existingCartItem);
            }
        }

        await _dbContext.SaveChangesAsync();
        return existingCartItem;
    }
}
