class Program
{
    static void Main(string[] args)
    {
        string filePath = "../../../file.txt";

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                string line;
                int lineNumber = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    if (lineNumber % 2 != 0)
                    {
                        Console.WriteLine(line);
                    }

                    lineNumber++;
                }
            }
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