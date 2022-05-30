using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EPayment_EpaymentFishes : System.Web.UI.Page
{
    #region Perproperties
    string PageMode
    {
        get
        {
            return HiddenFieldEpayment["PgMode"].ToString();
        }
        set
        {
            HiddenFieldEpayment["PgMode"] = value;
        }

    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["P"]))
            {
                if (Request.QueryString["P"].ToString() == "N")
                {
                    PageMode = "NotPayed";
                        ObjdsEpayment.SelectParameters["Status"].DefaultValue = ((int)TSP.DataManager.TSAccountingStatus.SaveInDB).ToString();
                }
                else if (Request.QueryString["P"].ToString() == "NL")
                {
                    PageMode = "NotPayedLearning";
                }
                else
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
            }

            ObjdsEpayment.SelectParameters["Type"].DefaultValue = ((int)TSP.DataManager.TSAccountingPaymentType.EPayment).ToString();
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            {
                ObjdsEpayment.SelectParameters["TMeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
                ObjdsEpayment.SelectParameters["IsPayerTempMe"].DefaultValue = "true";
                ObjdsEpayment.FilterExpression = "AccType=" + ((int)TSP.DataManager.TSAccountingAccType.Entrance).ToString()
                    + " OR " + "AccType=" + ((int)TSP.DataManager.TSAccountingAccType.Registeration).ToString()
                    + " OR " + "AccType=" + ((int)TSP.DataManager.TSAccountingAccType.Registeration_Entrance).ToString();
            }
            else
            {
                ObjdsEpayment.SelectParameters["FishPayerId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
                ObjdsEpayment.SelectParameters["IsPayerTempMe"].DefaultValue = "false";
            }
        }

    }
  
    protected void btnPayment_Click(object sender, EventArgs e)
    {
        string BankURL = "";
        try
        {

            if (GridViewAccounting.FocusedRowIndex <= -1)
            {
                ShowMessage("لطفاً برای پرداخت الکترونیکی اطلاعات ابتدا یک فیش را انتخاب نمائید");
                return;
            }
            DataRow row = GridViewAccounting.GetDataRow(GridViewAccounting.FocusedRowIndex);

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
            DataRow dr = AccountingDetailManager.NewRow();
            dr["AccountingId"] = _AccountingId;
            dr["TableId"] = TableId;
            dr["TableType"] = Convert.ToInt32(row["TableType"]);
            dr["Amount"] = row["Amount"];
            dr["Description"] = row["Description"];
            dr["UserId"] = row["UserId"];
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
                case (int)TSP.DataManager.TSAccountingAccType.DocMemberFile:
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
    #endregion

    #region Method
    protected void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }



    #endregion
}