using SU_Casino.game;
using SU_Casino.model;
using SU_Casino.util;
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
    public partial class Roulette : System.Web.UI.Page
    {
        int money;
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
                gamesSssion.gameToPlay = GameDummy.getDummyGame(GameName.Roulette);
            }            

            HiddenField_showInfo.Value = "0";
            if (!IsPostBack)
            {
                if (!String.IsNullOrWhiteSpace(Request["workerId"]))
                    gamesSssion.gameToPlay.UserId = Request["workerId"];
                
                HiddenFieldrouletteNr.Value = RandomSpin().ToString();
                money = gamesSssion.gameToPlay.Saldo;
                lblMoney.Text = money.ToString();
                RandomSpin();
                HiddenField_showInfo.Value = "1";                
                Hiddenfield_text.Value = gamesSssion.GetText(InfoTextType.playRouletteInfo);
                HiddenField_credit.Value = gamesSssion.gameToPlay.Win_O1.ToString();
                setCurrentBalance();
                gamesSssion.gameToPlay.TrialCount = 1;
            }
            

        }

        public int RandomSpin()
        {
            int randomNr = RandomSingleton.Next(1, 37);
            return randomNr;
        }

        public static bool IsEven(int value)
        {
            return value % 2 == 0;
        }
        private void checkForWin()
        {
            string bet = "";
            string WinLose = "";
            string[] result = HiddenField_result.Value.Split(',');
            bet = result[1].ToString();
            WinLose = result[2].ToString();

            int winningAmount = 0;
            int betAmount = 0;
            if (bet.Equals("bet_R1"))
            {
                betAmount = gamesSssion.gameToPlay.Bet_R1;
                if (WinLose.Equals("win"))
                    winningAmount = gamesSssion.gameToPlay.Win_O1;

            }
            else if (bet.Equals("bet_R2"))
            {
                betAmount = gamesSssion.gameToPlay.Bet_R2;
                if (WinLose.Equals("win"))
                    winningAmount = gamesSssion.gameToPlay.Win_O1;
            }

            money = Convert.ToInt32(HiddenField_currentBalance.Value) + betAmount + winningAmount;
            lblMoney.Text = money.ToString();
            SaveToDB(bet, betAmount, winningAmount);
            setCurrentBalance();
        }
        protected void btnPlay_Click(object sender, EventArgs e)
        {
            checkForWin();
            if (gamesSssion.gameToPlay.TrialCount > gamesSssion.gameToPlay.Trials)
            {
                MoveToNextGame(money);
            }

            RandomSpin();
        }

        private void setCurrentBalance()
        {
            HiddenField_currentBalance.Value = money.ToString(); //or get from DB?
        }

        public void SaveToDB(String bet, int betAmount, int winAmount)
        {
            Playerlog pl = new Playerlog();

            pl.userid = gamesSssion.gameToPlay.UserId; 
            pl.balance_in = Convert.ToInt32(HiddenField_currentBalance.Value);
            pl.balance_out = money;
            pl.bet = betAmount;
            pl.condition = gamesSssion.gameToPlay.Condition;
            pl.gamename = gamesSssion.gameToPlay.Name;
            pl.moment = gamesSssion.gameToPlay.Sequence;
            pl.outcome = winAmount;
            pl.response = bet;
            pl.stimuli = "S0";
            pl.timestamp_begin = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time1.Value));
            pl.timestamp_O = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time2.Value)); 
            pl.timestamp_R = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time3.Value));
            pl.trial = gamesSssion.gameToPlay.TrialCount++;
            pl.questionForWinChance = "";

            gamesSssion.UpdatePlayerLog(pl);            
        }
    }
}