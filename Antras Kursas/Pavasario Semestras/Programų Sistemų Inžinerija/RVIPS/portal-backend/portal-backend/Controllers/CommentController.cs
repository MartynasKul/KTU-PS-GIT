using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal_backend.models;
using portal_backend.Services;

namespace portal_backend.Controllers;

[ApiController]
[Route("comment")]
public class CommentController : Controller
{
    private readonly CommentService _commentService;

    public CommentController(CommentService commentService)
    {
        _commentService = commentService;
    }
    
    [HttpGet]
    [Authorize]
    [Route("list")]
    public IActionResult Get(int SponsorId)
    {
        var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
        var commentors = _commentService.GetComments(userId, SponsorId);

        return Ok(commentors);
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult AddComment([FromBody] CommentCreationRequest request)
    {
        
        var userId = int.Parse(User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value!);
        _commentService.CreateComment(request, userId);
        return Ok();
    }
}