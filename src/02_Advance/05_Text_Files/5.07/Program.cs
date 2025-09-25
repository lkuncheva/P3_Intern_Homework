public class Program
{
    public static void Main(string[] args)
    {
        string inputFilePath = "../../../file.txt";
        string outputFilePath = "../../../output.txt";

        try
        {
            string content = File.ReadAllText(inputFilePath);

            File.WriteAllText(outputFilePath, content.Replace("start", "finish"));

            Console.WriteLine("All occurrences of the sub-string start have been replaced with the sub-string finish in the output.txt file.");
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