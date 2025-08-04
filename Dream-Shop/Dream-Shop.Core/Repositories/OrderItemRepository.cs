using Dream_Shop.Database.Models;

namespace Dream_Shop.Core.Repositories;

public interface IOrderItemRepository : IBaseRepository<OrderItem>
{
    
}

public class OrderItemRepository :  BaseRepository<OrderItem>, IOrderItemRepository
{
    
}