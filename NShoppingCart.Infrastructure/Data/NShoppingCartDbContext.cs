using Microsoft.EntityFrameworkCore;
using NShoppingCart.Core.Entities;

namespace NShoppingCart.Infrastructure.Data;

public class NShoppingCartDbContext : DbContext
{
    public NShoppingCartDbContext(DbContextOptions<NShoppingCartDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<User> Users { get; set; }

}
