namespace Dream_Shop.Database.Models;

public class CartItem
{

    public CartItem(int quantity, decimal unitPrice, Guid cartId, Guid productId)
    {
        Quantity = quantity;
        UnitPrice = unitPrice;
        CartId = cartId;
        ProductId = productId;
    }
    
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Price { get; set; }
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
    public Cart Cart { get; set; }
    public Guid CartId { get; set; }

    public void SetTotalPrice()
    {
        
    }
}