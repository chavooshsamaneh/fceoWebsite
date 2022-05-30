using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EPayment_EpaymentVerifyParsian : System.Web.UI.Page
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
            #region
            var token = Convert.ToInt64(Request.Form["Token"]);
            var orderId = Convert.ToInt64(Request.Form["OrderId"]);
            var terminalNumber = Convert.ToInt32(Request.Form["TerminalNo"]);
            var rrn = Convert.ToInt64(Request.Form["RRN"]);
            var status = Convert.ToInt16(Request.Form["status"]);
            var cardNumberHashed = Request.Form["HashCardNumber"];
            var amountAsString = Request.Form["Amount"]; //amount is formatted as a currency string
                ////long amount;
                //////به دلیل اینکه مبلغ با جداکننده ویرگول به ازای هر سه رقم می باشد، باید بصورت زیر استخراج شود
                //////البته به دلیل اینکه مبلغ را پذیرنده قبلاً به درگاه پرداخت ارسال نموده است، بهتر است از پایگاه داده خودش آن را در صورتی که نیاز دارد استخراج نماید
                ////bool amountParseWasSucceed = long.TryParse(amountAsString, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out amount);

                var tspToken = Request.Form["TspToken"];
            if (Utility.ShowExceptionError())
            {
                lblError.Visible = true;
                lblError.Text += "step:0 Token=" + Request.Form["Token"]
                                 + "OrderId=" + Request.Form["OrderId"]
                                + "TerminalNo=" + Request.Form["TerminalNo"]
                                + "RRN=" + Request.Form["RRN"]
                                + "status=" + Request.Form["status"]
                                + "HashCardNumber=" + Request.Form["HashCardNumber"]
                                + "Amount=" + Request.Form["Amount"]
                                + "TspToken=" + Request.Form["TspToken"]
                                + "AccType:" + EPaymentUC.AccType.ToString();
            }

            PageMode = "BankReply";
            int AccountingId = -2;
            int AccDetailId = -2;// 162975;//?????????**** - 2;
            int CitId = -2;
            if (Request.Form["OrderId"] != null)
            {
                if (Utility.ShowExceptionError())
                    lblError.Text += "--Step:1 OrderId=" + Request.Form["OrderId"].ToString();
                AccDetailId = int.Parse(Request.Form["OrderId"]);
                if (Utility.ShowExceptionError())
                    lblError.Text += "--Step:2 OrderId=" + Request.Form["OrderId"].ToString();
            }
            else
            {
                if (Utility.ShowExceptionError())
                    lblError.Text += "--Step:3 OrderId is null";
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
            EPaymentUC.CitId = CitId = Convert.ToInt32(AccountingDetailManager[0]["CitId"]);
            if (Utility.ShowExceptionError())
                lblError.Text += "--Step:5/1 EPaymentUC.AccType=" + EPaymentUC.AccType.ToString() + "**EPaymentUC.TableId=" + EPaymentUC.TableId.ToString() + "**EPaymentUC.AccountingId=" + EPaymentUC.AccountingId.ToString();
            if (Utility.ShowExceptionError())
                lblError.Text += "--Step:6 *****";
            if (EPaymentUC.AccType > 0)
            {//***RRN= RefrenceId برای پیگیری تراکنش با بانک
                EPaymentUC.SetEPaymentParameters(EPaymentUC.AccType
                                            , EPaymentUC.TableId
                                              , PageMode, Request.Form["OrderId"] != null ? Convert.ToInt32(Request.Form["OrderId"]) : -1
                                              , Request.Form["status"] != null ? Request.Form["status"] : "-1"
                                              , Request.Form["RRN"] != null ? Request.Form["RRN"] : "-1", Request.Form["Token"] != null ? Request.Form["Token"].ToString() : "", CitId);

                //////EPaymentUC.SetEPaymentParameters(EPaymentUC.AccType
                //////                              , EPaymentUC.TableId
                //////                                , PageMode, 162975
                //////                                , "0"
                //////                                , Request.Form["RRN"] != null ? Request.Form["RRN"] : "-1", "433714250", CitId);
            }
            if (Utility.ShowExceptionError())
                lblError.Text += "--Step:7 *****";
            if (status == (short)TSP.DataManager.ParsianPaymentGateway.Successful)
            {
                EPaymentUC.DoNextTaskOfBankReplyParsian(Utility.GetCurrentUser_UserId(), Utility.GetCurrentUser_LoginType(), Request.Form["Token"] != null ? Request.Form["Token"].ToString() : "");// "433714250"
                if (Utility.ShowExceptionError())
                    lblError.Text += "--Step:8 *****";
            }
            else
            {
                ShowMessage(TSP.Utility.OnlinePayment.GetResultCodeTextForParsian((int)status) );
            }
            //if (status == (short)TSP.DataManager.ParsianPaymentGateway.Successful)
            //{
            //    ConfirmPayment(token);
            //}
            #endregion
           
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
            lblError.Text += "step:9 OrderId=" + Request.Form["OrderId"]
                            + "status=" + Request.Form["status"]
                            + "RRN=" + Request.Form["RRN"]
                            + "token=" + Request.Form["token"]
                            + "merchantId=" + Request.Form["merchantId"];
        }
    }


    ///// <summary>
    ///// Confirm payment must be performed only for SALE payment.
    ///// </summary>
    ///// <param name="token"></param>
    //private void ConfirmPayment(long token)
    //{
    //    ParsianPGWConfirmPaymentServices.ConfirmServiceSoapClient confirmSvc = new ParsianPGWConfirmPaymentServices.ConfirmServiceSoapClient();

    //    //ParsianPGWSalePaymentServices.cli response = SaleService.SalePaymentRequest(paymentRequest);

    //    ParsianPGWConfirmPaymentServices.ClientConfirmRequestData confirmRequestData = new ParsianPGWConfirmPaymentServices.ClientConfirmRequestData();
    //    confirmRequestData.Token = token;
    //    confirmRequestData.LoginAccount = "??????????";// ConfigHelper.LoginAccount;
    //    var confirmResponse = confirmSvc.ConfirmPayment(confirmRequestData);
    //    if (confirmResponse.Status == (short)TSP.DataManager.ParsianPaymentGateway.Successful)
    //    {
    //        ShowMessage("پرداخت با موفقیت انجام شد.");
    //    }
    //    else
    //    {
    //        ShowMessage("payment was successful, however, Confirm payment was not successful.");
    //    }

    //}

    protected void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
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