namespace CohesionAndCoupling;

public class Cuboid
{
    public double Width { get; }
    public double Height { get; }
    public double Depth { get; }

    public Cuboid(double width, double height, double depth)
    {
        if (width <= 0 || height <= 0 || depth <= 0)
        {
            throw new ArgumentException("All cuboid dimensions must be positive.");
        }

        Width = width;
        Height = height;
        Depth = depth;
    }
}