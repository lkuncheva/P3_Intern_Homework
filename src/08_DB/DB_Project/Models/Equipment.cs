using System.ComponentModel.DataAnnotations;

namespace RPGManager.Models;

public class Equipment
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Type { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Rarity { get; set; } = "Common";

    public int AttackBonus { get; set; } = 0;

    public int DefenseBonus { get; set; } = 0;

    public virtual ICollection<CharacterEquipment> CharacterEquipment { get; set; } = new List<CharacterEquipment>();
}