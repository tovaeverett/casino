﻿using System;
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
            if (!IsPostBack)
            {
                HiddenFieldrouletteNr.Value = RandomSpin().ToString();
            }
        } 

        public int RandomSpin()
        {
            Random rnd = new Random();

            int randomNr = rnd.Next(0, 37);
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
    }
}