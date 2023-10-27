using System;
using System.Linq;
using System.Text;

namespace ProjectPartB_B2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Makes symbols show on console.
                Console.OutputEncoding = Encoding.Unicode;

                // Creating an new instance of DeckOfCards named myDeck. 
                DeckOfCards myDeck = new DeckOfCards();


                // Creating new deck of 52 cards and printing out.
                myDeck.CreateFreshDeck();
                Console.WriteLine($"\nA freshly created deck with {myDeck.Count} cards:");
                Console.WriteLine(myDeck);

                // Sorting the deck
                Console.WriteLine($"\nA sorted deck with {myDeck.Count} cards:"); 
                myDeck.Sort();
                Console.WriteLine(myDeck);

                // Shuffling the deck
                Console.WriteLine($"\nA shuffled deck with {myDeck.Count} cards:"); 
                myDeck.Shuffle();
                Console.WriteLine(myDeck);

                //Create a specific poker hand to test the result: 
                //Change myDeck --> testDeck in Deal method. Set you desired poker hand in DeckOfCards --> CreateAPokerHand().

                /*
                DeckOfCards testDeck = new DeckOfCards();
                testDeck.CreateAPokerHand();
                Console.WriteLine($"\nTestDeck:");
                Console.WriteLine(testDeck);
                Console.WriteLine();
                */

                //Your code to Give 5 cards to the player and determine the rank
                //Continue for as long as the deck has at least 5 cards 

                PokerHand Player = new PokerHand();

                while (myDeck.Count > 5)
                {
                    //Calling the deal method.
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
                
                }
            }
            catch (Exception Message)
            {
                Console.WriteLine(Message);
                
            }
        }
        //Deal 5 cards to the player from the deck. 
        private static void Deal(DeckOfCards myDeck, PokerHand player)
        {
            // Clear cards and rand from previous poker hand and rank. 
            player.Clear();

            for (int i = 0; i < 5; i++)
            {
                player.Add(myDeck.RemoveTopCard());

            }
            // Print out the poker hand. 
            Console.WriteLine(player);
            
        }
    }
 }
