using System.Text;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static string EncryptDecrypt(string text, string key)
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            char textChar = text[i];
            char keyChar = key[i % key.Length];
            char encryptedChar = (char)(textChar ^ keyChar);
            result.Append(encryptedChar);
        }

        return result.ToString();
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a string:");
        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Invalid input. String is null or empty.");
            return;
        }

        Console.WriteLine("Enter a key: ");
        string key = Console.ReadLine();

        if (string.IsNullOrEmpty(key))
        {
            Console.WriteLine("Invalid input. Key is null or empty.");
            return;
        }

        string encodedText = EncryptDecrypt(input, key);
        Console.WriteLine("Encoded string: " + encodedText);

        string decodedText = EncryptDecrypt(encodedText, key);
        Console.WriteLine("Decoded string: " + decodedText);
    }
}