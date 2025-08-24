using Dream_Shop.Core.Manager;
using Dream_Shop.Core.Requests.CartItem;
using Dream_Shop.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dream_Shop.API.Controller;

[Controller, Route("cartItem")]
public class CartItemController : ControllerBase
{
    private readonly ICartItemManager _cartItemManager;
    private readonly ICartManager _cartManager;

    public CartItemController(ICartItemManager cartItemManager, ICartManager cartManager)
    {
        _cartItemManager = cartItemManager;
        _cartManager = cartManager;
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(AddCartItemRequest request)
    {
        var cart = await _cartManager.GetCartByUserId(request.UserId);
        await _cartItemManager.AddItemToCart(cart.Id, request.ProductId, request.Quantity);
        return Ok();
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> Remove([FromBody]  DeleteCartItemRequest request)
    {
        await _cartItemManager.RemoveItemFromCart(request.CartId, request.ProductId);
        return Ok();
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateCartItemRequest request)
    {
        await _cartItemManager.UpdateCartItem(request);
        return Ok();
    }
}