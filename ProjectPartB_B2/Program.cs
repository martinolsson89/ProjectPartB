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
            //myDeck.Sort();
            Console.WriteLine(myDeck);

            Console.WriteLine($"\nA shuffled deck with {myDeck.Count} cards:"); 
            //myDeck.Shuffle();
            Console.WriteLine(myDeck);

            PokerHand Player = new PokerHand();
            while (myDeck.Count > 5)
            {
                //Your code to Give 5 cards to the player and determine the rank
                //Continue for as long as the deck has at least 5 cards 
                Deal(myDeck, Player);
                //var rank = Player.DetermineRank();
                //Console.WriteLine($"Rank is: {rank}");
                Console.WriteLine($"Rank is {Player.DetermineRank()} with rank-high-card {Player.RankHiCard}");
                Console.WriteLine($"Deck now has {myDeck.Count} cards");
                Console.WriteLine();
                

            }
        }
        private static void Deal(DeckOfCards myDeck, PokerHand player)
        {

            player.Clear();

            for (int i = 0; i < 5; i++)
            {
                player.Add(myDeck.RemoveTopCard());

            }
            Console.WriteLine(player);
            
        }
    }
 }
