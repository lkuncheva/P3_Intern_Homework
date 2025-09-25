class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>
        {
            { ".NET", "Platform for applications from Microsoft" },
            { "CLR", "Managed execution environment for .NET" },
            { "namespace", "Hierarchical organization of classes" }
        };

        Console.WriteLine("Enter word:");
        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");

            return;
        }

        if (dictionary.ContainsKey(input))
        {
            Console.WriteLine(dictionary[input]);
        }
        else
        {
            Console.WriteLine($"Word '{input}' not in dictionary. Do you want to add it? (y/n)");
            char answer;
            while (!char.TryParse(Console.ReadLine(), out answer) || ((answer != 'y') && (answer != 'n')))
            {
                Console.WriteLine("Invalid input. Please enter y or n: ");
            }

            if (answer == 'y')
            {
                Console.WriteLine("Enter definition:");
                string definition = Console.ReadLine();
                if (string.IsNullOrEmpty(definition))
                {
                    Console.WriteLine("Invalid input. The string cannot be null or empty.");

                    return;
                }

                dictionary.Add(input, definition);
                Console.WriteLine("Word added to dictionary successfully!");

                Console.WriteLine("\nUpdated dictionary:\n");
                foreach (KeyValuePair<string, string> pair in dictionary)
                {
                    Console.WriteLine($"Word: {pair.Key} \nDefinition: {pair.Value}\v");
                }
            }
            else
            {
                Console.WriteLine("No changes made to dictionary.");

                return;
            }
        }
    }
}