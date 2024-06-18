using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using portal_backend.Enums;

namespace portal_backend.Entities;

public partial class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public int Id { get; set; }

    [JsonIgnore] public string Name { get; set; } = null!;
    
    public UserTypeEnum UserType { get; set; }

    [JsonIgnore] public List<User> Users { get; set; } = null!;
}