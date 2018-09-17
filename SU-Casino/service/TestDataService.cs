using SU_Casino.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SU_Casino.service
{
    public class TestDataService : IDataService
    {
        public (DataSet ds, DataRowCollection rows) GetMatris() {
            throw new NotImplementedException();
        }

        public void DeleteMatris(String rowId)
        {
            throw new NotImplementedException();
        }

        public List<String> GetCondition()
        {            
            List<String> allContidiotns = new List<String> { "one.one", "two.one", "two.two" };
            return allContidiotns;
        }

        public Game GetOrderToPlay(int seq, String condition)
        {
            Game game = new Game();
            game.Saldo = 1000;
            game.Name = "Roulette";
            return game;
        }

        public void GetQuestionReports()
        {
            throw new NotImplementedException();
        }

        public void GetReport()
        {
            throw new NotImplementedException();
        }

        public String GetText(InfoTextType infoTextType)
        {
            return infoTextType.ToString();
            
        }

        public List<String> GetTexts()
        {
            throw new NotImplementedException();
        }

        public void InsertMatris()
        {
            throw new NotImplementedException();
        }

        public void Log(EventLog log)
        {
            throw new NotImplementedException();
        }

        public void ResetMatris()
        {
            throw new NotImplementedException();
        }

        public void SaveQuestions(List<String> list, String userid, String regionalInfo)
        {
            // saved
        }

        public void SendFile(String strFilePath, String strFileName)
        {
            throw new NotImplementedException();
        }

        public void UpdateMatris(String rowId, String[] paramz)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayerLog(Playerlog log)
        {
            // update in db
        }

        public void UpdateText(String textName, String infotext)
        {
            throw new NotImplementedException();
        }
    }
}