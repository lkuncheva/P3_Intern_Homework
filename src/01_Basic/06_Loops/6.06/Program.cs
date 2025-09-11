using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {
        int n, k;
        double factorialN = 1d;
        double factorialK = 1d;


        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out n) || (n < 3) || (n >= 100))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer in range (2, 100): ");
        }

        Console.WriteLine("Enter number K: ");
        while (!int.TryParse(Console.ReadLine(), out k) || (k <= 1) || (k >= n))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer in range (1, N): ");
        }


        for (int i = 1; i <= n; i++)
        {
            factorialN *= i;
            if (i <= k)
                factorialK *= i;
        }

        Console.WriteLine(factorialN / factorialK);
    }
}