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
        public SqlConnection connectionstring = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        public string getPlayerCredits(string userid)
        {
            string credit = "";
            try
            {
                SqlConnection con = connectionstring;
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
        public bool checkIfPlayerExsist(string userid)
        {
       
            bool userExist = false;
            string user = null;
            try
            {
                SqlConnection con = connectionstring;
                var sql = "checkIfUserExist";
                var da = new SqlDataAdapter(sql, con);
                var ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@UserId", userid);
                da.Fill(ds, "checkIfUserExist");
                dt = ds.Tables["checkIfUserExist"];

                foreach (DataRow dr in dt.Rows)
                {
                   user = dr[0].ToString();
                }
            }
            catch (Exception e)
            {
                // msg = "Error trying login user : " + txtUsername.Text;
            }
            if(user != null)
            {
                userExist = true;
            }
            return userExist;

        }
        public int getCredit()
        {
            return 100;
        }
    }
}