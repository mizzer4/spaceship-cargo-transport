using Microsoft.EntityFrameworkCore;
using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Persistence.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        public DbSet<Spaceship> Spaceships { get; set; }
    }
}