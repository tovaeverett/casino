using System;

namespace SU_Casino
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            Response.Redirect("StartPage.aspx");
        }
    }
}