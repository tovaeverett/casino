using SU_Casino.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace SU_Casino.service
{
    public class DBDataService : IDataService
    {
        public void DeleteMatris(string rowId)
        {
            string query = "Delete from [matris] WHERE RowId = @rowId";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
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

        }

        public List<string> GetCondition()
        {
            List<string> list = new List<string>();
            DataTable dt = new DataTable();

            string query = "getAllCondition";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, connection))

                try
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
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

        public Game GetOrderToPlay(int seq, string condition)
        {
            Game game = new Game();
            string query = "getGameToPlay";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, connection))
            using (DataTable dt = new DataTable())

                try
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@seq", seq);
                    da.SelectCommand.Parameters.AddWithValue("@condition", condition);

                    da.Fill(dt);

                    if (dt.Rows.Count != 0)
                    {
                        // TODO: can this be multiple rows? last one wins?
                        foreach (DataRow dr in dt.Rows)
                        {
                            game.Name = GetValueFromColumnWithName(dr, "name");
                            game.Trials = GetIntValueDefault0(dr, "trials");
                            game.Saldo = GetIntValueDefault0(dr, "saldo");
                            game.Bet_R1 = GetIntValueDefault0(dr, "bet_R1");
                            game.Bet_R2 = GetIntValueDefault0(dr, "bet_R2");
                            game.Bet_R3 = GetIntValueDefault0(dr, "bet_R3");
                            game.Bet_R4 = GetIntValueDefault0(dr, "bet_B4");
                            game.If_R1 = GetValueFromColumnWithName(dr, "If_R1");
                            game.If_R2 = GetValueFromColumnWithName(dr, "If_R2");
                            game.If_R3 = GetValueFromColumnWithName(dr, "If_R3");
                            game.If_R4 = GetValueFromColumnWithName(dr, "If_R4");
                            game.Prob_O1 = GetDoubleDefault0(dr, "prob_O1");
                            game.Prob_O2 = GetDoubleDefault0(dr, "prob_O2");
                            game.Win_O1 = GetIntValueDefault0(dr, "win_O1");
                            game.Win_O2 = GetIntValueDefault0(dr, "win_O2");
                            game.IfS1probX = GetIntValueDefault1(dr, "ifS1probX");
                            game.IfS2probX = GetIntValueDefault1(dr, "ifS2probX");
                            game.Perc_S1 = GetDoubleDefault0(dr, "perc_S1");
                            game.Perc_S2 = GetDoubleDefault0(dr, "perc_S2");
                            game.Perc_S3 = GetDoubleDefault0(dr, "perc_S3");
                            game.Perc_S4 = GetDoubleDefault0(dr, "perc_S4");
                            game.ThemeVariant = GetValueFromColumnWithName(dr, "S1_variant");
                            game.IfS1win = GetValueFromColumnWithName(dr, "ifS1win");
                            game.IfS2win = GetValueFromColumnWithName(dr, "ifS2win");
                            game.IfS3win = GetValueFromColumnWithName(dr, "ifS3win");
                            game.IfS4win = GetValueFromColumnWithName(dr, "ifS4win");
                            game.InfoTextType = GetValueFromColumnWithName(dr, "InfoTextType");
                            game.JackpotTextType = GetValueFromColumnWithName(dr, "JackpotTextType");
                            game.BannerTextType = GetValueFromColumnWithName(dr, "BannerTextType");
                            game.JackpotTime = GetIntValueDefault0(dr, "JackpotTime");
                            game.CloseToWinStep = GetIntValueDefault0(dr, "CloseToWinStep");
                            game.CloseToWinColour = GetBooleanValue(dr, "CloseToWinColour");
                            game.Multiplier = GetIntValueDefault0(dr, "Multiplier");
                            game.SpinDelay1 = GetIntValueDefault0(dr, "SpinDelay1");
                            game.SpinDelay2 = GetIntValueDefault0(dr, "SpinDelay2");
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
                    //throw ex;
                }

            return null;
        }

        public void GetQuestionReports()
        {
            string strFileName = "questionreports_" + DateTime.Now.ToString("MMddyyyy");
            string strFilePath = @"C:\temp\" + strFileName + ".csv";

            string strSeperator = ";";
            StringBuilder sbOutput = new StringBuilder();

            string query = "getQuestionLog";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, connection))
            using (DataTable dt = new DataTable())

                try
                {

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    //da.SelectCommand.Parameters.AddWithValue("@UserId", lblUserId.Text);  

                    da.Fill(dt);
                    sbOutput.AppendLine("studyID;q1;q2;q3;q4;q5;q6;q7;q8;q9;q10;age;gambling;average;date;device;country;SurveyCode");
                    foreach (DataRow dr in dt.Rows)
                    {
                        sbOutput.AppendLine(string.Join(strSeperator, dr[0] + strSeperator + dr[1] + strSeperator + dr[2] + strSeperator + dr[3] + strSeperator + dr[4] + strSeperator + dr[5] + strSeperator
                            + dr[6] + strSeperator + dr[7] + strSeperator + dr[8] + strSeperator + dr[9] + strSeperator + dr[10] + strSeperator + dr[11] + strSeperator + dr[12] + strSeperator + dr[13] + strSeperator
                            + dr[14] + strSeperator + dr[15].ToString().Replace(';', ':') + strSeperator + dr[16] + strSeperator + dr[17]));
                    }

                    File.WriteAllText(strFilePath, sbOutput.ToString());
                    SendFile(strFilePath, strFileName);
                    // To append more lines to the csv file
                    // File.AppendAllText(strFilePath, sbOutput.ToString());
                }
                catch (Exception ex)
                {
                    var log = new EventLog($"Error getting question reports, path: {strFileName}, file: {strFilePath}", null, ex);

                    Log(log);
                    //throw ex;
                }
        }

        public void GetReport()
        {
            string strFileName = "playerreports_" + DateTime.Now.ToString("MMddyyyy");
            string strFilePath = @"C:\temp\" + strFileName + ".csv";
            string strSeperator = ";";
            StringBuilder sbOutput = new StringBuilder();

            string query = "getLog";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, connection))
            using (DataTable dt = new DataTable())

                try
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    //da.SelectCommand.Parameters.AddWithValue("@UserId", lblUserId.Text);

                    da.Fill(dt);

                    sbOutput.AppendLine("studyID;condition;name;moment;trail;timestamp_begin;timestam_R;timestamp_O;balance_in;response;bet;stimuli;outcome;balance_out;q_win_chance");
                    foreach (DataRow dr in dt.Rows)
                    {
                        sbOutput.AppendLine(string.Join(strSeperator, dr[0] + strSeperator + dr[1] + strSeperator + dr[2] + strSeperator + dr[3] + strSeperator + dr[4] + strSeperator + dr[5] + strSeperator
                            + dr[6] + strSeperator + dr[7] + strSeperator + dr[8] + strSeperator + dr[9] + strSeperator + dr[10] + strSeperator + dr[11] + strSeperator + dr[12] + strSeperator + dr[13] + strSeperator + dr[14]));
                    }

                    File.WriteAllText(strFilePath, sbOutput.ToString());
                    SendFile(strFilePath, strFileName);
                    // To append more lines to the csv file
                    // File.AppendAllText(strFilePath, sbOutput.ToString());
                }
                catch (Exception ex)
                {
                    var log = new EventLog($"Error getting report, path: {strFileName}, file: {strFilePath}", null, ex);

                    Log(log);
                    //throw ex;
                }
        }

        //public (DataSet ds, DataRowCollection rows) GetMatris()
        public DataTable GetMatrixTable()
        {
            DataTable dt = new DataTable();
            string query = "getMatris";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, connection))

                try
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    var log = new EventLog("Error trying to get matris", null, ex);

                    Log(log);
                }

            return dt;
        }

        public DataRow[] GetMatrisByProp()
        {
            DataRow[] rows = null;
            string query = "getMatris";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, connection))
            using (DataTable dt = new DataTable())

                try
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    da.Fill(dt);
                    rows = dt.Select("prop_n <> '' AND prop_n IS NOT NULL");
                }
                catch (Exception ex)
                {
                    var log = new EventLog("Error trying to get matris", null, ex);

                    Log(log);
                }

            return rows;
        }


        public string GetText(AllTextType allTextType)
        {
            string query = "select Text from InfoText where Text_Name = " + "'" + allTextType.ToString() + "'";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
                try
                {
                    //sqlcmd.Connection = connection;
                    //sqlcmd.CommandText = sqlselectQuery;
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.Text;
                    connection.Open();

                    //using (spContentConn)
                    //{
                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            return sdr.GetString(0);
                        }
                    }
                    //}
                }
                catch (Exception ex)
                {
                    var log = new EventLog("Admin page error", null, ex);

                    Log(log);
                }

            return "";
        }

        public List<string> GetTexts()
        {
            List<string> list = new List<string>();
            //var texts = new List<string>();
            string query = "select Text_Id, Text, Text_Name from InfoText order by Text_Id, Text_Name";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))

                try
                {
                    //command.Connection = spContentConn;
                    //command.CommandText = sqlselectQuery;
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    //using (spContentConn)
                    //{
                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            list.Add(sdr.GetString(2));
                        }
                    }
                    //}
                }
                catch (Exception ex)
                {
                    var log = new EventLog("Admin page error", null, ex);

                    Log(log);
                }

            return list;
        }

        public void InsertMatris()
        {
            string query = "INSERT into matris (prop_n,condition,seq,trials,name,saldo," +
                "perc_S0,perc_S1,S1_variant,perc_S2,perc_S3,perc_S4,bet_R1,bet_R2,bet_R3,bet_B4," +
                "if_R1,if_R2,if_R3,if_R4,prob_O1,prob_O2,win_O1,win_O2,ifS1win,ifS2win,ifS3win,ifS4win,ifS1probX,ifS2probX," +
                "hide,freeze_win," +
                "CloseToWinStep,CloseToWinColour,InfoTextType,JackpotTextType,JackpotTime,BannerTextType," +
                "Multiplier,SpinDelay1,SpinDelay2) " +
                " VALUES (@prop_n,@condition,@seq,@trials,@name,@saldo," +
                "@perc_S0,@perc_S1,@S1_variant,@perc_S2,@perc_S3,@perc_S4,@bet_R1,@bet_R2,@bet_R3,@bet_B4," +
                "@if_R1,@if_R2,@if_R3,@if_R4,@prob_O1,@prob_O2,@win_O1,@win_O2,@ifS1win,@ifS2win,@ifS3win,@ifS4win,@ifS1probX,@ifS2probX," +
                "@hide,@freeze_win," +
                "@CloseToWinStep,@CloseToWinColour,trim(@InfoTextType),trim(@JackpotTextType),@JackpotTime,trim(@BannerTextType)," +
                "@Multiplier,@SpinDelay1,@SpinDelay2)";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))

                try
                {
                    //command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("@prop_n", ""));
                    command.Parameters.Add(new SqlParameter("@condition", ""));
                    command.Parameters.Add(new SqlParameter("@seq", ""));
                    command.Parameters.Add(new SqlParameter("@trials", ""));
                    command.Parameters.Add(new SqlParameter("@name", ""));
                    command.Parameters.Add(new SqlParameter("@saldo", ""));
                    command.Parameters.Add(new SqlParameter("@perc_S0", ""));
                    command.Parameters.Add(new SqlParameter("@perc_S1", ""));
                    command.Parameters.Add(new SqlParameter("@S1_variant", ""));
                    command.Parameters.Add(new SqlParameter("@perc_S2", ""));
                    command.Parameters.Add(new SqlParameter("@perc_S3", ""));
                    command.Parameters.Add(new SqlParameter("@perc_S4", ""));
                    command.Parameters.Add(new SqlParameter("@bet_R1", ""));
                    command.Parameters.Add(new SqlParameter("@bet_R2", ""));
                    command.Parameters.Add(new SqlParameter("@bet_R3", ""));
                    command.Parameters.Add(new SqlParameter("@bet_B4", ""));
                    command.Parameters.Add(new SqlParameter("@if_R1", ""));
                    command.Parameters.Add(new SqlParameter("@if_R2", ""));
                    command.Parameters.Add(new SqlParameter("@if_R3", ""));
                    command.Parameters.Add(new SqlParameter("@if_R4", ""));
                    command.Parameters.Add(new SqlParameter("@prob_O1", ""));
                    command.Parameters.Add(new SqlParameter("@prob_O2", ""));
                    command.Parameters.Add(new SqlParameter("@win_O1", ""));
                    command.Parameters.Add(new SqlParameter("@win_O2", ""));
                    command.Parameters.Add(new SqlParameter("@ifS1win", ""));
                    command.Parameters.Add(new SqlParameter("@ifS2win", ""));
                    command.Parameters.Add(new SqlParameter("@ifS3win", ""));
                    command.Parameters.Add(new SqlParameter("@ifS4win", ""));
                    command.Parameters.Add(new SqlParameter("@ifS1probX", ""));
                    command.Parameters.Add(new SqlParameter("@ifS2probX", ""));
                    command.Parameters.Add(new SqlParameter("@hide", ""));
                    command.Parameters.Add(new SqlParameter("@freeze_win", ""));
                    command.Parameters.Add(new SqlParameter("@InfoTextType", ""));
                    command.Parameters.Add(new SqlParameter("@JackpotTextType", ""));
                    command.Parameters.Add(new SqlParameter("@BannerTextType", ""));
                    command.Parameters.Add(new SqlParameter("@JackpotTime", ""));
                    command.Parameters.Add(new SqlParameter("@CloseToWinStep", ""));
                    command.Parameters.Add(new SqlParameter("@CloseToWinColour", ""));
                    command.Parameters.Add(new SqlParameter("@Multiplier", ""));
                    command.Parameters.Add(new SqlParameter("@SpinDelay1", ""));
                    command.Parameters.Add(new SqlParameter("@SpinDelay2", ""));

                    connection.Open();

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    var log = new EventLog("Error trying to login user", null, ex);

                    Log(log);
                }
        }

        public void Log(EventLog log)
        {
            // Finns också en SP som heter insertEventLog men ger samma "fel"...
            // ... att @userid inte följer med....
            string query = "INSERT into [eventLog] (user_Id,logDate,title,message) VALUES (@userid,@logDate,@title,@message)";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand(query))

                try
                {
                    command.Connection = connection;

                    command.Parameters.Add(new SqlParameter("@userid", log.userid));
                    command.Parameters.Add(new SqlParameter("@logDate", DateTime.Now));
                    command.Parameters.Add(new SqlParameter("@title", log.title));
                    command.Parameters.Add(new SqlParameter("@message", log.message));

                    connection.Open();

                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    // Where to throw the exception?
                }
        }

        public void ResetMatris()
        {
            string query = "resetMatris";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))

                try
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    var log = new EventLog("Error trying to get matris", null, ex);

                    Log(log);
                }
                finally
                {
                    GetMatrixTable();
                }
        }

        public void SaveQuestions(List<string> list, string userid, string regionalInfo)
        {
            string query = "insertQuestionsLog";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("userid", userid);
                    command.Parameters.AddWithValue("q1", list[0].ToString());
                    command.Parameters.AddWithValue("q2", list[1].ToString());
                    command.Parameters.AddWithValue("q3", list[2].ToString());
                    command.Parameters.AddWithValue("q4", list[3].ToString());
                    command.Parameters.AddWithValue("q5", list[4].ToString());
                    command.Parameters.AddWithValue("q6", list[5].ToString());
                    command.Parameters.AddWithValue("q7", list[6].ToString());
                    command.Parameters.AddWithValue("q8", list[7].ToString());
                    command.Parameters.AddWithValue("q9", list[8].ToString());
                    command.Parameters.AddWithValue("q10", list[9].ToString());
                    command.Parameters.AddWithValue("q11", list[10].ToString());
                    command.Parameters.AddWithValue("q12", list[11].ToString());
                    command.Parameters.AddWithValue("q13", list[12].ToString());
                    command.Parameters.AddWithValue("Date", DateTime.Now);
                    command.Parameters.AddWithValue("Device", list[13].ToString());
                    command.Parameters.AddWithValue("Country", regionalInfo);
                    command.Parameters.AddWithValue("SurveyCode", list[14].ToString());

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    var log = new EventLog($"Error saving questions for user: {userid}", userid, ex);

                    Log(log);
                }
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

        public void UpdateMatris(string rowId, string[] paramz)
        {
            string query = "UPDATE [matris] SET prop_n = REPLACE(TRIM(@prop_n),',','.'), " +
                " condition = @condition, seq = @seq, trials = @trials, name = @name, saldo = @saldo, " +
                " perc_S0 = REPLACE(@perc_S0,',','.'), perc_S1 = REPLACE(@perc_S1,',','.'), " +
                " perc_S2 = REPLACE(@perc_S2,',','.'), perc_S3 = REPLACE(@perc_S3,',','.'), " +
                " perc_S4 = REPLACE(@perc_S4,',','.'), " +
                " bet_R1 = REPLACE(@bet_R1,',','.'), bet_R2 = REPLACE(@bet_R2,',','.'), " +
                " bet_R3 = REPLACE(@bet_R3,',','.'), bet_B4 = REPLACE(@bet_B4,',','.'), " +
                " if_R1 = @if_R1, if_R2 = @if_R2, if_R3 = @if_R3, if_R4 = @if_R4, " +
                " prob_O1 = REPLACE(@prob_O1,',','.'), prob_O2 = REPLACE(@prob_O2,',','.'), " +
                " win_O1 = @win_O1, win_O2 = @win_O2, S1_variant = @S1_variant, " +
                " ifS1win = @ifS1win, ifS2win = @ifS2win, ifS3win = @ifS3win, ifS4win = @ifS4win, " +
                " ifS1probX = @ifS1probX, ifS2probX = @ifS2probX, hide = @hide, freeze_win = @freeze_win, " +
                " CloseToWinStep = @CloseToWinStep, CloseToWinColour = @CloseToWinColour, " +
                " InfoTextType = trim(@InfoTextType), JackpotTextType = trim(@JackpotTextType), " +
                " JackpotTime = @JackpotTime,BannerTextType = trim(@BannerTextType), " +
                " Multiplier = @Multiplier, SpinDelay1 = @SpinDelay1, SpinDelay2 = @SpinDelay2 " +
                " WHERE RowId = @RowID ";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
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
                    p.Add(new SqlParameter("@InfoTextType", paramz[32]));
                    p.Add(new SqlParameter("@JackpotTextType", paramz[33]));
                    p.Add(new SqlParameter("@BannerTextType", paramz[34]));
                    p.Add(new SqlParameter("@JackpotTime", paramz[35]));
                    p.Add(new SqlParameter("@CloseToWinStep", paramz[36]));
                    p.Add(new SqlParameter("@CloseToWinColour", paramz[37]));
                    p.Add(new SqlParameter("@Multiplier", paramz[38]));
                    p.Add(new SqlParameter("@SpinDelay1", paramz[39]));
                    p.Add(new SqlParameter("@SpinDelay2", paramz[40]));

                    connection.Open();
                    GetExample(command, p.ToArray());
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                catch (Exception ex)
                {
                    var log = new EventLog($"Error updating matris with RowID: {rowId}", null, ex);

                    Log(log);
                    // log and handle exception(s)
                }
                finally
                {
                    GetMatrixTable();
                }
        }

        public void UpdatePlayerLog(Playerlog log)
        {
            string query = "insertIntoLog";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))

                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("userid", log.userid);
                    command.Parameters.AddWithValue("condition", log.condition);
                    command.Parameters.AddWithValue("gamename", log.gamename);
                    command.Parameters.AddWithValue("moment", log.moment);
                    command.Parameters.AddWithValue("trial", log.trial);
                    command.Parameters.AddWithValue("balance_in", log.balance_in);
                    command.Parameters.AddWithValue("balance_out", log.balance_out);
                    command.Parameters.AddWithValue("stimuli", log.stimuli);
                    command.Parameters.AddWithValue("bet", log.bet);
                    command.Parameters.AddWithValue("outcome", log.outcome);
                    command.Parameters.AddWithValue("response", log.response);
                    command.Parameters.AddWithValue("timestamp_begin", log.timestamp_begin);
                    command.Parameters.AddWithValue("timestamp_R", log.timestamp_R);
                    command.Parameters.AddWithValue("timestamp_O", log.timestamp_O);
                    command.Parameters.AddWithValue("q_win_chance", log.questionForWinChance);
