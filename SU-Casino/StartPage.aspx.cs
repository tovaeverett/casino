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
            getText();
            getBetingelse();
        }
        private void getBetingelse()
        {
            Random rnd = new Random();
            string nr = rnd.Next(0, 11).ToString();
            nr = "1." + nr;
            _database.getAllThemes(nr,1);            
        }

        private void getText()
        {
            string sqlselectQuery = "select Text from InfoText where Text_Name = " + "'" + "InfoText1" + "'";
            SqlCommand sqlcmd = new SqlCommand();

            SqlConnection spContentConn = connectionstring;
            sqlcmd.Connection = spContentConn;
            sqlcmd.CommandTimeout = 0;
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = sqlselectQuery;
            spContentConn.Open();
            using (spContentConn)
            {
                using (SqlDataReader sdr = sqlcmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        hiddenfield_text.Value = sdr.GetString(0);
                    }
                }
            }
            spContentConn.Close();
            spContentConn.Dispose();
        }

    }
}