using System;

class Program
{
    static void Main(string[] args)
    {
        uint n;
        int p, q, k;

        Console.WriteLine("Enter an integer n: ");
        while (!uint.TryParse(Console.ReadLine(), out n))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer n: ");
        }

        Console.WriteLine("Enter a integer p: ");
        while (!int.TryParse(Console.ReadLine(), out p))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer p: ");
        }

        Console.WriteLine("Enter a integer q: ");
        while (!int.TryParse(Console.ReadLine(), out q))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer q: ");
        }

        Console.WriteLine("Enter a integer k: ");
        while(!int.TryParse(Console.ReadLine(), out k))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer k: ");
        }

        uint mask = (uint)(1 << k) - 1;

        uint bitsP = (n >> p) & mask;
        uint bitsQ = (n >> q) & mask;

        n &= ~(mask << p);
        n &= ~(mask << q);

        n |= (bitsP << q);
        n |= (bitsQ << p);

        Console.WriteLine(n);

    }
}