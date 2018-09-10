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
    public partial class CardDraw : System.Web.UI.Page
    {
        private int startCard;
        private char startCardColor;
        public int money;
        private Game currentGame;
        private static int trial;
        Random rnd = new Random();
    
        Database _database = new Database();

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
            switch (currentGame.Name)
            {
                case "DET_realworld":
                    Hiddenfield_text.Value = _database.getText("playCardWinFreezeInfo");
                    break;
                case "Transfer_test":
                    Hiddenfield_text.Value = _database.getText("playCardNoSaldoInfo");
                    break;
                default:
                    Hiddenfield_text.Value = _database.getText("playCardInfo");
                    break;
            }

            if (!IsPostBack)
            { 
                currentGame.UserId = Request["workerId"];
                setTheme();
                setCards();
                HiddenField_game.Value = currentGame.Name;
                HiddenField_win1.Value = currentGame.Win_O1.ToString();
                HiddenField_win2.Value = currentGame.Win_O2.ToString();
                money = currentGame.Saldo;   //Convert.ToInt32(Request["saldo"]);
                lblMoney.Text = money.ToString();
                HiddenField_showInfo.Value = "1";
                trial = 1;
                setCurrentBalance();
                HiddenField_Trail.Value = currentGame.Trials.ToString();
                //SaveToDB(); //Is this really needed? User has not begun playing yet
            }
        }

        public string randomCard(string cardPosition)
        {
            // få array med "förlorar" korten
            var losingCards = currentGame.RetrieveLosingNumbers(1, 13, startCard);

            // vann eller vann inte?
            if (currentGame.didWinDrawCards(cardPosition, rnd))
            {
                return startCard.ToString() + startCardColor;
            }
            else
            {
                int randomcardIndex = rnd.Next(0, losingCards.Length);

                int randomcard = losingCards[randomcardIndex];
                // string url = "src/images/cards/" + randomcard + "C.png";
                string card = randomcard.ToString() + GetColor();
                return card;
            }
        }
             
        private char GetColor()
        {
            int num = rnd.Next(0, 4);
            return "SHDC".ToCharArray()[num];
        }
        
        
        public string randomStartCard()
        {
            int randomcard = rnd.Next(1, 14);
            startCard = randomcard;
            startCardColor = GetColor();
            return randomcard.ToString() + startCardColor;
        }

        public void setCards()
        {
            HiddenField_card3.Value = randomStartCard().ToString();
            HiddenField_card2.Value = randomCard("R2").ToString();
            HiddenField_card1.Value = randomCard("R1").ToString();

       }
        protected void btnPlay_Click(object sender, EventArgs e)
        {
                string result = HiddenField_result.Value;
                setTheme();
                checkForWin();
                setCards();
                setCurrentBalance();
                if (trial > currentGame.Trials)
                    GameLogic.getNextGame(currentGame, money,currentGame.UserId);
                else {
                    int trialsLeft = currentGame.Trials - trial;
                    HiddenField_Trail.Value = trialsLeft.ToString();
                }
        }
        private void checkForWin()
        {
            var credit = Int32.Parse(lblMoney.Text);
            string WinChance = "";
            string CardBet = "";
            string WinLose = "";
           
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

            if (currentGame.Name != "DET_realworld")
                money = Convert.ToInt32(HiddenField_currentBalance.Value) + betAmount + winningAmount;
            else
                money = Convert.ToInt32(HiddenField_currentBalance.Value) + betAmount;


            lblMoney.Text = money.ToString();
            SaveToDB(CardBet, betAmount, winningAmount);
        }

        public string setTheme()
        {

            if (currentGame != null && currentGame.Name == "Instrumental_acq")
            {
                HiddenField_theme.Value = "null";
                return "null";
            }
            else
            {
                HiddenField_theme.Value = currentGame.getRandomThemeBasedOnProcAndVariant(rnd);
                return HiddenField_theme.Value;
            }
        }

        public void setCurrentBalance()
        {
             //db -> getCredit();
            HiddenField_currentBalance.Value = money.ToString();
        }

        //TODO check if these initial values are correct, or may be we do not need this method at all?


        public void SaveToDB(String CardBetResponse, int betAmount, int winAmount)
        {
            Playerlog pl = new Playerlog();
            string themeToSave = "";

            switch (HiddenField_theme.Value)
            {
                case "1":
                    themeToSave = "perc_S1";
                    break;
                case "2":
                    themeToSave = "perc_S2";
                    break;
                case "3":
                    themeToSave = "perc_S3";
                    break;
                case "4":
                    themeToSave = "perc_S4";
                    break;
            }
            if (themeToSave.Length == 0)
                themeToSave = currentGame.Name;

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
            pl.timestamp_begin = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time1.Value)).ToLocalTime(); //dDateTime.Now; // TIMEbegin.
            pl.timestamp_O = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time2.Value)).ToLocalTime(); //DateTime.Now; // Time ? 
            pl.timestamp_R = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time3.Value)).ToLocalTime(); // Time ? 
            pl.trial = trial++;

            _database.updatePlayerLog(pl);
        }
    }
}