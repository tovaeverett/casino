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
            if (!IsPostBack)
            {
                HiddenFieldrouletteNr.Value = RandomSpin().ToString();
                money = currentGame.Saldo;
                lblMoney.Text = money.ToString();
                RandomSpin();
                HiddenField_showInfo.Value = "1";
                Hiddenfield_text.Value = _database.getText("playRouletteInfo");
                HiddenField_credit.Value = currentGame.Win_O1.ToString();
                setCurrentBalance();
                trial = 1;
            }

        }

        //TODO : Code cleanup, unused method?
        public int GetCssRandom()
        {
            Random rnd = new Random();

            var rndNr = rnd.Next(0, 2);

            return rndNr;
        }

        public int RandomSpin()
        {
            Random rnd = new Random();
            int randomNr = rnd.Next(1, 37);
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
                betAmount = currentGame.Bet_R1;
                if (WinLose.Equals("win"))
                    winningAmount = currentGame.Win_O1;

            }
            else if (bet.Equals("bet_R2"))
            {
                betAmount = currentGame.Bet_R2;
                if (WinLose.Equals("win"))
                    winningAmount = currentGame.Win_O2;
            }

            money = Convert.ToInt32(HiddenField_currentBalance.Value) + betAmount + winningAmount;
            lblMoney.Text = money.ToString();
            SaveToDB(bet, betAmount, winningAmount);
            setCurrentBalance();
        }
        protected void btnPlay_Click(object sender, EventArgs e)
        {
            checkForWin();
            if (trial > currentGame.Trials)
                GameLogic.getNextGame(currentGame,money);

            RandomSpin();

            // credit = isWin ? credit + 500 : credit - 500;
            // lblMoney.Text = credit.ToString();
        }

        private void setCurrentBalance()
        {
            HiddenField_currentBalance.Value = money.ToString(); //or get from DB?
        }

        public void SaveToDB(String bet, int betAmount, int winAmount)
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
            pl.response = bet;
            pl.stimuli = currentGame.Name;
            pl.timestamp_begin = DateTime.Now;
            pl.timestamp_O = DateTime.Now;
            pl.timestamp_R = DateTime.Now;
            pl.trial = trial++;

            _database.updatePlayerLog(pl);
        }
    }
}