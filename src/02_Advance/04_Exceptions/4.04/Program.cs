using System.Net;

public class Program
{
    public static async Task Main(string[] args)
    {
        using HttpClient client = new HttpClient();

        string remoteUri = "https://jollycontrarian.com/images/6/6c/Rickroll.jpg";
        string fileName = "hihi.jpg";
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        Console.WriteLine($"Attempting to download '{remoteUri}'...");
        Console.WriteLine($"Saving to: {filePath}\n");

        try
        {
            using HttpResponseMessage response = await client.GetAsync(remoteUri);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Download failed. HTTP Status Code: {(int)response.StatusCode} - {response.ReasonPhrase}");
                return;
            }

            using (Stream contentStream = await response.Content.ReadAsStreamAsync())
            {
                using (FileStream fileStream = File.Create(filePath))
                {
                    await contentStream.CopyToAsync(fileStream);
                    Console.WriteLine("File downloaded successfully!");
                }
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Download failed due to a network error: {ex.Message}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Error: Permission denied. The program could not save the file to '{filePath}'.");
            Console.WriteLine("Please check the folder permissions.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An I/O error occurred: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("\nTask complete.");
        }
    }
}