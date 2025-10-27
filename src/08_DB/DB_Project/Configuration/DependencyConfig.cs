using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RPGManager.Data;
using RPGManager.Interfaces;
using RPGManager.Repositories;
using RPGManager.Services;

namespace RPGManager.Configuration;

public class DependencyConfig
{
    public static IContainer Configure()
    {
        var builder = new ContainerBuilder();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        builder.RegisterInstance(configuration).As<IConfiguration>().SingleInstance();

        builder.Register(c =>
        {
            var config = c.Resolve<IConfiguration>();
            string connectionString = config.GetConnectionString("RpgDbContext");

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

        builder.RegisterType<CharacterStatsService>()
            .As<ICharacterStatsService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CharacterQuestService>()
            .As<ICharacterQuestService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CharacterEquipmentService>()
            .As<ICharacterEquipmentService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<QuestService>()
            .As<IQuestService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<EquipmentService>()
            .As<IEquipmentService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<DataSeederService>()
            .As<IDataSeederService>()
            .InstancePerLifetimeScope();

        return builder.Build();
    }
}