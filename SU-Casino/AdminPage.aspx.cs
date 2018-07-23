using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SU_Casino
{
    public partial class AdminPage : System.Web.UI.Page
    {
        public SqlConnection connectionstring = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                HiddenField1.Value = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                FillDropDown();
                GetMatris();
            }



        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectText();
        }

        private void SelectText()
        {
            string sqlselectQuery = "select Text from InfoText where Text_Name = " + "'" + ddlText.SelectedItem.Text + "'";
            SqlCommand sqlcmd = new SqlCommand();

            SqlConnection spContentConn = new SqlConnection(HiddenField1.Value);
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
                        txtText.Text = sdr.GetString(0);
                    }
                }
            }
            spContentConn.Close();
            spContentConn.Dispose();

        }
        private void FillDropDown()
        {
            string sqlselectQuery = "select Text_Id, Text, Text_Name from InfoText";
            SqlCommand sqlcmd = new SqlCommand();

            SqlConnection spContentConn = new SqlConnection(HiddenField1.Value);
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
                        ddlText.Items.Add(sdr.GetString(2));
                    }
                }
            }
            spContentConn.Close();
            spContentConn.Dispose();
        }
        private void UpdateText()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(HiddenField1.Value))
                {
                    conn.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("UPDATE InfoText SET Text=@Text" +
                            " WHERE Text_Name=" + "'" + ddlText.SelectedItem.Text + "'", conn))
                    {
                        cmd.Parameters.AddWithValue("@Text", txtText.Text);


                        int rows = cmd.ExecuteNonQuery();

                        //rows number of record got updated
                    }
                }
            }
            catch (SqlException ex)
            {
                //Log exception
                //Display Error message
            }
        }

        protected void btnText_Click(object sender, EventArgs e)
        {
            UpdateText();
        }
        public void GetMatris()
        {
            try
            {
                SqlConnection con = new SqlConnection(HiddenField1.Value);
                var sql = "getMatris";
                var da = new SqlDataAdapter(sql, con);
                var ds = new DataSet();
                DataTable dt = new DataTable();

                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                //da.SelectCommand.Parameters.AddWithValue("@UserId", lblUserId.Text);


                da.Fill(ds, "getMatris");
                dt = ds.Tables["getMatris"];

                foreach (DataRow dr in dt.Rows)
                {
                    gvMatris.DataSource = ds.Tables[0];
                    gvMatris.DataBind();
                    // msg = dr[0].ToString();

                }
            }
            catch (Exception e)
            {
                // msg = "Error trying login user : " + txtUsername.Text;
            }
        }

        protected void gvMatris_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMatris.EditIndex = e.NewEditIndex;
            gvMatris.Rows[e.NewEditIndex].RowState = DataControlRowState.Edit;
            GetMatris();
        }

        protected void gvMatris_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            UpdateMatris(e);
            gvMatris.EditIndex = -1;

            GetMatris();
        }
        public void UpdateMatris(GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvMatris.Rows[e.RowIndex];
            Label lblRowId = (Label)row.FindControl("lblRowId");
            TextBox txtprop_n = (TextBox)row.FindControl("txtEditProp_N");
            TextBox txtcondition = (TextBox)row.FindControl("txtEditcondition");
            TextBox txtmoment = (TextBox)row.FindControl("txtEditmoment");
            TextBox txtname = (TextBox)row.FindControl("txtEditname");
            TextBox txtprob_S0 = (TextBox)row.FindControl("txtEditprob_S0");
            TextBox txtperc_S1 = (TextBox)row.FindControl("txtEditperc_S1");
            TextBox txtperc_S2 = (TextBox)row.FindControl("txtEditperc_S2");
            TextBox txtperc_S3 = (TextBox)row.FindControl("txtEditperc_S3");
            TextBox txtperc_S4 = (TextBox)row.FindControl("txtEditperc_S4");
            TextBox txtbet_R1 = (TextBox)row.FindControl("txtEditbet_R1");
            TextBox txtbet_R2 = (TextBox)row.FindControl("txtEditbet_R2");
            TextBox txtprob_O1 = (TextBox)row.FindControl("txtEditprob_O1");
            TextBox txtprob_O2 = (TextBox)row.FindControl("txtEditprob_O2");
            TextBox txtwin_O1 = (TextBox)row.FindControl("txtEditwin_O1");
            TextBox txtwin_O2 = (TextBox)row.FindControl("txtEditwin_O2");
            TextBox txtifS0 = (TextBox)row.FindControl("txtEditifS0");
            TextBox txtifS1win = (TextBox)row.FindControl("txtEditifS1win");
            TextBox txtifS2win = (TextBox)row.FindControl("txtEditifS2win");
            TextBox txtifS3win = (TextBox)row.FindControl("txtEditifS3win");
            TextBox txtifS4win = (TextBox)row.FindControl("txtEditifS4win");
            TextBox txtifS1probX = (TextBox)row.FindControl("txtEditifS1probX");
            TextBox txtifS2probX = (TextBox)row.FindControl("txtEditifS2probX");
            TextBox txtifS3probX = (TextBox)row.FindControl("txtEditifS3probX");
            TextBox txtifS4probX = (TextBox)row.FindControl("txtEditifS4probX");
            TextBox txthide = (TextBox)row.FindControl("txtEdithide");



            string query = "UPDATE [matris] SET prop_n = @prop_n, condition = @condition, moment = @moment, name = @name, prob_S0 = @prob_S0," +
                " perc_S1 = @perc_S1, perc_S2 = @perc_S2, perc_S3 = @perc_S3, perc_S4 = @perc_S4, bet_R1 = @bet_R1, bet_R2 = @bet_R2, prob_O1 = @prob_O1, prob_O2 = @prob_O2, win_O1 = @win_O1," +
                " win_O2 = @win_O2, ifS0 = @ifS0, ifS1win = @ifS1win, ifS2win = @ifS2win, ifS3win = @ifS3win, ifS4win = @ifS4win, ifS1probX = @ifS1probX, ifS2probX = @ifS2probX,  " +
                "ifS3probX = @ifS3probX, ifS4probX = @ifS4probX , hide = @hide" +
                " WHERE RowId = @rowId";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString()))
            using (SqlCommand command = new SqlCommand(query, connection))

                try
                {
                    // open the connection, execute, etc
                    List<SqlParameter> p = new List<SqlParameter>();
                    p.Add(new SqlParameter("@rowid", lblRowId.Text));
                    p.Add(new SqlParameter("@prop_n", txtprop_n.Text));
                    p.Add(new SqlParameter("@condition", txtcondition.Text));
                    p.Add(new SqlParameter("@moment", txtmoment.Text));
                    p.Add(new SqlParameter("@name", txtname.Text));
                    p.Add(new SqlParameter("@prob_S0", txtprob_S0.Text));
                    p.Add(new SqlParameter("@perc_S1", txtperc_S1.Text));
                    p.Add(new SqlParameter("@perc_S2", txtperc_S2.Text));
                    p.Add(new SqlParameter("@perc_S3", txtperc_S3.Text));
                    p.Add(new SqlParameter("@perc_S4", txtperc_S4.Text));
                    p.Add(new SqlParameter("@bet_R1", txtbet_R1.Text));
                    p.Add(new SqlParameter("@bet_R2", txtbet_R2.Text));
                    p.Add(new SqlParameter("@prob_O1", txtprob_O1.Text));
                    p.Add(new SqlParameter("@prob_O2", txtprob_O2.Text));
                    p.Add(new SqlParameter("@win_O1", txtwin_O1.Text));
                    p.Add(new SqlParameter("@win_O2", txtwin_O2.Text));
                    p.Add(new SqlParameter("@ifS0", txtifS0.Text));
                    p.Add(new SqlParameter("@ifS1win", txtifS1win.Text));
                    p.Add(new SqlParameter("@ifS2win", txtifS2win.Text));
                    p.Add(new SqlParameter("@ifS3win", txtifS3win.Text));
                    p.Add(new SqlParameter("@ifS4win", txtifS4win.Text));
                    p.Add(new SqlParameter("@ifS1probX", txtifS1probX.Text));
                    p.Add(new SqlParameter("@ifS2probX", txtifS2probX.Text));
                    p.Add(new SqlParameter("@ifS3probX", txtifS3probX.Text));
                    p.Add(new SqlParameter("@ifS4probX", txtifS4probX.Text));
                    p.Add(new SqlParameter("@hide", txthide.Text));

                    connection.Open();
                    GetExample(command, p.ToArray());
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    GetMatris();


                }
                catch (Exception c)
                {
                    // log and handle exception(s)
                }
        }
        public void GetExample(SqlCommand command, params SqlParameter[] p)
        {
            if (p != null && p.Any())
            {
                command.Parameters.AddRange(p);
            }

        }

        protected void gvMatris_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DeleteMatris(e);
        }
        private void DeleteMatris(GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvMatris.Rows[e.RowIndex];
            Label lblRowId = (Label)row.FindControl("lblRowId");

            string query = "Delete from [matris] WHERE RowId = @rowId";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ToString()))
            using (SqlCommand command = new SqlCommand(query, connection))

                try
                {
                    // open the connection, execute, etc
                    List<SqlParameter> p = new List<SqlParameter>();
                    p.Add(new SqlParameter("@rowid", lblRowId.Text));

                    connection.Open();
                    GetExample(command, p.ToArray());
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    GetMatris();

                }
                catch (Exception c)
                {
                    // log and handle exception(s)
                }
        }



        protected void gvMatris_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMatris.EditIndex = -1;

            GetMatris();
        }

        private void InsertMatris()
        {
            using (SqlConnection openCon = connectionstring)
            {
                string saveStaff = "INSERT into matris (prop_n,condition,moment,name,prob_S0,perc_S1,perc_S2,perc_S3,perc_S4,bet_R1,bet_R2,prob_O1,prob_O2,win_O1,win_O2,ifS0,ifS1win,ifS2win,ifS3win,ifS4win,ifS1probX,ifS2probX,ifS3probX,ifS4probX,hide)" +
                    " VALUES (@prop_n,@condition,@moment,@name,@prob_S0,@perc_S1,@perc_S2,@perc_S3,@perc_S4,@bet_R1,@bet_R2,@prob_O1,@prob_O2,@win_O1,@win_O2,@ifS0,@ifS1win,@ifS2win,@ifS3win,@ifS4win,@ifS1probX,@ifS2probX,@ifS3probX,@ifS4probX,@hide)";

                using (SqlCommand Save = new SqlCommand(saveStaff))
                {
                    Save.Connection = openCon;

                    Save.Parameters.Add(new SqlParameter("@prop_n", ""));
                    Save.Parameters.Add(new SqlParameter("@condition", ""));
                    Save.Parameters.Add(new SqlParameter("@moment", ""));
                    Save.Parameters.Add(new SqlParameter("@name", ""));
                    Save.Parameters.Add(new SqlParameter("@prob_S0", ""));
                    Save.Parameters.Add(new SqlParameter("@perc_S1", ""));
                    Save.Parameters.Add(new SqlParameter("@perc_S2", ""));
                    Save.Parameters.Add(new SqlParameter("@perc_S3", ""));
                    Save.Parameters.Add(new SqlParameter("@perc_S4", ""));
                    Save.Parameters.Add(new SqlParameter("@bet_R1", ""));
                    Save.Parameters.Add(new SqlParameter("@bet_R2", ""));
                    Save.Parameters.Add(new SqlParameter("@prob_O1", ""));
                    Save.Parameters.Add(new SqlParameter("@prob_O2", ""));
                    Save.Parameters.Add(new SqlParameter("@win_O1", ""));
                    Save.Parameters.Add(new SqlParameter("@win_O2", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS0", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS1win", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS2win", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS3win", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS4win", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS1probX", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS2probX", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS3probX", ""));
                    Save.Parameters.Add(new SqlParameter("@ifS4probX", ""));
                    Save.Parameters.Add(new SqlParameter("@hide", ""));

                    openCon.Open();

                    Save.ExecuteNonQuery();
                }
                GetMatris();
            }


        }

        protected void AddRow_Click(object sender, EventArgs e)
        {
            InsertMatris();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            string strFilePath = @"C:\temp\testfile.csv";
            string strSeperator = ",";
            StringBuilder sbOutput = new StringBuilder();


            SqlConnection con = new SqlConnection(HiddenField1.Value);
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
                sbOutput.AppendLine(string.Join(strSeperator, dr[0]+ strSeperator+ dr[1] + strSeperator + dr[2] + strSeperator + dr[3] + strSeperator + dr[4] + strSeperator + dr[5] + strSeperator
                    + dr[6] + strSeperator + dr[7] + strSeperator + dr[8] + strSeperator + dr[9] + strSeperator + dr[10] + strSeperator + dr[11] + strSeperator + dr[12] + strSeperator + dr[13] + strSeperator));
            }

            File.WriteAllText(strFilePath, sbOutput.ToString());

            // To append more lines to the csv file
           // File.AppendAllText(strFilePath, sbOutput.ToString());

        }

    }
}












