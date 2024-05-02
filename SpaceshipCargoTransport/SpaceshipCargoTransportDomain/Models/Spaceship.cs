namespace SpaceshipCargoTransport.Domain.Models
{
    public class Spaceship
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CrewSize { get; set; }
        public int CargoStorageSize { get; set; }
    }
}
