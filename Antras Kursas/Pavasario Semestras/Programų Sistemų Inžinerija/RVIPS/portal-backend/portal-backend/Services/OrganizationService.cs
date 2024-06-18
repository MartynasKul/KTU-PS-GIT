using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using portal_backend.Entities;
using portal_backend.Enums;
using portal_backend.Helpers;
using portal_backend.models;

namespace portal_backend.Services;

public class OrganizationService
{
    private readonly RvipsContext _rvipsContext;

    public OrganizationService(RvipsContext rvipsContext)
    {
        _rvipsContext = rvipsContext;
    }

    public Organization? GetOrganization(int userId)
    {
        var user = _rvipsContext.Users.Include(x => x.Roles).FirstOrDefault(user => user.Id == userId);

        if (user == null) throw new Exception("User doesn't exist");

        var organization = _rvipsContext.Organizations.FirstOrDefault(org => org.Id == user.OrganizationId);

        return organization;
    }

    public void CreateOrganization(OrganizationCreationRequest request, int userId)
    {
        var user = _rvipsContext.Users.Include(x => x.Roles).FirstOrDefault(user => user.Id == userId);

        if (user == null) throw new Exception("User doesn't exist");
        if (!user.Roles.IsNullOrEmpty()) throw new Exception("User can't create organization");

        var organization = new Organization
        {
            Title = request.Name
        };
        
        var initial = _rvipsContext.Organizations.FirstOrDefault(x => x.Title == organization.Title);
        if (initial != null) throw new Exception("Organization with same name already exists");

            _rvipsContext.Organizations.Add(organization);
        
        _rvipsContext.SaveChanges();

        var updated = _rvipsContext.Organizations.FirstOrDefault(x => x.Title == organization.Title);

        var roles = _rvipsContext.Roles.Where(r => r.UserType == UserTypeEnum.Admin || r.UserType == UserTypeEnum.Regular).ToList();
        user.Roles = roles;
        user.OrganizationId = updated?.Id;
        
        _rvipsContext.Users.Update(user);

        _rvipsContext.SaveChanges();
    }

    public void RenameOrganization(OrganizationRenameRequest request, int userId)
    {
        var user = _rvipsContext.Users.Include(x => x.Roles)
            .FirstOrDefault(user => user.Id == userId);

        if (user == null) throw new Exception("User doesn't exist");
        if (!user.Roles.Exists(x => x.UserType.Equals(UserTypeEnum.Admin)))
        {
            throw new Exception("User is not administrator");
        }

        if (user.OrganizationId is null)
        {
            throw new Exception("User is not part of organization");
        }
        
        var organization = _rvipsContext.Organizations
            .FirstOrDefault(x => x.Id == user.OrganizationId);

        if (organization is null)
        {
            throw new Exception("Organization doesn't exist");
        }

        organization.Title = request.Name;
        
        
        var initial = _rvipsContext.Organizations
            .FirstOrDefault(x => x.Title == organization.Title);
        
        if (initial != null) throw new Exception("Organization with same name already exists");

        _rvipsContext.Organizations.Update(organization);

        _rvipsContext.SaveChanges();
    }
    
    public void ConfigureOrganizationImap(ConfigureOrganizationImapRequest request, int userId)
    {
        var user = _rvipsContext.Users.Include(x => x.Roles)
            .FirstOrDefault(user => user.Id == userId);

        if (user == null) throw new Exception("User doesn't exist");
        if (!user.Roles.Exists(x => x.UserType.Equals(UserTypeEnum.Admin)))
        {
            throw new Exception("User is not administrator");
        }

        if (user.OrganizationId is null)
        {
            throw new Exception("User is not part of organization");
        }
        
        var organization = _rvipsContext.Organizations
            .FirstOrDefault(x => x.Id == user.OrganizationId);

        if (organization is null)
        {
            throw new Exception("Organization doesn't exist");
        }

        organization.ImapServer = request.ImapServer;
        organization.ImapServerPort = request.ImapServerPort;
        organization.ImapServerUserName = request.ImapServerUserName;
        organization.ImapServerUserPassword = request.ImapServerUserPassword;
        
        var initial = _rvipsContext.Organizations
            .FirstOrDefault(x => 
                x.ImapServerUserName == request.ImapServerUserName 
                && x.Id != user.OrganizationId);
        
        if (initial != null) throw new Exception("Account is already in use");

        if (!EmailHelper.IsImapAccessible(request)) throw new Exception("Server or user is not valid");
        
        _rvipsContext.Organizations.Update(organization);

        _rvipsContext.SaveChanges();
    }
    
    public void ConfigureOrganizationSmtp(ConfigureOrganizationSmtpRequest request, int userId)
    {
        var user = _rvipsContext.Users.Include(x => x.Roles)
            .FirstOrDefault(user => user.Id == userId);

        if (user == null) throw new Exception("User doesn't exist");
        if (!user.Roles.Exists(x => x.UserType.Equals(UserTypeEnum.Admin)))
        {
            throw new Exception("User is not administrator");
        }

        if (user.OrganizationId is null)
        {
            throw new Exception("User is not part of organization");
        }
        
        var organization = _rvipsContext.Organizations
            .FirstOrDefault(x => x.Id == user.OrganizationId);

        if (organization is null)
        {
            throw new Exception("Organization doesn't exist");
        }

        organization.SmtpServer = request.SmtpServer;
        organization.SmtpServerPort = request.SmtpServerPort;
        organization.SmtpServerUserName = request.SmtpServerUserName;
        organization.SmtpServerUserPassword = request.SmtpServerUserPassword;
        
        var initial = _rvipsContext.Organizations
            .FirstOrDefault(x => 
                x.SmtpServerUserName == request.SmtpServerUserName 
                && x.Id != user.OrganizationId);
        
        if (initial != null) throw new Exception("Account is already in use");

        if (!EmailHelper.IsSmtpAccessible(request)) throw new Exception("Server or user is not valid");
        
        _rvipsContext.Organizations.Update(organization);

        _rvipsContext.SaveChanges();
    }
}