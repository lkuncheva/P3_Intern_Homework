public class Program
{
    public static void Main(string[] args)
    {
        string inputFilePath = "../../../file.txt";
        string outputFilePath = "../../../sorted.txt";

        try
        {
            string[] lines = File.ReadAllLines(inputFilePath);

            Array.Sort(lines);

            File.WriteAllLines(outputFilePath, lines);

            Console.WriteLine("Sorting complete. The sorted file has been saved.");
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