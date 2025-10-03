namespace Matrix;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter a positive number (N) for the matrix size:");

        int size = 0;

        while (true)
        {
            string input = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(input, out size) && size > 0 && size <= 100)
            {
                break;
            }

            Console.WriteLine("Invalid input. Please enter a positive number between 1 and 100.");
        }

        int[,] matrix = new int[size, size];
        int currentValue = 1;
        int currentRow = 0;
        int currentCol = 0;

        while (true)
        {
            currentValue = MatrixGenerator.Walk(
                matrix,
                currentRow,
                currentCol,
                currentValue,
                out currentRow,
                out currentCol);

            if (!MatrixGenerator.FindNextUnvisitedCell(matrix, out currentRow, out currentCol))
            {
                break;
            }
        }

        Console.WriteLine($"\nResulting {size}x{size} Matrix:");
        MatrixGenerator.PrintMatrix(matrix);
    }
}