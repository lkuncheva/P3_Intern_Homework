using System;

class Program
{
    static void Main(string[] args)
    {
        double width, height;

        Console.WriteLine("Enter rectangle's width: ");

        while (!double.TryParse(Console.ReadLine(), out width) || width <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid positive number for the width of the rectangle.");
        }

        Console.WriteLine("Enter rectangle's height: ");
        while (!double.TryParse(Console.ReadLine(), out height) || height <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid positive number for the height of the rectange.");
        }

        double area = width * height;
        double perimeter = 2 * (width + height);
        Console.WriteLine($"Rectangle's area = {area:F2}; Rectangle's perimeter = {perimeter:F2}");
    }
}