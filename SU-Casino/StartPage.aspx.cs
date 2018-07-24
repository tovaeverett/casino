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
            Random letter = new Random();
            var Array = _database.GetCondition();//{ "one.one","two.one","two.two","two.three","three.one","three.two","three.three","four.one","four.one" };

            var item = Array[Array.Count - 1]; ;

            int num = letter.Next(0, 5);
            string let = Array[num];

            _database.getAllThemes(let,1,"");
        }
        protected void btnStart_Click(object sender, EventArgs e)
        {
            Server.Transfer("Roulette.aspx", true);
        }
    }
}