namespace DB_Project.Models;
public class CharacterStats
{
    public int CharacterStatsId { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int HitPoints { get; set; }

    public Character Character { get; set; } = null!;
}