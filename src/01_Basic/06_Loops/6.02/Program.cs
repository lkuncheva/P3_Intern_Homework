using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {
        int number;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out number) || (number <= 1) || (number >= 1500))
        {
            Console.WriteLine("Invalid input. Please enter a valid positive integer in range (1, 1500): ");
        }

        for (int i = 1; i <= number; i++)
        {
            if ((i % 3 == 0) || (i % 7 == 0))
                continue;

            Console.Write(i + " ");
        }
    }
}