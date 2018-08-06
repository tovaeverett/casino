using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SU_Casino
{
    public partial class CardDraw2 : System.Web.UI.Page
    {
        private int CheckCard;
        public int money;
        private Game currentGame;
        private static int trial;

        Database _database = new Database();
        public SqlConnection connectionstring = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            currentGame = (Game)Session["currentGame"];
            if (currentGame == null)
            {
                currentGame = Game.getDummyGame();
                //TODO An error page might not be needed. Decide on error handling
                //Response.Redirect("ErrorPage.aspx");
            }

            HiddenField_showInfo.Value = "0";
            Hiddenfield_text.Value = _database.getText("playCardInfo");
            if (!IsPostBack)
            {
                setTheme();
                setCards();
                HiddenField_game.Value = currentGame.Name;
                money = currentGame.Saldo;
                HiddenField_win1.Value = currentGame.Win_O1.ToString();
                HiddenField_win1.Value = currentGame.Win_O2.ToString();
                lblMoney.Text = money.ToString();
                HiddenField_showInfo.Value = "1";
                trial = 1;
                setCurrentBalance();

                //SaveToDB(); //Is this really needed? User has not begun playing yet
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
            setCurrentBalance();
            if (trial > currentGame.Trials)
                GameLogic.getNextGame(currentGame, money);

        }
        private void checkForWin()
        {
           //int CardPressed = 0; 
           //var winLose = HiddenField_WinLose.Value;
            var credit = Int32.Parse(lblMoney.Text);
            string WinChance = "";
            string CardBet = "";
            string WinLose = "";
           // string test = "PressCard:1, WinChance: 1, WinLose: lose";
           
            string[] splitCards = HiddenField_result.Value.Split(',');
           //( foreach(var value in splitCards)
            
                WinChance = splitCards[0].ToString();
                CardBet = splitCards[1].ToString();
                WinLose = splitCards[2].ToString();

            //card1:null,card2:6H,showCard:5H,winChance:1,winLose:lose
            int winningAmount = 0;
            int betAmount = 0;
            if (CardBet.Equals("bet_R1"))
            {
                betAmount = currentGame.Bet_R1;
                if (WinLose.Equals("win"))                    
                    winningAmount = currentGame.Win_O1;

            }else if (CardBet.Equals("bet_R2")) { 
                betAmount = currentGame.Bet_R2;
                if (WinLose.Equals("win"))
                    winningAmount = currentGame.Win_O2;
            }

            money = Convert.ToInt32(HiddenField_currentBalance.Value) + betAmount + winningAmount;
            lblMoney.Text = money.ToString();
            SaveToDB(CardBet, betAmount, winningAmount);
        }

        public String setTheme()
        {
            if (currentGame != null && currentGame.Name == "Instrumental_acq2")
            {
                HiddenField_theme.Value = "null";
                return "null";
            }
            else
            {
                Random rnd = new Random();
                int randomTheme = rnd.Next(1, 5);
                //  var theme = _database.getTheme(randomTheme);
                HiddenField_theme.Value = randomTheme.ToString();
                return randomTheme.ToString();
            }
        }

        public void setCurrentBalance()
        {
            //HiddenField_credit.Value = "100"; //db -> getCredit();
            HiddenField_currentBalance.Value = money.ToString();
        }

        //TODO check if these initial values are correct, or may be we do not need this method at all?
        public void SaveToDB()
        {
            Playerlog pl = new Playerlog();

            pl.userid = "test1234"; //getFromSession
            pl.balance_in = money;  //initial Game saldo
            pl.balance_out = money;  //in the begininnig balance in and out is same
            pl.bet = 0; //initial bet is 0
            pl.condition = currentGame.Condition;
            pl.gamename = currentGame.Name;
            pl.stimuli = currentGame.Name;  //is this really needed?
            pl.moment = currentGame.Sequence;
            pl.outcome = 0;
            pl.response = null;
            pl.timestamp_begin = DateTime.Now;
            pl.timestamp_O = DateTime.Now;
            pl.timestamp_R = DateTime.Now;
            pl.trial = trial;
           
            _database.updatePlayerLog(pl);
        }

        public void SaveToDB(String CardBetResponse, int betAmount, int winAmount)
        {
            Playerlog pl = new Playerlog();

            pl.userid = "test1234";
            pl.balance_in = Convert.ToInt32(HiddenField_currentBalance.Value);
            pl.balance_out = money;
            pl.bet = betAmount;
            pl.condition = currentGame.Condition;
            pl.gamename = currentGame.Name;
            pl.moment = currentGame.Sequence;
            pl.outcome = winAmount;
            pl.response = CardBetResponse;
            pl.stimuli = currentGame.Name;
            pl.timestamp_begin = DateTime.Now;
            pl.timestamp_O = DateTime.Now;
            pl.timestamp_R = DateTime.Now;
            pl.trial = trial++;

            _database.updatePlayerLog(pl);
        }
    }
}