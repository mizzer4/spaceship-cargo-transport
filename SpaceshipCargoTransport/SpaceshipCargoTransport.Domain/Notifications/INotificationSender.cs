using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Notifications
{
    public interface INotificationSender
    {
        public void Send(Transport transport, string message);
    }
}
