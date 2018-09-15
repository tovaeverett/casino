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
    public partial class endPage : System.Web.UI.Page
    {
        //Database _database = new Database();
        //public SqlConnection connectionstring = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        GamesSssion gamesSssion;

        private void LoadGameSessoin()
        {
            if (Session["GamesSssion"] == null)
                Session["GamesSssion"] = new GamesSssion();

            gamesSssion = (GamesSssion)Session["GamesSssion"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGameSessoin();

            //hiddenfield_text.Value = _database.getText("endPage");
            hiddenfield_text.Value = gamesSssion.GetText(InfoTextType.endPage);
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            //send info
        }
    }
}