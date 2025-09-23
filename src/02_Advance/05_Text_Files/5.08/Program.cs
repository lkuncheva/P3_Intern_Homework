using System;
using System.IO;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

public class Program
{
    public static void Main(string[] args)
    {
        string inputFilePath = "../../../file.txt";
        string outputFilePath = "../../../output.txt";

        try
        {
            string content = File.ReadAllText(inputFilePath);

            string pattern = $@"\b{Regex.Escape("start")}\b";

            string modifiedContent = Regex.Replace(content, pattern, "finish", RegexOptions.IgnoreCase);

            File.WriteAllText(outputFilePath, modifiedContent);

            Console.WriteLine("All occurrences of the word start have been replaced with the word finish in the output.txt file.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The file '{inputFilePath}' was not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}