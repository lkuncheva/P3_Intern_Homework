namespace School;
public class StudentClass
{
    private readonly string identifier; 
    private string comments;
    private readonly List<Student> students = new List<Student>();
    private readonly List<Teacher> teachers = new List<Teacher>();

    public string Identifier => identifier;

    public string Comments { get => comments; set => comments = value; }

    public IReadOnlyList<Student> Students => students;
    public IReadOnlyList<Teacher> Teachers => teachers;

    public StudentClass(string identifier)
    {
        if (string.IsNullOrWhiteSpace(identifier))
            throw new ArgumentException("Group identifier cannot be empty.");
        this.identifier = identifier;
    }

    public void AddStudent(Student student)
    {
        if (student != null && !students.Any(s => s.ClassNumber == student.ClassNumber))
        {
            students.Add(student);
        }
    }

    public void RemoveStudent(Student student)
    {
        students.Remove(student);
    }

    public void AddTeacher(Teacher teacher)
    {
        if (teacher != null && !teachers.Contains(teacher))
        {
            teachers.Add(teacher);
        }
    }

    public void RemoveTeacher(Teacher teacher)
    {
        teachers.Remove(teacher);
    }
}