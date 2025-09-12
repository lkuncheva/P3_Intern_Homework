using System;
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

        int[] array = new int[n];

        for (int i = 0; i < n; i++)
        {
            array[i] = i * 5;
            Console.WriteLine(array[i]);
        }

        

    }
}