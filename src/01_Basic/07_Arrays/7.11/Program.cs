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

        Console.WriteLine("Enter sorted array: ");
        for (int i = 0; i < n; i++)
        {
            while (!int.TryParse(Console.ReadLine(), out array[i]))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer: ");
            }
        }

        int x;

        Console.WriteLine("Enter element X: ");
        while (!int.TryParse(Console.ReadLine(), out x))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer: ");
        }

        int low = 0;
        int high = n - 1;
        int mid;

        for (int i = 0; i < n ;i++)
        {
            mid = low + ((high - low) / 2);

            if (array[mid] == x)
            {
                Console.WriteLine(mid);
                return;
            }
            else if (array[mid] > x)
            {
                high = mid;
            }
            else
            {
                low = mid;
            }
        }

        Console.WriteLine("not in array");
    }
}
