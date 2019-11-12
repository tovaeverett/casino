using SU_Casino.model;
using SU_Casino.service;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SU_Casino.game
{
    public class GameSession
    {
        private IDataService dataService;
        private SurveyCodeService surveyCodeService;

        public Game GameToPlay { get; set; }
        public SurveyCode SurveyCode { get; set; }

        public GameSession() : this(new DBDataService(), new SurveyCodeService())
        {
        }

        public GameSession(IDataService dataService, SurveyCodeService surveyCodeService)
        {
            this.dataService = dataService;
            this.surveyCodeService = surveyCodeService;

            this.SurveyCode = GetNewSurveyCode();
        }

        public string GetText(AllTextType allTextType)
        {
            return dataService.GetText(allTextType);
        }

        public void SaveQuestions(List<string> answers, string userid, string regionalInfo)
        {
            dataService.SaveQuestions(answers, userid, regionalInfo);
        }

        public void GetInitialBetingelse()
        {
            //String condition = GetRandomConditionFromConditions(dataService.GetCondition());
            string condition = GetCondition();
            GameToPlay = dataService.GetOrderToPlay(1, condition);
        }

        public string GetRandomConditionFromConditions(List<string> conditions)
        {
            return conditions[RandomSingleton.Next(0, conditions.Count())];
        }

        public string GetCondition()
        {

            IList<ConditionRate> ConditionRates = GetConditionRates();
            string condition = "";
            double random = RandomSingleton.NextDouble();
            double baseValue = 0.0;

            foreach (ConditionRate conditionRate in ConditionRates)
            {

                baseValue += conditionRate.prop;
                if (baseValue >= random)
                {
                    condition = conditionRate.name;
                    break;
                }
            }

            return condition;
        }

        public IList<ConditionRate> GetConditionRates()
        {
            IList<ConditionRate> ConditionRates = new List<ConditionRate>();
            DataRow[] matris = dataService.GetMatrisByProp();
            if (matris == null)
                return ConditionRates;


            foreach (DataRow dataRow in matris)
            {
                ConditionRate conditionRate = new ConditionRate();
                conditionRate.name = dataRow["condition"].ToString();
                conditionRate.prop = double.Parse(dataRow["prop_n"].ToString());
                ConditionRates.Add(conditionRate);
            }

            return ConditionRates;
        }

        public string GetGameUUrl()
        {
            // no more game to play
            if (GameToPlay == null)
                return "EndPage.aspx";

            switch (GameToPlay.Name)
            {
                case "DET_control":
                    return "CardDraw.aspx?workerid=" + GameToPlay.UserId;
                case "DET_experimental":
                    return "CardDraw.aspx?workerid=" + GameToPlay.UserId;
                case "DET_realworld": // is this still in use? doesn´t exist in metrics table
                    return "CardDraw.aspx?workerid=" + GameToPlay.UserId;
                case "Instrumental_acq":
                    return "CardDraw.aspx?workerid=" + GameToPlay.UserId;
                case "Instrumental_acq2":
                    return "CardDraw2.aspx?workerid=" + GameToPlay.UserId;
                case "Pavlovian_acq":
                    return "OneArmdBandit.aspx?workerid=" + GameToPlay.UserId;
                case "Pavlovian_extinct":
                    return "OneArmdBandit.aspx?workerid=" + GameToPlay.UserId;
                case "Roulette":
                    return "Roulette.aspx?workerid=" + GameToPlay.UserId;
                case "Transfer_test":
                    return "CardDraw.aspx?workerid=" + GameToPlay.UserId;
                default:
                    return null;
            }
        }

        public Game LoadNextGame()
        {
            int curentUserBalance = GameToPlay.Saldo;
            string userId = GameToPlay.UserId;
            GameToPlay = dataService.GetOrderToPlay(GameToPlay.Sequence + 1, GameToPlay.Condition);
            if (GameToPlay != null)
            {
                GameToPlay.Saldo = curentUserBalance;
                GameToPlay.UserId = userId;
            }
            return GameToPlay;
            /*
            else
            {
                //No game found, this was the last game, go to end page in that case                
                return "EndPage.aspx";
            }
            */
        }

        public void UpdatePlayerLog(Playerlog log)
        {
            dataService.UpdatePlayerLog(log);
        }

        private SurveyCode GetNewSurveyCode()
        {
            return surveyCodeService.GetNewSurveyCode();
        }
    }
}