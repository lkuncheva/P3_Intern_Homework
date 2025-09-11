using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main(string[] args)
    {
        string[] allCards = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        Console.WriteLine("Enter a card: ");
        char inputCard = char.Parse(Console.ReadLine());


        for (int i = 0; i < allCards.Length; i++)
        {
            Console.WriteLine($"{allCards[i]} of spades, " +
                              $"{allCards[i]} of clubs, " +
                              $"{allCards[i]} of hearts, " +
                              $"{allCards[i]} of diamonds");

            if (allCards[i].Equals(inputCard.ToString()))
                break;

        }
    }
}