class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter text: ");
        string text = Console.ReadLine().ToLower();

        if (string.IsNullOrEmpty(text))
        {
            Console.WriteLine("Invalid input. String is null or empty.");
            return;
        }

        Console.WriteLine("Enter a sub-string: ");
        string subString = Console.ReadLine().ToLower();

        if (string.IsNullOrEmpty(subString))
        {
            Console.WriteLine("Invalid input. Sub-string is null or empty.");
            return;
        }

        int count = 0;

        for (int i = 0; i <= text.Length - subString.Length; i++)
        {
            if (text.Substring(i, subString.Length) == subString)
            {
                count++;
            }
        }

        Console.WriteLine(count);

        // Another way using Split()
        // This implementation doesnt work well with overlapping substrings
        /*
        string[] parts = text.Split(subString);
        count = parts.Length - 1;
        Console.WriteLine(count);
        */
    }
}