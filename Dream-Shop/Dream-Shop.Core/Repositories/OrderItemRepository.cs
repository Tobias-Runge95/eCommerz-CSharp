using Dream_Shop.Database;
using Dream_Shop.Database.Models;

namespace Dream_Shop.Core.Repositories;

public interface IOrderItemRepository : IBaseRepository<OrderItem>
{
    
}

public class OrderItemRepository(AppDbContext db) :  BaseRepository<OrderItem>(db), IOrderItemRepository
{
    
}