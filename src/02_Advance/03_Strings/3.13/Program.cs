using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter text:");
        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");
            return;
        }

        char[] signs = { ',', '!', '.', '?', ';', ':'};
        
        string[] words = input.Split(' ');
        char[] signsInPlace = new char[words.Length];

        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            for (int j = 0; j < signs.Length; j++)
            {
                if (word[word.Length - 1] == signs[j])
                {
                    signsInPlace[i] = signs[j];
                    words[i] = words[i].TrimEnd(signs);
                    break;
                }
            }
        }

        Array.Reverse(words);

        for (int i = 0; i < words.Length; i++)
        {
            words[i] += signsInPlace[i];
        }

        Console.WriteLine(string.Join(" ", words));
    }
}
