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
    public class GameLogicTests
    {
        const string theme_one = "1";
        const string theme_two = "2";
        const string theme_three = "3";

        [TestMethod()]
        public void getRandomConditionFromConditionsTest()
        {
            List<string> conditions = new List<string>();
            conditions.Add("first");
            conditions.Add("second");
            conditions.Add("third");
            conditions.Add("fouth");
            conditions.Add("fifth");

            List<string> result = new List<string>();
            Random rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                string item = GameLogic.getRandomConditionFromConditions(rand, conditions);
                result.Add(item);
                Assert.IsTrue(conditions.Contains(item));
            }

            foreach (string condition in conditions)
            {
                Assert.IsTrue(result.Contains(condition));
            }
        }

        [TestMethod()]
        public void CalculateCurrentThemeBasedOnPercColumnsTest()
        {
            testNoThemeIsPicked();

            test100percentCase();

            testThatMultipleThemesGetPicked();

            testThatThepercentageIsCorrect();
        }

        private static void test100percentCase()
        {
            Dictionary<string, double> themesToTest = new Dictionary<string, double>();

            themesToTest.Add(theme_one, 0);
            themesToTest.Add(theme_two, 1);
            Assert.AreEqual(theme_two, GameLogic.CalculateCurrentThemeBasedOnPercent(themesToTest));
        }

        private static void testNoThemeIsPicked()
        {
            Dictionary<string, double> themesToTest = new Dictionary<string, double>();
            themesToTest.Add(theme_one, 0);
            Assert.ThrowsException<NotSupportedException>(() => GameLogic.CalculateCurrentThemeBasedOnPercent(themesToTest));
        }

        private static void testThatMultipleThemesGetPicked()
        {
            List<string> result;
            Random rand;
            Dictionary<string, double> themesToTest = new Dictionary<string, double>();
            themesToTest.Add(theme_one, 0.50);
            themesToTest.Add(theme_two, 0.50);
            result = new List<string>();
            rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                string theme = GameLogic.CalculateCurrentThemeBasedOnPercent(themesToTest, rand);
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

        private static void testThatThepercentageIsCorrect()
        {

            Dictionary<string, double> themesToTest = new Dictionary<string, double>();
            themesToTest.Add(theme_one, 0.33);
            themesToTest.Add(theme_two, 0.33);
            themesToTest.Add(theme_three, 0.34);

            List<string> result = new List<string>();
            Random rand = new Random();
            const int iterations = 10000;
            for (int i = 0; i < iterations; i++)
            {
               result.Add(GameLogic.CalculateCurrentThemeBasedOnPercent(themesToTest, rand));
             }

      
            Assert.AreEqual(0.33, (double)result.Where(i => i.Equals(theme_one)).Count() / (double)iterations, 0.03);
            Assert.AreEqual(0.33, (double)result.Where(i => i.Equals(theme_two)).Count() / (double)iterations,  0.03);
            Assert.AreEqual(0.34, (double)result.Where(i => i.Equals(theme_three)).Count() / (double)iterations,  0.03);

        }
    }
}