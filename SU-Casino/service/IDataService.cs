using SU_Casino.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU_Casino.service
{
    public interface IDataService
    {
        void Log(EventLog log);

        void UpdatePlayerLog(Playerlog log);

        String GetText(InfoTextType infoTextType);

        List<String> GetTexts();

        void UpdateText(String textName, String infotext);

        List<String> GetCondition();

        Game GetOrderToPlay(int seq, String condition);

        //void GetExample(SqlCommand command, params SqlParameter[] p);

        void DeleteMatris(String rowId);

        void InsertMatris();

        (DataSet ds, DataRowCollection rows) GetMatris();

        DataRow[] GetMatrisByProp();

        void UpdateMatris(String rowId, String[] paramz);

        void GetReport();

        void SendFile(String strFilePath, String strFileName);

        void GetQuestionReports();

        void SaveQuestions(List<String> list, String userid, String regionalInfo);

        void ResetMatris();
    }
}
