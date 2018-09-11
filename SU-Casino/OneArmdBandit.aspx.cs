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
            
            if (currentGame.didWinSlot(rnd))
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
         
            setTheme();
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
            

            if (trial > currentGame.Trials)
                GameLogic.getNextGame(currentGame, money, currentGame.UserId);
        }
        private void checkForWin()
        {

            string[] splitCards = HiddenField_result.Value.Split(',');
            string WinChance = splitCards[0].ToString();

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

        public void setTheme()
        {          
            HiddenField_theme.Value = currentGame.getRandomThemeBasedOnProcAndVariant(rnd); 
        }

        private void setCurrentBalance()
        {
            HiddenField_currentBalance.Value = money.ToString(); //or get from DB?
        }

        public void SaveToDB(int betAmount, int winAmount)
        {
            Playerlog pl = new Playerlog();
            string themeToSave="";

            switch (HiddenField_theme.Value)
            {
                case "1": themeToSave= "perc_S1";
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
            if(themeToSave.Length == 0 )
                themeToSave = currentGame.Name;


            pl.userid = currentGame.UserId;
            pl.balance_in = Convert.ToInt32(HiddenField_currentBalance.Value);
            pl.balance_out = money;
            pl.bet = betAmount;
            pl.condition = currentGame.Condition;
            pl.gamename = currentGame.Name;
            pl.moment = currentGame.Sequence;
            pl.outcome = winAmount;
            pl.response = "bet_R1";
            pl.stimuli = themeToSave;
            pl.timestamp_begin = DateTime.Now;
            pl.timestamp_O = DateTime.Now;
            pl.timestamp_R = DateTime.Now;
            pl.trial = trial++;

            _database.updatePlayerLog(pl);
        }


    }
}