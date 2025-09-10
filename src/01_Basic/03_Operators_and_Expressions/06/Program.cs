using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main(string[] args)
    {
        const int length = 4;
        int input;
        int sum = 0;

        Console.WriteLine("Enter integer: ");

        while (!int.TryParse(Console.ReadLine(), out input) || input.ToString().Length != length)
        {
            Console.WriteLine("Invalid input. Please enter a valid positive 4-digit integer: ");
        }

        string abcd = input.ToString();
        for (int i = 0; i < length; i++)
        {
            sum += int.Parse(abcd[i].ToString());
        }
        Console.WriteLine($"Sum of digits: {sum}");

        string dcba = new string(abcd.Reverse().ToArray());
        Console.WriteLine("Reverse: " + dcba);

        string dabc = new string(abcd[length-1].ToString() + abcd.Substring(0, abcd.Length-1));
        Console.WriteLine("Last digit in front: " + dabc);

        string acbd = new string(abcd[0].ToString() + abcd[2].ToString() + abcd[1].ToString() + abcd[3].ToString());
        Console.WriteLine("Second and Third digits swapped: " + acbd);
    }
}