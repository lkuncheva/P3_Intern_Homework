using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {
        int n;
        double factorial2N = 1d;
        double factorialN = 1d;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out n) || (n < 0) || (n > 100))
        {
            Console.WriteLine("Invalid input. Please enter a valid positive integer in range [0, 100]: ");
        }

        

        for (int i = 1; i <= (2 * n); i++)
        {
            if (i <= n)
                factorialN *= i;
            factorial2N *= i;
        }

        double catalanNumer = factorial2N / (factorialN * (factorialN * (n + 1)));

        Console.WriteLine(Math.Round(catalanNumer));
    }
}