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

        Database database = new Database();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                setCards();
                money = 1500;
                lblMoney.Text = money.ToString();
                GetRandomTheme();
            }
        }

        public string randomCard(int min)
        {
            Random letter = new Random();
            char[] Array = "SHDC".ToCharArray();

           
            int num = letter.Next(0, 4); 
            char let = Array[num];
            
            Random rnd = new Random();
            int max = 0;
        
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
           
            // string url = "src/images/cards/" + randomcard + "C.png";
            string card = randomcard.ToString()+ let;
            return card;
        }


        public string randomStartCard()
        {
            Random letter = new Random();
            char[] Array = "SHDC".ToCharArray();
            int num = letter.Next(0, 4);
            char let = Array[num];

            Random rnd = new Random();
            int randomcard = rnd.Next(1, 13);
            //string url = "~/Cards/" + randomcard + ".png";
            CheckCard = randomcard;
            return randomcard.ToString() + let;
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
           var credit = Int32.Parse(lblMoney.Text);

           if (HiddenField_card1.Value != "null")
            {
                CardPressed = 1;
                if(winLose == "win")
                {
                    money = + 100;
                }
                else
                {
                    money = -100;
                }
            }
           else
            {
                CardPressed = 2;
                if (winLose == "win")
                {
                    money = +50;
                }
                else
                {
                    money = -50;
                }
            }
            money = money + credit;
            lblMoney.Text = money.ToString();
            SaveToDB(CardPressed, winLose);
        }

        public void GetRandomTheme()
        {
            Random rnd = new Random();
            int randomTheme = rnd.Next(0, 4);

            HiddenField_Theme.Value = randomTheme.ToString();
        }
        public void SaveToDB(int card, string WinLose)
        {
           
        }
    }
}