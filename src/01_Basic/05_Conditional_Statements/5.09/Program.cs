using System;
using System.Numerics;

class Program
{
    static void Main(string[] args)
    {        
        Console.WriteLine("Enter variable type: ");
        string input = Console.ReadLine().ToLower();

        switch (input)
        {
            case "integer":
                int integer;

                Console.WriteLine("Enter an integer number: ");
                while (!int.TryParse(Console.ReadLine(), out integer) || (integer < -1000) || (integer > 1000))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer in the range [-1000, 1000]");
                }

                Console.WriteLine(integer + 1);
                break;

            case "real":
                double real;

                Console.WriteLine("Enter a real number: ");
                while (!double.TryParse(Console.ReadLine(), out real) || (real < -1000) || (real > 1000))
                {
                    Console.WriteLine("Invalid input. Please enter a valid real number in the range [-1000, 1000]");
                }

                Console.WriteLine($"{(real + 1):F2}");
                break;

            case "text":
                Console.WriteLine("Enter text: ");
                string text = Console.ReadLine();

                Console.WriteLine(text + "*");
                break;

            default:
                Console.WriteLine("Invalid type");
                break;
        }     
    }
}