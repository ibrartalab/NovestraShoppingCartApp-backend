using Microsoft.AspNetCore.Mvc;
using NShoppingCart.Core.Interfaces.Services;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart(Guid userId)
    {
        var cart = await _cartService.GetCartAsync(userId);
        return Ok(cart);
    }

    [HttpPost("{userId}/items")]
    public async Task<IActionResult> AddToCart(Guid userId, [FromBody] AddItemRequest request)
    {
        await _cartService.AddItemToCartAsync(userId, request.ProductId, request.Quantity);
        return Ok(new { message = "Item added to cart successfully." });
    }

    [HttpDelete("{userId}/items/{productId}")]
    public async Task<IActionResult> RemoveFromCart(Guid userId, Guid productId)
    {
        await _cartService.RemoveItemFromCartAsync(userId, productId);
        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> ClearCart(Guid userId)
    {
        await _cartService.ClearCartAsync(userId);
        return NoContent();
    }
}

public record AddItemRequest(Guid ProductId, int Quantity);