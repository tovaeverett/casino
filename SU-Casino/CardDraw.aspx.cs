﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SU_Casino
{
    public partial class CardDraw : System.Web.UI.Page
    {
        private int CheckCard;
        public int money;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                setCards();
                money = 1500;
                lblMoney.Text = money.ToString();
            }

        }

        public string randomCard(int min)
        {
            int max = 0;
            Random rnd = new Random();
            if(min < 8)
            {
                max = min +5;
            }
            else if(min >= 8)
            {
                min = 8;
                max = 13;
            }

            int randomcard = rnd.Next(min, max);
            Random letter = new Random();
            int num = letter.Next(0, 3); // Zero to 25
            char let = (char)('a' + num);
            // string url = "src/images/cards/" + randomcard + "C.png";
            string card = randomcard.ToString()+ let;
            return card;
        }


        public int randomStartCard()
        {
            Random rnd = new Random();

            int randomcard = rnd.Next(0, 12);
            //string url = "~/Cards/" + randomcard + ".png";
            CheckCard = randomcard;
            return randomcard;
        }

        public void setCards()
        {
            HiddenField_card3.Value = randomStartCard().ToString();
            HiddenField_card2.Value = randomCard(CheckCard).ToString();
            HiddenField_card1.Value = randomCard(CheckCard).ToString();
        }
        protected void btnPlay_Click(object sender, EventArgs e)
        {
            checkForWin();
            setCards();
        }
        private void checkForWin()
        {
           int CardPressed = 0; 
           var winLose = HiddenField_WinLose.Value;
           if(HiddenField_card1.Value != null)
            {
                CardPressed = 1;
            }
           else
            {
                CardPressed = 2;
            }
            SaveToDB(CardPressed, winLose);
        }
        public void SaveToDB(int card, string WinLose)
        {

        }
    }
}