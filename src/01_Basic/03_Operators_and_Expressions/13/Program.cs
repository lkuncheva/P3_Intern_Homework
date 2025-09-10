using System;

class Program
{
    static void Main(string[] args)
    {
        int N;
        int P;
        byte v;

        Console.WriteLine("Enter an integer N: ");
        while (!int.TryParse(Console.ReadLine(), out N))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer: ");
        }

        Console.WriteLine("Enter a positive integer P in range [0, 64): ");
        while (!int.TryParse(Console.ReadLine(), out P) || (P < 0 || P >= 64))
        {
            Console.WriteLine("Invalid input. Please enter a valid positive integer P in range [0, 64): ");
        }

        Console.WriteLine("Enter a bit value v (0 or 1): ");
        while (!byte.TryParse(Console.ReadLine(), out v) || (v != 0 && v != 1))
        {
            Console.WriteLine("Invalid input. Please enter a valid bit value v (0 or 1): ");
        }

        int mask = 1 << P;

        N = N & ~mask;

        if (v == 1)
            N = N | mask;
        
        Console.WriteLine(N);
    }
}
