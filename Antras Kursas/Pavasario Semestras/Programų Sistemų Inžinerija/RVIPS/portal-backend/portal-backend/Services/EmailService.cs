using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MimeKit;
using portal_backend.Entities;
using portal_backend.models;
using portal_backend.Models;

namespace portal_backend.Services;

public class EmailService
{
    private readonly RvipsContext _rvipsContext;

    public EmailService(RvipsContext rvipsContext)
    {
        _rvipsContext = rvipsContext;
    }

    public IEnumerable<Email> GetReceivedEmails(int userId)
    {
        var user = GetUser(userId);

        var emails =
            _rvipsContext.Emails
                .Where(x => x.OrganizationId == user.OrganizationId)
                .ToList();

        return emails;
    }
    
    public IEnumerable<OutgoingEmailResponse> GetSentEmails(int userId)
    {
        var user = GetUser(userId);

        var emails =
            _rvipsContext.OutgoingEmails
                .Where(x => x.OrganizationId == user.OrganizationId)
                .ToList();

        return FillUsersInEmailsOutgoingResponse(emails);
    }
    
    public IEnumerable<Email> GetReceivedEmails(int userId, string address)
    {
        var user = GetUser(userId);

        var emails =
            _rvipsContext.Emails
                .Where(x => x.OrganizationId == user.OrganizationId && x.FromEmail == address)
                .ToList();

        return emails;
    }
    
    public IEnumerable<OutgoingEmailResponse> GetSentEmails(int userId, string address)
    {
        var user = GetUser(userId);

        var emails =
            _rvipsContext.OutgoingEmails
                .Where(x => x.OrganizationId == user.OrganizationId && x.ReceiverEmail == address)
                .ToList();

        return FillUsersInEmailsOutgoingResponse(emails);
    }

    public void SendEmail(int userId, SendEmailRequest request)
    {
        var user = GetUser(userId);
        var organization = GetOrganization(user.OrganizationId);

        if (organization.SmtpServerUserName is null)
        {
            throw new Exception();
        }
        
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(organization.Title, organization.SmtpServerUserName));
        message.To.Add(new MailboxAddress("", request.ReceiverEmail));
        message.Subject = request.Subject;
        
        var builder = new BodyBuilder
        {
            HtmlBody = request.Body
        };
        message.Body = builder.ToMessageBody();
        
        using (var client = new SmtpClient())
        {
            client.Connect(organization.SmtpServer, organization.SmtpServerPort ?? 0, true);
            client.Authenticate(organization.SmtpServerUserName, organization.SmtpServerUserPassword);
            
            client.Send(message);
            client.Disconnect(true);
        }

        _rvipsContext.OutgoingEmails.Add(new OutgoingEmail()
        {
            OrganizationId = organization.Id,
            UserId = userId,
            ReceiverEmail = request.ReceiverEmail,
            Subject = request.Subject,
            Body = request.Body,
            Date = DateTimeOffset.Now
        });
        
        _rvipsContext.SaveChanges();
    }

    private User GetUser(int userId)
    {
        var user = _rvipsContext.Users
            .FirstOrDefault(x => x.Id == userId);

        if (user is null)
        {
            throw new Exception("User doesn't exist");
        }

        if (user.OrganizationId is null)
        {
            throw new Exception("User doesn't have organization");
        }

        return user;
    }
    
    private Organization GetOrganization(int? organizationId)
    {
        var organization = _rvipsContext.Organizations
            .FirstOrDefault(x => x.Id == organizationId);

        if (organization is null)
        {
            throw new Exception("Organization doesn't exist");
        }

        return organization;
    }

    private List<OutgoingEmailResponse> FillUsersInEmailsOutgoingResponse(IEnumerable<OutgoingEmail> list)
    {
        return (from item in list
            let user = GetUser(item.UserId)
            select new OutgoingEmailResponse()
            {
                Body = item.Body,
                Date = item.Date,
                Id = item.Id,
                OrganizationId = item.OrganizationId,
                ReceiverEmail = item.ReceiverEmail,
                Subject = item.Subject,
                User = user
            }).ToList();
    }
}