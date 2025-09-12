using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("Enter decimal number: ");
        long decimalForm;

        while (!long.TryParse(Console.ReadLine(), out decimalForm))
        {
            Console.WriteLine("Invalid input. Please enter a valid number: ");
        }

        if (decimalForm == 0)
        {
            Console.WriteLine("0");
            return;
        }

        string binaryInteger = "";

        while (decimalForm > 0)
        {
            binaryInteger = (decimalForm % 2) + binaryInteger;
            decimalForm /= 2;
        }

        Console.WriteLine(binaryInteger);
    }
}