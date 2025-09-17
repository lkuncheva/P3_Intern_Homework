class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a string (up to 20 characters):");
        string input = Console.ReadLine();

        if (input.Length > 20)
        {
            Console.WriteLine("Invalid input. String is longer than 20 characters");
            return;
        }

        Console.WriteLine(input.PadRight(20, '*'));
    }
}