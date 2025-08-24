namespace Dream_Shop.Core.Requests.CartItem;

public class DeleteCartItemRequest
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
}