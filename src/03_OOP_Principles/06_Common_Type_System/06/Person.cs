namespace _06;

public class Person
{
    public string Name { get; set; }
    public int? Age { get; set; }

    public Person(string name, int? age = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty.");
        }

        Name = name;

        if (age < 0)
        {
            throw new ArgumentException("Age cannot be a negative number.");
        }

        Age = age;
    }

    public override string ToString()
    {
        if (Age.HasValue)
        {
            return $"Person: {Name}, Age: {Age.Value} years old.";
        }
        else
        {
            return $"Person: {Name}, Age: Unspecified.";
        }
    }
}