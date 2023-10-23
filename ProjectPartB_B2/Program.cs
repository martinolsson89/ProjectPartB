using System;
using System.Linq;
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

           /* DeckOfCards testDeck = new DeckOfCards();
            testDeck.CreateAPair();
            Console.WriteLine($"\nTestDeck:");
            Console.WriteLine(testDeck);
            Console.WriteLine();
           */
            PokerHand Player = new PokerHand();
            while (myDeck.Count > 5)
            {
                //Your code to Give 5 cards to the player and determine the rank
                //Continue for as long as the deck has at least 5 cards 
                Deal(myDeck, Player);

                //var rank = Player.DetermineRank();
                //Console.WriteLine($"Rank is: {rank}");
                var currentRank = Player.DetermineRank();
                if (Player.Rank == PokerRank.TwoPair)
                {
                    Console.WriteLine($"Rank is {currentRank} with rank-high-card {Player.RankHiCard}");
                    Console.WriteLine($"First pair rank-high-card {Player.RankHiCardPair1}");
                    Console.WriteLine($"Second pair rank-high-card {Player.RankHiCardPair2}");
                    Console.WriteLine($"Deck now has {myDeck.Count} cards");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Rank is {currentRank} with rank-high-card {Player.RankHiCard}");
                    Console.WriteLine($"Deck now has {myDeck.Count} cards");
                    Console.WriteLine();
                }

                if (myDeck.Count != 2)
                {
                    Console.WriteLine($"\nPress any key to see next hand\n");
                    Console.ReadKey();
                }
                //break;
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
