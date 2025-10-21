using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGManager.Models;

public class CharacterStats
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CharacterId { get; set; }

    public int Strength { get; set; } = 10;

    public int Dexterity { get; set; } = 10;

    public int Intelligence { get; set; } = 10;

    public int Constitution { get; set; } = 10;

    public int Wisdom { get; set; } = 10;

    public int Charisma { get; set; } = 10;

    [ForeignKey("CharacterId")]
    public virtual Character Character { get; set; } = null!;
}