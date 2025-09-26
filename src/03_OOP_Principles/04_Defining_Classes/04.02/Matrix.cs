using System.Numerics;
using System.Text;

namespace _04._02
{
    public class Matrix<T> where T :
    IAdditionOperators<T, T, T>,
    ISubtractionOperators<T, T, T>,
    IMultiplyOperators<T, T, T>,
    IAdditiveIdentity<T, T>,
    IEquatable<T>
    {
        private T[,] numberMatrix;

        public int Rows { get; private set; }

        public int Cols { get; private set; }

        public Matrix(int rows, int cols)
        {
            if (rows <= 0 || cols <= 0)
            {
                throw new ArgumentException("Matrix dimensions must be positive.");
            }

            this.Rows = rows;
            this.Cols = cols;
            this.numberMatrix = new T[rows, cols];
        }

        public T this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= Rows || col < 0 || col >= Cols)
                {
                    throw new IndexOutOfRangeException("Matrix index is outside the valid range.");
                }

                return numberMatrix[row, col];
            }
            set
            {
                if (row < 0 || row >= Rows || col < 0 || col >= Cols)
                {
                    throw new IndexOutOfRangeException("Matrix index is outside the valid range.");
                }

                numberMatrix[row, col] = value;
            }
        }

        public static Matrix<T> operator +(Matrix<T> left, Matrix<T> right)
        {
            if (left.Rows != right.Rows || left.Cols != right.Cols)
            {
                throw new InvalidOperationException("Matrix addition requires matrices of the same size.");
            }

            Matrix<T> result = new Matrix<T>(left.Rows, left.Cols);
            for (int i = 0; i < left.Rows; i++)
            {
                for (int j = 0; j < left.Cols; j++)
                {
                    result[i, j] = left[i, j] + right[i, j];
                }
            }

            return result;
        }

        public static Matrix<T> operator -(Matrix<T> left, Matrix<T> right)
        {
            if (left.Rows != right.Rows || left.Cols != right.Cols)
            {
                throw new InvalidOperationException("Matrix subtraction requires matrices of the same size.");
            }

            Matrix<T> result = new Matrix<T>(left.Rows, left.Cols);
            for (int i = 0; i < left.Rows; i++)
            {
                for (int j = 0; j < left.Cols; j++)
                {
                    result[i, j] = left[i, j] - right[i, j];
                }
            }

            return result;
        }

        public static Matrix<T> operator *(Matrix<T> left, Matrix<T> right)
        {
            if (left.Cols != right.Rows)
            {
                throw new InvalidOperationException(
                    "Matrix multiplication requires the number of columns of the first matrix " +
                    "to equal the number of rows of the second matrix (A is m x n, B must be n x p).");
            }

            int resultRows = left.Rows;
            int resultCols = right.Cols;
            int innerDim = left.Cols;

            Matrix<T> result = new Matrix<T>(resultRows, resultCols);
            T zero = T.AdditiveIdentity;

            for (int i = 0; i < resultRows; i++)
            {
                for (int j = 0; j < resultCols; j++)
                {
                    T sum = zero;
                    for (int k = 0; k < innerDim; k++)
                    {
                        T term = left[i, k] * right[k, j];
                        sum += term;
                    }

                    result[i, j] = sum;
                }
            }

            return result;
        }

        public static bool operator true(Matrix<T> matrix)
        {
            T zero = T.AdditiveIdentity;

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Cols; j++)
                {
                    if (!matrix[i, j].Equals(zero))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool operator false(Matrix<T> matrix)
        {
            T zero = T.AdditiveIdentity;

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Cols; j++)
                {
                    if (!matrix[i, j].Equals(zero))
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Matrix<{typeof(T).Name}> ({Rows}x{Cols}):");

            for (int i = 0; i < Rows; i++)
            {
                sb.Append("| ");
                for (int j = 0; j < Cols; j++)
                {
                    sb.Append(numberMatrix[i, j]?.ToString()?.PadRight(6) ?? "null  ");
                }

                sb.AppendLine("|");
            }

            return sb.ToString();
        }
    }
}