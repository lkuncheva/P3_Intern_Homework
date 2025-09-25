using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
class Program
{
    static void Main(string[] args)
    {
        string string1;
        string string2;

        Console.WriteLine("Enter first char array: ");
        while (true)
        {
            string1 = Console.ReadLine();

            if (string.IsNullOrEmpty(string1) || (string1.Length < 1) || (string1.Length > 128))
            {
                Console.WriteLine("Invalid input. Please enter a valid array with size in range [1, 128].");
            }
            else
            {
                break;
            }
        }

        Console.WriteLine("Enter second char array: ");
        while (true)
        {
            string2 = Console.ReadLine();

            if (string.IsNullOrEmpty(string2) || (string2.Length < 1) || (string2.Length > 128))
            {
                Console.WriteLine("Invalid input. Please enter a valid array with size in range [1, 128].");
            }
            else
            {
                break;
            }
        }

        //Easier way to do this withot converting the strings to char arrays
        /*
        if (string.Compare(string1, string2, CompareOptions.IgnoreCase) < 0)
            Console.WriteLine("<");
        else if (string.Compare(string1, string2, CompareOptions.IgnoreCase) > 0)
            Console.WriteLine(">");
        else
            Console.WriteLine("=");
        */

        char[] charArray1 = string1.ToCharArray();
        char[] charArray2 = string2.ToCharArray();

        int minLength = Math.Min(charArray1.Length, charArray2.Length);

        for (int i = 0; i < minLength; i++)
        {
            if (charArray1[i] < charArray2[i])
            {
                Console.WriteLine("<");

                return;
            }

            if (charArray1[i] > charArray2[i])
            {
                Console.WriteLine(">");

                return;
            }
        }

        //If the arrays are different sizess

        if (charArray1.Length < charArray2.Length)
        {
            Console.WriteLine("<");
        }
        else if (charArray1.Length > charArray2.Length)
        {
            Console.WriteLine(">");
        }
        else
        {
            Console.WriteLine("=");
        }
    }
}