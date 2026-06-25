using AdemideInfoWebsite.Application.Abstractions;
using AdemideInfoWebsite.Application.Dtos;
using AdemideInfoWebsite.Infrastructure.ThirdPartyServices.Models;
using AdemideInfoWebsite.SharedKernel.Models;

namespace AdemideInfoWebsite.Infrastructure.ThirdPartyServices;

// send mail using Google SMTP server use email dtos and settings.
public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    public EmailService(EmailSettings emailSettings)
    {
        _emailSettings = emailSettings;
    }
    public async Task<ResponseModel<EmailServiceResponse>> SendEmailAsync(EmailServiceRequest emailRequest)
    {
        try
        {
            // Use Google SMTP server to send email
            using (var client = new System.Net.Mail.SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
            {
                client.Credentials = new System.Net.NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
                client.EnableSsl = true;
                var mailMessage = new System.Net.Mail.MailMessage();
                mailMessage.From = new System.Net.Mail.MailAddress(emailRequest.From);
                mailMessage.To.Add(emailRequest.To);
                if (!string.IsNullOrEmpty(emailRequest.Cc))
                {
                    mailMessage.CC.Add(emailRequest.Cc);
                }
                if (!string.IsNullOrEmpty(emailRequest.Bcc))
                {
                    mailMessage.Bcc.Add(emailRequest.Bcc);
                }
                mailMessage.Subject = emailRequest.Subject;
                mailMessage.Body = emailRequest.Body;
                mailMessage.IsBodyHtml = true;
                await client.SendMailAsync(mailMessage);
                //return response model record type,
                return new ResponseModel<EmailServiceResponse>(
                    new EmailServiceResponse(true, "Email sent successfully"), true, 200, "Email sent successfully", null
                    );
            }

        }
        catch (Exception ex)
        {
            return new ResponseModel<EmailServiceResponse>(
                new EmailServiceResponse(false, $"Email sending failed: {ex.Message}"), false, 500, $"Email sending failed: {ex.Message}", null
                );

        }
    }
}