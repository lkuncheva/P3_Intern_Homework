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
                Console.WriteLine("Invalid number.");
            }
            else
            {
                double sqrt = Math.Sqrt(number);
                Console.WriteLine($"Result: {sqrt:F3}");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input format.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Good bye");
        }
    }
}