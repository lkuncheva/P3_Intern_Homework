using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter two integers separated by a whitespace: ");

        string[] input;
        int a, b;

        while (true)
        {
            input = Console.ReadLine().Split(' ');

            if (input.Length != 2 || (!int.TryParse(input[0], out a) || (a < 2) || (a > 500)) || (!int.TryParse(input[1], out b) || (b < 2) || (b > 500)))
            {
                Console.WriteLine("Invalid input. Please enter two valid integers separated by a whitespace in the range [2, 500].");
            }
            else
            {
                break;
            }
        }     

        while (b != 0)
        {
            int temp = b; 
            b = a % b;    
            a = temp;     

        }
        Console.WriteLine(a);
        
    }
}