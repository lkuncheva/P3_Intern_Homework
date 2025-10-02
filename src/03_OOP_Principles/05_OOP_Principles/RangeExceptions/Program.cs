namespace RangeExceptions;

public class Program
{
    private static void TestIntRange()
    {
        Console.WriteLine("\n--- Testing Integer Range ---");

        const int rangeStart = 1;
        const int rangeEnd = 100;

        Console.Write($"Please enter a number in the range [{rangeStart} ... {rangeEnd}]: ");

        if (!int.TryParse(Console.ReadLine(), out int inputNumber))
        {
            Console.WriteLine("Input error: That was not a valid integer.");
            return;
        }

        try
        {
            if (inputNumber < rangeStart || inputNumber > rangeEnd)
            {
                string errorMessage = $"The number {inputNumber} is outside the allowed range.";

                throw new InvalidRangeException<int>(errorMessage, rangeStart, rangeEnd);
            }

            Console.WriteLine($"SUCCESS: The number {inputNumber} is valid.");
        }
        catch (InvalidRangeException<int> ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[ERROR] Invalid Range Encountered!");
            Console.WriteLine($"\tDetails: {ex.Message}");
            Console.WriteLine($"\tViolating Value: {inputNumber}");
            Console.ResetColor();
        }
    }

    private static void TestDateTimeRange()
    {
        Console.WriteLine("\n--- Testing DateTime Range ---");

        DateTime rangeStart = new DateTime(1980, 1, 1);
        DateTime rangeEnd = new DateTime(2013, 12, 31);

        Console.Write($"Please enter a date (e.g., 25/10/2005) in the range [{rangeStart:d} ... {rangeEnd:d}]: ");

        if (!DateTime.TryParse(Console.ReadLine(), out DateTime inputDate))
        {
            Console.WriteLine("Input error: That was not a valid date format.");
            return;
        }

        try
        {
            if (inputDate.CompareTo(rangeStart) < 0 || inputDate.CompareTo(rangeEnd) > 0)
            {
                string errorMessage = $"The date {inputDate:d} is outside the allowed time frame.";

                throw new InvalidRangeException<DateTime>(errorMessage, rangeStart, rangeEnd);
            }

            Console.WriteLine($"SUCCESS: The date {inputDate:d} is valid.");
        }
        catch (InvalidRangeException<DateTime> ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[ERROR] Invalid Range Encountered!");
            Console.WriteLine($"\tDetails: {ex.Message}");
            Console.WriteLine($"\tViolating Value: {inputDate:d}");
            Console.ResetColor();
        }
    }

    public static void Main(string[] args)
    {
        TestIntRange();
        TestDateTimeRange();
    }
}