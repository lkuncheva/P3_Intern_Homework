using System;

class Program
{
    static void Main(string[] args)
    {
        double numberA, numberB, swapHelper;

        Console.WriteLine("Enter a number A: ");
        while (!double.TryParse(Console.ReadLine(), out numberA) || (numberA < -100 || numberA > 100))
        {
            Console.WriteLine("Invalid input. Please enter a valid number A in range (-100, 100): ");
        }

        Console.WriteLine("Enter a number B: ");
        while (!double.TryParse(Console.ReadLine(), out numberB) || (numberB < -100 || numberB > 100))
        {
            Console.WriteLine("Invalid input. Please enter a valid number B in range (-100, 100): ");
        }

        if (numberA > numberB)
        {
            swapHelper = numberA;
            numberA = numberB;
            numberB = swapHelper;
        }

        Console.WriteLine(numberA + " " + numberB);

    }
}