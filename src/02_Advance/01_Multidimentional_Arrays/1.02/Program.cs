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

        int[,] matrix = new int[n, m];

        Console.WriteLine("Enter matrix: ");
        for (int i = 0; i < n; i++)
        {
            int[] row = Console.ReadLine()
                               .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                               .Select(int.Parse)
                               .ToArray();
            for (int j = 0; j < m; j++)
            {
                matrix[i, j] = row[j];
            }
        }

        int maxSum = int.MinValue;
        int currentSum = 0;

        for (int row = 0; row <= n - 3; row++)
        {
            currentSum = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    currentSum += matrix[row + i, j];
                }
            }

            if (currentSum > maxSum)
            {
                maxSum = currentSum;
            }

            for (int col = 1; col <= m - 3; col++)
            {
                currentSum -= matrix[row, col - 1];
                currentSum -= matrix[row + 1, col - 1];
                currentSum -= matrix[row + 2, col - 1];

                currentSum += matrix[row, col + 2];
                currentSum += matrix[row + 1, col + 2];
                currentSum += matrix[row + 2, col + 2];

                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                }
            }
        }

        Console.WriteLine(maxSum);
    }
}