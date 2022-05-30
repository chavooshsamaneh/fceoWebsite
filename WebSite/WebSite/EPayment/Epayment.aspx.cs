using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EPayment_Epayment : System.Web.UI.Page
{

    private int _AccountingId
    {
        set
        {
            HiddenFieldEpayment["AccountingId"] = value;
        }
        get
        {
            return int.Parse(HiddenFieldEpayment["AccountingId"].ToString());
        }
    }
    /// <summary>
    /// InvoiceNo
    /// </summary>
    private int _AccDetailId
    {
        set
        {
            HiddenFieldEpayment["AccDetailId"] = value;
        }
        get
        {
            return int.Parse(HiddenFieldEpayment["AccDetailId"].ToString());
        }
    }

    private int _TMeId
    {
        set
        {
            HiddenFieldEpayment["TMeId"] = value;
        }
        get
        {
            return int.Parse(HiddenFieldEpayment["TMeId"].ToString());
        }
    }

    private int _PeriodRegisterId
    {
        set
        {
            HiddenFieldEpayment["PeriodRegisterId"] = value;
        }
        get
        {
            return int.Parse(HiddenFieldEpayment["PeriodRegisterId"].ToString());
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            if (!SetData()) return;
            SetEpaymentSetting();
        }
    }

    private Boolean SetData()
    {

        if (string.IsNullOrEmpty(Request.QueryString["InvoiceNo"]))
        {
            ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است.");
            return false;
        }
        _AccDetailId = int.Parse(Utility.DecryptQS(Request.QueryString["InvoiceNo"]));
        if (!string.IsNullOrEmpty(Request.QueryString["TMeId"]) && !Utility.IsDBNullOrNullValue(Utility.DecryptQS(Request.QueryString["TMeId"])))
        {
            _TMeId = int.Parse(Utility.DecryptQS(Request.QueryString["TMeId"]));
        }
        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
        AccountingDetailManager.FindById(_AccDetailId);
        if (AccountingDetailManager.Count != 1)
        {
            ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است.");
            return false;
        }
        EPaymentUC.AccountingId = _AccountingId = Convert.ToInt32(AccountingDetailManager[0]["AccountingId"]);
        EPaymentUC.AccType = Convert.ToInt32(AccountingDetailManager[0]["AccType"]);
        EPaymentUC.TableId = Convert.ToInt32(AccountingDetailManager[0]["TableTypeId"]);
        EPaymentUC.InvoiceNumber = _AccDetailId;
        if (Convert.ToInt32(AccountingDetailManager[0]["AccType"]) == (int)TSP.DataManager.TSAccountingAccType.PeriodRegister
            || Convert.ToInt32(AccountingDetailManager[0]["AccType"]) == (int)TSP.DataManager.TSAccountingAccType.SeminarRegister)
        {
            _PeriodRegisterId = Convert.ToInt32(AccountingDetailManager[0]["TableId"]);
        }
        if (EPaymentUC.AccType > 0)
        {
            EPaymentUC.SetEPaymentParameters(EPaymentUC.AccType
                                        , EPaymentUC.TableId
                                          , "View", _AccountingId
                                          , "-1"
                                          , "-1", "");
        }
        return true;
    }
    private void SetEpaymentSetting()
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.FindByAccountingId(_AccountingId);
        if (AccountingManager.Count != 1)
        {
            btnPayment.Visible = false;
            ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است.");
            return;
        }
        string AmountPayment = Convert.ToInt32(AccountingManager[0]["Amount"]).ToString();
        string InvoiceNumber = _AccDetailId.ToString();// AccountingManager[0]["AccountingId"].ToString();
        string SpecialPaymentId = AccountingManager[0]["FollowNumber"].ToString();
        int AccType = Convert.ToInt32(AccountingManager[0]["AccType"]);
        int TableId = Convert.ToInt32(AccountingManager[0]["TableTypeId"]);
        string Description = AccountingManager[0]["Description"].ToString();
        int FishPayerId = -2;
        if (!Utility.IsDBNullOrNullValue(AccountingManager[0]["FishPayerId"]))
         FishPayerId = Convert.ToInt32(AccountingManager[0]["FishPayerId"]);
        string PaymentId = Utility.GetCurrentUser_UserId().ToString();
        
        switch (AccType)
        {
            case (int)TSP.DataManager.TSAccountingAccType.Registeration:
            case (int)TSP.DataManager.TSAccountingAccType.Registeration_Entrance:
            case (int)TSP.DataManager.TSAccountingAccType.Entrance:
                //????????????????????????????????????Check it please
                PaymentId = TSP.Utility.OnlinePayment.GetPaymentId((TSP.DataManager.TSAccountingAccType)AccType, _TMeId.ToString(), 1);
                break;
            case (int)TSP.DataManager.TSAccountingAccType.PeriodRegister:
            case (int)TSP.DataManager.TSAccountingAccType.SeminarRegister:
                #region GetPaymentId for Seminar&Periods
                //بدست آوردن مقادیر (PaymentId)برای ارسال به پانک
                TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
                PeriodRegisterManager.FindByCode(_PeriodRegisterId);
                if (PeriodRegisterManager.Count != 1)
                    return;
                int RegisterPaymentId = 0;
                if (!Utility.IsDBNullOrNullValue(PeriodRegisterManager[0]["RegisterPaymentId"]))
                    RegisterPaymentId = Convert.ToInt32(PeriodRegisterManager[0]["RegisterPaymentId"]);
                else
                {
                    PeriodRegisterManager.DataTable.Clear();
                    System.Data.DataTable dt = PeriodRegisterManager.GetRegisterPaymentIdAndUpdatePeriodRegister(_PeriodRegisterId);
                    if (dt.Rows.Count != 1)
                        return;
                    RegisterPaymentId = Convert.ToInt32(PeriodRegisterManager[0]["RegisterPaymentId"]);
                }
                if (RegisterPaymentId == 0)
                    return;
                string TafziliCode = TSP.DataManager.PeriodRegisterManager.GetPeriodREgisterTafziliCode(Utility.GetCurrentUser_MeId(), RegisterPaymentId);
                PaymentId = TSP.Utility.OnlinePayment.GetPaymentId((TSP.DataManager.TSAccountingAccType)AccType, TafziliCode, 0);
                #endregion
                break;
            case (int)TSP.DataManager.TSAccountingAccType.MemberDebpt:
                //**FishPayerId==MeId==TafziliCode
                PaymentId = TSP.Utility.OnlinePayment.GetPaymentId((TSP.DataManager.TSAccountingAccType)AccType, FishPayerId.ToString(), 1);
                break;
                
        }
        
        //if (AccType == (int)TSP.DataManager.TSAccountingAccType.SeminarRegister
        //    || AccType == (int)TSP.DataManager.TSAccountingAccType.PeriodRegister)
        //{
        //    int MeId = Utility.GetCurrentUser_MeId();
        //    //int PPId=
        //}
        string RevertURLPayment = TSP.Utility.OnlinePayment.GetRevertURL(TSP.DataManager.EpaymentType.EpaymentForAllSite, (TSP.DataManager.TSAccountingAccType)AccType, TableId);
        if (TSP.Utility.OnlinePayment.SetepaymentAmountForTest())
        {
            AmountPayment = "1000";
            //RevertURLPayment = "http://localhost:2813/Epayment/EpaymentVerify.aspx";
            //EPaymentUC.merchantId = "AA6E";
        }
        if (Utility.ShowExceptionError())
        {
            lblError.Visible = true;
            lblError.Text += "Step0: AmountPayment:" + AmountPayment + "merchantId:" + EPaymentUC.merchantId + "InvoiceNumber:" + InvoiceNumber
                + "PaymentId:" + PaymentId + "SpecialPaymentId:" + SpecialPaymentId + "RevertURLPayment:" + RevertURLPayment + "Desc" + "سازمان نظام مهندسی ساختمان استان فارس";
        }
        TSP.Utilities.EpaymentToken.TokensClient TokensClient = new TSP.Utilities.EpaymentToken.TokensClient();
        TSP.Utilities.EpaymentToken.tokenResponse tokenResponse = TokensClient.MakeToken(AmountPayment, EPaymentUC.merchantId, InvoiceNumber, PaymentId, SpecialPaymentId, RevertURLPayment, "سازمان نظام مهندسی ساختمان استان فارس");
        token.Value = tokenResponse.token;

        if (Utility.ShowExceptionError())
        {
            lblError.Visible = true;
            lblError.Text += "**Step1: " + token.Value;
        }
        merchantId.Value = EPaymentUC.merchantId;
        if (Utility.ShowExceptionError())
        {
            lblError.Visible = true;
            lblError.Text += "**Step2: " + EPaymentUC.merchantId;
        }
        this.formEPayment.Action = TSP.Utility.OnlinePayment.GetOnlinePaymentWebSiteAddress();
        this.formEPayment.Method = "post";
        merchantId.Value = EPaymentUC.merchantId;
        if (Utility.ShowExceptionError())
        {
            lblError.Visible = true;
            lblError.Text += "**StepEnd**";
        }

    }
    protected void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    protected void btnPayment_Click(object sender, EventArgs e)
    {
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
}