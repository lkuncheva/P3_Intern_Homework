class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter text:");
        string text = Console.ReadLine();

        //Example input:
        //Write a program that reads a String from the console, aNd prints all different letters in the stRing along with information how many tImes each letter is found.

        if (string.IsNullOrEmpty(text))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");

            return;
        }

        Dictionary<char, int> letterCounts = new Dictionary<char, int>();

        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                char lowercaseLetter = char.ToLower(c);

                if (letterCounts.ContainsKey(lowercaseLetter))
                {
                    letterCounts[lowercaseLetter]++;
                }
                else
                {
                    letterCounts.Add(lowercaseLetter, 1);
                }
            }
        }

        Console.WriteLine("\nLetter counts:");
        foreach (KeyValuePair<char, int> pair in letterCounts)
        {
            Console.WriteLine($"Letter '{pair.Key}': {pair.Value} times");
        }
    }
}