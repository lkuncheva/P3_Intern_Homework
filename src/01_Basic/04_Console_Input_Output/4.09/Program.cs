using System;

class Program
{
    static void Main(string[] args)
    {

        int N;
        long n1 = 0;
        long n2 = 1;
        long n3;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out N) || ((N < 1) || (N > 50)))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer in range [1, 50]: ");
        }


        for (int i = 0; i < N; i++)
        {

            if (i == 0 || i == 1)
                Console.Write(i);
            else
            {
                n3 = n1 + n2;
                n1 = n2;
                n2 = n3;

                Console.Write(n3);
            }

            if ((i + 1) != N)
                Console.Write(", ");

        }
    }
}