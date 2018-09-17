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
    public partial class StartPage : System.Web.UI.Page
    {
        GamesSssion gamesSssion;

        //Database _database = new Database();        
        //private Game initialGame;
        //private GameLogic gameLogic = new GameLogic();

        private void LoadGameSessoin() {
            if (Session["GamesSssion"] == null)
                Session["GamesSssion"] = new GamesSssion();

            gamesSssion = (GamesSssion)Session["GamesSssion"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {            

            //hiddenfield_userid.Value = Request["workerId"];
            String workerid = Request["workerId"];
            if (!IsPostBack)
            {
                if (String.IsNullOrWhiteSpace(workerid))
                    workerid = Guid.NewGuid().ToString();

                // reset session
                Session["GamesSssion"] = null;

                hiddenfield_showInfo.Value = "0";
                if (Request["workerId"] != null)
                {
                    hiddenfield_userid.Value = Request["workerId"];
                    //hiddenfield_assignmentId.Value = Request["assignmentId"];
                    //hiddenfield_hitId.Value = Request["hitId"];
                    //hiddenfield_turkSubmitTo.Value = Request["turkSubmitTo"]; // https://www.mturk.com/

                }
                else
                {
                    // Response.Redirect("ErrorPage.aspx"); <--- Aktivera sen.
                }
            }
            if (String.IsNullOrWhiteSpace(hiddenfield_userid.Value))
                hiddenfield_userid.Value = workerid;
            LoadGameSessoin();
            //hiddenfield_text.Value = _database.getText("startPage");
            hiddenfield_text.Value = gamesSssion.GetText(InfoTextType.startPage);
            //getBetingelse();
        }

 
        protected void btnPlay_Click(object sender, EventArgs e)
        {
            //Save to db
            saveQuestions();
            hiddenfield_showInfo.Value = "1";

            //To get the start credit from DB
            //gameLogic.getInitialBetingelse();
            gamesSssion.GetInitialBetingelse();
            gamesSssion.gameToPlay.UserId = hiddenfield_userid.Value;
            hiddenfield_startCredit.Value = gamesSssion.gameToPlay.Saldo.ToString();

            //initialGame = (Game)Session["currentGame"];
            //initialGame.UserId = hiddenfield_userid.Value;
            //if (initialGame != null)
                //hiddenfield_startCredit.Value = initialGame.Saldo.ToString();
            //else
            // Response.Redirect("ErrorPage.aspx");



        }
        protected void btnStart_Click(object sender, EventArgs e)
        {
            //GameLogic.getInitialBetingelse();
            //gameLogic.redirectToGame(((Game)Session["currentGame"]).Name, hiddenfield_userid.Value);
            String gameUrl = gamesSssion.GetGameUUrl();
            if (!String.IsNullOrEmpty(gameUrl))
                HttpContext.Current.Response.Redirect(gameUrl);
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

            foreach (ListItem item in q12.Items.Cast<ListItem>().Where(li => li.Selected))
            {
                a12 += item.Text + ",";
            }
            answers.Add(a12);
            answers.Add(q13.SelectedItem.Value);
            answers.Add(hiddenfield_device.Value);

            //_database.saveQuestions(answers, hiddenfield_userid.Value, hiddenfield_country.Value);
            gamesSssion.SaveQuestions(answers, hiddenfield_userid.Value, hiddenfield_country.Value);
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }
    }
}