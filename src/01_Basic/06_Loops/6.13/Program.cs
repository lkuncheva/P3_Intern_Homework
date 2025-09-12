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

        string hexForm = "";

        while (decimalForm > 0)
        {
            if (decimalForm % 16 <= 9)
                hexForm = (decimalForm % 16) + hexForm;
            else
                hexForm = (char)('A' + (decimalForm % 16 - 10)) + hexForm;

            decimalForm /= 16;
        }

        Console.WriteLine(hexForm);
    }
}