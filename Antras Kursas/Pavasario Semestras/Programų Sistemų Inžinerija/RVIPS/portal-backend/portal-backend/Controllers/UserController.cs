using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal_backend.Services;

namespace portal_backend.Controllers;

[ApiController]
[Route("user")]
public class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
        var user = _userService.GetUser(userId);
        
        return Ok(user);
    }
}