namespace portal_backend.models;

public class ConfigureOrganizationImapRequest
{
    public string ImapServer { get; set; } = null!;
    public int ImapServerPort { get; set; }
    public string ImapServerUserName { get; set; } = null!;
    public string ImapServerUserPassword { get; set; } = null!;
}