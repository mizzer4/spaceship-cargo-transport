using SpaceshipCargoTransport.Domain.Notifications;
using SpaceshipCargoTransport.Notifications.Senders;

namespace SpaceshipCargoTransport.Notifications.Services
{
    internal class NotificationSender : INotificationSender
    {
        private readonly IEmailSender _emailSender;

        public NotificationSender(IEmailSender emailSender) 
        {
            _emailSender = emailSender;
        }

        public Task SendAsync(string recipient, string subject, string message)
        {
            return _emailSender.SendEmailAsync(recipient, subject, message);
        }
    }
}
