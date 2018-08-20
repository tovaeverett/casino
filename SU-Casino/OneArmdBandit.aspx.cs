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
    public partial class OneArmdBandit : System.Web.UI.Page
    {
        Database _database = new Database();
        public SqlConnection connectionstring = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        Random rnd = new Random();
        int money;
        private Game currentGame;
        private static int trial;
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
                currentGame.UserId = Request["workerId"];
                setTheme();
                HiddenField_showInfo.Value = "1";
                Hiddenfield_text.Value = _database.getText("playSlotInfo");
                SpinIt();
                HiddenField_credit.Value = currentGame.Win_O1.ToString();
                if (HiddenField_Spin1.Value == HiddenField_Spin2.Value && HiddenField_Spin1.Value == HiddenField_Spin3.Value)
                {
                    HiddenField_WinLose.Value = "win";
                }
                else
                {
                    HiddenField_WinLose.Value = "lose";
                }
                money = currentGame.Saldo;   //Convert.ToInt32(Request["saldo"]);
                lblMoney.Text = money.ToString();
                setCurrentBalance();
                trial = 1;
                
            }
        }

        private void SpinIt()
        {
            var firstSpin = randomSlotSpin();
            
            if (currentGame.didWin())
            {
                HiddenField_Spin1.Value = firstSpin.ToString();
                HiddenField_Spin2.Value = firstSpin.ToString();
                HiddenField_Spin3.Value = firstSpin.ToString();
            }
            else
            {
                int secondSpin;

                // chansen finns ju alltid att random blir samma som första
                // trots att man "bestämt" att användaren ska förlora
                do
                {
                    secondSpin = randomSlotSpin();
                } while (firstSpin == secondSpin);

                HiddenField_Spin1.Value = firstSpin.ToString();
                HiddenField_Spin2.Value = secondSpin.ToString();
                HiddenField_Spin3.Value = randomSlotSpin().ToString();
            }
        }

        protected void btnPlay_Click(object sender, EventArgs e)
        {
            checkForWin();
            SpinIt();
            if (Convert.ToInt32(HiddenField_Spin1.Value) == Convert.ToInt32(HiddenField_Spin2.Value) && Convert.ToInt32(HiddenField_Spin1.Value) == Convert.ToInt32(HiddenField_Spin3.Value))
            {
                HiddenField_WinLose.Value = "win";
            }
            else
            {
                HiddenField_WinLose.Value = "lose";
            }
            
            setCurrentBalance();
            setTheme();

            if (trial > currentGame.Trials)
                GameLogic.getNextGame(currentGame, money, currentGame.UserId);
        }
        private void checkForWin()
        {
            string WinChance = "";

            string[] splitCards = HiddenField_result.Value.Split(',');
            WinChance = splitCards[0].ToString();

            int winningAmount = 0;
            if (HiddenField_WinLose.Value == "win")
            {
                winningAmount = currentGame.Win_O1;
            }

            money = Convert.ToInt32(HiddenField_currentBalance.Value) + currentGame.Bet_R1 + winningAmount;

            lblMoney.Text = money.ToString();
            SaveToDB(currentGame.Bet_R1, winningAmount);
        }
        public int randomSlotSpin()
        {
            int randomfruit = rnd.Next(1, 4);

            return randomfruit;
        }
        public int[] getThemes()
        {
            int[] themearray = new int[] { };
            if (currentGame.Perc_S1 != 0)
            {
                themearray = new List<int>(themearray) { 1 }.ToArray();
            }
            if(currentGame.Perc_S2 != 0)
            {
                themearray = new List<int>(themearray) { 2 }.ToArray();
            }
            if (currentGame.Perc_S3 != 0)
            {
                themearray = new List<int>(themearray) { 3 }.ToArray();
            }
            if (currentGame.Perc_S4 != 0)
            {
                themearray = new List<int>(themearray) {4 }.ToArray();
            }
            return themearray;
        }
        public int setTheme()
        {
            //   var theme = _database.getTheme(currentGame.Name);
            int[] nr = getThemes();
            int randomTheme;

            
            Random rnd = new Random();
            if (nr.Length == 0)
            {
                randomTheme = 0;
            }
            else
            { 
                randomTheme = nr[rnd.Next(0, nr.Length)];
            }
            // int randomTheme = rnd.Next(1, 4);
            HiddenField_theme.Value = randomTheme.ToString();
            
            if (randomTheme == 1 && currentGame.ThemeVariant != "A") //perc_S1 -> themeRed
            {
                if (currentGame.ThemeVariant == "B")
                    HiddenField_theme.Value = (randomTheme + 1).ToString();
                else if (currentGame.ThemeVariant == "C")
                    HiddenField_theme.Value = (randomTheme + 2).ToString();

            }
            else
                HiddenField_theme.Value = randomTheme.ToString();
            if(randomTheme == 1 && currentGame.IfS1probX.ToString() != "")
            {
                currentGame.Prob_O1 = currentGame.Prob_O1 * currentGame.IfS1probX;
            }
            if (randomTheme == 1 && currentGame.IfS2probX.ToString() != "")
            {
                currentGame.Prob_O2 = currentGame.Prob_O2 * currentGame.IfS2probX;
            }
            if (randomTheme == 2 && currentGame.IfS1probX.ToString() != "")
            {
                currentGame.Prob_O1 = currentGame.Prob_O1 * currentGame.IfS1probX;
            }
            if (randomTheme == 2 && currentGame.IfS2probX.ToString() != "")
            {
                currentGame.Prob_O2 = currentGame.Prob_O2 * currentGame.IfS2probX;
            }
            return randomTheme;
        }

        private void setCurrentBalance()
        {
            HiddenField_currentBalance.Value = money.ToString(); //or get from DB?
        }

        public void SaveToDB(int betAmount, int winAmount)
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
            pl.response = "bet_R1";
            pl.stimuli = currentGame.Name;
            pl.timestamp_begin = DateTime.Now;
            pl.timestamp_O = DateTime.Now;
            pl.timestamp_R = DateTime.Now;
            pl.trial = trial++;

            _database.updatePlayerLog(pl);
        }


    }
}