using Autofac;
using SpaceshipCargoTransport.Application.Authentication;

namespace SpaceshipCargoTransport.Persistence.DependencyInjection
{
    public class ApplicationRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApiKeyAuthorizationFilter>();
            builder.RegisterType<ApiKeyValidator>().As<IApiKeyValidator>();
        }
    }
}
