using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {
        int n;
        int counterRow = 0;
        int counterCol = 1;
        
        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out n) || (n <= 1) || (n >= 20))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer in range [1, 20]: ");
        }

        
        for (int i = 0; i < (n * n); i++)
        {
            Console.Write(counterCol + " ");
            
            if (counterCol == (n + counterRow))
            {
                Console.WriteLine();
                counterRow++;

                counterCol = counterRow;
            }
            
            counterCol++;
        }

        //Implementation with nested loops
        /*
        for (int i = 0; i < n; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                Console.Write($"{j + i} ");
            }
            Console.WriteLine();
        }
        */

    }
}