using System;

class Program
{
    static void Main(string[] args)
    {
        double A, B;

        Console.WriteLine("Enter number A: ");
        while (!double.TryParse(Console.ReadLine(), out A))
        {
            Console.WriteLine("Invalid input. Please enter a valid number: ");
        }

        Console.WriteLine("Enter number B: ");
        while (!double.TryParse(Console.ReadLine(), out B))
        {
            Console.WriteLine("Invalid input. Please enter a valid number: ");
        }

        Console.WriteLine("The larger number is " + ((A > B)? A : B ) );
    }
}