using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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

        protected void btnCreateReport_Click(object sender, EventArgs e)
        {

        }

        public void getData()
        {
            string NewconnectionString = "myCoonectionString";
            StreamWriter CsvfileWriter = new StreamWriter(@"D:\testfile.csv");
            string sqlselectQuery = "select * from Mytable";
            SqlCommand sqlcmd = new SqlCommand();

            SqlConnection spContentConn = new SqlConnection(NewconnectionString);
            sqlcmd.Connection = spContentConn;
            sqlcmd.CommandTimeout = 0;
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = sqlselectQuery;
            spContentConn.Open();
            using (spContentConn)
            {
                using (SqlDataReader sdr = sqlcmd.ExecuteReader())
                using (CsvfileWriter)
                {
                    //This Block of code for getting the Table Headers
                    DataTable Tablecolumns = new DataTable();

                    for (int i = 0; i < sdr.FieldCount; i++)
                    {
                        Tablecolumns.Columns.Add(sdr.GetName(i));
                    }
                    //  CsvfileWriter.WriteLine(string.Join(",", Tablecolumns.Columns.Cast<datacolumn>().Select(csvfile => csvfile.ColumnName)));
                    //This block of code for getting the Table Headers

                    while (sdr.Read())
                    {

                    }
                    //based on your Table columns you can increase and decrese columns
                    //  YourWriter.WriteLine(sdr[0].ToString() + "," + sdr[1].ToString() + "," + sdr[2].ToString() + "," + sdr[3].ToString() + "," + sdr[4].ToString() + "," + sdr[5].ToString() + "," + sdr[6].ToString() + "," + sdr[7].ToString() + "," + sdr[8].ToString() + "," + sdr[9].ToString() + "," + sdr[10].ToString() + "," + sdr[11].ToString() + ",");

                }
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


            //gvMatris.EditIndex = -1;
            //conn.Open();
            //SqlCommand cmd = new SqlCommand("update  emp set marks=" + textmarks.Text + " , name='" + textname.Text + "' where rowid=" + lbl.Text + "", conn);
            //cmd.ExecuteNonQuery();
            //conn.Close();
            //bind();

        }

        public void updateMatris(GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gvMatris.Rows[e.RowIndex];
            Label lblRowId = (Label)row.FindControl("lblrow");
            TextBox txtprop_n = (TextBox)row.FindControl("txtEditProp_n");
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
        }
    }
}