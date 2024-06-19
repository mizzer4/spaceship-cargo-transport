using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Notifications
{
    public interface INotificationSender
    {
        Task SendAsync(string recipient, string subject, string message);
    }
}
