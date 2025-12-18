using System;
using NShoppingCart.Application.Services.Interfaces;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces;

namespace NShoppingCart.Application.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetCatalogAsync() =>
        await _productRepository.GetAllProductsAsync();

    public async Task<Product> GetProductDetailsAsync(Guid id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null) throw new KeyNotFoundException($"Product with ID {id} not found.");
        return product;
    }

    public async Task CreateProductAsync(Product product)
    {
        // Add business validation (e.g., duplicate SKU or Name check)
        await _productRepository.AddProductAsync(product);
    }

    public async Task UpdateInventoryAsync(Guid productId, int adjustment)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product == null) throw new Exception("Product not found");

        product.Stock += adjustment;
        if (product.Stock < 0) throw new Exception("Stock cannot be negative");

        await _productRepository.UpdateProductAsync(product);
    }

    public async Task DeleteProductAsync(Guid id) =>
        await _productRepository.DeleteProductAsync(id);
}