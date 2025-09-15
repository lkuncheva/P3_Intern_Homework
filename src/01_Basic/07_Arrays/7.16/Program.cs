using System;
class Program
{
    static void Main(string[] args)
    {
        int n;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out n) || (n < 1) || (n > 1024))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer in range [1, 1024]: ");
        }

        int[] array = new int[n];

        Console.WriteLine("Enter array: ");
        for (int i = 0; i < n; i++)
        {
            while (!int.TryParse(Console.ReadLine(), out array[i]))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer: ");
            }
        }

        int[] lisLength = new int[n];
        for (int i = 0; i < n; i++)
        {
            lisLength[i] = 1;
        }

        int maxLisLength = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (array[i] >= array[j] && lisLength[i] <= lisLength[j])
                {
                    lisLength[i] = lisLength[j] + 1;
                }
            }

            if (lisLength[i] > maxLisLength)
            {
                maxLisLength = lisLength[i];
            }
        }

        int elementsToRemove = n - maxLisLength;

        Console.WriteLine(elementsToRemove);
    }
}