using DB_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace DB_Project.Data;
public class RpgDbContext : DbContext
{
    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<CharacterStats> Stats { get; set; } = null!;
    public DbSet<Class> Classes { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<CharacterItem> CharacterItems { get; set; } = null!;

    public RpgDbContext(DbContextOptions<RpgDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = "Server=P3U3EUJRQS67NTP;Database=DbProject;Trusted_Connection=True;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CharacterItem>()
            .HasKey(ci => new { ci.CharacterId, ci.ItemId });

        modelBuilder.Entity<Character>()
            .HasOne(c => c.Stats)
            .WithOne(cs => cs.Character)
            .HasForeignKey<CharacterStats>(cs => cs.CharacterStatsId);

        modelBuilder.Entity<Class>()
            .HasMany(c => c.Characters)
            .WithOne(c => c.Class)
            .HasForeignKey(c => c.ClassId);

        base.OnModelCreating(modelBuilder);
    }
}