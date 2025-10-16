namespace DB_Project.Models;
public class Character
{
    public int CharacterId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public CharacterStats Stats { get; set; } = null!;
    public int ClassId { get; set; }
    public Class Class { get; set; } = null!;
    public ICollection<CharacterItem> CharacterItems { get; set; } = new List<CharacterItem>();
}