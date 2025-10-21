namespace Matrix;
public struct Location
{
    public int Row { get; }
    public int Col { get; }

    public Location(int row, int col)
    {
        Row = row;
        Col = col;
    }
}