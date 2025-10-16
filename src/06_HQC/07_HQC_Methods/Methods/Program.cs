namespace Methods;
public class Program
{
    public static void Main()
    {
        try
        {
            Console.WriteLine("--- Triangle Area ---");
            Console.WriteLine($"Area of (3, 4, 5): {Methods.CalculateTriangleArea(3, 4, 5):F2}");

            Console.WriteLine("\n--- Digit Conversion ---");
            Console.WriteLine($"Digit 5 as word: {Methods.ConvertDigitToWord(5)}");

            Console.WriteLine("\n--- Find Max ---");
            Console.WriteLine($"Max element: {Methods.FindMaxElement(5, -1, 3, 2, 14, 2, 3)}");


            Console.WriteLine("\n--- Number Formatting ---");
            Console.WriteLine($"Formatted 1.3 (f): {Methods.FormatNumber(1.3, "f")}");
            Console.WriteLine($"Formatted 0.75 (%): {Methods.FormatNumber(0.75, "%")}");
            Console.WriteLine($"Formatted 2.30 (r): {Methods.FormatNumber(2.30, "r")}");

            Console.WriteLine("\n--- Distance Calculation ---");
            double xA = 3, yA = -1;
            double xB = 3, yB = 2.5;

            double distance = Methods.CalculateDistance(xA, yA, xB, yB);

            Console.WriteLine($"Distance between ({xA}, {yA}) and ({xB}, {yB}): {distance:F2}");

            Console.WriteLine("\n--- Horizontal / Vertical Check ---");
            bool horizontal = Methods.IsHorizontal(yA, yB);
            bool vertical = Methods.IsVertical(xA, xB);

            Console.WriteLine($"Horizontal? {horizontal}");
            Console.WriteLine($"Vertical? {vertical}");

            Console.WriteLine("\n--- Student Age Comparison ---");
            var student1 = new Student("Peter", "Ivanov", "Sofia", new DateTime(1992, 03, 17));
            var student2 = new Student("Stella", "Markova", "Vidin", new DateTime(1993, 11, 03));

            Console.WriteLine($"{student1.FirstName} older than {student2.FirstName} -> {student1.IsOlderThan(student2)}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR: Out of Range] {ex.Message}");
            Console.ResetColor();
        }
        catch (ArgumentNullException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR: Null Argument] {ex.Message}");
            Console.ResetColor();
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR: Invalid Argument] {ex.Message}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[UNEXPECTED ERROR] A general error occurred: {ex.Message}");
            Console.ResetColor();

            throw;
        }
    }
}