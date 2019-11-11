﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace SU_Casino.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void GetWinningChanceTest()
        {
            Game testgame = new Game();
            testgame.CurrentTheme = "1";
            testgame.Prob_O1 = 0.6;
            testgame.Prob_O2 = 0.4;
            testgame.IfS1win = "O1";
            testgame.IfS1probX = 1;
            Assert.AreEqual(0.6, testgame.GetWinningChanceOneArmedBandit(), 0.001);
            testgame.IfS1win = "O2";
            Assert.AreEqual(0.4, testgame.GetWinningChanceOneArmedBandit(), 0.001);
            testgame.IfS1probX = 0;
            Assert.AreEqual(0, testgame.GetWinningChanceOneArmedBandit(), 0.001);

            testgame.CurrentTheme = "2";
            testgame.IfS2win = "O1";
            testgame.IfS2probX = 1;
            Assert.AreEqual(0.6, testgame.GetWinningChanceOneArmedBandit(), 0.001);
            testgame.IfS2win = "O2";
            Assert.AreEqual(0.4, testgame.GetWinningChanceOneArmedBandit(), 0.001);
            testgame.IfS2probX = 0;
            Assert.AreEqual(0, testgame.GetWinningChanceOneArmedBandit(), 0.001);

            testgame.CurrentTheme = "3";
            testgame.IfS3win = "O1";
            Assert.AreEqual(0.6, testgame.GetWinningChanceOneArmedBandit(), 0.001);
            testgame.IfS3win = "O2";
            Assert.AreEqual(0.4, testgame.GetWinningChanceOneArmedBandit(), 0.001);


            testgame.CurrentTheme = "4";
            testgame.IfS4win = "O1";
            Assert.AreEqual(0.6, testgame.GetWinningChanceOneArmedBandit(), 0.001);
            testgame.IfS4win = "O2";
            Assert.AreEqual(0.4, testgame.GetWinningChanceOneArmedBandit(), 0.001);


        }


        [TestMethod()]
        public void GetWinningChanceCardDrawTest()
        {
            Game testgame = new Game();
            testgame.CurrentTheme = "0";
            testgame.If_R1 = "O1";
            testgame.If_R2 = "O2";
            testgame.Prob_O1 = 0.2;
            testgame.Prob_O2 = 0.8;

            Assert.AreEqual(0.2, testgame.GetWinningChanceCardDraw("R1"), 0.001);
            testgame.If_R1 = "O2";
            Assert.AreEqual(0.8, testgame.GetWinningChanceCardDraw("R1"), 0.001);

            Assert.AreEqual(0.8, testgame.GetWinningChanceCardDraw("R2"), 0.001);
            testgame.If_R2 = "O1";
            Assert.AreEqual(0.2, testgame.GetWinningChanceCardDraw("R2"), 0.001);

            testgame.If_R3 = "O2";
            Assert.AreEqual(0.8, testgame.GetWinningChanceCardDraw("R3"), 0.001);
            testgame.If_R3 = "O1";
            Assert.AreEqual(0.2, testgame.GetWinningChanceCardDraw("R3"), 0.001);


            testgame.CurrentTheme = "1";
            testgame.IfS1probX = 0;
            Assert.AreEqual(0, testgame.GetWinningChanceCardDraw("R1"), 0.001);


            testgame.CurrentTheme = "2";
            testgame.IfS2probX = 0;
            Assert.AreEqual(0, testgame.GetWinningChanceCardDraw("R1"), 0.001);

            testgame.CurrentTheme = "2";
            testgame.IfS2probX = 1;
            Assert.AreEqual(0.8, testgame.GetWinningChanceCardDraw("R1"), 0.001);


            testgame.CurrentTheme = "3";
            testgame.IfS1probX = 0;
            Assert.AreEqual(0.8, testgame.GetWinningChanceCardDraw("R1"), 0.001);

        }

        [TestMethod()]
        public void IsWinOrLoseBasedOnPercentTest()
        {
            Game game = new Game();
            Assert.IsTrue(game.IsWinOrLoseBasedOnPercent(1));
            Assert.IsFalse(game.IsWinOrLoseBasedOnPercent(0));

            List<bool> result = new List<bool>();

            const int iterations = 10000;
            for (int i = 0; i < iterations; i++)
            {
                result.Add(game.IsWinOrLoseBasedOnPercent(0.5));
            }

            Assert.AreEqual(0.5, (double)result.Where(i => i == true).Count() / (double)iterations, 0.03);
            Assert.AreEqual(0.5, (double)result.Where(i => i == false).Count() / (double)iterations, 0.03);

        }

        [TestMethod()]
        public void CalculateWinTest20Percent()
        {
            Game game = new Game();
            List<bool> result = new List<bool>();

            const int iterations = 10000;
            for (int i = 0; i < iterations; i++)
            {
                result.Add(game.IsWinOrLoseBasedOnPercent(0.2));
            }

            Assert.AreEqual(0.2, (double)result.Where(i => i == true).Count() / (double)iterations, 0.03);
            Assert.AreEqual(0.8, (double)result.Where(i => i == false).Count() / (double)iterations, 0.03);

        }
    }
}