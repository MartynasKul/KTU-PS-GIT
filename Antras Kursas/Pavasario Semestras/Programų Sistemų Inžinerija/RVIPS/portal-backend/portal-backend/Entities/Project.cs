using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace portal_backend.Entities;


public partial class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int OrganizationId { get; set; }
    
    public string Title {get; set;} = null!;

    public string Description {get; set;} = null!;

    public DateTimeOffset CreationDate { get; set; }

    public DateTimeOffset EndOfProjectDate { get; set; }

    public List<User> Users { get; set; } = null!;
    
    public List<Sponsor> Sponsors { get; set; } = null!;
}