using System;
class Program
{
    static void Main(string[] args)
    {
        int n, k;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out n) || (n < 1) || (n > 1024))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer in range [1, 1024]: ");
        }

        Console.WriteLine("Enter number K: ");
        while (!int.TryParse(Console.ReadLine(), out k) || (k < 1) || (k > n))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer in range [1, N]: ");
        }


        // Easier implementation using a list instead of an array
        /*
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter array elements on separate lines:");
        for (int i = 0; i < n; i++)
        {
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer:");
            }
            numbers.Add(number);
        }

        int sum = 0;
        int maxNumber;

        for (int i = 0; i < k; i++)
        {
            maxNumber = numbers.Max();

            sum += maxNumber;

            numbers.Remove(maxNumber);
        }

        Console.WriteLine(sum);
        */


        int[] array = new int[n];

        Console.WriteLine("Enter array: ");
        for (int i = 0; i < n; i++)
        {
            while (!int.TryParse(Console.ReadLine(), out array[i]))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer: ");
            }

        }


        // Implementation using built-in sorting method
        /*
        int[] sortedArray = array.OrderByDescending(x => x).ToArray();

        int sum = 0;

        // Sum the first K elements, which are now the largest.
        for (int i = 0; i < k; i++)
        {
            sum += sortedArray[i];
        }

        Console.WriteLine(sum);
        */

        // Implementation without using any helper methods
        
        int sum = 0;
        int[] maxNumber = new int[k];

        for (int i = 0; i < k; i++)
        {
            maxNumber[i] = array[i];
        }

        // Puts the K largest numbers in an array to get their sum
        for (int i = 0; i < k; i++)
        {
            for (int j = 0; j < array.Length; j++)
            {
                if ((array[j] > maxNumber[i]) && (i == 0))
                    maxNumber[i] = array[j];
                else if ((array[j] > maxNumber[i]) && (array[j] < maxNumber[i - 1]))
                    maxNumber[i] = array[j];  
            }
            sum += maxNumber[i];
        }

        Console.WriteLine(sum);
        
    }
}