namespace Shapes
{
    public abstract class Shape
    {
        public double Width { get; private set; }

        public double Height { get; private set; }

        public Shape(double width, double height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Dimensions must be positive.");
            }

            Width = width;
            Height = height;
        }

        public abstract double CalculateSurface();
    }
}