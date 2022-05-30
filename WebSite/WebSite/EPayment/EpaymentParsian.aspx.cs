using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EPayment_EpaymentParsian : System.Web.UI.Page
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

    private int _CitId
    {
        set
        {
            HiddenFieldEpayment["CitId"] = value;
        }
        get
        {
            return int.Parse(HiddenFieldEpayment["CitId"].ToString());
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
        }
    }

    private Boolean SetData()
    {

        if (string.IsNullOrEmpty(Request.QueryString["InvoiceNo"]) || string.IsNullOrEmpty(Request.QueryString["Cit"]))
        {
            ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است.");
            return false;
        }
        _AccDetailId = int.Parse(Utility.DecryptQS(Request.QueryString["InvoiceNo"]));
        _CitId = int.Parse(Utility.DecryptQS(Request.QueryString["Cit"]));

        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
        AccountingDetailManager.FindById(_AccDetailId);
        if (AccountingDetailManager.Count != 1)
        {
            ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است.");
            return false;
        }
        EPaymentUC.CitId = _CitId;
        EPaymentUC.AccountingId = _AccountingId = Convert.ToInt32(AccountingDetailManager[0]["AccountingId"]);
        EPaymentUC.AccType = Convert.ToInt32(AccountingDetailManager[0]["AccType"]);
        EPaymentUC.TableId = Convert.ToInt32(AccountingDetailManager[0]["TableTypeId"]);
        EPaymentUC.Amount = Convert.ToInt32(AccountingDetailManager[0]["Amount"]);
        EPaymentUC.InvoiceNumber = _AccDetailId;
        if (EPaymentUC.AccType > 0)
        {
            EPaymentUC.SetEPaymentParameters(EPaymentUC.AccType
                                        , EPaymentUC.TableId
                                          , "View", _AccountingId
                                          , "-1"
                                          , "-1", "", _CitId);
        }
        return true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Amount"></param>
    /// <param name="PinCode"></param>
    /// <param name="RevertURLPayment"></param>
    /// <param name="OrderId">InvoiceNumber : AccDetailId</param>
    private void ParsianPayment(string Amount, string PinCode, string RevertURLPayment, long OrderId, string PaymentIdProvnce)
    {
        long token = 0;
        ParsianPGWSalePaymentServices.SaleServiceSoapClient SaleService = new ParsianPGWSalePaymentServices.SaleServiceSoapClient();
        //بی خیال شدن اس اس ال جهت ارتباط امن با سرویس پرداخت قبض 
        System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback((o, xc, xch, sslP) => true);
        ////تعیین مسیر صحیح وب سرویس درخواست پرداخت قبض پارسیان
        //service.Url = PecPGW.Samples.Common.ConfigHelper.ParsianPGWSaleServiceUrl;
        //ایجاد یک نمونه از نوع پارامتر ورودی به متد درخواست پرداخت مبلغ خرید کالا یا خدمات وب سرویس درخواست پرداخت خرید
        var paymentRequest = new ParsianPGWSalePaymentServices.ClientSaleRequestData();
        //شناسه پذیرندگی در درگاه پرداخت اینترنتی پارسیان
        paymentRequest.LoginAccount = PinCode;// txtLoginAccount.Text.Trim();

        //make sure you set the CallBackUrl property. because after user has completed Payment on IPG page, it will be redirected to the callback url you provided
        //to get you back result of the user Payment on IPG.


        paymentRequest.CallBackUrl = RevertURLPayment;// ConfigHelper.SalePaymentCallback;

        //Amount is not used. you should not assign a value to this property.
        paymentRequest.Amount = long.Parse(Amount); // long.Parse(txtAmount.Text);

        //Order Id MUST be UNIQUE at all times. if a duplicated Order Id is received from your request, you will get Status=-112
        paymentRequest.OrderId = OrderId;// DateTime.Now.Ticks;

        paymentRequest.AdditionalData = PaymentIdProvnce;// txtAddData.Text;
        ParsianPGWSalePaymentServices.ClientPaymentResponseDataBase response = SaleService.SalePaymentRequest(paymentRequest);

        if (response == null)
        {
            ShowMessage(" نتيجه ارسال درخواست ورود به درگاه پارسيان نال بوده است ");
            return;
        }


        //check Status property of the response object to see if the operation was successful.
        if (response.Status == (short)TSP.DataManager.ParsianPaymentGateway.Successful)// PecPGW.Samples.WebApp.WebForms.Constants.ParsianPaymentGateway.Successful)
        {
            //if everything is OK (LoginAccount and your IP address is valid in the Parsian PGW), save the token in a data store
            // to use it for redirectgion of your web site's user to the Parsian IPG (Internet Payment Gateway) page to complete payment.
            token = response.Token;

            //you must save the token in a data store for further support and rosolving 
            Session["Token"] = token;

            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
            AccountingManager.FindByAccountingId(_AccountingId);
            if (AccountingManager.Count != 1)
            {
                btnPayment.Visible = false;
                ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است.");
                return;
            }
            AccountingManager[0].BeginEdit();
            AccountingManager[0]["Token"] = token;
            AccountingManager[0].EndEdit();
            AccountingManager.Save();
            if (response.Status == (short)TSP.DataManager.ParsianPaymentGateway.Successful && token != 0L)
            {
                //first, save token to your database to be able to track payment process with your business.
                //after successfully retrieved a token from Parsian PGW, redirect user to Parsian IPG to complete the payment operation.
                var redirectUrl = string.Format(TSP.Utility.OnlinePayment.ParsianIPGPageUrl, token);
                Response.Redirect(redirectUrl);
            }
            else
            {
                ShowMessage(TSP.Utility.OnlinePayment.GetResultCodeTextForParsian((int)response.Status) + " " + "در حال حاضر امکان پرداخت وجود ندارد");
            }
        }
        else
        {
            ShowMessage(TSP.Utility.OnlinePayment.GetResultCodeTextForParsian((int)response.Status) + " نتيجه ارسال درخواست ورود به درگاه پارسيان می باشد ");
        }

    }
    //private void ParsianPaymentSample()
    //{
    //    long token = 0;
    //    short paymentStatus = Int16.MinValue;

    //    //ایجاد یک نمونه از سرویس درخواست پرداخت قبض درگاه پرداخت اینترنتی پارسیان
    //    using (var service = new ParsianPGWSalePaymentServices.SaleService())
    //    {
    //        //بی خیال شدن اس اس ال جهت ارتباط امن با سرویس پرداخت قبض 
    //        System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback((o, xc, xch, sslP) => true);

    //        //تعیین مسیر صحیح وب سرویس درخواست پرداخت قبض پارسیان
    //        service.Url = PecPGW.Samples.Common.ConfigHelper.ParsianPGWSaleServiceUrl;

    //        //ایجاد یک نمونه از نوع پارامتر ورودی به متد درخواست پرداخت مبلغ خرید کالا یا خدمات وب سرویس درخواست پرداخت خرید
    //        var paymentRequest = new ParsianPGWSalePaymentServices.ClientSaleRequestData();

    //        //شناسه پذیرندگی در درگاه پرداخت اینترنتی پارسیان
    //        paymentRequest.LoginAccount = txtLoginAccount.Text.Trim();

    //        //make sure you set the CallBackUrl property. because after user has completed Payment on IPG page, it will be redirected to the callback url you provided
    //        //to get you back result of the user Payment on IPG.


    //        paymentRequest.CallBackUrl = ConfigHelper.SalePaymentCallback;

    //        //Amount is not used. you should not assign a value to this property.
    //        paymentRequest.Amount = long.Parse(txtAmount.Text);

    //        //Order Id MUST be UNIQUE at all times. if a duplicated Order Id is received from your request, you will get Status=-112
    //        paymentRequest.OrderId = DateTime.Now.Ticks;

    //        paymentRequest.AdditionalData = txtAddData.Text;
    //        ClientPaymentResponseDataBase response = service.SalePaymentRequest(paymentRequest);

    //        if (response == null)
    //        {
    //            return;
    //        }

    //        paymentStatus = response.Status;

    //        //check Status property of the response object to see if the operation was successful.
    //        if (response.Status == PecPGW.Samples.WebApp.WebForms.Constants.ParsianPaymentGateway.Successful)
    //        {
    //            //if everything is OK (LoginAccount and your IP address is valid in the Parsian PGW), save the token in a data store
    //            // to use it for redirectgion of your web site's user to the Parsian IPG (Internet Payment Gateway) page to complete payment.
    //            token = response.Token;

    //            //you must save the token in a data store for further support and rosolving 
    //            Session["Token"] = token;
    //        }
    //        else
    //        {
    //            logger.Error($"Parsian PGW service call status code : {response.Status}");
    //        }
    //    }

    //    if (paymentStatus == Constants.ParsianPaymentGateway.Successful && token != 0L)
    //    {
    //        //first, save token to your database to be able to track payment process with your business.
    //        //after successfully retrieved a token from Parsian PGW, redirect user to Parsian IPG to complete the payment operation.
    //        var redirectUrl = string.Format(ConfigHelper.ParsianIPGPageUrl, token);
    //        Response.Redirect(redirectUrl);
    //    }
    //    else
    //    {
    //        Server.TransferRequest("~/Error.aspx");
    //    }
    //}

    protected void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    protected void btnPayment_Click(object sender, EventArgs e)
    {
        SetData();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.FindByAccountingId(_AccountingId);
        if (AccountingManager.Count != 1)
        {
            btnPayment.Visible = false;
            ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است.");
            return;
        }
        if (Convert.ToInt32(AccountingManager[0]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.Payment)
        {
            ShowMessage("شما پیش از این فیش انتخاب شده را پرداخت کرده اید.");
            return;
        }
        string AmountPayment = Convert.ToInt32(AccountingManager[0]["Amount"]).ToString();
        string InvoiceNumber = _AccDetailId.ToString();
        string SpecialPaymentId = AccountingManager[0]["FollowNumber"].ToString();
        int AccType = Convert.ToInt32(AccountingManager[0]["AccType"]);
        int TableId = Convert.ToInt32(AccountingManager[0]["TableTypeId"]);
        string Description = AccountingManager[0]["Description"].ToString();
        int FishPayerId = -2;
        if (!Utility.IsDBNullOrNullValue(AccountingManager[0]["FishPayerId"]))
            FishPayerId = Convert.ToInt32(AccountingManager[0]["FishPayerId"]);
        string PaymentId = Utility.GetCurrentUser_UserId().ToString();//**برای بانک تجارت مقدار براساس نوع حساب در محاسبه می شود//**GetPaymentId

        string RevertURLPayment = TSP.Utility.OnlinePayment.GetRevertURL(TSP.DataManager.EpaymentType.ParsianGetWay, (TSP.DataManager.TSAccountingAccType)AccType, TableId);
        if (TSP.Utility.OnlinePayment.SetepaymentAmountForTest())
        {
            AmountPayment = "1000";
        }
        if (Utility.ShowExceptionError())
        {
            lblError.Visible = true;
            lblError.Text += "Step0: AmountPayment:" + AmountPayment + "PINCode:" + EPaymentUC.PINCode + "InvoiceNumber:" + InvoiceNumber
                + "PaymentId:" + PaymentId + "SpecialPaymentId:" + SpecialPaymentId + "RevertURLPayment:" + RevertURLPayment + "Desc" + "سازمان نظام مهندسی ساختمان استان فارس";
        }
        string PaymentIdProvnce = "";
        string TafziliCodeProvince = "";
        string AgentCodeForPaymentIdProvince = "";
        int MunId = -2;
        int DocumentOfMemberRequestType = -2;
        switch (AccType)
        { //**** ProjectId = FishPayerId;
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
            case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:
            case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
            case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
                System.Data.DataTable dtPrj = ProjectManager.SelectTSProjectAgentCod(FishPayerId);
                if (dtPrj.Rows.Count != 0)
                {
                    AgentCodeForPaymentIdProvince = dtPrj.Rows[0]["AgentCodeForPaymentIdProvince"].ToString();
                    MunId = Convert.ToInt32(dtPrj.Rows[0]["MunId"]);
                }
                break;
            case (int)TSP.DataManager.TSAccountingAccType.DocMemberFile:
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DocMemberFileManager.FindByCode(TableId, 0);
                if(DocMemberFileManager.Count==0)
                {
                    btnPayment.Visible = false;
                    ShowMessage("خطایی در بازیابی اطلاعات بوجود آمده است.");
                    return;
                }
                DocumentOfMemberRequestType= Convert.ToInt32(DocMemberFileManager[0]["Type"]);
                break;
        }
        switch (AccType)
        { //**** ProjectId = FishPayerId;
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                TafziliCodeProvince = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTSProvince(TSP.DataManager.TSAccountingAccType.ObserversFiche, AgentCodeForPaymentIdProvince, FishPayerId.ToString(), "99999");
                break;
            case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:
            case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
            case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                TafziliCodeProvince = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForTSProvince((TSP.DataManager.TSAccountingAccType)AccType, AgentCodeForPaymentIdProvince, FishPayerId.ToString(), Utility.GetCurrentUser_MeId().ToString());
                break;
            case (int)TSP.DataManager.TSAccountingAccType.DocMemberFile:
                TafziliCodeProvince = TSP.Utility.OnlinePayment.GetTafziliCodeOfPaymentIdForDocument((TSP.DataManager.TSAccountingAccType)AccType,(TSP.DataManager.DocumentOfMemberRequestType)DocumentOfMemberRequestType, Utility.GetCurrentUser_MeId().ToString());
                break;
        }
        if (AccType == (int)TSP.DataManager.TSAccountingAccType.ObserversFiche ||
  AccType == (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation ||
  AccType == (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure ||
  AccType == (int)TSP.DataManager.TSAccountingAccType.Designing5Percent ||
  AccType == (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation ||
  AccType == (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure ||
  AccType == (int)TSP.DataManager.TSAccountingAccType.DocMemberFile)
        {
            PaymentIdProvnce = TSP.Utility.OnlinePayment.GetPaymentIdForProvince((TSP.DataManager.TSAccountingAccType)AccType, TafziliCodeProvince, MunId, false);
        }
        ParsianPayment(AmountPayment, EPaymentUC.PINCode, RevertURLPayment, _AccDetailId, PaymentIdProvnce);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Members/MemberHome.aspx", false);
    }

}