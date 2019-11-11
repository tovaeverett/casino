using SU_Casino.model;
using System;
using System.Collections.Generic;
using System.Data;

namespace SU_Casino.service
{
    public class TestDataService : IDataService
    {
        public DataTable GetMatrixTable()
        {
            throw new NotImplementedException();
        }

        public DataRow[] GetMatrisByProp()
        {
            throw new NotImplementedException();
        }

        public void DeleteMatris(string rowId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetCondition()
        {
            List<string> allContidiotns = new List<string> { "one.one", "two.one", "two.two" };
            return allContidiotns;
        }

        public Game GetOrderToPlay(int seq, string condition)
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

        public string GetText(AllTextType allTextType)
        {
            return allTextType.ToString();

        }

        public List<string> GetTexts()
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

        public void SaveQuestions(List<string> list, string userid, string regionalInfo)
        {
            // saved
        }

        public void SendFile(string strFilePath, string strFileName)
        {
            throw new NotImplementedException();
        }

        public void UpdateMatris(string rowId, string[] paramz)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayerLog(Playerlog log)
        {
            // update in db
        }

        public void UpdateText(string textName, string infotext)
        {
            throw new NotImplementedException();
        }
    }
}