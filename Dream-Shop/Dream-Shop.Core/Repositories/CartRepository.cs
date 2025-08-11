using Dream_Shop.Database;
using Dream_Shop.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Dream_Shop.Core.Repositories;

public interface ICartRepository : IBaseRepository<Cart>
{
    Task<Cart?> GetCartByUserId(Guid userId);
    Task<Cart?> GetCartById(Guid cartId);
    Task<decimal> GetTotalAmount(Guid cartId);
}

public class CartRepository(AppDbContext db) : BaseRepository<Cart>(db), ICartRepository
{
    public async Task<Cart?> GetCartByUserId(Guid userId)
    {
        return await _db.Carts
            .Where(c => c.UserId == userId)
            .Include(x => x.CartItems)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync();
    }

    public async Task<Cart?> GetCartById(Guid cartId)
    {
        return await _db.Carts
            .Where(c => c.Id == cartId)
            .Include(x => x.CartItems)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync();
    }

    public async Task<decimal> GetTotalAmount(Guid cartId)
    {
        return await _db.Carts
            .Where(c => c.Id == cartId)
            .Select(c => c.TotalAmount)
            .FirstOrDefaultAsync();
    }
}