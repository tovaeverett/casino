using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SU_Casino
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateReport_Click(object sender, EventArgs e)
        {
                
        }

        public void getData()
        {
            string NewconnectionString = "myCoonectionString";
            StreamWriter CsvfileWriter = new StreamWriter(@"D:\testfile.csv");
            string sqlselectQuery = "select * from Mytable";
            SqlCommand sqlcmd = new SqlCommand();

            SqlConnection spContentConn = new SqlConnection(NewconnectionString);
            sqlcmd.Connection = spContentConn;
            sqlcmd.CommandTimeout = 0;
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = sqlselectQuery;
            spContentConn.Open();
            using (spContentConn)
            {
                using (SqlDataReader sdr = sqlcmd.ExecuteReader())
                using (CsvfileWriter)
                {
                    //This Block of code for getting the Table Headers
                    DataTable Tablecolumns = new DataTable();

                    for (int i = 0; i < sdr.FieldCount; i++)
                    {
                        Tablecolumns.Columns.Add(sdr.GetName(i));
                    }
                  //  CsvfileWriter.WriteLine(string.Join(",", Tablecolumns.Columns.Cast<datacolumn>().Select(csvfile => csvfile.ColumnName)));
                    //This block of code for getting the Table Headers

                    while (sdr.Read())
                    {

                    }
                        //based on your Table columns you can increase and decrese columns
                      //  YourWriter.WriteLine(sdr[0].ToString() + "," + sdr[1].ToString() + "," + sdr[2].ToString() + "," + sdr[3].ToString() + "," + sdr[4].ToString() + "," + sdr[5].ToString() + "," + sdr[6].ToString() + "," + sdr[7].ToString() + "," + sdr[8].ToString() + "," + sdr[9].ToString() + "," + sdr[10].ToString() + "," + sdr[11].ToString() + ",");

                }
            }
        }
    }
}