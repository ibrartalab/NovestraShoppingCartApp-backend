using NShoppingCart;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces;
using NShoppingCart.Core.Interfaces.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task<NShoppingCart.Cart> GetCartAsync(Guid userId) => 
        await _cartRepository.GetOrCreateCartByUserIdAsync(userId);

    public async Task AddItemToCartAsync(Guid userId, Guid productId, int quantity)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product == null || product.Stock < quantity)
            throw new Exception("Product unavailable or insufficient stock.");

        var cart = await _cartRepository.GetOrCreateCartByUserIdAsync(userId);
        var existingItem = await _cartRepository.GetCartItemAsync(new Guid(cart.Id.ToString()), productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            await _cartRepository.UpdateCartItemAsync(existingItem);
        }
        else
        {
            await _cartRepository.AddItemToCartAsync(new Guid(cart.Id.ToString()), new CartItem 
            { 
                ProductId = productId, 
                Quantity = quantity 
            });
        }
    }

    public async Task RemoveItemFromCartAsync(Guid userId, Guid productId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null) return;

        var item = await _cartRepository.GetCartItemAsync(new Guid(cart.Id.ToString()), productId);
        if (item != null) await _cartRepository.DeleteCartItemAsync(new Guid(item.Id.ToString()));
    }

    public async Task ClearCartAsync(Guid userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart != null) await _cartRepository.ClearCartAsync(new Guid(cart.Id.ToString()));
    }
}