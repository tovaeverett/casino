using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
//using System.Globalization;


namespace SU_Casino
{
    public class Database
    {

        public void Log(EventLog log)
        {
            // Finns också en SP som heter insertEventLog men ger samma "fel"...
            // ... att @userid inte följer med....
            string query = "INSERT into [eventLog] (user_Id,logDate,title,message) VALUES (@userid,@logDate,@title,@message)";

            using (SqlConnection openCon = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
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
                finally
                {
                    openCon.Close();
                }
            }
                    

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
                    // TODO: can this be multiple rows? last one wins?
                    foreach (DataRow dr in dt.Rows)
                    {

                        game.Name = getValueFromColumnWithName(dr, "name");
                        game.Trials = getIntValueDefault0(dr, "trials");
                        game.Saldo = getIntValueDefault0(dr, "saldo");
                        game.Bet_R1 = getIntValueDefault0(dr, "bet_R1");
                        game.Bet_R2 = getIntValueDefault0(dr, "bet_R2");
                        game.Bet_R3 = getIntValueDefault0(dr, "bet_R3");
                        game.Bet_R4 = getIntValueDefault0(dr, "bet_B4");
                        game.If_R1 = getValueFromColumnWithName(dr, "If_R1");
                        game.If_R2 = getValueFromColumnWithName(dr, "If_R2");
                        game.If_R3 = getValueFromColumnWithName(dr, "If_R3");
                        game.If_R4 = getValueFromColumnWithName(dr, "If_R4");
                        game.Prob_O1 = getDoubleDefault0(dr, "prob_O1");
                        game.Prob_O2 = getDoubleDefault0(dr, "prob_O2");
                        game.Win_O1 = getIntValueDefault0(dr, "win_O1");
                        game.Win_O2 = getIntValueDefault0(dr, "win_O2");
                        game.IfS1probX = getIntValueDefault1(dr, "ifS1probX");
                        game.IfS2probX = getIntValueDefault1(dr, "ifS2probX"); 
                        game.Perc_S1 = getDoubleDefault0(dr, "perc_S1");
                        game.Perc_S2 = getDoubleDefault0(dr, "perc_S2");
                        game.Perc_S3 = getDoubleDefault0(dr, "perc_S3");
                        game.Perc_S4 = getDoubleDefault0(dr, "perc_S4");
                        game.ThemeVariant = getValueFromColumnWithName(dr, "S1_variant");
                        game.IfS1win = getValueFromColumnWithName(dr, "ifS1win");
                        game.IfS2win = getValueFromColumnWithName(dr, "ifS2win");
                        game.IfS3win = getValueFromColumnWithName(dr, "ifS3win");
                        game.IfS4win = getValueFromColumnWithName(dr, "ifS4win");

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
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return null;
        }

        private static double getDoubleDefault0(DataRow dr, string ColumnName1)
        {
            string value = getValueFromColumnWithName(dr, ColumnName1);
            return Convert.ToDouble(value != "" ? value : "0");
        }


        private static int getIntValueDefault0(DataRow dr, string ColumnName)
        {
            string value = getValueFromColumnWithName(dr, ColumnName);
            return Convert.ToInt32(value != "" ? value : "0");
        }

        private static int getIntValueDefault1(DataRow dr, string ColumnName)
        {
            string value = getValueFromColumnWithName(dr, ColumnName);
            return Convert.ToInt32(value != "" ? value : "1");
        }

        private static string getValueFromColumnWithName(DataRow dr, string columnName)
        {
            return dr[dr.Table.Columns[columnName].Ordinal].ToString() != "" ? dr[dr.Table.Columns[columnName].Ordinal].ToString() : "";
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
            using (SqlConnection openCon = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString) )
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
            string strFileName = "playerreports_" + DateTime.Now.ToString("MMddyyyy");
            string strFilePath = @"C:\temp\"+ strFileName  + ".csv";
            string strSeperator = ";";
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
            sbOutput.AppendLine("studyID;condition;name;moment;trail;timestamp_begin;timestam_R;timestamp_O;balance_in;response;bet;stimuli;outcome;balance_out");
            foreach (DataRow dr in dt.Rows)
            {
                sbOutput.AppendLine(string.Join(strSeperator, dr[0] + strSeperator + dr[1] + strSeperator + dr[2] + strSeperator + dr[3] + strSeperator + dr[4] + strSeperator + dr[5] + strSeperator
                    + dr[6] + strSeperator + dr[7] + strSeperator + dr[8] + strSeperator + dr[9] + strSeperator + dr[10] + strSeperator + dr[11] + strSeperator + dr[12] + strSeperator + dr[13] + strSeperator));
            }

            File.WriteAllText(strFilePath, sbOutput.ToString());
            SendFile(strFilePath, strFileName);
            // To append more lines to the csv file
            // File.AppendAllText(strFilePath, sbOutput.ToString());

        }

        public void SendFile(string strFilePath, string strFileName)
        {
            System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
            string fileName = strFileName;
            byte[] Content = File.ReadAllBytes(strFilePath); //missing ;
            Response.ContentType = "text/csv";
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".csv");
            Response.BufferOutput = true;
            Response.OutputStream.Write(Content, 0, Content.Length);
            Response.End();
        }
        public void GetQuestionReports()
        {
            string strFileName = "questionreports_" + DateTime.Now.ToString("MMddyyyy");
            string strFilePath = @"C:\temp\" + strFileName + ".csv";
      
            string strSeperator = ";";
            StringBuilder sbOutput = new StringBuilder();


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            var sql = "getQuestionLog";
            var da = new SqlDataAdapter(sql, con);
            var ds = new DataSet();
            DataTable dt = new DataTable();

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            //da.SelectCommand.Parameters.AddWithValue("@UserId", lblUserId.Text);


            da.Fill(ds, "getQuestionLog");
            dt = ds.Tables["getQuestionLog"];
            sbOutput.AppendLine("studyID;q1;q2;q3;q4;q5;q6;q7;q8;q9;q10;age;gambling;average;date;device;country");
            foreach (DataRow dr in dt.Rows)
            {
                sbOutput.AppendLine(string.Join(strSeperator, dr[0] + strSeperator + dr[1] + strSeperator + dr[2] + strSeperator + dr[3] + strSeperator + dr[4] + strSeperator + dr[5] + strSeperator
                    + dr[6] + strSeperator + dr[7] + strSeperator + dr[8] + strSeperator + dr[9] + strSeperator + dr[10] + strSeperator + dr[11] + strSeperator + dr[12] + strSeperator + dr[13] + strSeperator
                    + dr[14] + strSeperator + dr[15].ToString().Replace(';', ':') + strSeperator + dr[16]));
            }

            File.WriteAllText(strFilePath, sbOutput.ToString());
            SendFile(strFilePath, strFileName);
            // To append more lines to the csv file
            // File.AppendAllText(strFilePath, sbOutput.ToString());

        }

        public void saveQuestions(List<string> list,string userid, string regionalInfo)
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
            cmd.Parameters.AddWithValue("Country", regionalInfo);
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

        public void resetMatris()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            try
            {
                using (var command = new SqlCommand("resetMatris", con)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    con.Open();
                    command.ExecuteNonQuery();
                }

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
                GetMatris();
            }          
        }
    }
}