namespace DB_Project.Models;
public class Class
{
    public int ClassId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<Character> Characters { get; set; } = new List<Character>();
}