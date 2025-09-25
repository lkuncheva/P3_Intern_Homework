using System.Net;

public class Program
{
    public static void Main(string[] args)
    {
        string remoteUri = "https://jollycontrarian.com/images/6/6c/Rickroll.jpg";

        string fileName = "hihi.jpg";
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        Console.WriteLine($"Attempting to download '{remoteUri}'...");
        Console.WriteLine($"Saving to: {filePath}\n");

        WebClient webClient = null;

        try
        {
            webClient = new WebClient();
            webClient.DownloadFile(remoteUri, filePath);

            Console.WriteLine("File downloaded successfully!");
        }
        catch (WebException ex)
        {
            Console.WriteLine($"Download failed due to a network error: {ex.Message}");
            if (ex.Status == WebExceptionStatus.ProtocolError)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                Console.WriteLine($"HTTP Status Code: {(int)response.StatusCode} - {response.StatusCode}");
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Error: Permission denied. The program could not save the file to '{filePath}'.");
            Console.WriteLine("Please check the folder permissions or run the program as an administrator.");
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
            if (webClient != null)
            {
                webClient.Dispose();
                Console.WriteLine("\nResources have been released.");
            }
        }
    }
}