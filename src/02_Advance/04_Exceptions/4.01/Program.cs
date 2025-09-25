public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter a number:");

        try
        {
            double number = double.Parse(Console.ReadLine());

            if (number < 0)
            {
                throw new ArgumentOutOfRangeException("The number cannot be negative.");
            }

            double sqrt = Math.Sqrt(number);

            Console.WriteLine($"{sqrt:F3}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid number.");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Invalid number.");
        }
        finally
        {
            Console.WriteLine("Good bye");
        }
    }
}