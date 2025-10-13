using CohesionAndCoupling;

public static class CuboidExtensions
{
    private static double Volume(Cuboid cuboid)
    {
        return cuboid.Width * cuboid.Height * cuboid.Depth;
    }

    public static double CalculateVolume(this Cuboid cuboid)
    {
        return Volume(cuboid);
    }

    private static double SpaceDiagonal(Cuboid cuboid)
    {
        return DistanceCalculator.CalculateDistance3DTo(0, 0, 0, cuboid.Width, cuboid.Height, cuboid.Depth);
    }

    public static double CalculateSpaceDiagonal(this Cuboid cuboid)
    {
        return SpaceDiagonal(cuboid);
    }

    private static double FaceDiagonalXY(Cuboid cuboid)
    {
        return DistanceCalculator.CalculateDistance2DTo(0, 0, cuboid.Width, cuboid.Height);
    }

    public static double CalculateFaceDiagonalXY(this Cuboid cuboid)
    {
        return FaceDiagonalXY(cuboid);
    }

    private static double FaceDiagonalXZ(Cuboid cuboid)
    {
        return DistanceCalculator.CalculateDistance2DTo(0, 0, cuboid.Width, cuboid.Depth);
    }

    public static double CalculateFaceDiagonalXZ(this Cuboid cuboid)
    {
        return FaceDiagonalXZ(cuboid);
    }

    private static double FaceDiagonalYZ(Cuboid cuboid)
    {
        return DistanceCalculator.CalculateDistance2DTo(0, 0, cuboid.Height, cuboid.Depth);
    }

    public static double CalculateFaceDiagonalYZ(this Cuboid cuboid)
    {
        return FaceDiagonalYZ(cuboid);
    }
}
