using Dream_Shop.Core.Manager;
using Dream_Shop.Core.Requests.Auth;
using Microsoft.AspNetCore.Mvc;
using Roll20Helper.Core.Auth;

namespace Dream_Shop.API.Controller;

[Controller, Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthManager _authManager;

    public AuthenticationController(IAuthManager authManager)
    {
        _authManager = authManager;
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authManager.Login(request);
        return Ok(result);
    }

    [HttpDelete("/logout")]
    public async Task<IActionResult> Logout([FromQuery] Guid userId)
    {
        await _authManager.Logout(userId);
        return Ok();
    }
}