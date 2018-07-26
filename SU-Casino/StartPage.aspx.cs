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
            hiddenfield_text.Value = _database.getText("startpage");
            //getBetingelse();
        }
        public void getBetingelse()
        {
            Random letter = new Random();
            var Array = _database.GetCondition();

            int i = Array.Count();
            int num = letter.Next(0, i);
            string let = Array[num];


            var list = _database.getOrderToPlay(1, let);

            if (list != null)
            {
                string value = list[0];
                switch (value)
                {
                    case "DET_control":

                        break;
                    case "DET_experimental":

                        break;
                    case "DET_realworld":

                        break;
                    case "Instrumental_acq":
                        Response.Redirect("CardDraw.aspx?seq=" + list[1] + "&credit=" + list[2] + "&gamename=" + list[0]);
                        break;
                    case "Instrumental_acq2":
                        Response.Redirect("CardDraw2.aspx?seq=" + list[1] + "&credit=" + list[2] + "&gamename=" + list[0]);
                        break;
                    case "Pavlovian_acq":
                        Response.Redirect("OneArmdBandit.aspx?seq=" + list[1] + "&credit=" + list[2] + "&gamename=" + list[0]);
                        break;
                    case "Pavlovian_extinct":
                        Response.Redirect("OneArmdBandit.aspx?seq=" + list[1] + "&credit=" + list[2] + "&gamename=" + list[0]);
                        break;
                    case "Roulette":
                        Response.Redirect("Roulette.aspx?seq=" + list[1] + "&credit=" + list[2] + "&gamename=" + list[0]);
                        break;
                    case "Transfer_test":
                        break;
                }
            }
        }


        protected void btnPlay_Click(object sender, EventArgs e)
        {
            //Save to db
            saveQuestions();
            hiddenfield_showInfo.Value = "1";

        }
        protected void btnStart_Click(object sender, EventArgs e)
        {
            
            getBetingelse();
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

            _database.saveQuestions(answers, hiddenfield_userid.Value);
        }
    }
}