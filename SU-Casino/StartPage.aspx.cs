using SU_Casino.game;
using SU_Casino.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SU_Casino
{
    public partial class StartPage : System.Web.UI.Page
    {
        GameSession gameSession;

        private void LoadGameSession()
        {
            if (Session["GameSession"] == null)
                Session["GameSession"] = new GameSession();

            gameSession = (GameSession)Session["GameSession"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            string workerid = Request["workerId"];
            if (!IsPostBack)
            {
                if (string.IsNullOrWhiteSpace(workerid))
                    workerid = Guid.NewGuid().ToString();

                // reset session
                Session["GamesSssion"] = null;

                hiddenfield_showInfo.Value = "0";
                if (Request["workerId"] != null)
                {
                    hiddenfield_userid.Value = Request["workerId"];
                }
            }
            if (string.IsNullOrWhiteSpace(hiddenfield_userid.Value))
                hiddenfield_userid.Value = workerid;

            LoadGameSession();
            hiddenfield_text.Value = gameSession.GetText(AllTextType.startPage);
        }


        protected void btnPlay_Click(object sender, EventArgs e)
        {
            //Save to db
            saveQuestions();
            hiddenfield_showInfo.Value = "1";

            //To get the start credit from DB
            //gameLogic.getInitialBetingelse();
            gameSession.GetInitialBetingelse();

            gameSession.GameToPlay.UserId = hiddenfield_userid.Value;
            hiddenfield_startCredit.Value = gameSession.GameToPlay.Saldo.ToString();
        }
        protected void btnStart_Click(object sender, EventArgs e)
        {
            string gameUrl = gameSession.GetGameUUrl();
            if (!string.IsNullOrEmpty(gameUrl))
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
            answers.Add(gameSession.SurveyCode.ToString());


            gameSession.SaveQuestions(answers, hiddenfield_userid.Value, hiddenfield_country.Value);
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }
    }
}