using System;

class Program
{
    static void Main(string[] args)
    {
        char[] letters = new char[26];

        for (int i = 0; i < 26; i++)
        {
            letters[i] = (char)('a' + i);
        }

        Console.WriteLine("Write a word: ");
        string word = Console.ReadLine();

        for (int i = 0; i < word.Length; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                if (word[i] == letters[j])
                {
                    Console.WriteLine(j);
                    break;
                }
            }
        }        
    }
}
