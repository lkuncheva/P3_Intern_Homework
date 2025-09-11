using System;

class Program
{
    static void Main(string[] args)
    {
        int score;

        Console.WriteLine("Enter score: ");
        while (!int.TryParse(Console.ReadLine(), out score))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer number for the score: ");
        }

        if (score >= 1 && score <= 3)
            Console.WriteLine(score * 10);
        else if (score >= 4 && score <= 6)
            Console.WriteLine(score * 100);
        else if (score >= 7 && score <= 9)
            Console.WriteLine(score * 1000);
        else
            Console.WriteLine("Invalid Score");
    }
}