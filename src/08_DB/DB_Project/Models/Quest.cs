using System.ComponentModel.DataAnnotations;

namespace RPGManager.Models;

public class Quest
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public int RewardGold { get; set; } = 0;

    public int RewardExperience { get; set; } = 0;

    public int RequiredLevel { get; set; } = 1;

    [MaxLength(50)]
    public string Difficulty { get; set; } = "Easy";

    public virtual ICollection<CharacterQuest> CharacterQuests { get; set; } = new List<CharacterQuest>();
}