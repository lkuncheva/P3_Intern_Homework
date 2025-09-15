using System;

class Program
{
    static void Main(string[] args)
    {
        int N;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out N) || ((N < 1) || (N > 200)))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer in range [1, 200]: ");
        }

        double numbers;
        double sum = 0;

        for (int i = 0; i < N; i++)
        {
            Console.WriteLine($"Enter number {i + 1}: ");
            while (!double.TryParse(Console.ReadLine(), out numbers))
            {
                Console.WriteLine("Invalid input. Please enter a valid number: ");
            }

            sum += numbers;
        }

        Console.WriteLine($"The sum of the {N} number(s) is: {sum}");


        //Here I missunderstood the task (N is not the amount of numbers for the input, but a number itself)
        //I will leave the code anyway:)

        /*
        double N;
        List<double> numbers = new List<double>();
        char anotherNumber = 'y';
                
        for(int i = 0; anotherNumber != 'n'; i++)
        {
            Console.WriteLine("Enter number N: ");
            while (!double.TryParse(Console.ReadLine(), out N))
            {
                Console.WriteLine("Invalid input. Please enter a valid number: ");
            }

            numbers.Add(N);

            Console.Write("Do you want to enter another number? (y/n): ");

            while (!char.TryParse(Console.ReadLine().ToLower(), out anotherNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid answer (y or n): ");
            }
        }
    
        Console.WriteLine("The sum of the numbers is: " + numbers.Sum());*/
    }
}