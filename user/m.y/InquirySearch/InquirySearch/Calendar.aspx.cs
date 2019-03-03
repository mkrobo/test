using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InquirySearch
{
    public partial class Calender : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                //GETパラメータの日付を初期表示
                DateTime dateParam;
                DateTime.TryParse(Request.QueryString["date"], out dateParam);
                Calendar1.VisibleDate = dateParam;
                Calendar1.SelectedDate = dateParam;
            }

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            //選択された日付を保持
            string selectDate = Calendar1.SelectedDate.ToString("d");

            //変更する子フォームのIDを取得
            string id = Request.QueryString["text1Id"];

            //親フォームの要素を変更するスクリプトを作成
            string script;
            script = "if(!window.opener || window.opener.closed) {" +
                     "    window.close();" +
                     "} else { " +
                     "    window.opener.document.getElementById('" + id + "').value = '" + selectDate + "';" +
                     "    window.close();" +
                     "}";


   

            ClientScript.RegisterClientScriptBlock(this.GetType(), "calendar", script, true);
        }
    }
}