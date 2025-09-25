class Program
{
    static void Main(string[] args)
    {
        string readFilePath1 = "../../../file1.txt";
        string readFilePath2 = "../../../file2.txt";

        int sameLinesCount = 0;
        int differentLinesCount = 0;

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

            using (var reader1 = new StreamReader(readFilePath1))
            using (var reader2 = new StreamReader(readFilePath2))
            {
                string line1;
                string line2;

                while ((line1 = reader1.ReadLine()) != null && (line2 = reader2.ReadLine()) != null)
                {
                    if (string.Equals(line1, line2, StringComparison.Ordinal))
                    {
                        sameLinesCount++;
                    }
                    else
                    {
                        differentLinesCount++;
                    }
                }
            }

            Console.WriteLine($"Number of same lines: {sameLinesCount}");
            Console.WriteLine($"Number of different lines: {differentLinesCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}