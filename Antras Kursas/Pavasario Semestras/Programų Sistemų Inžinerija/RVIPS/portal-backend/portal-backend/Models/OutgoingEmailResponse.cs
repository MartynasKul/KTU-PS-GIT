using portal_backend.Entities;

namespace portal_backend.Models;

public class OutgoingEmailResponse
{
    public int Id { get; set; }
    public int OrganizationId { get; set; }
    public User User { get; set; } = null!;
    public string ReceiverEmail { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public DateTimeOffset Date { get; set; }
}