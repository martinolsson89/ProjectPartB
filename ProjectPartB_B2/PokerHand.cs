using System;
using System.Linq;
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

        private int NrSameValue(int valueIdx, out PlayingCard HighCard)
        {
            HighCard = HandCards[valueIdx];
            int count = 1;

            for (int i = valueIdx + 1; i < HandCards.Count; i++)
            {
                if (HandCards[i].Value == HighCard.Value)
                {
                    count++;
                    if (HandCards[i].Value > HighCard.Value)
                    {
                        HighCard = HandCards[i];
                    }
                }
            }

            return count;
        }

        private void FindMostCommonCard(out PlayingCard mostCommonCard, out int count)
        {
            var groupedCards = HandCards
                .GroupBy(card => card)
                .OrderByDescending(group => group.Count());

            var mostCommonGroup = groupedCards.First();

            mostCommonCard = mostCommonGroup.Key;
            count = mostCommonGroup.Count();
        }

         private void CountRestOfCards(out int count, out PlayingCard mostCommonCard, PlayingCard excludedCard = null)
         {
             var restOfCards = HandCards.Where(card => card != excludedCard).ToList();

             if (restOfCards.Count > 0)
             {
                 var groupedRest = restOfCards.GroupBy(card => card)
                     .OrderByDescending(group => group.Count());

                 var mostCommonGroup = groupedRest.First();
                 mostCommonCard = mostCommonGroup.Key;
                 count = mostCommonGroup.Count();
             }
             else
             {
                 // No cards in the rest of the list
                 mostCommonCard = null;
                 count = 0;
             }
         }



        private bool IsSameColor(out PlayingCard HighCard)
        {
            HighCard = HandCards[4];
            for (int i = 0; i < HandCards.Count - 1; i++)
            {
                if (HandCards[i + 1].Color != HandCards[i].Color)
                {
                    return false; // If any two adjacent elements are not consecutive, return false.
                }

            }

            RankHiCard = HighCard;
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
                int lastValueIdx;
                PlayingCard HighCard;

                int count = NrSameValue(0 , out HighCard);

                if (count == 4)
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
                int lastValueIdx;
                PlayingCard HighCard;

                int count = NrSameValue(0, out HighCard);

                if (count == 3)
                {
                    RankHiCard = HighCard;

                    count = NrSameValue(0,  out HighCard);
                    
                    if (count == 2)
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

                int count = NrSameValue(0, out HighCard);

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
                int firstPairCount = NrSameValue(0, out PlayingCard HighCardPair1);

                if (firstPairCount == 2)
                {
                    int nextValueIdx = HandCards.IndexOf(HighCardPair1) + 1;
                    int secondPairCount = NrSameValue(nextValueIdx, out PlayingCard HighCardPair2);

                    if (secondPairCount == 2)
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
                PlayingCard mostCommonCard;
                int count;

                FindMostCommonCard(out mostCommonCard, out count);


                if (count == 2)
                {
                    RankHiCard = mostCommonCard;
                    return true;
                }

                return false;
            }
        }

        public PokerRank DetermineRank()
        {
            PlayingCard HighCard;

            ClearRank(); // Clear the previous rank information

            HandCards.Sort(); // Sort the hand for easier evaluation

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

            RankHiCard = HandCards[4];
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
