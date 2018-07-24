﻿using System;
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

        public string getTheme(int randomnr,string gamename = null)
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
        public string getAllThemes(string condition, int moment, string gamename = null)
        {
            if (gamename != null)
            {
                gamename = "nothing";
            }
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
                da.SelectCommand.Parameters.AddWithValue("@condition", condition);
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
                 CalculateChance(theme1, theme2, theme3, theme4,gamename).ToString();
                return "";
            }
            else
            {
                return null;
            }
        }
        private string CalculateChance(int theme1, int theme2, int theme3, int theme4, string gamename)
        {
            Random rnd = new Random();
            int nr = rnd.Next(0, theme4);

            if(nr < theme1)
            {
                return getTheme(1, gamename);
            }
            else if(nr > theme1 && nr < theme2)
            {
                return getTheme(2, gamename);
            }
            else if(nr > theme2 && nr < theme3 )
            {
                return getTheme(3, gamename);
            }
            else if(nr > theme3)
            {
                return getTheme(4, gamename);
            }
            return "";
        }
        public string getText(string infotext)
        {
            string sqlselectQuery = "select Text from InfoText where Text_Name = " + "'" + infotext + "'";
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
                        return sdr.GetString(0);
                    }
                }
            }
            
            spContentConn.Close();
            spContentConn.Dispose();
            return "";
        }

       public List<string> GetCondition()
       {
            List<string> array = new List<string>();
            try
            {
                SqlConnection connectionstring = new SqlConnection(@"Data Source=LAPTOP-TGVH7EEV\HUGOSSONSQL;Initial Catalog=SU_Casino;Integrated Security=True");
                SqlConnection con = connectionstring;
                var sql = "getAllCondition";
                var da = new SqlDataAdapter(sql, con);
                var ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds, "getAllCondition");
                dt = ds.Tables["getAllCondition"];
             
                foreach (DataRow dr in dt.Rows)
                {
                    array.Add(dr[0].ToString());
                   
                }
            }
            catch (Exception e)
            {
                // msg = "Error trying login user : " + txtUsername.Text;
            }
            //if (theme1 != 0)
            //{
            //    CalculateChance(theme1, theme2, theme3, theme4);
            //    return "";
            //}
            //else
            //{
            //    return null;
            //}
            return array;
        }
    }
}