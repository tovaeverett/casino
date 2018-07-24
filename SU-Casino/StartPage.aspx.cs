using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
            if (!IsPostBack)
            {
                hiddenfield_showInfo.Value = "0";
            }
            hiddenfield_text.Value = _database.getText("startpage");
            getBetingelse();
        }
        private void getBetingelse()
        {
            Random rnd = new Random();
            string nr = rnd.Next(0, 11).ToString();
            nr = "1." + nr;
            _database.getAllThemes(nr,1);            
        }

        protected void btnPlay_Click(object sender, EventArgs e)
        {
            //Save to db
            hiddenfield_showInfo.Value = "1";

        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            Server.Transfer("Roulette.aspx", true);
        }
    }
}