using Microsoft.EntityFrameworkCore;
using portal_backend.Entities;
using portal_backend.Enums;
using portal_backend.models;
using portal_backend.Models;

namespace portal_backend.Services;
public class CommentService
{
    private readonly RvipsContext _rvipsContext;

    public CommentService(RvipsContext rvipsContext)
    {
        _rvipsContext = rvipsContext;
    }

    public void CreateComment(CommentCreationRequest request, int userId)
    {
        var user = _rvipsContext.Users
            .FirstOrDefault(x => x.Id == userId);

        if(user == null)
        {
            throw new Exception("User doesn't exist");
        }
        if(user.OrganizationId == null)
        {
            throw new Exception("Organization doesn't exist");
        }

        var sponsor =
            _rvipsContext.Sponsors.FirstOrDefault(x =>
                x.Id == request.SponsorId && x.OrganizationId == user.OrganizationId);

        if (sponsor is null)
        {
            throw new Exception("Sponsor doesn't exist for organization");
        }

        var comment = new Comment
        {
            UserId = userId,
            SponsorId = request.SponsorId,
            Date = DateTimeOffset.UtcNow,
            CommentText = request.CommentText,
            
        };
        
        _rvipsContext.Comments.Add(comment);
        
        _rvipsContext.SaveChanges();
    }
    
    public List<CommentResponse> GetComments(int userId, int sponsorId)
    {
        var user = _rvipsContext.Users
            .FirstOrDefault(x => x.Id == userId);

        if (user is null)
        {
            return new List<CommentResponse>();
        }
        if (user.OrganizationId is null) throw new Exception("User is not part of organization");
        
        var sponsor =
            _rvipsContext.Sponsors.FirstOrDefault(x =>
                x.Id == sponsorId && x.OrganizationId == user.OrganizationId);

        if (sponsor is null)
        {
            throw new Exception("Sponsor doesn't exist for organization");
        }
        
        var comments =
            _rvipsContext.Comments
                .Where(x => x.SponsorId == sponsorId)
                .ToList();

        var list = new List<CommentResponse>();

        foreach (var comment in comments)
        {
            var commentUser = _rvipsContext.Users.FirstOrDefault(x => x.Id == comment.UserId);
            
            var item = new CommentResponse()
            {
                Id = comment.Id,
                CommentText = comment.CommentText,
                Date = comment.Date,
                SponsorId = comment.SponsorId,
                User = commentUser ?? new User()
            };
            list.Add(item);
        }

        list.Reverse();
        
        return list;
    }   
}