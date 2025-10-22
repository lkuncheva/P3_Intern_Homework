using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGManager.Models;

public class CharacterEquipment
{
    [Required]
    public int CharacterId { get; set; }

    [Required]
    public int EquipmentId { get; set; }

    public bool IsEquipped { get; set; } = false;

    [ForeignKey("CharacterId")]
    public virtual Character Character { get; set; } = null!;

    [ForeignKey("EquipmentId")]
    public virtual Equipment Equipment { get; set; } = null!;
}