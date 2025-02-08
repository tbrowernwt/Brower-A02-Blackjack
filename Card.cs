using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Brower_A02_Blackjack
{

    public class Card
    {
        public enum CardSuit
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }
        public enum CardRank
        {
            Ace = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13
        }
        private CardSuit Suit;
        private CardRank Rank;
        private Image CardFront;
        public Card(CardSuit Suit, int Rank, Image CardFront)
        {
            this.Suit = Suit;
            this.Rank = (CardRank)Rank;
            this.CardFront = CardFront;
        }
        public CardSuit GetSuit() { return Suit; }
        public CardRank GetRank() { return Rank; }
        public Image GetCardFront() { return CardFront;  }
    }
}
