namespace DB_Project.Models;
public class Item
{
    public int ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public ICollection<CharacterItem> CharacterItems { get; set; } = new List<CharacterItem>();
}