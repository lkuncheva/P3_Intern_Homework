using System;

class Program
{
    static void Main(string[] args)
    {
        const float MoonG = 0.17f;
        Console.WriteLine("Enter weight: ");
        string input = Console.ReadLine();
         
        if (float.TryParse(input, out float weight))
        {
            if (weight > 0 && weight < 1000)
            {
                float weightOnMoon = weight * MoonG; 
                Console.WriteLine($"Weight on moon: {weightOnMoon:F3}"); 
            }
            else
                Console.WriteLine("The weight you entered is invalid. Range is (0, 1000).");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a floating-point number in the renge (0, 1000).");
        }
    }

}
