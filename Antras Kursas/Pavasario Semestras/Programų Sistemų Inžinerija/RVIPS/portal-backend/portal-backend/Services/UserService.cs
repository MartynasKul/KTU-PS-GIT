using Microsoft.EntityFrameworkCore;
using portal_backend.Entities;
using portal_backend.Enums;

namespace portal_backend.Services;

public class UserService
{
    private readonly RvipsContext _rvipsContext;

    public UserService(RvipsContext rvipsContext)
    {
        _rvipsContext = rvipsContext;
    }
    public User? GetUser(int userId)
    {
        var user = _rvipsContext.Users.Include(x => x.Roles).FirstOrDefault(x => x.Id == userId);
        return user;
    }

    public void AddUserToOrganization(int userId, int organizationId)
    {
        if (organizationId is -1)
        {
            throw new Exception("Bad organization id");
        }
        var user = _rvipsContext.Users.Include(x => x.Roles).FirstOrDefault(x => x.Id == userId);

        if (user is null)
        {
            throw new Exception("User doesn't exist");
        }
        
        var roles = _rvipsContext.Roles.Where(r => r.UserType == UserTypeEnum.Regular).ToList();
        user.Roles = roles;
        user.OrganizationId = organizationId;
        
        _rvipsContext.Users.Update(user);

        _rvipsContext.SaveChanges();
    }
    
    public List<User> GetOrganizationUsers(int userId)
    {
        var user = _rvipsContext.Users
            .Include(x => x.Roles)
            .FirstOrDefault(x => x.Id == userId);

        if (user is null)
        {
            return new List<User>();
        }

        if (user.OrganizationId is null)
        {
            return new List<User>(){user};
        }

        var organizationUsers =
            _rvipsContext.Users
                .Include(x => x.Roles)
                .Where(x => x.OrganizationId == user.OrganizationId)
                .ToList();

        return organizationUsers;
    }   
}