using SU_Casino.game;
using SU_Casino.model;
using System;

namespace SU_Casino
{
    public partial class endPage : System.Web.UI.Page
    {
        //Database _database = new Database();
        //public SqlConnection connectionstring = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        GameSession gameSession;

        private void LoadGameSession()
        {
            if (Session["GameSession"] == null)
                Session["GameSession"] = new GameSession();

            gameSession = (GameSession)Session["GameSession"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGameSession();

            hiddenfield_text.Value = gameSession.GetText(AllTextType.endPage);
            lblCode.Text = gameSession.SurveyCode.ToString();
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            //send info
        }
    }
}