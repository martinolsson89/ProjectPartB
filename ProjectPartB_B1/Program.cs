using System;
using System.Linq.Expressions;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectPartB_B1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Makes symbols show on console.
            Console.OutputEncoding = Encoding.Unicode;

            int NrOfCards;
            int NrOfRounds;
            bool _continue = true;
            do
            {
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

                HandOfCards player1 = new HandOfCards();
                HandOfCards player2 = new HandOfCards();

            //Your code to play the game comes here

            
                Console.WriteLine("Let's play a game of highest card with two players."); 
                if (!TryReadNrOfCards(out NrOfCards))
                {
                    _continue = false;
                    break;
                }

                if (!TryReadNrOfRounds(out NrOfRounds))
                {
                    _continue = false;
                    break;
                }

                for (int i = 0; i < NrOfRounds; i++)
                {
                    Console.WriteLine($"\nPlaying round nr {i + 1}\n------------------");
                    Deal(myDeck, NrOfCards, player1, player2);
                    DetermineWinner(player1, player2);
                }

                
                if (!PlayAgain())
                {
                    _continue = false;
                }

            } while (_continue);
        }

        /// <summary>
        /// Asking a user to give how many cards should be given to players.
        /// User enters an integer value between 1 and 5. 
        /// </summary>
        /// <param name="NrOfCards">Number of cards given by user</param>
        /// <returns>true - if value could be read and converted. Otherwise false</returns>
        private static bool TryReadNrOfCards(out int NrOfCards)
        {
            NrOfCards = 0;
            const int minInt = 1;
            const int maxInt = 5;

            string sInput;
            do
            {
                Console.WriteLine("How many cards to deal to each player (1-5  or  Q to quit)?");
                sInput = Console.ReadLine().ToLower();
                if (int.TryParse(sInput, out NrOfCards) && NrOfCards >= minInt && NrOfCards <= maxInt)
                {
                    return true;
                }
                else if (sInput != "q")
                {
                    Console.WriteLine("Wrong input, please try again.");
                }
            }
            while (sInput != "q");

            return false;


        }

        /// <summary>
        /// Asking a user to give how many round should be played.
        /// User enters an integer value between 1 and 5. 
        /// </summary>
        /// <param name="NrOfRounds">Number of rounds given by user</param>
        /// <returns>true - if value could be read and converted. Otherwise false</returns>
        private static bool TryReadNrOfRounds(out int NrOfRounds)
        {
            NrOfRounds = 0;

            const int minInt = 1;
            const int maxInt = 5;

            string sInput;
            do
            {
                Console.WriteLine("How many rounds should we play (1-5  or  Q to quit)?");
                sInput = Console.ReadLine().ToLower();
                if (int.TryParse(sInput, out NrOfRounds) && NrOfRounds >= minInt && NrOfRounds <= maxInt)
                {
                    return true;
                }
                else if (sInput != "q")
                {
                    Console.WriteLine("Wrong input, please try again.");
                }
            }
            while (sInput != "q");

            return false;
        }


        /// <summary>
        /// Removes from myDeck one card at the time and gives it to player1 and player2. 
        /// Repeated until players have recieved nrCardsToPlayer 
        /// </summary>
        /// <param name="myDeck">Deck to remove cards from</param>
        /// <param name="nrCardsToPlayer">Number of cards each player should recieve</param>
        /// <param name="player1">Player 1</param>
        /// <param name="player2">Player 2</param>
        private static void Deal(DeckOfCards myDeck, int nrCardsToPlayer, HandOfCards player1, HandOfCards player2)
        {
            player1.HandCards.Clear();
            player2.HandCards.Clear();

            for (int i = 0; i < nrCardsToPlayer; i++)
            {
                player1.Add(myDeck.RemoveTopCard());
                
            }
            for (int i = 0; i < nrCardsToPlayer; i++)
            {
                player2.Add(myDeck.RemoveTopCard());

            }

            Console.WriteLine($"Gave {nrCardsToPlayer} card each to the players from the top of the deck. Deck now has {myDeck.Count}\n");

            Console.WriteLine($"Player1 hand with {nrCardsToPlayer} cards:");
            Console.WriteLine($"Lowest card in hand is {player1.Lowest} and highest is {player1.Highest}:");

            for (int i = 0; i < nrCardsToPlayer; i++)
            {
                Console.Write($"{player1.HandCards[i].ToString().PadRight(9)}");
                
            }

            Console.WriteLine();
            Console.WriteLine($"\nPlayer2 hand with {nrCardsToPlayer} cards:");
            Console.WriteLine($"Lowest card in hand is {player2.Lowest} and highest is {player2.Highest}:");
            for (int i = 0; i < nrCardsToPlayer; i++)
            {
                Console.Write($"{player2.HandCards[i].ToString().PadRight(9)}");

            }
            
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// Determines and writes to Console the winner of player1 and player2. 
        /// Player with higest card wins. If both cards have equal value it is a tie.
        /// </summary>
        /// <param name="player1">Player 1</param>
        /// <param name="player2">Player 2</param>
        private static void DetermineWinner(HandOfCards player1, HandOfCards player2)
        {
            switch (player1.Highest.CompareTo(player2.Highest))
            {
                case -1:
                    Console.WriteLine("Player2 wins!\n");
                    break;
                case 0:
                    Console.WriteLine("It's a draw.\n");
                    break;
                case 1: 
                    Console.WriteLine("Player1 wins!\n");
                    break;

            }
        }

        public static bool PlayAgain()
        {
            Console.WriteLine($"Do you like to play again (Y = Yes / Q = Quit)?");
            string sInput = Console.ReadLine().ToLower();
            if (!string.IsNullOrEmpty(sInput) && !string.IsNullOrWhiteSpace(sInput) && sInput == "y")
            {
                
                return true;
            }

            return false;
        }
    }
}
