using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPartB_B1
{
    class HandOfCards : DeckOfCards, IHandOfCards
    {
        #region Pick and Add related
        // Add cards to the poker hand and sort. 
        public void Add(PlayingCard card)
        {
            cards.Add(card);

            Sort();
        }

        #endregion
        #region Highest Card related
        // Finding highest and lowest cards of each poker hand.
        public PlayingCard Highest
        {
            get
            {
                int _hiVal = int.MinValue;
                PlayingCard _hiCard= null;

                foreach (var card in cards)
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

                foreach (var card in cards)
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
    }
        #endregion
}
