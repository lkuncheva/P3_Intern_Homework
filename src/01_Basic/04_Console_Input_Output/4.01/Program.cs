using System;

class Program
{
    static void Main(string[] args)
    {
        double a;
        double b;
        double c;

        Console.WriteLine("Enter a real number a: ");
        while (!double.TryParse(Console.ReadLine(), out a) || (a < -1000 || a > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid real number a: ");
        }

        Console.WriteLine("Enter a real number b: ");
        while (!double.TryParse(Console.ReadLine(), out b) || (b < -1000 || b > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid real number b: ");
        }

        Console.WriteLine("Enter a real number c: ");
        while (!double.TryParse(Console.ReadLine(), out c) || (c < -1000 || c > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid real number c: ");
        }

        Console.WriteLine($"Sum: {a + b + c}");
    }
}