using BuberDinner.Application.Authentication;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [Route("register")]
    [HttpPost]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authenticationResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        return authenticationResult.Match(
            authenticationResult => Ok(MapAuthenticationResponse(authenticationResult)),
            firstError =>
                Problem(
                    statusCode: StatusCodes.Status409Conflict,
                    title: firstError.First().Description
                )
        );
    }

    private AuthenticationResponse MapAuthenticationResponse(
        AuthenticationResult authenticationResult
    )
    {
        return new AuthenticationResponse(
            authenticationResult.User.Id,
            authenticationResult.User.FirstName,
            authenticationResult.User.LastName,
            authenticationResult.User.Email,
            authenticationResult.Token
        );
    }

    [Route("login")]
    [HttpPost]
    public IActionResult Login(LoginRequest request)
    {
        ErrorOr<AuthenticationResult> authenticationResult = _authenticationService.Login(
            request.Email,
            request.Password
        );

        return authenticationResult.Match(
            authenticationResult => Ok(MapAuthenticationResponse(authenticationResult)),
            firstError =>
                Problem(
                    statusCode: StatusCodes.Status409Conflict,
                    title: firstError.First().Description
                )
        );
    }
}
