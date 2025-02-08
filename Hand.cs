using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brower_A02_Blackjack
{
    public class Hand
    {
        private List<Card> CardsInHand;
        public Hand()
        {
            CardsInHand = new List<Card>();
        }
        public void AddCardToHand(Card card)
        {
            CardsInHand.Add(card);
        }
        public List<Card> GetCardsInHand()
        {
            return CardsInHand;
        }
        public int GetHandValue(bool ObfuscateSecondCard = false)
        {
            if (ObfuscateSecondCard) // Optional parameter to hide the second card to calculate Dealer hand while players turn
            {
                switch (CardsInHand[0].GetRank())
                {
                    case Card.CardRank.Ace:
                        return 11;
                    case Card.CardRank.Jack:
                        return 10;
                    case Card.CardRank.Queen:
                        return 10;
                    case Card.CardRank.King:
                        return 10;
                    default:
                        return (int)CardsInHand[0].GetRank();                
                }
            }
            int Total = 0;
            int CountOfAces = 0; // Aces need to be tallied after summing other cards.
            foreach (Card Card in CardsInHand)
            {
                switch(Card.GetRank())
                {
                    case Card.CardRank.Ace:
                        CountOfAces++;
                        break;
                    case Card.CardRank.Jack:
                        Total += 10;
                        break;
                    case Card.CardRank.Queen:
                        Total += 10;
                        break;
                    case Card.CardRank.King:
                        Total += 10;
                        break;
                    default:
                        Total += (int)Card.GetRank();
                        break;
                }
            }
            for(int i = 1; i <= CountOfAces; i++) // Process aces
            {
                if(Total + 11 > 21)
                {
                    Total++;
                }
                else
                {
                    Total += 11;
                }
            }
            return Total;
        }
    }
}
