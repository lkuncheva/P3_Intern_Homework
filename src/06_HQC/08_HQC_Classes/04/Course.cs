using System.Text;

namespace InheritanceAndPolymorphism;

public abstract class Course
{
    public string Name { get; }
    public string? TeacherName { get; set; }
    public IList<string> Students { get; }

    public Course(string name, string? teacherName = null, IList<string> students = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Course name is mandatory and cannot be null, empty, or whitespace.", nameof(name));
        }

        Name = name;
        TeacherName = teacherName;
        Students = students ?? new List<string>();
    }

    protected string GetStudentsAsString()
    {
        if (Students == null || Students.Count == 0)
        {
            return "{ }";
        }

        return "{ " + string.Join(", ", this.Students) + " }";
    }

    protected virtual StringBuilder GetBaseCourseInfo()
    {
        StringBuilder result = new StringBuilder();
        result.Append($" Name = {this.Name}");

        if (!string.IsNullOrWhiteSpace(this.TeacherName))
        {
            result.Append($"; Teacher = {this.TeacherName}");
        }

        result.Append($"; Students = {this.GetStudentsAsString()}");

        return result;
    }
}