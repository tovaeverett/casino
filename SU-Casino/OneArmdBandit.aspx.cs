using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SU_Casino
{
    public partial class OneArmdBandit : System.Web.UI.Page
    {
        Random rnd = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPull_Click(object sender, EventArgs e)
        {
            IMGslot1.ImageUrl = randomStartCard();
            IMGslot2.ImageUrl = randomStartCard();
            IMGslot3.ImageUrl = randomStartCard();
            checkIfWin();
        }
        public void checkIfWin()
        {
            if (IMGslot1.ImageUrl == IMGslot2.ImageUrl && IMGslot1.ImageUrl == IMGslot3.ImageUrl)
            {
                imgWin.ImageUrl = "~/Cards/youwin.png";
            }
            else
            {
                imgWin.ImageUrl = "~/Cards/youlose.png";
            }
        }
        public string randomStartCard()
        {


            int randomfruit = rnd.Next(0, 6);
            string url = "~/Fruit/" + randomfruit + ".png";

            return url;
        }
    }
}