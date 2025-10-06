

using NShoppingCart.Core.Entities;

namespace NShoppingCart.Core.Interfaces;

public interface ICartRepository
{
    Task<Cart> GetCartByIdAsync(int id);

    Task<Cart> GetCartByUserIdAsync(int userId);

    Task<bool> DeleteCartByIdAsync(int id);
}
