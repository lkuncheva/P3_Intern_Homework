public static class Board
{
    public const int BoardRows = 15;
    public const int BoardCols = 20;

    public const int NumMines = 30;
    public const int MaxSafeCells = BoardRows * BoardCols - NumMines;

    public const char CoverChar = '?';
    public const char MineChar = '*';
    private const char EmptyCharInternal = '-';
    public const char RevealedEmptyChar = '.';

    public static char[,] CreateEmptyBoard()
    {
        char[,] board = new char[BoardRows, BoardCols];

        for (int i = 0; i < BoardRows; i++)
        {
            for (int j = 0; j < BoardCols; j++)
            {
                board[i, j] = CoverChar;
            }
        }

        return board;
    }

    public static char[,] InitializeMineField()
    {
        char[,] mineField = new char[BoardRows, BoardCols];

        for (int i = 0; i < BoardRows; i++)
        {
            for (int j = 0; j < BoardCols; j++)
            {
                mineField[i, j] = EmptyCharInternal;
            }
        }

        Random random = new Random();
        int minesPlaced = 0;

        while (minesPlaced < NumMines)
        {
            int r = random.Next(BoardRows);
            int c = random.Next(BoardCols);
            if (mineField[r, c] != MineChar)
            {
                mineField[r, c] = MineChar;
                minesPlaced++;
            }
        }

        for (int r = 0; r < BoardRows; r++)
        {
            for (int c = 0; c < BoardCols; c++)
            {
                if (mineField[r, c] != MineChar)
                {
                    int mineCount = CountAdjacentMines(mineField, r, c);
                    mineField[r, c] = (mineCount > 0) ? (char)(mineCount + '0') : RevealedEmptyChar;
                }
            }
        }

        return mineField;
    }
    private static int CountAdjacentMines(char[,] field, int row, int col)
    {
        int count = 0;

        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = col - 1; j <= col + 1; j++)
            {
                if (i >= 0 && i < BoardRows && j >= 0 && j < BoardCols && (i != row || j != col))
                {
                    if (field[i, j] == MineChar)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }
    public static void DisplayBoard(char[,] board)
    {
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkGray;

        Console.Write("  ");

        for (int c = 0; c < BoardCols; c++)
        {
            Console.Write($"{(c + 1), 3}");
        }

        Console.WriteLine();
        Console.WriteLine("  " + new string('-', BoardCols * 3));

        Console.ResetColor();

        for (int r = 0; r < BoardRows; r++)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"{(r + 1):00}| ");
            Console.ResetColor();

            for (int c = 0; c < BoardCols; c++)
            {
                char cell = board[r, c];

                if (cell >= '1' && cell <= '8')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (cell == MineChar)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (cell == CoverChar)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else if (cell == RevealedEmptyChar)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write(cell);

                Console.ResetColor();
                Console.Write("  ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
    public static void RevealCell(char[,] visibleBoard, char[,] mineField, int row, int col)
    {
        if (row < 0 || row >= BoardRows || col < 0 || col >= BoardCols)
        {
            return;
        }
        if (visibleBoard[row, col] != CoverChar)
        {
            return;
        }
        if (mineField[row, col] == MineChar)
        {
            return;
        }

        visibleBoard[row, col] = mineField[row, col];

        if (visibleBoard[row, col] == RevealedEmptyChar)
        {
            for (int rOffset = -1; rOffset <= 1; rOffset++)
            {
                for (int cOffset = -1; cOffset <= 1; cOffset++)
                {
                    if (rOffset == 0 && cOffset == 0) continue;

                    RevealCell(visibleBoard, mineField, row + rOffset, col + cOffset);
                }
            }
        }
    }
}