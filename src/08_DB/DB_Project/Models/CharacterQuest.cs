using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGManager.Models;

public class CharacterQuest
{
    [Required]
    public int CharacterId { get; set; }

    [Required]
    public int QuestId { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "NotStarted";

    public DateTime? StartedDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    [ForeignKey("CharacterId")]
    public virtual Character Character { get; set; } = null!;

    [ForeignKey("QuestId")]
    public virtual Quest Quest { get; set; } = null!;
}