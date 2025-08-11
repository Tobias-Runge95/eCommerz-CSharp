using Dream_Shop.Database;
using Dream_Shop.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Dream_Shop.Core.Repositories;

public interface IImageRepository : IBaseRepository<Image>
{
    Task<List<Image>> FindByProductId(Guid productId);
    Task<Image?> FindImageById(Guid id);
}

public class ImageRepository : BaseRepository<Image>, IImageRepository
{

    public ImageRepository(AppDbContext dbContext) : base(dbContext)
    {
        
    }
    
    public async Task<List<Image>> FindByProductId(Guid productId)
    {
        return await _db.Images
            .Where(x => x.ProductId == productId)
            .ToListAsync();
    }

    public async Task<Image?> FindImageById(Guid id)
    {
        return await _db.Images.FindAsync(id);
    }
}