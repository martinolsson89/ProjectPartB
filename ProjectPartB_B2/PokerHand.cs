using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectPartB_B2
{
    class PokerHand : HandOfCards, IPokerHand
    {
        #region Clear

        // Overriding clear and to implement it for PokerHand.
        public override void Clear()
        {
            cards.Clear();

            ClearRank();
        }

        #endregion

        #region Remove and Add related

        public override void Add(PlayingCard card)
        {
            cards.Add(card);

            Sort();
        }

        #endregion

        #region Poker Rank related

        //https://www.poker.org/poker-hands-ranking-chart/

        //Hint: using backing fields
        private PokerRank _rank = PokerRank.Unknown;
        private PlayingCard _rankHigh = null;
        private PlayingCard _rankHighPair1 = null;
        private PlayingCard _rankHighPair2 = null;

        // Used in method NrSameValue to store cards temporary.
        protected List<PlayingCard> tempCards = new List<PlayingCard>();

        public PokerRank Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public PlayingCard RankHiCard
        {
            get { return _rankHigh; }
            set { _rankHigh = value; }

        }

        public PlayingCard RankHiCardPair1
        {
            get { return _rankHighPair1; }
            set { _rankHighPair1 = value; }
        }

        public PlayingCard RankHiCardPair2
        {
            get { return _rankHighPair2; }
            set { _rankHighPair2 = value; }
        }

        //Hint: Worker Methods to examine a sorted hand
        
        // This method counts the number of cards with the same value and returns the count.
        // It also sets the 'HighCard' to the highest card with a matching value.
        private int NrSameValue(out PlayingCard HighCard)
        {
            tempCards.Clear(); // Clear a temporary list of cards.
            int firstValueIdx = 0;
            HighCard = null;    // Initialize HighCard to null.

            if (firstValueIdx < 0 || firstValueIdx >= cards.Count)
            {
                throw new Exception("Error: First index of first value is out of range");
            }

            int count = 0;  // Counter to track number of matching cards.

            // Loop through the list starting from firstValueIdx.
            for (int i = firstValueIdx; i < cards.Count; i++)
            {

                // Nested loop to compare the card at 'i' with other cards in the list.
                for (int j = i + 1; j < cards.Count; j++)
                {

                    // If two cards have the same value, increment the count, set 'HighCard' to one of them,
                    // and add the matching card to 'tempCards' list. This might be considered shallow copy, but it work for this purpose.
                    if (cards[i].Value.Equals(cards[j].Value))
                    { 
                        count++;
                        HighCard = cards[i];
                        tempCards.Add(cards[i]);
                    }
                }
            }

            return count;
        }
        //Check if cards are same Color Suit. 
        private bool IsSameColor(out PlayingCard HighCard)
        {
            int lastCard = 4;
            HighCard = cards[lastCard];

            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i + 1].Color != cards[i].Color)
                {
                    return false; // If any two adjacent elements are not consecutive, return false.
                }
            }

            return true;
        }

        //Check if cards are in consecutive order E.g. 2,3,4,5,6.
        private bool IsConsecutive(out PlayingCard HighCard)
        {
            int lastCard = 4;
            HighCard = cards[lastCard];

            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i + 1].Value != cards[i].Value + 1)
                {
                    return false; // If any two adjacent elements are not consecutive, return false.
                }
            }

            return true;
        }

        //Hint: Worker Properties to examine each rank
        private bool IsRoyalFlush
        {
            get
            {
                // Checking if poker hand is same color AND in consecutive order
                if (IsSameColor(out _) && IsConsecutive(out PlayingCard HighCard))
                {
                    // If HighCard is Ace return true, we have a RoyalFlush!
                    RankHiCard = HighCard;
                    if (RankHiCard.Value == PlayingCardValue.Ace)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        private bool IsStraightFlush
        {
            get
            {
                if (IsSameColor(out _) && IsConsecutive(out PlayingCard HighCard))
                { 
                    RankHiCard = HighCard;
                    return true;
                }
                return false;
            }
        }
        private bool IsFourOfAKind
        {
            get
            {
                int count = NrSameValue(out PlayingCard HighCard);

                // Four cards with same value == count 6. 
                // For example: 
                // cards[0] = cards[1], cards[0] = cards[2], cards[0] = cards[3]
                // cards[1] = cards[2], cards[1] = cards[3]
                // cards[2] = cards[3]

                if (count == 6)
                {
                    RankHiCard = HighCard;
                    return true;
                }

                return false;
            }
        }
        private bool IsFullHouse
        {
            get
            {
                int count = NrSameValue(out PlayingCard Highcard);

                // Count == 4 equals 3 cards of the same value and 2 cards of the same value.
                if (count == 4)
                {
                    RankHiCard = Highcard;

                    //Determine which card should be Highcard.
                    int cardCount = 0;
                    foreach (var card in cards)
                    {
                        if (card.Value != Highcard.Value)
                            cardCount++;
                    }

                    if (cardCount > 2)
                        RankHiCard = cards[0];
                    
                    return true;
                }

                return false;
            }
        }
        private bool IsFlush
        {
            get
            {
                if (IsSameColor(out PlayingCard HighCard))
                {
                    RankHiCard = HighCard;
                    return true;
                }
                return false;
            }

        }
        private bool IsStraight
        {
            get
            {
                if (IsConsecutive(out PlayingCard HighCard))
                {
                    RankHiCard = HighCard;
                    return true;
                }
                return false;
            }
        }
        private bool IsThreeOfAKind
        {
            get
            {
                int count = NrSameValue(out PlayingCard HighCard);

                if (count == 3)
                {
                    RankHiCard = HighCard;
                    return true;
                }

                return false;
            }
        }
        private bool IsTwoPair
        {
            get
            {
                int Count = NrSameValue(out PlayingCard HighCardPair1);
                
                // count is 2 we have two pairs. HighCardsPair1 is RankHighCard, second pair is at index 0 and index 1 in tempCards.
                if (Count == 2)
                {
                    RankHiCardPair1 = HighCardPair1;
                    RankHiCard = RankHiCardPair1;
                    RankHiCardPair2 = tempCards[0];

                    return true;
                }

                return false;
            }
        }
        private bool IsPair
        {
            get
            {
                int count = NrSameValue(out PlayingCard HighCard);

                //Count 1 we have one pair. 
                if (count == 1)
                {
                    RankHiCard = HighCard;
                    return true;
                }

                return false;
            }
        }

        public PokerRank DetermineRank()
        {
            PlayingCard HighCard = cards[4]; //Last card is the highest card when the hand is sorted.  

            //Using a switch expression to set the accurate Poker rank. 
            Rank = true switch
            {
                _ when IsRoyalFlush => PokerRank.RoyalFlush,
                _ when IsStraightFlush => PokerRank.StraightFlush,
                _ when IsFourOfAKind => PokerRank.FourOfAKind,
                _ when IsFullHouse => PokerRank.FullHouse,
                _ when IsFlush => PokerRank.Flush,
                _ when IsStraight => PokerRank.Straight,
                _ when IsThreeOfAKind => PokerRank.ThreeOfAKind,
                _ when IsTwoPair => PokerRank.TwoPair,
                _ when IsPair => PokerRank.Pair,
                _ => PokerRank.HighCard
            };

            if (Rank == PokerRank.HighCard)
            {
                RankHiCard = HighCard; // Set RankHiCard to last card in the hand when Rank is HighCard. 
            }

            return Rank;
        }

        //Hint: Clear rank
        private void ClearRank()
        {
            _rankHigh = null;
            _rankHighPair1 = null;
            _rankHighPair2 = null;
            _rank = PokerRank.Unknown;
        }
        #endregion
    }
}
