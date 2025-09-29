using System.Text;

class Matrix
{
    private int[,] _data;
    public int Rows { get; }
    public int Columns { get; }
    public Matrix(int rows, int cols)
    {
        if (rows <= 0 || cols <= 0)
        {
            throw new ArgumentException("Each dimension must be greater than 0!");
        }

        Rows = rows;
        Columns = cols;
        _data = new int[rows, cols];
    }

    public Matrix(int[,] matrix)
    {
        Rows = matrix.GetLength(0);
        Columns = matrix.GetLength(1);
        _data = matrix;
    }

    public int this[int row, int col]
    {
        get
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Columns)
            {
                throw new ArgumentOutOfRangeException("Index is out of range!");
            }

            return _data[row, col];
        }
        set
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Columns)
            {
                throw new ArgumentOutOfRangeException("Index is out of range!");
            }

            _data[row, col] = value;
        }
    }
    public static Matrix operator +(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
        {
            throw new InvalidOperationException("Matrices must have the same dimensions for addition.");
        }

        Matrix result = new Matrix(matrix1.Rows, matrix1.Columns);

        for (int i = 0; i < result.Rows; i++)
        {
            for (int j = 0; j < result.Columns; j++)
            {
                result._data[i, j] = matrix1._data[i, j] + matrix2._data[i, j];
            }
        }

        return result;
    }

    public static Matrix operator -(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
        {
            throw new InvalidOperationException("Matrices must have the same dimensions for subtraction.");
        }

        Matrix result = new Matrix(matrix1.Rows, matrix1.Columns);

        for (int i = 0; i < result.Rows; i++)
        {
            for (int j = 0; j < result.Columns; j++)
            {
                result._data[i, j] = matrix1._data[i, j] - matrix2._data[i, j];
            }
        }

        return result;
    }

    public static Matrix operator *(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.Columns != matrix2.Rows)
        {
            throw new InvalidOperationException("The number of columns in the first matrix must equal the number of rows in the second matrix for multiplication.");
        }

        Matrix result = new Matrix(matrix1.Rows, matrix2.Columns);

        for (int i = 0; i < matrix1.Rows; i++)
        {
            for (int j = 0; j < matrix2.Columns; j++)
            {
                for (int k = 0; k < matrix1.Columns; k++)
                {
                    result._data[i, j] += matrix1._data[i, k] * matrix2._data[k, j];
                }
            }
        }

        return result;
    }

    public override string ToString()
    {
        StringBuilder matrixString = new StringBuilder();
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                matrixString.AppendFormat("{0,4}", _data[i, j]);
            }

            matrixString.AppendLine();
        }

        return matrixString.ToString();
    }
}
class Program
{
    static void Main()
    {
        int[,] firstMatrix = {{1, 2, 3},
                              {4, 5, 6},
                              {7, 8, 9}};

        int[,] secondMatrix = {{5, 10, 9},
                               {7, 2, -5},
                               {3, 15, 1}};

        Matrix matrix1 = new Matrix(firstMatrix);
        Matrix matrix2 = new Matrix(secondMatrix);

        Console.WriteLine("1st matrix: ");
        Console.WriteLine(matrix1);

        Console.WriteLine("2nd Matrix: ");
        Console.WriteLine(matrix2);

        Matrix sum = matrix1 + matrix2;
        Console.WriteLine("Matrices added: ");
        Console.WriteLine(sum);

        Matrix difference = matrix1 - matrix2;
        Console.WriteLine("Matrices subtracted: ");
        Console.WriteLine(difference);

        Matrix product = matrix1 * matrix2;
        Console.WriteLine("Matrices multiplied: ");
        Console.WriteLine(product);

        Console.WriteLine("Element with index [2, 2] from the second matrix: " + matrix2[2, 2]);
    }
}