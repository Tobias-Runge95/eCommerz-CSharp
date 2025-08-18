using Dream_Shop.Core.Response.OrderItem;

namespace Dream_Shop.Core.Response.Order;

public class OrderDTO
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }
}