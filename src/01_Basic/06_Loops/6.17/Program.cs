using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a number:");

        int n;

        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive integer:");
        }

        int[,] spiraMmatrix = new int[n, n];

        // Counter
        int number = 1;

        // Boundaries 
        int top = 0;
        int bottom = n - 1;
        int left = 0;
        int right = n - 1;
        

        while (number <= (n * n))
        {
            // Going right
            for (int i = left; i <= right; i++)
            {
                spiraMmatrix[top, i] = number;
                number++;
            }
            top++;

            // Going down
            for (int i = top; i <= bottom; i++)
            {
                spiraMmatrix[i, right] = number;
                number++;
            }
            right--;

           
            // Going left
            for (int i = right; i >= left; i--)
            {
                spiraMmatrix[bottom, i] = number;
                number++;
            }
            bottom--;
           

            // Going up
            for (int i = bottom; i >= top; i--)
            {
                spiraMmatrix[i, left] = number;
                number++;
            }
            left++;

        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(spiraMmatrix[i, j].ToString().PadLeft(4, ' '));
            }
            Console.WriteLine();
        }
    }
}
