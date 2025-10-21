namespace CohesionAndCoupling;

public static class DistanceCalculator
{
    private static double CalculateDistance2D(double x1, double y1, double x2, double y2)
    {
        double deltaX = x2 - x1;
        double deltaY = y2 - y1;
        return Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
    }

    public static double CalculateDistance2DTo(double x1, double y1, double x2, double y2)
    {
        return CalculateDistance2D(x1, y1, x2, y2);
    }

    private static double CalculateDistance3D(double x1, double y1, double z1, double x2, double y2, double z2)
    {
        double deltaX = x2 - x1;
        double deltaY = y2 - y1;
        double deltaZ = z2 - z1;
        return Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY) + (deltaZ * deltaZ));
    }

    public static double CalculateDistance3DTo(double x1, double y1, double z1, double x2, double y2, double z2)
    {
        return CalculateDistance3D(x1, y1, z1, x2, y2, z2);
    }
}