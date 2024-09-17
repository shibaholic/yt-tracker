using MediatR;
using Microsoft.AspNetCore.Mvc;

using Application.UseCases;
using Presentation.Extensions;

namespace server.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediadtor)
    {
        _mediator = mediadtor;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request, CancellationToken cancellation)
    {
        var response = await _mediator.Send(request, cancellation);
        
        if (response is null) return BadRequest();

        response.Data.Token = JwtExtension.Generate(response.Data);
        return Ok(response);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellation)
    {
        var response = await _mediator.Send(request, cancellation);

        if (response is null) return BadRequest();

        response.Data.Token = JwtExtension.Generate(response.Data);
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken cancellation)
    {
        var response = await _mediator.Send(request, cancellation);

        if (response is null) return BadRequest();

        // token is not added to the response UserDTO
        // the client needs to authenticate first using the newly registered username and password
        return Ok(response);
    }
}