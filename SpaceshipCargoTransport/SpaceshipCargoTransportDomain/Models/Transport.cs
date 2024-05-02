namespace SpaceshipCargoTransport.Domain.Models
{
    public class Transport
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public Planet StartingLocation { get; set; }
        public Planet EndingLocation { get; set; }
        public CargoType CargoType { get; set; }
        public int CargoSize { get; set; }
    }
}
