namespace Shapes;

public class Program
{
    public static void Main(string[] args)
    {
        List<Shape> shapes = new List<Shape>
        {
            new Rectangle(10.0, 5.0),
            new Triangle(8.0, 6.0),
            new Square(7.0),
            new Rectangle(3.5, 9.2),
            new Triangle(4.0, 10.0)
        };

        Console.WriteLine("--- Shape Surface Area Calculator ---");
        Console.WriteLine("-------------------------------------");

        foreach (Shape shape in shapes)
        {
            double surface = shape.CalculateSurface();
            Console.WriteLine($"Shape Type: {shape.GetType().Name.PadRight(10)} | Dimensions: W = {shape.Width:F2}, H = {shape.Height:F2} | Surface Area: {surface:F2}");
        }

        Console.WriteLine("-------------------------------------");
    }
}