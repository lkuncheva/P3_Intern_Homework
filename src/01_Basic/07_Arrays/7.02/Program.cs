using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
class Program
{
    static void Main(string[] args)
    {
        int n;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out n) || (n < 1) || (n > 20))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer in range [1, 20]: ");
        }

        int[] array1 = new int[n];
        int[] array2 = new int[n];

        Console.WriteLine("First Array: ");
        for (int i = 0; i < n; i++)
        {
            while (!int.TryParse(Console.ReadLine(), out array1[i]))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer: ");
            }
        }

        Console.WriteLine("Second Array: ");
        for (int i = 0; i < n; i++)
        {
            while (!int.TryParse(Console.ReadLine(), out array2[i]))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer: ");
            }
        }

        for (int i = 0; i < n; i++)
        {
            if (array1[i] != array2[i])
            {
                Console.WriteLine("Not Equal");
                return;
            }
         }

        Console.WriteLine("Equal");

    }
}