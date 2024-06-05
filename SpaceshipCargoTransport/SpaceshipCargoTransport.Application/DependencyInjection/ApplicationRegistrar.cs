using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using SpaceshipCargoTransport.Application.Authentication;
using SpaceshipCargoTransport.Application.Profiles;
using SpaceshipCargoTransport.Domain.Repositories;

namespace SpaceshipCargoTransport.Persistence.DependencyInjection
{
    public class ApplicationRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApiKeyAuthorizationFilter>();
            builder.RegisterType<ApiKeyValidator>().As<IApiKeyValidator>();

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
