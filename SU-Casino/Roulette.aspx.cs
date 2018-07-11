﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SU_Casino
{
    public partial class Roulette : System.Web.UI.Page
    {
        private static int credit = 1500;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HiddenFieldrouletteNr.Value = RandomSpin().ToString();
                //lblCredit.Text = credit.ToString();

                
            }
        }

        public int GetCssRandom()
        {
            Random rnd = new Random();

            var rndNr = rnd.Next(0, 2);

            return rndNr;
        }

        public int RandomSpin()
        {
            Random rnd = new Random();

            int randomNr = rnd.Next(1, 37);
            lblNr.Text = randomNr.ToString();
            return randomNr;
        }

        public static bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        protected void btnSpin_Click(object sender, EventArgs e)
        {
            RandomSpin();
        }

        [WebMethod]
        public static void WinOrLose(string isWin, string betOptions, string expectedWinningChance)
        {
            credit = bool.Parse(isWin) ? credit + 100 : credit - 100;

            var t = isWin;
            var t2 = betOptions;
            var t3 = expectedWinningChance;

            // send to DB
        }
    }
}