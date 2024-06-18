using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal_backend.models;
using portal_backend.Services;

namespace portal_backend.Controllers;

[ApiController]
[Route("project")]

public class ProjectController : Controller
{
    private readonly ProjectService _projectService;
    
    public ProjectController(ProjectService projectService)
    {
        _projectService = projectService;
    }
    
    [HttpGet]
    [Authorize]
    [Route("{id}")]
    public IActionResult GetProject(int id)
    {
        var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
        var project = _projectService.GetProject(userId, id);

        return Ok(project);
    }
    
    [HttpPut]
    [Authorize]
    public IActionResult UpdateProject([FromBody] ProjectUpdateRequest request)
    {
        var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
        _projectService.UpdateProject(userId, request);

        return Ok();
    }
    
    [HttpGet]
    [Authorize]
    [Route("list")]
    public IActionResult GetProjects()
    {
        var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
        var projects = _projectService.GetProjects(userId);

        return Ok(projects);
    }
    
    [HttpGet]
    [Authorize]
    [Route("active-list")]
    public IActionResult GetActiveUserProjects()
    {
        var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
        var projects = _projectService.GetActiveUserProjects(userId);

        return Ok(projects);
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult AddProject([FromBody] ProjectCreationRequest request)
    {
        
        var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
        _projectService.CreateProject(request, userId);
        return Ok();
    }
    
    
    [HttpPost]
    [Authorize]
    [Route("user")]
    public IActionResult AddUserToProject([FromBody] AddUserToProjectRequest request)
    {
        
        var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);

        _projectService.AddUserToProject(userId, request);
        return Ok();
    }
    
    [HttpPost]
    [Authorize]
    [Route("sponsor")]
    public IActionResult AddSponsorToProject([FromBody] AddSponsorToProjectRequest request)
    {
        
        var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);

        _projectService.AddSponsorToProject(userId, request);
        return Ok();
    }
}