class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a string:");
        string input = Console.ReadLine();

        if (input.Length > 20)
        {
            Console.WriteLine($"Input longer than 20 characters. Taking the first 20 characters: {input.Substring(0, 20)}");
            input = input.Substring(0, 20);
        }
        else if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Invalid input. String is null or empty.");
            return;
        }

        Console.WriteLine(input.PadRight(20, '*'));
    }
}