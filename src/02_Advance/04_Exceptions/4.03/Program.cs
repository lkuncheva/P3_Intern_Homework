class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please enter the full file path (e.g., C:\\WINDOWS\\win.ini):");
        string filePath = Console.ReadLine();

        try
        {
            string fileContent = File.ReadAllText(filePath);
            Console.WriteLine("File content: ");
            Console.WriteLine(fileContent);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: The file was not found!");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Error: The directory doesn't exist!");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: Access to the file is denied. You don't have the required permissions to access this file!");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Error: The file path is not valid. It may be an empty string, contain only whitespace, or have invalid characters.");
        }
        catch (NotSupportedException)
        {
            Console.WriteLine("Error: The file path is in an invalid format.");
        }
        catch (PathTooLongException)
        {
            Console.WriteLine("Error: The specified path, file name, or both are too long.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An I/O error occurred: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}