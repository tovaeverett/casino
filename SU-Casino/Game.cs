using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SU_Casino
{
    public class Game
    {
        private String name;
        private String condition;
        private int sequence;
        private int trials;
        private int saldo;
        private double prob_S0;
        private int bet_R1;
        private int bet_R2;
        private int bet_R3;
        private int bet_R4;
        private double prob_O1;
        private double prob_O2;
        private int win_O1;
        private int win_O2;
        private String themeVariant;
        private int ifS1probX;
        private int ifS2probX;
        private double perc_S1;
        private double perc_S2;
        private double perc_S3;
        private double perc_S4;
        private string userid;
        private String ifS1win, ifS2win, ifS3win, ifS4win;
        private string currentTheme;

        public string Name { get => name; set => name = value; }
        public int Trials { get => trials; set => trials = value; }
        public int Saldo { get => saldo; set => saldo = value; }
        public int Bet_R1 { get => bet_R1; set => bet_R1 = value; }
        public int Bet_R2 { get => bet_R2; set => bet_R2 = value; }
        public int Bet_R3 { get => bet_R3; set => bet_R3 = value;}
        public int Bet_R4 { get => bet_R4; set => bet_R4 = value; }
        public double Prob_O1 { get => prob_O1; set => prob_O1 = value; }
        public double Prob_O2 { get => prob_O2; set => prob_O2 = value; }
        public int Win_O1 { get => win_O1; set => win_O1 = value; }
        public int Win_O2 { get => win_O2; set => win_O2 = value; }
        public string Condition { get => condition; set => condition = value; }
        public int Sequence { get => sequence; set => sequence = value; }
        public string ThemeVariant { get => themeVariant; set => themeVariant = value; }
        public int IfS1probX { get => ifS1probX; set => ifS1probX = value; }
        public int IfS2probX { get => ifS2probX; set => ifS2probX = value; }
        public double Perc_S1 { get => perc_S1; set => perc_S1 = value; }
        public double Perc_S2 { get => perc_S2; set => perc_S2 = value; }
        public double Perc_S3 { get => perc_S3; set => perc_S3 = value; }
        public double Perc_S4 { get => perc_S4; set => perc_S4 = value; }
        public string IfS1win { get => ifS1win; set => ifS1win = value; }
        public string IfS2win { get => ifS2win; set => ifS2win = value; }
        public string IfS3win { get => ifS3win; set => ifS3win = value; }
        public string IfS4win { get => ifS4win; set => ifS4win = value; }
           
        public string UserId { get => userid; set => userid = value; }
        public string CurrentTheme { get => currentTheme; set => currentTheme = value; }
        public string If_R1 { get; set; }
        public string If_R2 { get; set; }
        public string If_R3 { get; set; }
        public string If_R4 { get; set; }

        public static Game getDummyGame()
        {
            Game dummy = new Game();
            dummy.Name = "Pavlovian_extinct";
            dummy.Condition = "Pavlovian_extinct";
            dummy.Sequence = 1;
            dummy.Trials = 24;
            dummy.Saldo = 1000;
            dummy.Bet_R1 = -10; // blått kort. eller rött. Definerar bettinstats på de kortet eller knappen
            dummy.Bet_R2 = 0;  // rött kort. eller blått Definerar bettinstats på de kortet eller knappen.
            dummy.Bet_R3 = 100; 
            dummy.Bet_R4 = -50;
            dummy.Prob_O1 = 0;
            dummy.Prob_O2 = 1;
            dummy.Win_O1 = 50;
            dummy.Win_O2 = 20;
            dummy.Perc_S1 = 0.25;
            dummy.Perc_S2 = 0.25;
            dummy.Perc_S3 = 0.25;
            dummy.Perc_S4 = 0.25;
            dummy.IfS1win = "O1";
            dummy.IfS2win = "O1";
            dummy.IfS3win = "O2";
            dummy.IfS4win = "O2";
            dummy.IfS1probX = 1;
            dummy.IfS2probX = 0;
            dummy.If_R1 = "O1";
            dummy.If_R2 = "O2";
            dummy.If_R3 = "O1";
            dummy.If_R4 = "O2";
            return dummy;

        }

        /// <summary>
        /// Creates and returns a int array within min and max range values. winningNumber is excluded.
        /// </summary>
        /// <param name="min">The lowest value for the int array.</param>
        /// <param name="max">The highest value for the int array.</param>
        /// <param name="winningNumber">A "winning number", which will be excluded from the array.</param>
        /// <returns>A int array.</returns>
        public int[] RetrieveLosingNumbers(int min, int max, int winningNumber)
        {
            var range = Math.Abs(min - max) + 1;
            var losers = new List<int>(range);

            for (int i = 0; i < range; i++)
            {
                losers.Add(min + i);
            }

            var winnerIndex = losers.FindIndex(x => x == winningNumber);
            losers.RemoveAt(winnerIndex);

            return losers.ToArray();
        }


        public bool didWinDrawCards(String cardPosition)
        {
            return CalculateWinOrNot(getWinningChanceCardDraw(cardPosition), new Random());
        }
        public bool didWinSlot()
        {
            return CalculateWinOrNot(getWinningChanceOneArmedBandit(), new Random());
        }

        public bool CalculateWinOrNot(double winChance, Random rand)
        {
            double winningNumber = rand.NextDouble();
            return winChance >= winningNumber ? true : false;
        }

        
        public double getWinningChanceCardDraw(string cardPosition)
        {
            string probO = "";
            if (cardPosition.Equals("R1"))
                probO = If_R1;
            else if (cardPosition.Equals("R2"))
                probO = If_R2;
            else if (cardPosition.Equals("R3"))
                probO = If_R3;
            else if (cardPosition.Equals("R4"))
                probO = If_R4;

            return probO.Equals("O1") ? Prob_O1 : Prob_O2;
        }


        public double getWinningChanceOneArmedBandit()
        {
            double prob = 0;
            prob += GetPercentBasedOnIfSxwinForGame("1", IfS1win) * IfS1probX;
            prob += GetPercentBasedOnIfSxwinForGame("2", IfS2win) * IfS2probX;
            prob += GetPercentBasedOnIfSxwinForGame("3", IfS3win);
            prob += GetPercentBasedOnIfSxwinForGame("4", IfS4win);

            return prob;
        }

        private double GetPercentBasedOnIfSxwinForGame(string game, string ifSxwin)
        {
            double prob = 0;
            if (CurrentTheme.Equals(game))
            {
                prob = GetPercentBasedOnIfSxwin(ifSxwin);
            }
            return prob;
        }

        private double GetPercentBasedOnIfSxwin(String ifwin)
        {
            return ifwin.Equals("O1") ? Prob_O1 : Prob_O2;
        }

        public string getRandomThemeBasedOnProcAndVariant()
        {
            CurrentTheme = ChangeTemeBasedOnThemeVariant(GameLogic.CalculateCurrentThemeBasedOnPercent(getThemes()));
            return CurrentTheme;

        }

        private string ChangeTemeBasedOnThemeVariant(string theme)
        {
            if (theme.Equals("1")) //perc_S1 -> themeRed
            {
                if (ThemeVariant == "B")
                    theme = "5";
                else if (ThemeVariant == "C")
                    theme = "6";
            }
            return theme;
        }


        private Dictionary<string, double> getThemes()
        {
            Dictionary<string, double> themes = new Dictionary<string, double>();

            themes.Add("1", Perc_S1);
            themes.Add("2", Perc_S2);
            themes.Add("3", Perc_S3);
            themes.Add("4", Perc_S4);

            return themes;
        }
    }
}