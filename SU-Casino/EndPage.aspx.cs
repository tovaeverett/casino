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
        Database _database = new Database();
        public SqlConnection connectionstring = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            hiddenfield_text.Value = _database.getText("endPage");
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            //send info
        }
    }
}