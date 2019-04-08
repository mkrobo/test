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
        /// <summary>
        /// Dbcodeで接続先の情報を記述、仕様によって、Hostやdatabase、UID(UserID)等の情報は変更してください
        /// Sqlcodeの部分はテーブル情報を指定し検索する部分なので、変更可能
        /// 
        /// 更新情報
        /// 2019/04/08 保存名を指定できるようにtextboxを設置、動作確認済み
        /// 2019/04/08 ２重に記述していたところを削除
        /// </summary>
        
        string Dbcode = "Host = localhost; database = workmandb1; UID = root; password = $X4dy@7h; SslMode = none";
        string Sqlcode = "select * from member";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            DataGridView1.Columns.Clear();
            DataGridView1.DataSource = null;
            if (IsPostBack == true) return;
        }

        /// <summary>
        /// CSV出力ボタンを押すことで、
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CSVOutput_Click(object sender, EventArgs e)
        {
            string NameValue = NameBox.Text;
            
            try
            {
                DataSet ds = MakeDataTables(Sqlcode, Dbcode);

                DataTable DT = ds.Tables[0];
                DataGridView1.DataSource = DT;
                DataGridView1.DataBind();
                ConvertDAtaToCSV0(DT, @"E:\testCSV\" + NameValue + ".csv", true);

                Csv_Message.Text = "変換処理に成功しました、確認してみてください";
            }catch(FormatException)
            {
                Csv_Message.Text = "処理に失敗しました、保存名を指定して下さい";
            }
        }
        
        /// <summary>
        /// テーブル情報を保管しておく
        /// </summary>
        /// <param name="sq"></param>
        /// <param name="cc"></param>
        /// <returns></returns>
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

            try
            {
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
                Grid_Message.Text = "処理に成功しました";
            }catch(FormatException)
            {
                Grid_Message.Text = "処理に失敗しました、接続情報を見直してください。";
            }
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
        
       
        /*************CSVに変換する(後)***************/



    }
}