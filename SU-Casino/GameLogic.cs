﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SU_Casino
{
    public class GameLogic
    {
        //private Database _database = new Database();

        //public void getInitialBetingelse()
        //{

        //    string condition = getRandomConditionFromConditions(_database.GetCondition());
        //    Game gameToPlay = _database.getOrderToPlay(1, condition);

        //    if (gameToPlay != null)
        //    {
        //        HttpContext.Current.Session.Add("currentGame", gameToPlay);
        //        //redirectToGame(gameToPlay.Name);
        //    }

        //}

        //public string getRandomConditionFromConditions(List<string> conditions)
        //{
        //    return conditions[RandomSingleton.Next(0, conditions.Count())];
        //}

        //public void getNextGame(Game currentGame, int curentUserBalance, string userid)
        //{
        //    int nextSeq = currentGame.Sequence;
        //    Game gameToPlay = _database.getOrderToPlay(nextSeq+1, currentGame.Condition);
        //    if(gameToPlay != null)
        //    {
        //        gameToPlay.Saldo = curentUserBalance;
        //        HttpContext.Current.Session.Add("currentGame", gameToPlay);
        //        redirectToGame(gameToPlay.Name,userid);
        //    }
        //    else 
        //    {
        //        //No game found, this was the last game, go to end page in that case
        //        HttpContext.Current.Response.Redirect("EndPage.aspx");
        //    }
        //}

        //public void redirectToGame(String gameName, string userid)
        //{
        //    switch (gameName)
        //    {
        //        case "DET_control":

        //            break;
        //        case "DET_experimental":
        //            HttpContext.Current.Response.Redirect("CardDraw.aspx?workerid="+userid);
        //            break;                 
        //        case "DET_realworld":
        //            HttpContext.Current.Response.Redirect("CardDraw.aspx?workerid=" + userid);
        //            break;
        //        case "Instrumental_acq":
        //            HttpContext.Current.Response.Redirect("CardDraw.aspx?workerid=" + userid);
        //            break;
        //        case "Instrumental_acq2":
        //            HttpContext.Current.Response.Redirect("CardDraw2.aspx?workerid=" + userid);
        //            break;
        //        case "Pavlovian_acq":
        //            HttpContext.Current.Response.Redirect("OneArmdBandit.aspx?workerid=" + userid);
        //            break;
        //        case "Pavlovian_extinct":
        //            HttpContext.Current.Response.Redirect("OneArmdBandit.aspx?workerid=" + userid);
        //            break;
        //        case "Roulette":
        //            HttpContext.Current.Response.Redirect("Roulette.aspx?workerid=" + userid);
        //            break;
        //        case "Transfer_test":
        //            HttpContext.Current.Response.Redirect("CardDraw.aspx?workerid=" + userid);
        //            break;
        //    }

        //}


        public string CalculateCurrentThemeBasedOnPercent(Dictionary<string, double> themeNumberAndPercentage)
        {
            if (themeNumberAndPercentage.Max(i => i.Value).Equals(0))
            {
                return "0";
            }

            IOrderedEnumerable<KeyValuePair<string, double>> enumerable =
                        themeNumberAndPercentage.OrderByDescending(i => i.Key);

            double random = RandomSingleton.NextDouble();
            double baseValue = 0.0;

            foreach (KeyValuePair<string, double> theme in enumerable)
            {
                if (baseValue + theme.Value >= random)
                {
                    return theme.Key;
                }
                baseValue += theme.Value;
            }

            throw new NotSupportedException("Unable to find theme, please make sure the percentages equals to 1.");
        }

    }

}