using System;
using System.Linq;

class FindLargestNumber
{
    static void Main(string[] args)
    {
        int n;
        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out n) || (n < 1))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer >= 1: ");
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

        int k;
        Console.WriteLine("Enter number K: ");
        while (!int.TryParse(Console.ReadLine(), out k))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer: ");
        }

        Array.Sort(array);

        int index = Array.BinarySearch(array, k);

        if (index >= 0)
        {
            Console.WriteLine($"The largest number <= {k} is: {array[index]}");
        }
        else
        {
            int insertionPoint = ~index;
            if (insertionPoint > 0)
            {
                Console.WriteLine($"The largest number <= {k} is: {array[insertionPoint - 1]}");
            }
            else
            {
                Console.WriteLine($"There is no number in the array that is <= {k}.");
            }
        }
    }
}