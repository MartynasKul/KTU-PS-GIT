namespace portal_backend.models;

public class AddUserToProjectRequest
{
    public int UserToProjectId { get; set; }
    
    public int ProjectId { get; set; }
}