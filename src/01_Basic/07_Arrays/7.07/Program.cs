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

        for (int i = 0; i < n - 1; i++)
        {
            int minNumberIndex = i;

            // Finds the index of the smallest numbr in the unsorted part
            for (int j = i + 1; j < n; j++)
            {
                if (array[j] < array[minNumberIndex])
                {
                    minNumberIndex = j;
                }
            }

            // Swap the smallest number with the first element of the unsorted part.
            if (minNumberIndex != i)
            {
                int temp = array[i];
                array[i] = array[minNumberIndex];
                array[minNumberIndex] = temp;
            }
        }

        Console.WriteLine("Sorted array: ");
        foreach (int number in array)
        {
            Console.WriteLine(number);
        }
    }
}