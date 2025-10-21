namespace MineSweeper;

public static class Game
{
    private const string CommandTop = "top";
    private const string CommandRestart = "restart";
    private const string CommandExit = "exit";
    private const string CommandTurn = "turn";
    private const string CommandInvalid = "invalid";

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

    private static void RestartGame(
        out char[,] visibleBoard,
        out char[,] mineField,
        out int scoreCounter,
        out bool exploded,
        out bool isFirstTurn,
        out bool hasWon)
    {
        visibleBoard = Board.CreateEmptyBoard();
        mineField = Board.InitializeMineField();

        Board.DisplayBoard(visibleBoard);

        scoreCounter = 0;
        exploded = false;
        isFirstTurn = true;
        hasWon = false;
    }

    public static void GamePlay()
    {
        RestartGame(
            out char[,] visibleBoard,
            out char[,] mineField,
            out int scoreCounter,
            out bool exploded,
            out bool isFirstTurn,
            out bool hasWon);

        string command = string.Empty;

        List<PlayerScore> highScores = new List<PlayerScore>(6);

        int rowRaw = 0;
        int colRaw = 0;

        int row = 0;
        int col = 0;

        do
        {
            if (isFirstTurn)
            {
                Console.WriteLine("Welcome to Minesweeper! Try your luck finding fields without mines. " +
                                  "Commands: 'top' (show scoreboard), 'restart' (start new game), 'exit' (quit).");
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
                    command = CommandTurn;
                }
                else
                {
                    command = CommandInvalid;
                }
            }
            else if (command != CommandTop && command != CommandRestart && command != CommandExit)
            {
                command = CommandInvalid;
            }

            switch (command)
            {
                case CommandTop:
                    HighScores.DisplayHighScores(highScores);
                    break;

                case CommandRestart:
                    RestartGame(
                        out visibleBoard,
                        out mineField,
                        out scoreCounter,
                        out exploded,
                        out isFirstTurn,
                        out hasWon);
                    break;

                case CommandExit:
                    Console.WriteLine("Goodbye! Thanks for playing!");
                    break;

                case CommandTurn:
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

                RestartGame(
                        out visibleBoard,
                        out mineField,
                        out scoreCounter,
                        out exploded,
                        out isFirstTurn,
                        out hasWon);
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

                RestartGame(
                        out visibleBoard,
                        out mineField,
                        out scoreCounter,
                        out exploded,
                        out isFirstTurn,
                        out hasWon);
            }
        } while (command != CommandExit);
    }
}