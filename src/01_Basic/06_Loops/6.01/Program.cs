using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {
        int number;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out number) || (number <= 0))
        {
            Console.WriteLine("Invalid input. Please enter a valid positive integer: ");
        }

        for (int i = 1; i <= number; i++)
        {
            Console.Write(i + " ");

        }

        
    }
}