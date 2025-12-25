using Microsoft.AspNetCore.Mvc;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces.Services;

namespace NShoppingCart.Api.Controllers
{
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
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }
        [HttpPost("{userId}/items")]
        public async Task<IActionResult> AddItem(int userId, [FromBody] CartItem request)
        {
            await _cartService.AddItemToCartAsync(userId, request.ProductId, request.Quantity);
            return Ok();
        }
        [HttpDelete("{userId}/items/{productId}")]
        public async Task<IActionResult> RemoveItem(int userId, int productId)
        {
            await _cartService.RemoveItemFromCartAsync(userId, productId);
            return Ok();
        }
        [HttpDelete("{userId}/clear")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _cartService.ClearCartAsync(userId);
            return Ok();
        }
        

    }
}