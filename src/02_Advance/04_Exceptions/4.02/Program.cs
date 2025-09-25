class Program
{
    static int ReadNumber(int start, int end)
    {
        Console.Write($"Enter number in range ({start}, {end}): ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int number))
        {
            throw new FormatException("Input needs to be an integer!");
        }

        if (number <= start || number >= end)
        {
            throw new ArgumentOutOfRangeException("Number not in valid range!");
        }

        return number;
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Enter 10 numbers:");
        try
        {
            int[] numbers = new int[10];
            int start = 1;
            int end = 100;

            for (int i = 0; i < 10; i++)
            {
                numbers[i] = ReadNumber(start, end);
                start = numbers[i];
            }

            Console.Write("1 ");

            foreach (int number in numbers)
            {
                Console.Write($"< {number} ");
            }

            Console.Write("< 100");
        }
        catch (FormatException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}