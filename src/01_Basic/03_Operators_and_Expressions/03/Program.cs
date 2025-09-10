using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a number: ");

        if (int.TryParse(Console.ReadLine(), out int num))
        {
            bool isDiv = (num % 7 == 0 && num % 5 == 0);

            Console.WriteLine($"{isDiv} {num}");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
        }

    }
}