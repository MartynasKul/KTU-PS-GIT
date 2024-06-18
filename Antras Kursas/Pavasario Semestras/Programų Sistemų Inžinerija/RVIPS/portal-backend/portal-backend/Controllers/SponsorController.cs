using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal_backend.models;
using portal_backend.Services;

namespace portal_backend.Controllers;

[ApiController]
[Route("sponsor")]
public class SponsorController : Controller
{
    private readonly SponsorService _sponsorService;

    public SponsorController(SponsorService sponsorService)
    {
        _sponsorService = sponsorService;
    }
    
    [HttpGet]
    [Authorize]
    [Route("{id}")]
    public IActionResult Get(int id)
    {
        var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
        var sponsors = _sponsorService.GetSponsor(userId, id);

        return Ok(sponsors);
    }

    [HttpGet]
    [Authorize]
    [Route("list")]
    public IActionResult GetList()
    {
        var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
        var sponsors = _sponsorService.GetSponsors(userId);

        return Ok(sponsors);
    }

    [HttpPost]
    [Authorize]
    public IActionResult AddSponsor([FromBody] SponsorCreationRequest request)
    {
        
        var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
        _sponsorService.CreateSponsor(request, userId);
        return Ok();
    }
}