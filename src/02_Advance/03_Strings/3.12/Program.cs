class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter URL:");
        string url = Console.ReadLine();

        if (string.IsNullOrEmpty(url))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");
            return;
        }

        url = url.Trim();

        int protocolEndIndex = url.IndexOf("://");

        if (protocolEndIndex == -1)
        {
            Console.WriteLine("Invalid URL format. Could not find '://'.");
            return;
        }

        string protocol = url.Substring(0, protocolEndIndex);
        int serverEndIndex = url.IndexOf('/', protocolEndIndex + 3);

        string server;
        string resource;
        if (serverEndIndex == -1)
        {
            server = url.Substring(protocolEndIndex + 3);
            resource = "";
        }
        else
        {
            server = url.Substring(protocolEndIndex + 3, serverEndIndex - (protocolEndIndex + 3));
            resource = url.Substring(serverEndIndex);
        }

        Console.WriteLine($"[protocol] = {protocol}");
        Console.WriteLine($"[server] = {server}");
        Console.WriteLine($"[resource] = {resource}");
    }
}