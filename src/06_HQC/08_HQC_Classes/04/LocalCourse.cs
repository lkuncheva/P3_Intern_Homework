using System.Text;

namespace InheritanceAndPolymorphism;

public class LocalCourse : Course
{
    private string? lab;

    public string? Lab
    {
        get => this.lab;
        set
        {
            if (value != null && string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Lab name cannot be set to an empty or whitespace string.", nameof(Lab));
            }

            this.lab = value;
        }
    }

    public LocalCourse(string name) : base(name) { }

    public LocalCourse(string name, string? teacherName) : base(name, teacherName) { }

    public LocalCourse(string name, string? teacherName, IList<string> students, string? lab = null)
            : base(name, teacherName, students)
    {
        Lab = lab;
    }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder("LocalCourse {");
        result.Append(GetBaseCourseInfo());

        if (!string.IsNullOrWhiteSpace(Lab))
        {
            result.Append($"; Lab = {Lab}");
        }

        result.Append(" }");

        return result.ToString();
    }
}