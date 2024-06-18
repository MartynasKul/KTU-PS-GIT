namespace portal_backend.models;

public class ConfigureOrganizationSmtpRequest
{
    public string SmtpServer { get; set; } = null!;
    public int SmtpServerPort { get; set; }
    public string SmtpServerUserName { get; set; } = null!;
    public string SmtpServerUserPassword { get; set; } = null!;
}