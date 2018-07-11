﻿using System;
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
        int money;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                HiddenField_Spin1.Value = randomStartCard().ToString();
                HiddenField_Spin2.Value = randomStartCard().ToString();
                HiddenField_Spin3.Value = randomStartCard().ToString();
                checkIfWin();
                GetRandomTheme();
            }
        }

        protected void btnPull_Click(object sender, EventArgs e)
        {
            HiddenField_Spin1.Value = randomStartCard().ToString();
            HiddenField_Spin2.Value = randomStartCard().ToString();
            HiddenField_Spin3.Value = randomStartCard().ToString();
            checkIfWin();
        }
        public void checkIfWin()
        {
            if (HiddenField_Spin1.Value == HiddenField_Spin2.Value && HiddenField_Spin1.Value == HiddenField_Spin3.Value)
            {
                HiddenField_WinLose.Value = "Win";
                money = 100;
            }
            else
            {
                HiddenField_WinLose.Value = "Lose";
                money = -100;
            }
            lblMoney.Text = money.ToString();
        }
        public int randomStartCard()
        {
            int randomfruit = rnd.Next(0, 6);

            return randomfruit;
        }
        public void GetRandomTheme()
        {
            Random rnd = new Random();
            int randomTheme = rnd.Next(0, 4);

            HiddenField_Theme.Value = randomTheme.ToString();
        }
    }
}