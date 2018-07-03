using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SU_Casino
{
    public partial class CardDraw : System.Web.UI.Page
    {
        private int CheckCard;
        public int money;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                setCards();
                money = 1500;
                lblMoney.Text = money.ToString();
            }

        }

        public string randomCard(int min)
        {
            int max = 0;
            Random rnd = new Random();
            if(min < 8)
            {
                max = min +5;
            }
            else if(min >= 8)
            {
                min = 8;
                max = 13;
            }

            int randomcard = rnd.Next(min, max);
            Random letter = new Random();
            int num = letter.Next(0, 3); // Zero to 25
            char let = (char)('a' + num);
            // string url = "src/images/cards/" + randomcard + "C.png";
            string card = randomcard.ToString()+ let;
            return card;
        }


        public int randomStartCard()
        {
            Random rnd = new Random();

            int randomcard = rnd.Next(0, 12);
            //string url = "~/Cards/" + randomcard + ".png";
            CheckCard = randomcard;
            return randomcard;
        }

        public void setCards()
        {
            HiddenField_card3.Value = randomStartCard().ToString();
            HiddenField_card2.Value = randomCard(CheckCard).ToString();
            HiddenField_card1.Value = randomCard(CheckCard).ToString();
        }
        protected void btnPlay_Click(object sender, EventArgs e)
        {
            checkForWin();
            setCards();
        }
        private void checkForWin()
        {
           int CardPressed = 0; 
           var winLose = HiddenField_WinLose.Value;
           if(HiddenField_card1.Value != null)
            {
                CardPressed = 1;
            }
           else
            {
                CardPressed = 2;
            }
            SaveToDB(CardPressed, winLose);
        }
        public void SaveToDB(int card, string WinLose)
        {

        }

        /* protected void card1_back_Click(object sender, ImageClickEventArgs e)
         {
             money = Convert.ToInt32(lblMoney.Text);
             //card1.ImageUrl = randomCard(CheckCard);
             //card2.Visible = false;
             checkCards(1);
             Savedata(1, 100);
         }

         protected void card3_Click(object sender, ImageClickEventArgs e)
         {
             money = Convert.ToInt32(lblMoney.Text);
             card3.ImageUrl = randomCard(CheckCard);
             //card1.Visible = false;
             checkCards(2);
             Savedata(2, 50);
         }

         public void checkCards(int card)
         {
             string imgUrl = "src/images/";
             imgWinner.Visible = true;
             if (card == 1)
             {
                 if (card1.ImageUrl == card2.ImageUrl)
                 {
                     imgWinner.ImageUrl = imgUrl + "coins.gif";
                     money = money + 50;
                 }
                 else
                 {
                     imgWinner.ImageUrl = "~/Cards/youlose.png";
                     money = money - 20;
                 }
             }
             else
             {
                 if (card2.ImageUrl == card2.ImageUrl)
                 {
                     imgWinner.ImageUrl = imgUrl + "coins.gif";
                     money = money + 200;
                 }
                 else
                 {
                     imgWinner.ImageUrl = "~/Cards/youlose.png";
                     money = money - 100;
                 }
             }
             lblMoney.Text = money.ToString();
             // NextRound();
         }

         public void NextRound()
         {
             Thread.Sleep(1);
             card1.Visible = true;
             card1.ImageUrl = "~/Cards/TopCardQuestion.png";
             card2.Visible = true;
             card2.ImageUrl = "~/Cards/TopCardQuestion.png";
             card3.ImageUrl = randomStartCard();
         }
         public void Savedata(int card, int bet)
         {


         }*/

    }
}