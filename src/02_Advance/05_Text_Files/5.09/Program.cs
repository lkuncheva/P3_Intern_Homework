using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "../../../file.txt";
        var evenLinesContent = new StringBuilder();

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                int lineNumber = 1;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (lineNumber % 2 == 0)
                    {
                        evenLinesContent.AppendLine(line);
                    }

                    lineNumber++;
                }
            }

            File.WriteAllText(filePath, evenLinesContent.ToString());
            Console.WriteLine("File successfully updated. Odd lines removed.");
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