using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        const string dateFormat = "d.M.yyyy";

        Console.WriteLine("Enter first date in format day.month.year");
        string input1 = Console.ReadLine();

        if (string.IsNullOrEmpty(input1))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");
            return;
        }

        DateTime date1 = DateTime.ParseExact(input1, dateFormat, CultureInfo.InvariantCulture);

        Console.WriteLine("Enter second date in format day.month.year");
        string input2 = Console.ReadLine();

        if (string.IsNullOrEmpty(input2))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");
            return;
        }

        DateTime date2 = DateTime.ParseExact(input2, dateFormat, CultureInfo.InvariantCulture);

        TimeSpan difference = date2 - date1;

        int days = Math.Abs(difference.Days);
        Console.WriteLine($"\nDistance: {days} days");
    }
}