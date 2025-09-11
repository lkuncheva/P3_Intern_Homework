using System;

class Program
{
    static void Main(string[] args)
    {
        int a, b, c, d, e;

        Console.WriteLine("Enter an integer a: ");
        while (!int.TryParse(Console.ReadLine(), out a) || (a < -1000 || a > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer number a: ");
        }

        Console.WriteLine("Enter an integer b: ");
        while (!int.TryParse(Console.ReadLine(), out b) || (b < -1000 || b > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer number b: ");
        }

        Console.WriteLine("Enter an integer c: ");
        while (!int.TryParse(Console.ReadLine(), out c) || (c < -1000 || c > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer number c: ");
        }

        Console.WriteLine("Enter an integer d: ");
        while (!int.TryParse(Console.ReadLine(), out d) || (d < -1000 || d > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer number d: ");
        }

        Console.WriteLine("Enter an integer e: ");
        while (!int.TryParse(Console.ReadLine(), out e) || (e < -1000 || e > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer number e: ");
        }

        Console.WriteLine($"The sum of the numbers is: {a + b + c + d + e}");


    }
}