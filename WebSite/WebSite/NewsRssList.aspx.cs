using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class NewsRssList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TSP.DataManager.NewsSubjectManager NewsSubjectManager = new TSP.DataManager.NewsSubjectManager();
        NewsSubjectManager.FindByCode(-1);
        if (NewsSubjectManager.Count <= 0)
        {
            divNewsSubject.Visible = false;
            RoundPanelNewsSubject.Visible = false;

            return;
        }

        divNewsSubject.Visible = true;
        RoundPanelNewsSubject.Visible = true;

        StringBuilder sb = new StringBuilder();
        sb.Append("<table width='100%' class='NewsRSSSubject'>");
        foreach (DataRow row in NewsSubjectManager.DataTable.Rows)
        {
            string ImageUrl = "Images/DefaultNewsSubject.png";
            if (!Utility.IsDBNullOrNullValue(row["ImageUrl"]))
                ImageUrl = row["ImageUrl"].ToString().Replace("~/", "");

            sb.Append("<tr><td valign='top'><div class='DivRss'><img alt='' width='20px' height='20px' src='" +
                                ImageUrl + "' /><a href='NewsRss.aspx?SubjectId=" + Utility.EncryptQS(row["SubId"].ToString()) + "&Category=" + Utility.EncryptQS(row["Name"].ToString())
                                + "'>" + row["Name"].ToString() + "</a></div></td></tr><tr><td valign='top' style='word-wrap:break-word' class='tdRss'><div style='word-wrap:break-word' align='left'>" +Utility.GetWebSiteAddress()+ "/NewsRss.aspx?SubjectId=" + Utility.EncryptQS(row["SubId"].ToString()) + "&Category=" + Utility.EncryptQS(row["Name"].ToString())
                                + "</div></td></tr>");
        }

        sb.Append("</table>");
        divNewsSubject.InnerHtml = sb.ToString();
    }
}