using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Domain.Repositories
{
    public interface ITransportRepository
    {
        Transport GetDetails(int id);
        bool RegisterNew(Transport transport);
        bool MarkAsFinished(Transport transport);
        bool Cancel(int id);
    }
}
