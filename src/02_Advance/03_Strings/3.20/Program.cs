using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter text:");
        string text = Console.ReadLine();

        //Example input:
        //Write a program that extracts from a given text all palindromes, e.g. ABBA, lamal, exe.

        if (string.IsNullOrEmpty(text))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");
            return;
        }

        char[] punctuation = { ',', '.', '!', '?', ';' };
        string[] textParts = text.Split(' ');

        for (int i = 0; i < textParts.Length; i++)
        {
            string cleanedPart = textParts[i].TrimEnd(punctuation);
            bool isPalindrome = false;

            for (int j = 0; j < cleanedPart.Length / 2; j++)
            {
                if (cleanedPart[j] != cleanedPart[cleanedPart.Length - 1 - j])
                {
                    isPalindrome = false;
                    break;
                }
                isPalindrome = true;
            }

            if (isPalindrome)
            {
                Console.WriteLine(cleanedPart);
            }
        }
    }
}
