using System;

class Program
{
    static void Main(string[] args)
    {
        double a, b, c;

        Console.WriteLine("Enter a real number a: ");
        while (!double.TryParse(Console.ReadLine(), out a) || (a < -1000 || a > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid real number a: ");
        }

        Console.WriteLine("Enter a real number b: ");
        while (!double.TryParse(Console.ReadLine(), out b) || (b < -1000 || b > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid real number b: ");
        }

        Console.WriteLine("Enter a real number c: ");
        while (!double.TryParse(Console.ReadLine(), out c) || (c < -1000 || c > 1000))
        {
            Console.WriteLine("Invalid input. Please enter a valid real number c: ");
        }



        double discriminant = (b * b) - (4 * a * c);
        
        if (discriminant > 0)
        {
            double root1 = (-b + Math.Sqrt(discriminant)) / (2 * a);

            if (root1 == -0)
                root1 = 0; 

            double root2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

            if (root2 == -0)
                root2 = 0;

            Console.WriteLine("The two real roots are: " + ( root1 < root2 ? $"{root1:F2} and {root2:F2}" : $"{root2:F2} and {root1:F2}" ) );

        }
        else if (discriminant == 0)
        {
            double root = -b / (2 * a);

            if (root == -0) 
                root = 0; 

            Console.WriteLine($"The single real root is {root}.");
        }
        else
            Console.WriteLine("The equation has no real roots.");
        
    }
}