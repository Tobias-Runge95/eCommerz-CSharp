using Dream_Shop.Core.Exceptions;
using Dream_Shop.Core.Repositories;
using Dream_Shop.Core.Response.Order;
using Dream_Shop.Database.Enums;
using Dream_Shop.Database.Models;

namespace Dream_Shop.Core.Manager;

public interface IOrderManager
{
    Task<Order> CreateOrder(Guid userId);
    Task<OrderDTO>  GetOrder(Guid id);
    Task<List<OrderDTO>> GetUserOrders(Guid userId);
    OrderDTO ConvertToDTO(Order order);
}

public class OrderManager : IOrderManager
{
    
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICartManager _cartManager;

    public OrderManager(IOrderRepository orderRepository, ICartManager cartManager)
    {
        _orderRepository = orderRepository;
        _cartManager = cartManager;
    }

    public async Task<Order> CreateOrder(Guid userId)
    {
        Cart cart = await _cartManager.GetCartByUserId(userId);
        Order order = new Order()
        {
            Id = Guid.NewGuid(),
            OrderDate = DateTime.Now,
            UserId = userId,
            OrderStatus = OrderStatus.PENDING,
        };
        order.OrderItems = await CreateOrderItems(order, cart);
        order.TotalAmount = CalculateTotalAmount(order.OrderItems);
        _orderRepository.Add(order);
        await _orderRepository.SaveChangesAsync();
        await _cartManager.ClearCard(cart.Id);
        return order;
    }

    private decimal CalculateTotalAmount(List<OrderItem> orderItems)
    {
        decimal totalAmount = 0;
        foreach (var item in orderItems)
        {
            totalAmount += (item.Quantity * item.Product.Price);
        }
        
        return totalAmount;
    }

    private async Task<List<OrderItem>> CreateOrderItems(Order order, Cart cart)
    {
        List<OrderItem> orderItems = new List<OrderItem>();
        foreach (CartItem cartItem in cart.CartItems)
        {
            _productRepository.Update(cartItem.Product);
            cartItem.Product.Inventory -= cartItem.Quantity;
            orderItems.Add(new OrderItem(cartItem.Product, order, cartItem.Quantity, cartItem.Product.Price));
        }

        await _productRepository.SaveChangesAsync();
        return orderItems;
    } 
        
    public async Task<OrderDTO> GetOrder(Guid id)
    {
        var order = await _orderRepository.FindById(id) ?? throw new ResourceNotFoundException("Order not found");
        return order.ToOrderDTO();
    }

    public async Task<List<OrderDTO>> GetUserOrders(Guid userId)
    {
        var orders = await _orderRepository.FindByUserId(userId);
        return orders.Select(o => o.ToOrderDTO()).ToList();
    }

    public OrderDTO ConvertToDTO(Order order) => order.ToOrderDTO();
}