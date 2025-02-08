using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brower_A02_Blackjack
{
    public partial class Form1 : Form
    {
        private Game GamePlay;
        private List<PictureBox> PlayerPictureBoxesList = new List<PictureBox>();
        private List<PictureBox> DealerPictureBoxesList = new List<PictureBox>();
        private int PlayerWins = 0;
        private int DealerWins = 0;
        private int Pushes = 0;
        
        public Form1()
        {
            InitializeComponent();
            PlayerPictureBoxesList.Add(pictureBoxPlayerCard1);
            PlayerPictureBoxesList.Add(pictureBoxPlayerCard2);
            PlayerPictureBoxesList.Add(pictureBoxPlayerCard3);
            PlayerPictureBoxesList.Add(pictureBoxPlayerCard4);
            PlayerPictureBoxesList.Add(pictureBoxPlayerCard5);
            PlayerPictureBoxesList.Add(pictureBoxPlayerCard6);
            PlayerPictureBoxesList.Add(pictureBoxPlayerCard7);
            DealerPictureBoxesList.Add(pictureBoxDealerCard1);
            DealerPictureBoxesList.Add(pictureBoxDealerCard2);
            DealerPictureBoxesList.Add(pictureBoxDealerCard3);
            DealerPictureBoxesList.Add(pictureBoxDealerCard4);
            DealerPictureBoxesList.Add(pictureBoxDealerCard5);
            DealerPictureBoxesList.Add(pictureBoxDealerCard6);
            DealerPictureBoxesList.Add(pictureBoxDealerCard7);
        }

        private void buttonStartNewGame_Click(object sender, EventArgs e)
        {
            foreach (PictureBox p in PlayerPictureBoxesList)
            {
                p.Image = null;
            }
            foreach(PictureBox p in DealerPictureBoxesList)
            {
                p.Image = null;
            }
            buttonHit.Enabled = true;
            buttonStand.Enabled = true;
            buttonResetStatistics.Enabled = true;
            GamePlay = new Game(ImageListCardFronts);
            UpdatePlayfield();
        }

        private void buttonResetStatistics_Click(object sender, EventArgs e)
        {
            PlayerWins = 0;
            DealerWins = 0;
            Pushes = 0;
            UpdatePlayfield();
        }

        private void buttonHit_Click(object sender, EventArgs e)
        {
            GamePlay.Hit(GamePlay.GetPlayerHand());
            buttonResetStatistics.Enabled = false;
            UpdatePlayfield();
        }

        private void buttonStand_Click(object sender, EventArgs e)
        {
            GamePlay.Stand();
            buttonResetStatistics.Enabled = false;
            UpdatePlayfield();
        }
        private void UpdatePlayfield()
        {
            int i = 0;
            foreach (Card c in GamePlay.GetPlayerHand().GetCardsInHand())
            {
                PlayerPictureBoxesList[i].Image = c.GetCardFront();
            }
            labelPlayerTotalAmt.Text = GamePlay.GetPlayerHand().GetHandValue().ToString();
            if (GamePlay.GetIfPlayerTurn()) // Update dealer's side of the field. Show first card if it's player's turn
            {
                pictureBoxDealerCard1.Image = GamePlay.GetDealerHand().GetCardsInHand()[0].GetCardFront();
                pictureBoxDealerCard2.Image = ImageListCardFronts.Images[52]; // Second card will show the card backing.
                labelDealerTotalAmt.Text = GamePlay.GetDealerHand().GetHandValue(true).ToString(); // Obfuscate the second card's value and tally only the first card's value
            }
            else // Otherwise show all of dealer's cards.
            {
                i = 0;
                foreach(Card c in GamePlay.GetDealerHand().GetCardsInHand())
                {
                    DealerPictureBoxesList[i].Image = c.GetCardFront();
                    i++;
                }
                labelDealerTotalAmt.Text = GamePlay.GetDealerHand().GetHandValue().ToString();
            }                    
            i = 0;
            foreach(Card c in GamePlay.GetPlayerHand().GetCardsInHand()) // Update player's field
            {
                PlayerPictureBoxesList[i].Image = c.GetCardFront();
                i++;
            }
            if(!GamePlay.GetIfGameActive()) // If game is over, then update appropriate value
            {
                buttonHit.Enabled = false;
                buttonStand.Enabled = false;
                buttonResetStatistics.Enabled = false;
                switch(GamePlay.GetGameResult())
                {
                    case Game.GameResult.Win:
                        PlayerWins++;
                        break;
                    case Game.GameResult.Loss:
                        DealerWins++;
                        break;
                    case Game.GameResult.Tie:
                        Pushes++;
                        break;
                }
            }
            labelPlayerWinsAmt.Text = PlayerWins.ToString();
            labelDealerWinsAmt.Text = DealerWins.ToString();
            labelTiesAmt.Text = Pushes.ToString();
        }
    }
}
