class Program
{
    static void Main(string[] args)
    {
        string[] array = { "aaa", "bb", "cccccc", "dddd", "e", "ffffffffff", "gggg" };

        SortByLength(array);

        Console.WriteLine("Sorted array of strings by the length of its elements::");
        foreach (string s in array)
        {
            Console.WriteLine(s);
        }
    }

    static void SortByLength(string[] array)
    {
        int n = array.Length;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j].Length > array[j + 1].Length)
                {
                    string temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }
}