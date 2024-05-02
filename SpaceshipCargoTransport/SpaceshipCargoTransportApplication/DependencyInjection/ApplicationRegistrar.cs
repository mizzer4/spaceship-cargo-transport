using Autofac;
using AutoMapper;
using SpaceshipCargoTransport.Application.Profiles;

namespace SpaceshipCargoTransport.Persistence.DependencyInjection
{
    public class ApplicationRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SpaceshipProfile());
            }
            )).AsSelf().SingleInstance();

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper())
            .As<IMapper>()
            .InstancePerLifetimeScope();
        }
    }
}
