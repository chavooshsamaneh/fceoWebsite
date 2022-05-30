using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class PrintNews : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["News"]))
        {
            Response.Redirect("News.aspx");
            return;
        }

        int NewsId = -1;
        try
        {
            NewsId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["News"]).ToString());
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        Load_News(NewsId);
    }

    void Load_News(int NewsId)
    {
        TSP.DataManager.NewsManager NewsManager = new TSP.DataManager.NewsManager();
        NewsManager.FindByCode(NewsId);
        if (NewsManager.Count > 0)
        {
            lblNewsDetail.Text = "تاریخ: <span dir=ltr>" + NewsManager[0]["Date"] + "</span>  " + NewsManager[0]["StrTime"] + "<br><br>";
            lblNewsDetail.Text += "گروه: " + NewsManager[0]["Name"] + "<br><br>";
            lblNewsDetail.Text += "عنوان: <b>" + NewsManager[0]["Title"] + "</b>";

            this.Title = NewsManager[0]["Title"].ToString();

            lblNews.Text = "<div align=justify>" + NewsManager[0]["Body"] + "</div>";
        }
    }
}
