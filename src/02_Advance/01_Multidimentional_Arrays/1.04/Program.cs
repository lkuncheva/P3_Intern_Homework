class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter array elements separated by a space: ");

        string[] input = Console.ReadLine()
                               .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        int n = input.Length;
        int[] array = new int[n];

        for (int i = 0; i < n; i++)
        {
            array[i] = Convert.ToInt32(input[i]);
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