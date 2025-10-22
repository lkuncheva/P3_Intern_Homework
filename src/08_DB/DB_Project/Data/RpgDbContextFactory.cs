using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RPGManager.Data;

public class RPGDbContextFactory : IDesignTimeDbContextFactory<RpgDbContext>
{
    public RpgDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RpgDbContext>();
        optionsBuilder.UseSqlServer("Server=P3U3EUJRQS67NTP;Database=DbProjectRpg;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");

        return new RpgDbContext(optionsBuilder.Options);
    }
}