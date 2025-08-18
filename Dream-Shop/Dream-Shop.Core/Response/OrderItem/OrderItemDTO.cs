namespace Dream_Shop.Core.Response.OrderItem;

public class OrderItemDTO
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}