using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter list of words separated by spaces:");
        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");
            return;
        }

        string[] words = input.Split(' ');
        Array.Sort(words);
        string result = string.Join(" ", words);

        Console.WriteLine(result);
    }
}