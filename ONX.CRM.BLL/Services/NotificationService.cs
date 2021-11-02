using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using ONX.CRM.BLL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class NotificationService : INotificationService
    {

        public async Task SendEmailAsync(string userEmail, string userFirstName, string userLastName, string userPass, string subject)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("ONX Academy", "onx.crm@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", userEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = CreateMessage(userEmail, userFirstName, userLastName, userPass)
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync("onx.crm@gmail.com", "onxCRM123");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
        private string CreateMessage(string userEmail, string userFirstName, string userLastName, string userPass)
        {
            string message = $"<h1>Dear, {userFirstName} {userLastName}!</h1>" +
                             "<h3>For authorization, use the following data:</h3>" +
                             "<ul>" +
                             $"<li><span>E-mail: {userEmail}</span></li>" +
                             $"<li><span>Password: {userPass}</span></li>" +
                             "</ul>" +
                             "<p>We strongly recommend changing your password immediately after authorization.</p>";
            return message;
        }


    }
}
