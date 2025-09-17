class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter text: ");
        string text = Console.ReadLine();

        if (string.IsNullOrEmpty(text))
        {
            Console.WriteLine("Invalid input. String is null or empty.");
            return;
        }

        string openingTag = "<upcase>";
        string closingTag = "</upcase>";

        while (true)
        {
            int startIndex = text.IndexOf(openingTag);

            if (startIndex == -1)
            {
                break;
            }

            int endIndex = text.IndexOf(closingTag, startIndex);

            if (endIndex == -1)
            {
                Console.WriteLine("Invalid input.");
                break;
            }

            int contentStart = startIndex + openingTag.Length;
            int contentLength = endIndex - contentStart;
            string content = text.Substring(contentStart, contentLength);

            string textToReplace = openingTag + content + closingTag;

            text = text.Replace(textToReplace, content.ToUpper());
        }

        Console.WriteLine(text);
    }
}