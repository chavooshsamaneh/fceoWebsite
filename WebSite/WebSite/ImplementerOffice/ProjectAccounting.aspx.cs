using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ImplementerOffice_ProjectAccounting : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            int PrjReId = -2;
            int ProjectId = -2;
            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            ProjectRequestManager.SelectRequestLastVersion(ProjectId, -1, -1);
            if (ProjectRequestManager.Count > 0)
                PrjReId = Convert.ToInt32(ProjectRequestManager[0]["PrjReId"]);
            prjInfo.Fill(PrjReId);
            HiddenPage["MsgOwner"] = "اینجانب " + prjInfo.OwnerName + "  مالک ملک با اطلاعات نمایش داده شده رضایت خود را جهت واریز صد در صد حق الزحمه نظارت به مبلغ نمایش داده شده در ردیف انتخاب شده به حساب ناظرین پس از صدور پروانه ساختمانی و یا حداکثر بمدت سه ماه پس از واریز وجه اعلام می دارم.";

            ObjectDataSourceTsAcc.SelectParameters["Type"].DefaultValue = ((int)TSP.DataManager.AccountingPaymentType.EPayment).ToString();
            ObjectDataSourceTsAcc.SelectParameters["ProjectId"].DefaultValue = ProjectId.ToString();
            ObjectDataSourceTsAcc.SelectParameters["ProjectIngridientTypeId"].DefaultValue = "-1";
            ObjectDataSourceTsAcc.SelectParameters["AccTypeList"].DefaultValue = (((int)TSP.DataManager.TSAccountingAccType._2In1000).ToString() + "," +
            ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation).ToString() + "," +
            ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure).ToString());



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
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["InActive"] = 0;
            dr["ModifedDate"] = DateTime.Now;
            AccountingDetailManager.AddRow(dr);
            AccountingDetailManager.Save();
            AccountingDetailManager.DataTable.AcceptChanges();
            switch (Convert.ToInt32(row["AccType"]))
            {

                case (int)TSP.DataManager.TSAccountingAccType._2In1000:

                    Response.Redirect("~/EPayment/EpaymentParsian.aspx?InvoiceNo=" + Utility.EncryptQS(AccountingDetailManager[AccountingDetailManager.Count - 1]["AccDetailId"].ToString()) + "&Cit=" + Utility.EncryptQS(CitId.ToString()), false);
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