using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace Samurai_CMS.Services {
    public class EmailService {
        public string Account { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }

        public EmailService(string account, string password, string displayName) 
        {
            Account = account;
            Password = password;
            DisplayName = displayName;
        }

        public void sendEmail(string destinationEmail, string destinationDisplayName, string subject, string message) 
        {
            MailAddress from = new MailAddress ( Account, DisplayName );
            MailAddress to = new MailAddress ( destinationEmail, destinationDisplayName );
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage ( from,to );

            mailMessage.Subject = subject;
            mailMessage.Body = message;
            SmtpClient client = new SmtpClient ( );
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential( Account,Password);
            
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try 
            {
                client.Send ( mailMessage );
            }
            catch ( Exception ) 
            {

            }
        }
        public async Task<bool> sendEmailAsync(string destinationEmail, string destinationDisplayName, string subject, string message) {
            MailAddress from = new MailAddress ( Account, DisplayName );
            MailAddress to = new MailAddress ( destinationEmail, destinationDisplayName );
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage ( from, to );

            mailMessage.Subject = subject;
            mailMessage.Body = message;
            SmtpClient client = new SmtpClient ( );
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential ( Account, Password );

            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try {
                client.Send ( mailMessage );
                return true;
                }
            catch ( Exception ) {

                }
            return false;
            }
            
        }

    public static class SamuraiEmail {
        public static string AccountName { get; } = "samuraicms1@gmail.com";
        public static string AccountPassword { get; } = "cotanfilip";
    }

}