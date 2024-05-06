namespace SpaceshipCargoTransport.Application.DTOs.Transport
{
    public class TransportCreateDTO
    {
        public Guid SpaceshipId { get; set; }
        public DateTime StartDateTime { get; set; }
        public Guid StartingLocationId { get; set; }
        public Guid EndingLocationId { get; set; }
        public string CargoType { get; set; }
        public int CargoSize { get; set; }
        public string Requestor { get; set; }
    }
}
