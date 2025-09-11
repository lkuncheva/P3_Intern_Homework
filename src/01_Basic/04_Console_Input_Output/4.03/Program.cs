using System;

class Program
{
    static void Main(string[] args)
    {
        double radius;

        Console.WriteLine("Enter a radius: ");
        while (!double.TryParse(Console.ReadLine(), out radius) || radius <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid radius: ");
        }

        double area, circumference;

        area = Math.PI * Math.Pow(radius, 2);
        circumference = Math.PI * 2 * radius;

        Console.WriteLine($"Circle area: {area:F2}; Circle circumference: {circumference:F2}");
    }
}