using System;
using System.Collections.Generic;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            applyThemeTemp(); //temporarily added, to be removed when themes are finalized and triggering logic is done
            HiddenField_showInfo.Value = "0";
            if (!IsPostBack)
            {
                HiddenField_showInfo.Value = "1";
                HiddenField_Spin1.Value = randomStartCard().ToString();
                HiddenField_Spin2.Value = randomStartCard().ToString();
                HiddenField_Spin3.Value = randomStartCard().ToString();
               //checkForWin();
                setTheme();
            }
        }

        protected void btnPlay_Click(object sender, EventArgs e)
        {
            HiddenField_Spin1.Value = randomStartCard().ToString();
            HiddenField_Spin2.Value = randomStartCard().ToString();
            HiddenField_Spin3.Value = randomStartCard().ToString();
            checkForWin();
        }
        private void checkForWin()
        {
            //int CardPressed = 0; 
            //var winLose = HiddenField_WinLose.Value;
            var credit = 0;//Int32.Parse(lblMoney.Text);
            string WinChance = "";
            string CardColor = "";
            string WinLose = "";
            // string test = "PressCard:1, WinChance: 1, WinLose: lose";

            string[] splitCards = HiddenField_result.Value.Split(':');
            foreach (var value in splitCards)
            {
                WinChance = value[0].ToString();
                CardColor = value[1].ToString();
                WinLose = value[2].ToString();
            }
            //card1:null,card2:6H,showCard:5H,winChance:1,winLose:lose


            if (CardColor != "null")
            {

                if (CardColor == "1")
                {
                    money = +100;
                }
                else
                {
                    money = -100;
                }
            }
            else
            {
                if (CardColor == "2")
                {
                    money = +50;
                }
                else
                {
                    money = -50;
                }
            }
            money = money + credit;
            lblMoney.Text = money.ToString();
            //SaveToDB(CardPressed, winLose);
        }
        public int randomStartCard()
        {
            int randomfruit = rnd.Next(0, 6);

            return randomfruit;
        }
        public int setTheme()
        {
            Random rnd = new Random();
            int randomTheme = rnd.Next(1, 4);
            var theme = _database.getTheme(randomTheme);
            HiddenField_theme.Value = randomTheme.ToString();
            return randomTheme;
        }

        private void applyThemeTemp()
        {
            String themeName = Request.QueryString["theme"];
            if (themeName == null)
                themeCSS.Attributes["href"] = "src/css/themeGlow.css";
            else
            {
                switch (themeName)
                {
                    case "casino":
                        themeCSS.Attributes["href"] = "src/css/themeCasino.css";
                        break;
                    case "gold":
                        themeCSS.Attributes["href"] = "src/css/themeGold.css";
                        break;
                    case "black":
                        themeCSS.Attributes["href"] = "src/css/themeBlack.css";
                        break;
                    case "glow":
                        themeCSS.Attributes["href"] = "src/css/themeGlow.css";
                        break;
                    default:
                        themeCSS.Attributes["href"] = "src/css/theme1.css";
                        break;
                }

            }
        }
    }
}