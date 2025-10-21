namespace Matrix;

public class MatrixGenerator
{
    private readonly int[,] matrix;
    private readonly int size;

    private static readonly Direction[] Directions =
    {
        new Direction(1, 1),   // Down-Right
        new Direction(1, 0),   // Right
        new Direction(1, -1),  // Up-Right
        new Direction(0, -1),  // Up
        new Direction(-1, -1), // Up-Left
        new Direction(-1, 0),  // Left
        new Direction(-1, 1),  // Down-Left
        new Direction(0, 1)    // Down
    };

    public MatrixGenerator(int[,] matrix)
    {
        if (matrix == null)
        {
            throw new ArgumentNullException(nameof(matrix), "Matrix cannot be null.");
        }

        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        if (rows != cols)
        {
            throw new ArgumentException("Matrix must be square (rows must equal columns).");
        }

        this.matrix = matrix;
        this.size = rows;
    }

    private static int GetNextDirectionIndex(int currentDirectionIndex)
    {
        return (currentDirectionIndex + 1) % Directions.Length;
    }

    private bool CanContinueWalk(int row, int col)
    {
        foreach (var dir in Directions)
        {
            int nextRow = row + dir.DeltaRow;
            int nextCol = col + dir.DeltaCol;

            bool isInBounds = nextRow >= 0 && nextRow < size &&
                              nextCol >= 0 && nextCol < size;

            if (isInBounds && matrix[nextRow, nextCol] == 0)
            {
                return true;
            }
        }

        return false;
    }

    private bool isValidMove(int r, int c)
    {
        return r >= 0 && r < size && c >= 0 && c < size && matrix[r, c] == 0;
    }

    public int Walk(
        Location startPosition,
        int startValue,
        out Location nextPosition)
    {
        int currentValue = startValue;
        int currentRow = startPosition.Row;
        int currentCol = startPosition.Col;
        int currentDirectionIndex = 0;

        while (true)
        {
            matrix[currentRow, currentCol] = currentValue;
            currentValue++;

            if (!CanContinueWalk(currentRow, currentCol))
            {
                break;
            }

            Direction currentDirection = Directions[currentDirectionIndex];
            int nextRow = currentRow + currentDirection.DeltaRow;
            int nextCol = currentCol + currentDirection.DeltaCol;

            if (!isValidMove(nextRow, nextCol))
            {
                for (int i = 0; i < Directions.Length; i++)
                {
                    currentDirectionIndex = GetNextDirectionIndex(currentDirectionIndex);
                    currentDirection = Directions[currentDirectionIndex];
                    nextRow = currentRow + currentDirection.DeltaRow;
                    nextCol = currentCol + currentDirection.DeltaCol;

                    if (isValidMove(nextRow, nextCol))
                    {
                        break;
                    }
                }
            }

            currentRow = nextRow;
            currentCol = nextCol;
        }

        nextPosition = new Location(currentRow, currentCol);
        return currentValue;
    }

    public bool TryFindNextUnvisitedCell(out Location position)
    {
        position = default;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (matrix[i, j] == 0)
                {
                    position = new Location(i, j);
                    return true;
                }
            }
        }

        return false;
    }

    public void PrintMatrix()
    {
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Console.Write("{0,3}", matrix[row, col]);
            }

            Console.WriteLine();
        }
    }
}