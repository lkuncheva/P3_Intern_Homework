public class Size
{
    public double Width { get; set; }
    public double Height { get; set; }
    public Size(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public static Size GetRotatedSize(Size originalSize, double rotationAngle)
    {
        double absCos = Math.Abs(Math.Cos(rotationAngle));
        double absSin = Math.Abs(Math.Sin(rotationAngle));

        double newWidth = (absCos * originalSize.Width) + (absSin * originalSize.Height);
        double newHeight = (absSin * originalSize.Width) + (absCos * originalSize.Height);

        return new Size(newWidth, newHeight);
    }
}