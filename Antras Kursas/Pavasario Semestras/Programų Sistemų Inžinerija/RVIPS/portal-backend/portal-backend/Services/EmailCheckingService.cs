using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using portal_backend.Entities;
using portal_backend.Helpers;

namespace portal_backend.Services;

public class EmailCheckingService : IHostedService
{
    private Timer _timer;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public EmailCheckingService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(CheckEmails, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Dispose();
        return Task.CompletedTask;
    }
    
    private void CheckEmails(object? state)
    {
        try
        {
            List<Organization>? organizations;
        
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<RvipsContext>();

                organizations = context?.Organizations.Where(x => x.ImapServerPort != null).ToList();
            }
            
            foreach (var organization in organizations ?? new List<Organization>())
            {
                CheckEmail(organization);
            }
        }
        catch (Exception e)
        {
            // ignored
        }
    }

    private void CheckEmail(Organization organization)
    {
        using (var client = new ImapClient())
        {
            client.Connect(organization.ImapServer, organization.ImapServerPort ?? 0, true);
            client.Authenticate(organization.ImapServerUserName, organization.ImapServerUserPassword);

            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadWrite);

            var uids = inbox.Search(SearchQuery.NotSeen);

            foreach (var uid in uids)
            {
                var message = inbox.GetMessage(uid);
                var email = EmailHelper.MapEmail(message, organization.Id);
                
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    try
                    {
                        var context = scope.ServiceProvider.GetService<RvipsContext>();
                        context!.Emails.Add(email);
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Duplicate: {email.MessageId}");
                    }
                }
            }

            inbox.AddFlags(uids, MessageFlags.Seen, true);
                
            client.Disconnect(true);
        }
    }
}