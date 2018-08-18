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
        Database _database = new Database();

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
            txtText.Text = _database.getText(ddlText.SelectedItem.Text);
        }
        private void FillDropDown()
        {
            var texts = _database.getTexts();

            texts.ForEach(x => ddlText.Items.Add(x));
        }
        private void UpdateText()
        {
            _database.UpdateText(ddlText.SelectedItem.Text, txtText.Text);
        }

        protected void btnText_Click(object sender, EventArgs e)
        {
            UpdateText();
        }

        public void GetMatris()
        {
            var matris = _database.GetMatris();

            var ds = matris.ds;

            foreach (DataRow dr in matris.rows)
            {
                gvMatris.DataSource = ds.Tables[0];
                gvMatris.DataBind();
                // msg = dr[0].ToString();

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
            TextBox txtseq = (TextBox)row.FindControl("txtEditSeq");
            TextBox txttrials = (TextBox)row.FindControl("txtEditTrials");
            TextBox txtname = (TextBox)row.FindControl("txtEditName");
            TextBox txtsaldo = (TextBox)row.FindControl("txtEditSaldo");
            TextBox txtperc_S0 = (TextBox)row.FindControl("txtEditperc_S0");
            TextBox txtperc_S1 = (TextBox)row.FindControl("txtEditperc_S1");
            TextBox txtS1_variant = (TextBox)row.FindControl("txtEditS1_variant");
            TextBox txtperc_S2 = (TextBox)row.FindControl("txtEditperc_S2");
            TextBox txtperc_S3 = (TextBox)row.FindControl("txtEditperc_S3");
            TextBox txtperc_S4 = (TextBox)row.FindControl("txtEditperc_S4");
            TextBox txtbet_R1 = (TextBox)row.FindControl("txtEditbet_R1");
            TextBox txtbet_R2 = (TextBox)row.FindControl("txtEditbet_R2");
            TextBox txtbet_R3 = (TextBox)row.FindControl("txtEditbet_R3");
            TextBox txtbet_B4 = (TextBox)row.FindControl("txtEditbet_B4");
            TextBox txtif_R1 = (TextBox)row.FindControl("txtEditIf_R1");
            TextBox txtif_R2 = (TextBox)row.FindControl("txtEditIf_R2");
            TextBox txtif_R3 = (TextBox)row.FindControl("txtEditIf_R3");
            TextBox txtif_R4 = (TextBox)row.FindControl("txtEditIf_R4");
            TextBox txtprob_O1 = (TextBox)row.FindControl("txtEditprob_O1");
            TextBox txtprob_O2 = (TextBox)row.FindControl("txtEditprob_O2");
            TextBox txtwin_O1 = (TextBox)row.FindControl("txtEditwin_O1");
            TextBox txtwin_O2 = (TextBox)row.FindControl("txtEditwin_O2");
            TextBox txtifS1win = (TextBox)row.FindControl("txtEditifS1win");
            TextBox txtifS2win = (TextBox)row.FindControl("txtEditifS2win");
            TextBox txtifS3win = (TextBox)row.FindControl("txtEditifS3win");
            TextBox txtifS4win = (TextBox)row.FindControl("txtEditifS4win");
            TextBox txtifS1probX = (TextBox)row.FindControl("txtEditifS1probX");
            TextBox txtifS2probX = (TextBox)row.FindControl("txtEditifS2probX");
            TextBox txthide = (TextBox)row.FindControl("txtEdithide");
            TextBox txtfreeze_win = (TextBox)row.FindControl("txtEditFreeze_win");

            var paramz = new[] 
            {
                txtprop_n.Text,
                txtcondition.Text,
                txtseq.Text,
                txttrials.Text,
                txtname.Text,
                txtsaldo.Text,
                txtperc_S0.Text,
                txtperc_S1.Text,
                txtS1_variant.Text,
                txtperc_S2.Text,
                txtperc_S3.Text,
                txtperc_S4.Text,
                txtbet_R1.Text,
                txtbet_R2.Text,
                txtbet_R3.Text,
                txtbet_B4.Text,
                txtif_R1.Text,
                txtif_R2.Text,
                txtif_R3.Text,
                txtif_R4.Text,
                txtprob_O1.Text,
                txtprob_O2.Text,
                txtwin_O1.Text,
                txtwin_O2.Text,
                txtifS1win.Text,
                txtifS2win.Text,
                txtifS3win.Text,
                txtifS4win.Text,
                txtifS1probX.Text,
                txtifS2probX.Text,
                txthide.Text,
                txtfreeze_win.Text
            };

            _database.UpdateMatris(lblRowId.Text, paramz);
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

            _database.DeleteMatris(lblRowId.Text);
            GetMatris();
        }
        
        protected void gvMatris_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMatris.EditIndex = -1;

            GetMatris();
        }

        private void InsertMatris()
        {
            _database.InsertMatris();

            GetMatris();
        }

        protected void AddRow_Click(object sender, EventArgs e)
        {
            InsertMatris();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            
            _database.GetReport();
           


        }

        protected void btnResetMatris_Click(object sender, EventArgs e)
        {
            _database.resetMatris();
        }

        protected void btnReportQuestions_Click(object sender, EventArgs e)
        {
            _database.GetQuestionReports();
        }
    }
}
