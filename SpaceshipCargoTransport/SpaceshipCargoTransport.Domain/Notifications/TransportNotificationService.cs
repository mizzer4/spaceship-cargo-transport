using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Notifications
{
    internal class TransportNotificationService : ITransportNotificationService
    {
        private readonly INotificationSender _notificationSender;

        public TransportNotificationService(INotificationSender notificationSender)
        {
            _notificationSender = notificationSender;
        }

        public void NotifyCancelled(Transport transport)
        {
            var message = $"";

            _notificationSender.Send(transport, message);
        }

        public void NotifyFinished(Transport transport)
        {
            var message = $"";

            _notificationSender.Send(transport, message);
        }

        public void NotifyLost(Transport transport)
        {
            var message = $"";

            _notificationSender.Send(transport, message);
        }
    }
}
