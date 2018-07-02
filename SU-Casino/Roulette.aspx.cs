using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SU_Casino
{
    public partial class Roulette : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        } 

        public string RandomSpin()
        {
            Random rnd = new Random();

            int randomNr = rnd.Next(0, 37);
            lblNr.Text = randomNr.ToString();
            if (IsEven(randomNr) == true)
            {
                if (RadioButtonList1.SelectedValue == "Black")
                {
                    imgWinLose.ImageUrl = "~/Cards/youwin.png";
                }
                else
                {
                    imgWinLose.ImageUrl = "~/Cards/youlose.png";
                } 
                return "Black";
            }
            else
            {
                if (RadioButtonList1.SelectedValue == "Red")
                { 
                    imgWinLose.ImageUrl = "~/Cards/youwin.png";
                }
                else
                {
                    imgWinLose.ImageUrl = "~/Cards/youlose.png";
                }
                return "Red";
            }


        }

        public static bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        protected void btnSpin_Click(object sender, EventArgs e)
        {
            lblColor.Text = RandomSpin();
        }
    }
}