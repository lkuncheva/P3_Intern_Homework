class Program
{
    static void Main(string[] args)
    {
        string readFilePath1 = "../../../file1.txt";
        string readFilePath2 = "../../../file2.txt";
        string outputFilePath = "../../../output_file.txt";

        try
        {
            if (!File.Exists(readFilePath1))
            {
                throw new FileNotFoundException($"Error: The file '{readFilePath1}' was not found.");
            }

            if (!File.Exists(readFilePath2))
            {
                throw new FileNotFoundException($"Error: The file '{readFilePath2}' was not found.");
            }

            string content1 = File.ReadAllText(readFilePath1);
            string content2 = File.ReadAllText(readFilePath2);

            string combinedContent = content1 + Environment.NewLine + content2;

            File.WriteAllText(outputFilePath, combinedContent);

            Console.WriteLine($"Successfully concatenated '{readFilePath1}' and '{readFilePath2}' into '{outputFilePath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}