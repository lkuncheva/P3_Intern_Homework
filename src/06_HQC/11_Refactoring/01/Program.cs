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

        int[,] data = new int[size, size];

        var matrixField = new MatrixGenerator(data);

        int currentValue = 1;

        var currentPosition = new Location(0, 0);
        Location nextPosition;

        while (true)
        {
            currentValue = matrixField.Walk(
                currentPosition,
                currentValue,
                out nextPosition);

            currentPosition = nextPosition;

            if (!matrixField.TryFindNextUnvisitedCell(out currentPosition))
            {
                break;
            }
        }

        Console.WriteLine($"\nResulting {size}x{size} Matrix:");
        matrixField.PrintMatrix();
    }
}