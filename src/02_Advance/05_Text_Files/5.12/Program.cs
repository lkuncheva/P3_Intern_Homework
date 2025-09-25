using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string textFilePath = "../../../text.txt";
        string wordsFilePath = "../../../words.txt";
        string outputFilePath = "../../../output_file.txt";

        Console.WriteLine("Original content in text.txt:");
        Console.WriteLine("---------------------------------");
        DisplayFileContent(textFilePath);

        Console.WriteLine("\nWords to be removed from words.txt:");
        Console.WriteLine("---------------------------------------------");
        DisplayFileContent(wordsFilePath);
        
        RemoveWords(textFilePath, wordsFilePath, outputFilePath);

        Console.WriteLine("\nProcessing complete. New content in output_file.txt:");
        Console.WriteLine("----------------------------------------------");
        DisplayFileContent(outputFilePath);
    }

    private static void RemoveWords(string textPath, string wordsPath, string outputPath)
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

            var wordsToRemove = new List<string>();
            using (var reader = new StreamReader(wordsPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    wordsToRemove.Add(line.Trim().ToLowerInvariant());
                }
            }

            string textContent = File.ReadAllText(textPath);

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

            var modifiedContentBuilder = new StringBuilder();
            int lastIndex = 0;

            foreach (var word in words)
            {
                int wordStartIndex = textContent.IndexOf(word, lastIndex, StringComparison.Ordinal);

                if (wordStartIndex > lastIndex)
                {
                    modifiedContentBuilder.Append(textContent.Substring(lastIndex, wordStartIndex - lastIndex));
                }

                if (!wordsToRemove.Contains(word.ToLowerInvariant()))
                {
                    modifiedContentBuilder.Append(word);
                }

                lastIndex = wordStartIndex + word.Length;
            }

            modifiedContentBuilder.Append(textContent.Substring(lastIndex));

            File.WriteAllText(outputPath, modifiedContentBuilder.ToString());
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