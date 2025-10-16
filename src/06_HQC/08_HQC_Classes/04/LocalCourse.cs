using System.Text;

namespace InheritanceAndPolymorphism;

public class LocalCourse : Course
{
    public string Lab { get; set; }

    public LocalCourse(string name) : base(name) 
    {
        Lab = "N/A";
    }

    public LocalCourse(string name, string teacherName, IList<string> students, string lab)
            : base(name, teacherName, students)
    {
        if (lab != null && string.IsNullOrWhiteSpace(lab))
        {
            throw new ArgumentException("Lab name cannot be set to an empty or whitespace string.", nameof(Lab));
        }

        Lab = lab;
    }

    public override string ToString()
    {
        var result = new StringBuilder("LocalCourse {");

        result.Append(GetBaseCourseInfo());
        result.Append($"; Lab = {Lab} }}");

        return result.ToString();
    }
}