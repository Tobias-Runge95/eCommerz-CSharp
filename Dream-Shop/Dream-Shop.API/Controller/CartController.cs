using Dream_Shop.Core.Manager;
using Microsoft.AspNetCore.Mvc;

namespace Dream_Shop.API.Controller;

[Controller, Route("cart")]
public class CartController : ControllerBase
{
    private readonly ICartManager _cartManager;

    public CartController(ICartManager cartManager)
    {
        _cartManager = cartManager;
    }

    [HttpGet("get/{cartId}")]
    public async Task<IActionResult> GetCart([FromRoute] Guid cartId)
    {
        var cart = await _cartManager.GetCartById(cartId);
        return Ok(cart);
    }

    [HttpGet("total-price/{cartId}")]
    public async Task<IActionResult> GetTotalPrice([FromRoute] Guid cartId)
    {
        var price = await _cartManager.GetTotalAmount(cartId);
        return Ok(price);
    }

    [HttpDelete("clear/{cartId}")]
    public async Task<IActionResult> ClearCart([FromRoute] Guid cartId)
    {
        await _cartManager.ClearCard(cartId);
        return Ok();
    }
}