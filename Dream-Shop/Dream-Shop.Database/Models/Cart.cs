namespace Dream_Shop.Database.Models;

public class Cart
{
    public Guid Id { get; set; }
    public decimal TotalAmount { get; set; }
    public List<CartItem> CartItems { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }


    public void addItem(CartItem item)
    {
        
    }

    public void updateTotalAmount()
    {
        
    }

    public void removeItem(CartItem item)
    {
        
    }
}