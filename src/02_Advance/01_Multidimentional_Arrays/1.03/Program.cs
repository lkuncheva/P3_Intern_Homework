class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter number of rows N and columns M (separated by a space): ");
        int[] inputNM = Console.ReadLine()
                                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse)
                                .ToArray();

        int n = inputNM[0];
        int m = inputNM[1];

        string[,] matrix = new string[n, m];

        Console.WriteLine("Enter matrix: ");
        for (int i = 0; i < n; i++)
        {
            string[] row = Console.ReadLine()
                               .Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < m; j++)
            {
                matrix[i, j] = row[j];
            }
        }

        int maxSequence = 1;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                string currentElement = matrix[i, j];
                int currentRightSequence = 1;
                int currentDownSequence = 1;
                int currentDiagonalRightSequence = 1;
                int currentDiagonalLeftSequence = 1;

                for (int k = j + 1; k < m; k++)
                {
                    if (matrix[i, k] == currentElement)
                    {
                        currentRightSequence++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (currentRightSequence > maxSequence)
                {
                    maxSequence = currentRightSequence;
                }

                for (int k = i + 1; k < n; k++)
                {
                    if (matrix[k, j] == currentElement)
                    {
                        currentDownSequence++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (currentDownSequence > maxSequence)
                {
                    maxSequence = currentDownSequence;
                }

                int row = i + 1;
                int col = j + 1;
                while (row < n && col < m && matrix[row, col] == currentElement)
                {
                    currentDiagonalRightSequence++;
                    row++;
                    col++;
                }
                if (currentDiagonalRightSequence > maxSequence)
                {
                    maxSequence = currentDiagonalRightSequence;
                }

                row = i + 1;
                col = j - 1;
                while (row < n && col >= 0 && matrix[row, col] == currentElement)
                {
                    currentDiagonalLeftSequence++;
                    row++;
                    col--;
                }
                if (currentDiagonalLeftSequence > maxSequence)
                {
                    maxSequence = currentDiagonalLeftSequence;
                }
            }
        }

        Console.WriteLine(maxSequence);
    }
}