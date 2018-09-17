using SU_Casino.model;
using SU_Casino.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SU_Casino.game
{
    public class GamesSssion
    {
        private IDataService dataService;
        public Game gameToPlay { get; set; }

        public GamesSssion() : this(new DBDataService()) { 
        }

        public GamesSssion(IDataService dataService) {
            this.dataService = dataService;
        }

        public String GetText(InfoTextType infoTextType) {
            return dataService.GetText(infoTextType);
        }

        public void SaveQuestions(List<String> answers, String userid, String regionalInfo)
        {
            dataService.SaveQuestions(answers, userid, regionalInfo);            
        }

        public void GetInitialBetingelse()
        {
            String condition = GetRandomConditionFromConditions(dataService.GetCondition());
            gameToPlay = dataService.GetOrderToPlay(1, condition);
        }

        public String GetRandomConditionFromConditions(List<String> conditions)
        {
            return conditions[RandomSingleton.Next(0, conditions.Count())];
        }

        public String GetGameUUrl()
        {
            // no more game to play
            if (gameToPlay == null)
                return "EndPage.aspx";

            switch (gameToPlay.Name)
            {
                case "DET_control":
                    return "CardDraw.aspx?workerid=" + gameToPlay.UserId;
                case "DET_experimental":
                    return "CardDraw.aspx?workerid=" + gameToPlay.UserId;
                case "DET_realworld": // is this still in use? doesn´t exist in metrics table
                    return "CardDraw.aspx?workerid=" + gameToPlay.UserId;
                case "Instrumental_acq":
                    return "CardDraw.aspx?workerid=" + gameToPlay.UserId;
                case "Instrumental_acq2":
                    return "CardDraw2.aspx?workerid=" + gameToPlay.UserId;
                case "Pavlovian_acq":
                    return "OneArmdBandit.aspx?workerid=" + gameToPlay.UserId;
                case "Pavlovian_extinct":
                    return "OneArmdBandit.aspx?workerid=" + gameToPlay.UserId;
                case "Roulette":
                    return "Roulette.aspx?workerid=" + gameToPlay.UserId;
                case "Transfer_test":
                    return "CardDraw.aspx?workerid=" + gameToPlay.UserId;
                default:
                    return null;
            }
        }

        public Game LoadNextGame()
        {
            int curentUserBalance = gameToPlay.Saldo;
            String userId = gameToPlay.UserId;
            gameToPlay = dataService.GetOrderToPlay(gameToPlay.Sequence + 1, gameToPlay.Condition);
            if (gameToPlay != null) {
                gameToPlay.Saldo = curentUserBalance;
                gameToPlay.UserId = userId;
            }
            return gameToPlay;
            /*
            else
            {
                //No game found, this was the last game, go to end page in that case                
                return "EndPage.aspx";
            }
            */
        }

        public void UpdatePlayerLog(Playerlog log) {
            dataService.UpdatePlayerLog(log);
        }
    }
}