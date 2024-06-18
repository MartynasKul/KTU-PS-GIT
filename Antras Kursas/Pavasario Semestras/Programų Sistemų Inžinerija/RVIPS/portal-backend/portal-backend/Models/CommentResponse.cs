using portal_backend.Entities;

namespace portal_backend.Models;

public class CommentResponse
{
    public int Id { get; set; }
    public User User { get; set; } = null!;
    public int SponsorId { get; set; }
    public string CommentText { get; set; } = null!;
    public DateTimeOffset? Date { get; set; }
}