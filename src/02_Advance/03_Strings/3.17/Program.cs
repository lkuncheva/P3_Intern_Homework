using System.Globalization;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        const string dateFormat = "d.M.yyyy H:m:s";

        Console.WriteLine("Enter first date in format day.month.year");
        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");

            return;
        }

        DateTime date = DateTime.ParseExact(input, dateFormat, CultureInfo.InvariantCulture);
        DateTime newDate = date.AddHours(6.5);

        CultureInfo bulgarianCulture = new CultureInfo("bg-BG");

        string newDateString = newDate.ToString(dateFormat);
        string dayOfWeekBulgarian = newDate.ToString("dddd", bulgarianCulture);

        Console.WriteLine($"\nDate and time after 6 hours and 30 minutes:");
        Console.WriteLine($"{newDateString}");
        Console.WriteLine($"Day of the week in Bulgarian: {dayOfWeekBulgarian}");
    }
}