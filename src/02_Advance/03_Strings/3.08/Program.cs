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

        Console.WriteLine("Enter a word: ");
        string word = Console.ReadLine().ToLower();

        if (string.IsNullOrEmpty(word))
        {
            Console.WriteLine("Invalid input. String is null or empty.");

            return;
        }

        string[] sentences = text.Split('.');
        
        for (int i = 0; i < sentences.Length; i++)
        {
            if (sentences[i].ToLower().Contains(' ' + word + ' '))
            {
                Console.WriteLine(sentences[i].Trim() + ".");
            }
        }
    }
}