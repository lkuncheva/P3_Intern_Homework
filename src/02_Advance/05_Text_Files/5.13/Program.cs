class Program
{
    static void Main(string[] args)
    {
        string textFilePath = "../../../test.txt";
        string wordsFilePath = "../../../words.txt";
        string outputFilePath = "../../../result.txt";

        Console.WriteLine("Original content in test.txt:");
        Console.WriteLine("---------------------------------");
        DisplayFileContent(textFilePath);

        Console.WriteLine("\nWords to be counted from words.txt:");
        Console.WriteLine("---------------------------------------------");
        DisplayFileContent(wordsFilePath);

        CountWords(textFilePath, wordsFilePath, outputFilePath);

        Console.WriteLine("\nProcessing complete. New content in result.txt:");
        Console.WriteLine("----------------------------------------------");
        DisplayFileContent(outputFilePath);
    }

    private static void CountWords(string textPath, string wordsPath, string outputPath)
    {
        try
        {
            if (!File.Exists(textPath))
            {
                throw new FileNotFoundException($"Error: The file '{textPath}' was not found.");
            }

            if (!File.Exists(wordsPath))
            {
                throw new FileNotFoundException($"Error: The file '{wordsPath}' was not found.");
            }

            var wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var wordsToFind = File.ReadAllLines(wordsPath);

            foreach (var word in wordsToFind)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    wordCounts[word.Trim().ToLowerInvariant()] = 0;
                }
            }

            string textContent = File.ReadAllText(textPath).ToLowerInvariant();

            var delimiters = new List<char>();
            foreach (char c in textContent)
            {
                if (!char.IsLetterOrDigit(c) && c != '_' && !delimiters.Contains(c))
                {
                    delimiters.Add(c);
                }
            }

            char[] delimiterArray = delimiters.ToArray();

            string[] words = textContent.Split(delimiterArray, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                if (wordCounts.ContainsKey(word))
                {
                    wordCounts[word]++;
                }
            }

            var sorted = wordCounts.OrderByDescending(kv => kv.Value);

            using (var writer = new StreamWriter(outputPath))
            {
                foreach (var pair in sorted)
                {
                    writer.WriteLine($"{pair.Key}: {pair.Value}");
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error: An I/O error occurred while accessing the files. {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Error: You do not have the necessary permissions to access these files. {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    private static void DisplayFileContent(string filePath)
    {
        try
        {
            Console.WriteLine(File.ReadAllText(filePath));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The file '{filePath}' was not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while displaying '{filePath}': {ex.Message}");
        }
    }
}