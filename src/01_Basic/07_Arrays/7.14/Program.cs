class Program
{
    static void Sort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(array, left, right);

            Sort(array, left, pivot - 1);
            Sort(array, pivot + 1, right);
        }
    }

    static int Partition(int[] array, int left, int right)
    {
        int pivot = array[right];
        int i = (left - 1); 

        for (int j = left; j < right; j++)
        {
            if (array[j] <= pivot)
            {
                i++;

                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        int temp1 = array[i + 1];
        array[i + 1] = array[right];
        array[right] = temp1;

        return i + 1;
    }

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

        Sort(array, 0, n - 1);

        Console.WriteLine("Sorted array: ");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(array[i]);
        }
    }
}