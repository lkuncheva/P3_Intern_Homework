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

        int currentSequenceLength = 1;
        int maxSequenceLength = 1;

        for (int i = 1; i < n; i++)
        {
            if (array[i] > array[i - 1])
            {
                currentSequenceLength++;
            }
            else
            {
                if (currentSequenceLength > maxSequenceLength)
                {
                    maxSequenceLength = currentSequenceLength;
                }

                currentSequenceLength = 1;
            }
        }

        Console.WriteLine(maxSequenceLength);
    }
}