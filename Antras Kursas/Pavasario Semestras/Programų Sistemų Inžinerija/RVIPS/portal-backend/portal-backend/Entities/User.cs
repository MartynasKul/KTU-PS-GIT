using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace portal_backend.Entities;

public partial class User
{
    public List<Role> Roles { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    [JsonIgnore]
    public string Password { get; set; } = null!;

    [JsonIgnore]
    public string Salt { get; set; } = null!;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTimeOffset CreationDate { get; set; }
    
    public int? OrganizationId { get; set; }

    [JsonIgnore] public List<Project> Projects { get; set; } = null!;
}