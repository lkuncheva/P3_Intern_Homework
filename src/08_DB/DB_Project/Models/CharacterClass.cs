using System.ComponentModel.DataAnnotations;

namespace RPGManager.Models;

public class CharacterClass
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public int BaseHealth { get; set; }

    public int BaseMana { get; set; }

    [MaxLength(50)]
    public string PrimaryAttribute { get; set; } = string.Empty;

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();
}