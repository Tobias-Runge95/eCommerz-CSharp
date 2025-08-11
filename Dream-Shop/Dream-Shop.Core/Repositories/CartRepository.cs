using Dream_Shop.Database;
using Dream_Shop.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Dream_Shop.Core.Repositories;

public interface ICartRepository : IBaseRepository<Cart>
{
    Task<Cart?> GetCartByUserId(Guid userId);
}

public class CartRepository(AppDbContext db) : BaseRepository<Cart>(db), ICartRepository
{
    public async Task<Cart?> GetCartByUserId(Guid userId)
    {
        return await _db.Carts
            .Where(c => c.UserId == userId)
            .Include(x => x.CartItems)
            .FirstOrDefaultAsync();
    }
}