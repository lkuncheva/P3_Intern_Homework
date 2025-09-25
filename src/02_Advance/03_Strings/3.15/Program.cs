using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter text:");
        string htmlDocument = Console.ReadLine();

        if (string.IsNullOrEmpty(htmlDocument))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");

            return;
        }

        // Implementation using regular expressions:
        /*
        string pattern = "<a href=\"(?<url>.*?)\">(?<text>.*?)</a>";
        string replacement = "[URL=${url}]${text}[/URL]";

        string outputText = Regex.Replace(htmlDocument, pattern, replacement);

        Console.WriteLine($"\n{output}");
        */

        // Implementation without regular expressions:
        StringBuilder output = new StringBuilder(htmlDocument);

        while (true)
        {
            int tagStartIndex = output.ToString().IndexOf("<a href=\"");
            if (tagStartIndex == -1) break;

            int urlEndIndex = output.ToString().IndexOf("\">", tagStartIndex);
            if (urlEndIndex == -1) break;

            int textEndIndex = output.ToString().IndexOf("</a>", urlEndIndex);
            if (textEndIndex == -1) break;

            string url = output.ToString().Substring(tagStartIndex + "<a href=\"".Length, urlEndIndex - (tagStartIndex + "<a href=\"".Length));
            string linkText = output.ToString().Substring(urlEndIndex + "\">".Length, textEndIndex - (urlEndIndex + "\">".Length));

            string newTag = $"[URL={url}]{linkText}[/URL]";

            output.Remove(tagStartIndex, textEndIndex + "</a>".Length - tagStartIndex);
            output.Insert(tagStartIndex, newTag);
        }

        Console.WriteLine($"\n{output.ToString()}");
    }
}