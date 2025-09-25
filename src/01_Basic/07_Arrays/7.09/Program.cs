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

        Dictionary<int, int> frequencyMap = new Dictionary<int, int>();

        foreach (int number in array)
        {
            if (frequencyMap.ContainsKey(number))
            {
                frequencyMap[number]++;
            }
            else
            {
                frequencyMap[number] = 1;
            }
        }

        int mostFrequentNumber = frequencyMap.Keys.First();
        int maxCount = frequencyMap[mostFrequentNumber];

        foreach (var pair in frequencyMap)
        {
            if (pair.Value > maxCount)
            {
                maxCount = pair.Value;
                mostFrequentNumber = pair.Key;
            }
        }

        Console.WriteLine($"{mostFrequentNumber} ({maxCount} times)");
    }
}