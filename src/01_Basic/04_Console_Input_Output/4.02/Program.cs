using System;

class Manager
{
    private string firstName, lastName, phoneNumber;
    private int age;

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public int Age { get; set; }
}

class Company
{
    private Manager manager;
    private string name, address, phoneNumber, webSite;
    private string? faxNumber;

    public Manager Manager { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string WebSite { get; set; }
    public string? FaxNumber { get; set; }

    public override string ToString()
    {
        return "\nCompany Details:\n" +
               "------------------\n\n" +
               $"Company Name: {Name}\n" +
               $"Company Address: {Address}\n" +
               $"Company Phone Number: {PhoneNumber}\n" +
               $"Company Fax Number: {FaxNumber}\n" +
               $"Company Web Site: {WebSite}\n" +
               $"Manager First Name: {Manager.FirstName}\n" +
               $"Manager Last Name: {Manager.LastName}\n" +
               $"Manager Age: {Manager.Age}\n" +
               $"Manager Phone Number:{Manager.PhoneNumber}";
    }
}
class Program
{
    static void Main(string[] args)
    {
        Company company = new Company();
        company.Manager = new Manager();

        Console.Write("Enter company name: ");
        company.Name = Console.ReadLine();

        Console.Write("Enter company address: ");
        company.Address = Console.ReadLine();

        Console.Write("Enter phone number: ");
        company.PhoneNumber = Console.ReadLine();

        Console.Write("Enter fax number: ");
        string? fax = Console.ReadLine();

        if (string.IsNullOrEmpty(fax))
        {
            company.FaxNumber = "No Fax";
        }            
        else
        {
            company.FaxNumber = fax;
        }            

        Console.Write("Enter website: ");
        company.WebSite = Console.ReadLine();

        Console.Write("Enter manager's first name: ");
        company.Manager.FirstName = Console.ReadLine();

        Console.Write("Enter manager's last name: ");
        company.Manager.LastName = Console.ReadLine();

        Console.Write("Enter manager's age: ");
        company.Manager.Age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter manager's phone: ");
        company.Manager.PhoneNumber = Console.ReadLine();

        Console.WriteLine(company);
    }
}