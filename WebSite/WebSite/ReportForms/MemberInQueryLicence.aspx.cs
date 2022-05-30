using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_MemberInQueryLicence : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string MeId, MlId, IsMeTemp;
        MeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString()));
        MlId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MlId"].ToString()));
        IsMeTemp = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["IsMeTemp"].ToString()));
        if ((!Utility.IsDBNullOrNullValue(MeId)) && (!Utility.IsDBNullOrNullValue(MlId)))
        {
            TSP.WebsiteReports.Members.InQueryMemberLicence MeR = new TSP.WebsiteReports.Members.InQueryMemberLicence(Convert.ToInt32(MeId), Convert.ToInt32(MlId), Utility.GetLicenceInqueryRequestAsignerId(), Convert.ToInt32(IsMeTemp));
            RptVMembersLicence.Report = MeR;
        }
    }

    protected void CallbackPanelReport_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        try
        {
            string MeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString()));
            TSP.DataManager.PrintingHistoryManager PrintingHistoryManager = new TSP.DataManager.PrintingHistoryManager();
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(Convert.ToInt32(MeId));
            System.Data.DataRow dr = PrintingHistoryManager.NewRow();
            dr["TableId"] = MeId;
            dr["TableType"] = TSP.DataManager.TableType.MemberLicenceInqueryPrint;
            dr["Description"] = "چاپ درخواست استعلام مدرک تحصیلی";
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