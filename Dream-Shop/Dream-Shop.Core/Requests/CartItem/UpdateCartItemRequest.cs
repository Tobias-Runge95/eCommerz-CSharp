namespace Dream_Shop.Core.Requests.CartItem;

public class UpdateCartItemRequest
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}