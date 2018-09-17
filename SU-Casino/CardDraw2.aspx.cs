using SU_Casino.game;
using SU_Casino.model;
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
        private int startCard;
        private char startCardColor;
        public int money;
        GamesSssion gamesSssion;

        private void LoadGameSessoin()
        {
            if (Session["GamesSssion"] == null)
                Session["GamesSssion"] = new GamesSssion();

            gamesSssion = (GamesSssion)Session["GamesSssion"];
        }

        private void MoveToNextGame(int cuttrentBalance)
        {
            gamesSssion.gameToPlay.Saldo = cuttrentBalance;
            gamesSssion.LoadNextGame();
            String gameUrl = gamesSssion.GetGameUUrl();
            HttpContext.Current.Response.Redirect(gameUrl);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGameSessoin();

            if (gamesSssion.gameToPlay == null)
            {
                gamesSssion.gameToPlay = Game.getDummyGame();
                gamesSssion.gameToPlay.getRandomThemeBasedOnProcAndVariant();                
            }

            HiddenField_showInfo.Value = "0";
            Hiddenfield_text.Value = gamesSssion.GetText(InfoTextType.playCardInfo);
            if (!IsPostBack)
            {
                gamesSssion.gameToPlay.UserId = Request["workerId"];
                setTheme();
                setCards();
                HiddenField_game.Value = gamesSssion.gameToPlay.Name;
                money = gamesSssion.gameToPlay.Saldo;
                HiddenField_win1.Value = gamesSssion.gameToPlay.Win_O1.ToString();
                HiddenField_win2.Value = gamesSssion.gameToPlay.Win_O2.ToString();
                lblMoney.Text = money.ToString();
                HiddenField_showInfo.Value = "1";
                gamesSssion.gameToPlay.TrialCount = 1;
                setCurrentBalance();
                HiddenField_Trail.Value = gamesSssion.gameToPlay.Trials.ToString();
            }
        }

        public string randomCard(string cardPosition)
        {
            
            // få array med "förlorar" korten
            var losingCards = gamesSssion.gameToPlay.RetrieveLosingNumbers(1, 13, startCard);

            // vann eller vann inte?
            if (gamesSssion.gameToPlay.didWinDrawCards(cardPosition))
            {
                return startCard.ToString() + startCardColor;
            }
            else
            {
                int randomcardIndex = RandomSingleton.Next(0, losingCards.Length);
                int randomcard = losingCards[randomcardIndex];
                string card = randomcard.ToString() + GetColor();
                return card;
            }
        }

        private char GetColor()
        {
            int num = RandomSingleton.Next(0, 4);
            return "SHDC".ToCharArray()[num];
        }
        
        public string randomStartCard()
        {
            int randomcard = RandomSingleton.Next(1, 14);
            startCard = randomcard;
            startCardColor = GetColor();
            return randomcard.ToString() + startCardColor;
        }

        public void setCards()
        {
            HiddenField_card3.Value = randomStartCard().ToString();
            HiddenField_card2.Value = randomCard("R4").ToString();
            HiddenField_card1.Value = randomCard("R3").ToString();

       }
        protected void btnPlay_Click(object sender, EventArgs e)
        {
            string result = HiddenField_result.Value;
            setTheme();
            checkForWin();
            setCards();
            setCurrentBalance();
            if (gamesSssion.gameToPlay.TrialCount > gamesSssion.gameToPlay.Trials) {
                MoveToNextGame(money);
            }                
            else
            {
                int trialsLeft = gamesSssion.gameToPlay.Trials - gamesSssion.gameToPlay.TrialCount;
                HiddenField_Trail.Value = trialsLeft.ToString();
            }

        }
        private void checkForWin()
        {
           //var winLose = HiddenField_WinLose.Value;
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
            if (CardBet.Equals("bet_R3"))
            {
                betAmount = gamesSssion.gameToPlay.Bet_R3;
                if (WinLose.Equals("win"))                    
                    winningAmount = gamesSssion.gameToPlay.Win_O1;

            }else if (CardBet.Equals("bet_R4")) { 
                betAmount = gamesSssion.gameToPlay.Bet_R4;
                if (WinLose.Equals("win"))
                    winningAmount = gamesSssion.gameToPlay.Win_O2;
            }

            money = Convert.ToInt32(HiddenField_currentBalance.Value) + betAmount + winningAmount;
            lblMoney.Text = money.ToString();
            SaveToDB(CardBet, betAmount, winningAmount);
        }
     
        public String setTheme()
        {

            if (gamesSssion.gameToPlay != null && gamesSssion.gameToPlay.Name == "Instrumental_acq2")
            {
                HiddenField_theme.Value = "null";
                return "null";
            }
            else
            {
                HiddenField_theme.Value = gamesSssion.gameToPlay.getRandomThemeBasedOnProcAndVariant();
                return HiddenField_theme.Value;
            }
        }

        public void setCurrentBalance()
        {
            //HiddenField_credit.Value = "100"; //db -> getCredit();
            HiddenField_currentBalance.Value = money.ToString();
        }

        public void SaveToDB(String CardBetResponse, int betAmount, int winAmount)
        {
            Playerlog pl = new Playerlog();
            string themeToSave = "";

            switch (HiddenField_theme.Value)
            {
                case "1":
                    themeToSave = "S1A";
                    break;
                case "2":
                    themeToSave = "S2";
                    break;
                case "3":
                    themeToSave = "S3";
                    break;
                case "4":
                    themeToSave = "S4";
                    break;
                case "5":
                    themeToSave = "S1B";
                    break;
                case "6":
                    themeToSave = "S1C";
                    break;
            }
            if (themeToSave.Length == 0)
                themeToSave = gamesSssion.gameToPlay.Name;

            pl.userid = gamesSssion.gameToPlay.UserId;
            pl.balance_in = Convert.ToInt32(HiddenField_currentBalance.Value);
            pl.balance_out = money;
            pl.bet = betAmount;
            pl.condition = gamesSssion.gameToPlay.Condition;
            pl.gamename = gamesSssion.gameToPlay.Name;
            pl.moment = gamesSssion.gameToPlay.Sequence;
            pl.outcome = winAmount;
            pl.response = CardBetResponse;
            pl.stimuli = themeToSave;
            pl.timestamp_begin = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time1.Value));
            pl.timestamp_O = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time2.Value)); 
            pl.timestamp_R = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time3.Value)); 

            pl.trial = gamesSssion.gameToPlay.TrialCount++;

            gamesSssion.UpdatePlayerLog(pl);
        }
    }
}