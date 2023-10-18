using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPartB_B2
{
    class HandOfCards : DeckOfCards, IHandOfCards
    {
        #region Pick and Add related
        public List<PlayingCard> HandCards { get; set; } = new List<PlayingCard>();

        protected PlayingCard this[int idx]
        {
            get
            {
                if (idx >= 0 && idx < HandCards.Count)
                {
                    return HandCards[idx];
                }
                else
                {
                    // Handle out-of-range indices gracefully
                    return null;
                }
            }
        }
        public virtual void Add(PlayingCard card)
        {
            HandCards.Add(card);
        }
        #endregion
        #region ToString() related
        public override string ToString()
        {
            string str = "Player hand: ";

            for (int i = 0; i < HandCards.Count; i++)
            {
                str += $"{HandCards[i].ToString().PadRight(9)}";
            }
            return str;
        }
        #endregion
        #region Highest Card related
        public PlayingCard Highest
        {
            get
            {
                int _hiVal = int.MinValue;
                PlayingCard _hiCard = null;

                foreach (var card in HandCards)
                {
                    if ((int)card.Value > _hiVal)
                    {
                        _hiCard = card;
                        _hiVal = (int)card.Value;
                    }
                }

                return _hiCard;
            }
         }
        public PlayingCard Lowest
        {
            get
            {
                int _lowVal = int.MaxValue;
                PlayingCard _lowCard = null;

                foreach (var card in HandCards)
                {
                    if ((int)card.Value < _lowVal)
                    {
                        _lowCard = card;
                        _lowVal = (int)card.Value;
                    }
                }

                return _lowCard;
            }
        }
        #endregion

        public int CountSameCards(PlayingCard cardToCheck)
        {
            int count = HandCards.Count(card => card.CompareTo(cardToCheck) == 0);
            return count;
        }
    }
}
