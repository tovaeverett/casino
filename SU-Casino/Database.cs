using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SU_Casino
{
    public class Database
    {
        public string connectionstring = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString).ToString();
        public string getPlayerCredits(string userid)
        {
            string credit = "";
            try
            {
                SqlConnection con = new SqlConnection(connectionstring);
                var sql = "getPlayerCredit";
                var da = new SqlDataAdapter(sql, con);
                var ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                dt = ds.Tables["getPlayerCredit"];

                foreach (DataRow dr in dt.Rows)
                {
                    credit = dr[0].ToString();
                }
            }
            catch (Exception e)
            {
                // msg = "Error trying login user : " + txtUsername.Text;
            }
            return credit;
        }
        public void updatePlayerLog()
        {

        }
        public void insertNewPlayer()
        {

        }
        public void checkIfPlayerExsist()
        {

        }
    }
}