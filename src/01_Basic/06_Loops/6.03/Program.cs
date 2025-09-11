using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {
        int n;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out n) || (n < 1) || (n > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid positive integer in range [1, 1000]: ");
        }

        double[] numbers = new double[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter number: ");
            while (!double.TryParse(Console.ReadLine(), out numbers[i]) || (numbers[i] < -10000) || (numbers[i] > 10000))
            {
                Console.WriteLine("Invalid input. Please enter a valid number in range [-10000, 10000]: ");
            }
                        
        }

       
        Console.WriteLine($"Min = {numbers.Min():F2}");
        Console.WriteLine($"Max = {numbers.Max():F2}");
        Console.WriteLine($"Sum = {numbers.Sum():F2}");
        Console.WriteLine($"Avg = {numbers.Average():F2}");


        //Below is implementation of the same task without using built-in sorting functionality
        /*
        double sum = 0;
        double min = numbers[0];
        double max = numbers[0];
        double avg;

        for (int i = 0; i < n; i++)
        {

            sum += numbers[i];

            if (numbers[i] < min)
                min = numbers[i];

            if (numbers[i] > max)
                max = numbers[i];

        }

        avg = sum / n;

        Console.WriteLine($"Min = {min:F2}");
        Console.WriteLine($"Max = {max:F2}");
        Console.WriteLine($"Sum = {sum:F2}");
        Console.WriteLine($"Avg = {avg:F2}");
        */
    }
}