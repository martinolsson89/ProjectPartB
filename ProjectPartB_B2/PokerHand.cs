using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectPartB_B2
{
    class PokerHand : HandOfCards, IPokerHand
    {
        #region Clear

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

            ClearRank();
        }

        #endregion

        #region Poker Rank related

        //https://www.poker.org/poker-hands-ranking-chart/

        //Hint: using backing fields
        private PokerRank _rank = PokerRank.Unknown;
        private PlayingCard _rankHigh = null;
        private PlayingCard _rankHighPair1 = null;
        private PlayingCard _rankHighPair2 = null;

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
        
        private int NrSameValue(int firstValueIdx, out int lastValueIdx, out PlayingCard HighCard)
        {
            lastValueIdx = firstValueIdx;  // Initialize lastValueIdx to -1 in case no match is found.
            HighCard = null;    // Initialize HighCard to null.

            if (firstValueIdx < 0 || firstValueIdx >= cards.Count)
            {
                // Handle an invalid input index.
                return -1;  // You can choose an appropriate error code or value here.
            }

            int count = 0;  // Initialize a counter to track the number of matching cards.

            // Loop through the list starting from firstValueIdx and find the last index of a card with the same value.
            for (int i = firstValueIdx; i < cards.Count; i++)
            {

                for (int j = i + 1; j < cards.Count; j++) // Change the initialization and condition here
                {
                    if (cards[i].IsSameValueAs(cards[j]))
                    {
                        count++;
                        lastValueIdx = i;
                        HighCard = cards[i];
                    }
                }
            }


            return count;
        }
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
                PlayingCard HighCard;
                if (IsSameColor(out HighCard) && IsConsecutive(out HighCard))
                {
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
                PlayingCard HighCard;
                if (IsSameColor(out HighCard) && IsConsecutive(out HighCard))
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
                int firstValueIdx = 0;
                int lastValueIdx = 0;
                PlayingCard HighCard;

                int count = NrSameValue(firstValueIdx , out lastValueIdx, out HighCard);

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
                int firstValueIdx = 0;
                int lastValueIdx = 0;
                PlayingCard HighCard;

                int count = NrSameValue(0, out lastValueIdx, out HighCard);

                if (count == 4)
                {
                    RankHiCard = HighCard;

                    count = NrSameValue(lastValueIdx + 1, out lastValueIdx, out HighCard);
                    
                    if (count == 1)
                    {
                        // You have a full house.
                        return true;
                    }
                }

                return false;
            }
        }
        private bool IsFlush
        {
            get
            {
                PlayingCard HighCard;
                if (IsSameColor(out HighCard))
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
                PlayingCard HighCard;
                if (IsConsecutive(out HighCard))
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
                int firstValueIdx = 0;
                int lastValueIdx;
                PlayingCard HighCard;

                int count = NrSameValue(0, out lastValueIdx, out HighCard);

                if (count == 4)
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
                int lastValueIdx;

                int firstPairCount = NrSameValue(0, out lastValueIdx, out PlayingCard HighCardPair1);

                if (firstPairCount == 1)
                {
                    int nextValueIdx = lastValueIdx + 1;
                    int secondPairCount = NrSameValue(nextValueIdx,out lastValueIdx, out PlayingCard HighCardPair2);

                    if (secondPairCount == 1)
                    {
                        RankHiCardPair1 = HighCardPair1;
                        RankHiCardPair2 = HighCardPair2;

                        // Compare the values of the two pairs to determine which is higher.
                        if (RankHiCardPair1.Value > RankHiCardPair2.Value)
                        {
                            RankHiCard = RankHiCardPair1;
                        }
                        else
                        {
                            RankHiCard = RankHiCardPair2;
                        }

                        return true;
                    }
                }

                return false;
            }
        }
        private bool IsPair
        {
            get
            {
                PlayingCard HighCard;
                int firstValueIdx = 0;
                int lastValueIdx;
                int count;

               count = NrSameValue(firstValueIdx,out lastValueIdx, out HighCard);


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
            PlayingCard HighCard;

            cards.Sort(); // Sort the hand for easier evaluation

            // Check for the highest-ranking hands first.
            if (IsRoyalFlush)
            {
                Rank = PokerRank.RoyalFlush;
                return Rank;
            }

            if (IsStraightFlush)
            {
                Rank = PokerRank.StraightFlush;
                return Rank;
            }

            if (IsFourOfAKind)
            {
                Rank = PokerRank.FourOfAKind;
                return Rank;
            }

            if (IsFullHouse)
            {
                Rank = PokerRank.FullHouse;
                return Rank;
            }

            if (IsFlush)
            {
                Rank = PokerRank.Flush;
                return Rank;
            }

            if (IsStraight)
            {
                Rank = PokerRank.Straight;
                return Rank;
            }

            if (IsThreeOfAKind)
            {
                Rank = PokerRank.ThreeOfAKind;
                return Rank;
            }

            if (IsTwoPair)
            {
                if (RankHiCardPair1.Value > RankHiCardPair2.Value)
                {
                    RankHiCard = RankHiCardPair1;
                }
                else
                {
                    RankHiCard = RankHiCardPair2;
                }
                
                Rank = PokerRank.TwoPair;
                return Rank;
            }

            if (IsPair)
            {
                Rank = PokerRank.Pair;
                return Rank;
            }

            RankHiCard = cards[4];
            Rank = PokerRank.HighCard;
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
