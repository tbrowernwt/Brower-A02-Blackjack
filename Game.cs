using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace Brower_A02_Blackjack
{
    public class Game
    {
        public enum GameResult
        {
            Win,
            Loss,
            Tie
        }
        private Deck Deck;
        private Hand PlayerHand;
        private Hand DealerHand;
        private bool GameActive;
        private bool PlayersTurn;
        private GameResult Result;

        public Game(ImageList CardFronts) 
        {
            GameActive = true;
            Deck = new Deck(CardFronts);
            PlayersTurn = true;
            PlayerHand = new Hand();
            DealerHand = new Hand();
            PlayerHand.AddCardToHand(Deck.DealRandomCard());
            DealerHand.AddCardToHand(Deck.DealRandomCard());
            PlayerHand.AddCardToHand(Deck.DealRandomCard());
            DealerHand.AddCardToHand(Deck.DealRandomCard());
            if (GetIfBlackJack(PlayerHand) && GetIfBlackJack(DealerHand)) // Apparently this is a push according to Blackjack rules.
            {
                PlayersTurn = false;
                GameActive = false;
                Result = GameResult.Tie;
            }
            else if (GetIfBlackJack(PlayerHand))
            {
                PlayersTurn = false;
                GameActive = false;
                Result = GameResult.Win;
            }
            else if (GetIfBlackJack(DealerHand))
            {
                PlayersTurn = false;
                GameActive = false;
                Result = GameResult.Loss;
            }
        }
        private bool GetIfBlackJack(Hand h)
        {
            return h.GetHandValue() == 21;
        }
        private bool GetIfBust(Hand h)
        {
            return h.GetHandValue() > 21;
        }
        private void ProcessDealersMoves()
        {
           while(DealerHand.GetHandValue() < 17)
            {
                Hit(DealerHand);
            }
            if (DealerHand.GetHandValue() > 21)
            {
                GameActive = false;
                Result = GameResult.Win;
            }
            else
            {
                Stand();
            }
        }
        public void Hit(Hand activeHand)
        {
            activeHand.AddCardToHand(Deck.DealRandomCard());
            if(GetIfBust(activeHand))
            {
                if (PlayersTurn)
                {
                    PlayersTurn = false;
                    GameActive = false;
                    Result = GameResult.Loss;
                }
                else
                {
                    GameActive = false;
                    Result = GameResult.Win;
                }
            }
        }
        public void Stand()
        {
            if(PlayersTurn)
            {
                PlayersTurn = false;
                ProcessDealersMoves();
            }
            else
            {
                GameActive = false;
                CalculateWinnerOfRound();
            }
        }
        public bool GetIfPlayerTurn()
        {
            return PlayersTurn;
        }
        public bool GetIfGameActive()
        {
            return GameActive; 
        }
        public GameResult GetGameResult()
        {
            return Result;
        }
        public void CalculateWinnerOfRound()
        {
            if(PlayerHand.GetHandValue() > DealerHand.GetHandValue())
            {
                Result = GameResult.Win;
            }
            else if(PlayerHand.GetHandValue() == DealerHand.GetHandValue())
            {
                Result = GameResult.Tie;
            }
            else
            {
                Result = GameResult.Loss;
            }
        }
        public Hand GetPlayerHand()
        {
            return PlayerHand;
        }
        public Hand GetDealerHand()
        {
            return DealerHand;
        }
    }
}
