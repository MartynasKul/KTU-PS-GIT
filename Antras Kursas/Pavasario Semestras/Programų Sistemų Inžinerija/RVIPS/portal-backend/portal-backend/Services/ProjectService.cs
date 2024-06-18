using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using portal_backend.Entities;
using portal_backend.Enums;
using portal_backend.models;

namespace portal_backend.Services;

public class ProjectService
{
    private readonly RvipsContext _rvipsContext;

    public ProjectService(RvipsContext rvipsContext)
    {
        _rvipsContext = rvipsContext;
    }

    public List<Project> GetProjects(int userId)
    {
        var user = _rvipsContext.Users.Include(x => x.Roles).FirstOrDefault(user => user.Id == userId);
        if (user == null) throw new Exception("User doesn't exist");
        if (user.OrganizationId is null) throw new Exception("User is not part of organization");
        
        var project = _rvipsContext.Projects
            .Include(x => x.Users)
            .Include(x => x.Sponsors)
            .Where(x => x.OrganizationId == user.OrganizationId)
            .ToList();
        return project;
    }
    
    public List<Project> GetActiveUserProjects(int userId)
    {
        var user = _rvipsContext.Users.Include(x => x.Roles).FirstOrDefault(user => user.Id == userId);
        if (user == null) throw new Exception("User doesn't exist");
        if (user.OrganizationId is null) throw new Exception("User is not part of organization");
        
        var project = _rvipsContext.Projects
            .Include(x => x.Users)
            .Include(x => x.Sponsors)
            .Where(x => x.OrganizationId == user.OrganizationId
                        && x.EndOfProjectDate > DateTimeOffset.Now
                        && x.Users.Contains(user))
            .OrderBy(x => x.EndOfProjectDate)
            .ToList();
        return project;
    }
    
    public Project GetProject(int userId, int id)
    {
        var user = _rvipsContext.Users.Include(x => x.Roles).FirstOrDefault(user => user.Id == userId);
        if (user == null) throw new Exception("User doesn't exist");
        if (user.OrganizationId is null) throw new Exception("User is not part of organization");
        
        var project = _rvipsContext.Projects
            .Include(x => x.Users)
            .Include(x => x.Sponsors)
            .FirstOrDefault(x => x.OrganizationId == user.OrganizationId && x.Id == id);
        return project ?? new Project();
    }

    public void CreateProject(ProjectCreationRequest request, int userId)
    {
        var user = _rvipsContext.Users.Include(x => x.Roles).FirstOrDefault(user => user.Id == userId);

        if (user == null) throw new Exception("User doesn't exist");
        if (user.OrganizationId is null) throw new Exception("User is not part of organization");
        if (request.CreationDate > request.EndOfProjectDate) {
            throw new Exception("Project can't start after it ended");
        }
        
        var project = new Project {
            Title = request.Name,
            Description = request.Description,
            CreationDate = request.CreationDate,
            EndOfProjectDate = request.EndOfProjectDate,
            OrganizationId =  user.OrganizationId??0,
            Users = new List<User>(){user},
            Sponsors = new List<Sponsor>()
        };

        var initial = _rvipsContext.Projects.FirstOrDefault(x => x.Title == project.Title && x.OrganizationId == user.OrganizationId);
        if (initial != null) throw new Exception("Project with same name already exists");

        _rvipsContext.Projects.Add(project);

        _rvipsContext.SaveChanges();
    }
    
    public void UpdateProject(int userId, ProjectUpdateRequest request)
    {
        var user = _rvipsContext.Users.Include(x => x.Roles).FirstOrDefault(user => user.Id == userId);

        if (user == null) throw new Exception("User doesn't exist");
        if (user.OrganizationId is null) throw new Exception("User is not part of organization");
        if (request.CreationDate > request.EndOfProjectDate) {
            throw new Exception("Project can't start after it ended");
        }
        
        var project = _rvipsContext.Projects.FirstOrDefault(x => x.Id == request.Id && x.OrganizationId == user.OrganizationId);
        if (project == null) throw new Exception("Project doesn't exist");

        project.Id = request.Id;
        project.Title = request.Name;
        project.Description = request.Description;
        project.CreationDate = request.CreationDate;
        project.EndOfProjectDate = request.EndOfProjectDate;

        var initial = _rvipsContext.Projects.FirstOrDefault(
            x => x.Title == project.Title 
                 && x.OrganizationId == user.OrganizationId
                 && x.Id != project.Id
        );
        if (initial != null) throw new Exception("Project with same name already exists");

        _rvipsContext.Projects.Update(project);

        _rvipsContext.SaveChanges();
    }
    
    
    public void AddUserToProject(int userId, AddUserToProjectRequest request)
    {
        if (request.ProjectId is -1) {
            throw new Exception("Bad project id");
        }
        var user = _rvipsContext.Users.Include(x => x.Roles).FirstOrDefault(x => x.Id == userId);

        if (user is null) {
            throw new Exception("User doesn't exist");
        }
        if (user.OrganizationId is null) throw new Exception("User is not part of organization");
        
        var userreq = _rvipsContext.Users.Include(x => x.Roles).FirstOrDefault(x => x.Id == request.UserToProjectId);
        
        if (user.OrganizationId != userreq.OrganizationId) {
            throw new Exception("User is part of a different organization");
        }
        
        var project = _rvipsContext.Projects.FirstOrDefault(r => r.OrganizationId == user.OrganizationId && r.Id == request.ProjectId);

        if (project is null) {
            throw new Exception("Project doesn't exist");
        }
        
        if (project.Users is not null && project.Users.Contains(userreq)) {
            throw new Exception("User is already a part of the project");
        }
        
        var newList = new List<User>(project.Users??new List<User>());
        newList.Add(userreq);

        project.Users = newList;

        _rvipsContext.Projects.Update(project);

        _rvipsContext.SaveChanges();
    }
    
    public void AddSponsorToProject(int userId, AddSponsorToProjectRequest request)
    {
        if (request.ProjectId is -1) {
            throw new Exception("Bad project id");
        }
        var user = _rvipsContext.Users.Include(x => x.Roles).FirstOrDefault(x => x.Id == userId);

        if (user is null) {
            throw new Exception("User doesn't exist");
        }
        if (user.OrganizationId is null) throw new Exception("User is not part of organization");
        
        var sponsor = _rvipsContext.Sponsors.FirstOrDefault(x =>
            x.OrganizationId == user.OrganizationId && x.Id == request.SponsorToProjectId);
        
        if (sponsor is null) {
            throw new Exception("Sponsor doesn't exist in this organization");
        }
        
        var project = _rvipsContext.Projects.FirstOrDefault(r => r.OrganizationId == user.OrganizationId && r.Id == request.ProjectId);

        if (project is null) {
            throw new Exception("Project doesn't exist");
        }
        
        if (project.Sponsors is not null && project.Sponsors.Contains(sponsor)) {
            throw new Exception("Sponsor is already a part of the project");
        }
        
        var newList = new List<Sponsor>(project.Sponsors??new List<Sponsor>());
        newList.Add(sponsor);

        project.Sponsors = newList;

        _rvipsContext.Projects.Update(project);

        _rvipsContext.SaveChanges();
    }
}