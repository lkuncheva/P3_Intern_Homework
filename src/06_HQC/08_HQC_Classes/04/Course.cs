using System.Text;

namespace InheritanceAndPolymorphism;

public abstract class Course
{
    public string Name { get; }
    public string TeacherName { get; set; }
    public IList<string> Students { get; }

    public Course(string name)
    {
        Name = name;
        TeacherName = "N/A";
        Students = new List<string>();
    }

    public Course(string name, string teacherName, IList<string> students)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Course name is mandatory and cannot be null, empty, or whitespace.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(teacherName))
        {
            throw new ArgumentException("Teacher name is mandatory and cannot be null, empty, or whitespace.", nameof(teacherName));
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

        return "{ " + string.Join(", ", Students) + " }";
    }

    protected virtual StringBuilder GetBaseCourseInfo()
    {
        var result = new StringBuilder();

        result.Append($" Name = {Name}");
        result.Append($"; Teacher = {TeacherName}");
        result.Append($"; Students = {GetStudentsAsString()}");

        return result;
    }
}