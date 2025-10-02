namespace _04._02;
public static class PathExtensions
{
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

    public static void SavePathTo(this Path path, string filePath)
    {
        SavePath(path, filePath);
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

    public static void LoadPathTo(this string filePath)
    {
        LoadPath(filePath);
    }
}