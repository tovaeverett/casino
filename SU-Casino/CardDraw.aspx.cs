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
            HiddenField_showInfo.Value = "0";
            if (!IsPostBack)
            {
                setTheme();
                setCards();
                money = 1500;
                lblMoney.Text = money.ToString();
                HiddenField_showInfo.Value = "1";
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
            setTheme();
            checkForWin();
            setCards();
            setCredit(); 
        }
        private void checkForWin()
        {
           //int CardPressed = 0; 
           //var winLose = HiddenField_WinLose.Value;
           var credit = Int32.Parse(lblMoney.Text);
            string WinChance = "";
            string CardColor = "";
            string WinLose = "";
           // string test = "PressCard:1, WinChance: 1, WinLose: lose";
           
            string[] splitCards = HiddenField_result.Value.Split(':');
            foreach(var value in splitCards)
            {
                WinChance = value[0].ToString();
                CardColor = value[1].ToString();
                WinLose = value[2].ToString();
            }
                //card1:null,card2:6H,showCard:5H,winChance:1,winLose:lose


           if (CardColor != "null")
            {

                if(CardColor == "1")
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
                if (CardColor == "2")
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
            //SaveToDB(CardPressed, winLose);
        }

        public int setTheme()
        {
            Random rnd = new Random();
            int randomTheme = rnd.Next(1, 4);
            var theme = database.getTheme(randomTheme);
            HiddenField_theme.Value = randomTheme.ToString();
            return randomTheme;
        }

        public void setCredit()
        {
            HiddenField_credit.Value = "100"; //db -> getCredit();
        }
        public void SaveToDB(int card, string WinLose)
        {

        }
    }
}