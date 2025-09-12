using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("Enter hex number: ");

        string hexForm;


        while (true)
        {
            hexForm = Console.ReadLine();

            if (string.IsNullOrEmpty(hexForm) || hexForm.Any(c => !((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F'))))
            {
                Console.WriteLine("Invalid input. Please enter a valid hex number.");
            }
            else
            {
                break;
            }
        }

        long decimalForm = 0;
                
        for (int i = hexForm.Length - 1, j = 0; i >= 0; i--, j++)
        {
            if (hexForm[i] >= '0' && hexForm[i] <= '9')
                decimalForm += (hexForm[i] - '0') * (long)Math.Pow(16, j);            
            else            
                decimalForm += (hexForm[i] - 'A' + 10) * (long)Math.Pow(16, j);
            
        }

        Console.WriteLine(decimalForm);
    }
}