using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter text:");
        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");

            return;
        }

        StringBuilder unicodeString = new StringBuilder();

        foreach (char c in input)
        {
            unicodeString.Append($"\\u{((int)c).ToString("X4")}");
        }

        Console.WriteLine(unicodeString.ToString());
    }
}