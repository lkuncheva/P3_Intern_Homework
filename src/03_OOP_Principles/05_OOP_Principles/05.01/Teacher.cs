namespace School;

public class Teacher : Person
{
    private readonly List<Discipline> taughtDisciplines = new List<Discipline>();

    public IReadOnlyList<Discipline> TaughtDisciplines => taughtDisciplines;

    public Teacher(string name, string? comments) : base(name, comments) { }

    public void AddDiscipline(Discipline discipline)
    {
        if (discipline != null && !taughtDisciplines.Contains(discipline))
        {
            taughtDisciplines.Add(discipline);
        }
        else
        {
            throw new ArgumentException("The teacher already teaches this discipline!");
        }
    }

    public void RemoveDiscipline(Discipline discipline)
    {
        if (taughtDisciplines.Contains(discipline))
        {
            taughtDisciplines.Remove(discipline); 
        }
        else
        {
            throw new ArgumentException("The teacher doesn't teach this discipline!");
        }
    }
}