using System;

class Program
{
    static void Main(string[] args)
    {
        long P;
        uint N;

        Console.WriteLine("Enter a number P: ");
        while (!long.TryParse(Console.ReadLine(), out P) || (P < 0 || P > Math.Pow(2 , 55)))
        {
            Console.WriteLine("Invalid input. Please enter a valid positive integer P: ");
        }

        Console.WriteLine("Enter a number N: ");
        while (!uint.TryParse(Console.ReadLine(), out N) || (N < 0 || N >= 55))
        {
            Console.WriteLine("Invalid input. Please enter a valid positive integer N: ");
        }

        Console.WriteLine((P >> (int)P) & 1);
    }
}
