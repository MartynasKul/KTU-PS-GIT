using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace portal_backend.Entities;

public partial class Organization
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    public string? ImapServer { get; set; }
    public int? ImapServerPort { get; set; }
    public string? ImapServerUserName { get; set; }
    [JsonIgnore] public string? ImapServerUserPassword { get; set; }
    public string? SmtpServer { get; set; }
    public int? SmtpServerPort { get; set; }
    public string? SmtpServerUserName { get; set; }
    [JsonIgnore] public string? SmtpServerUserPassword { get; set; }
}

