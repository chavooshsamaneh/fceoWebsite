using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportForms_Accounting_BankFish : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string AccountingId, ProjectId = "-2", PrjReId = "-2", PlnTypeId = "-2";
        AccountingId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AccountingId"].ToString()));
        if (Request.QueryString["ProjectId"] != null)
            ProjectId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["ProjectId"].ToString()));
        if (Request.QueryString["PrjReId"] != null)
            PrjReId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PrjReId"].ToString()));
        if (Request.QueryString["PlnTypeId"] != null)
            PlnTypeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PlnTypeId"].ToString()));

        if (!String.IsNullOrEmpty(AccountingId))
        {
            TSP.WebsiteReports.Accounting.BankFish fish = new TSP.WebsiteReports.Accounting.BankFish(Convert.ToInt32(AccountingId), Convert.ToInt32(ProjectId), Convert.ToInt32(PrjReId), Convert.ToInt32(PlnTypeId));
            RptVMembers.Report = fish;
        }
    }

    protected void CallbackPanelReport_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string AccountingId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["AccountingId"].ToString()));
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PrintingHistoryManager PrintingHistoryManager = new TSP.DataManager.PrintingHistoryManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        trans.Add(PrintingHistoryManager);
        trans.Add(AccountingManager);
        try
        {
            trans.BeginSave();
            AccountingManager.FindByAccountingId(int.Parse(AccountingId));
            if (Convert.ToInt32(AccountingManager[0]["Status"]) != (int)TSP.DataManager.TSAccountingStatus.Payment)
            {
                AccountingManager[0].BeginEdit();
                AccountingManager[0]["Status"] = (int)TSP.DataManager.TSAccountingStatus.Print;
                AccountingManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                AccountingManager[0]["ModifiedDate"] = DateTime.Now;
                AccountingManager[0].EndEdit();
                AccountingManager.Save();
            }
            System.Data.DataRow dr = PrintingHistoryManager.NewRow();
            dr["TableId"] = AccountingId;
            dr["TableType"] = TSP.DataManager.TableType.AccountingPrintedBankFish;
            dr["Description"] = AccountingManager[0]["AccTypeName"].ToString();
            dr["CreateDate"] = TSP.DataManager.Utility.GetDateOfToday();
            dr["CreateTime"] = TSP.DataManager.Utility.GetCurrentTime();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            PrintingHistoryManager.AddRow(dr);
            PrintingHistoryManager.Save();
            trans.EndSave();
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            ShowCallBackMessage("خطایی در ذخیره گزارش فیش انجام گرفته است");
        }
    }

    void ShowCallBackMessage(string Msg)
    {
        CallbackPanelReport.JSProperties["cpMsg"] = Msg;
        CallbackPanelReport.JSProperties["cpError"] = 1;
    }
}