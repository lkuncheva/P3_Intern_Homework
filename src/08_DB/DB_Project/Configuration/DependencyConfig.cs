using Autofac;
using RPGManager.Data;
using RPGManager.Interfaces;
using RPGManager.Repositories;
using RPGManager.Services;
using Microsoft.EntityFrameworkCore;

namespace RPGManager.Configuration;

public class DependencyConfig
{
    public static IContainer Configure()
    {
        var builder = new ContainerBuilder();

        builder.Register(c =>
        {
            string connectionString = "Server=(localdb)\\msqllocaldb;Database=DbProjectRpg;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";
            var optionsBuilder = new DbContextOptionsBuilder<RpgDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new RpgDbContext(optionsBuilder.Options);
        }).AsSelf().InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(Repository<>))
            .As(typeof(IRepository<>))
            .InstancePerLifetimeScope();

        builder.RegisterType<CharacterRepository>()
            .As<ICharacterRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CharacterService>()
            .As<ICharacterService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<QuestService>()
            .As<IQuestService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<DataSeederService>()
            .As<IDataSeederService>()
            .InstancePerLifetimeScope();

        return builder.Build();
    }
}