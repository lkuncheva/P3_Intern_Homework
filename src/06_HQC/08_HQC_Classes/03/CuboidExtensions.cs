using CohesionAndCoupling;

public static class CuboidExtensions
{
    public static double CalculateVolume(this Cuboid cuboid)
    {
        return cuboid.Width * cuboid.Height * cuboid.Depth;
    }

    public static double CalculateSpaceDiagonal(this Cuboid cuboid)
    {
        return DistanceCalculator.CalculateDistance3DTo(0, 0, 0, cuboid.Width, cuboid.Height, cuboid.Depth);
    }

    public static double CalculateFaceDiagonalXY(this Cuboid cuboid)
    {
        return DistanceCalculator.CalculateDistance2DTo(0, 0, cuboid.Width, cuboid.Height);
    }

    public static double CalculateFaceDiagonalXZ(this Cuboid cuboid)
    {
        return DistanceCalculator.CalculateDistance2DTo(0, 0, cuboid.Width, cuboid.Depth);
    }

    public static double CalculateFaceDiagonalYZ(this Cuboid cuboid)
    {
        return DistanceCalculator.CalculateDistance2DTo(0, 0, cuboid.Height, cuboid.Depth);
    }
}