//                    command.Parameters.AddWithValue("figure_1", log.figure1);
//                    command.Parameters.AddWithValue("figure_2", log.figure2);
//                    command.Parameters.AddWithValue("figure_3", log.figure3);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    var evLog = new EventLog($"Exception adding account: {log.userid}", log.userid, ex);

                    Log(evLog);
                }
        }

        public void UpdateText(string textName, string infotext)
        {
            string query = "UPDATE InfoText SET Text = @Text" +
                            " WHERE Text_Name=" + "'" + textName + "'";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))

                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Text", infotext);

                    int rows = command.ExecuteNonQuery();

                    //rows number of record got updated
                }
                catch (SqlException ex)
                {
                    var log = new EventLog("Admin page error", null, ex);

                    Log(log);
                }
        }

        private double GetDoubleDefault0(DataRow dr, string ColumnName1)
        {
            string value = GetValueFromColumnWithName(dr, ColumnName1);
            return Convert.ToDouble(value != "" ? value : "0");
        }

        private int GetIntValueDefault0(DataRow dr, string ColumnName)
        {
            string value = GetValueFromColumnWithName(dr, ColumnName);
            return Convert.ToInt32(value != "" ? value : "0");
        }

        private int GetIntValueDefault1(DataRow dr, string ColumnName)
        {
            string value = GetValueFromColumnWithName(dr, ColumnName);
            return Convert.ToInt32(value != "" ? value : "1");
        }

        private Boolean GetBooleanValue(DataRow dr, string ColumnName)
        {
            return dr[dr.Table.Columns[ColumnName].Ordinal].Equals(true);
        }

        private string GetValueFromColumnWithName(DataRow dr, string columnName)
        {
            return dr[dr.Table.Columns[columnName].Ordinal].ToString() != "" ? dr[dr.Table.Columns[columnName].Ordinal].ToString() : "";
        }

        private void GetExample(SqlCommand command, params SqlParameter[] p)
        {
            if (p != null && p.Any())
            {
                command.Parameters.AddRange(p);
            }
        }
    }
}