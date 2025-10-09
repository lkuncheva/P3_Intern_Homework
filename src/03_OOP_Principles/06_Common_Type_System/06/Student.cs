namespace _06;

public class Student : ICloneable
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }

    public string Name
    {
        get
        {
            return FirstName + " " + MiddleName + " " + LastName;
        }
    }

    public string? SSN { get; set; }

    public string PermanentAddress { get; set; }
    public string MobilePhone { get; set; }
    public string Email { get; set; }
    public string Course { get; set; }

    public Specialty Specialty { get; set; }
    public University University { get; set; }
    public Faculty Faculty { get; set; }

    public Student(string firstName, string middleName, string lastName, string? ssn,
                    string address, string mobile, string email, string course,
                    Specialty specialty, University university, Faculty faculty)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        SSN = ssn;
        PermanentAddress = address;
        MobilePhone = mobile;
        Email = email;
        Course = course;
        Specialty = specialty;
        University = university;
        Faculty = faculty;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Student otherStudent || obj is null || this.SSN is null)
        {
            return false;
        }

        return this.SSN == otherStudent.SSN;
    }

    public override int GetHashCode()
    {
        return SSN?.GetHashCode() ?? 0;
    }

    public override string ToString()
    {
        return $"Student: {Name}\n" +
                $"  SSN: {SSN}\n" +
                $"  Contact: {MobilePhone} | {Email}\n" +
                $"  Address: {PermanentAddress}\n" +
                $"  Studies: Course {Course}, {Specialty} ({Faculty})\n" +
                $"  University: {University}";
    }

    public static bool operator ==(Student student1, Student student2)
    {
        if (ReferenceEquals(student1, student2))
        {
            return true;
        }

        if (ReferenceEquals(student1, null) || ReferenceEquals(student2, null))
        {
            return false;
        }

        return student1.Equals(student2);
    }

    public static bool operator !=(Student student1, Student student2)
    {
        return !(student1 == student2);
    }

    public object Clone()
    {
        Student deepCopy = new Student(
            this.FirstName,
            this.MiddleName,
            this.LastName,
            this.SSN,
            this.PermanentAddress,
            this.MobilePhone,
            this.Email,
            this.Course,
            this.Specialty,
            this.University,
            this.Faculty
        );

        return deepCopy;
    }
}