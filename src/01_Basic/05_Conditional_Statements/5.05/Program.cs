using System;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main(string[] args)
    {
        double a, b, c;
        
        Console.WriteLine("Enter a number: ");
        while (!double.TryParse(Console.ReadLine(), out a) || (a < -200 || a > 200))
        {
            Console.WriteLine("Invalid input. Please enter a valid number in range [-200, 200]: ");
        }

        Console.WriteLine("Enter a number: ");
        while (!double.TryParse(Console.ReadLine(), out b) || (b < -200 || b > 200))
        {
            Console.WriteLine("Invalid input. Please enter a valid number in range [-200, 200]: ");
        }

        Console.WriteLine("Enter a number: ");
        while (!double.TryParse(Console.ReadLine(), out c) || (c < -200 || c > 200))
        {
            Console.WriteLine("Invalid input. Please enter a valid number in range [-200, 200]: ");
        }

        double biggest = a;

        if (biggest < b)
            biggest = b;        
        if(biggest < c)
            biggest = c;

        Console.WriteLine(biggest);


    }
}