using Dream_Shop.Database.Enums;

namespace Dream_Shop.Database.Models;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public OrderStatus OrderStatus { get; set; }
}