using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace SpaceshipCargoTransport.Notifications.Senders
{
    internal class EmailSender : IEmailSender
    {
        private readonly string _smtpServer;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public EmailSender(IOptions<EmailSenderOptions> options)
        {
            _smtpServer = options.Value.SmtpServer;
            _smtpUsername = options.Value.SmtpUsername;
            _smtpPassword = options.Value.SmtpPassword;
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            try
            {
                var message = CreateMessage(recipientEmail, subject, body);

                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpServer);
                await client.AuthenticateAsync(_smtpUsername, _smtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        private MimeMessage CreateMessage(string recipientEmail, string subject, string body) 
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("App", _smtpUsername));
            message.To.Add(new MailboxAddress("Recipient", recipientEmail));
            message.Subject = subject;       

            var textPart = new TextPart("plain")
            {
                Text = body
            };

            message.Body = textPart;

            return message;
        }
    }
}
