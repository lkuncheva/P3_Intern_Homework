namespace MineSweeper;

public static class Game
{
    private static int CountRevealedCells(char[,] visibleBoard)
    {
        int count = 0;

        for (int r = 0; r < Board.BoardRows; r++)
        {
            for (int c = 0; c < Board.BoardCols; c++)
            {
                if (visibleBoard[r, c] != Board.CoverChar)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public static void GamePlay()
    {
        string command = string.Empty;
        char[,] visibleBoard = Board.CreateEmptyBoard();
        char[,] mineField = Board.InitializeMineField();
        int scoreCounter = 0;
        bool exploded = false;

        List<PlayerScore> highScores = new List<PlayerScore>(6);

        int rowRaw = 0;
        int colRaw = 0;

        int row = 0;
        int col = 0;

        bool isFirstTurn = true;
        bool hasWon = false;

        do
        {
            if (isFirstTurn)
            {
                Console.WriteLine("Welcome to Minesweeper! Try your luck finding fields without mines. " +
                                  "Commands: 'top' (show scoreboard), 'restart' (start new game), 'exit' (quit).");
                Board.DisplayBoard(visibleBoard);
                isFirstTurn = false;
            }

            Console.Write("Enter row and column (e.g., '1 5'): ");
            string rawCommand = Console.ReadLine()?.Trim() ?? string.Empty;
            command = rawCommand.ToLower();

            string[] parts = rawCommand.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 2 &&
                int.TryParse(parts[0], out rowRaw) &&
                int.TryParse(parts[1], out colRaw))
            {
                row = rowRaw - 1;
                col = colRaw - 1;

                if (row >= 0 && row < Board.BoardRows &&
                    col >= 0 && col < Board.BoardCols)
                {
                    command = "turn";
                }
            }
            else if (command == "top" || command == "restart" || command == "exit") { }
            else
            {
                command = "invalid";
            }

            switch (command)
            {
                case "top":
                    HighScores.DisplayHighScores(highScores);
                    break;

                case "restart":
                    visibleBoard = Board.CreateEmptyBoard();
                    mineField = Board.InitializeMineField();
                    Board.DisplayBoard(visibleBoard);
                    exploded = false;
                    isFirstTurn = true;
                    scoreCounter = 0;
                    break;

                case "exit":
                    Console.WriteLine("Goodbye! Thanks for playing!");
                    break;

                case "turn":
                    if (visibleBoard[row, col] == Board.CoverChar)
                    {
                        if (mineField[row, col] != Board.MineChar)
                        {
                            Board.RevealCell(visibleBoard, mineField, row, col);

                            scoreCounter = CountRevealedCells(visibleBoard);

                            if (Board.MaxSafeCells == scoreCounter)
                            {
                                hasWon = true;
                            }

                            if (!hasWon)
                            {
                                Board.DisplayBoard(visibleBoard);
                            }
                        }
                        else
                        {
                            exploded = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nCell already revealed. Try a different spot.\n");
                        Board.DisplayBoard(visibleBoard);
                    }

                    break;

                default:
                    Console.WriteLine("\nError! Invalid command or coordinates. Ensure you use 'row col' format.\n");
                    break;
            }

            if (exploded)
            {
                Board.DisplayBoard(mineField);
                Console.Write($"\nBoom! You exploded heroically with {scoreCounter} points. Enter your nickname: ");
                string? input = Console.ReadLine();
                string nickname = string.IsNullOrWhiteSpace(input) ? "N/A" : input;

                PlayerScore newScore = new PlayerScore(nickname, scoreCounter);
                HighScores.UpdateHighScores(highScores, newScore);

                visibleBoard = Board.CreateEmptyBoard();
                mineField = Board.InitializeMineField();
                scoreCounter = 0;
                exploded = false;
                isFirstTurn = true;
            }

            if (hasWon)
            {
                Console.WriteLine($"\nCONGRATULATIONS! You cleared all {Board.MaxSafeCells} safe cells!");
                Board.DisplayBoard(mineField);
                Console.Write("Enter your name for the hall of fame: ");
                string? input = Console.ReadLine();
                string name = string.IsNullOrWhiteSpace(input) ? "N/A" : input;

                PlayerScore winningScore = new PlayerScore(name, scoreCounter);
                HighScores.UpdateHighScores(highScores, winningScore);

                visibleBoard = Board.CreateEmptyBoard();
                mineField = Board.InitializeMineField();
                scoreCounter = 0;
                hasWon = false;
                isFirstTurn = true;
            }
        } while (command != "exit");
    }
}