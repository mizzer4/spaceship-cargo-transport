using Autofac;
using Microsoft.OpenApi.Models;
using SpaceshipCargoTransport.Application.Authentication;
using SpaceshipCargoTransport.Notifications.Senders;
using SpaceshipCargoTransport.Persistence.DependencyInjection;
using System.Reflection;

namespace SpaceshipCargoTransport.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions()
                .Configure<ApiKeyAuthorizationFilterOptions>(Configuration.GetSection(nameof(ApiKeyAuthorizationFilterOptions)))
                .Configure<EmailSenderOptions>(Configuration.GetSection(nameof(EmailSenderOptions)));

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid ApiKey",
                    Name = Configuration
                        .GetSection(nameof(ApiKeyAuthorizationFilterOptions))
                        .Get<ApiKeyAuthorizationFilterOptions>().HeaderName,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="ApiKey"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            services.AddControllers();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationRegistrar());
            builder.RegisterModule(new DomainRegistrar());
            builder.RegisterModule(new PersistenceRegistrar(Configuration));
        }

        public void Configure(
          IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors(builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();
            app.UseEndpoints(endpoints =>  endpoints.MapControllers());
        }
    }
}
