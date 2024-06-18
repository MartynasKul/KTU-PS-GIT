using Microsoft.EntityFrameworkCore;
using portal_backend.Entities;
using portal_backend.Enums;
using portal_backend.models;

namespace portal_backend.Services;

public class SponsorService
{
    private readonly RvipsContext _rvipsContext;

    public SponsorService(RvipsContext rvipsContext)
    {
        _rvipsContext = rvipsContext;
    }

    public void CreateSponsor(SponsorCreationRequest request, int userId)
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

        var initial = _rvipsContext.Sponsors.FirstOrDefault(x => x.SponsorName == request.SponsorName && x.OrganizationId == user.OrganizationId);
        if (initial != null) throw new Exception("Sponsor with same name already exists");

        var sponsor = new Sponsor
        {
            SponsorName = request.SponsorName,
            Address = request.Address,
            TelephoneNumber = request.TelephoneNumber,
            Email = request.Email,
            Description = request.Description,
            OrganizationId = user.OrganizationId ?? 0
        };

        _rvipsContext.Sponsors.Add(sponsor);
        
        _rvipsContext.SaveChanges();
    }
    
    public List<Sponsor> GetSponsors(int userId)
    {
        var user = _rvipsContext.Users
            .FirstOrDefault(x => x.Id == userId);

        if (user is null)
        {
            return new List<Sponsor>();
        }

        var organizationId = user.OrganizationId;  

        var sponsors =
            _rvipsContext.Sponsors
                .Where(x => x.OrganizationId == organizationId)
                .ToList();

        return sponsors;
    }   
    
    public Sponsor GetSponsor(int userId, int id)
    {
        var user = _rvipsContext.Users
            .FirstOrDefault(x => x.Id == userId);

        if (user is null)
        {
            return new Sponsor();
        }

        var organizationId = user.OrganizationId;  

        var sponsor =
            _rvipsContext.Sponsors
                .FirstOrDefault(x => x.OrganizationId == organizationId && x.Id == id);

        return sponsor ?? new Sponsor();
    }  
}