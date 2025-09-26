public struct Point3D
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    private static readonly Point3D coordinateSystemStartPoint = new Point3D(0, 0, 0);

    public static Point3D CoordinateSystemStartPoint
    {
        get { return coordinateSystemStartPoint; }
    }

    public Point3D(double x, double y, double z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public override string ToString()
    {
        return $"Point coordinates: \nX: {this.X}\nY: {this.Y}\nZ: {this.Z}";
    }
}