using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Persistence.Db;
using SpaceshipCargoTransport.Persistence.Repositories;

namespace SpaceshipCargoTransport.Persistence.DependencyInjection
{
    public class PersistenceRegistrar : Module
    {
        public IConfiguration Configuration { get; private set; }

        public PersistenceRegistrar(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        protected override void Load(ContainerBuilder builder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            builder.RegisterType<PlanetRepository>().As<IPlanetRepository>();
            builder.RegisterType<SpaceshipRepository>().As<ISpaceshipRepository>();
            builder.RegisterType<TransportRepository>().As<ITransportRepository>();

            builder.Register(c =>
            {
                var opt = new DbContextOptionsBuilder<AppDbContext>();

                // TODO
                if (environment == "Development")
                {
                    opt.UseInMemoryDatabase("InMem");
                }
                else
                {
                    opt.UseSqlServer(Configuration.GetConnectionString("SpaceshipCargoDb"));
                }

                return new AppDbContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();
        }
    }
}
