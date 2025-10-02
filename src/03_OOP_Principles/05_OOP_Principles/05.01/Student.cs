namespace School;

public class Student : Person
{
    private readonly int classNumber;

    public int ClassNumber => classNumber;

    public Student(string name, int classNumber, string? comments) : base(name, comments)
    {
        if (classNumber <= 0)
        {
            throw new ArgumentException("Class number must be positive.");
        }

        this.classNumber = classNumber;
    }
}