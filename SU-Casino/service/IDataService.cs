using SU_Casino.model;
using System.Collections.Generic;
using System.Data;

namespace SU_Casino.service
{
    public interface IDataService
    {
        void Log(EventLog log);

        void UpdatePlayerLog(Playerlog log);

        string GetText(AllTextType allTextType);

        List<string> GetTexts();

        void UpdateText(string textName, string infotext);

        List<string> GetCondition();

        Game GetOrderToPlay(int seq, string condition);

        //void GetExample(SqlCommand command, params SqlParameter[] p);

        void DeleteMatris(string rowId);

        void InsertMatris();

        //(DataSet ds, DataRowCollection rows) GetMatris();
        DataTable GetMatrixTable();

        DataRow[] GetMatrisByProp();

        void UpdateMatris(string rowId, string[] paramz);

        void GetReport();

        void SendFile(string strFilePath, string strFileName);

        void GetQuestionReports();

        void SaveQuestions(List<string> list, string userid, string regionalInfo);

        void ResetMatris();
    }
}
