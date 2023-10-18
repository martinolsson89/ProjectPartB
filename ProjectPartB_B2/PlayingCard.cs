﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPartB_B2
{
	public class PlayingCard:IComparable<PlayingCard>, IPlayingCard
	{
        public PlayingCardColor Color { get; init; }
        public PlayingCardValue Value { get; init; }

        #region IComparable Implementation
        //Need only to compare value in the project
        public int CompareTo(PlayingCard card1)
        {
            return Value.CompareTo(card1.Value);

        }
        #endregion

        public bool IsSameValueAs(PlayingCard other)
        {
            return Value == other.Value;
        }

        #region ToString() related
        string ShortDescription
        {
            //Use switch statment or switch expression
            //https://en.wikipedia.org/wiki/Playing_cards_in_Unicode
            get
            {
                char colorSymbol = GetColorSymbol();
                string valueSymbol = GetValueSymbol();

                return $"{colorSymbol} {valueSymbol}";

            }
        }
        public PlayingCard(PlayingCardColor color, PlayingCardValue value)
        {
            Color = color;
            Value = value;
        }

        public override string ToString() => ShortDescription;
        #endregion

        private char GetColorSymbol()
        {
            switch (Color)
            {
                case PlayingCardColor.Clubs:
                    return '\u2663';
                case PlayingCardColor.Diamonds:
                    return '\u2666';
                case PlayingCardColor.Hearts:
                    return '\u2665';
                case PlayingCardColor.Spades:
                    return '\u2660';
                default:
                    return '0';
            }
        }

        private string GetValueSymbol()
        {
            if (Value >= PlayingCardValue.Two && Value <= PlayingCardValue.Ten)
            {
                return Value.ToString();
            }
            else
            {
                switch (Value)
                {
                    case PlayingCardValue.Knight:
                        return "Knight";
                    case PlayingCardValue.Queen:
                        return "Queen";
                    case PlayingCardValue.King:
                        return "King";
                    case PlayingCardValue.Ace:
                        return "Ace";
                    default:
                        return null;
                }
            }
        }

    }
}
