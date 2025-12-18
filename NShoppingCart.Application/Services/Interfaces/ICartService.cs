namespace NShoppingCart.Core.Interfaces.Services;

public interface ICartService
{
    Task<Cart> GetCartAsync(Guid userId);
    Task AddItemToCartAsync(Guid userId, Guid productId, int quantity);
    Task RemoveItemFromCartAsync(Guid userId, Guid productId);
    Task ClearCartAsync(Guid userId);
}