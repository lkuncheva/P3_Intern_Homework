using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("Enter binary number: ");
        string binaryInteger;

        
        while (true)
        {
            binaryInteger = Console.ReadLine();

            if (string.IsNullOrEmpty(binaryInteger) || binaryInteger.Any(c => c != '0' && c != '1'))
            {
                Console.WriteLine("Invalid input. Please enter a valid binary number containing only 0s and 1s.");
            }
            else
            {
                break;
            }
        }

        long decimalForm = 0;


        for (int i = (binaryInteger.Length - 1), j = 0; i >= 0; i--, j++)
        {
            if (binaryInteger[i] == '1')
            {
                decimalForm += (long)Math.Pow(2, j);
            }
        }


        Console.WriteLine(decimalForm);

    }
}