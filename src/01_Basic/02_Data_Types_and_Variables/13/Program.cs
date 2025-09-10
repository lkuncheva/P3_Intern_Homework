using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter first floating-point number: ");
        double d1 = double.Parse(Console.ReadLine());

        Console.WriteLine("Enter second floating-point number: ");
        double d2 = double.Parse(Console.ReadLine());

        double eps = 0.000001;

        bool compare = Math.Abs(d1 - d2) < eps;
        Console.WriteLine(compare);

    }
}