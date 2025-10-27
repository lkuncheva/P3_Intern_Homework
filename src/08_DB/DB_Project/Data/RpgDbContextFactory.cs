using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RPGManager.Data;

public class RPGDbContextFactory : IDesignTimeDbContextFactory<RpgDbContext>
{
    public RpgDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string connectionString = configuration.GetConnectionString("RpgDbContext") ??
                                  throw new InvalidOperationException("RpgDbContext connection string not found in configuration.");

        var optionsBuilder = new DbContextOptionsBuilder<RpgDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new RpgDbContext(optionsBuilder.Options);
    }
}