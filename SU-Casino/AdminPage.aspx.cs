using SU_Casino.model;
using SU_Casino.service;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace SU_Casino
{
    public partial class AdminPage : System.Web.UI.Page
    {
        IDataService dataService = new DBDataService();
        protected DropDownList ddlInfoTextType;
        protected Label lblInfoTextType;
        protected DropDownList ddlBannerTextType;
        protected Label lblBannerTextType;
        protected DropDownList ddlJackpotTextType;
        protected Label lblJackpotTextType;
        protected Label lblName;

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
            txtText.Text = dataService.GetText((AllTextType)Enum.Parse(typeof(AllTextType), ddlText.SelectedItem.Text));
        }

        private void FillDropDown()
        {
            var texts = dataService.GetTexts();

            texts.ForEach(x => ddlText.Items.Add(x));
        }

        private void UpdateText()
        {
            dataService.UpdateText(ddlText.SelectedItem.Text, txtText.Text);
        }

        protected void btnText_Click(object sender, EventArgs e)
        {
            UpdateText();
        }

        protected void DropDownListInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fetch dropdownlist that are selected
            foreach (GridViewRow row in gvMatris.Rows)
            {
                ddlInfoTextType = (DropDownList)row.FindControl("ddlInfoTextType");

                // Dropdownlist is null (not found) if not selected
                if (ddlInfoTextType != null)
                {
                    if (!ddlInfoTextType.SelectedValue.Equals(""))
                    {
                        //Change selected texttype in "Text for pages", clear previous selection
                        ddlText.ClearSelection();
                        ddlText.Items.FindByText(ddlInfoTextType.SelectedValue).Selected = true;

                        //Display correct text in "Text for pages"
                        SelectText();
                    }
                    lblInfoTextType = (row.FindControl("lblInfoTextType") as Label);
                    lblInfoTextType.Text = ddlInfoTextType.SelectedValue;

                    return;
                }
            }
        }

        protected void DropDownListBanner_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fetch dropdownlist that are selected
            foreach (GridViewRow row in gvMatris.Rows)
            {
                ddlBannerTextType = (row.FindControl("ddlBannerTextType") as DropDownList);

                // Dropdownlist is null (not found) if not selected
                if (ddlBannerTextType != null)
                {
                    if (!ddlBannerTextType.SelectedValue.Equals(""))
                    {
                        //Change selected texttype in "Text for pages", clear previous selection
                        ddlText.ClearSelection();
                        ddlText.Items.FindByText(ddlBannerTextType.SelectedValue).Selected = true;

                        //Display correct text in "Text for pages"
                        SelectText();
                    }
                    lblBannerTextType = (row.FindControl("lblBannerTextType") as Label);
                    lblBannerTextType.Text = ddlBannerTextType.SelectedValue;

                    return;
                }
            }
        }

        protected void DropDownListJackpot_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fetch dropdownlist that are selected
            foreach (GridViewRow row in gvMatris.Rows)
            {
                ddlJackpotTextType = (DropDownList)row.FindControl("ddlJackpotTextType");

                // Dropdownlist is null (not found) if not selected
                if (ddlJackpotTextType != null)
                {
                    if (!ddlJackpotTextType.SelectedValue.Equals(""))
                    {
                        //Change selected texttype in "Text for pages", clear previous selection
                        ddlText.ClearSelection();
                        ddlText.Items.FindByText(ddlJackpotTextType.SelectedValue).Selected = true;

                        //Display correct text in "Text for pages"
                        SelectText();
                    }
                    lblJackpotTextType = (row.FindControl("lblJackpotTextType") as Label);
                    lblJackpotTextType.Text = ddlJackpotTextType.SelectedValue;

                    return;
                }
            }
        }

        private void FillMatrixDropDown(int MatrixRowIndex)
        {
            if (gvMatris.Rows[MatrixRowIndex].RowType == DataControlRowType.DataRow)
            {
                TextBox GameName = (gvMatris.Rows[MatrixRowIndex].FindControl("txtEditName") as TextBox);
                Array infoTextTypes = null;
                Array bannerTextTypes = null;
                Array jackpotTextTypes = null;

                switch (GameName.Text)
                {
                    case "Instrumental_acq":
                        infoTextTypes = Enum.GetNames(typeof(InfoTextTypeCard));
                        bannerTextTypes = Enum.GetNames(typeof(BannerTextTypeCard));
                        jackpotTextTypes = Enum.GetNames(typeof(JackpotTextTypeCard));
                        break;
                    case "Pavlovian_acq":
                        infoTextTypes = Enum.GetNames(typeof(InfoTextTypeSlot));
                        bannerTextTypes = Enum.GetNames(typeof(BannerTextTypeSlot));
                        jackpotTextTypes = Enum.GetNames(typeof(JackpotTextTypeSlot));
                        break;
                    case "Roulette":
                        infoTextTypes = Enum.GetNames(typeof(InfoTextTypeRoulette));
                        break;
                    default:
                        break;
                }

                //Welcome texttypes
                //Fetch and fill dropdown infotexts
                ddlInfoTextType = (gvMatris.Rows[MatrixRowIndex].FindControl("ddlInfoTextType") as DropDownList);

                //First row is a empty text, means no banner selected
                ddlInfoTextType.Items.Add("");
                if (infoTextTypes != null) 
                {
                    foreach (string itt in infoTextTypes)
                    {
                        ddlInfoTextType.Items.Add(itt);
                    }
                } 

                //Find selected bannertexttype (saved in label from database)
                lblInfoTextType = (gvMatris.Rows[MatrixRowIndex].FindControl("lblInfoTextType") as Label);
                ddlInfoTextType.Items.FindByText(lblInfoTextType.Text).Selected = true;

                //Jackpot bannertext
                //Fetch and fill dropdown 
                ddlBannerTextType = (gvMatris.Rows[MatrixRowIndex].FindControl("ddlBannerTextType") as DropDownList);

                //First row is a empty text, means no banner selected
                ddlBannerTextType.Items.Add("");
                if (bannerTextTypes != null)
                {
                    foreach (string btt in bannerTextTypes)
                    {
                        ddlBannerTextType.Items.Add(btt);
                    }
                }
                
                //Find selected bannertexttype (saved in label from database)
                lblBannerTextType = (gvMatris.Rows[MatrixRowIndex].FindControl("lblBannerTextType") as Label);
                ddlBannerTextType.Items.FindByText(lblBannerTextType.Text).Selected = true;

                //Jackpot text
                //Fetch and fill dropdown 
                ddlJackpotTextType = (gvMatris.Rows[MatrixRowIndex].FindControl("ddlJackpotTextType") as DropDownList);

                //First row is a empty text, means no banner selected
                ddlJackpotTextType.Items.Add("");
                if (jackpotTextTypes != null)
                {
                    foreach (string btt in jackpotTextTypes)
                    {
                        ddlJackpotTextType.Items.Add(btt);
                    }
                }

                //Find selected jackpottexttype (saved in label from database)
                lblJackpotTextType = (gvMatris.Rows[MatrixRowIndex].FindControl("lblJackpotTextType") as Label);
                ddlJackpotTextType.Items.FindByText(lblJackpotTextType.Text).Selected = true;
            }
        }

        public void GetMatris()
        {
            DataTable MatrisTable = dataService.GetMatrixTable();

            gvMatris.DataSource = MatrisTable;
            gvMatris.DataBind();

            //foreach (DataRow dr in matris.Rows)
            //{
            //gvMatris.DataSource = ds.Tables[0];
            //gvMatris.DataBind();
            // msg = dr[0].ToString();
            //}
        }

        protected void gvMatris_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMatris.EditIndex = e.NewEditIndex;
            gvMatris.Rows[e.NewEditIndex].RowState = DataControlRowState.Edit;

            GetMatris();

            //Fill in dropdownlists in matrix
            FillMatrixDropDown(e.NewEditIndex);
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
            TextBox txtclosetowinstep = (TextBox)row.FindControl("txtEditCloseToWinStep");
            TextBox txtclosetowincolour = (TextBox)row.FindControl("txtEditCloseToWinColour");
            //TextBox txtInfoTextType = (TextBox)row.FindControl("txtEditInfoTextType");
            Label lblinfotexttype = (Label)row.FindControl("lblInfoTextType");
            //TextBox txtjackpottexttype = (TextBox)row.FindControl("txtEditJackpotTextType");
            Label lbljackpottexttype = (Label)row.FindControl("lblJackpotTextType");
            TextBox txtjackpottime = (TextBox)row.FindControl("txtEditJackpotTime");
            //TextBox txtBannerTextType = (TextBox)row.FindControl("txtEditBannerTextType");
            Label lblbannertexttype = (Label)row.FindControl("lblBannerTextType");
            TextBox txtmultiplier = (TextBox)row.FindControl("txtEditMultiplier");
            TextBox txtspindelay1 = (TextBox)row.FindControl("txtEditSpinDelay1");
            TextBox txtspindelay2 = (TextBox)row.FindControl("txtEditSpinDelay2");

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
                txtfreeze_win.Text,
                lblinfotexttype.Text,
                lbljackpottexttype.Text,
                lblbannertexttype.Text,
                txtjackpottime.Text,
                txtclosetowinstep.Text,
                txtclosetowincolour.Text,
                txtmultiplier.Text,
                txtspindelay1.Text,
                txtspindelay2.Text
            };

            dataService.UpdateMatris(lblRowId.Text, paramz);
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

            dataService.DeleteMatris(lblRowId.Text);
            GetMatris();
        }

        protected void gvMatris_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMatris.EditIndex = -1;

            GetMatris();
        }

        private void InsertMatris()
        {
            dataService.InsertMatris();

            GetMatris();
        }

        protected void addRow_Click(object sender, EventArgs e)
        {
            InsertMatris();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {

            dataService.GetReport();



        }

        protected void btnResetMatris_Click(object sender, EventArgs e)
        {
            dataService.ResetMatris();
            GetMatris();
        }

        protected void btnReportQuestions_Click(object sender, EventArgs e)
        {
            dataService.GetQuestionReports();
        }
    }
}
