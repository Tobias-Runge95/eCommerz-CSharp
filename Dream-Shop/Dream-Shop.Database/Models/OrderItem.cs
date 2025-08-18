namespace Dream_Shop.Database.Models;

public class OrderItem
{
    public OrderItem()
    {
        
    }
    
    public OrderItem(Product product, Order order, int quantity, decimal unitPrice)
    {
        Product = product;
        ProductId = product.id;
        Order = order;
        OrderId = order.Id;
        Quantity = quantity;
        Price = unitPrice;
    }
    
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
    public Order Order { get; set; }
    public Guid OrderId { get; set; }
}