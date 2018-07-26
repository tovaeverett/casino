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

        Database _database = new Database();
        protected void Page_Load(object sender, EventArgs e)
        {
            HiddenField_showInfo.Value = "0";
            if (!IsPostBack)
            {
                setTheme();
                setCards();
                money = Convert.ToInt32(Request["saldo"]);
                lblMoney.Text = money.ToString();
                HiddenField_showInfo.Value = "1";
                SaveToDB();
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
            string result = HiddenField_result.Value;
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
           
            string[] splitCards = HiddenField_result.Value.Split(',');
           //( foreach(var value in splitCards)
            
                WinChance = splitCards[0].ToString();
                CardColor = splitCards[1].ToString();
                WinLose = splitCards[2].ToString();
            
                //card1:null,card2:6H,showCard:5H,winChance:1,winLose:lose


           if (CardColor == "1")
            {
                if(WinLose == "win")
                {
                    money = + 100;
                }
                else
                {
                    money = -100;
                }
            }
           else if (CardColor == "2")
            {
                if( WinLose == "win")
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
          //  var theme = _database.getTheme(randomTheme);
            HiddenField_theme.Value = randomTheme.ToString();
            return randomTheme;
        }

        public void setCredit()
        {
            //HiddenField_credit.Value = "100"; //db -> getCredit();
        }
        public void SaveToDB()
        {
            Playerlog pl = new Playerlog();

            pl.userid = "test1234";
            pl.balance_in = 5;
            pl.balance_out = 10;
            pl.bet = 5;
            pl.condition = "testcondition";
            pl.gamename = "CardDraw";
            pl.moment = 1;
            pl.outcome = 0;
            pl.response = "r1";
            pl.stimuli = "s2";
            pl.timestamp_begin = DateTime.Now;
            pl.timestamp_O = DateTime.Now;
            pl.timestamp_R = DateTime.Now;
            pl.trial = 1;
           
            _database.updatePlayerLog(pl);
        }
    }
}