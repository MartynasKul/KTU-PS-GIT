using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace portal_backend.Entities;

public class OutgoingEmail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int OrganizationId { get; set; }
    public int UserId { get; set; }
    public string ReceiverEmail { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public DateTimeOffset Date { get; set; }
}