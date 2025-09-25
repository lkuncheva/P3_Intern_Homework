class Program
{
    static void Main(string[] args)
    {
        string filePath = "../../../file.txt";
        string outputFilePath = "../../../output_file.txt";

        try
        {
            using (var reader = new StreamReader(filePath))
            using (var writer = new StreamWriter(outputFilePath))
            {
                string line;
                int lineNumber = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    writer.WriteLine($"{lineNumber}. {line}");
                    lineNumber++;
                }
            }

            Console.WriteLine($"Successfully added line numbers from '{filePath}' to '{outputFilePath}'.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The file '{filePath}' was not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}