using Microsoft.AspNetCore.Mvc;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces.Services;

namespace NShoppingCart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("checkout/{userId}")]
    public async Task<IActionResult> Checkout(int userId, [FromBody] Order request)
    {
        var order = await _orderService.PlaceOrderAsync(userId, request.ShippingAddress, request.Notes);
        return Ok(order);
    }

    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        var orders = await _orderService.GetUserOrderHistoryAsync(userId);
        return Ok(orders);
    }
}