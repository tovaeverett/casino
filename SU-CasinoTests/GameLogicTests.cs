using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace SU_Casino.Tests
{
    [TestClass()]
    public class GameLogicTests
    {
        const string no_theme = "0";
        const string theme_one = "1";
        const string theme_two = "2";
        const string theme_three = "3";
        const string theme_four = "4";

        //[TestMethod()]
        //public void getRandomConditionFromConditionsTest()
        //{
        //    GameLogic gameLogic = new GameLogic();
        //    List<string> conditions = new List<string>();
        //    conditions.Add("first");
        //    conditions.Add("second");
        //    conditions.Add("third");
        //    conditions.Add("fouth");
        //    conditions.Add("fifth");

        //    List<string> result = new List<string>();
        //    for (int i = 0; i < 100; i++)
        //    {
        //        string item = gameLogic.getRandomConditionFromConditions(conditions);
        //        result.Add(item);
        //        Assert.IsTrue(conditions.Contains(item));
        //    }

        //    foreach (string condition in conditions)
        //    {
        //        Assert.IsTrue(result.Contains(condition));
        //    }
        //}


        [TestMethod()]
        public void Test100percentCase()
        {
            Dictionary<string, double> themesToTest = new Dictionary<string, double>();
            GameLogic gameLogic = new GameLogic();

            themesToTest.Add(theme_one, 0);
            themesToTest.Add(theme_two, 1);
            Assert.AreEqual(theme_two, gameLogic.CalculateCurrentThemeBasedOnPercent(themesToTest));
        }

        [TestMethod()]
        public void TestNoThemeIsPicked()
        {
            GameLogic gameLogic = new GameLogic();
            Dictionary<string, double> themesToTest = new Dictionary<string, double>();
            themesToTest.Add(theme_one, 0);
            string theme = gameLogic.CalculateCurrentThemeBasedOnPercent(themesToTest);
            Assert.AreEqual(no_theme, theme);
        }
        [TestMethod()]
        public void TestThatMultipleThemesGetPicked()
        {
            List<string> result;
            GameLogic gameLogic = new GameLogic();
            Dictionary<string, double> themesToTest = new Dictionary<string, double>();
            themesToTest.Add(theme_one, 0.50);
            themesToTest.Add(theme_two, 0.50);
            result = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                string theme = gameLogic.CalculateCurrentThemeBasedOnPercent(themesToTest);
                if (!result.Contains(theme))
                {
                    result.Add(theme);
                }
                if (themesToTest.Count == result.Count)
                { break; }
            }
            Assert.IsTrue(result.Contains(theme_one));
            Assert.IsTrue(result.Contains(theme_two));
        }

        [TestMethod()]
        public void TestThatThePercentageIsCorrect()
        {
            GameLogic gameLogic = new GameLogic();
            Dictionary<string, double> themesToTest = new Dictionary<string, double>();
            themesToTest.Add(theme_one, 0.25);
            themesToTest.Add(theme_two, 0.25);
            themesToTest.Add(theme_three, 0.25);
            themesToTest.Add(theme_four, 0.25);

            Dictionary<string, double> themes = new Dictionary<string, double>();



            List<string> result = new List<string>();
            const int iterations = 10000;
            for (int i = 0; i < iterations; i++)
            {
                result.Add(gameLogic.CalculateCurrentThemeBasedOnPercent(themesToTest));
            }


            Assert.AreEqual(0.33, (double)result.Where(i => i.Equals(theme_one)).Count() / (double)iterations, 0.03);
            Assert.AreEqual(0.33, (double)result.Where(i => i.Equals(theme_two)).Count() / (double)iterations, 0.03);
            Assert.AreEqual(0.34, (double)result.Where(i => i.Equals(theme_three)).Count() / (double)iterations, 0.03);

        }
    }
}