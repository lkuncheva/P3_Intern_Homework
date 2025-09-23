using System;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "../../../file.xml";

        try
        {
            string xmlContent = File.ReadAllText(filePath);

            Console.WriteLine("Original XML Content:\n");
            Console.WriteLine(xmlContent);

            Console.WriteLine("\nExtracted Text:\n");

            var resultBuilder = new StringBuilder();
            bool inTag = false;

            foreach (char c in xmlContent)
            {
                if (c == '<')
                {
                    inTag = true;
                }
                else if (c == '>')
                {
                    inTag = false;
                }
                else if (!inTag)
                {
                    resultBuilder.Append(c);
                }
            }

            Console.WriteLine(resultBuilder.ToString());
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