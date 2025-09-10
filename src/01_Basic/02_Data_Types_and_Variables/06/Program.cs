using System;

class Program
{
    static void Main(string[] args)
    {
        string h = "Hello";
        string w = "World";

        object obj = h + " " + w;
        string objString = obj.ToString();

        Console.WriteLine(objString);
    }
}