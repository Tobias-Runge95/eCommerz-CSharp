using Dream_Shop.Core.Manager;
using Dream_Shop.Core.Requests.User;
using Microsoft.AspNetCore.Mvc;

namespace Dream_Shop.API.Controller;

[Controller]
public class UserController : ControllerBase
{
    private readonly UserManager _userManager;

    public UserController(UserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("/create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUser request, CancellationToken cancellationToken)
    {
        var user = await _userManager.CreateAsync(request, cancellationToken);
        return Ok(user);
    }

    [HttpPost("/update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUser request, CancellationToken cancellationToken)
    {
        await _userManager.UpdateUserAsync(request, cancellationToken);
        return Ok();
    }

    [HttpGet("/get")]
    public async Task<IActionResult> GetUsers([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserAsync(userId, cancellationToken);
        return Ok(user);
    }
}