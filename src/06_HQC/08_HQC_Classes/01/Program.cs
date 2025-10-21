namespace Abstraction;

public class Program
{
    public static void Main()
    {
        try
        {
            Circle circle = new Circle(5);
            Console.WriteLine($"Circle R={circle.Radius:f2}: " +
                              $"Perimeter = {circle.CalculatePerimeter():f2}, " +
                              $"Area = {circle.CalculateArea():f2}");

            Rectangle rect = new Rectangle(2.5, 3.5);
            Console.WriteLine($"Rectangle W={rect.Width:f2}, H={rect.Height:f2}: " +
                              $"Perimeter = {rect.CalculatePerimeter():f2}, " +
                              $"Area = {rect.CalculateArea():f2}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR: Out of Range] {ex.Message}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"An unexpected fatal error occurred: {ex.Message}");
            Console.ResetColor();

            throw;
        }
    }
}