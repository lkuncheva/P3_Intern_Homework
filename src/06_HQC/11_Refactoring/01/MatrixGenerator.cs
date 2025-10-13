namespace Matrix;

public static class MatrixGenerator
{
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

    public static int GetNextDirectionIndex(int currentDirectionIndex)
    {
        return (currentDirectionIndex + 1) % Directions.Length;
    }

    public static bool CanContinueWalk(int[,] matrix, int row, int col)
    {
        int size = matrix.GetLength(0);

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

    public static int Walk(
        int[,] matrix,
        int startRow,
        int startCol,
        int startValue,
        out int currentRow,
        out int currentCol)
    {
        int size = matrix.GetLength(0);
        int currentValue = startValue;
        currentRow = startRow;
        currentCol = startCol;
        int currentDirectionIndex = 0;

        while (true)
        {
            matrix[currentRow, currentCol] = currentValue;
            currentValue++;

            if (!CanContinueWalk(matrix, currentRow, currentCol))
            {
                break;
            }

            Direction currentDirection = Directions[currentDirectionIndex];
            int nextRow = currentRow + currentDirection.DeltaRow;
            int nextCol = currentCol + currentDirection.DeltaCol;

            bool isValidMove(int r, int c)
            {
                return r >= 0 && r < size && c >= 0 && c < size && matrix[r, c] == 0;
            }

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

        return currentValue;
    }

    public static bool FindNextUnvisitedCell(int[,] matrix, out int row, out int col)
    {
        int size = matrix.GetLength(0);
        row = 0;
        col = 0;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (matrix[i, j] == 0)
                {
                    row = i;
                    col = j;
                    return true;
                }
            }
        }

        return false;
    }

    public static void PrintMatrix(int[,] matrix)
    {
        int size = matrix.GetLength(0);
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