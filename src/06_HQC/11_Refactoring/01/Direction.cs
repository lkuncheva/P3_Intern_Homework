namespace Matrix;

public struct Direction
{
    public int DeltaRow { get; }
    public int DeltaCol { get; }

    public Direction(int deltaRow, int deltaCol)
    {
        DeltaRow = deltaRow;
        DeltaCol = deltaCol;
    }
}