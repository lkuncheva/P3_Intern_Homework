class Program
{
    static void Main(string[] args)
    {
        string filePath = "../../../matrix.txt";
        int sizeOfMatrix;
        int[,] matrix;

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                if (!int.TryParse(reader.ReadLine(), out sizeOfMatrix))
                {
                    throw new FormatException("The first line of the file is not a valid integer for the matrix size.");
                }

                matrix = new int[sizeOfMatrix, sizeOfMatrix];

                for (int i = 0; i < sizeOfMatrix; i++)
                {
                    int[] row = reader.ReadLine()
                                       .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(int.Parse)
                                       .ToArray();

                    for (int j = 0; j < sizeOfMatrix; j++)
                    {
                        matrix[i, j] = row[j];
                    }
                }
            }

            int maxSum = int.MinValue;

            for (int row = 0; row <= sizeOfMatrix - 2; row++)
            {
                for (int col = 0; col <= sizeOfMatrix - 2; col++)
                {
                    int currentSum = 0;

                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            currentSum += matrix[row + i, col + j];
                        }
                    }

                    if (currentSum > maxSum)
                    {
                        maxSum = currentSum;
                    }
                }
            }

            File.WriteAllText("../../../result.txt", maxSum.ToString());
            Console.WriteLine("Max sum written succesfully in the result.txt file.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The file '{filePath}' was not found.");
        }
        catch (FormatException)
        {
            Console.WriteLine($"Error: An invalid number found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}