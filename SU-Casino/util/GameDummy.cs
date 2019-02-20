using SU_Casino.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SU_Casino.util
{
    public class GameDummy
    {
        public static Game getDummyGame(GameName gameName)
        {
            switch (gameName)
            {
                case GameName.Transfer_test:
                    return Get_Transfer_test();
                case GameName.Pavlovian_extinct:
                    return Get_Pavlovian_extinct();
                default:
                    return Get_Roulette();
            }
        }

        private static Game Get_Transfer_test() {

            Game dummy = new Game();
            dummy.UserId = "dummy_test";
            dummy.Name = "Transfer_test";
            dummy.Condition = "one.one";
            dummy.Sequence = 1;
            dummy.Trials = 10;
            dummy.Saldo = 1000;
            dummy.Bet_R1 = -10; // blått kort. eller rött. Definerar bettinstats på de kortet eller knappen
            dummy.Bet_R2 = -20;  // rött kort. eller blått Definerar bettinstats på de kortet eller knappen.
            dummy.Bet_R3 = -30;
            dummy.Bet_R4 = -40;
            dummy.Prob_O1 = 0;
            dummy.Prob_O2 = 0;
            dummy.Win_O1 = 0;
            dummy.Win_O2 = 0;
            dummy.ThemeVariant = "A";
            dummy.Perc_S1 = 0.25;
            dummy.Perc_S2 = 0.25;
            dummy.Perc_S3 = 0.25;
            dummy.Perc_S4 = 0.25;
            dummy.IfS1win = "";
            dummy.IfS2win = "";
            dummy.IfS3win = "";
            dummy.IfS4win = "";
            dummy.IfS1probX = 0;
            dummy.IfS2probX = 0;
            dummy.If_R1 = "O1";
            dummy.If_R2 = "O2";
            dummy.If_R3 = "";
            dummy.If_R4 = "";            
            return dummy;
        }

        private static Game Get_Pavlovian_extinct()
        {
            Game dummy = new Game();
            dummy.UserId = "dummy_test";
            dummy.Name = "Pavlovian_extinct";
            dummy.Condition = "one.one";
            dummy.Sequence = 1;
            dummy.Trials = 10;
            dummy.Saldo = 1000;
            dummy.Bet_R1 = -10; // blått kort. eller rött. Definerar bettinstats på de kortet eller knappen
            dummy.Bet_R2 = 0;  // rött kort. eller blått Definerar bettinstats på de kortet eller knappen.
            dummy.Bet_R3 = 0;
            dummy.Bet_R4 = 0;
            dummy.Prob_O1 = 0.99;
            dummy.Prob_O2 = 0.99;
            dummy.Win_O1 = 5;
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
            dummy.If_R1 = "";
            dummy.If_R2 = "";
            dummy.If_R3 = "";
            dummy.If_R4 = "";
            return dummy;
        }

        private static Game Get_Roulette()
        {
            Game dummy = new Game();
            dummy.UserId = "dummy_test";
            dummy.Name = "Roulette";
            dummy.Condition = "one.one";
            dummy.Sequence = 1;
            dummy.Trials = 10;
            dummy.Saldo = 1000;
            dummy.Bet_R1 = -20; // svart knapp, blått kort. eller rött. Definerar bettinstats på de kortet eller knappen
            dummy.Bet_R2 = -10;  // räd knapp, rött kort. eller blått Definerar bettinstats på de kortet eller knappen.
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
    }
}