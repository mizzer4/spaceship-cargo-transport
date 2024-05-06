namespace SpaceshipCargoTransport.Domain.Models
{
    public class Transport
    {
        public Guid Id { get; set; }
        public Spaceship Spaceship { get; set; }
        public TransportStatus Status { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public Planet StartingLocation { get; set; }
        public Planet EndingLocation { get; set; }
        public CargoType CargoType { get; set; }
        public int CargoSize { get; set; }
        public string Requestor { get; set; }
    }
}
