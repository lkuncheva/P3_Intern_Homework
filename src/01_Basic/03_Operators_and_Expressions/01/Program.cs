using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine( "Enter an integer number:" );
        string input = Console.ReadLine();

        if (int.TryParse(input, out int intNumber))
        {
            if (intNumber >= -30 && intNumber <= 30)
            {
                if (intNumber % 2 == 0)
                    Console.WriteLine($"even {intNumber}");
                else
                    Console.WriteLine($"odd {intNumber}");
            }
            else
                Console.WriteLine("The number you entered is invalid (range is [-30,30])");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
        }


    }
}