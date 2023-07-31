using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    [Route("register")]
    [HttpPost]
    public IActionResult Register(RegisterRequest request)
    {
        return Ok(request);
    }

    [Route("login")]
    [HttpPost]
    public IActionResult Login(LoginRequest request)
    {
        return Ok(request);
    }
}
