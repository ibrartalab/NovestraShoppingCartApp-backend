using Microsoft.AspNetCore.Mvc;
using Moq;
using NShoppingCart.Core.Interfaces.Services;

public class CartControllerTests
{
    private readonly Mock<ICartService> _cartServiceMock;
    private readonly CartController _controller;

    public CartControllerTests()
    {
        _cartServiceMock = new Mock<ICartService>();
        _controller = new CartController(_cartServiceMock.Object);
    }

    [Fact]
    public async Task AddToCart_ShouldReturnOk_WhenSuccessful()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new AddItemRequest(Guid.NewGuid(), 2);

        // Act
        var result = await _controller.AddToCart(userId, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        _cartServiceMock.Verify(s => s.AddItemToCartAsync(userId, request.ProductId, request.Quantity), Times.Once);
    }

    [Fact]
    public async Task ClearCart_ShouldReturnNoContent()
    {
        // Act
        var result = await _controller.ClearCart(Guid.NewGuid());

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}