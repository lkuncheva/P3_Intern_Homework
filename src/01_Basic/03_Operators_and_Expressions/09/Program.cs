using System;

class Program
{
    static void Main(string[] args)
    {
        double sideA, sideB, height;

        Console.WriteLine("Enter trapezoid's first side: ");

        while (!double.TryParse(Console.ReadLine(), out sideA) || (sideA <= 0 || sideA > 500))
        {
            Console.WriteLine("Invalid input. Please enter a valid positive number for the trapezoid's first side.");
        }

        Console.WriteLine("Enter trapezoid's second side: ");
        while (!double.TryParse(Console.ReadLine(), out sideB) || (sideB <= 0 || sideB > 500))
        {
            Console.WriteLine("Invalid input. Please enter a valid positive number for the trapezoid's second side.");
        }

        Console.WriteLine("Enter trapezoid's height: ");
        while (!double.TryParse(Console.ReadLine(), out height) || (height <= 0 || height > 500))
        {
            Console.WriteLine("Invalid input. Please enter a valid positive number for the trapezoid's height.");
        }

        double area = 0.5 * (sideA + sideB) * height;
        Console.WriteLine($"Trapezoid's area = {area:F7}");
    }
}