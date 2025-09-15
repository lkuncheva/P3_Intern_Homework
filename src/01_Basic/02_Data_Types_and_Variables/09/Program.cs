using System;

class Program
{
    static void Main(string[] args)
    {
        int a = 5;
        int b = 10;

        Console.WriteLine("Before Swap:");
        Console.WriteLine($"a = {a}\n" +
                          $"b = {b}\n");

        a = a + b;
        b = a - b;
        a = a - b;

        Console.WriteLine("After Swap:");
        Console.WriteLine($"a = {a}\n" +
                          $"b = {b}");
    }
}
