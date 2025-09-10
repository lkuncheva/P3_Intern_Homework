using System;
using System.Reflection;

class Employee
{
    private byte age;
    private char gender;

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long PersonalId { get; set; }
    public int EmployeeNumber { get; set; }
    public byte Age
    {
        get { return age; }
        set
        {
            if (value > 0 && value <= 100)
                age = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value), "Age must be between 1 and 100.");
        }
    }
    public char Gender
    {
        get { return gender; }
        set
        {
            if (value == 'm' || value == 'f')
                gender = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value), "Use \"m\" for male or \"f\" for female ");
        }
    }


    public Employee(string firstName, string lastName, byte age, char gender, long personalId, int employeeNumber)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Age = age;
        this.Gender = gender;
        this.PersonalId = personalId;
        this.EmployeeNumber = employeeNumber;
    }

}
class Program
{
    static void Main(string[] args)
    {
        Employee theBestEmployeeEver = new Employee("Lia", "Kuntcheva", 23, 'f', 6969696969, 27560420);

        Console.WriteLine("Employee details:\n");
        Console.WriteLine($"First name: {theBestEmployeeEver.FirstName}\nLast name: {theBestEmployeeEver.LastName}\nAge: {theBestEmployeeEver.Age}\n" +
            $"Gender: {theBestEmployeeEver.Gender}\nPersonalID: {theBestEmployeeEver.PersonalId}\nEmployee number: {theBestEmployeeEver.EmployeeNumber}");
    }
}