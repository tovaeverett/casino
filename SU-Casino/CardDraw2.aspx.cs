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
                currentGame.getTheme();
                //TODO An error page might not be needed. Decide on error handling
                //Response.Redirect("ErrorPage.aspx");
            }

            HiddenField_showInfo.Value = "0";
            Hiddenfield_text.Value = _database.getText("playCardInfo");
            if (!IsPostBack)
            {
                currentGame.UserId = Request["workerId"];
                setTheme();
                setCards();
                HiddenField_game.Value = currentGame.Name;
                money = currentGame.Saldo;
                HiddenField_win1.Value = currentGame.Win_O1.ToString();
                HiddenField_win2.Value = currentGame.Win_O2.ToString();
                lblMoney.Text = money.ToString();
                HiddenField_showInfo.Value = "1";
                trial = 1;
                setCurrentBalance();
                HiddenField_Trail.Value = currentGame.Trials.ToString();
                //SaveToDB(); //Is this really needed? User has not begun playing yet
            }
        }

        public string randomCard(int min, double winChance)
        {
            var cardColor = GetColor();

            Random letter = new Random();
            char[] Array = "SHDC".ToCharArray();

           
            int num = letter.Next(0, 4); 
            char let = Array[num];
            
            Random rnd = new Random();
            //int max = 0;
        
            //if(min < 8)
            //{
            //    max = min +5;
            //}
            //else if(min >= 8)
            //{
            //    min = 8;
            //    max = 13;
            //}

            // få array med "förlorar" korten
            var losingCards = currentGame.RetrieveLosingNumbers(1, 13, CheckCard);

            // vann eller vann inte?
            if (currentGame.didWin(winChance))
            {
                return CheckCard.ToString() + cardColor;
            }
            else
            {
                int randomcardIndex = rnd.Next(0, losingCards.Length);

                int randomcard = losingCards[randomcardIndex];
                // string url = "src/images/cards/" + randomcard + "C.png";
                string card = randomcard.ToString() + cardColor;
                return card;
            }
        }

        private char GetColor()
        {
            Random letter = new Random();
            char[] Array = "SHDC".ToCharArray();

            int num = letter.Next(0, 4);
            char let = Array[num];
            return let;
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
            HiddenField_card2.Value = randomCard(CheckCard, currentGame.Prob_O2).ToString();
            HiddenField_card1.Value = randomCard(CheckCard, currentGame.Prob_O1).ToString();

       }
        protected void btnPlay_Click(object sender, EventArgs e)
        {
            string result = HiddenField_result.Value;
            setTheme();
            checkForWin();
            setCards();
            setCurrentBalance();
            if (trial > currentGame.Trials)
                GameLogic.getNextGame(currentGame, money, currentGame.UserId);
            else
            {
                int trialsLeft = currentGame.Trials - trial;
                HiddenField_Trail.Value = trialsLeft.ToString();
            }

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
            if (CardBet.Equals("bet_R3"))
            {
                betAmount = currentGame.Bet_R3;
                if (WinLose.Equals("win"))                    
                    winningAmount = currentGame.Win_O1;

            }else if (CardBet.Equals("bet_R4")) { 
                betAmount = currentGame.Bet_R4;
                if (WinLose.Equals("win"))
                    winningAmount = currentGame.Win_O2;
            }

            money = Convert.ToInt32(HiddenField_currentBalance.Value) + betAmount + winningAmount;
            lblMoney.Text = money.ToString();
            SaveToDB(CardBet, betAmount, winningAmount);
        }
        public int[] getThemes()
        {
            int[] themearray = new int[] { };
            if (currentGame.Perc_S1 != 0)
            {
                themearray = new List<int>(themearray) { 1 }.ToArray();
            }
            if (currentGame.Perc_S2 != 0)
            {
                themearray = new List<int>(themearray) { 2 }.ToArray();
            }
            if (currentGame.Perc_S3 != 0)
            {
                themearray = new List<int>(themearray) { 3 }.ToArray();
            }
            if (currentGame.Perc_S4 != 0)
            {
                themearray = new List<int>(themearray) { 4 }.ToArray();
            }

            return themearray;
        }
        public String setTheme()
        {
            Random rnd = new Random();
            int randomTheme;
            int[] nr = getThemes();
            if (nr.Length == 0)
            {
                randomTheme = 0;
            }
            else
            {
                randomTheme = nr[rnd.Next(0, nr.Length)];
                //  var theme = _database.getTheme(randomTheme);
            }
            if (currentGame != null && currentGame.Name == "Instrumental_acq2")
            {
                HiddenField_theme.Value = "null";
                return "null";
            }
            else
            {
                //  var theme = _database.getTheme(randomTheme);
                if (randomTheme == 1 && currentGame.ThemeVariant != "A")
                {
                    if (currentGame.ThemeVariant == "B")
                        HiddenField_theme.Value = (randomTheme + 1).ToString();
                    else if (currentGame.ThemeVariant == "C")
                        HiddenField_theme.Value = (randomTheme + 2).ToString();

                }
                else
                    HiddenField_theme.Value = randomTheme.ToString();

                return HiddenField_theme.Value;
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

            pl.userid = currentGame.UserId;
            pl.balance_in = Convert.ToInt32(HiddenField_currentBalance.Value);
            pl.balance_out = money;
            pl.bet = betAmount;
            pl.condition = currentGame.Condition;
            pl.gamename = currentGame.Name;
            pl.moment = currentGame.Sequence;
            pl.outcome = winAmount;
            pl.response = CardBetResponse;
            pl.stimuli = currentGame.Name;
            pl.timestamp_begin = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time1.Value));//dDateTime.Now; // TIMEbegin.
            pl.timestamp_O = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time2.Value)); //DateTime.Now; // Time ? 
            pl.timestamp_R = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time3.Value)); // Time ? 

            pl.trial = trial++;

            _database.updatePlayerLog(pl);
        }
    }
}