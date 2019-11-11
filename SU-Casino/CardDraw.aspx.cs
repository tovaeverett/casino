﻿using SU_Casino.game;
using SU_Casino.model;
using SU_Casino.util;
using System;
using System.Web;

namespace SU_Casino
{
    public partial class CardDraw : System.Web.UI.Page
    {
        private int startCard;
        private char startCardColor;
        public int money;
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
                gameSession.GameToPlay = GameDummy.getDummyGame(GameName.Transfer_test);
            }
            HiddenField_showInfo.Value = "0";
            switch (gameSession.GameToPlay.Name)
            {
                case "DET_realworld":
                    Hiddenfield_text.Value = gameSession.GetText(AllTextType.playCardWinFreezeInfo);
                    break;
                case "Transfer_test":
                    Hiddenfield_text.Value = gameSession.GetText(AllTextType.playCardNoSaldoInfo);
                    break;
                default:
                    Hiddenfield_text.Value = gameSession.GetText(AllTextType.playCardInfo1);
                    break;
            }

            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request["workerId"]))
                    gameSession.GameToPlay.UserId = Request["workerId"];

                SetTheme();
                SetCards();
                HiddenField_game.Value = gameSession.GameToPlay.Name;
                HiddenField_win1.Value = gameSession.GameToPlay.Win_O1.ToString();
                HiddenField_win2.Value = gameSession.GameToPlay.Win_O2.ToString();
                HiddenField_Bet_Card1.Value = gameSession.GameToPlay.Bet_R1.ToString();
                HiddenField_Bet_Card2.Value = gameSession.GameToPlay.Bet_R2.ToString();
                money = gameSession.GameToPlay.Saldo;   //Convert.ToInt32(Request["saldo"]);
                lblMoney.Text = money.ToString();
                HiddenField_showInfo.Value = "1";
                //trial = 1;
                gameSession.GameToPlay.TrialCount = 1;
                SetCurrentBalance();
                HiddenField_Trail.Value = gameSession.GameToPlay.Trials.ToString();
            }
        }

        public string RandomCard(string cardPosition)
        {
            // få array med "förlorar" korten
            var losingCards = gameSession.GameToPlay.RetrieveLosingNumbers(1, 13, startCard);

            // vann eller vann inte?
            if (gameSession.GameToPlay.DidWinDrawCards(cardPosition))
            {
                return startCard.ToString() + startCardColor;
            }
            else
            {
                int randomcardIndex = RandomSingleton.Next(0, losingCards.Length);

                int randomcard = losingCards[randomcardIndex];
                // string url = "src/images/cards/" + randomcard + "C.png";
                string card = randomcard.ToString() + GetColor();
                return card;
            }
        }

        private char GetColor()
        {
            int num = RandomSingleton.Next(0, 4);
            return "SHDC".ToCharArray()[num];
        }


        public string RandomStartCard()
        {
            int randomcard = RandomSingleton.Next(1, 14);
            startCard = randomcard;
            startCardColor = GetColor();
            return randomcard.ToString() + startCardColor;
        }

        public void SetCards()
        {
            HiddenField_card3.Value = RandomStartCard().ToString();
            HiddenField_card2.Value = RandomCard("R2").ToString();
            HiddenField_card1.Value = RandomCard("R1").ToString();

        }
        protected void btnPlay_Click(object sender, EventArgs e)
        {
            //string result = HiddenField_result.Value;
            SetTheme();
            CheckForWin();
            SetCards();
            SetCurrentBalance();
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
        private void CheckForWin()
        {
            //var credit = Int32.Parse(lblMoney.Text);
            string WinChance = "";
            string CardBet = "";
            string WinLose = "";

            string[] splitCards = HiddenField_result.Value.Split(',');
            //( foreach(var value in splitCards)

            WinChance = GetQuestionForWinChanceText(splitCards[0].ToString());
            CardBet = splitCards[1].ToString();
            WinLose = splitCards[2].ToString();

            //card1:null,card2:6H,showCard:5H,winChance:1,winLose:lose
            int winningAmount = 0;
            int betAmount = 0;
            if (CardBet.Equals("bet_R1"))
            {
                betAmount = gameSession.GameToPlay.Bet_R1;
                if (WinLose.Equals("win"))
                    winningAmount = gameSession.GameToPlay.Win_O1;

            }
            else if (CardBet.Equals("bet_R2"))
            {
                betAmount = gameSession.GameToPlay.Bet_R2;
                if (WinLose.Equals("win"))
                    winningAmount = gameSession.GameToPlay.Win_O2;
            }

            if (gameSession.GameToPlay.Name != "DET_realworld" && WinLose.Equals("win"))
                money = Convert.ToInt32(HiddenField_currentBalance.Value) + winningAmount;
            else
                money = Convert.ToInt32(HiddenField_currentBalance.Value) + betAmount;



            lblMoney.Text = money.ToString();
            SaveToDB(CardBet, betAmount, winningAmount, WinChance);
        }

        public string SetTheme()
        {

            if (gameSession.GameToPlay != null && gameSession.GameToPlay.Name == "Instrumental_acq")
            {
                HiddenField_theme.Value = "null";
                return "null";
            }
            else
            {
                HiddenField_theme.Value = gameSession.GameToPlay.GetRandomThemeBasedOnProcAndVariant();
                return HiddenField_theme.Value;
            }
        }

        public void SetCurrentBalance()
        {
            //db -> getCredit();
            HiddenField_currentBalance.Value = money.ToString();
        }

        //TODO refactor this.
        private string GetQuestionForWinChanceText(string questionForWinChanceId)
        {
            switch (questionForWinChanceId)
            {
                case "3":
                    return "Blue deck";
                case "2":
                    return "Red deck";
                case "0":
                    return "Don't know";
                default:
                    return "";
            }
        }

        public void SaveToDB(string CardBetResponse, int betAmount, int winAmount, string questionForWinChance)
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
            pl.response = CardBetResponse;
            pl.stimuli = themeToSave;
            pl.timestamp_begin = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time1.Value)).ToLocalTime();
            pl.timestamp_O = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time2.Value)).ToLocalTime();
            pl.timestamp_R = new DateTime(1970, 01, 01).AddMilliseconds(Convert.ToInt64(HiddenField_Time3.Value)).ToLocalTime();
            pl.trial = gameSession.GameToPlay.TrialCount++;
            pl.questionForWinChance = questionForWinChance;

            //Store the card that is flipped
            switch (HiddenField_FlippedCard.Value)
            {
                case "card1":
                    pl.figure1 = HiddenField_card1.Value;
                    pl.figure2 = "";
                    break;
                case "card2":
                    pl.figure1 = "";
                    pl.figure2 = HiddenField_card2.Value;
                    break;
                default:
                    pl.figure1 = "";
                    pl.figure2 = "";
                    break;
            } 
            //Store the middle card that is shown from start
            pl.figure3 = HiddenField_card3.Value;

            gameSession.UpdatePlayerLog(pl);
        }
    }
}