using Dream_Shop.Database;
using Dream_Shop.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Dream_Shop.Core.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<List<Order>> FindByUserId(Guid userId);
    Task<Order?> FindById(Guid id);
}

public class OrderRepository(AppDbContext db) : BaseRepository<Order>(db), IOrderRepository
{
    public async Task<List<Order>> FindByUserId(Guid userId)
    {
        return await _db.Orders
            .Where(x => x.UserId == userId)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .ToListAsync();
    }

    public async Task<Order?> FindById(Guid id)
    {
        return await _db.Orders.FindAsync(id);
    }
}