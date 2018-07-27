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
        private static int credit = 1500;
        int money;
        Database _database = new Database();
        public SqlConnection connectionstring = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            HiddenField_showInfo.Value = "0";
            if (!IsPostBack)
            {
                HiddenFieldrouletteNr.Value = RandomSpin().ToString();
                lblMoney.Text = credit.ToString();
                RandomSpin();
                HiddenField_showInfo.Value = "1";
                Hiddenfield_text.Value = _database.getText("playRouletteInfo");
            }
        }

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
            //int CardPressed = 0; 
            //var winLose = HiddenField_WinLose.Value;
            int credit = 0;
            if (lblMoney.Text != "")
            {
                credit = Convert.ToInt32(lblMoney.Text);
            }
            string WinChance = "";
            string CardColor = "";
            string WinLose = "";
            // string test = "PressCard:1, WinChance: 1, WinLose: lose";

            string[] splitCards = HiddenField_result.Value.Split(',');

            WinChance = splitCards[0].ToString();
            CardColor = splitCards[1].ToString();
            WinLose = splitCards[2].ToString();

            //card1:null,card2:6H,showCard:5H,winChance:1,winLose:lose


            if (WinLose == "win")
            {

                if (CardColor == "1")
                {
                    money = +500;
                }
                else
                {
                    money = -500;
                }
            }
            else
            {
                if (CardColor == "2")
                {
                    money = +500;
                }
                else
                {
                    money = -500;
                }
            }
            money = money + credit;
            lblMoney.Text = money.ToString();
            //SaveToDB(CardPressed, winLose);
        }
        protected void btnPlay_Click(object sender, EventArgs e)
        {
            checkForWin();
           // bool isWin = true; //Change...
            RandomSpin();
           // credit = isWin ? credit + 500 : credit - 500;
           // lblMoney.Text = credit.ToString();
        }
    }
}