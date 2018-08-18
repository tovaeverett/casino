using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SU_Casino
{
    public class GameLogic
    {
        static Database _database = new Database();
        public SqlConnection connectionstring = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

        public static void getInitialBetingelse()
        {
            Random letter = new Random();
            var Array = _database.GetCondition();

            int i = Array.Count();
            int num = letter.Next(0, i);
            string let = Array[num];

            Game gameToPlay = _database.getOrderToPlay(1, let);

            if (gameToPlay != null)
            {
                HttpContext.Current.Session.Add("currentGame", gameToPlay);
                //redirectToGame(gameToPlay.Name);
            }

        }
        
        public static void getNextGame(Game currentGame, int curentUserBalance, string userid)
        {
            int nextSeq = currentGame.Sequence;
            Game gameToPlay = _database.getOrderToPlay(nextSeq+1, currentGame.Condition);
            if(gameToPlay != null)
            {
                gameToPlay.Saldo = curentUserBalance;
                HttpContext.Current.Session.Add("currentGame", gameToPlay);
                redirectToGame(gameToPlay.Name,userid);
            }
            else 
            {
                //No game found, this was the last game, go to end page in that case
                HttpContext.Current.Response.Redirect("EndPage.aspx");
            }
        }

        public static void redirectToGame(String gameName, string userid)
        {
            switch (gameName)
            {
                case "DET_control":

                    break;
                case "DET_experimental":
                    HttpContext.Current.Response.Redirect("CardDraw.aspx?workerid="+userid);
                    break;                 
                case "DET_realworld":
                    HttpContext.Current.Response.Redirect("CardDraw.aspx?workerid=" + userid);
                    break;
                case "Instrumental_acq":
                    HttpContext.Current.Response.Redirect("CardDraw.aspx?workerid=" + userid);
                    break;
                case "Instrumental_acq2":
                    HttpContext.Current.Response.Redirect("CardDraw2.aspx?workerid=" + userid);
                    break;
                case "Pavlovian_acq":
                    HttpContext.Current.Response.Redirect("OneArmdBandit.aspx?workerid=" + userid);
                    break;
                case "Pavlovian_extinct":
                    HttpContext.Current.Response.Redirect("OneArmdBandit.aspx?workerid=" + userid);
                    break;
                case "Roulette":
                    HttpContext.Current.Response.Redirect("Roulette.aspx?workerid=" + userid);
                    break;
                case "Transfer_test":
                    HttpContext.Current.Response.Redirect("CardDraw.aspx?workerid=" + userid);
                    break;
            }


        }
    }

}