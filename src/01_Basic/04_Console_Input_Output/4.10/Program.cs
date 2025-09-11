using System;

class Program
{
    static void Main(string[] args)
    {
        int N, M;
        int counter = 0;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out N) || (N > 2000) || (N < 0))
        {
            Console.WriteLine("Invalid input. Please enter a valid positive integer in range [0, 2000]: ");
        }

        Console.WriteLine("Enter number M larger or equal to N: ");
        while (!int.TryParse(Console.ReadLine(), out M) || (M < N) || (M > 2000) || (M < 0))
        {
            Console.WriteLine("Invalid input. Please enter a valid positive integer in range [N, 2000]: ");
        }


        for (int i = N + 1; i < M; i++)
        {
            if( i % 5 == 0)
                counter++;
                
        }

        Console.WriteLine(counter);
    }
}