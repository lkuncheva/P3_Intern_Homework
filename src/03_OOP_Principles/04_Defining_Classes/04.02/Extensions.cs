namespace _04._02;

public static class Extensions
{
    public static double CalculateDistance(Point3D p1, Point3D p2)
    {
        double deltaX = p2.X - p1.X;
        double deltaY = p2.Y - p1.Y;
        double deltaZ = p2.Z - p1.Z;

        double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2) + Math.Pow(deltaZ, 2));

        return distance;
    }

    public static void SavePath(Path path, string filePath)
    {
        try
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var point in path.Points3DList)
                {
                    writer.WriteLine($"{point.X},{point.Y},{point.Z}");
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The file '{filePath}' was not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    public static Path LoadPath(string filePath)
    {
        Path path = new Path();

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] coordinates = line.Split(',');

                    if (coordinates.Length == 3)
                    {
                        if (double.TryParse(coordinates[0], out double x) &&
                            double.TryParse(coordinates[1], out double y) &&
                            double.TryParse(coordinates[2], out double z))
                        {
                            path.AddPoint(new Point3D(x, y, z));
                        }
                    }
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The file '{filePath}' was not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

        return path;
    }
}