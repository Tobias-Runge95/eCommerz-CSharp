using Dream_Shop.Core.Repositories;

namespace Dream_Shop.Core.Manager;

public interface IOrderItemManager
{
    
}

public class OrderItemManager : IOrderItemManager
{
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderItemManager(IOrderItemRepository orderItemRepository)
    {
        _orderItemRepository = orderItemRepository;
    }
}