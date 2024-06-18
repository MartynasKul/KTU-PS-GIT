using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using portal_backend.Enums;
using portal_backend.models;
using portal_backend.Services;

namespace portal_backend.Controllers;


public class AuthController : Controller
{
    private readonly AuthService _authService;
    private readonly IConfiguration _config;
    private readonly UserService _userService;

    public AuthController(AuthService authService, IConfiguration config, UserService userService)
    {
        _authService = authService;
        _config = config;
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public IActionResult RegisterUser([FromBody]UserRegistrationRequest request)
    {
        try
        {
            _authService.RegisterUser(request);
            return Ok();
        }
        catch(Exception exception)
        {
            return exception.Message switch
            {
                "Not valid email" => BadRequest(),
                "Not valid password" => BadRequest(),
                "User already exists" => Conflict(),
                _ => StatusCode(500)
            };
        }
    }

    [Authorize]
    [HttpPost]
    [Route("change-password")]
    public IActionResult ChangeUserPassword([FromBody] UserChangePasswordRequest request)
    {
        try
        {
            var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            _authService.ChangeUserPassword(userId, request);
            return Ok();
        }
        catch (Exception e)
        {
            return Forbid(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    [Route("add-user")]
    public IActionResult CreateUser([FromBody] UserRegistrationRequest request)
    {
        int adminId;
        try
        {
            adminId = GetUserIdFromClaim();
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        var administrator = _userService.GetUser(adminId);

        if (administrator is null)
        {
            return Forbid();
        }

        if (!administrator.Roles.Exists(x => x.UserType.Equals(UserTypeEnum.Admin)))
        {
            return Forbid();
        }
        
        try
        {
            var newUserId = _authService.RegisterUser(request);
            _userService.AddUserToOrganization(newUserId, administrator.OrganizationId ?? -1);
            return Ok();
        }
        catch(Exception exception)
        {
            return exception.Message switch
            {
                "Not valid email" => BadRequest(),
                "Not valid password" => BadRequest(),
                "User already exists" => Conflict(),
                _ => StatusCode(500)
            };
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public IActionResult LoginUser(
        [FromBody] UserLoginRequest userLoginRequest)
    {
        try
        {
            var user =_authService.LoginUser(userLoginRequest.Email, userLoginRequest.Password);

            if (user is not null)
            {
                return Ok(GenerateToken(user.Id));
            }

            return Unauthorized();
        }
        catch(Exception exception)
        {
            return exception.Message switch
            {
                "User not found" => Unauthorized(),
                _ => StatusCode(500)
            };
        }
    }

    [HttpGet]
    [Authorize]
    [Route("renew-token")]
    public IActionResult RenewToken()
    {
        int userId;
        try
        {
            userId = GetUserIdFromClaim();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
        
        return Ok(GenerateToken(userId));
    }

    private int GetUserIdFromClaim()
    {
        var claimValue = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        if (claimValue is null)
        {
            throw new Exception("Bad request");
        }
        
        return int.Parse(claimValue);
    }

    private string GenerateToken(int userId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userId.ToString()),
            }),
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"]!)),
            SigningCredentials = credentials,
            Issuer = _config["Jwt:Issuer"]!,
            Audience = _config["Jwt:Audience"]!
        };

        var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}