namespace portal_backend.models;

public class SendEmailRequest
{
    public string ReceiverEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}