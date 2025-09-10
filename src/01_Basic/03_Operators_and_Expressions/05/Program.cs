using System;

class Program
{
    static void Main(string[] args)
    {
        int N;

        Console.WriteLine("Enter integer: ");

        while (!int.TryParse(Console.ReadLine(), out N) || N <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid non-negative integer: ");
        }

        if (N >= 100)
        {
            int thirdDigit = (N / 100) % 10;
            Console.WriteLine(thirdDigit == 7 ? $"True" : $"False {thirdDigit}");
        }
        else
            Console.WriteLine("False 0");

    }
}