using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Visible ASCII Characters (33 to 126):");

        for (int i = 33; i <= 126; i++)
        {
            Console.Write((char)i);
        }
    }
}