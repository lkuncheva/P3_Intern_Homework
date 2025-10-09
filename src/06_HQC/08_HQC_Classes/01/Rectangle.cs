namespace Abstraction;

public class Rectangle : Shape
{
    private double width;
    private double height;

    public double Width
    {
        get => this.width;
        set => this.width = ValidateDimension(value, nameof(Width));
    }

    public double Height
    {
        get => this.height;
        set => this.height = ValidateDimension(value, nameof(Height));
    }

    public Rectangle() : this(1, 1) { }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public override double CalculatePerimeter()
    {
        return 2 * (Width + Height);
    }

    public override double CalculateArea()
    {
        return Width * Height;
    }
}