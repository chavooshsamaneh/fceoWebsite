using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_MemberCardRequestReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string MeId;
            MeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString()));
            if (!(String.IsNullOrEmpty(MeId)))
            {
                TSP.WebsiteReports.Members.MemberCardRequestReport rpt = 
                    new TSP.WebsiteReports.Members.MemberCardRequestReport(Convert.ToInt32(MeId),Utility.GetCurrentUser_UserId());
                ReportViewer1.Report = rpt;
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
        }
        catch (Exception)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.GeneralErr).ToString());
        }
    }

    protected void CallbackPanelReport_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        try
        {
            string MeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString()));
            TSP.DataManager.PrintingHistoryManager PrintingHistoryManager = new TSP.DataManager.PrintingHistoryManager();
            System.Data.DataRow dr = PrintingHistoryManager.NewRow();
            dr["TableId"] = MeId;
            dr["TableType"] = TSP.DataManager.TableType.MemberCardRequestPrint;
            dr["Description"] = "چاپ درخواست کارت عضویت";
            dr["CreateDate"] = TSP.DataManager.Utility.GetDateOfToday();
            dr["CreateTime"] = TSP.DataManager.Utility.GetCurrentTime();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            PrintingHistoryManager.AddRow(dr);
            if (PrintingHistoryManager.Save() > 0)
            {
                PrintingHistoryManager.DataTable.AcceptChanges();
            }
        }
        catch(Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowCallBackMessage("خطایی در ذخیره گزارش درخواست های چاپ کارت عضویت انجام گرفته است");
        }
    }

    void ShowCallBackMessage(string Msg)
    {
        CallbackPanelReport.JSProperties["cpMsg"] = Msg;
        CallbackPanelReport.JSProperties["cpError"] = 1;
    }
}