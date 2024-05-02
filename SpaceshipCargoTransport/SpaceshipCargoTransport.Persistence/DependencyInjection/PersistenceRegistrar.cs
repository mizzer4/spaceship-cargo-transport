using Autofac;
using Microsoft.EntityFrameworkCore;
using SpaceshipCargoTransport.Domain.Repositories;
using SpaceshipCargoTransport.Persistence.Db;
using SpaceshipCargoTransport.Persistence.Repositories;

namespace SpaceshipCargoTransport.Persistence.DependencyInjection
{
    public class PersistenceRegistrar : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SpaceshipRepository>().As<ISpaceshipRepository>();

            builder.Register(c =>
            {
                var opt = new DbContextOptionsBuilder<AppDbContext>();
                opt.UseInMemoryDatabase("InMem");

                return new AppDbContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();
        }
    }
}
