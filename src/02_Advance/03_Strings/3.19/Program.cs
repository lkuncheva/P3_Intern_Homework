using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter text:");
        string text = Console.ReadLine();

        //Example input:
        //Valid dates are 15.03.2023, and 25.12.2024. Invalid date is 31.02.2023 or .01.2025.

        if (string.IsNullOrEmpty(text))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");

            return;
        }

        char[] punctuation = { ',', '.', '!', '?', ';' };
        string[] textParts = text.Split(' ');
        string formatString = "dd.MM.yyyy";

        CultureInfo canadianCulture = new CultureInfo("en-CA");

        foreach (string part in textParts)
        {
            string cleanedPart = part.TrimEnd(punctuation);

            DateTime date;
            if (DateTime.TryParseExact(cleanedPart, formatString, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                Console.WriteLine(date.ToString("d", canadianCulture));
            }
        }
    }
}