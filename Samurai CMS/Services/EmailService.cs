using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Samurai_CMS.Services
{
    public class EmailService
    {

        //TODO: move credentials to web.config
        private const string EmailAddress = "samuraicms1@gmail.com";
        private const string Password = "cotanfilip";
        private const string DisplayName = "Samurai CMS";

        public async Task SendEmailAsync(string destinationEmail, string displayName, string subject, string message)
        {
            var from = new MailAddress(EmailAddress, DisplayName);
            var to = new MailAddress(destinationEmail, displayName);
            var mailMessage = new MailMessage(from, to)
            {
                Subject = subject, Body = message
            };

            var client = new SmtpClient
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(EmailAddress, Password),
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true
            };

            try
            {
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong while sending your email! Please try again.");
            }
        }
    }
}