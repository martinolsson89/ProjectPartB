using System;
using System.Numerics;

namespace ProjectPartB_B2
{
    class PokerHand : HandOfCards, IPokerHand
    {
        #region Clear

        public override void Clear()
        {
            HandCards.Clear();
        }
        #endregion

        #region Remove and Add related

        public override void Add(PlayingCard card)
        {
            HandCards.Add(card);
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
            get
            {
                return _rank;
            }
            set
            {
                _rank = value;
            }
        }

        public PlayingCard RankHiCard
        {
            get
            {
                return _rankHigh;
            }
            set
            {
                _rankHigh = value; 
            }

        }
        public PlayingCard RankHiCardPair1 => _rankHighPair1;
        public PlayingCard RankHiCardPair2 => _rankHighPair2;

        //Hint: Worker Methods to examine a sorted hand
        private int NrSameValue(int firstValueIdx, out int lastValueIdx, out PlayingCard HighCard)
        {
            lastValueIdx = firstValueIdx;
            HighCard = HandCards[firstValueIdx]; // Initialize HighCard with the first card.

            int count = 1; // Initialize the count with 1 because we already have the first card.

            for (int i = firstValueIdx + 1; i < HandCards.Count; i++)
            {
                if (HandCards[i].Value == HandCards[firstValueIdx].Value)
                {
                    // Found a card with the same value.
                    count++;

                    // Update lastValueIdx to the current index.
                    lastValueIdx = i;

                    // Update HighCard if the current card is higher.
                    if (HandCards[i].CompareTo(HighCard) > 0)
                    {
                        HighCard = HandCards[i];
                    }
                }
            }

            return count;

        }
        private bool IsSameColor(out PlayingCard HighCard)
        {
            HighCard = null;
            for (int i = 0; i < HandCards.Count - 1; i++)
            {
                if (HighCard == null || HighCard.Value < HandCards[i].Value)
                {
                    HighCard = HandCards[i];
                }
                if (HandCards[i + 1].Color != HandCards[i].Color)
                {
                    return false; // If any two adjacent elements are not consecutive, return false.
                }
                
            }

            return true;
        }
        private bool IsConsecutive(out PlayingCard HighCard)
        {
            HighCard = RankHiCard;
            for (int i = 0; i < HandCards.Count - 1; i++)
            {
                if (HighCard == null || HighCard.Value < HandCards[i].Value)
                {
                    HighCard = HandCards[i];
                }
                if (HandCards[i + 1].Value != HandCards[i].Value + 1)
                {
                    return false; // If any two adjacent elements are not consecutive, return false.
                }
            }

            return true;
        }

        //Hint: Worker Properties to examine each rank
        private bool IsRoyalFlush => false;
        private bool IsStraightFlush => false;
        private bool IsFourOfAKind => false;
        private bool IsFullHouse => false;

        private bool IsFlush => false;

        private bool IsStraight => false;
        private bool IsThreeOfAKind => false;
        private bool IsTwoPair => false;

        private bool IsPair
        {
            get;

        }

        public PokerRank DetermineRank()
        {
            PlayingCard HighCard;

            ClearRank(); // Clear the previous rank information

            HandCards.Sort(); // Sort the hand for easier evaluation

            if (IsSameColor(out HighCard) && IsConsecutive(out HighCard))
            {
                RankHiCard = HighCard;
                if (RankHiCard.Value == PlayingCardValue.Ace)
                {
                    return PokerRank.RoyalFlush;
                }
                else
                {
                    return PokerRank.StraightFlush;
                }
            }
            else
            {
                int firstValueIdx = 0;
                int lastValueIdx;

                int count = NrSameValue(0, out lastValueIdx, out HighCard);

                if (count == 4)
                {
                    // You have Four of a Kind with HighCard as the highest card.
                    RankHiCard = HighCard;
                    _rank = PokerRank.FourOfAKind;
                    return PokerRank.FourOfAKind;
                }
                if (count == 3)
                {
                    RankHiCard = HighCard;
                    _rank = PokerRank.ThreeOfAKind;
                    int updatedValueIdx = lastValueIdx + 1;
                    count = NrSameValue(updatedValueIdx, out lastValueIdx, out HighCard);
                    if (count == 2)
                    {
                        // You have a full house.
                        _rank = PokerRank.FullHouse;
                        return PokerRank.FullHouse;
                    }
                    else
                    {
                        return PokerRank.ThreeOfAKind;
                    }
                }
                if(IsSameColor(out HighCard))
                {
                    RankHiCard = HighCard;
                    _rank = PokerRank.Flush;
                    return PokerRank.Flush;
                }

                if (IsConsecutive(out HighCard))
                {
                    RankHiCard = HighCard;
                    _rank = PokerRank.Straight;
                    return PokerRank.Straight;

                }
                count = NrSameValue(0, out lastValueIdx, out HighCard);
                if (count == 2)
                {
                    // You have a full house.
                    RankHiCard = HighCard;
                    _rank = PokerRank.Pair;
                    return PokerRank.Pair;
                }
            }

            RankHiCard = HandCards[HandCards.Count - 1];
            _rank = PokerRank.HighCard;
            return PokerRank.HighCard;
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
