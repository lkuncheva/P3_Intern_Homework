namespace DB_Project.Models;
public class CharacterItem
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }

    public Character Character { get; set; } = null!;
    public Item Item { get; set; } = null!;
}