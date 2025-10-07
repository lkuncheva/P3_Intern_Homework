using System.Text;

namespace InheritanceAndPolymorphism;

public class OffsiteCourse : Course
{
    private string? town;

    public string? Town
    {
        get => this.town;
        set
        {
            if (value != null && string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Town name cannot be set to an empty or whitespace string.", nameof(Town));
            }

            this.town = value;
        }
    }

    public OffsiteCourse(string name) : base(name) { }

    public OffsiteCourse(string name, string teacherName) : base(name, teacherName) { }

    public OffsiteCourse(string name, string? teacherName, IList<string> students, string? town = null)
        : base(name, teacherName, students)
    {
        Town = town;
    }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder("OffsiteCourse {");
        result.Append(this.GetBaseCourseInfo());

        if (!string.IsNullOrWhiteSpace(Town))
        {
            result.Append($"; Town = {Town}");
        }

        result.Append(" }");

        return result.ToString();
    }
}