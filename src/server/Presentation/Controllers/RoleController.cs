using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

[ApiController]
[Route("role")]
public class RoleController : ControllerBase
{
    [HttpGet("user")]
    [Authorize(Roles = "USER")]
    public ActionResult User()
    {
        return Ok("Hello User");
    }

    [HttpGet("admin")]
    [Authorize(Roles = "ADMIN")]
    public ActionResult Admin()
    {
        return Ok("Hello Admin");
    }
}