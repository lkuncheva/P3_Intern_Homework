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

        try
        {
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
                        {
                            maxArea = currentArea;
                        }
                    }
                }
            }

            Console.WriteLine(maxArea);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine("Please ensure the input format is correct (N M on line 1, followed by N lines of M space-separated numbers).");
        }
    }

    static int DepthFirstSearch(int startRow, int startCol)
    {
        var stack = new Stack<(int row, int col)>();

        int targetValue = matrix[startRow, startCol];
        int area = 0;

        stack.Push((startRow, startCol));
        visited[startRow, startCol] = true;

        while (stack.Count > 0)
        {
            var (currentRow, currentCol) = stack.Pop();
            area++; 

            for (int i = 0; i < 4; i++)
            {
                int nextRow = currentRow + rowDirections[i];
                int nextCol = currentCol + colDirections[i];

                bool inBounds = nextRow >= 0 && nextRow < n && nextCol >= 0 && nextCol < m;

                if (inBounds)
                {
                    if (!visited[nextRow, nextCol] && matrix[nextRow, nextCol] == targetValue)
                    {
                        visited[nextRow, nextCol] = true;

                        stack.Push((nextRow, nextCol));
                    }
                }
            }
        }

        return area;
    }
}