using SU_Casino.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SU_Casino.service
{
    public class SurveyCodeService
    {
        public SurveyCode GetNewSurveyCode() {
            Random random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            int randomNumber = random.Next(1, Enum.GetNames(typeof(SurveyCode)).Length);
            System.Diagnostics.Debug.WriteLine(randomNumber);

            return (SurveyCode)randomNumber;
        }
    }
}