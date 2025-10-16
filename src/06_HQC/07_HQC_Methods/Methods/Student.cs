namespace Methods;
class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BirthCity { get; set; }
    public DateTime DateOfBirth { get; set; }

    public Student(string firstName, string lastName, string birthCity, DateTime dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));
        }

        if (string.IsNullOrWhiteSpace(birthCity))
        {
            throw new ArgumentException("Birth city cannot be null or empty.", nameof(birthCity));
        }

        if (!dateOfBirth.HasValue)
        {
            throw new ArgumentNullException(nameof(dateOfBirth), "The date of birth must be provided and cannot be null.");
        }

        FirstName = firstName;
        LastName = lastName;
        BirthCity = birthCity;
        DateOfBirth = dateOfBirth.Value;
    }

    public bool IsOlderThan(Student otherStudent)
    {
        if (otherStudent == null)
        {
            throw new ArgumentNullException(nameof(otherStudent), "Cannot compare age to a null student object.");
        }

        return DateOfBirth < otherStudent.DateOfBirth;
    }
}