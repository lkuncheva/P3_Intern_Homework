namespace Shapes;

public class Triangle : Shape
{
    public Triangle(double width, double height) : base(width, height) { }

    public override double CalculateSurface()
    {
        return Height * Width / 2.0;
    }
}