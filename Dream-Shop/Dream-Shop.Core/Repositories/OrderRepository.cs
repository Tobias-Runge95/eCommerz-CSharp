using Dream_Shop.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Dream_Shop.Core.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<List<Order>> FindByUserId(Guid userId);
}

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public async Task<List<Order>> FindByUserId(Guid userId)
    {
        return await _db.Orders
            .Where(x => x.UserId == userId)
            .Include(x => x.OrderItems)
            .ThenInclude(x => x.Product)
            .ToListAsync();
    }
}