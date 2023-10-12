using System;
using System.Text;

namespace ProjectPartB_B2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Makes symbols show on console.
            Console.OutputEncoding = Encoding.Unicode;

            DeckOfCards myDeck = new DeckOfCards();
            myDeck.CreateFreshDeck();
            Console.WriteLine($"\nA freshly created deck with {myDeck.Count} cards:");
            Console.WriteLine(myDeck);

            Console.WriteLine($"\nA sorted deck with {myDeck.Count} cards:");
            myDeck.Sort();
            Console.WriteLine(myDeck);

            Console.WriteLine($"\nA shuffled deck with {myDeck.Count} cards:");
            myDeck.Shuffle();
            Console.WriteLine(myDeck);

            PokerHand Player = new PokerHand();
            while (myDeck.Count > 5)
            {
                //Your code to Give 5 cards to the player and determine the rank
                //Continue for as long as the deck has at least 5 cards 
                Deal(myDeck, Player);
                Player.DetermineRank();



            }
        }
        private static void Deal(DeckOfCards myDeck, PokerHand player)
        {

            player.Clear();
            string sRes = $"Player hand: ";

            for (int i = 0; i < 5; i++)
            {
                player.Add(myDeck.RemoveTopCard());

            }
            for (int i = 0; i < 5; i++)
            {
               sRes += $"{player.HandCards[i].ToString().PadRight(9)}";

            }

            Console.WriteLine(sRes);
            Console.WriteLine($"Deck now has {myDeck.Count} cards");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
 }
