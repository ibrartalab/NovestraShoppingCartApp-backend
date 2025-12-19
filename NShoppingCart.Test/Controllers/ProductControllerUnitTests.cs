using Microsoft.AspNetCore.Mvc;
using Moq;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces.Services;

public class ProductsControllerTests
{
    private readonly Mock<IProductService> _productServiceMock;
    private readonly ProductsController _controller;

    public ProductsControllerTests()
    {
        _productServiceMock = new Mock<IProductService>();
        _controller = new ProductsController(_productServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WithListOfProducts()
    {
        // Arrange
        var products = new List<Product> { new() { Id = 1, Name = "Laptop" } };
        _productServiceMock.Setup(s => s.GetCatalogAsync()).ReturnsAsync(products);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
        Assert.Single(returnProducts);
    }

    [Fact]
    public async Task GetById_ProductExists_ReturnsOk()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product { Name = "Mouse" };
        _productServiceMock.Setup(s => s.GetProductDetailsAsync(productId)).ReturnsAsync(product);

        // Act
        var result = await _controller.GetById(productId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}