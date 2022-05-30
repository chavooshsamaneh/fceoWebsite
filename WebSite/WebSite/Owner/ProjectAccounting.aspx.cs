using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Owner_ProjectAccounting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            int PrjReId = -2;

            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            ProjectRequestManager.SelectRequestLastVersion(Utility.GetCurrentUser_MeId(), -1, -1);
            if (ProjectRequestManager.Count > 0)
                PrjReId = Convert.ToInt32(ProjectRequestManager[0]["PrjReId"]);
            prjInfo.Fill(PrjReId);
            HiddenPage["MsgOwner"] = "اینجانب "+ prjInfo.OwnerName+ "  مالک ملک با اطلاعات نمایش داده شده رضایت خود را جهت واریز صد در صد حق الزحمه نظارت به مبلغ نمایش داده شده در ردیف انتخاب شده به حساب ناظرین پس از صدور پروانه ساختمانی و یا حداکثر بمدت سه ماه پس از واریز وجه اعلام می دارم.";

            ObjectDataSourceTsAcc.SelectParameters["Type"].DefaultValue = ((int)TSP.DataManager.AccountingPaymentType.EPayment).ToString();
            ObjectDataSourceTsAcc.SelectParameters["ProjectId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
            ObjectDataSourceTsAcc.SelectParameters["ProjectIngridientTypeId"].DefaultValue = "-1";
            ObjectDataSourceTsAcc.SelectParameters["AccTypeList"].DefaultValue = (  ( (int)TSP.DataManager.TSAccountingAccType.ObserversFiche).ToString()+","+
            ( (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation).ToString() + ","+
            ( (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure).ToString() );


            
        }
    }

    protected void btnPayment_Click(object sender, EventArgs e)
    {

        string BankURL = "";
        try
        {

            if (GridViewProjectAccounting.FocusedRowIndex <= -1)
            {
                ShowMessage("لطفاً برای پرداخت الکترونیکی اطلاعات ابتدا یک فیش را انتخاب نمائید");
                return;
            }
            System.Data.DataRow row = GridViewProjectAccounting.GetDataRow(GridViewProjectAccounting.FocusedRowIndex);

            string Error = TSP.Utility.OnlinePayment.CheckUserCanPayOnline(Convert.ToInt32(row["AccType"]), Convert.ToInt32(row["AccountingId"]), Convert.ToInt32(row["FishPayerId"]));
            if (!string.IsNullOrEmpty(Error))
            {
                ShowMessage(Error);
                return;
            }
            int _AccountingId = Convert.ToInt32(row["AccountingId"]);
            int TableId = Convert.ToInt32(row["TableTypeId"]);
            int CitId = !Utility.IsDBNullOrNullValue(row["CitId"]) ? Convert.ToInt32(row["CitId"]) : -2;
            TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
            AccountingDetailManager.FindByAccountingId(_AccountingId);
            if (AccountingDetailManager.Count > 0)
            {
                TableId = Convert.ToInt32(AccountingDetailManager[0]["TableId"]);
            }
            System.Data.DataRow dr = AccountingDetailManager.NewRow();
            dr["AccountingId"] = _AccountingId;
            dr["TableId"] = TableId;
            dr["TableType"] = Convert.ToInt32(row["TableType"]);
            dr["Amount"] = row["Amount"];
            dr["Description"] = row["Description"];
            dr["UserId"] =Utility.GetCurrentUser_UserId();
            dr["InActive"] = 0;
            dr["ModifedDate"] = DateTime.Now;
            AccountingDetailManager.AddRow(dr);
            AccountingDetailManager.Save();
            AccountingDetailManager.DataTable.AcceptChanges();
            switch (Convert.ToInt32(row["AccType"]))
            {

                case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:
                case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:

                    Response.Redirect("~/EPayment/EpaymentParsian.aspx?InvoiceNo=" + Utility.EncryptQS(AccountingDetailManager[AccountingDetailManager.Count - 1]["AccDetailId"].ToString()) + "&Cit=" + Utility.EncryptQS(CitId.ToString()), false);
                    break;
                default:
                    Response.Redirect("~/EPayment/Epayment.aspx?InvoiceNo=" + Utility.EncryptQS(AccountingDetailManager[AccountingDetailManager.Count - 1]["AccDetailId"].ToString()) + "&TMeId=" + Utility.EncryptQS(row["TMeId"].ToString()), false);
                    break;
            }

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }


    protected void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
}