using Dream_Shop.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dream_Shop.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("shop");
        
        var user = builder.Entity<User>();
        user
            .HasOne(c => c.Cart)
            .WithOne(u => u.User)
            .HasForeignKey<User>(u => u.CartId)
            .OnDelete(DeleteBehavior.Cascade);
        
        var product = builder.Entity<Product>();
        product.HasKey(k => k.id);
        product
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(k => k.CategoryId);
        
        var category = builder.Entity<Category>();
        category.HasKey(k => k.Id);
        
        var images = builder.Entity<Image>();
        images.HasKey(k => k.Id);
        images.HasOne(p => p.Product)
            .WithMany(i => i.Images)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        
        var cart = builder.Entity<Cart>();
        cart.HasKey(k => k.Id);
        
        var cartItem = builder.Entity<CartItem>();
        cartItem.HasKey(k => k.Id);
        cartItem.HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        
        cartItem.HasOne(p => p.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);
        
        var order =  builder.Entity<Order>();
        order.HasKey(o => o.Id);
        order.HasOne(u => u.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        var orderItems = builder.Entity<OrderItem>();
        orderItems.HasKey(o => o.Id);
        orderItems
            .HasOne(u => u.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        orderItems
            .HasOne(u => u.Product)
            .WithMany()
            .HasForeignKey(o => o.ProductId);
    }
}