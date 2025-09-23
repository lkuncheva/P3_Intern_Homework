using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "../../../file.txt";

        try
        {
            string fileContent = File.ReadAllText(filePath);

            Console.WriteLine("Original content:\n");
            Console.WriteLine(fileContent);

            Regex regex = new Regex(@"\btest\w*\b", RegexOptions.IgnoreCase);

            string modifiedContent = regex.Replace(fileContent, "");

            File.WriteAllText(filePath, modifiedContent);

            Console.WriteLine("\nModified content:\n");
            Console.WriteLine(modifiedContent);

            Console.WriteLine($"\nSuccessfully processed the file '{filePath}'.");
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