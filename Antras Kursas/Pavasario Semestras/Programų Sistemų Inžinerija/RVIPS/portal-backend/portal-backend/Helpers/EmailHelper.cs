using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MimeKit;
using portal_backend.Entities;
using portal_backend.models;

namespace portal_backend.Helpers;

public static class EmailHelper
{
    public static Email MapEmail(MimeMessage originalEmail, int organizationId)
    {
        var email = new Email
        {
            MessageId = originalEmail.MessageId,
            OrganizationId = organizationId,
            Subject = originalEmail.Subject,
            HtmlBody = originalEmail.HtmlBody,
            TextBody = originalEmail.TextBody,
            Date = originalEmail.Date,
            FromName = originalEmail.From.Mailboxes.FirstOrDefault()?.Name ?? "",
            FromEmail = originalEmail.From.Mailboxes.FirstOrDefault()?.Address ?? ""
        };

        return email;
    }
    
    public static bool IsImapAccessible(ConfigureOrganizationImapRequest request)
    {
        try
        {
            using var client = new ImapClient();
            
            client.Connect(request.ImapServer, request.ImapServerPort, true);
            client.Authenticate(request.ImapServerUserName, request.ImapServerUserPassword);
            
            client.Disconnect(true);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    public static bool IsSmtpAccessible(ConfigureOrganizationSmtpRequest request)
    {
        try
        {
            using var client = new SmtpClient();
            
            client.Connect(request.SmtpServer, request.SmtpServerPort, true);
            client.Authenticate(request.SmtpServerUserName, request.SmtpServerUserPassword);
            
            client.Disconnect(true);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}