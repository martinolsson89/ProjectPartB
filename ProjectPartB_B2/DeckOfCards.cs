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

        public PlayingCard this[int idx]
        {
            get
            {
                if (idx >= 0 && idx < cards.Count)
                {
                    return cards[idx];
                }
                else
                {
                    // Handle out-of-range indices gracefully
                    return null;
                }
            }
        }

        public int Count => cards.Count;
        #endregion

        #region ToString() related
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

        public void CreateFreshDeck()
        {
            // Create a fresh deck with 52 cards
            foreach (PlayingCardColor color in Enum.GetValues(typeof(PlayingCardColor)))
            {
                foreach (PlayingCardValue value in Enum.GetValues(typeof(PlayingCardValue)))
                {
                    cards.Add(new PlayingCard(color, value));

                }
            }
        }
        #endregion

        #region Dealing
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
