using System;

class Program
{
    static void Main(string[] args)
    {
        int number;

        Console.WriteLine("Enter a number: ");
        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("Invalid input. Please enter a valid positive integer: ");
        }

        const int bitIndex = 3;

        Console.WriteLine((number >> bitIndex) & 1);
    }
}
