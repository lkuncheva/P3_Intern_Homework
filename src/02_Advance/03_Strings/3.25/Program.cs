class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter text:");
        string htmlDocument = Console.ReadLine();

        if (string.IsNullOrEmpty(htmlDocument))
        {
            Console.WriteLine("Invalid input. The input cannot be null or empty.");

            return;
        }

        string title = "Title not found";
        string body = "Body not found";

        int startTitle = htmlDocument.IndexOf("<title>", StringComparison.OrdinalIgnoreCase);
        if (startTitle != -1)
        {
            startTitle += "<title>".Length;
            int endTitle = htmlDocument.IndexOf("</title>", startTitle, StringComparison.OrdinalIgnoreCase);

            if (endTitle != -1)
            {
                title = htmlDocument.Substring(startTitle, endTitle - startTitle).Trim();
            }
        }

        int startBody = htmlDocument.IndexOf("<body>", StringComparison.OrdinalIgnoreCase);
        if (startBody != -1)
        {
            startBody += "<body>".Length;
            int endBody = htmlDocument.IndexOf("</body>", startBody, StringComparison.OrdinalIgnoreCase);
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

                body = bodyContent;
            }
        }

        Console.WriteLine($"\nTitle: {title}");
        Console.WriteLine($"\nBody: {body}");
    }
}