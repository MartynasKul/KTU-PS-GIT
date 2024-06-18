using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace portal_backend.Entities;

public partial class Sponsor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string SponsorName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string TelephoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int OrganizationId { get; set; }

    [JsonIgnore] public List<Project> Projects { get; set; } = null!;
}

