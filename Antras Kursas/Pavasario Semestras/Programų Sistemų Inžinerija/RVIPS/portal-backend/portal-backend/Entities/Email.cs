using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace portal_backend.Entities;

public class Email
{
    [Key] public string MessageId { get; set; } = null!;
    [Key] public int OrganizationId { get; set; }
    public string? Subject { get; set; }
    public string? FromName { get; set; }
    public string? FromEmail { get; set; }
    public string? TextBody { get; set; }
    public string? HtmlBody { get; set; }
    public DateTimeOffset? Date { get; set; }
}