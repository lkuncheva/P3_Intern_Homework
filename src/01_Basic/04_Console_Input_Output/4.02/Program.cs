using System;

class Manager
{
    public string firstName, lastName, phoneNumber;
    public int age;


}

class Company
{

    public Manager manager;
    public string name, address, phoneNumber, webSite;
    public string? faxNumber;

    public override string ToString()
    {

        return "\nCompany Details:\n" +
               "------------------\n\n" +
               $"Company Name: {name}\n" +
               $"Company Address: {address}\n" +
               $"Company Phone Number: {phoneNumber}\n" +
               $"Company Fax Number: {faxNumber}\n" +
               $"Company Web Site: {webSite}\n" +
               $"Manager First Name: {manager.firstName}\n" +
               $"Manager Last Name: {manager.lastName}\n" +
               $"Manager Age: {manager.age}\n" +
               $"Manager Phone Number:{manager.phoneNumber}";
    }

}
class Program
{
    static void Main(string[] args)
    {
        Company company = new Company();
        company.manager = new Manager();

        Console.Write("Enter company name: ");
        company.name = Console.ReadLine();

        Console.Write("Enter company address: ");
        company.address = Console.ReadLine();

        Console.Write("Enter phone number: ");
        company.phoneNumber = Console.ReadLine();

        Console.Write("Enter fax number: ");
        string? fax = Console.ReadLine();

        if (string.IsNullOrEmpty(fax)) 
            company.faxNumber = "No Fax";
        else
            company.faxNumber = fax;

        Console.Write("Enter website: ");
        company.webSite = Console.ReadLine();

        Console.Write("Enter manager's first name: ");
        company.manager.firstName = Console.ReadLine();

        Console.Write("Enter manager's last name: ");
        company.manager.lastName = Console.ReadLine();

        Console.Write("Enter manager's age: ");
        company.manager.age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter manager's phone: ");
        company.manager.phoneNumber = Console.ReadLine();

        Console.WriteLine(company);
    }
}