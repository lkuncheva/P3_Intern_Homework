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

        const string HREF_START = "<a href=\"";
        const string LINK_TEXT_START = "\">";
        const string LINK_END = "</a>";

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
            string currentHtml = output.ToString();

            int tagStartIndex = currentHtml.IndexOf(HREF_START);
            if (tagStartIndex == -1) break;

            int urlEndIndex = currentHtml.IndexOf(LINK_TEXT_START, tagStartIndex);
            if (urlEndIndex == -1) break;

            int textEndIndex = currentHtml.IndexOf(LINK_END, urlEndIndex);
            if (textEndIndex == -1) break;

            int urlStart = tagStartIndex + HREF_START.Length;
            string url = currentHtml.Substring(urlStart, urlEndIndex - urlStart);

            int linkTextStart = urlEndIndex + LINK_TEXT_START.Length;
            string linkText = currentHtml.Substring(linkTextStart, textEndIndex - linkTextStart);

            string newTag = $"[URL={url}]{linkText}[/URL]";

            int fragmentLength = textEndIndex + LINK_END.Length - tagStartIndex;

            output.Remove(tagStartIndex, fragmentLength);
            output.Insert(tagStartIndex, newTag);
        }

        Console.WriteLine($"\n{output.ToString()}");
    }
}