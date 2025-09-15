using System;

class Program
{
    static void Main(string[] args)
    {
        string[] allCards = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        string input;
        bool isCard = false;
        
        Console.WriteLine("Enter a card: ");
        while (string.IsNullOrWhiteSpace(input = Console.ReadLine()) || input.Length < 1 || input.Length > 5)
        {
            Console.WriteLine("Invalid input. Please enter a string between 1 and 5 characters long: ");
        }

        for (int i = 0; i < allCards.Length; i++)
        {
            if (input == allCards[i])
            {
                isCard = true;
                break;
            }               
        }

        Console.WriteLine(isCard? $"yes {input}" : $"no {input}");   
    }
}