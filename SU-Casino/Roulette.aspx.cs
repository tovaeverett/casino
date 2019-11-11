using SU_Casino.game;
using SU_Casino.model;
using SU_Casino.util;
using System;
using System.Web;

namespace SU_Casino
{
    public partial class Roulette : System.Web.UI.Page
    {
        int money;
        GameSession gameSession;

        private void LoadGameSession()
        {
            if (Session["GameSession"] == null)
                Session["GameSession"] = new GameSession();

            gameSession = (GameSession)Session["GameSession"];
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
                gameSession.GameToPlay = GameDummy.getDummyGame(GameName.Roulette);
            }

            HiddenField_showInfo.Value = "0";
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request["workerId"]))
                    gameSession.GameToPlay.UserId = Request["workerId"];

                HiddenFieldrouletteNr.Value = RandomSpin().ToString();
                money = gameSession.GameToPlay.Saldo;
                lblMoney.Text = money.ToString();
                RandomSpin();
                HiddenField_showInfo.Value = "1";
                Hiddenfield_text.Value = gameSession.GetText(AllTextType.playRouletteInfo1);
                HiddenField_credit.Value = gameSession.GameToPlay.Win_O1.ToString();
                HiddenField_Bet_R1.Value = gameSession.GameToPlay.Bet_R1.ToString();
                HiddenField_Bet_R2.Value = gameSession.GameToPlay.Bet_R2.ToString();
                setCurrentBalance();
                gameSession.GameToPlay.TrialCount = 1;
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
                betAmount = gameSession.GameToPlay.Bet_R1;
            }
            else if (bet.Equals("bet_R2"))
            {
                betAmount = gameSession.GameToPlay.Bet_R2;
            }

            if (WinLose.Equals("win"))
            {
                winningAmount = gameSession.GameToPlay.Win_O1;
                money = Convert.ToInt32(HiddenField_currentBalance.Value) + winningAmount;
            }
            else
            {
                money = Convert.ToInt32(HiddenField_currentBalance.Value) + betAmount;
            }


            lblMoney.Text = money.ToString();
            SaveToDB(bet, betAmount, winningAmount);
            setCurrentBalance();
        }
        protected void btnPlay_Click(object sender, EventArgs e)
        {
            checkForWin();
            if (gameSession.GameToPlay.TrialCount > gameSession.GameToPlay.Trials)
            {
                MoveToNextGame(money);
            }

            RandomSpin();
        }

        private void setCurrentBalance()
        {
            HiddenField_currentBalance.Value = money.ToString(); //or get from DB?
        }

        public void SaveToDB(string bet, int betAmount, int winAmount)
        {
            Playerlog pl = new Playerlog();

            pl.userid = gameSession.GameToPlay.UserId;
            pl.balance_in = Convert.ToInt32(HiddenField_currentBalance.Value);
            pl.balance_out = money;
            pl.bet = betAmount;
            pl.condition = gameSession.GameToPlay.Condition;
            pl.gamename = gameSession.GameToPlay.Name;
            pl.moment = gameSession.GameToPlay.Sequence;
            pl.outcome = winAmount;
            pl.response = bet;
            pl.stimuli = "S0";
            pl.timestamp_begin = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time1.Value));
            pl.timestamp_O = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time2.Value));
            pl.timestamp_R = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time3.Value));
            pl.trial = gameSession.GameToPlay.TrialCount++;
            pl.questionForWinChance = "";

            gameSession.UpdatePlayerLog(pl);
        }
    }
}