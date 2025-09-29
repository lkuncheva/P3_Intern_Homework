class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter text: ");
        string text = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Invalid input. String is null or empty, or contains only whitespace.");
            return;
        }

        Console.WriteLine("Enter a sub-string: ");
        string subString = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(subString))
        {
            Console.WriteLine("Invalid input. Sub-string is null or empty, or contains only whitespace.");
            return;
        }

        int count = 0;
        int startIndex = 0;
        int subStringLength = subString.Length;

        while (startIndex < text.Length)
        {
            int index = text.IndexOf(subString, startIndex, StringComparison.OrdinalIgnoreCase );

            if (index != -1)
            {
                count++;

                startIndex = index + 1;
            }
            else
            {
                break;
            }
        }
    }
}