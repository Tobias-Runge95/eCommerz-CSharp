using Dream_Shop.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Dream_Shop.Core.Repositories;

public interface IImageRepository : IBaseRepository<Image>
{
    Task<List<Image>> FindByProductId(Guid productId);
}

public class ImageRepository : BaseRepository<Image>, IImageRepository
{
    public async Task<List<Image>> FindByProductId(Guid productId)
    {
        return await _db.Images
            .Where(x => x.ProductId == productId)
            .ToListAsync();
    }
}