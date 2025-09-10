using System;

class Program
{
    static void Main(string[] args)
    {
        int a = 5;
        int b = 10;
        int c;

        Console.WriteLine("Before Swap:");
        Console.WriteLine($"a = {a}\n" +
                          $"b = {b}\n");

        c = a;
        a = b;
        b = c;

        Console.WriteLine("After Swap:");
        Console.WriteLine($"a = {a}\n" +
                          $"b = {b}");
    }
}
