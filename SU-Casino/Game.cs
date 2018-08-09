using System;
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
        private int bet_R1;
        private int bet_R2;
        private double prob_O1;
        private double prob_O2;
        private int win_O1;
        private int win_O2;
        private String themeVariant;

        public string Name { get => name; set => name = value; }
        public int Trials { get => trials; set => trials = value; }
        public int Saldo { get => saldo; set => saldo = value; }
        public int Bet_R1 { get => bet_R1; set => bet_R1 = value; }
        public int Bet_R2 { get => bet_R2; set => bet_R2 = value; }
        public double Prob_O1 { get => prob_O1; set => prob_O1 = value; }
        public double Prob_O2 { get => prob_O2; set => prob_O2 = value; }
        public int Win_O1 { get => win_O1; set => win_O1 = value; }
        public int Win_O2 { get => win_O2; set => win_O2 = value; }
        public string Condition { get => condition; set => condition = value; }
        public int Sequence { get => sequence; set => sequence = value; }
        public string ThemeVariant { get => themeVariant; set => themeVariant = value; }

        public static Game getDummyGame()
        {
            Game dummy = new Game();
            dummy.Name = "Dummy Test Game";
            dummy.condition = "testCondition";
            dummy.sequence = 2;
            dummy.Trials = 2;
            dummy.Saldo = 1500;
            dummy.Bet_R1 = -25;
            dummy.Bet_R2 = -45;
            dummy.Prob_O1 = 0.2;
            dummy.Prob_O2 = 0.2;
            dummy.Win_O1 = 500;
            dummy.Win_O2 = 1000;
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

        /// <summary>
        /// Assert if current game did win.
        /// </summary>
        /// <returns>True for win, false for lose</returns>
        public bool didWin()
        {
            Random winRnd = new Random();
            var accumulator = 100 / (int)(getWinningChance() * 100);

            var res = winRnd.Next(0, accumulator);

            return res == 1 ? true : false;
        }

        /// <summary>
        /// Assert if current game did win. Parameter is used through CardDraw since Game object uses two probability properties and card game uses two cards.
        /// </summary>
        /// <param name="winChance">Decimal value for winning chance</param>
        /// <returns>True for win, false for lose</returns>
        public bool didWin(double winChance)
        {
            Random winRnd = new Random();
            var accumulator = 100 / (int)(winChance * 100);

            var res = winRnd.Next(0, accumulator);

            return res == 1 ? true : false;
        }

        /// <summary>
        /// Evaluates Game objects two probability properties.
        /// </summary>
        /// <returns>Returns one of the probability properties.</returns>
        public double getWinningChance()
        {
            double prob;
            
            if (this.Prob_O1 == this.Prob_O2)
            {
                prob = this.Prob_O2;
            }
            else
            {
                var rnd = new Random();

                if (rnd.Next(1, 2) == 1)
                {
                    prob = this.Prob_O1;
                }
                else
                {
                    prob = this.Prob_O2;
                }
            }
            return prob;
        }
    }
}