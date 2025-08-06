using Dream_Shop.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Dream_Shop.Core.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<List<Product>> findByCategoryName(string categoryName);

    Task<List<Product>> findByBrand(string brandName);

    Task<List<Product>> findByCategoryNameAndBrand(string categoryName, string brand);

    Task<List<Product>> findByName(string name);

    Task<List<Product>> findByBrandAndName(string brand, string name);
    
    Task<List<Product>> GetAllProducts();
    
    Task<Product?> findById(Guid id);

    int countByBrandAndName(string brand, string name);

    Task<bool> existsByNameAndBrand(string name, string brand);
}

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public async Task<List<Product>> findByCategoryName(string categoryName)
    {
        return await _db.Products
            .Where(x => x.Category.Name == categoryName)
            .Include(x => x.Category)
            .Include(x => x.Images)
            .ToListAsync();
    }

    public async Task<List<Product>> findByBrand(string brandName)
    {
        return await _db.Products
            .Where(x => x.Brand == brandName)
            .Include(x => x.Category)
            .Include(x => x.Images)
            .ToListAsync();
    }

    public async Task<List<Product>> findByCategoryNameAndBrand(string categoryName, string brand)
    {
        return await _db.Products
            .Where(x => x.Category.Name == categoryName && x.Brand == brand)
            .Include(x => x.Category)
            .Include(x => x.Images)
            .ToListAsync();
    }

    public async Task<List<Product>> findByName(string name)
    {
        return await _db.Products
            .Where(x => x.Name == name)
            .Include(x => x.Category)
            .Include(x => x.Images)
            .ToListAsync();
    }

    public async Task<List<Product>> findByBrandAndName(string brand, string name)
    {
        return await _db.Products
            .Where(x => x.Name == name && x.Brand == brand)
            .Include(x => x.Category)
            .Include(x => x.Images)
            .ToListAsync();
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return await _db.Products.ToListAsync();
    }

    public async Task<Product?> findById(Guid id)
    {
        return await _db.Products.FirstOrDefaultAsync(x => x.id == id);
    }

    public int countByBrandAndName(string brand, string name)
    {
        return _db.Products
            .Count(x => x.Name == name && x.Brand == brand);
    }

    public async Task<bool> existsByNameAndBrand(string name, string brand)
    {
        return await _db.Products.AnyAsync(x => x.Name == name && x.Brand == brand);
    }
}