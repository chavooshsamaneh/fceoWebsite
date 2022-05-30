using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebsiteStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  Load_Statistics();
        }
    }

    //private void Load_Statistics()
    //{
    //    lblWebsiteStatistics.Text = "<table width='100%'>";
    //    lblWebsiteStatistics.Text += "<tr><td width='60%'><strong>کاربران مهمان:</strong></td><td width='40%'>" + Utility.GetGuestVisitors() + "</td></tr>";
    //    lblWebsiteStatistics.Text += "<tr><td><strong>کاربران عضو:</strong></td><td>" + Utility.GetOnlineUsers() + "</td></tr>";
    //    lblWebsiteStatistics.Text += "<tr><td><strong>بازدیدکنندگان امروز:</strong></td><td>" + Utility.GetDailyVisitor() + "</td></tr>";
    //    lblWebsiteStatistics.Text += "<tr><td><strong>بازدیدکنندگان هفتگی:</strong></td><td>" + Utility.GetWeeklyVisitor() + "</td></tr>";
    //    lblWebsiteStatistics.Text += "<tr><td><strong>بازدیدکنندگان ماهانه:</strong></td><td>" + Utility.GetMonthlyVisitor() + "</td></tr></table>";

      
    //}
}