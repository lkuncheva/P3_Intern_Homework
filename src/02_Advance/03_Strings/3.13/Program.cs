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

        // I will try to implement this task with keeping the punctuation in place.... TBC>>>
        char[] signs = { ',', '!', '.', '?' };
        
        string[] words = input.Split(' ');
        Array.Reverse(words);

        Console.WriteLine(string.Join(" ", words));
    }
}
