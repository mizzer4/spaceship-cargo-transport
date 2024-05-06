using Autofac;
using SpaceshipCargoTransport.Domain.Services;

namespace SpaceshipCargoTransport.Persistence.DependencyInjection
{
    public class DomainRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SpaceshipService>().As<ISpaceshipService>();
            builder.RegisterType<PlanetService>().As<IPlanetService>();
            builder.RegisterType<TransportService>().As<ITransportService>();
        }
    }
}
