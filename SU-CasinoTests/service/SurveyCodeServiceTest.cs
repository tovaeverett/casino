using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SU_Casino.service;

namespace SU_CasinoTests.service
{
    [TestClass]
    public class SurveyCodeServiceTest
    {
        [TestMethod]
        public void TestGetNewSurveyCode()
        {
            SurveyCodeService surveyCodeService = new SurveyCodeService();
            
            for (int x = 1; x <= 20; x++) {                
                System.Diagnostics.Debug.WriteLine(surveyCodeService.GetNewSurveyCode());
            }

        }
}
}
