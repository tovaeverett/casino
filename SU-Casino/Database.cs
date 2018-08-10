using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;


namespace SU_Casino
{
    public class Database
    {
        public SqlConnection connectionstring = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString); //(@"Data Source=LAPTOP-TGVH7EEV\HUGOSSONSQL;Initial Catalog=SU_Casino;Integrated Security=True");//

        public string getPlayerCredits(string userid)
        {
            string credit = "";
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
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
            catch (Exception ex)
            {
                var log = new EventLog($"Error getting credits for user: {userid}", userid, ex);

                Log(log);
                // msg = "Error trying login user : " + txtUsername.Text;
            }
            return credit;
        }

        public void Log(EventLog log)
        {
            // Finns också en SP som heter insertEventLog men ger samma "fel"...
            // ... att @userid inte följer med....
            string query = "INSERT into [eventLog] (user_Id,logDate,title,message) VALUES (@userid,@logDate,@title,@message)";

            using (SqlConnection openCon = connectionstring)
            using (SqlCommand Save = new SqlCommand(query))
            {
                Save.Connection = openCon;

                Save.Parameters.Add(new SqlParameter("@userid", log.userid));
                Save.Parameters.Add(new SqlParameter("@logDate", DateTime.Now));
                Save.Parameters.Add(new SqlParameter("@title", log.title));
                Save.Parameters.Add(new SqlParameter("@message", log.message));

                try
                {
                    openCon.Open();

                    Save.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    openCon.Close();
                }
            }
                    
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            //SqlCommand cmd = new SqlCommand(query, conn);
            
            //cmd.Parameters.Add(new SqlParameter("@userid", log.userid));
            //cmd.Parameters.Add(new SqlParameter("@title", log.title));
            //cmd.Parameters.Add(new SqlParameter("@message", log.message));

            //try
            //{
            //    conn.Open();
            //    //GetExample(cmd, p.ToArray());
            //    cmd.ExecuteNonQuery();
            //    cmd.Parameters.Clear();
            //}
            //catch (Exception ex)
            //{
            //    //   throw new Exception("Execption adding eventlog. " + ex.Message);
            //}
            //finally
            //{
            //    conn.Close();
            //    conn.Dispose();
            //    cmd.Dispose();
            //}
        }

        public void updatePlayerLog(Playerlog log)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
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
                var evLog = new EventLog($"Exception adding account: {log.userid}", log.userid, ex);

