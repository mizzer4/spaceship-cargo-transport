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

        public Task NotifyCancelled(Transport transport)
        {
            var subject = BuildSubjectForStatus(transport, TransportStatus.Cancelled);
            var message = BuildMessageForStatus(transport, TransportStatus.Cancelled);
            var recipient = transport.Requestor;

            return _notificationSender.SendAsync(subject, recipient, message);
        }

        public Task NotifyFinished(Transport transport)
        {
            var subject = BuildSubjectForStatus(transport, TransportStatus.Finished);
            var message = BuildMessageForStatus(transport, TransportStatus.Finished);
            var recipient = transport.Requestor;

            return _notificationSender.SendAsync(subject, recipient, message);
        }

        public Task NotifyLost(Transport transport)
        {
            var subject = BuildSubjectForStatus(transport, TransportStatus.Lost);
            var message = BuildMessageForStatus(transport, TransportStatus.Lost);
            var recipient = transport.Requestor;

            return _notificationSender.SendAsync(subject, recipient, message);
        }

        private string BuildMessageForStatus(Transport transport, TransportStatus status) 
        {
            return $"Transport {transport.Id} of {transport.CargoSize} {transport.CargoType} by {transport.Spaceship.Name} has been {status}.";
        }

        private string BuildSubjectForStatus(Transport transport, TransportStatus status)
        {
            return $"Transport {transport.Id} has been {status}.";
        }
    }
}
