using Autofac.Extensions.DependencyInjection;
using SpaceshipCargoTransport.Application;

var builder = Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.UseStartup<Startup>());

builder.Build().Run();
