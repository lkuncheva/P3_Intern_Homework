class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a string: ");
        string input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid input. String is null or empty, or contains only whitespace.");
            return;
        }

        int bracketBalance = 0;

        foreach (char character in input)
        {
            if (character == '(')
            {
                bracketBalance++;
            }
            else if (character == ')')
            {
                bracketBalance--;
            }

            if (bracketBalance < 0)
            {
                Console.WriteLine($"The expression is NOT valid. Closing bracket ')' at index {input.IndexOf(character)} is unmatched.");
                return; 
            }
        }

        if (bracketBalance == 0)
        {
            Console.WriteLine("The expression is valid. All brackets are correctly balanced.");
        }
        else
        {
            Console.WriteLine("The expression is NOT valid. One or more opening brackets '(' were never closed.");
        }
    }
}