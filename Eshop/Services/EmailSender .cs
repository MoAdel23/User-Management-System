
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace Eshop.Services;

public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var fromMail = "Add Email";
        var fromPassword = "Add Password";
        var message = new MailMessage();

        message.From = new MailAddress(fromMail);
        message.Subject = subject;
        message.To.Add(email);

        message.Body = $"<html><body>{htmlMessage}</body></html>";
        message.IsBodyHtml = true;
        using (var smtpClient = new SmtpClient("smtp-mail.outlook.com", 587)) // chenge host 
        {
            smtpClient.Credentials = new NetworkCredential(fromMail, fromPassword);
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(message);
        }
    }
}
//public class EmailSender : IEmailSender
//{
//    private readonly IConfiguration _configuration;

//    public EmailSender(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }
//    public async Task SendEmailAsync(string email, string subject, string message)
//    {
//        var smtpClient = new SmtpClient(_configuration["Email:Smtp:Host"])
//        {
//            Port = int.Parse(_configuration["Email:Smtp:Port"]),
//            Credentials = new NetworkCredential(_configuration["Email:Smtp:Username"], _configuration["Email:Smtp:Password"]),
//            EnableSsl = true,
//        };

//        var mailMessage = new MailMessage
//        {
//            From = new MailAddress(_configuration["Email:Smtp:From"]),
//            Subject = subject,
//            Body = $"<html><body>{message}</body></html>",
//            IsBodyHtml = true,
//        };
//        mailMessage.To.Add(email);

//        try
//        {
//            await smtpClient.SendMailAsync(mailMessage);
//        }
//        catch (SmtpException ex)
//        {
//            // Log the exception or handle it as needed
//            throw new InvalidOperationException("Failed to send email", ex);
//        }
//    }
//}
