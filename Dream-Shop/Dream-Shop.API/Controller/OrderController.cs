using Dream_Shop.Core.Manager;
using Dream_Shop.Core.Response.Order;
using Microsoft.AspNetCore.Mvc;

namespace Dream_Shop.API.Controller;

[Controller, Route("order")]
public class OrderController : ControllerBase
{
    private readonly IOrderManager _orderManager;

    public OrderController(IOrderManager orderManager)
    {
        _orderManager = orderManager;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder([FromRoute] Guid userId)
    {
        var result = await _orderManager.CreateOrder(userId);
        var orderDTO = _orderManager.ConvertToDTO(result);
        return Ok(orderDTO);
    }

    [HttpGet("get/{orderId}")]
    public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId)
    {
        var order = await _orderManager.GetOrder(orderId);
        return Ok(order);
    }

    [HttpGet("get-all/{userId}")]
    public async Task<IActionResult> GetAllOrders([FromRoute] Guid userId)
    {
        var orders = await _orderManager.GetUserOrders(userId);
        return Ok(orders);
    }
}