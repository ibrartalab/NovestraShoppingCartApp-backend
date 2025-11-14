using System;
using NShoppingCart.Application.Services.Interfaces;
using NShoppingCart.Core.Interfaces;

namespace NShoppingCart.Application.Services.Implementations;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await productRepository.GetAllProductsAsync();
        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description??"",
            Price = p.Price,
            StockQuantity = p.Stock
        });
        return productDtos;
    }
    public async Task<ProductDto> GetProductByIdAsync(Guid productId)
    {
        var product = await productRepository.GetProductByIdAsync(productId);
        if (product is null)
        {
            throw new Exception("Product not found.");
        }
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description??"",
            Price = product.Price,
            StockQuantity = product.Stock
        };
    }
    public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
    {
        var newProduct = new Core.Entities.Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Stock = productDto.StockQuantity
        };
        await productRepository.AddProductAsync(newProduct);
        productDto.Id = newProduct.Id;
        return productDto;
    }
    public async Task<ProductDto> UpdateProductAsync(Guid productId, ProductDto productDto)
    {
        var existingProduct = await productRepository.GetProductByIdAsync(productId);
        if (existingProduct is null)
        {
            throw new Exception("Product not found.");
        }
        existingProduct.Name = productDto.Name;
        existingProduct.Description = productDto.Description;
        existingProduct.Price = productDto.Price;
        existingProduct.Stock = productDto.StockQuantity;

        await productRepository.UpdateProductAsync(existingProduct);
        return productDto;
    }
    public async Task<bool> DeleteProductAsync(Guid productId)
    {
        var existingProduct = await productRepository.GetProductByIdAsync(productId);
        if (existingProduct is null)
        {
            throw new Exception("Product not found.");
        }
        await productRepository.DeleteProductAsync(productId);
        return true;
    }
}
