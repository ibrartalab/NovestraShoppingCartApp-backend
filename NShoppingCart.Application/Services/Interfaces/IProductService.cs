using NShoppingCart.Core.Entities;

namespace NShoppingCart.Core.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetCatalogAsync();
    Task<Product> GetProductDetailsAsync(int id);
    Task CreateProductAsync(Product product);
    Task UpdateInventoryAsync(int productId, int adjustment);
    Task DeleteProductAsync(int id);
}