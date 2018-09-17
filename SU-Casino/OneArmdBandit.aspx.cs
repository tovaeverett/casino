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
    public partial class OneArmdBandit : System.Web.UI.Page
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
                gamesSssion.gameToPlay = Game.getDummyGame();
            }

            HiddenField_showInfo.Value = "0";
            if (!IsPostBack)
            {
                gamesSssion.gameToPlay.UserId = Request["workerId"];
                setTheme();
                HiddenField_showInfo.Value = "1";
                Hiddenfield_text.Value = gamesSssion.GetText(InfoTextType.playSlotInfo);
                SpinIt();
                HiddenField_credit.Value = gamesSssion.gameToPlay.Win_O1.ToString();
                HiddenField_WinLose.Value = HiddenField_Spin1.Value.Equals(HiddenField_Spin2.Value) && HiddenField_Spin1.Value.Equals(HiddenField_Spin3.Value) ? "win" : "lose";
                money = gamesSssion.gameToPlay.Saldo;
                lblMoney.Text = money.ToString();
                setCurrentBalance();
                gamesSssion.gameToPlay.TrialCount = 1;
                HiddenField_Trail.Value = gamesSssion.gameToPlay.Trials.ToString();
            }
        }

        private void SpinIt()
        {
            var firstSpin = randomSlotSpin();
            
            if (gamesSssion.gameToPlay.didWinSlot())
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


            if (gamesSssion.gameToPlay.TrialCount > gamesSssion.gameToPlay.Trials)
            {
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

            string[] splitCards = HiddenField_result.Value.Split(',');
            string WinChance = splitCards[0].ToString();

            int winningAmount = 0;
            if (HiddenField_WinLose.Value == "win")
            {
                winningAmount = gamesSssion.gameToPlay.Win_O1;
            }

            money = Convert.ToInt32(HiddenField_currentBalance.Value) + gamesSssion.gameToPlay.Bet_R1 + winningAmount;

            lblMoney.Text = money.ToString();
            SaveToDB(gamesSssion.gameToPlay.Bet_R1, winningAmount);
        }
        public int randomSlotSpin()
        {
            int randomfruit = RandomSingleton.Next(1, 4);
            return randomfruit;
        }



        public void setTheme()
        {          
            HiddenField_theme.Value = gamesSssion.gameToPlay.getRandomThemeBasedOnProcAndVariant(); 
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
                case "1": themeToSave= "S1A";
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
            if(themeToSave.Length == 0 )
                themeToSave = gamesSssion.gameToPlay.Name;


            pl.userid = gamesSssion.gameToPlay.UserId;
            pl.balance_in = Convert.ToInt32(HiddenField_currentBalance.Value);
            pl.balance_out = money;
            pl.bet = betAmount;
            pl.condition = gamesSssion.gameToPlay.Condition;
            pl.gamename = gamesSssion.gameToPlay.Name;
            pl.moment = gamesSssion.gameToPlay.Sequence;
            pl.outcome = winAmount;
            pl.response = "bet_R1";
            pl.stimuli = themeToSave;
            pl.timestamp_begin = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time1.Value)).ToLocalTime();
            pl.timestamp_O = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time2.Value)).ToLocalTime(); 
            pl.timestamp_R = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time3.Value)).ToLocalTime(); 
            pl.trial = gamesSssion.gameToPlay.TrialCount++;

            gamesSssion.UpdatePlayerLog(pl);
        }


    }
}