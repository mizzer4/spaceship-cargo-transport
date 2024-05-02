using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Services
{
    public interface ITransportService
    {
        Transport GetDetails(int id);
        bool RegisterNew(Transport transport);
        bool MarkAsFinished(Transport transport);
        bool Cancel(int id);
    }
}
