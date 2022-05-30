using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EPayment_EpaymentVerify : System.Web.UI.Page
{
    #region Properties
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

    private string MultiplePaymentType
    {
        set
        {
            HiddenFieldEpayment["MultiplePaymentType"] = value;
        }
        get
        {
            return HiddenFieldEpayment["MultiplePaymentType"].ToString();
        }
    }

    private int SumMutipleCost
    {
        set
        {
            HiddenFieldEpayment["SumMutipleCost"] = value;
        }
        get
        {
            return Convert.ToInt32(HiddenFieldEpayment["SumMutipleCost"]);
        }
    }

    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Visible = false;
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            SetKey();
            //  SetMode();
        }
        if (Utility.ShowExceptionError())
        {
            lblError.Visible = true;
            lblError.Text += "--End";
        }
    }
    #endregion

    #region Methods
    private void SetKey()
    {
        try
        {
            if (Utility.ShowExceptionError())
            {
                lblError.Visible = true;
                lblError.Text += "step:0 InvoiceNumber=" + Request.Form["invoiceNumber"]
                                // + "InvoiceNumber=" + Request.Form["InvoiceNumber"]
                                + "resultCode=" + Request.Form["resultCode"]
                                + "referenceId=" + Request.Form["referenceId"]
                                + "token=" + Request.Form["token"]
                                + "merchantId=" + Request.Form["merchantId"]
                                + "AccType:" + EPaymentUC.AccType.ToString();
            }
            PageMode = "BankReply";
            int AccountingId = - 2;// 462788;// 
            int AccDetailId =  - 2;
            if (Request.Form["invoiceNumber"] != null)
            {
                if (Utility.ShowExceptionError())
                    lblError.Text += "--Step:1 invoiceNumber=" + Request.Form["invoiceNumber"].ToString();
                AccDetailId = int.Parse(Request.Form["invoiceNumber"]);
                if (Utility.ShowExceptionError())
                    lblError.Text += "--Step:2 invoiceNumber=" + Request.Form["invoiceNumber"].ToString();
            }
            else
            {
                if (Utility.ShowExceptionError())
                    lblError.Text += "--Step:3 invoiceNumber is null";
            }

            if (AccDetailId == -2)
            {
                if (Utility.ShowExceptionError())
                    lblError.Text += "--Step:4 AccDetailId ==-2";
                return;
            }
            TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
            if (Utility.ShowExceptionError())
                lblError.Text += "--Step:4/1 AccDetailId=" + AccDetailId.ToString();
            AccountingDetailManager.FindById(AccDetailId);
            if (AccountingDetailManager.Count != 1)
            {
                if (Utility.ShowExceptionError())
                    lblError.Text += "--Step:5 count!=1";
                ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است.");
                return;
            }
            EPaymentUC.AccType = Convert.ToInt32(AccountingDetailManager[0]["AccType"]);
            EPaymentUC.TableId = Convert.ToInt32(AccountingDetailManager[0]["TableTypeId"]);
            EPaymentUC.AccountingId = Convert.ToInt32(AccountingDetailManager[0]["AccountingId"]);
            if (Utility.ShowExceptionError())
                lblError.Text += "--Step:5/1 EPaymentUC.AccType=" + EPaymentUC.AccType.ToString() + "**EPaymentUC.TableId=" + EPaymentUC.TableId.ToString() + "**EPaymentUC.AccountingId=" + EPaymentUC.AccountingId.ToString();
            if (Utility.ShowExceptionError())
                lblError.Text += "--Step:6 *****";
            if (EPaymentUC.AccType > 0)
            {
                EPaymentUC.SetEPaymentParameters(EPaymentUC.AccType
                                            , EPaymentUC.TableId
                                              , PageMode, Request.Form["invoiceNumber"] != null ? Convert.ToInt32(Request.Form["invoiceNumber"]) : -1
                                              , Request.Form["resultCode"] != null ? Request.Form["resultCode"] : "-1"
                                              , Request.Form["referenceId"] != null ? Request.Form["referenceId"] : "-1", Request.Form["Token"] != null ? Request.Form["Token"].ToString() : "");
            }
            if (Utility.ShowExceptionError())
                lblError.Text += "--Step:7 *****";
            EPaymentUC.DoNextTaskOfBankReply(Utility.GetCurrentUser_UserId(), Utility.GetCurrentUser_LoginType(), Request.Form["Token"] != null ? Request.Form["Token"].ToString() : "");
            if (Utility.ShowExceptionError())
                lblError.Text += "--Step:8 *****";
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است.");
            if (Utility.ShowExceptionError())
            {
                lblError.Visible = true;
                lblError.Text += "step:catch**paymentId=" + Request.Form["paymentId"]
                                + "resultCode=" + Request.Form["resultCode"]
                                + "referenceId=" + Request.Form["referenceId"]
                                + "token=" + Request.Form["token"]
                                + "merchantId=" + Request.Form["merchantId"];
            }
        }
        if (Utility.ShowExceptionError())
        {
            lblError.Visible = true;
            lblError.Text += "step:9 InvoiceNumber=" + Request.Form["InvoiceNumber"]
                            + "resultCode=" + Request.Form["resultCode"]
                            + "referenceId=" + Request.Form["referenceId"]
                            + "token=" + Request.Form["token"]
                            + "merchantId=" + Request.Form["merchantId"];
        }
    }



    protected void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetMultiplePaymentTypeByAccType()
    {
        switch (EPaymentUC.AccType)
        {
            case (int)TSP.DataManager.TSAccountingAccType.PeriodRegister:
                MultiplePaymentType = "PeriodRegister";
                break;
        }
    }

    String GetPortalUrl()
    {
        String Url = "";

        switch (Utility.GetCurrentUser_LoginType())
        {
            case (int)TSP.DataManager.UserType.Member:// 1:
                Url = "~/Members/MemberHome.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
                break;
            case (int)TSP.DataManager.UserType.TemporaryMembers:// 1:
                Url = "~/Members/MemberHome.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
                break;
            case (int)TSP.DataManager.UserType.Office://2:
                Url = "~/Office/OfficeHome.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
                break;
            case 3:
                break;
            case (int)TSP.DataManager.UserType.Employee://4:
                Url = "~/Employee/EmployeeHome.aspx";
                break;
            case (int)TSP.DataManager.UserType.Teacher://5:
                Url = "~/Teachers/TeacherHome.aspx";
                break;
            case (int)TSP.DataManager.UserType.Institute://6:
                Url = "~/Institue/Amoozesh/InstitueHome.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
                break;
            case (int)TSP.DataManager.UserType.Settlement://7:
                Url = "~/Settlement/SettlmentHomePage.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
                break;
            case (int)TSP.DataManager.UserType.Municipality://8:
                Url = "~/Municipality/MunHomePage.aspx?MeId=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString());
                break;
            default:
                Url = "~/ErrorPage.aspx";
                break;
        }
        return Url;
    }
    #endregion
}