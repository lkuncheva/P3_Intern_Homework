using System;

class Program
{
    static void Main(string[] args)
    {
        int N;
        bool isPrime = true;

        Console.WriteLine("Enter integer: ");

        while (!int.TryParse(Console.ReadLine(), out N) || N > 100)
        {
            Console.WriteLine("Invalid input. Please enter a valid integer <= 100: ");
        }

        if (N == 2)
            isPrime = true;
        else if (N > 1)
        {
            for (int i = 2; i < N; i++)
            {
                if (N % i == 0)
                {
                    isPrime = false;
                    break;
                }
            }
        }
        else
            isPrime = false;

        Console.WriteLine(isPrime);

    }
}