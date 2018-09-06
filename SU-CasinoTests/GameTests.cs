using Microsoft.VisualStudio.TestTools.UnitTesting;
using SU_Casino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU_Casino.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void getWinningChanceTest()
        {

            Game testgame = createTestGame();
            testgame.CurrentTheme = "1";
            testgame.Prob_O1 = 0.6;
            testgame.Prob_O2 = 0.4;
            testgame.IfS1win = "O1";
            testgame.IfS1probX = 1;
            Assert.AreEqual(0.6, testgame.getWinningChanceOneArmedBandit(), 0.001);
            testgame.IfS1win = "O2";
            Assert.AreEqual(0.4, testgame.getWinningChanceOneArmedBandit(), 0.001);
            testgame.IfS1probX = 0;
            Assert.AreEqual(0, testgame.getWinningChanceOneArmedBandit(), 0.001);

            testgame.CurrentTheme = "2";
            testgame.IfS2win = "O1";
            testgame.IfS2probX = 1;
            Assert.AreEqual(0.6, testgame.getWinningChanceOneArmedBandit(), 0.001);
            testgame.IfS2win = "O2";
            Assert.AreEqual(0.4, testgame.getWinningChanceOneArmedBandit(), 0.001);
            testgame.IfS2probX = 0;
            Assert.AreEqual(0, testgame.getWinningChanceOneArmedBandit(), 0.001);

            testgame.CurrentTheme = "3";
            testgame.IfS3win = "O1";
            Assert.AreEqual(0.6, testgame.getWinningChanceOneArmedBandit(), 0.001);
            testgame.IfS3win = "O2";
            Assert.AreEqual(0.4, testgame.getWinningChanceOneArmedBandit(), 0.001);


            testgame.CurrentTheme = "4";
            testgame.IfS4win = "O1";
            Assert.AreEqual(0.6, testgame.getWinningChanceOneArmedBandit(), 0.001);
            testgame.IfS4win = "O2";
            Assert.AreEqual(0.4, testgame.getWinningChanceOneArmedBandit(), 0.001);


        }

        private Game createTestGame()
        {
            Game game = new Game
            {
                Name = "Pavlovian_extinct",
                Condition = "Pavlovian_extinct",
                Sequence = 1,
                Trials = 24,
                Saldo = 1000,
                Bet_R1 = -10, // blått kort. eller rött. Definerar bettinstats på de kortet eller knappen
                Bet_R2 = 0,  // rött kort. eller blått Definerar bettinstats på de kortet eller knappen.
                Bet_R3 = 0,
                Bet_R4 = 0,
                Prob_O1 = 0.5,
                Prob_O2 = 0.5,
                Win_O1 = 50,
                Win_O2 = 20,
                Perc_S1 = 0.25,
                Perc_S2 = 0.25,
                Perc_S3 = 0.25,
                Perc_S4 = 0.25,
                IfS1win = "O1",
                IfS2win = "O1",
                IfS3win = "O2",
                IfS4win = "O2",
                IfS1probX = 1,
                IfS2probX = 0
            };

            return game;

        }

        [TestMethod()]
        public void getWinningChanceCardDrawTest()
        {
            Game testgame = new Game();
            testgame.CurrentTheme = "0";
            testgame.If_R1 = "O1";
            testgame.If_R2 = "O2";
            testgame.Prob_O1 = 0.2;
            testgame.Prob_O2 = 0.8;


            Assert.AreEqual(0.2, testgame.getWinningChanceCardDraw("R1"), 0.001);
            testgame.If_R1 = "O2";
            Assert.AreEqual(0.8, testgame.getWinningChanceCardDraw("R1"), 0.001);

            Assert.AreEqual(0.8, testgame.getWinningChanceCardDraw("R2"), 0.001);
            testgame.If_R2 = "O1";
            Assert.AreEqual(0.2, testgame.getWinningChanceCardDraw("R2"), 0.001);

            testgame.If_R3 = "O2";
            Assert.AreEqual(0.8, testgame.getWinningChanceCardDraw("R3"), 0.001);
            testgame.If_R3 = "O1";
            Assert.AreEqual(0.2, testgame.getWinningChanceCardDraw("R3"), 0.001);


        }

        [TestMethod()]
        public void CalculateWinOrNotTest()
        {
            Random rand = new Random();
            Game game = new Game();
            Assert.IsTrue(game.CalculateWinOrNot(1, rand));
            Assert.IsFalse(game.CalculateWinOrNot(0, rand));

            List<bool> result = new List<bool>();

            const int iterations = 10000;
            for (int i = 0; i < iterations; i++)
            {
                result.Add(game.CalculateWinOrNot(0.5, rand));
            }

            Assert.AreEqual(0.5, (double)result.Where(i => i == true).Count() / (double)iterations, 0.03);
            Assert.AreEqual(0.5, (double)result.Where(i => i == false).Count() / (double)iterations, 0.03);
            
        }
    }
}