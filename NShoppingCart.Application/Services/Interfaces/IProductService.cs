using System;
using NShoppingCart.Application.Services.Implementations;

namespace NShoppingCart.Application.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> GetProductByIdAsync(Guid productId);
    Task<ProductDto> CreateProductAsync(ProductDto productDto);
    Task<ProductDto> UpdateProductAsync(Guid productId, ProductDto productDto);
    Task<bool> DeleteProductAsync(Guid productId);
}

