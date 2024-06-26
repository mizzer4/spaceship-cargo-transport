﻿using Microsoft.EntityFrameworkCore;
using SpaceshipCargoTransport.Domain.Models;

namespace SpaceshipCargoTransport.Persistence.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Transport> Transports { get; set; }
    }
}