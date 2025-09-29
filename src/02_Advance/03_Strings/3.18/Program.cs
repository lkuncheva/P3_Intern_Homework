class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter text:");
        string text = Console.ReadLine();

        //Example input:
        //Ala bala nitsa info@example.com turska panitsa john.doe123@webmail.co.uk oi gidi vancho @no.user nash kalpazancho domain@.com

        if (string.IsNullOrEmpty(text))
        {
            Console.WriteLine("Invalid input. The string cannot be null or empty.");
            return;
        }

        // Implementation using regular expressions:
        /*
        const string emailPattern = @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b";

        MatchCollection matches = Regex.Matches(text, emailPattern);

        List<string> emailList = new List<string>();

        foreach (Match match in matches)
        {
            emailList.Add(match.Value);
        }

        if (emailList.Count > 0)
        {
            foreach (string email in emailList)
            {
                Console.WriteLine(email);
            }
        }
        */

        // Without regular expressions:
        string[] textParts = text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine("\nValid Emails:");

        foreach (string part in textParts)
        {
            if (IsValidEmailStructure(part))
            {
                Console.WriteLine(part);
            }
        }
    }

    static bool IsValidEmailStructure(string part)
    {
        int atCount = part.Count(c => c == '@');
        if (atCount != 1)
        {
            return false;
        }

        int atIndex = part.IndexOf('@');
        string localPart = part.Substring(0, atIndex);
        string domainPart = part.Substring(atIndex + 1);

        if (string.IsNullOrEmpty(localPart) || string.IsNullOrEmpty(domainPart))
        {
            return false;
        }

        if (!domainPart.Contains('.'))
        {
            return false;
        }

        if (domainPart.StartsWith('.') || domainPart.EndsWith('.'))
        {
            return false;
        }

        if (part.Contains(".."))
        {
            return false;
        }

        int lastDotIndex = domainPart.LastIndexOf('.');
        string tld = domainPart.Substring(lastDotIndex + 1);

        if (tld.Length < 2)
        {
            return false;
        }

        return true;
    }
}