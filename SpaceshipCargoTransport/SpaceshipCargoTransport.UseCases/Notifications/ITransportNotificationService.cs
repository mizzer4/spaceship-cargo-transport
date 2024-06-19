using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Notifications
{
    public interface ITransportNotificationService
    {
        Task NotifyCancelled(Transport transport);
        Task NotifyLost(Transport transport);
        Task NotifyFinished(Transport transportId);
    }
}
