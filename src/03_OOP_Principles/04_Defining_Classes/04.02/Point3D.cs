struct Point3D
{
    private double x;
    private double y;
    private double z;

    private static readonly Point3D coordinateSystemStartPoint = new Point3D(0, 0, 0);

    static Point3D CoordinateSystemStartPoint
    {
        get { return coordinateSystemStartPoint; }
    }

    public Point3D(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public double X
    {
        get { return this.x; }
        set { this.x = value; }
    }

    public double Y
    {
        get { return this.y; }
        set { this.y = value; }
    }

    public double Z
    {
        get { return this.y; }
        set { this.y = value; }
    }

    public override string ToString()
    {
        return $"Point coordinates: \nX: {X}\nY: {Y}\nZ: {z}";
    }
}

internal record struct Point3D(int Item1, int Item2, int Item3)
{
    public static implicit operator (int, int, int)(Point3D value)
    {
        return (value.Item1, value.Item2, value.Item3);
    }

    public static implicit operator Point3D((int, int, int) value)
    {
        return new Point3D(value.Item1, value.Item2, value.Item3);
    }
}