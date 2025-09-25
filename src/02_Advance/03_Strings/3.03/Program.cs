class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a string: ");
        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Invalid input. String is null or empty.");

            return;
        }

        bool[] isValid = new bool[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            if (!isValid[i] && input[i] == ')')
            {
                Console.WriteLine("The expression is not valid. There is a closing bracket without an opening one.");

                return;
            }

            if (input[i] == '(')
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (input[j] == ')' && !isValid[j])
                    {
                        isValid[j] = true;
                        break;
                    }
                }

                Console.WriteLine("The expression is not valid. There is an opening bracket without a closing one.");

                return;
            }
        }

        Console.WriteLine("The expression is valid");
    }
}