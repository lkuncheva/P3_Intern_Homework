class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter text:");
        string text = Console.ReadLine();

        //Example input:
        //Write a program that reads a string from the console, and lists all different words in the string, along with information how many times each word is found. Words are separated by spaces.

        if (string.IsNullOrEmpty(text))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");

            return;
        }

        char[] punctuation = { ',', '.', '!', '?', ';', '"', '(', ')' };
        string[] textParts = text.Split(' ');

        Dictionary<string, int> wordsCounts = new Dictionary<string, int>();

        foreach (string word in textParts)
        {
            word.Trim(punctuation);

            string lowercaseWord = word.ToLower();

            if (wordsCounts.ContainsKey(lowercaseWord))
            {
                wordsCounts[lowercaseWord]++;
            }
            else
            {
                wordsCounts.Add(lowercaseWord, 1);
            }
        }

        Console.WriteLine("\nWord counts:");
        foreach (KeyValuePair<string, int> pair in wordsCounts)
        {
            Console.WriteLine($"Word '{pair.Key}': {pair.Value} times");
        }
    }
}