namespace NShoppingCart.Core.Interfaces.Services;

public interface ICartService
{
    Task<Cart> GetCartAsync(int userId);
    Task AddItemToCartAsync(int userId, int productId, int quantity);
    Task RemoveItemFromCartAsync(int userId, int productId);
    Task ClearCartAsync(int userId);
}