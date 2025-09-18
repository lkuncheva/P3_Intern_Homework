using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        double number;

        Console.WriteLine("Enter a number: ");
        while (!double.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("Invalid input. Please enter a valid number: ");
        }

        Console.WriteLine($"{"Decimal:",-20}\n{number,15:F}");
        Console.WriteLine($"{"Hexadecimal:",-20}\n{Convert.ToInt64(number),15:X}");
        Console.WriteLine($"{"Percentage:",-20}\n{number,15:P}");
        Console.WriteLine($"{"Scientific:",-20}\n{number,15:E}");
    }
}
