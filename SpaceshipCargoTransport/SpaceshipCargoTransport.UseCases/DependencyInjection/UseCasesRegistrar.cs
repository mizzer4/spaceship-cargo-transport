using Autofac;
using AutoMapper;
using SpaceshipCargoTransport.Application.Profiles;
using SpaceshipCargoTransport.Domain.Services;

namespace SpaceshipCargoTransport.Persistence.DependencyInjection
{
    public class UseCasesRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SpaceshipService>().As<ISpaceshipService>();
            builder.RegisterType<PlanetService>().As<IPlanetService>();
            builder.RegisterType<TransportService>().As<ITransportService>();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SpaceshipProfile());
                cfg.AddProfile(new PlanetProfile());
                cfg.AddProfile(new TransportProfile());
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            }).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
