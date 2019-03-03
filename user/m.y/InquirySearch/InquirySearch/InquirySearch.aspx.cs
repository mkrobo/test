using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Data;

namespace InquirySearch
{


    public partial class InquirySearch : System.Web.UI.Page
    {

        int item = 0;

        protected void Page_Load(object sender, EventArgs e)
        {


            //今日の日付を取得する
            if (!IsPostBack)
            {
                
                var now = System.DateTime.Now;
                System.Diagnostics.Debug.WriteLine(now);
                string today = now.ToString("yyyyMMdd");
                int minDate = int.Parse(today);
                
                
                //Console.WriteLine(today);
                Label1.Text = today;
                
            }

            /*
            //HTML要素のIDを取得
            string text1Id = TextBox1.ClientID;
            string opt = "height=350,width=350,dependent=yes,location=no,menubar=no," +
                         "scrolbars=no,toolbar=no,status=no";

            //ポップアップカレンダーを開くスクリプトを作成
            string script = "var url = 'Calendar.aspx?text1Id={0}&date=' + getElementById('{0}').value;" +
                            "window.open(url,'_blank', '{1}'); return true;";
            script = String.Format(script, text1Id, opt);
            
            

            // onclick 属性にスクリプトを設定する
            Button1.Attributes.Add("onclick", script);
            */

        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.Calendar1.Visible = true;
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            //今日の日付を取得
            var now = System.DateTime.Now;
            System.Diagnostics.Debug.WriteLine(now);
            string sToday = now.ToString("yyyyMMdd");
            int minDate = int.Parse(sToday);

            MySqlConnection con = new MySqlConnection();

            con.ConnectionString = "uid = root; Password = Androborobo3908; database = inquiry_info; Host = localhost";

            //サーバに接続する
            con.Open();

            DataTable dt = new DataTable();
            MySqlDataAdapter dataAdapter;

            if(CheckBox1.Checked == true && CheckBox2.Checked == true)
            {

            }else if(CheckBox1.Checked == true && CheckBox2.Checked == false){

                //期日(終了)を取得
                string sTerm = TextBox1.Text;
                System.Diagnostics.Debug.WriteLine(sTerm);

                //期日(終了)をint型に変換
                int maxDate = int.Parse(sTerm);

                dataAdapter = new MySqlDataAdapter("select * from info where period_day between " + minDate + " and " + maxDate + " ;", con);
                dataAdapter.Fill(dt);

            }
            else if(CheckBox1.Checked == false && CheckBox2.Checked == true){
                if(item == 0)
                {
                    searchError.Visible = true;
                    searchError.Text = "1つ以上チェックしてください";
                }
                else
                {
                    searchError.Visible = false;
                    dataAdapter = new MySqlDataAdapter("select * from info where period_day >=" + minDate + " and item=" + item + " ;", con);
                    dataAdapter.Fill(dt);
                }

            }else{

            }

            //dataAdapter.Fill(dt);

            GridView1.DataSource = dt;

            GridView1.DataBind();

            con.Close();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            DateTime today = System.DateTime.Now.Date;

            if (Calendar1.SelectedDate < today)
            {
                calendarError.Visible = true;
                calendarError.Text = today.ToString("yyyyMMdd") + "より後の日付を選択してください";
            }
            else
            {
                calendarError.Visible = false;
                TextBox1.Text = Calendar1.SelectedDate.ToString("yyyyMMdd");

                Calendar1.Visible = false;
            }

        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox2.Checked == true)
            {
                RadioButtonList1.Enabled = true;
            }
            else
            {
                RadioButtonList1.Enabled = false;
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itemSelected = 0;
            for(int i = 0; i < RadioButtonList1.Items.Count; i++)
            {
                if(RadioButtonList1.Items[itemSelected].Selected == true)
                {
                    item = itemSelected + 1;
                }
                itemSelected++;
            }
        }
    }
}