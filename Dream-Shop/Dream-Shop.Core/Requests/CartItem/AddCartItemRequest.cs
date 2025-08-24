﻿namespace Dream_Shop.Core.Requests.CartItem;

public class AddCartItemRequest
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}