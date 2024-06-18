using portal_backend.Entities;

namespace portal_backend.models;

public class ProjectUpdateRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public DateTimeOffset CreationDate { get; set; }
    
    public DateTimeOffset EndOfProjectDate { get; set; }
}