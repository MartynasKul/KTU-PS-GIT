namespace portal_backend.models;

public class CommentCreationRequest
{
    public int SponsorId { get; set; }
    public string CommentText { get; set; } = null!;
}