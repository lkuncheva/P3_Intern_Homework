using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main(string[] args)
    {
        int n;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out n) || (n < 1) || (n > 20))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer in range [1, 20]: ");
        }

        char fillOption;

        Console.WriteLine("Enter character (a, b, c, d): ");
        while (!char.TryParse(Console.ReadLine(), out fillOption) || ((fillOption != 'a') && (fillOption != 'b') && (fillOption != 'c') && (fillOption != 'd')))
        {
            Console.WriteLine("Invalid input. Please enter a valid character (a, b, c, d): ");
        }

        int[,] matrix = new int[n, n];

        int number = 1;

        switch (fillOption)
        {
            case 'a':
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        matrix[j, i] = number++;
                    }
                }
                break;

            case 'b':
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        matrix[j, i] = number++;
                    }

                    if (i < n - 1)
                    {
                        for (int j = n - 1; j >= 0; j--)
                        {
                            matrix[j, i + 1] = number++;
                        }
                        i++;
                    }
                }
                break;

            case 'c':
                for (int diagonal = n - 1; diagonal >= -(n - 1); diagonal--)
                {
                    int row, col;
                    if (diagonal >= 0)
                    {
                        row = diagonal;
                        col = 0;
                    }
                    else
                    {
                        row = 0;
                        col = -diagonal;
                    }

                    while (row < n && col < n)
                    {
                        matrix[row, col] = number++;
                        row++;
                        col++;
                    }
                }
                break;

            case 'd':
                int top = 0;
                int bottom = n - 1;
                int left = 0;
                int right = n - 1;

                while (number <= (n * n))
                {
                    for (int i = top; i <= bottom; i++)
                    {
                        matrix[i, left] = number++;
                    }
                    left++;

                    for (int i = left; i <= right; i++)
                    {
                        matrix[bottom, i] = number++;
                    }
                    bottom--;

                    for (int i = bottom; i >= top; i--)
                    {
                        matrix[i, right] = number++;
                    }
                    right--;

                    for (int i = right; i >= left; i--)
                    {
                        matrix[top, i] = number++;
                    }
                    top++;
                }
                break;

            default:
                break;
        }
           
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(matrix[i, j].ToString().PadLeft(4));
            }
            Console.WriteLine();
        }
    }
}