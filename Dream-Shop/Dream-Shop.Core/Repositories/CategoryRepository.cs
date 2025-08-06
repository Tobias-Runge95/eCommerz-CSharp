using Dream_Shop.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Dream_Shop.Core.Repositories;

public interface ICategoriesRepository : IBaseRepository<Category>
{
    Task<Category?> FindByName(string name);
    Task<bool> ExistsByName(string name);
    
    Task<Category?> FindById(Guid id);
    
    Task<List<Category>> GetAllCategories();
}

public class CategoryRepository : BaseRepository<Category>, ICategoriesRepository
{
    public async Task<Category?> FindByName(string name)
    {
        return await _db.Categories.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<bool> ExistsByName(string name)
    {
        return await _db.Categories.AnyAsync(p => p.Name == name);
    }

    public async Task<Category?> FindById(Guid id)
    {
        return await _db.Categories.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Category>> GetAllCategories()
    {
        return await _db.Categories.ToListAsync();
    }
}