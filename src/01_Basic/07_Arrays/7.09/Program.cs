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
            while (!int.TryParse(Console.ReadLine(), out array[i]) || (array[i] < 0) || (array[i] > 10000))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer in range [0, 10000]: ");
            }
        }

        int counter = 1;
        int maxCounter = 1;
        int mostFrequentNumber = array[0];
    
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if ((array[i] == array[j]) && (i != j))
                {
                    counter++;
                }
            }
                        
            if (counter > maxCounter)
            {
                maxCounter = counter;
                mostFrequentNumber = array[i];
            }

            counter = 1;                     
        }

        Console.WriteLine($"{mostFrequentNumber} ({maxCounter} times)");
    }
}
