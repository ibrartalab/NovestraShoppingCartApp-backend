using System;
using Microsoft.EntityFrameworkCore;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces;
using NShoppingCart.Infrastructure.Data;

namespace NShoppingCart.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly NShoppingCartDbContext _dbContext;

    public ProductRepository(NShoppingCartDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }
    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task AddProductAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);

        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateProductAsync(Product product)
    {
        var exProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

        if (exProduct == null)
        {
            _dbContext.Entry(exProduct).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task DeleteProductAsync(int id)
    {
        var exProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (exProduct == null)
        {
            _dbContext.Products.Remove(exProduct);

            await _dbContext.SaveChangesAsync();
        }
    }
}
