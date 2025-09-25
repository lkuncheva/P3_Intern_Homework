class Program
{
    static void Main(string[] args)
    {
        int n;

        Console.WriteLine("Enter number N: ");
        while (!int.TryParse(Console.ReadLine(), out n) || (n < 2) || (n > 10000000))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer in range [2, 10 000 000]: ");
        }

        bool[] primeCheck = new bool[n + 1];
        for (int i = 0; i <= n; i++)
        {
            primeCheck[i] = true;
        }

        for (int p = 2; p * p <= n; p++)
        {
            if (primeCheck[p])
            {
                for (int i = p * p; i <= n; i += p)
                {
                    primeCheck[i] = false;
                }
            }
        }

        List<int> primeNumbers = new List<int>();
        for (int i = 2; i <= n; i++)
        {
            if (primeCheck[i])
            {
                primeNumbers.Add(i);
            }
        }

        Console.Write(primeNumbers[primeNumbers.Count - 1]);
    }
}