                Log(evLog);
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
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
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
            catch (Exception ex)
            {
                var log = new EventLog($"Error checking for user: {userid}", userid, ex);

                Log(log);
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
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
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
            catch (Exception ex)
            {
                var log = new EventLog($"Error getting theme for game: {gamename} with random nr: {randomnr}", null, ex);

                Log(log);
            }

            return theme != "" ? theme : null;
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
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
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
            catch (Exception ex)
            {
                var log = new EventLog($"Error getting all themes, condition: {condition}, moment: {moment}, gamename: {gamename}", null, ex);

                Log(log);
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

            SqlConnection spContentConn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            sqlcmd.Connection = spContentConn;
            sqlcmd.CommandTimeout = 0;
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = sqlselectQuery;
            try
            {
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
            }
            catch (Exception ex)
            {
                var log = new EventLog("Admin page error", null, ex);

                Log(log);
            }
            finally
            {
                spContentConn.Close();
                spContentConn.Dispose();
            }

            return "";
        }

        public List<string> getTexts()
        {
            var texts = new List<string>();
            string sqlselectQuery = "select Text_Id, Text, Text_Name from InfoText";
            SqlCommand sqlcmd = new SqlCommand();

            SqlConnection spContentConn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            sqlcmd.Connection = spContentConn;
            sqlcmd.CommandTimeout = 0;
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = sqlselectQuery;

            try
            {
                spContentConn.Open();
                using (spContentConn)
                {
                    using (SqlDataReader sdr = sqlcmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            texts.Add(sdr.GetString(2));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var log = new EventLog("Admin page error", null, ex);

                Log(log);
            }
            finally
            {
                spContentConn.Close();
                spContentConn.Dispose();
            }

            return texts;
        }

        public void UpdateText(string textName, string infotext)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("UPDATE InfoText SET Text=@Text" +
                            " WHERE Text_Name=" + "'" + textName + "'", conn))
                    {
                        cmd.Parameters.AddWithValue("@Text", infotext);


                        int rows = cmd.ExecuteNonQuery();

                        //rows number of record got updated
                    }
                }
            }
            catch (SqlException ex)
            {
                var log = new EventLog("Admin page error", null, ex);

                Log(log);
            }
        }

       public List<string> GetCondition()
       {
            List<string> list = new List<string>();
            try
            {
                SqlConnection connectionstring = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
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
                    list.Add(dr[0].ToString());
                   
                }
            }
            catch (Exception ex)
            {
                var log = new EventLog($"Error getting conditions", null, ex);

                Log(log);
            }
            return list;
        }

        //public List<string> getOrderToPlay(int seq, string condition)
        //{
        //    List<string> list = new List<string>();        
        //    try
        //    {
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        //        var sql = "getGameToPlay";
        //        var da = new SqlDataAdapter(sql, con);
        //        var ds = new DataSet();
        //        DataTable dt = new DataTable();

        //        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        da.SelectCommand.Parameters.AddWithValue("@seq", seq);
        //        da.SelectCommand.Parameters.AddWithValue("@condition", condition);

        //        da.Fill(ds, "getGameToPlay");
        //        dt = ds.Tables["getGameToPlay"];

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            list.Add(dr[0].ToString());
        //            list.Add(dr[1].ToString());
        //            list.Add(dr[2].ToString());
        //        }
        //        return list;
        //    }
        //    catch (Exception e)
        //    {
        //        // msg = "Error trying login user : " + txtUsername.Text;
        //    }
        //    return null;
        //}

        public Game getOrderToPlay(int seq, string condition)
        {
            Game game = new Game();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            try
            {
                var sql = "getGameToPlay";
                var da = new SqlDataAdapter(sql, con);
                var ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@seq", seq);
                da.SelectCommand.Parameters.AddWithValue("@condition", condition);

                da.Fill(ds, "getGameToPlay");
                dt = ds.Tables["getGameToPlay"];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        game.Name = dr[0].ToString();
                        game.Trials = Convert.ToInt32(dr[1].ToString());
                        game.Saldo = dr[2].ToString() != "" ? Convert.ToInt32(dr[2].ToString()) : 0;
                        game.Bet_R1 = dr[3].ToString() != "" ? Convert.ToInt32(dr[3].ToString()) : 0;
                        game.Bet_R2 = dr[4].ToString() != "" ? Convert.ToInt32(dr[4].ToString()) : game.Bet_R1;
                        game.Prob_O1 = dr[5].ToString() != "" ? Convert.ToDouble(dr[5].ToString()) : 0;
                        game.Prob_O2 = dr[6].ToString() != "" ? Convert.ToDouble(dr[6].ToString()) : game.Prob_O1;
                        game.Win_O1 = dr[7].ToString() != "" ? Convert.ToInt32(dr[7].ToString()) : 0;
                        game.Win_O2 = dr[8].ToString() != "" ? Convert.ToInt32(dr[8].ToString()) : game.Win_O1;
                        game.Sequence = seq;
                        game.Condition = condition;
                    }

                    return game;
                }
            }
            catch (Exception ex)
            {
                var log = new EventLog($"Error getting order to play, seq: {seq}, condition: {condition}", null, ex);

                Log(log);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return null;
        }

        public void GetExample(SqlCommand command, params SqlParameter[] p)
        {
            if (p != null && p.Any())
            {
                command.Parameters.AddRange(p);
            }
        }

        public void DeleteMatris(string rowId)
        {
            string query = "Delete from [matris] WHERE RowId = @rowId";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString()))
            using (SqlCommand command = new SqlCommand(query, connection))

            try
            {
                // open the connection, execute, etc
                List<SqlParameter> p = new List<SqlParameter>();
                p.Add(new SqlParameter("@rowid", rowId));

                connection.Open();
                GetExample(command, p.ToArray());
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            catch (Exception ex)
            {
                var log = new EventLog($"Error trying to delete matris: {rowId}", null, ex);

                Log(log);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public void InsertMatris()
        {
            using (SqlConnection openCon = connectionstring)
            {
                string saveStaff = "INSERT into matris (prop_n,condition,seq,trials,name,saldo,perc_S0,perc_S1,S1_variant,perc_S2,perc_S3,perc_S4,bet_R1,bet_R2,bet_R3,bet_B4,if_R1,if_R2,if_R3,if_R4,prob_O1,prob_O2,win_O1,win_O2,ifS1win,ifS2win,ifS3win,ifS4win,ifS1probX,ifS2probX,hide,freeze_win)" +
                    " VALUES (@prop_n,@condition,@seq,@trials,@name,@saldo,@perc_S0,@perc_S1,@S1_variant,@perc_S2,@perc_S3,@perc_S4,@bet_R1,@bet_R2,@bet_R3,@bet_B4,@if_R1,@if_R2,@if_R3,@if_R4,  @prob_O1,@prob_O2,@win_O1,@win_O2,@ifS1win,@ifS2win,@ifS3win,@ifS4win,@ifS1probX,@ifS2probX,@hide,@freeze_win)";

                using (SqlCommand Save = new SqlCommand(saveStaff))
                {
                    Save.Connection = openCon;

                    Save.Parameters.Add(new SqlParameter("@prop_n", ""));
                    Save.Parameters.Add(new SqlParameter("@condition", ""));
                    Save.Parameters.Add(new SqlParameter("@seq", ""));
                    Save.Parameters.Add(new SqlParameter("@trials", ""));
                    Save.Parameters.Add(new SqlParameter("@name", ""));
                    Save.Parameters.Add(new SqlParameter("@saldo", ""));
                    Save.Parameters.Add(new SqlParameter("@perc_S0", ""));
                    Save.Parameters.Add(new SqlParameter("@perc_S1", ""));
                    Save.Parameters.Add(new SqlParameter("@S1_variant", ""));
                    Save.Parameters.Add(new SqlParameter("@perc_S2", ""));
                    Save.Parameters.Add(new SqlParameter("@perc_S3", ""));
                    Save.Parameters.Add(new SqlParameter("@perc_S4", ""));
                    Save.Parameters.Add(new SqlParameter("@bet_R1", ""));
                    Save.Parameters.Add(new SqlParameter("@bet_R2", ""));
                    Save.Parameters.Add(new SqlParameter("@bet_R3", ""));
                    Save.Parameters.Add(new SqlParameter("@bet_B4", ""));
                    Save.Parameters.Add(new SqlParameter("@if_R1", ""));
                    Save.Parameters.Add(new SqlParameter("@if_R2", ""));
                    Save.Parameters.Add(new SqlParameter("@if_R3", ""));
                    Save.Parameters.Add(new SqlParameter("@if_R4", ""));
                    Save.Parameters.Add(new SqlParameter("@prob_O1", ""));
                    Save.Parameters.Add(new SqlParameter("@prob_O2", ""));
                    Save.Parameters.Add(new SqlParameter("@win_O1", ""));
                    Save.Parameters.Add(new SqlParameter("@win_O2", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS1win", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS2win", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS3win", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS4win", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS1probX", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS2probX", ""));
                    Save.Parameters.Add(new SqlParameter("@hide", ""));
                    Save.Parameters.Add(new SqlParameter("@freeze_win", ""));

                    try
                    {
                        openCon.Open();

                        Save.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        var log = new EventLog("Error trying to login user", null, ex);

                        Log(log);
                    }
                    finally
                    {
                        openCon.Close();
                    }
                }
            }
        }

        public (DataSet ds, DataRowCollection rows) GetMatris()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            var ds = new DataSet();
            DataTable dt = new DataTable();
            
            try
            {
                var sql = "getMatris";
                var da = new SqlDataAdapter(sql, con);

                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(ds, "getMatris");
                dt = ds.Tables["getMatris"];

            }
            catch (Exception ex)
            {
                var log = new EventLog("Error trying to get matris", null, ex);

                Log(log);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return (ds, dt.Rows);
        }

        public void UpdateMatris(string rowId, string[] paramz)
        {
            string query = "UPDATE [matris] SET prop_n = @prop_n," +
                " condition = @condition, seq = @seq, trials = @trials, name = @name, saldo = @saldo, perc_S0 = @perc_S0," +
                " perc_S1 = @perc_S1, S1_variant = @S1_variant, perc_S2 = @perc_S2, perc_S3 = @perc_S3, perc_S4 = @perc_S4, bet_R1 = @bet_R1, bet_R2 = @bet_R2, bet_R3 = @bet_R3, bet_B4 = @bet_B4," +
                " if_R1 = @if_R1, if_R2 = @if_R2, if_R3 = @if_R3, if_R4 = @if_R4, prob_O1 = @prob_O1, prob_O2 = @prob_O2, win_O1 = @win_O1," +
                " win_O2 = @win_O2, ifS1win = @ifS1win, ifS2win = @ifS2win, ifS3win = @ifS3win, ifS4win = @ifS4win, ifS1probX = @ifS1probX, ifS2probX = @ifS2probX, hide = @hide, freeze_win = @freeze_win" +
                " WHERE RowId = @RowID";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString()))
            using (SqlCommand command = new SqlCommand(query, connection))

            try
            {
                // open the connection, execute, etc
                List<SqlParameter> p = new List<SqlParameter>();
                p.Add(new SqlParameter("@RowID", rowId));
                p.Add(new SqlParameter("@prop_n", paramz[0]));
                p.Add(new SqlParameter("@condition", paramz[1]));
                p.Add(new SqlParameter("@seq", paramz[2]));
                p.Add(new SqlParameter("@trials", paramz[3]));
                p.Add(new SqlParameter("@name", paramz[4]));
                p.Add(new SqlParameter("@saldo", paramz[5]));
                p.Add(new SqlParameter("@perc_S0", paramz[6]));
                p.Add(new SqlParameter("@perc_S1", paramz[7]));
                p.Add(new SqlParameter("@S1_variant", paramz[8]));
                p.Add(new SqlParameter("@perc_S2", paramz[9]));
                p.Add(new SqlParameter("@perc_S3", paramz[10]));
                p.Add(new SqlParameter("@perc_S4", paramz[11]));
                p.Add(new SqlParameter("@bet_R1", paramz[12]));
                p.Add(new SqlParameter("@bet_R2", paramz[13]));
                p.Add(new SqlParameter("@bet_R3", paramz[14]));
                p.Add(new SqlParameter("@bet_B4", paramz[15]));
                p.Add(new SqlParameter("@if_R1", paramz[16]));
                p.Add(new SqlParameter("@if_R2", paramz[17]));
                p.Add(new SqlParameter("@if_R3", paramz[18]));
                p.Add(new SqlParameter("@if_R4", paramz[19]));
                p.Add(new SqlParameter("@prob_O1", paramz[20]));
                p.Add(new SqlParameter("@prob_O2", paramz[21]));
                p.Add(new SqlParameter("@win_O1", paramz[22]));
                p.Add(new SqlParameter("@win_O2", paramz[23]));
                p.Add(new SqlParameter("@ifS1win", paramz[24]));
                p.Add(new SqlParameter("@ifS2win", paramz[25]));
                p.Add(new SqlParameter("@ifS3win", paramz[26]));
                p.Add(new SqlParameter("@ifS4win", paramz[27]));
                p.Add(new SqlParameter("@ifS1probX", paramz[28]));
                p.Add(new SqlParameter("@ifS2probX", paramz[29]));
                p.Add(new SqlParameter("@hide", paramz[30]));
                p.Add(new SqlParameter("@freeze_win", paramz[31]));

                connection.Open();
                GetExample(command, p.ToArray());
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                GetMatris();
            }
            catch (Exception ex)
            {
                var log = new EventLog($"Error updating matris with RowID: {rowId}", null, ex);

                Log(log);
                // log and handle exception(s)
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public void GetReport()
        {
            string strFilePath = @"C:\temp\testfile.csv";
            string strSeperator = ",";
            StringBuilder sbOutput = new StringBuilder();


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            var sql = "getLog";
            var da = new SqlDataAdapter(sql, con);
            var ds = new DataSet();
            DataTable dt = new DataTable();

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            //da.SelectCommand.Parameters.AddWithValue("@UserId", lblUserId.Text);


            da.Fill(ds, "getLog");
            dt = ds.Tables["getLog"];

            foreach (DataRow dr in dt.Rows)
            {
                sbOutput.AppendLine(string.Join(strSeperator, dr[0] + strSeperator + dr[1] + strSeperator + dr[2] + strSeperator + dr[3] + strSeperator + dr[4] + strSeperator + dr[5] + strSeperator
                    + dr[6] + strSeperator + dr[7] + strSeperator + dr[8] + strSeperator + dr[9] + strSeperator + dr[10] + strSeperator + dr[11] + strSeperator + dr[12] + strSeperator + dr[13] + strSeperator));
            }

            File.WriteAllText(strFilePath, sbOutput.ToString());

            // To append more lines to the csv file
            // File.AppendAllText(strFilePath, sbOutput.ToString());

        }

        public void saveQuestions(List<string> list,string userid)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("insertQuestionsLog", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("userid", userid);
            cmd.Parameters.AddWithValue("q1",list[0].ToString());
            cmd.Parameters.AddWithValue("q2", list[1].ToString());
            cmd.Parameters.AddWithValue("q3", list[2].ToString());
            cmd.Parameters.AddWithValue("q4", list[3].ToString());
            cmd.Parameters.AddWithValue("q5", list[4].ToString());
            cmd.Parameters.AddWithValue("q6", list[5].ToString());
            cmd.Parameters.AddWithValue("q7", list[6].ToString());
            cmd.Parameters.AddWithValue("q8", list[7].ToString());
            cmd.Parameters.AddWithValue("q9", list[8].ToString());
            cmd.Parameters.AddWithValue("q10", list[9].ToString());
            cmd.Parameters.AddWithValue("q11", list[10].ToString());
            cmd.Parameters.AddWithValue("q12", list[11].ToString());
            cmd.Parameters.AddWithValue("q13", list[12].ToString());
            cmd.Parameters.AddWithValue("Date", DateTime.Now);
            cmd.Parameters.AddWithValue("Device", list[13].ToString());
            cmd.Parameters.AddWithValue("Country", "sweden");
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var log = new EventLog($"Error saving questions for user: {userid}", userid, ex);

                Log(log);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}