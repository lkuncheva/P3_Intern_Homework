using System;
class Program
{
    static void Main(string[] args)
    {
        int? a = null;
        double? b = null;

        Console.WriteLine($"Null int: {a}\n" +
                          $"Null double: {b}\n");

        Console.WriteLine($"Null int + 10: {a + 10}\n" +
                          $"Null double + 10.5: {b + 10.5}");
    }
}