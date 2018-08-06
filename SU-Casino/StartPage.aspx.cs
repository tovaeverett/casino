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
    public partial class StartPage : System.Web.UI.Page
    {
        Database _database = new Database();
        public SqlConnection connectionstring = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        private static Game initialGame;
        protected void Page_Load(object sender, EventArgs e)
        {
            // string name = RegionInfo.CurrentRegion.DisplayName;
            if (!IsPostBack)
            {
                hiddenfield_showInfo.Value = "0";
                if (Request["userid"] != null)
                {
                    hiddenfield_userid.Value = Request["userid"];
                }
            }
            hiddenfield_text.Value = _database.getText("startPage");
            //getBetingelse();
        }

 
        protected void btnPlay_Click(object sender, EventArgs e)
        {
            //Save to db
            saveQuestions();
            hiddenfield_showInfo.Value = "1";

            //To get the start credit from DB
            GameLogic.getInitialBetingelse();
            initialGame = (Game)Session["currentGame"];
            if (initialGame != null)
                hiddenfield_startCredit.Value = initialGame.Saldo.ToString();
            else       //error handling?    
                Response.Redirect("ErrorPage.aspx");

        }
        protected void btnStart_Click(object sender, EventArgs e)
        {
            //GameLogic.getInitialBetingelse();
            GameLogic.redirectToGame(initialGame.Name);
 
        }

        private void saveQuestions()
        {
            string a12 = "";
            List<string> answers = new List<string>
            {
                q1.SelectedItem.Value,
                q2.SelectedItem.Value,
                q3.SelectedItem.Value,
                q4.SelectedItem.Value,
                q5.SelectedItem.Value,
                q6.SelectedItem.Value,
                q7.SelectedItem.Value,
                q8.SelectedItem.Value,
                q9.SelectedItem.Value,
                q10.SelectedItem.Value,
                q11.Text
            };

            foreach (ListItem item in q12.Items)
            {
                a12 += item.Text + ",";
                
            }
            answers.Add(a12);
            answers.Add(q13.SelectedItem.Value);
            answers.Add(hiddenfield_device.Value);

            _database.saveQuestions(answers, hiddenfield_userid.Value);
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }
    }
}