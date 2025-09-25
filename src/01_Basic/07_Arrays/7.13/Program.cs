class Program
{
    //Not using recursion
    static void Sort(int[] array, int n)
    {
        for (int currentSize = 1; currentSize <= n - 1; currentSize *= 2)
        {
            for (int leftStart = 0; leftStart < n - 1; leftStart += 2 * currentSize)
            {
                int middle = Math.Min(leftStart + currentSize - 1, n - 1);
                int rightEnd = Math.Min(leftStart + 2 * currentSize - 1, n - 1);

                Merge(array, leftStart, middle, rightEnd);
            }
        }
    }

    static void Merge(int[] array, int left, int middle, int right)
    {
        int leftArrayLength = middle - left + 1;
        int rightArrayLength = right - middle;

        int[] leftArray = new int[leftArrayLength];
        int[] rightArray = new int[rightArrayLength];

        int i;
        int j;

        for (i = 0; i < leftArrayLength; i++)
        {
            leftArray[i] = array[left + i];
        }

        for (j = 0; j < rightArrayLength; j++)
        {
            rightArray[j] = array[middle + 1 + j];
        }

        i = 0;
        j = 0;

        for (int k = left; k <= right; k++)
        {
            if (i >= leftArrayLength || (j < rightArrayLength && leftArray[i] > rightArray[j]))
            {
                array[k] = rightArray[j];
                j++;
            }
            else
            {
                array[k] = leftArray[i];
                i++;
            }
        }
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

        Sort(array, n);

        Console.WriteLine("Sorted array: ");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(array[i]);
        }
    }

    //Using recursion
    /*
    static void Sort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;
            Sort(array, left, middle);
            Sort(array, middle + 1, right);
            Merge(array, left, middle, right);
        }
    }

    static void Merge(int[] array, int left, int middle, int right)
    {
        int leftArrayLength = middle - left + 1;
        int rightArrayLength = right - middle;

        int[] leftArray = new int[leftArrayLength];
        int[] rightArray = new int[rightArrayLength];

        int i;
        int j;

        for (i = 0; i < leftArrayLength; i++)
        {
            leftArray[i] = array[left + i];
        }

        for (j = 0; j < rightArrayLength; j++)
        {
            rightArray[j] = array[middle + 1 + j];
        }

        i = 0;
        j = 0;

        for (int k = left; k <= right; k++)
        {
            if (i >= leftArrayLength || (j < rightArrayLength && leftArray[i] > rightArray[j]))
            {
                array[k] = rightArray[j];
                j++;
            }
            else
            {
                array[k] = leftArray[i];
                i++;
            }
        }
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

        Sort(array, 0, n-1);

        Console.WriteLine("Sorted array: ");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(array[i]);
        }
    }*/
}