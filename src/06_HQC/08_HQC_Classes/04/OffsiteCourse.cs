using System.Text;

namespace InheritanceAndPolymorphism;

public class OffsiteCourse : Course
{
    public string Town { get; set; }

    public OffsiteCourse(string name) : base(name) { }

    public OffsiteCourse(string name, string teacherName, IList<string> students, string town)
        : base(name, teacherName, students)
    {
        if (town != null && string.IsNullOrWhiteSpace(town))
        {
            throw new ArgumentException("Town name cannot be set to an empty or whitespace string.", nameof(Town));
        }

        Town = town;
    }

    public override string ToString()
    {
        var result = new StringBuilder("OffsiteCourse {");

        result.Append(GetBaseCourseInfo());
        result.Append($"; Town = {Town} }}");

        return result.ToString();
    }
}