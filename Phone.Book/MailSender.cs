using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace Phone.Book
{
    public class MailSender : IMessageSender
    {
        public async Task Send(string to, string address, string subject, string body)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Me", "mikhailkrovikov@gmail.com"));
            emailMessage.To.Add(new MailboxAddress(address, to));

            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") 
            { 
                Text = body
            };

            using (var client = new SmtpClient())
            {
                var password = Environment.GetEnvironmentVariable("PHONEBOOK_MAIL_PASSWORD");
                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new InvalidOperationException("Set PHONEBOOK_MAIL_PASSWORD before sending email.");
                }
                await client.ConnectAsync(
                    "smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                await client.AuthenticateAsync("mikhailkrovikov@gmail.com", password);

                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}


