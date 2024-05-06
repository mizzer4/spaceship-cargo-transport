namespace SpaceshipCargoTransport.Domain.Models
{
    public enum TransportStatus
    {
        New,
        CargoLoading,
        InFlight,
        CargoUnloading,
        Finished,
        Cancelled,
        Lost
    }
}