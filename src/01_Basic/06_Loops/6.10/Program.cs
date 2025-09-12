using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {
        int n;
        int productEven = 1;
        int productOdd = 1;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out n) || (n < 4) || (n > 50))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer in range [4, 50]: ");
        }

        int[] numbers = new int[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter number: ");
            while (!int.TryParse(Console.ReadLine(), out numbers[i]))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer: ");
            }

        }

      
        for (int i = 0; i < n; i++)
        {
            if ((i + 1) % 2 == 0)
                productEven *= numbers[i];
            else
                productOdd *= numbers[i];
        }

        Console.WriteLine(productEven == productOdd ? $"yes {productEven}" : $"no {productOdd} {productEven}");
    }
}