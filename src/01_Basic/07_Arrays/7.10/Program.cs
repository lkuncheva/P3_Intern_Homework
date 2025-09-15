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

        int s;

        Console.WriteLine("Enter a sum S: ");
        while (!int.TryParse(Console.ReadLine(), out s))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer: ");
        }

        int[] array = new int[n];

        Console.WriteLine("Enter array: ");
        for (int i = 0; i < n; i++)
        {
            while (!int.TryParse(Console.ReadLine(), out array[i]) || (array[i] < 0) || (array[i] > 10000))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer in range [0, 10000]: ");
            }
        }

        bool found = false;

        for (int i = 0; i < n; i++)
        {
            int currentSum = 0;

            for (int j = i; j < n; j++)
            {
                currentSum += array[j];

                if (currentSum == s)
                {
                    found = true;

                    for (int k = i; k <= j; k++)
                    {
                        Console.WriteLine(array[k]);
                    }

                    return;
                }

                if (currentSum > s)
                {
                    break;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine("No Subset Adds To Sum");
        }
    }
}
