using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal_backend.models;
using portal_backend.Services;

namespace portal_backend.Controllers;

[Route("email")]
public class EmailController : Controller
{
    private readonly EmailService _emailService;

    
    public EmailController(EmailService emailService)
    {
        _emailService = emailService;
    }
    
    [HttpGet]
    [Authorize]
    [Route("list")]
    public IActionResult GetReceivedEmails()
    {
        try
        {
            var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            var emails = _emailService.GetReceivedEmails(userId).OrderByDescending(x => x.Date);
            return Ok(emails);
        }
        catch(Exception exception)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("outgoing-list")]
    public IActionResult GetOutgoingEmails()
    {
        try
        {
            var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            var emails = _emailService.GetSentEmails(userId).OrderByDescending(x => x.Date);
            return Ok(emails);
        }
        catch(Exception exception)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("list/{address}")]
    public IActionResult GetReceivedEmailsByAddress(string address)
    {
        try
        {
            var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            var emails = _emailService.GetReceivedEmails(userId, address).OrderByDescending(x => x.Date);
            return Ok(emails);
        }
        catch(Exception exception)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("outgoing-list/{address}")]
    public IActionResult GetOutgoingEmailsByAddress(string address)
    {
        try
        {
            var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            var emails = _emailService.GetSentEmails(userId, address).OrderByDescending(x => x.Date);
            return Ok(emails);
        }
        catch(Exception exception)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    [Authorize]
    [Route("send")]
    public IActionResult SendEmail([FromBody] SendEmailRequest request)
    {
        try
        {
            var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
            _emailService.SendEmail(userId, request);
            return Ok();
        }
        catch (Exception e)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}