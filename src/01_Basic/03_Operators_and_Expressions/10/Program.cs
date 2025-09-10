using System;

class Program
{
    static void Main(string[] args)
    {
        float x, y;

        float[] centerOfCircle = { 1, 1 };
        float radiusOfCircle = 1.5f;

        int rectangleTop = 1;
        int rectangleLeft = -1;
        int rectangleWidth = 6;
        int rectangleHeight = 2;


        Console.WriteLine("Enter value of X: ");

        while (!float.TryParse(Console.ReadLine(), out x) || (x < -1000 || x > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid number for X in range (-1000, 1000).");
        }

        Console.WriteLine("Enter value of Y: ");
        while (!float.TryParse(Console.ReadLine(), out y) || (y < -1000 || y > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid number for Y in range (-1000, 1000).");
        }

        double distance = Math.Pow((Math.Pow((x - centerOfCircle[0]), 2) + Math.Pow((y - centerOfCircle[1]), 2)), 0.5);

        Console.Write(distance <= radiusOfCircle ? "Inside circle " : "Outside circle ");

        if (y > rectangleTop || y < (rectangleTop - rectangleHeight) || x < rectangleLeft || x > (rectangleLeft + rectangleWidth))
            Console.WriteLine("outside rectangle");
        else
            Console.WriteLine("inside rectangle");
    }
}