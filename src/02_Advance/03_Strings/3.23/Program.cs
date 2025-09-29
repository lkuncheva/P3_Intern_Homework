using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter string:");
        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");

            return;
        }

        StringBuilder result = new StringBuilder();

        result.Append(input[0]);

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] != input[i - 1])
            {
                result.Append(input[i]);
            }
        }

        Console.WriteLine(result.ToString());
    }
}