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
        protected void Page_Load(object sender, EventArgs e)
        {
           // applyThemeTemp(); //temporarily added, to be removed when themes are finalized and triggering logic is done
            HiddenField_showInfo.Value = "0";
            if (!IsPostBack)
            {
                HiddenField_showInfo.Value = "1";
                Hiddenfield_text.Value = _database.getText("playSlotInfo");
                HiddenField_Spin1.Value = randomStartCard().ToString();
                HiddenField_Spin2.Value = randomStartCard().ToString();
                HiddenField_Spin3.Value = randomStartCard().ToString();
                if (HiddenField_Spin1.Value == HiddenField_Spin2.Value && HiddenField_Spin1.Value == HiddenField_Spin3.Value)
                {
                    HiddenField_WinLose.Value = "win";
                }
                else
                {
                    HiddenField_WinLose.Value = "lose";
                }
                setTheme();
            }
        }

        protected void btnPlay_Click(object sender, EventArgs e)
        {
            HiddenField_Spin1.Value = randomStartCard().ToString();
            HiddenField_Spin2.Value = randomStartCard().ToString();
            HiddenField_Spin3.Value = randomStartCard().ToString();
            if (Convert.ToInt32(HiddenField_Spin1.Value) == Convert.ToInt32(HiddenField_Spin2.Value) && Convert.ToInt32(HiddenField_Spin1.Value) == Convert.ToInt32(HiddenField_Spin3.Value))
            {
                HiddenField_WinLose.Value = "win";
            }
            else
            {
                HiddenField_WinLose.Value = "lose";
            }
            checkForWin();
        }
        private void checkForWin()
        {
 
            //int CardPressed = 0; 
            //var winLose = HiddenField_WinLose.Value;
                int credit = 0;
            if(lblMoney.Text != "")
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


            if (HiddenField_WinLose.Value == "win")
            {
              money = +100;
                
            }
            else
            {
                money = -100;
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
          //  var theme = _database.getTheme(randomTheme);
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
                        themeCSS.Attributes["href"] = "src/css/theme2.css";
                        break;
                }

            }
        }
    }
}