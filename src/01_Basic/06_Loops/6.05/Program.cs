using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {
        int n;
        double x;
        double sum = 1d;
        double factorial = 1d;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out n) || (n < 2) || (n > 10))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer in range [2, 10]: ");
        }

        Console.WriteLine("Enter number x: ");
        while (!double.TryParse(Console.ReadLine(), out x) || (x < 0.5) || (x > 100))
        {
            Console.WriteLine("Invalid input. Please enter a valid number in range [0.5, 100]: ");
        }


        for (int i = 1; i <= n; i++)
        {
            factorial *= i;
            sum += factorial / Math.Pow(x, i);
        }

        Console.WriteLine($"{sum:F5}");
    }
}