namespace Kiosco.Controllers;

using Microsoft.AspNetCore.Mvc;
using Kiosco.Authorization;
using Kiosco.Services;
using Kiosco.Models;

[ApiController]
[Authorize]
[Route("[controller]")]

public class UsersController : ControllerBase
{
    private UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAllLogines();
        return Ok(users);
    }
}