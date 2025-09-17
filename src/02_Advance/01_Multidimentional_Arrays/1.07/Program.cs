class Program
{
    private static int n;
    private static int m;
    private static int[,] matrix;
    private static bool[,] visited;

    private static readonly int[] rowDirections = { -1, 1, 0, 0 };
    private static readonly int[] colDirections = { 0, 0, -1, 1 };

    static void Main(string[] args)
    {
        Console.WriteLine("Enter number of rows N and columns M (separated by a space): ");
        int[] inputNM = Console.ReadLine()
                                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse)
                                .ToArray();

        n = inputNM[0];
        m = inputNM[1];
        matrix = new int[n, m];
        visited = new bool[n, m];

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

        int maxArea = 0;

        for (int row = 0; row < n; row++)
        {
            for (int col = 0; col < m; col++)
            {
                if (!visited[row, col])
                {
                    int currentArea = DepthFirstSearch(row, col);
                    if (currentArea > maxArea)
                        maxArea = currentArea;
                }
            }
        }

        Console.WriteLine(maxArea);
    }

    static int DepthFirstSearch(int row, int col)
    {
        visited[row, col] = true;
        int area = 1;
        int currentNumber = matrix[row, col];

        for (int i = 0; i < 4; i++)
        {
            int nextRow = row + rowDirections[i];
            int nextCol = col + colDirections[i];

            if (nextRow >= 0 && nextRow < n && nextCol >= 0 && nextCol < m &&
                !visited[nextRow, nextCol] && matrix[nextRow, nextCol] == currentNumber)
            {
                area += DepthFirstSearch(nextRow, nextCol);
            }
        }

        return area;
    }
}