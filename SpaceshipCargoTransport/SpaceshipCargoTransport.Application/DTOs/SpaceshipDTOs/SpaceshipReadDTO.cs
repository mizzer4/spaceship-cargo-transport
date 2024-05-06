namespace SpaceshipCargoTransport.Application.DTOs.SpaceshipDTOs
{
    public class SpaceshipReadDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CrewSize { get; set; }
        public int CargoStorageSize { get; set; }
    }
}
