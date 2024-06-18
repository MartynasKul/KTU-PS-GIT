namespace portal_backend.models;

public class ProjectCreationRequest
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public DateTimeOffset CreationDate { get; set; }
    
    public DateTimeOffset EndOfProjectDate { get; set; }
    
}