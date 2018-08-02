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
        private int trials;
        private int saldo;
        private int bet_R1;
        private int bet_R2;
        private int win_O1;
        private int win_O2;

        public string Name { get => name; set => name = value; }
        public int Trials { get => trials; set => trials = value; }
        public int Saldo { get => saldo; set => saldo = value; }
        public int Bet_R1 { get => bet_R1; set => bet_R1 = value; }
        public int Bet_R2 { get => bet_R2; set => bet_R2 = value; }
        public int Win_O1 { get => win_O1; set => win_O1 = value; }
        public int Win_O2 { get => win_O2; set => win_O2 = value; }
        public string Condition { get => condition; set => condition = value; }

        public static Game getDummyGame()
        {
            Game dummy = new Game();
            dummy.Name = "Dummy Test Game";
            dummy.condition = "testCondition";
            dummy.Trials = 24;
            dummy.Saldo = 1500;
            dummy.Bet_R1 = -25;
            dummy.Bet_R2 = -45;
            dummy.Win_O1 = 500;
            dummy.Win_O2 = 1000;
            return dummy;

        }
    }
}