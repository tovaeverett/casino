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
                card3.ImageUrl = randomStartCard();
                money = 1500;
                lblMoney.Text = money.ToString();
            }

        }

        public string randomCard(int min)
        {
            Random rnd = new Random();

            int randomcard = rnd.Next(min, min + 5);
            string url = "~/Cards/" + randomcard + ".png";
            return url;
        }

        public string randomStartCard()
        {
            Random rnd = new Random();

            int randomcard = rnd.Next(1, 9);
            string url = "~/Cards/" + randomcard + ".png";
            CheckCard = randomcard;
            return url;
        }

        protected void card1_Click(object sender, ImageClickEventArgs e)
        {
            money = Convert.ToInt32(lblMoney.Text);
            card1.ImageUrl = randomCard(CheckCard);
            card2.Visible = false;
            checkCards(1);
            Savedata(1, 100);
        }

        protected void card2_Click(object sender, ImageClickEventArgs e)
        {
            money = Convert.ToInt32(lblMoney.Text);
            card2.ImageUrl = randomCard(CheckCard);
            card1.Visible = false;
            checkCards(2);
            Savedata(2, 50);
        }

        public void checkCards(int card)
        {
            imgWinner.Visible = true;
            if (card == 1)
            {
                if (card1.ImageUrl == card3.ImageUrl)
                {
                    imgWinner.ImageUrl = "~/Cards/youwin.png";
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
                if (card2.ImageUrl == card3.ImageUrl)
                {
                    imgWinner.ImageUrl = "~/Cards/youwin.png";
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


        }

    }
}