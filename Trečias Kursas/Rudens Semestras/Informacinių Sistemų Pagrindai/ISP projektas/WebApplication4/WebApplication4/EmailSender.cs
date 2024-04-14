using System.Net;
using System.Net.Mail;

namespace WebApplication4.Models
{
    public class EmailSender : IEmailSender
    {
        
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "isplaboras@gmail.com";
            var pw = "bvnd uayd yyok kdgw";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            var mailMessage = new MailMessage(from: mail, to: email, subject, message)
            {
                IsBodyHtml = true // Set this property to true to enable HTML rendering
            };

            return client.SendMailAsync(mailMessage);
        }

    }
}
