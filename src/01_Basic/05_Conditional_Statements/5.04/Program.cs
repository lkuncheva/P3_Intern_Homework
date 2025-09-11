using System;

class Program
{
    static void Main(string[] args)
    {
        double a, b, c;

        Console.WriteLine("Enter a number: ");
        while (!double.TryParse(Console.ReadLine(), out a))
        {
            Console.WriteLine("Invalid input. Please enter a valid number: ");
        }

        Console.WriteLine("Enter a number: ");
        while (!double.TryParse(Console.ReadLine(), out b))
        {
            Console.WriteLine("Invalid input. Please enter a valid number: ");
        }

        Console.WriteLine("Enter a number: ");
        while (!double.TryParse(Console.ReadLine(), out c))
        {
            Console.WriteLine("Invalid input. Please enter a valid number: ");
        }

        if (a * b * c < 0)
            Console.WriteLine("-");
        else if (a * b * c > 0)
            Console.WriteLine("+");
        else
            Console.WriteLine("0");


    }
}