﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Members_Accounting_EpaymentFisheView : System.Web.UI.Page
{
    #region Properties
    private int AccountingId
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

    private string PageMode
    {
        set
        {
            HiddenFieldEpayment["PageMode"] = value;
        }
        get
        {
            return HiddenFieldEpayment["PageMode"].ToString();
        }
    }

    private string PrePgMd
    {
        set
        {
            HiddenFieldEpayment["PrePgMd"] = value;
        }
        get
        {
            return HiddenFieldEpayment["PrePgMd"].ToString();
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            SetKey();
            SetMode();
            this.ViewState["btnPayment"] = btnPayment.Enabled;
        }
        if (this.ViewState["btnPayment"] != null)
            this.btnPayment.Enabled = this.btnPayment1.Enabled = (bool)this.ViewState["btnPayment"];
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EpaymentFishes.aspx?P=" + PrePgMd);
    }

    protected void btnPayment_Click(object sender, EventArgs e)
    {
        if (AccountingId == -1)
        {
            ShowMessage("مقادیر صفحه معتبر نمی باشد");
            return;
        }
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.FindByAccountingId(AccountingId);
        if (AccountingManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        if (Convert.ToInt32(AccountingManager[0]["Status"]) != (int)TSP.DataManager.TSAccountingStatus.Payment)
            btnPayment.Enabled = btnPayment1.Enabled = true;
        else
            btnPayment.Enabled = btnPayment1.Enabled = false;
        string AmountPayment = Convert.ToInt32(AccountingManager[0]["Amount"]).ToString();
        string CustomerIdPayment = Utility.GetCurrentUser_UserId().ToString(); // AccountingManager[0]["FollowNumber"].ToString();
        string InvoiceNumber = AccountingManager[0]["AccountingId"].ToString();
        string SpecialPaymentId = AccountingManager[0]["FollowNumber"].ToString();
        string Description = AccountingManager[0]["Description"].ToString();  
        int AccType = Convert.ToInt32(AccountingManager[0]["AccType"]);
        int TableId = Convert.ToInt32(AccountingManager[0]["TableTypeId"]);
        //string BankURL = TSP.Utility.OnlinePayment.FindBankURL(AmountPayment, InvoiceNumber, TSP.DataManager.EpaymentType.MemberPayment
        //    , (TSP.DataManager.TSAccountingAccType)AccType, TableId);
        string Token = "";
        string BankURL = TSP.Utility.OnlinePayment.FindBankURL(AmountPayment, InvoiceNumber, SpecialPaymentId, CustomerIdPayment, TSP.DataManager.EpaymentType.MemberPayment
            , (TSP.DataManager.TSAccountingAccType)AccType, TableId, "", ref Token);
        Response.Redirect(BankURL);
    }

    #region Methods
    private void SetKey()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["QS"]))
        {
            string Qs = Utility.DecryptQS(Request.QueryString["QS"]);
            string[] ArrayQS = Qs.Split(';');
            PageMode = ArrayQS[0];
            EPaymentUC.AccType = int.Parse(ArrayQS[1].ToString());
            EPaymentUC.TableId = int.Parse(ArrayQS[2].ToString());
        }
        else
        {
            if (string.IsNullOrEmpty(Request.QueryString["AId"]) || string.IsNullOrEmpty(Request.QueryString["PgMd"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);
            AccountingId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["AId"]));
            PrePgMd = Request.QueryString["PrePgMd"];
        }
    }

    private void SetMode()
    {
        switch (PageMode)
        {
            case "View":
                SetViewMode();
                break;
            case "BankReply":
                SetBankReplyMode();
                break;
        }
    }

    private void SetViewMode()
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.FindByAccountingId(AccountingId);
        if (AccountingManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        if (!Utility.IsDBNullOrNullValue(AccountingManager[0]["Status"]) && Convert.ToInt32(AccountingManager[0]["Status"]) != (int)TSP.DataManager.TSAccountingStatus.Payment)
            btnPayment.Enabled = btnPayment1.Enabled = true;
        else
            btnPayment.Enabled = btnPayment1.Enabled = false;
        EPaymentUC.PaymentId = Convert.ToInt32(AccountingManager[0]["AccountingId"]);
        int AccType = Convert.ToInt32(AccountingManager[0]["AccType"]);
        int TableType = Convert.ToInt32(AccountingManager[0]["TableType"]);
        EPaymentUC.SetEPaymentParameters(AccType
                                        , TableType
                                        , PageMode, Request.Form["paymentId"] != null ? Convert.ToInt32(Request.Form["paymentId"]) : -1
                                        , Request.Form["resultCode"] != null ? Request.Form["resultCode"] : "-1"
                                        , Request.Form["referenceId"] != null ? Request.Form["referenceId"] : "-1", Request.Form["token"] != null ? Request.Form["token"] : "");
    }

    private void SetBankReplyMode()
    {
         //   AccountingId = Request.Form["paymentId"] != null ? Convert.ToInt32(Request.Form["paymentId"]) : -1;
        EPaymentUC.SetEPaymentParameters(EPaymentUC.AccType
                                    , TSP.DataManager.TechnicalServices.AccountingManager.FindPaymentTableTypeByAccType(EPaymentUC.AccType)
                                    , PageMode, Request.Form["paymentId"] != null ? Convert.ToInt32(Request.Form["paymentId"]) : -1
                                    , Request.Form["resultCode"] != null ? Request.Form["resultCode"] : "-1"
                                    , Request.Form["referenceId"] != null ? Request.Form["referenceId"] : "-1", Request.Form["token"] != null ? Request.Form["token"] : "");
        AccountingId = EPaymentUC.PaymentId;
        if (!EPaymentUC.DoNextTaskOfBankReply(Utility.GetCurrentUser_UserId(), Utility.GetCurrentUser_LoginType(),EPaymentUC.Token))
        {
            btnPayment.Enabled = true;
            btnPayment1.Enabled = true;
            return;
        }
        btnPayment.Enabled = false;
        btnPayment1.Enabled = false;
    }

    protected void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    #endregion
}