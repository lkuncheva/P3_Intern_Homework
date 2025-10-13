namespace Abstraction;

public abstract class Shape : IShape
{
    public abstract double CalculatePerimeter();
    public abstract double CalculateArea();

    protected double ValidateDimension(double value, string dimensionName)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(dimensionName,
                $"{dimensionName} must be a positive value. Received: {value}"
            );
        }

        return value;
    }
}