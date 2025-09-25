class Program
{
    static void Main(string[] args)
    {
        
        Console.WriteLine("Enter text: ");
        string text = Console.ReadLine();

        if (string.IsNullOrEmpty(text))
        {
            Console.WriteLine("Invalid input. String is null or empty.");

            return;
        }

        Console.WriteLine("Enter forbidden words separated by a whitespace: ");
        string forbiddenWordsInput = Console.ReadLine();

        if (string.IsNullOrEmpty(forbiddenWordsInput))
        {
            Console.WriteLine("Invalid input. String is null or empty.");

            return;
        }

        string[] forbiddenWords = forbiddenWordsInput.Split(' ');

        // Case insensitive
        /*
        StringBuilder result = new StringBuilder(text);

        for (int i = 0; i < forbiddenWords.Length; i++)
        {
            int startIndex = 0;

            while ((startIndex = result.ToString().IndexOf(forbiddenWords[i], startIndex, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                for (int j = 0; j < forbiddenWords[i].Length; j++)
                {
                    result[startIndex + j] = '*';
                }

                startIndex += forbiddenWords[i].Length;
            }
        }

        Console.WriteLine(result.ToString());
        */

        // Case sensitive
        for (int i = 0; i < forbiddenWords.Length; i++)
        {
            string asterisks = new string('*', forbiddenWords[i].Length);
            if (text.Contains(forbiddenWords[i]))
            {
                text = text.Replace(forbiddenWords[i], asterisks);
            }
        }

        Console.WriteLine(text);
    }
}