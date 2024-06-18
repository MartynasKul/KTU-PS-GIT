using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace portal_backend.Entities;

public partial class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SponsorId { get; set; }
    public string CommentText { get; set; } = null!;
    public DateTimeOffset? Date { get; set; }
}