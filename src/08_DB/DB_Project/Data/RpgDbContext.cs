using Microsoft.EntityFrameworkCore;
using RPGManager.Models;

namespace RPGManager.Data;

public class RpgDbContext : DbContext
{
    public RpgDbContext(DbContextOptions<RpgDbContext> options) : base(options)
    {
    }

    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<CharacterClass> CharacterClasses { get; set; } = null!;
    public DbSet<CharacterStats> CharacterStats { get; set; } = null!;
    public DbSet<Equipment> Equipment { get; set; } = null!;
    public DbSet<CharacterEquipment> CharacterEquipment { get; set; } = null!;
    public DbSet<Quest> Quests { get; set; } = null!;
    public DbSet<CharacterQuest> CharacterQuests { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // One-to-Many: CharacterClass -> Character
        modelBuilder.Entity<Character>()
            .HasOne(c => c.CharacterClass)
            .WithMany(cc => cc.Characters)
            .HasForeignKey(c => c.CharacterClassId)
            .OnDelete(DeleteBehavior.Restrict);

        // One-to-One: Character -> CharacterStats
        modelBuilder.Entity<Character>()
            .HasOne(c => c.CharacterStats)
            .WithOne(cs => cs.Character)
            .HasForeignKey<CharacterStats>(cs => cs.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);

        // Many-to-Many: Character <-> Equipment through CharacterEquipment
        modelBuilder.Entity<CharacterEquipment>()
            .HasKey(ce => new { ce.CharacterId, ce.EquipmentId });

        modelBuilder.Entity<CharacterEquipment>()
            .HasOne(ce => ce.Character)
            .WithMany(c => c.CharacterEquipment)
            .HasForeignKey(ce => ce.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CharacterEquipment>()
            .HasOne(ce => ce.Equipment)
            .WithMany(e => e.CharacterEquipment)
            .HasForeignKey(ce => ce.EquipmentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Many-to-Many: Character <-> Quest through CharacterQuest
        modelBuilder.Entity<CharacterQuest>()
            .HasKey(cq => new { cq.CharacterId, cq.QuestId });

        modelBuilder.Entity<CharacterQuest>()
            .HasOne(cq => cq.Character)
            .WithMany(c => c.CharacterQuests)
            .HasForeignKey(cq => cq.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CharacterQuest>()
            .HasOne(cq => cq.Quest)
            .WithMany(q => q.CharacterQuests)
            .HasForeignKey(cq => cq.QuestId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Character>()
            .HasIndex(c => c.Name);

        modelBuilder.Entity<Character>()
            .HasIndex(c => c.Level);

        modelBuilder.Entity<CharacterClass>()
            .HasIndex(cc => cc.Name)
            .IsUnique();

        modelBuilder.Entity<Quest>()
            .HasIndex(q => q.Title);

        modelBuilder.Entity<Quest>()
            .HasIndex(q => q.Difficulty);

        modelBuilder.Entity<Equipment>()
            .HasIndex(e => e.Name);

        modelBuilder.Entity<Equipment>()
            .HasIndex(e => e.Rarity);
    }
}