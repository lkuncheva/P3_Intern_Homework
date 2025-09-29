using System.Text;

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
            Console.WriteLine("No forbidden words entered. Outputting original text.");
            Console.WriteLine(text);
            return;
        }

        string[] forbiddenWords = forbiddenWordsInput.Split(' ');

        StringBuilder result = new StringBuilder(text);

        foreach (string forbiddenWord in forbiddenWords)
        {
            int startIndex = 0;

            while (startIndex < result.Length)
            {
                int matchIndex = result.ToString().IndexOf(
                                forbiddenWord,
                                startIndex,
                                StringComparison.OrdinalIgnoreCase );

                if (matchIndex == -1)
                {
                    break;
                }

                int wordLength = forbiddenWord.Length;

                bool isLeftBoundaryGood;
                if (matchIndex == 0)
                {
                    isLeftBoundaryGood = true;
                }
                else
                {
                    char leftChar = result[matchIndex - 1];
                    isLeftBoundaryGood = !Char.IsLetterOrDigit(leftChar);
                }

                bool isRightBoundaryGood;
                if (matchIndex + wordLength == result.Length)
                {
                    isRightBoundaryGood = true;
                }
                else
                {
                    char rightChar = result[matchIndex + wordLength];
                    isRightBoundaryGood = !Char.IsLetterOrDigit(rightChar);
                }

                if (isLeftBoundaryGood && isRightBoundaryGood)
                {
                    for (int j = 0; j < wordLength; j++)
                    {
                        result[matchIndex + j] = '*';
                    }

                    startIndex = matchIndex + wordLength;
                }
                else
                {
                    startIndex = matchIndex + 1;
                }
            }
        }

        Console.WriteLine(result.ToString());
    }
}