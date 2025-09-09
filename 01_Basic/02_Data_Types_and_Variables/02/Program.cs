using System;

class Program
{
    static void Main(string[] args)
    {
        float a = 12.345f;
        float b = 3456.091f;
        double c = 34.567839023;
        double d = 8923.1234857;

        Console.WriteLine("Both float and double store fractional numbers.\n" +
                          "Float is sufficient for storing 6 to 7 decimal digits.\n" +
                          "Double is sufficient for storing 15 decimal digits.\n");

        Console.WriteLine($"float a = {a}\n" +
                          $"float b = {b}\n" +
                          $"double c = {c}\n" +
                          $"double d = {d}");
    }
}