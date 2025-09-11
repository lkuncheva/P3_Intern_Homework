using System;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main(string[] args)
    {
        double a, b, c;

        Console.WriteLine("Enter a number: ");
        while (!double.TryParse(Console.ReadLine(), out a) || (a < -1000 || a > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid number in range [-1000, 1000]: ");
        }

        Console.WriteLine("Enter a number: ");
        while (!double.TryParse(Console.ReadLine(), out b) || (b < -1000 || b > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid number in range [-1000, 1000]: ");
        }

        Console.WriteLine("Enter a number: ");
        while (!double.TryParse(Console.ReadLine(), out c) || (c < -1000 || c > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid number in range [-1000, 1000]: ");
        }

        if (a > b && a > c)
        {
            Console.Write(a + " ");
            if(b > c)
                Console.WriteLine(b + " " + c);
            else
                Console.WriteLine(c + " " + c);
        }
        else if (b > a && b > c)
        {
            Console.Write(b + " ");
            if (a > c)
                Console.WriteLine(a + " " + c);
            else
                Console.WriteLine(c + " " + a);
        }
        else
        {
            if(a > b)
                Console.WriteLine(c + " " + a + " " + b);
            else
                Console.WriteLine(c + " " + b + " " + a);
        }


    }
}