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
        public void updatePlayerLog(Playerlog log)
        {
            SqlConnection conn = connectionstring;
            SqlCommand cmd = new SqlCommand("insertIntoLog", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("userid", log.userid);
            cmd.Parameters.AddWithValue("condition", log.condition);
            cmd.Parameters.AddWithValue("gamename", log.gamename);
            cmd.Parameters.AddWithValue("moment", log.moment);
            cmd.Parameters.AddWithValue("trial", log.trial);
            cmd.Parameters.AddWithValue("balance_in", log.balance_in);
            cmd.Parameters.AddWithValue("balance_out", log.balance_out);
            cmd.Parameters.AddWithValue("stimuli", log.stimuli);
            cmd.Parameters.AddWithValue("bet", log.bet);
            cmd.Parameters.AddWithValue("outcome", log.outcome);
            cmd.Parameters.AddWithValue("response", log.response);
            cmd.Parameters.AddWithValue("timestamp_begin", log.timestamp_begin);
            cmd.Parameters.AddWithValue("timestamp_R", log.timestamp_R);
            cmd.Parameters.AddWithValue("timestamp_O", log.timestamp_O);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
             //   throw new Exception("Execption adding account. " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
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
            if (user != null)
            {
                userExist = true;
            }
            return userExist;

        }
        public void CreateReport()
        {

        }
        public string getTheme(int randomnr)
        {
            string theme = "";
            try
            {
                SqlConnection con = connectionstring;
                var sql = "getTheme";
                var da = new SqlDataAdapter(sql, con);
                var ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ThemeNr", randomnr.ToString());
                da.Fill(ds, "getTheme");
                dt = ds.Tables["getTheme"];

                foreach (DataRow dr in dt.Rows)
                {
                    theme = dr[0].ToString();
                }
            }
            catch (Exception e)
            {
                // msg = "Error trying login user : " + txtUsername.Text;
            }
            if (theme != "")
            {
                return theme;
            }
            else
            {
                return null;
            }
        }
        public string getAllThemes(string prop_n, int moment)
        {
            int theme1 = 0;
            int theme2 = 0;
            int theme3 = 0;
            int theme4 = 0;

            try
            {
                SqlConnection con = connectionstring;
                var sql = "getAllTheme";
                var da = new SqlDataAdapter(sql, con);
                var ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@prop_n", prop_n);
                da.SelectCommand.Parameters.AddWithValue("@moment", moment);
                da.Fill(ds, "getAllTheme");
                dt = ds.Tables["getAllTheme"];
           
                foreach (DataRow dr in dt.Rows)
                {
                    theme1 = Convert.ToInt32(dr[0]);
                    theme2 = Convert.ToInt32(dr[1]);
                    theme3 = Convert.ToInt32(dr[2]);
                    theme4 = Convert.ToInt32(dr[3]);
                }
            }
            catch (Exception e)
            {
                // msg = "Error trying login user : " + txtUsername.Text;
            }
            if (theme1 != 0)
            {
                CalculateChance(theme1, theme2, theme3, theme4);
                return "";
            }
            else
            {
                return null;
            }
        }
        private int CalculateChance(int theme1, int theme2, int theme3, int theme4)
        {
            Random rnd = new Random();
            int nr = rnd.Next(0, theme4);

            if(nr < theme1)
            {
                return theme1;
            }
            else if(nr > theme1 && nr < theme2)
            {
                return theme2;
            }
            else if(nr > theme2 && nr < theme3 )
            {
                return theme3;
            }
            else if(nr > theme3)
            {
                return theme4;
            }
            return 0;
        }
    }
}