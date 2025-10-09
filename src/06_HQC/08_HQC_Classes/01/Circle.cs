namespace Abstraction;

public class Circle : Shape
{
    private double radius;

    public double Radius
    {
        get => this.radius;
        set => this.radius = ValidateDimension(value, nameof(Radius));
    }

    public Circle() : this(1) { }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override double CalculatePerimeter()
    {
        return 2 * Math.PI * Radius;
    }

    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}