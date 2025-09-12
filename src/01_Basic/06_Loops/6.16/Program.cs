using System;
class Program
{
    static void Main(string[] args)
    {
        int n;
        int zeroCounter = 0;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out n) || (n <= 0))
        {
            Console.WriteLine("Invalid input. Please enter a valid positive integer: ");
        }

        while (n > 0)
        {
            n /= 5;
            zeroCounter += n;
        }

        Console.WriteLine(zeroCounter);

    }
}