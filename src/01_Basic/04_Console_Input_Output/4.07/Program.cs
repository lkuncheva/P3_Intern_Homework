using System;

class Program
{
    static void Main(string[] args)
    {
        int number;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer in range [1, 1000): ");
        }

        for (int i = 1; i <= number; i++)
        {
            Console.WriteLine(i);
        }
    }
}