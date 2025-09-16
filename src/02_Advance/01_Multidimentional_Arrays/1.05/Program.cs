using System;
class Program
{
    static void Main(string[] args)
    {
        string[] array = { "aaa", "bb", "cccccc", "dddd", "e", "ffffffffff", "gggg" };

        Sort(array);
    }

    static void Sort(string[] array)
    {
        string[] sortedArray = array.OrderBy(s => s.Length).ToArray();

        Console.WriteLine("Sorted array of strings by the length of its elements::");
        foreach (string s in sortedArray)
        {
            Console.WriteLine(s);
        }
    }
}