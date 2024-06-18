using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portal_backend.Entities;
using portal_backend.models;
using portal_backend.Services;

namespace portal_backend.Controllers;

[ApiController]
[Route("organization")]
public class OrganizationController : Controller
{
    private readonly RvipsContext _rvipsContext;
    private readonly OrganizationService _organizationService;
    private readonly UserService _userService;
    
    public OrganizationController(RvipsContext rvipsContext, OrganizationService organizationService, UserService userService)
    {
        _rvipsContext = rvipsContext;
        _organizationService = organizationService;
        _userService = userService;
    }
    
    [HttpPost]
    [Authorize]
    [Route("create")]
    public IActionResult Create(
        [FromBody] OrganizationCreationRequest request)
    {
        try
        {
            var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            _organizationService.CreateOrganization(request, userId);
            return new StatusCodeResult(StatusCodes.Status200OK);
        }
        catch(Exception exception)
        {
            return exception.Message switch
            {
                "User can't create organization" => new StatusCodeResult(StatusCodes.Status403Forbidden),
                "User doesn't exist" => new StatusCodeResult(StatusCodes.Status400BadRequest),
                "Organization with same name already exists" => new StatusCodeResult(StatusCodes.Status400BadRequest),
                _ => new StatusCodeResult(StatusCodes.Status500InternalServerError)
            };
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("organization-users")]
    public IActionResult GetOrganizationUsers()
    {
        try
        {
            var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            var users = _userService.GetOrganizationUsers(userId);
            return Ok(users);
        }
        catch(Exception exception)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    [Authorize]
    [Route("rename")]
    public IActionResult RenameOrganization([FromBody] OrganizationRenameRequest request)
    {
        try
        {
            var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            _organizationService.RenameOrganization(request, userId);
            return Ok();
        }
        catch (Exception e)
        {
            return Forbid();
        }
    }
    
    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        try
        {
            var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            
            return Ok(_organizationService.GetOrganization(userId));
        }
        catch (Exception e)
        {
            return Forbid();
        }
    }
    
    [HttpPut]
    [Authorize]
    [Route("configure/imap")]
    public IActionResult ConfigureImap([FromBody] ConfigureOrganizationImapRequest request)
    {
        try
        {
            var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            _organizationService.ConfigureOrganizationImap(request, userId);
            return Ok();
        }
        catch (Exception e)
        {
            return Forbid(e.Message);
        }
    }
    
    [HttpPut]
    [Authorize]
    [Route("configure/smtp")]
    public IActionResult ConfigureSmtp([FromBody] ConfigureOrganizationSmtpRequest request)
    {
        try
        {
            var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            _organizationService.ConfigureOrganizationSmtp(request, userId);
            return Ok();
        }
        catch (Exception e)
        {
            return Forbid(e.Message);
        }
    }
}