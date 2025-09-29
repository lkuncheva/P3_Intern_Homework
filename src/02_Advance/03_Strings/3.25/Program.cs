class Program
{
    static void Main(string[] args)
    {
        const string TITLE_OPEN_TAG = "<title>";
        const string TITLE_CLOSE_TAG = "</title>";
        const string BODY_OPEN_TAG = "<body>";
        const string BODY_CLOSE_TAG = "</body>";

        const string FALLBACK_TITLE = "Title not found";
        const string FALLBACK_BODY = "Body not found";

        string title = FALLBACK_TITLE;
        string body = FALLBACK_BODY;

        Console.WriteLine("Enter text:");
        string htmlDocument = Console.ReadLine();

        if (string.IsNullOrEmpty(htmlDocument))
        {
            Console.WriteLine("Invalid input. The input cannot be null or empty.");

            return;
        }

        int startTitle = htmlDocument.IndexOf(TITLE_OPEN_TAG, StringComparison.OrdinalIgnoreCase);
        if (startTitle != -1)
        {
            startTitle += TITLE_OPEN_TAG.Length;
            int endTitle = htmlDocument.IndexOf(TITLE_CLOSE_TAG, startTitle, StringComparison.OrdinalIgnoreCase);

            if (endTitle != -1)
            {
                title = htmlDocument.Substring(startTitle, endTitle - startTitle).Trim();
            }
        }

        int startBody = htmlDocument.IndexOf(BODY_OPEN_TAG, StringComparison.OrdinalIgnoreCase);
        if (startBody != -1)
        {
            startBody += BODY_OPEN_TAG.Length;
            int endBody = htmlDocument.IndexOf(BODY_CLOSE_TAG, startBody, StringComparison.OrdinalIgnoreCase);
            if (endBody != -1)
            {
                string bodyContent = htmlDocument.Substring(startBody, endBody - startBody);

                while (bodyContent.Contains("<") && bodyContent.Contains(">"))
                {
                    int startTag = bodyContent.IndexOf("<");
                    int endTag = bodyContent.IndexOf(">");

                    if (startTag != -1 && endTag != -1)
                    {
                        bodyContent = bodyContent.Remove(startTag, endTag - startTag + 1).Insert(startTag, " ");
                    }
                    else
                    {
                        break;
                    }
                }

                string[] parts = bodyContent.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                body = string.Join(" ", parts).Trim();
            }
        }

        Console.WriteLine($"\nTitle: {title}");
        Console.WriteLine($"\nBody: {body}");
    }
}