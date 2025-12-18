using NShoppingCart.Core.Entities;

namespace NShoppingCart.Core.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetCatalogAsync();
    Task<Product> GetProductDetailsAsync(Guid id);
    Task CreateProductAsync(Product product);
    Task UpdateInventoryAsync(Guid productId, int adjustment);
    Task DeleteProductAsync(Guid id);
}