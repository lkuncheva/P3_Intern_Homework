class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a string: ");
        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Invalid input. String is null or empty.");

            return;
        }

        for (int i = 0; i < input.Length; i++)
        {
            Console.Write(input[input.Length - i - 1]);
        }

        // If we also want to store it
        /*
        char[] charArray = input.ToCharArray();

        Array.Reverse(charArray);

        string reversed = new string(charArray);

        Console.WriteLine(reversed);
        */
    }
}