using System;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

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
        string[] textParts = text.Split(' ');

        foreach (string part in textParts)
        {
            if (part.Contains('@') && part.IndexOf('@') > 0)
            {
                string hostAndDomain = part.Substring(part.IndexOf('@') + 1);

                if (hostAndDomain.Contains('.') && hostAndDomain.IndexOf('.') > 0)
                {
                    Console.WriteLine(part);
                }
            }
        }
    }
}
