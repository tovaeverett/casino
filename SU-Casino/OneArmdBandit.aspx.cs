using SU_Casino.game;
using SU_Casino.model;
using SU_Casino.util;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace SU_Casino
{
    public partial class OneArmdBandit : System.Web.UI.Page
    {
        int money;
        GameSession gameSession;

        private void LoadGameSession()
        {
            if (Session["GameSession"] == null)
                Session["GameSession"] = new GameSession();

            gameSession = (GameSession) Session["GameSession"];
        }

        private void MoveToNextGame(int cuttrentBalance)
        {
            gameSession.GameToPlay.Saldo = cuttrentBalance;
            gameSession.LoadNextGame();
            string gameUrl = gameSession.GetGameUUrl();
            HttpContext.Current.Response.Redirect(gameUrl);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGameSession();
            if (gameSession.GameToPlay == null)
            {
                gameSession.GameToPlay = GameDummy.getDummyGame(GameName.Pavlovian_extinct);
            }

            HiddenField_showInfo.Value = "0";

            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request["workerId"]))
                    gameSession.GameToPlay.UserId = Request["workerId"];
//                jackpotInfo.Visible = false;
                setTheme();
                setInfoText();
                ShowJackpotText();
                showJackpotBanner();
                HiddenField_CloseToWin.Value = gameSession.GameToPlay.CloseToWinColour.ToString();
                SpinIt();
                //HiddenField_credit.Value = gameSession.gameToPlay.Win_O1.ToString();
                HiddenField_credit.Value = GetWinAmount(gameSession.GameToPlay).ToString();

                HiddenField_WinLose.Value =
                    HiddenField_Spin1.Value.Equals(HiddenField_Spin2.Value) &&
                    HiddenField_Spin1.Value.Equals(HiddenField_Spin3.Value)
                        ? "win"
                        : "lose";
                HiddenField_Bet_R1.Value = gameSession.GameToPlay.Bet_R1.ToString();
                money = gameSession.GameToPlay.Saldo;
                lblMoney.Text = money.ToString();
                setCurrentBalance();
                gameSession.GameToPlay.TrialCount = 1;

                HiddenField_Trail.Value = gameSession.GameToPlay.Trials.ToString();
                HiddenField_Multiply.Value = gameSession.GameToPlay.Multiplier.ToString();
                HiddenField_spin_delay1.Value = gameSession.GameToPlay.SpinDelay1.ToString();
                HiddenField_spin_delay2.Value = gameSession.GameToPlay.SpinDelay2.ToString();
                showMultiplier();
            }
        }

        private void setInfoText()
        {
            HiddenField_showInfo.Value = "1";
            string infoTextType = gameSession.GameToPlay.InfoTextType;
            if (infoTextType == "")
            {
                startInfo.Visible = false;

//                Add Jackpot text here
            }
            else
            {
                //                    OR  ? HiddenField_showInfo.Value = "0";
                Hiddenfield_text.Value =
                    gameSession.GetText((AllTextType) Enum.Parse(typeof(AllTextType), infoTextType));
            }
        }

        private void ShowJackpotText()
        {
            string jackpotTextType = gameSession.GameToPlay.JackpotTextType;
            if (jackpotTextType == "")
            {
                jackpotInfo.Visible = false;
            }
            else
            {
                HiddenField_jackpot_text.Value =
                    gameSession.GetText((AllTextType) Enum.Parse(typeof(AllTextType), jackpotTextType));
                int jackpot_time = gameSession.GameToPlay.JackpotTime;
                if (jackpot_time == null)
                {
                    jackpot_time = 3;
                }

                HiddenField_jackpot_time.Value = jackpot_time.ToString();
            }
        }

        private void showJackpotBanner()
        {
            string bannerType = gameSession.GameToPlay.BannerTextType;
            if (bannerType == "")
            {
                Jackpot_Banner.Visible = false;
            }
            else
            {
                bannerText.Text = gameSession.GetText((AllTextType) Enum.Parse(typeof(AllTextType), bannerType));
            }
        }

        private void SpinIt()
        {
            var firstSpin = randomSlotSpin();

            if (gameSession.GameToPlay.DidWinSlot())
            {
                HiddenField_Spin1.Value = firstSpin.ToString();
                HiddenField_Spin2.Value = firstSpin.ToString();
                HiddenField_Spin3.Value = firstSpin.ToString();
            }
            else
            {
                //LOST GAME 
                if (HiddenField_CloseToWin.Value.Equals("True"))
                {
                    //slot 1 and 2 should be same, but not 3
                    HiddenField_Spin1.Value = firstSpin.ToString();
                    HiddenField_Spin2.Value = firstSpin.ToString();
                    int thirdSpin;
                    // chansen finns ju alltid att random blir samma som första
                    // trots att man "bestämt" att användaren ska förlora
                    do
                    {
                        thirdSpin = randomSlotSpin();
                    } while (firstSpin == thirdSpin);

                    HiddenField_Spin3.Value = thirdSpin.ToString();
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
        }

        protected void btnPlay_Click(object sender, EventArgs e)
        {
            checkForWin();

            setTheme();
            HiddenField_credit.Value = GetWinAmount(gameSession.GameToPlay).ToString();

            SpinIt();
            if (Convert.ToInt32(HiddenField_Spin1.Value) == Convert.ToInt32(HiddenField_Spin2.Value) &&
                Convert.ToInt32(HiddenField_Spin1.Value) == Convert.ToInt32(HiddenField_Spin3.Value))
            {
                HiddenField_WinLose.Value = "win";
            }
            else
            {
                HiddenField_WinLose.Value = "lose";
            }

            setCurrentBalance();


            if (gameSession.GameToPlay.TrialCount > gameSession.GameToPlay.Trials)
            {
                MoveToNextGame(money);
            }
            else
            {
                int trialsLeft = gameSession.GameToPlay.Trials - gameSession.GameToPlay.TrialCount;
                HiddenField_Trail.Value = trialsLeft.ToString();
            }
        }

//        private string winOrLose()
//        {
//            bool winner = false;
////            if all are non equal
//            if (!(HiddenField_Spin1.Value.Equals(HiddenField_Spin2.Value)) && 
//                !(HiddenField_Spin1.Value.Equals(HiddenField_Spin3.Value)) && 
//                !(HiddenField_Spin2.Value.Equals(HiddenField_Spin3.Value))
//                )
//            {
//                HiddenField_CloseToWin.Value = "none Are same";
//            }
//            else
//            {
//                HiddenField_CloseToWin.Value = "some are same";
//            }
//
//            if (HiddenField_Spin1.Value.Equals(HiddenField_Spin2.Value) &&
//                HiddenField_Spin1.Value.Equals(HiddenField_Spin3.Value))
//            {
//                winner = true;
//            }
//
//            if (winner)
//                return "win";
//            return "lose";
//        }

        private void checkForWin()
        {
            string[] splitCards = HiddenField_result.Value.Split(',');
            string WinChance = GetQuestionForWinChanceText(splitCards[0].ToString());
            ListItem multiplierListItem = multiplier.Items.FindByValue("multiplyItem");
            int winningAmount = 0;
            if (HiddenField_WinLose.Value == "win")
            {
                winningAmount = Convert.ToInt32(multiplierListItem.Selected
                    ? HiddenField_Multiplied_Credit.Value
                    : HiddenField_credit.Value);
                money = Convert.ToInt32(HiddenField_currentBalance.Value) + winningAmount;
            }
            else
            {
                money =
                    multiplierListItem.Selected
                        ? (Convert.ToInt32(HiddenField_currentBalance.Value) +
                           Convert.ToInt32(HiddenField_Multiplied_Bet_R1.Value))
                        : Convert.ToInt32(HiddenField_currentBalance.Value) + gameSession.GameToPlay.Bet_R1;
            }

            lblMoney.Text = money.ToString();
            SaveToDB(gameSession.GameToPlay.Bet_R1, winningAmount, WinChance);
        }

        public int randomSlotSpin()
        {
            int randomfruit = RandomSingleton.Next(1, 4);
            return randomfruit;
        }

        public void showMultiplier()
        {
            if (gameSession.GameToPlay.Multiplier > 1)
            {
                HiddenField_Multiply.Value = gameSession.GameToPlay.Multiplier.ToString();
                int multipliedBet = gameSession.GameToPlay.Multiplier * Convert.ToInt32(HiddenField_Bet_R1.Value);
                int multipliedCredit = gameSession.GameToPlay.Multiplier * Convert.ToInt32(HiddenField_credit.Value);
                HiddenField_Multiplied_Bet_R1.Value = multipliedBet.ToString();
                HiddenField_Multiplied_Credit.Value = multipliedCredit.ToString();
            }
            else
            {
                HiddenField_Multiply.Value = null;
            }
        }

        public void showJackpotText()
        {
            if (gameSession.GameToPlay.JackpotTextType != null)
            {
//                HiddenField_jackpot_text.Value = gameSession.GameToPlay.JackpotTextTypes.ToString();
            }
        }


        public void setTheme()
        {
            HiddenField_theme.Value = gameSession.GameToPlay.GetRandomThemeBasedOnProcAndVariant();
        }

        private void setCurrentBalance()
        {
            HiddenField_currentBalance.Value = money.ToString(); //or get from DB?
        }

        //TODO refactor this.
        private string GetQuestionForWinChanceText(string questionForWinChanceId)
        {
            switch (questionForWinChanceId)
            {
                case "3":
                    return "High";
                case "2":
                    return "Low";
                case "1":
                    return "Zero";
                case "0":
                    return "Don't know";
                default:
                    return "";
            }
        }

        public void SaveToDB(int betAmount, int winAmount, string questionForWinChance)
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
                themeToSave = gameSession.GameToPlay.Name;


            pl.userid = gameSession.GameToPlay.UserId;
            pl.balance_in = Convert.ToInt32(HiddenField_currentBalance.Value);
            pl.balance_out = money;
            pl.bet = betAmount;
            pl.condition = gameSession.GameToPlay.Condition;
            pl.gamename = gameSession.GameToPlay.Name;
            pl.moment = gameSession.GameToPlay.Sequence;
            pl.outcome = winAmount;
            pl.response = "bet_R1";
            pl.stimuli = themeToSave;
            pl.timestamp_begin = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time1.Value))
                .ToLocalTime();
            pl.timestamp_O = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time2.Value))
                .ToLocalTime();
            pl.timestamp_R = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time3.Value))
                .ToLocalTime();
            pl.trial = gameSession.GameToPlay.TrialCount++;
            pl.questionForWinChance = questionForWinChance;
            gameSession.UpdatePlayerLog(pl);
        }

        public int GetWinAmount(Game game)
        {
            switch (game.CurrentTheme)
            {
                case "1":
                    return (game.IfS1win.Equals("O1")) ? game.Win_O1 : game.Win_O2;
                case "2":
                    return (game.IfS2win.Equals("O1")) ? game.Win_O1 : game.Win_O2;
                case "3":
                    return (game.IfS3win.Equals("O1")) ? game.Win_O1 : game.Win_O2;
                case "4":
                    return (game.IfS4win.Equals("O1")) ? game.Win_O1 : game.Win_O2;
                default:
                    return -1;
            }
        }
    }
}