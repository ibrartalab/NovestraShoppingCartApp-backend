<<<<<<< HEAD
using NShoppingCart.Core.Entities;
=======
using System;
using NShoppingCart.Application.Services.Implementations;
>>>>>>> 947f1ba63b774114116ecd2dee7c55a89a3b20bb

namespace NShoppingCart.Core.Interfaces.Services;

public interface IProductService
{
<<<<<<< HEAD
    Task<IEnumerable<Product>> GetCatalogAsync();
    Task<Product> GetProductDetailsAsync(Guid id);
    Task CreateProductAsync(Product product);
    Task UpdateInventoryAsync(Guid productId, int adjustment);
    Task DeleteProductAsync(Guid id);
}
=======
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> GetProductByIdAsync(Guid productId);
    Task<ProductDto> CreateProductAsync(ProductDto productDto);
    Task<ProductDto> UpdateProductAsync(Guid productId, ProductDto productDto);
    Task<bool> DeleteProductAsync(Guid productId);
}

>>>>>>> 947f1ba63b774114116ecd2dee7c55a89a3b20bb
