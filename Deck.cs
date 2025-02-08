using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brower_A02_Blackjack
{
    public class Deck
    {
        private List<Card> cardsList = new List<Card>();
        private Random rng = new Random();

        public Deck(ImageList cardFaces) 
        {
            loadDeck(cardFaces);
        }
        private void loadDeck(ImageList imageListCardFronts)
        {
            cardsList.Clear();

            Card card;
            int imageIndex = 0;
            for(int i = 1; i <= 13; i++)
            {
                card = new Card(Card.CardSuit.Clubs, i, imageListCardFronts.Images[imageIndex]);
                cardsList.Add(card);
                imageIndex++;

                card = new Card(Card.CardSuit.Diamonds, i, imageListCardFronts.Images[imageIndex]);
                cardsList.Add(card);
                imageIndex++;

                card = new Card(Card.CardSuit.Hearts, i, imageListCardFronts.Images[imageIndex]);
                cardsList.Add(card);
                imageIndex++;

                card = new Card(Card.CardSuit.Spades, i, imageListCardFronts.Images[imageIndex]);
                cardsList.Add(card);
                imageIndex++;
            }
        }  
        public Card DealRandomCard()
        {
            return cardsList[rng.Next(cardsList.Count)];
        }
    }
}
