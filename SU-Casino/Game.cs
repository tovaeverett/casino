using System;
using System.Collections.Generic;

namespace SU_Casino
{
    public class Game
    {
        private const string Theme_RED = "1";
        private const string Theme_BLUE = "2";
        private const string Theme_GOLD = "3";
        private const string Theme_BLACK = "4";
        private const string Theme_RED_Version_A = Theme_RED;
        private const string Theme_RED_Version_B = "5";
        private const string Theme_RED_Version_C = "6";
        private String name;
        private String condition;
        private int sequence;
        private int trials;
        private int saldo;
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
//            throw new Exception("Not allowd to use dummy game at the moment!");
            Game dummy = new Game();
            dummy.UserId = "dummy_test";
            dummy.Name = "Roulette";
            dummy.Condition = "one.one";
            dummy.Sequence = 1;
            dummy.Trials = 1;
            dummy.Saldo = 1000;
            dummy.Bet_R1 = -10; // blått kort. eller rött. Definerar bettinstats på de kortet eller knappen
            dummy.Bet_R2 = 0;  // rött kort. eller blått Definerar bettinstats på de kortet eller knappen.
            dummy.Bet_R3 = 100; 
            dummy.Bet_R4 = -50;
            dummy.Prob_O1 = 0.5;
            dummy.Prob_O2 = 0.5;
            dummy.Win_O1 = 50;
            dummy.Win_O2 = 100;
            dummy.Perc_S1 = 0;
            dummy.Perc_S2 = 1;
            dummy.Perc_S3 = 0;
            dummy.Perc_S4 = 0;
            dummy.IfS1win = "O2";
            dummy.IfS2win = "O2";
            dummy.IfS3win = "O1";
            dummy.IfS4win = "O1";
            dummy.IfS1probX = 1;
            dummy.IfS2probX = 1;
            dummy.If_R1 = "O1";
            dummy.If_R2 = "O1";
            dummy.If_R3 = "O2";
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
            return IsWinOrLoseBasedOnPercent(getWinningChanceCardDraw(cardPosition));
        }
        public bool didWinSlot()
        {
            return IsWinOrLoseBasedOnPercent(getWinningChanceOneArmedBandit());
        }

        public bool IsWinOrLoseBasedOnPercent(double winChance)
        {
            double winningNumber = RandomSingleton.NextDouble();
            return winChance >= winningNumber;
        }

        
        public double getWinningChanceCardDraw(string cardPosition)
        {
            string probO = "";
            if (cardPosition.Equals("R1"))
            {
                probO = If_R1;
            }
            else if (cardPosition.Equals("R2"))
            {
                probO = If_R2;
            }
            else if (cardPosition.Equals("R3"))
            {
                probO = If_R3;
            }
            else if (cardPosition.Equals("R4"))
            {
                probO = If_R4;
            }
            return CurrentTheme != null ? GetPercentBasedOnProbValueForTheme(CurrentTheme, probO) : GetPercentBasedProbValue(probO);
        }


        public double getWinningChanceOneArmedBandit()
        {
            double prob = 0;
            prob += GetPercentBasedOnProbValueForTheme(Theme_RED, IfS1win);
            prob += GetPercentBasedOnProbValueForTheme(Theme_BLUE, IfS2win);
            prob += GetPercentBasedOnProbValueForTheme(Theme_GOLD, IfS3win);
            prob += GetPercentBasedOnProbValueForTheme(Theme_BLACK, IfS4win);

            return prob;
        }

        private double GetPercentBasedOnProbValueForTheme(string theme, string O1orO2)
        {
            double prob = 0;
            if (CurrentTheme.Equals(theme))
            {
                prob = GetPercentBasedProbValue(O1orO2);
            }

            return addLogicForProbX(prob);
        }

        private double addLogicForProbX(double prob)
        {
            if (CurrentTheme.Equals(Theme_RED))
            {
                prob *= IfS1probX;
            }
            if (CurrentTheme.Equals(Theme_BLUE))
            {
                prob *= IfS2probX;
            }

            return prob;
        }

        private double GetPercentBasedProbValue(String O1orO2)
        {
            return O1orO2.Equals("O1") ? Prob_O1 : Prob_O2;
        }

        public string getRandomThemeBasedOnProcAndVariant()
        {
            GameLogic gameLogic = new GameLogic();
            CurrentTheme = ChangeTemeBasedOnThemeVariant(gameLogic.CalculateCurrentThemeBasedOnPercent(getThemes()));
            return CurrentTheme;

        }

        private string ChangeTemeBasedOnThemeVariant(string theme)
        {
            if (theme.Equals(Theme_RED)) 
            {
                if (ThemeVariant == "B")
                    theme = Theme_RED_Version_B;
                else if (ThemeVariant == "C")
                    theme = Theme_RED_Version_C;
                else
                    theme = Theme_RED_Version_A;
            }
            return theme;
        }


        private Dictionary<string, double> getThemes()
        {
            Dictionary<string, double> themes = new Dictionary<string, double>();

            themes.Add(Theme_RED, Perc_S1);
            themes.Add(Theme_BLUE, Perc_S2);
            themes.Add(Theme_GOLD, Perc_S3);
            themes.Add(Theme_BLACK, Perc_S4);

            return themes;
        }
    }
}