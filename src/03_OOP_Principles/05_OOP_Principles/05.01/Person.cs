namespace School;

public abstract class Person
{
    private readonly string name;
    private string? comments;

    public string Name => name;
    public string? Comments { get => comments; set => comments = value; }

    public Person(string name, string? comments)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty.");
        }
                
        this.name = name;
        this.comments = comments;
    }
}