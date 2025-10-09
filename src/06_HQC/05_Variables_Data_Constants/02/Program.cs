public class StatisticsCalculator
{
    public void PrintStatistics(double[] numbers)
    {
        if (numbers == null || !numbers.Any())
        {
            Console.WriteLine("Cannot calculate statistics for a null or empty array.");
            return;
        }

        double maxElement = numbers[0];
        double minElement = numbers[0];
        double sum = 0;
        double average;

        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] > maxElement)
            {
                maxElement = numbers[i];
            }

            if (numbers[i] < minElement)
            {
                minElement = numbers[i];
            }

            sum += numbers[i];
        }

        average = sum / numbers.Length;

        // Or we can just do this
        /*
        double maxElement = numbers.Max();
        double minElement = numbers.Min();
        double average = numbers.Average();
        */

        Console.WriteLine($"Max: {maxElement}");
        Console.WriteLine($"Min: {minElement}");
        Console.WriteLine($"Average: {average}");
    }
}