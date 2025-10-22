using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGManager.Models;

public class Character
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public int Level { get; set; } = 1;

    public int Experience { get; set; } = 0;

    public int Gold { get; set; } = 0;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    [Required]
    public int CharacterClassId { get; set; }

    [ForeignKey("CharacterClassId")]
    public virtual CharacterClass CharacterClass { get; set; } = null!;

    public virtual CharacterStats CharacterStats { get; set; }

    public virtual ICollection<CharacterEquipment> CharacterEquipment { get; set; } = new List<CharacterEquipment>();

    public virtual ICollection<CharacterQuest> CharacterQuests { get; set; } = new List<CharacterQuest>();
}