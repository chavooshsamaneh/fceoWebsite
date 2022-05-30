using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_ReportMemberFilePrePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int MfId = -1;
        if (!Utility.IsDBNullOrNullValue(Server.HtmlDecode(Request.QueryString["MfId"])))
        {
            MfId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MfId"].ToString())));
        }
        string MeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString()));
        if (!String.IsNullOrEmpty(MeId))
        {
            if (MfId == -1)
            {
                TSP.WebsiteReports.Document.ReportDocPersonPrePrint ReportDocPersonPrePrint = new TSP.WebsiteReports.Document.ReportDocPersonPrePrint(Convert.ToInt32(MeId));
                RptMemberFile.Report = ReportDocPersonPrePrint;
            }else
            {

                TSP.WebsiteReports.Document.ReportDocPersonPrePrint ReportDocPersonPrePrint = new TSP.WebsiteReports.Document.ReportDocPersonPrePrint(MfId, Convert.ToInt32(MeId));
                RptMemberFile.Report = ReportDocPersonPrePrint;
            }
        }
    }

    protected void CallbackPanelReport_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        try
        {
            int MeId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString())));
            string MfId = "";
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            System.Data.DataTable dt = DocMemberFileManager.SelectForReportMemberFile(MeId, -1);
            if (dt.Rows.Count == 1)
                MfId = dt.Rows[0]["MfId"].ToString();
            if (Utility.IsDBNullOrNullValue(MfId))
            {
                ShowCallBackMessage("خطایی در ذخیره گزارش انجام گرفته است");
                return;
            }

            TSP.DataManager.PrintingHistoryManager PrintingHistoryManager = new TSP.DataManager.PrintingHistoryManager();
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(Convert.ToInt32(MeId));
            System.Data.DataRow dr = PrintingHistoryManager.NewRow();
            dr["TableId"] = MfId;
            dr["TableType"] = TSP.DataManager.TableType.MemberFile;
            dr["Description"] = "چاپ نسخه اولیه گواهینامه اشتغال به کار";
            dr["CreateDate"] = TSP.DataManager.Utility.GetDateOfToday();
            dr["CreateTime"] = TSP.DataManager.Utility.GetCurrentTime();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            PrintingHistoryManager.AddRow(dr);
            PrintingHistoryManager.Save();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowCallBackMessage("خطایی در ذخیره گزارش انجام گرفته است");
        }
    }

    void ShowCallBackMessage(string Msg)
    {
        CallbackPanelReport.JSProperties["cpMsg"] = Msg;
        CallbackPanelReport.JSProperties["cpError"] = 1;
    }
}