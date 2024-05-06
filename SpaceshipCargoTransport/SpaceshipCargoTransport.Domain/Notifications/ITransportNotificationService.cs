using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Notifications
{
    public interface ITransportNotificationService
    {
        public void NotifyCancelled(Transport transport);
        public void NotifyLost(Transport transport);
        public void NotifyFinished(Transport transportId);
    }
}
