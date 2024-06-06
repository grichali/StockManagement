
using System.Net;
using System.Net.Mail;
using api.Interfaces;

namespace api.Service
{
    public class EmailVerificationService : IEmailVerificationService
    {

        private readonly IConfiguration _configuration;

        public EmailVerificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

      public Task SendEmailAsync(string email, string subject, string message)
        {

            var emailUsername = _configuration["EmailSettings:Username"];
            var emailPassword = _configuration["EmailSettings:Password"];

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(emailUsername, emailPassword)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("simohh224@gmail.com"),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);
            return client.SendMailAsync(mailMessage);
        }

    }
}