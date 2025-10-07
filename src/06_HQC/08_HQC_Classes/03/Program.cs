namespace CohesionAndCoupling;
public class Program
{
    public static void Main()
    {
        try
        {
            Console.WriteLine("--- File Operations ---");

            Console.WriteLine($"Extension of 'example': {FileExtensions.GetFileExtension("example")}");
            Console.WriteLine($"Extension of 'example.pdf': {FileExtensions.GetFileExtension("example.pdf")}");
            Console.WriteLine($"Name without extension of 'example.new.pdf': {FileExtensions.GetFileNameWithoutExtension("example.new.pdf")}");

            Console.WriteLine("\n--- Distance Calculations ---");

            Console.WriteLine("Distance in the 2D space = {0:f2}",
                DistanceCalculator.CalculateDistance2DTo(1, -2, 3, 4));
            Console.WriteLine("Distance in the 3D space = {0:f2}",
                DistanceCalculator.CalculateDistance3DTo(5, 2, -1, 3, -6, 4));

            Console.WriteLine("\n--- Cuboid Operations ---");

            Cuboid box = new Cuboid(width: 3, height: 4, depth: 5);

            Console.WriteLine($"Cuboid Dimensions: W={box.Width}, H={box.Height}, D={box.Depth}");
            Console.WriteLine("Volume = {0:f2}", box.CalculateVolume());

            Console.WriteLine("Diagonal Space (XYZ) = {0:f2}", box.CalculateSpaceDiagonal());
            Console.WriteLine("Diagonal Face (XY) = {0:f2}", box.CalculateFaceDiagonalXY());
            Console.WriteLine("Diagonal Face (XZ) = {0:f2}", box.CalculateFaceDiagonalXZ());
            Console.WriteLine("Diagonal Face (YZ) = {0:f2}", box.CalculateFaceDiagonalYZ());
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR: Invalid Argumnts] {ex.Message}");
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