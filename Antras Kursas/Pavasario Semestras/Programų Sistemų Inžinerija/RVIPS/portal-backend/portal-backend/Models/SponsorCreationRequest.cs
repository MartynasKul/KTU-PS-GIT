namespace portal_backend.models;

public class SponsorCreationRequest
{
    public string SponsorName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string TelephoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Description { get; set; } = null!;
}