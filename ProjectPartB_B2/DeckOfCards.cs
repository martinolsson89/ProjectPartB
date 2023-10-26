using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPartB_B2
{
    class DeckOfCards : IDeckOfCards
    {
        #region cards List related
        protected const int MaxNrOfCards = 52;
        protected List<PlayingCard> cards = new List<PlayingCard>(MaxNrOfCards);

        // This is an indexer for accessing elements in the list.
        public PlayingCard this[int idx]
        {
            get
            {
                // Check if the provided index is within the valid range.
                if (idx >= 0 && idx < cards.Count)
                {
                    return cards[idx];
                }
                else
                {
                    // Throw an exception if you try to get an index outside the range of cards.
                    throw new Exception("Error: Index out of range");
                }
            }
        }

        //Keeping count of the deck. 
        public int Count => cards.Count;
        #endregion

        #region ToString() related

        // Print out the cards in the deck. 
        public override string ToString()
        {
            string str = "";
            int lineBreak = 1;
            for (int i = 0; i < cards.Count; i++)
            {
                str += $"{cards[i].ToString().PadRight(9)}";
                if (lineBreak % 13 == 0)
                {
                    str += $"\n";
                }
                lineBreak++;
            }
            return str;
        }
        #endregion

        #region Shuffle and Sorting

        // Using the Fisher-Yates shuffle algorithm.
        public void Shuffle()
        {
            var rnd = new Random();
            int i = cards.Count;
            while (i > 1)
            {
                i--;
                int r = rnd.Next(i + 1);
                PlayingCard value = cards[r];
                cards[r] = cards[i];
                cards[i] = value;
            }
        }

        // Sort the cards in the deck.
        public void Sort()
        {
            cards.Sort();
        }
        #endregion

        #region Creating a fresh Deck

        public virtual void Clear()
        {
            cards.Clear();
        }

        // Create a fresh deck with 52 cards
        public void CreateFreshDeck()
        {
            //Going trough each color suit. 
            foreach (PlayingCardColor color in Enum.GetValues(typeof(PlayingCardColor)))
            {
                // For each color suit go trough each value from two - ace. 
                foreach (PlayingCardValue value in Enum.GetValues(typeof(PlayingCardValue)))
                {
                    // Add new Playing card with color och value to cards list.
                    cards.Add(new PlayingCard(color, value));

                }
            }
        }
        #endregion

        //Create your own poker hand. 
        public void CreateAPokerHand()
        {
            cards.Add(new PlayingCard(PlayingCardColor.Clubs, PlayingCardValue.Knight));
            cards.Add(new PlayingCard(PlayingCardColor.Clubs, PlayingCardValue.Ace));
            cards.Add(new PlayingCard(PlayingCardColor.Clubs, PlayingCardValue.Ten));
            cards.Add(new PlayingCard(PlayingCardColor.Clubs, PlayingCardValue.King));
            cards.Add(new PlayingCard(PlayingCardColor.Clubs, PlayingCardValue.Queen));

        }
        

        #region Dealing

        //Removing card at the top of the deck en return it. 
        public PlayingCard RemoveTopCard()
        {
            if (Count > 0)
            {
                PlayingCard topCard = cards[0];
                cards.RemoveAt(0);
                return topCard;
            }
            return null;
        }
        #endregion
    }
}
