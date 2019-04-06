using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Text;
using System.Windows;




namespace Empy030CSV
{
    public partial class TestCSVOutput : System.Web.UI.Page
    {
        //String Connectingstring = "userid = root; password = 3S1e8t104@4; database=workmandb1; Host = localhost";
        string connectionstring = string.Format("Host = localhost; database = workmandb1; UID = root; password = 3S1e8t104@4; SslMode = none");
       
        /*****/
        string Dbcode = "Host = localhost; database = workmandb1; UID = root; password = 3S1e8t104@4; SslMode = none";
        string Sqlcode = "select * from member";
        /********/
        protected void Page_Load(object sender, EventArgs e)
        {
            DataGridView1.Columns.Clear();
            DataGridView1.DataSource = null;
            if (IsPostBack == true) return;
        }

        
        DataTable m_dt;
        //DataSet ds;
        public void Mysqlconnnect()
        {
            try
            {
                
           
            MySqlConnection Con = new MySqlConnection(connectionstring);

            Con.Open();
            DataTable Dt = new DataTable();
                

            MySqlDataAdapter Da = new MySqlDataAdapter("select * from member", Con);

            Da.Fill(Dt);

            DataGridView1.DataSource = Dt;
            DataGridView1.DataBind();
            //ds.Tables.Add(Dt);
            m_dt = Dt;
            Con.Close();
                Label2.Text = "表示に成功しました";
            }
            catch(MySqlException)
            {
                Label2.Text = "エラーが起きました";
            }
        }
        /****************/
        static DataSet MakeDataTables(string sq, string cc)
        {
           
            string mySql = sq;
            string connectionstring = string.Format(cc);
            MySqlConnection Con = new MySqlConnection(connectionstring);
            MySqlDataAdapter da = new MySqlDataAdapter(mySql, Con);

            DataSet Ds = new DataSet();
            Ds.Clear();
            da.Fill(Ds);
            
            return Ds;
        }

        /*************CSVに変換する（前）*****************************/
        public void ConvertDAtaToCSV0(DataTable dt, string csvPath, bool writeHeader)
        {
            Encoding enc = Encoding.GetEncoding("Shift_jis");

            StreamWriter SW = new StreamWriter(csvPath, false, enc);

           // dt.CaseSensitive = true;

            int colCount = dt.Columns.Count;
            int lastColIndex = colCount - 1;

            if (writeHeader)
            {
                for (int i = 0; i < colCount; i++)
                {
                    //ヘッダの取得
                    string field = dt.Columns[i].Caption;
                    //"で囲む
                    field = EncloseDoubleQuotesIfNeed(field);
                    //フィールドを書き込む
                    SW.Write(field);
                    //カンマを書き込む
                    if (lastColIndex > i)
                    {
                        SW.Write(',');
                    }
                }
                //改行する
                SW.Write("\r\n");
            }

            //レコードを書き込む
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < colCount; i++)
                {
                    //フィールドの取得
                    string field = row[i].ToString();
                    //"で囲む
                    field = EncloseDoubleQuotesIfNeed(field);
                    //フィールドを書き込む
                    SW.Write(field);
                    //カンマを書き込む
                    if (lastColIndex > i)
                    {
                        SW.Write(',');
                    }
                }
                //改行する
                SW.Write("\r\n");
            }

            //閉じる
            SW.Close();
        }

        private string EncloseDoubleQuotesIfNeed(string field)
        {
            if (NeedEncloseDoubleQuotes(field))
            {
                return EncloseDoubleQuotes(field);
            }
            return field;
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む
        /// </summary>
        private string EncloseDoubleQuotes(string field)
        {
            if (field.IndexOf('"') > -1)
            {
                //"を""とする
                field = field.Replace("\"", "\"\"");
            }
            return "\"" + field + "\"";
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む必要があるか調べる
        /// </summary>
        private bool NeedEncloseDoubleQuotes(string field)
        {
            return field.IndexOf('"') > -1 ||
                field.IndexOf(',') > -1 ||
                field.IndexOf('\r') > -1 ||
                field.IndexOf('\n') > -1 ||
                field.StartsWith(" ") ||
                field.StartsWith("\t") ||
                field.EndsWith(" ") ||
                field.EndsWith("\t");
        }
        
        protected void CSVOutput_Click(object sender, EventArgs e)
        {
            DataSet ds = MakeDataTables(Sqlcode, Dbcode);

            DataTable DT = ds.Tables[0];
            DataGridView1.DataSource = DT;
            DataGridView1.DataBind();
            ConvertDAtaToCSV0(DT, @"E:\testCSV\test22.csv", true);
        }



        /*************CSVに変換する(後)***************/



    }
}