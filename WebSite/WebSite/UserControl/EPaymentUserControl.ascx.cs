using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UserControl_EPaymentUserControl : System.Web.UI.UserControl
{
    #region Properties

    private int _PaymentGateWay = 0;
    public int PayGateWay
    {
        get
        {
            return _PaymentGateWay;
        }
        set
        {
            _PaymentGateWay = value;
        }
    }

    private int _CitId = -1;
    public int CitId
    {
        get
        {
            return _CitId;
        }
        set
        {
            _CitId = value;
        }
    }
    private string _BankURL = "";
    public string BankURL
    {
        get
        {
            return _BankURL;
        }
        set
        {
            _BankURL = value;
        }
    }

    private int _AccType = -1;
    public int AccType
    {
        get
        {
            return _AccType;
        }
        set
        {
            _AccType = value;
        }
    }

    private int _Amount = -1;
    public int Amount
    {
        get
        {
            return _Amount;
        }
        set
        {
            _Amount = value;
        }
    }
    #region درگاه پارسیان
    public string _PINCode = "";
    public string PINCode
    {
        get
        {
            return _PINCode;
        }
        set
        {
            _PINCode = value;
        }
    }
    public string _Terminal = "";
    public string Terminal
    {
        get
        {
            return _Terminal;
        }
        set
        {
            _Terminal = value;
        }
    }

    private string _PINCodeDocumentMember
    {
        get
        {
            return "k42cOT7ig41wJ8S765Hi";
        }
    }
    private string _TerminalDocumentMember
    {
        get
        {
            return "44959784";
        }
    }
    #endregion

    public string _merchantId = "";
    public string merchantId
    {
        get
        {
            return _merchantId;
        }
        set
        {
            _merchantId = value;
        }
    }

    private string _RevertURL = "";
    public string RevertURL
    {
        get
        {
            return _RevertURL;
        }
        set
        {
            _RevertURL = value;
        }
    }

    private int _PaymentId = -1;
    /// <summary>
    /// شناسه خرید مشتری
    /// 1-شناسه پرداخت
    ///OR 2-Userid
    /// </summary>
    public int PaymentId
    {
        get
        {
            return _PaymentId;
        }
        set
        {
            _PaymentId = value;
        }
    }

    private int _InvoiceNumber = -2;
    /// <summary>
    /// AccountingDetailId - شناسه فاکتور
    /// </summary>
    public int InvoiceNumber
    {
        get
        {
            return _InvoiceNumber;
        }
        set
        {
            _InvoiceNumber = value;
        }
    }

    private int _AccountingId = -2;
    /// <summary>
    /// AccountingId - شناسه فاکتور
    /// </summary>
    public int AccountingId
    {
        get
        {
            return _AccountingId;
        }
        set
        {
            _AccountingId = value;
        }
    }

    private string _SpecialPaymentId = "";

    /// <summary>
    /// FollowCode جهت پیگیری در درگاه
    /// </summary>
    public string SpecialPaymentId
    {
        get
        {
            return _SpecialPaymentId;
        }
        set
        {
            _SpecialPaymentId = value;
        }
    }

    private string _Token = "";
    public string Token
    {
        get
        {
            return _Token;
        }
        set
        {
            _Token = value;
        }
    }

    ///// <summary>
    ///// شماره پیگیری
    ///// </summary>
    //private string _customerId = "";
    //public string customerId
    //{
    //    get
    //    {
    //        return _customerId;
    //    }
    //    set
    //    {
    //        _customerId = value;
    //    }
    //}

    private int _TableId = -1;
    public int TableId
    {
        get
        {
            return _TableId;
        }
        set
        {
            _TableId = value;
        }
    }

    private int _TableType = -1;
    public int TableType
    {
        get
        {
            return _TableType;
        }
        set
        {
            _TableType = value;
        }
    }

    private string _PageMode = "";
    public string PageMode
    {
        get
        {
            return _PageMode;
        }
        set
        {
            _PageMode = value;
        }
    }

    private string _ResultCode = "-1";
    public string ResultCode
    {
        get
        {
            return _ResultCode;
        }
        set
        {
            _ResultCode = value;
        }
    }

    private string _ReferenceId = "-1";
    public string ReferenceId
    {
        get
        {
            return _ReferenceId;
        }
        set
        {
            _ReferenceId = value;
        }
    }

    private string _FullNamePayer = "";
    public string FullNamePayer
    {
        get
        {
            return _FullNamePayer;
        }
        set
        {
            _FullNamePayer = value;
        }
    }

    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        SetPaymentInfo();
        if (!IsPostBack)
        {
            SetMode();
        }
        FillInfo();
    }
    #endregion

    #region Methods
    private Boolean CheckTsTimeOut()
    {
        if (Session["FishEPayment"] == null)
        {
            return true;
        }
        return false;
    }

    private void SetPaymentInfo()
    {
        if (_PaymentGateWay == (int)TSP.DataManager.PaymentGateWay.IranKish)
        {
            merchantId = TSP.Utility.OnlinePayment.GetNezamMerchantId((TSP.DataManager.TSAccountingAccType)AccType);
            Amount = FindAmount(AccType);
        }
        else if (_PaymentGateWay == (int)TSP.DataManager.PaymentGateWay.Parsian)
        {
            TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
            switch (AccType)
            {
                case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:
                case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
                    CityManager.FindByCode(_CitId);
                    merchantId = "";
                    if (CityManager.Count != 0 && CityManager[0]["PINCodeDesign"] != null)
                    {
                        PINCode = CityManager[0]["PINCodeDesign"].ToString();
                        Terminal = CityManager[0]["TerminalDesign"].ToString();
                    }
                    break;

                case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                    CityManager.FindByCode(_CitId);
                    merchantId = "";
                    if (CityManager.Count != 0 && CityManager[0]["PINCodeObserver"] != null)
                    {
                        PINCode = CityManager[0]["PINCodeObserver"].ToString();
                        Terminal = CityManager[0]["TerminalObserver"].ToString();
                    }
                    break;
                case (int)TSP.DataManager.TSAccountingAccType.DocMemberFile:
                    merchantId = "";
                    PINCode = _PINCodeDocumentMember;
                    Terminal = _TerminalDocumentMember;
                    Amount = FindAmount(AccType);
                    break;
            }
        }
    }

    #region Set Mode
    private void SetMode()
    {
        switch (PageMode)
        {
            case "BankReply"://EpaymentVerify
                SetBankReplyMode();
                break;
            case "EPayment":
                SetEPaymentMode();
                break;
            case "MultipleEPayment":
                SetMultipleEPaymentMode();
                break;
            case "View"://Epayment
                SetViewMode();
                break;
            default:
                ShowError("مقادیر صفحه معتبر نمی باشد");
                break;
        }
    }

    private void SetBankReplyMode()
    {
        GridViewAccounting.Visible = false;
        PanelPaymentInfo.Visible = true;
    }

    private void SetEPaymentMode()
    {
        GridViewAccounting.Visible = true;
        PanelPaymentInfo.Visible = false;
        Session["FishEPayment"] = null;
        InsertFishAndPayment();
    }

    private void SetMultipleEPaymentMode()
    {
        GridViewAccounting.Visible = true;
        PanelPaymentInfo.Visible = false;
        Session["FishEPayment"] = null;
        //InsertFishAndPayment();
    }

    private void SetViewMode()
    {
        GridViewAccounting.Visible = false;
        PanelPaymentInfo.Visible = true;
    }
    #endregion

    #region FillInfo
    private void FillInfo()
    {

        switch (PageMode)
        {
            case "EPayment":
                FillEPaymentInfo();
                break;
            case "MultipleEPayment":
                //FillEPaymentInfo();
                BindAccountingGrid();
                break;
            case "BankReply":
                //  FillBankReplyInfo();
                break;
            case "View":
                FillViewMode();
                break;
            default:
                ShowError("مقادیر صفحه معتبر نمی باشد");
                break;
        }
    }

    private void FillEPaymentInfo()
    {
        if (CheckTsTimeOut())
            return;
        BindAccountingGrid();
    }

    private void FillViewMode()
    {
        FillFishInfo();
    }

    private void FillFishInfo()
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.FindByAccountingId(_AccountingId);
        if (AccountingManager.Count != 1)
        {
            ShowError(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        lblPaymentAmount.Text = Convert.ToDecimal(AccountingManager[0]["Amount"]).ToString("#,#");
        //lblPaymentFolloCode.Text = AccountingManager[0]["FollowNumber"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingManager[0]["ReferenceId"]))
            lblPaymentRefrenceId.Text = "کدرهگیری بانکی: " + AccountingManager[0]["ReferenceId"].ToString();
        lblPaymentType.Text = AccountingManager[0]["AccTypeName"].ToString();
        lblFishPayerName.Text = AccountingManager[0]["FishPayerName"].ToString();
        lblPaymentStatus.Text = AccountingManager[0]["StatusName"].ToString();
        lblPaymentDate.Text = AccountingManager[0]["Date"].ToString();
        if (!Utility.IsDBNullOrNullValue(AccountingManager[0]["Time"]))
            lblPaymentTime.Text = AccountingManager[0]["Time"].ToString();
    }
    #endregion

    #region PaymentMode
    private void BindAccountingGrid()
    {
        if (Session["FishEPayment"] != null)
        {
            GridViewAccounting.DataSource = ((TSP.DataManager.TechnicalServices.AccountingManager)Session["FishEPayment"]).DataTable;
            GridViewAccounting.DataBind();
        }
    }

    private void InsertFishAndPayment()
    {
        if (Session["FishEPayment"] == null)
        {
            TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
            DataRow dr = AccountingManager.NewRow();
            dr.BeginEdit();
            dr["TableTypeId"] = TableId;
            dr["TableType"] = TableType;
            dr["Type"] = (int)TSP.DataManager.AccountingPaymentType.EPayment;
            dr["Bank"] = DBNull.Value;
            dr["BranchCode"] = DBNull.Value;
            dr["BranchName"] = DBNull.Value;
            dr["AccType"] = AccType;
            dr["AccTypeName"] = TSP.DataManager.TechnicalServices.AccTypeManager.FindAccTypeName(AccType);
            dr["Number"] = "";
            dr["Date"] = Utility.GetDateOfToday();
            dr["Amount"] = Amount;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            dr.EndEdit();
            AccountingManager.AddRow(dr);
            Session["FishEPayment"] = AccountingManager;
        }

    }
    #endregion

    #region MultiplePaymentMode
    public int AddAccountingRow(int RowTableId, int RowTableType, string RowName, int RowAmount)
    {
        int RowCount = 0;
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager;
        if (Session["FishEPayment"] == null)
            AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        else
            AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["FishEPayment"];
        DataRow dr = AccountingManager.NewRow();
        dr.BeginEdit();
        dr["TableTypeId"] = RowTableId;
        dr["TableType"] = RowTableType;
        dr["Type"] = (int)TSP.DataManager.AccountingPaymentType.EPayment;
        dr["Bank"] = DBNull.Value;
        dr["BranchCode"] = DBNull.Value;
        dr["BranchName"] = DBNull.Value;
        dr["AccType"] = AccType;
        dr["AccTypeName"] = TSP.DataManager.TechnicalServices.AccTypeManager.FindAccTypeName(AccType) + "-" + RowName;
        dr["Number"] = "";
        dr["Date"] = Utility.GetDateOfToday();
        dr["Amount"] = RowAmount;
        if (RowAmount == 0)
        {
            dr["Description"] = "رایگان";
            GridViewAccounting.Columns["Description"].Visible = true;
        }
        else
            GridViewAccounting.Columns["Description"].Visible = false;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        dr.EndEdit();
        AccountingManager.AddRow(dr);
        Session["FishEPayment"] = AccountingManager;
        //Amount += RowAmount;
        BindAccountingGrid();
        RowCount = AccountingManager.Count;
        return RowCount;
    }

    public Boolean DeleteAccountingRow(int TableTypeId)
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager;
        if (Session["FishEPayment"] == null)
            AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        else
            AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["FishEPayment"];
        AccountingManager.CurrentFilter = "TableTypeId=" + TableTypeId.ToString();
        if (AccountingManager.Count != 1) return false;
        AccountingManager.DataTable.Rows[0].Delete();
        AccountingManager.DataTable.AcceptChanges();
        AccountingManager.CurrentFilter = "";
        Session["FishEPayment"] = AccountingManager;
        BindAccountingGrid();
        return true;
    }
    #endregion

    #region Bank Reply
    /// <summary>
    /// used in DoNextTaskOfBankReply function
    /// </summary>
    /// <param name="Message"></param>
    /// <param name="FollowCode"></param>
    /// <param name="PaymentAmount"></param>
    /// <param name="PaymentType"></param>
    /// <param name="BankReferenceId"></param>
    private void SetPaymentResualMode(string Message)
    {
        lblError.Visible = false;
        GridViewAccounting.Visible = false;
        PanelPaymentInfo.Visible = true;
        if (!Utility.IsDBNullOrNullValue(Message))
        {
            if (Utility.ShowExceptionError())
            {
                lblError.Visible = true;
                lblError.Text += Message;
            }
            lblEPaymentMessage.Text = Message;
        }
        else
            lblEPaymentMessage.Text = "خطایی در ارتباط با بانک ایجاد شده است.مبلغ پرداختی ، توسط بانک به حساب شما بازگردانده خواهد شد.";
    }

    private int BankPaymentsVerification()
    {
        var sha1Key = "22338240992352910814917221751200141041845518824222260";
        using (var VerifyService = new EpaymentVerify.VerifyClient())
        {
            int count = 0;
            //  while (count < 5)
            //  {
            int Result = -1;
            do
            {
                try
                {
                    var res = VerifyService.KicccPaymentsVerification(Token, merchantId, ReferenceId, sha1Key);
                    Result = Convert.ToInt32(res);
                    count = 5;
                }
                catch (Exception err)
                {
                    count++;
                }


            }
            while (Result < 0 && count < 5);
            return Convert.ToInt32(Result);

            // }
        }
    }
    /// <summary>
    /// ConfirmPayment
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private int BankPaymentsVerificationParsian(long token)
    {
        ParsianPGWConfirmPaymentServices.ConfirmServiceSoapClient confirmSvc = new ParsianPGWConfirmPaymentServices.ConfirmServiceSoapClient();

        ParsianPGWConfirmPaymentServices.ClientConfirmRequestData confirmRequestData = new ParsianPGWConfirmPaymentServices.ClientConfirmRequestData();
        confirmRequestData.Token = token;
        confirmRequestData.LoginAccount = PINCode;
        var confirmResponse = confirmSvc.ConfirmPayment(confirmRequestData);
        return confirmResponse.Status;

    }
    private int BankConfirmTransactionold()
    {
        if (Utility.IsEPaymentTest())
        {
            net.sabapardazesh.pg.MerchantService MerchantService = new net.sabapardazesh.pg.MerchantService();
            net.sabapardazesh.pg.verifyRequest verifyRequest = new net.sabapardazesh.pg.verifyRequest();
            verifyRequest.merchantId = merchantId;
            verifyRequest.referenceNumber = ReferenceId;
            int Value = Convert.ToInt32(MerchantService.verify(verifyRequest));
            return Value;
        }
        else
        {
            net.tejaratbank.pg.MerchantService MerchantService = new net.tejaratbank.pg.MerchantService();
            net.tejaratbank.pg.verifyRequest verifyRequest = new net.tejaratbank.pg.verifyRequest();
            verifyRequest.merchantId = merchantId;
            verifyRequest.referenceNumber = ReferenceId;
            int Value = Convert.ToInt32(MerchantService.verify(verifyRequest));
            return Value;
        }
    }
    #endregion

    public void SetEPaymentParameters(int AccTypeUserControl, int TableTypeUserControl, string PageModeUserControl, int InvoiceNumberUserControl, string ResultCodeUserControl
   , string ReferenceIdUserControl, string TokenUserControl)
    {
        SetEPaymentParameters(AccTypeUserControl, TableTypeUserControl, PageModeUserControl, InvoiceNumberUserControl, ResultCodeUserControl
   , ReferenceIdUserControl, TokenUserControl, -2);
    }
    public void SetEPaymentParameters(int AccTypeUserControl, int TableTypeUserControl, string PageModeUserControl, int InvoiceNumberUserControl, string ResultCodeUserControl
 , string ReferenceIdUserControl, string TokenUserControl, int CitId)
    {
        _CitId = CitId;
        AccType = AccTypeUserControl;
        TableType = TableTypeUserControl;
        PageMode = PageModeUserControl;

        if (TokenUserControl != "")
            Token = TokenUserControl;
        if (ResultCodeUserControl != "-1")
            ResultCode = ResultCodeUserControl;
        if (InvoiceNumberUserControl != -1)
            InvoiceNumber = InvoiceNumberUserControl;
        if (ReferenceIdUserControl != "-1")
            ReferenceId = ReferenceIdUserControl;
        switch (AccType)
        {
            case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:
            case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
            case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
            case (int)TSP.DataManager.TSAccountingAccType.DocMemberFile:
                _PaymentGateWay = (int)TSP.DataManager.PaymentGateWay.Parsian;
                break;
            default:
                _PaymentGateWay = (int)TSP.DataManager.PaymentGateWay.IranKish;
                break;

        }
        if (_PaymentGateWay == (int)TSP.DataManager.PaymentGateWay.IranKish)
        {

            if (AccType != -1)
                merchantId = TSP.Utility.OnlinePayment.GetNezamMerchantId((TSP.DataManager.TSAccountingAccType)AccType);

        }
        else if (_PaymentGateWay == (int)TSP.DataManager.PaymentGateWay.Parsian)
        {
            TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
            switch (AccType)
            {
                case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:
                case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
                    CityManager.FindByCode(_CitId);
                    merchantId = "";
                    if (CityManager.Count != 0 && CityManager[0]["PINCodeDesign"] != null)
                    {
                        _PINCode = CityManager[0]["PINCodeDesign"].ToString();
                        _Terminal = CityManager[0]["TerminalDesign"].ToString();
                    }
                    break;
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
                case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                    CityManager.FindByCode(_CitId);
                    merchantId = "";
                    if (CityManager.Count != 0 && CityManager[0]["PINCodeObserver"] != null)
                    {
                        PINCode = CityManager[0]["PINCodeObserver"].ToString();
                        Terminal = CityManager[0]["TerminalObserver"].ToString();
                    }
                    break;

                case (int)TSP.DataManager.TSAccountingAccType.DocMemberFile:
                    merchantId = "";
                    PINCode = _PINCodeDocumentMember;
                    Terminal = _TerminalDocumentMember;
                    break;
            }
        }
    }
    //********************************************************************************************************************************

    /// <summary>
    /// هنگام ذخیره اطلاعات قبل از رفتن به صفحه بانک فراخوانی می شود
    /// </summary>
    /// <param name="TransactionManager"></param>
    /// <param name="MainTableId"></param>
    /// <returns></returns>
    public int SaveFish(TSP.DataManager.TransactionManager TransactionManager, int MainTableId, int UserId, TSP.DataManager.EpaymentType EpaymentType, string TafziliCode, int DetailTableId)
    {
        string PaymentId = UserId.ToString();
        if (EpaymentType == TSP.DataManager.EpaymentType.WizardMemberRegistration
            || EpaymentType == TSP.DataManager.EpaymentType.MemberMultiplePayment)
        {
            PaymentId = TSP.Utility.OnlinePayment.GetPaymentId((TSP.DataManager.TSAccountingAccType)AccType, TafziliCode, 1);
        }
        switch ((TSP.DataManager.TSAccountingAccType)AccType)
        {
            case TSP.DataManager.TSAccountingAccType.MemberDebpt:
            case TSP.DataManager.TSAccountingAccType.Registeration:
            case TSP.DataManager.TSAccountingAccType.Entrance:
            case TSP.DataManager.TSAccountingAccType.Registeration_Entrance:
                PaymentId = TSP.Utility.OnlinePayment.GetPaymentId((TSP.DataManager.TSAccountingAccType)AccType, TafziliCode, 1);
                break;
        }
        if (Session["FishEPayment"] == null)
            InsertFishAndPayment();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["FishEPayment"];
        TransactionManager.Add(AccountingManager);
        AccountingManager[AccountingManager.Count - 1].BeginEdit();
        AccountingManager[AccountingManager.Count - 1]["TableTypeId"] = MainTableId;
        AccountingManager[AccountingManager.Count - 1]["FollowNumber"] = SpecialPaymentId = Utility.GenFollowCode(Utility.FollowType.EPayment);
        AccountingManager[AccountingManager.Count - 1]["Amount"] = Amount = FindAmount(AccType);
        if (Amount <= 0)
        {
            return -1;
        }
        if (String.IsNullOrWhiteSpace(FullNamePayer))
            FullNamePayer = Utility.GetCurrentUser_FullName();
        string Description = "سند پرداخت الکترونیکی مبلغ " + Amount + "ريال" + " بابت " + TSP.DataManager.TechnicalServices.AccTypeManager.FindAccTypeName(AccType) + "  در تاریخ" + Utility.GetDateOfToday() + " توسط " + FullNamePayer + " ثبت گردید";
        AccountingManager[AccountingManager.Count - 1]["Description"] = Description;
        AccountingManager[AccountingManager.Count - 1]["Date"] = Utility.GetDateOfToday();
        AccountingManager[AccountingManager.Count - 1]["Time"] = DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":" + DateTime.Now.TimeOfDay.Seconds;// DateTime.Today.TimeOfDay.ToString();
        AccountingManager[AccountingManager.Count - 1]["Status"] = (int)TSP.DataManager.TSAccountingStatus.SaveInDB;
        AccountingManager[AccountingManager.Count - 1]["UserId"] = UserId;
        AccountingManager[AccountingManager.Count - 1]["PaymentId"] = PaymentId;
        AccountingManager[AccountingManager.Count - 1].EndEdit();
        AccountingManager.Save();
        AccountingManager.DataTable.AcceptChanges();
        _AccountingId = Convert.ToInt32(AccountingManager[AccountingManager.Count - 1]["AccountingId"]);
        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
        TransactionManager.Add(AccountingDetailManager);
        DataRow dr = AccountingDetailManager.NewRow();
        dr["AccountingId"] = _AccountingId;
        if (DetailTableId == -1)
            dr["TableId"] = TableId;
        else
            dr["TableId"] = DetailTableId;
        dr["TableType"] = TableType;
        dr["Amount"] = Amount;
        dr["Description"] = Description;
        dr["UserId"] = UserId;
        dr["InActive"] = 0;
        dr["ModifedDate"] = DateTime.Now;
        AccountingDetailManager.AddRow(dr);
        AccountingDetailManager.Save();
        AccountingDetailManager.DataTable.AcceptChanges();

        InvoiceNumber = Convert.ToInt32(AccountingDetailManager[0]["AccDetailId"]);
        TableId = MainTableId;
        return InvoiceNumber;
    }

    public int SaveFish(TSP.DataManager.TransactionManager TransactionManager, int MainTableId, int UserId, TSP.DataManager.EpaymentType EpaymentType)
    {
        return SaveFish(TransactionManager, MainTableId, UserId, EpaymentType, "", -1);
    }
    public int UpdateFishAmount(TSP.DataManager.TransactionManager TransactionManager, int MainTableId, int UserId, TSP.DataManager.EpaymentType EpaymentType)
    {
        string PaymentId = UserId.ToString();
        Amount = FindAmount(AccType);

        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        TransactionManager.Add(AccountingManager);
        AccountingManager.FindByTableTypeId(MainTableId, TableType);
        if (AccountingManager.Count == 0)
        {
            return -1;
        }
        int SumAmountPayed = 0;
        int SumAmountUnPayed = 0;
        for (int i = 0; i < AccountingManager.Count; i++)
        {
            if (Convert.ToInt32(AccountingManager[i]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.Payment)
            {
                SumAmountPayed += Convert.ToInt32(AccountingManager[i]["Amount"]);
            }
            if (Convert.ToInt32(AccountingManager[i]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.SaveInDB)
            {
                SumAmountUnPayed += Convert.ToInt32(AccountingManager[i]["Amount"]);
            }
        }
        int DiffAmount = Amount - (SumAmountPayed + SumAmountUnPayed);
        if (DiffAmount > 0)//Convert.ToInt32(AccountingManager[0]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.Payment)
        {

            if (DiffAmount > 1)//بخاطر گرد کردن مبلغ فیش طراح برای یک بازه برای بعضی از فیش های طراحی تفاوت یک ریال وجود آمد به همین دلیل اینجا یک در نظر گرفتیم تا ماوتفاوت یک ریال ثبت نکند
            {
                Amount = DiffAmount;
                return SaveFish(TransactionManager, MainTableId, UserId, EpaymentType);
            }
            else
                return 0;
        }
        else if (Amount - SumAmountPayed != 0 && Convert.ToInt32(AccountingManager[AccountingManager.Count - 1]["Status"]) != (int)TSP.DataManager.TSAccountingStatus.Payment)
        {
            AccountingManager[AccountingManager.Count - 1].BeginEdit();
            AccountingManager[AccountingManager.Count - 1]["TableTypeId"] = MainTableId;
            AccountingManager[AccountingManager.Count - 1]["FollowNumber"] = SpecialPaymentId = Utility.GenFollowCode(Utility.FollowType.EPayment);
            AccountingManager[AccountingManager.Count - 1]["Amount"] = Amount - SumAmountPayed;
            if (Amount <= 0)
            {
                return -1;
            }
            if (String.IsNullOrWhiteSpace(FullNamePayer))
                FullNamePayer = Utility.GetCurrentUser_FullName();
            string Description = "سند پرداخت الکترونیکی مبلغ " + (Amount - SumAmountPayed) + "ريال" + " بابت " + TSP.DataManager.TechnicalServices.AccTypeManager.FindAccTypeName(AccType) + "  در تاریخ" + Utility.GetDateOfToday() + " توسط " + FullNamePayer + " ثبت گردید";
            AccountingManager[AccountingManager.Count - 1]["Description"] = Description;
            AccountingManager[AccountingManager.Count - 1]["Date"] = Utility.GetDateOfToday();
            AccountingManager[AccountingManager.Count - 1]["Time"] = DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":" + DateTime.Now.TimeOfDay.Seconds;// DateTime.Today.TimeOfDay.ToString();
            AccountingManager[AccountingManager.Count - 1]["Status"] = (int)TSP.DataManager.TSAccountingStatus.SaveInDB;
            AccountingManager[AccountingManager.Count - 1]["UserId"] = UserId;
            AccountingManager[AccountingManager.Count - 1]["PaymentId"] = PaymentId;
            AccountingManager[AccountingManager.Count - 1].EndEdit();
            AccountingManager.Save();
            AccountingManager.DataTable.AcceptChanges();
            _AccountingId = Convert.ToInt32(AccountingManager[AccountingManager.Count - 1]["AccountingId"]);
            TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
            TransactionManager.Add(AccountingDetailManager);
            AccountingDetailManager.FindByAccountingId(_AccountingId);
            if (AccountingDetailManager.Count > 0)
            {
                TableId = Convert.ToInt32(AccountingDetailManager[0]["TableId"]);
            }
            DataRow dr = AccountingDetailManager.NewRow();
            dr["AccountingId"] = _AccountingId;
            dr["TableId"] = TableId;
            dr["TableType"] = TableType;
            dr["Amount"] = Amount - SumAmountPayed;
            dr["Description"] = Description;
            dr["UserId"] = UserId;
            dr["InActive"] = 0;
            dr["ModifedDate"] = DateTime.Now;
            AccountingDetailManager.AddRow(dr);
            AccountingDetailManager.Save();
            AccountingDetailManager.DataTable.AcceptChanges();

            InvoiceNumber = Convert.ToInt32(AccountingDetailManager[AccountingDetailManager.Count - 1]["AccDetailId"]);
            TableId = MainTableId;
            return InvoiceNumber;
        }
        else
            return 0;
    }
    public int SaveFish(TSP.DataManager.TransactionManager TransactionManager, int MainTableId, int UserId, TSP.DataManager.EpaymentType EpaymentType, string TafziliCode)
    {
        return SaveFish(TransactionManager, MainTableId, UserId, EpaymentType, TafziliCode, -1);
    }

    private int FindAmount(int AccType)
    {
        int Amount = 0;
        switch (AccType)
        {
            case (int)TSP.DataManager.TSAccountingAccType.DocMemberFile:
                Amount = Convert.ToInt32(TSP.DataManager.AccountingCostSettingsManager.FindCostByType(TSP.DataManager.CostSettingsSData.MemberFileRegistrationCost));
                break;
            case (int)TSP.DataManager.TSAccountingAccType.Registeration_Entrance:
                Amount = Convert.ToInt32(TSP.DataManager.AccountingCostSettingsManager.FindCostByType(TSP.DataManager.CostSettingsSData.FirstMembershipCost))
                     + Convert.ToInt32(TSP.DataManager.AccountingCostSettingsManager.FindCostByType(TSP.DataManager.CostSettingsSData.YearlyMembershipCost));
                break;
            case (int)TSP.DataManager.TSAccountingAccType.Entrance:
                Amount = Convert.ToInt32(TSP.DataManager.AccountingCostSettingsManager.FindCostByType(TSP.DataManager.CostSettingsSData.FirstMembershipCost));
                break;
            case (int)TSP.DataManager.TSAccountingAccType.Registeration:
                Amount = Convert.ToInt32(TSP.DataManager.AccountingCostSettingsManager.FindCostByType(TSP.DataManager.CostSettingsSData.YearlyMembershipCost));
                break;
            case (int)TSP.DataManager.TSAccountingAccType.PeriodRegister:
            case (int)TSP.DataManager.TSAccountingAccType.SeminarRegister:
            case (int)TSP.DataManager.TSAccountingAccType.MemberDebpt:
                Amount = this.Amount;
                break;

            case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:
            case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
            case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:

            case (int)TSP.DataManager.TSAccountingAccType.ObserversFiche:
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation:
            case (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure:
                Amount = this.Amount;
                break;
        }
        return Amount;
    }

    /// <summary>
    /// هنگام بازگشت از صفحه بانک و ارسال پارامترها توسط بانک فراخوانی می شود
    /// </summary>
    /// <returns></returns>
    public Boolean DoNextTaskOfBankReply(int UserId, int UltId, string Token)
    {
        _PaymentGateWay = (int)TSP.DataManager.PaymentGateWay.IranKish;
        SetPaymentInfo();
        string ErrorMessage = "";
        string PayId = "";
        int NmcId = -1;
        string EPaymentResualtMessage = ""; int PayedAmount = 0; int ConfirmTransactionResultCode = 0;
        Boolean PaymentSucced = true;
        if (AccountingId <= 0)//InvoiceNumber <= 0)
        {
            ErrorMessage = "شناسه سند معتبر نمی باشد";
            if (Utility.ShowExceptionError())
                ErrorMessage += "AccountingId=" + AccountingId.ToString();
            ShowError(ErrorMessage);
            return false;
        }
        if (TableId <= 0)
        {
            ErrorMessage = "شناسه پرونده معتبر نمی باشد";
            if (Utility.ShowExceptionError())
                ErrorMessage += "TableId=" + TableId.ToString();
            ShowError(ErrorMessage);
            return false;
        }
        if (int.Parse(ResultCode) != (int)TSP.DataManager.AccountingEPaymentResultCodeManager.EPaymentSuccessResultCode.SuccessResultCode)
        {
            EPaymentResualtMessage = TSP.DataManager.AccountingEPaymentResultCodeManager.FindResualtCodeText(int.Parse(ResultCode));
            SetPaymentResualMode(EPaymentResualtMessage);
            if (Utility.ShowExceptionError())
            {
                lblError.Visible = true;
                lblError.Text += "SetpEpayUserControl00:ResualtCode=" + ResultCode + "ConfirmTransactionResultCode=" + ConfirmTransactionResultCode + "merchantId" + merchantId + "ReferenceId=" + ReferenceId + "Token=" + Token;
            }
            PaymentSucced = false;
            FillFishInfo();
            return PaymentSucced;
        }
        #region Define Manager
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        #endregion
        TransactionManager.Add(AccountingManager);
        AccountingManager.FindByAccountingId(AccountingId);//PaymentId);
        string resultOfflineDebtId = "-1";
        try
        {
            int ConfirmedTaskId = -2;
            switch (AccType)
            {
                case (int)TSP.DataManager.TSAccountingAccType.MemberDebpt:
                    TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
                    WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.ConfirmMemberAndEndProccess);
                    ConfirmedTaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);
                    break;
            }
            TransactionManager.BeginSave();
            #region Save ResualtInfo
            if (AccountingManager.Count != 1)
            {
                if (Utility.ShowExceptionError())
                    ErrorMessage += "AccountingId=" + AccountingId.ToString();
                ErrorMessage = "اطلاعات سند قابل بازیابی نمی باشد";
                ShowError(ErrorMessage);
                return false;
            }
            AccountingManager[0].BeginEdit();
            AccountingManager[0]["ResultCode"] = ResultCode;
            AccountingManager[0]["ReferenceId"] = ReferenceId;
            AccountingManager[0]["Time"] = DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":" + DateTime.Now.TimeOfDay.Seconds;// DateTime.Now.TimeOfDay;
            AccountingManager[0]["Number"] = ReferenceId;
            AccountingManager[0]["Status"] = (int)TSP.DataManager.TSAccountingStatus.Payment;
            AccountingManager[0]["Token"] = Token;
            AccountingManager[0]["Date"] = Utility.GetDateOfToday();
            AccountingManager[0]["Description"] = "پرداخت الکترونیکی مبلغ " + Convert.ToDouble(AccountingManager[0]["Amount"]).ToString("#,#") + "ريال" + " بابت " + TSP.DataManager.TechnicalServices.AccTypeManager.FindAccTypeName(AccType)
                                               + "  در تاریخ" + Utility.GetDateOfToday() + " و ساعت" + AccountingManager[0]["Time"] + " توسط " + AccountingManager[0]["FishPayerName"].ToString()
                                               + " انجام گردید"
                                               + "شماره پیگیری: " + AccountingManager[0]["FollowNumber"].ToString() + "کد رهگیری بانکی: " + ReferenceId;
            AccountingManager[0].EndEdit();
            AccountingManager.Save();
            PaymentId = UserId;
            string FishAmount = Convert.ToDouble(AccountingManager[0]["Amount"]).ToString("#,#");

            switch (AccType)
            {
                case (int)TSP.DataManager.TSAccountingAccType.MemberDebpt:
                    //**FishPayerId==MeId==TafziliCode
                    int FishPayerId = Convert.ToInt32(AccountingManager[0]["FishPayerId"]);
                    PayId = TSP.Utility.OnlinePayment.GetPaymentId((TSP.DataManager.TSAccountingAccType)AccType, FishPayerId.ToString(), 1);
                    break;

            }

            if (Utility.ShowExceptionError())
            {
                lblError.Visible = true;
                lblError.Text += "SetpEpayUserControl0:ResualtCode=" + ResultCode + "merchantId" + merchantId + "ReferenceId=" + ReferenceId + "Token=" + Token;
            }

            switch (AccType)
            {          
                case (int)TSP.DataManager.TSAccountingAccType.Registeration_Entrance:
                    #region Membership

                    #endregion
                    break;
                case (int)TSP.DataManager.TSAccountingAccType.PeriodRegister:
                    #region PeriodRegister
                    TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
                    TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
                    TransactionManager.Add(PeriodRegisterManager);
                    TransactionManager.Add(AccountingDetailManager);
                    AccountingDetailManager.FindByAccountingId(AccountingId);
                    // for (int i = 0; i < AccountingDetailManager.Count; i++)
                    if (AccountingDetailManager.Count > 0)
                    {
                        PeriodRegisterManager.FindByCode(Convert.ToInt32(AccountingDetailManager[0]["TableId"]));
                        if (PeriodRegisterManager.Count != 1)
                        {
                            TransactionManager.CancelSave();
                            ShowError("خطایی در ذخیره اطلاعات ایجاد شده است");
                            return false;
                        }
                        PeriodRegisterManager[0]["IsConfirm"] = 1;
                        PeriodRegisterManager.Save();
                    }
                    #endregion
                    break;
                case (int)TSP.DataManager.TSAccountingAccType.SeminarRegister:
                    #region SeminarRegister
                    TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManagerForSeminar = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
                    TSP.DataManager.PeriodRegisterManager PeriodRegisterManagerForSeminar = new TSP.DataManager.PeriodRegisterManager();
                    TransactionManager.Add(PeriodRegisterManagerForSeminar);
                    TransactionManager.Add(AccountingDetailManagerForSeminar);
                    AccountingDetailManagerForSeminar.FindByAccountingId(AccountingId);
                    // for (int i = 0; i < AccountingDetailManagerForSeminar.Count; i++)
                    if (AccountingDetailManagerForSeminar.Count > 0)
                    {
                        PeriodRegisterManagerForSeminar.FindByCode(Convert.ToInt32(AccountingDetailManagerForSeminar[0]["TableId"]));
                        if (PeriodRegisterManagerForSeminar.Count != 1)
                        {
                            TransactionManager.CancelSave();
                            ShowError("خطایی در ذخیره اطلاعات ایجاد شده است");
                            return false;
                        }
                        PeriodRegisterManagerForSeminar[0]["IsConfirm"] = 1;
                        PeriodRegisterManagerForSeminar.Save();
                    }
                    #endregion
                    break;
                case (int)TSP.DataManager.TSAccountingAccType.MemberDebpt:
                    #region MemberDebpt
                    TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager(TransactionManager);
                    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                    TransactionManager.Add(MemberRequestManager);
                    MemberRequestManager.FindByCode(TableId);
                    if (MemberRequestManager.Count != 1)
                    {
                        TransactionManager.CancelSave();
                        ShowError("مقادیر صفحه معتبر نمی باشد");
                        return false;
                    }
                    int MemberId = NmcId = Convert.ToInt32(MemberRequestManager[0]["MeId"]);
                    int MemberUserId = Convert.ToInt32(MemberRequestManager[0]["MemberUserId"]);
                    if (ConfirmedTaskId < 0)
                    {
                        TransactionManager.CancelSave();
                        ShowError("مقادیر صفحه معتبر نمی باشد");
                        return false;
                    }
                    MemberRequestManager.ConfirmedAutomaticRequest(MemberId, TableId, TSP.DataManager.MemberRequestType.ActivateDebtorMember, ConfirmedTaskId, "تایید و پایان بررسی فعالسازی اتوماتیک عضویت شخص حقیقی توسط سیستم"
                        , NmcId, (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId, MemberUserId, MemberManager, MemberRequestManager);
                    if (Utility.ShowExceptionError())
                    {
                        lblError.Visible = true;
                        lblError.Text += "SetpEpayUserControl0-1:AccType=" + (Convert.ToInt32(AccType)).ToString() + "PaymentId=" + PayId + "FishAmount=" + FishAmount + "Name=" + MemberRequestManager[0]["FirstName"].ToString() + "lastname=" + MemberRequestManager[0]["LastName"].ToString();
                    }
                    resultOfflineDebtId = TSP.DataManager.Utility.OfflineDebtAddPayment(MemberId, MemberRequestManager[0]["FirstName"].ToString(), MemberRequestManager[0]["LastName"].ToString(), FishAmount, PayId, ReferenceId);
                    if (resultOfflineDebtId == "-1")
                    {
                        TransactionManager.CancelSave();
                        ShowError("خطا در ثبت سند ایجاد شده است");

                        return false;
                    }

                    if (Utility.ShowExceptionError())
                    {
                        lblError.Visible = true;
                        lblError.Text += "SetpEpayUserControl0-1:AccType=" + (Convert.ToInt32(AccType)).ToString() + "PaymentId=" + PayId + "resultOfflineDebtId" + resultOfflineDebtId + "FishAmount=" + FishAmount + "Name=" + MemberRequestManager[0]["FirstName"].ToString() + "lastname=" + MemberRequestManager[0]["LastName"].ToString();
                    }
                    #endregion
                    break;

            }
            #endregion



            ConfirmTransactionResultCode = BankPaymentsVerification();
            if (ConfirmTransactionResultCode <= 0)
            {
                EPaymentResualtMessage = TSP.DataManager.AccountingEPaymentResultCodeManager.FindConfirmTransactionResualtCodeText(ConfirmTransactionResultCode);
                SetPaymentResualMode(EPaymentResualtMessage);
                if (Utility.ShowExceptionError())
                {
                    lblError.Visible = true;
                    lblError.Text += "SetpEpayUserControl2:ResualtCode=" + ResultCode + "ConfirmTransactionResultCode=" + ConfirmTransactionResultCode + "merchantId" + merchantId + "ReferenceId=" + ReferenceId + "Token=" + Token;
                }
                PaymentSucced = false;
            }
            if (Utility.ShowExceptionError())
            {
                lblError.Visible = true;
                lblError.Text += "SetpEpayUserControl3:ResualtCode=" + ResultCode + "ConfirmTransactionResultCode=" + ConfirmTransactionResultCode + "merchantId" + merchantId + "ReferenceId=" + ReferenceId + "Token=" + Token;
            }
            if (PaymentSucced)
            {
                EPaymentResualtMessage = TSP.DataManager.AccountingEPaymentResultCodeManager.FindResualtCodeText(int.Parse(ResultCode));
                PayedAmount = ConfirmTransactionResultCode;
                SetPaymentResualMode(EPaymentResualtMessage);
                switch (AccType)
                {
                    case (int)TSP.DataManager.TSAccountingAccType.MemberDebpt:
                        resultOfflineDebtId = TSP.DataManager.Utility.OfflineDebtUpdatePayment(resultOfflineDebtId);
                        if (resultOfflineDebtId == "-1")
                        {
                            ShowError("خطا در تایید سند مالی ایجاد شده است.لطفا به واحد فناوری و اطلاعات سازمان مراجعه نمایید.");
                            return false;
                        }
                        break;
                }
                TransactionManager.EndSave();
                string MobileNo = GetPayerMobileNo(_AccType, Convert.ToInt32(AccountingManager[AccountingManager.Count - 1]["TableTypeId"]));
                if (!Utility.IsDBNullOrNullValue(AccountingManager[AccountingManager.Count - 1]["Description"]) && MobileNo != "")
                    SendSMSNotification(Utility.Notifications.NotificationTypes.Epayment, AccountingManager[AccountingManager.Count - 1]["Description"].ToString(), MobileNo);
            }
            else
            {
                PaymentSucced = false;
                TransactionManager.CancelSave();
            }
            FillFishInfo();
        }
        catch (Exception err)
        {
            if (Utility.ShowExceptionError())
            {
                lblError.Visible = true;
                lblError.Text += "SetpEpayUserControlCach:ResualtCode=" + ResultCode + "merchantId" + merchantId + "ReferenceId=" + ReferenceId + "Token=" + Token + "ERR:" + err.Message;
            }
            TransactionManager.CancelSave();
            PaymentSucced = false;
            Utility.SaveWebsiteError(err);
        }
        return PaymentSucced;
    }

    /// <summary>
    /// هنگام بازگشت از صفحه بانک و ارسال پارامترها توسط بانک فراخوانی می شود
    /// </summary>
    /// <returns></returns>
    public Boolean DoNextTaskOfBankReplyParsian(int UserId, int UltId, string Token)
    {
        _PaymentGateWay = (int)TSP.DataManager.PaymentGateWay.Parsian;
        _Token = Token;
        SetPaymentInfo();
        string ErrorMessage = "";
        string PayId = "";
        int NmcId = -1;
        string EPaymentResualtMessage = ""; int PayedAmount = 0; int ConfirmTransactionResultCode = 0;
        Boolean PaymentSucced = true;
        if (AccountingId <= 0)//InvoiceNumber <= 0)
        {
            ErrorMessage = "شناسه سند معتبر نمی باشد";
            if (Utility.ShowExceptionError())
                ErrorMessage += "AccountingId=" + AccountingId.ToString();
            ShowError(ErrorMessage);
            return false;
        }
        if (TableId <= 0)
        {
            ErrorMessage = "شناسه پرونده معتبر نمی باشد";
            if (Utility.ShowExceptionError())
                ErrorMessage += "TableId=" + TableId.ToString();
            ShowError(ErrorMessage);
            return false;
        }
        if (int.Parse(ResultCode) != (int)TSP.DataManager.ParsianPaymentGateway.Successful)
        {
            EPaymentResualtMessage = TSP.Utility.OnlinePayment.GetResultCodeTextForParsian(int.Parse(ResultCode));
            SetPaymentResualMode(EPaymentResualtMessage);
            if (Utility.ShowExceptionError())
            {
                lblError.Visible = true;
                lblError.Text += "SetpEpayUserControl00Parsian:ResualtCode=" + ResultCode + "PINCode" + PINCode + "ConfirmTransactionResultCode =" + ConfirmTransactionResultCode + "CitId" + _CitId.ToString() + "ReferenceId=" + ReferenceId + "Token=" + Token;
            }
            PaymentSucced = false;
            FillFishInfo();
            return PaymentSucced;
        }
        #region Define Manager

        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        #endregion
        TransactionManager.Add(AccountingManager);
        AccountingManager.FindByAccountingId(AccountingId);
        string resultOfflineDebtId = "-1";
        try
        {
            int ConfirmedTaskId = -2;
            TransactionManager.BeginSave();
            #region Save ResualtInfo
            if (AccountingManager.Count != 1)
            {
                if (Utility.ShowExceptionError())
                    ErrorMessage += "AccountingId=" + AccountingId.ToString();
                ErrorMessage = "اطلاعات سند قابل بازیابی نمی باشد";
                ShowError(ErrorMessage);
                return false;
            }
            AccountingManager[0].BeginEdit();
            AccountingManager[0]["ResultCode"] = ResultCode;
            AccountingManager[0]["ReferenceId"] = ReferenceId;
            AccountingManager[0]["Time"] = DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":" + DateTime.Now.TimeOfDay.Seconds;// DateTime.Now.TimeOfDay;
            AccountingManager[0]["Number"] = ReferenceId;
            AccountingManager[0]["Status"] = (int)TSP.DataManager.TSAccountingStatus.Payment;
            AccountingManager[0]["Token"] = Token.ToString();
            AccountingManager[0]["Date"] = Utility.GetDateOfToday();
            AccountingManager[0]["PaymentDate"] = Utility.GetDateOfToday();
            AccountingManager[0]["Description"] = "پرداخت الکترونیکی مبلغ " + Convert.ToDouble(AccountingManager[0]["Amount"]).ToString("#,#") + "ريال" + " بابت " + TSP.DataManager.TechnicalServices.AccTypeManager.FindAccTypeName(AccType)
                                               + "  در تاریخ" + Utility.GetDateOfToday() + " و ساعت" + AccountingManager[0]["Time"] + " توسط " + AccountingManager[0]["FishPayerName"].ToString()
                                               + " انجام گردید"
                                               + "شماره پیگیری: " + AccountingManager[0]["FollowNumber"].ToString() + "شماره مرجع بانکی: " + ReferenceId;
            AccountingManager[0].EndEdit();
            AccountingManager.Save();
            PaymentId = UserId;
            string FishAmount = Convert.ToDouble(AccountingManager[0]["Amount"]).ToString("#,#");


            if (Utility.ShowExceptionError())
            {
                lblError.Visible = true;
                lblError.Text += "SetpEpayUserControl0Parsian:ResualtCode=" + ResultCode + "CitId" + _CitId.ToString() + "ReferenceId=" + ReferenceId + "Token=" + Token.ToString();
            }

            switch (AccType)
            {
                case (int)TSP.DataManager.TSAccountingAccType.Designing5Percent:
                case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure:
                case (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation:
                    #region Designer
                    TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
                    TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
                    TransactionManager.Add(DesignerPlansManager);
                    TransactionManager.Add(PlansManager);
                    DesignerPlansManager.FindByPrjDesignerIdAndPlansId(-1, TableId);
                    if (DesignerPlansManager.Count == 0)
                    {
                        TransactionManager.CancelSave();
                        ShowError("خطایی در ذخیره اطلاعات ایجاد شده است");
                        return false;
                    }
                    int _PlansId = Convert.ToInt32(DesignerPlansManager[0]["PlansId"]);
                    DataTable dtPlan = PlansManager.SelectTSPlansByProjectForEpaymentConfirm(_PlansId, (int)TSP.DataManager.WorkFlowTask.SavePlanInfo);
                    if (dtPlan.Rows.Count > 0)
                    {
                        NmcId = Convert.ToInt32(DesignerPlansManager[0]["MeId"]);
                        UserId = Convert.ToInt32(DesignerPlansManager[0]["MemberUserId"]);
                        UltId = (int)TSP.DataManager.UserType.Member;
                        #region Foundation And CapacityDecreamentCheck
                        int SumDecreament = 0;
                        int ProjectFoundation = Convert.ToInt32(Math.Round(Convert.ToDouble(Convert.ToInt32(DesignerPlansManager[0]["Foundation"]) * Convert.ToInt32(DesignerPlansManager[0]["ProjectDecrementPercent"]) / 100)));
                        DataTable dtDesinger = DesignerPlansManager.SelectTSDesignerPlansByPlanAndAccountingInfo(_PlansId);
                        Boolean IsAllFishedPayed = true;
                        for (int i = 0; i < dtDesinger.Rows.Count; i++)
                        {
                            SumDecreament += Convert.ToInt32(dtDesinger.Rows[i]["CapacityDecrement"]);
                            if (Convert.ToInt32(dtDesinger.Rows[i]["AccStatus"]) != (int)TSP.DataManager.TSAccountingStatus.Payment)
                            {
                                if (Utility.ShowExceptionError())
                                {
                                    lblError.Visible = true;
                                    lblError.Text += "SetpEpayUserControlCachParsian:ResualtCode=" + ResultCode + "merchantId" + merchantId + "ReferenceId=" + ReferenceId + "Token=" + Token + "PrjDesignerId:" + dtDesinger.Rows[i]["PrjDesignerId"].ToString() + "SomeFishNotPayed";
                                }
                                IsAllFishedPayed = false;
                                break;
                            }
                        }

                        if (Utility.ShowExceptionError())
                        {
                            lblError.Visible = true;
                            lblError.Text += "SetpEpayUserControlCachParsian:ResualtCode=" + ResultCode + "merchantId" + merchantId + "ReferenceId=" + ReferenceId + "Token=" + Token + "SumDecreament:" + SumDecreament.ToString() + "ProjectFoundation" + ProjectFoundation.ToString();
                        }
                        if (SumDecreament == ProjectFoundation && IsAllFishedPayed)
                        {

                            if (Utility.ShowExceptionError())
                            {
                                lblError.Visible = true;
                                lblError.Text += "SetpEpayUserControlCachParsian:ResualtCode=" + ResultCode + "merchantId" + merchantId + "ReferenceId=" + ReferenceId + "Token=" + Token + "SumDecreament:" + SumDecreament.ToString() + "ProjectFoundation" + ProjectFoundation.ToString() + "SendToNextWFStep";
                            }
                            int StateId = DesignerPlansManager.DoNextTaskOfBankReply(TableId, UltId, NmcId, (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId, UserId, TransactionManager);
                            if (StateId < 0)
                            {
                                TransactionManager.CancelSave();
                                ShowError("خطایی در ذخیره اطلاعات ایجاد شده است");
                                return false;
                            }

                            if (Utility.ShowExceptionError())
                            {
                                lblError.Visible = true;
                                lblError.Text += "SetpEpayUserControl0-1Parsian:AccType=" + (Convert.ToInt32(AccType)).ToString() + "PaymentId=" + PayId + "resultOfflineDebtId" + resultOfflineDebtId + "FishAmount=" + FishAmount + "DesMeID=" + DesignerPlansManager[0]["MeId"].ToString();
                            }
                        }
                        #endregion
                    }
                    #endregion
                    break;
                case (int)TSP.DataManager.TSAccountingAccType.DocMemberFile:
                    #region DocMemberFile
                    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                    TransactionManager.Add(DocMemberFileManager);
                    DocMemberFileManager.FindByCode(TableId, 0);
                    if (DocMemberFileManager.Count == 0)
                    {
                        TransactionManager.CancelSave();
                        ShowError("خطایی در ذخیره اطلاعات ایجاد شده است");
                        return false;
                    }

                    NmcId = Convert.ToInt32(DocMemberFileManager[0]["MeId"]);
                    UserId = Convert.ToInt32(DocMemberFileManager[0]["MemberUserId"]);
                    UltId = (int)TSP.DataManager.UserType.Member;                  
                    int StateIdDoc = DocMemberFileManager.DoNextTaskOfBankReply(TableId, UltId, NmcId, (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId, UserId, TransactionManager);
                    if (StateIdDoc < 0)
                    {
                        TransactionManager.CancelSave();
                        ShowError("خطایی در ذخیره اطلاعات ایجاد شده است");
                        return false;
                    }


                    DocMemberFileManager[0].BeginEdit();
                    DocMemberFileManager[0]["RegDate"] = Utility.GetDateOfToday();
                    Utility.Date Date = new Utility.Date();
                    int MonthCount = 12;
                    MonthCount = 12 * Utility.GetDefaultPermanentMemberDocExpireDate();
                    string ExpireDate = (Date.AddMonths(MonthCount));

                    DocMemberFileManager[0]["ExpireDate"] = ExpireDate;
                    DocMemberFileManager[0]["CurrentWFStateId"] = StateIdDoc;
                    DocMemberFileManager[0].EndEdit();
                    DocMemberFileManager.Save();
                    #endregion
                    break;
            }
            #endregion

            ConfirmTransactionResultCode = BankPaymentsVerificationParsian(long.Parse(Token));
            if (ConfirmTransactionResultCode != (short)TSP.DataManager.ParsianPaymentGateway.Successful)
            {
                EPaymentResualtMessage = TSP.Utility.OnlinePayment.GetResultCodeTextForParsian(ConfirmTransactionResultCode);
                SetPaymentResualMode(EPaymentResualtMessage);
                if (Utility.ShowExceptionError())
                {
                    lblError.Visible = true;
                    lblError.Text += "SetpEpayUserControl2Parsian:ResualtCode=" + ResultCode + "ConfirmTransactionResultCode=" + ConfirmTransactionResultCode + "merchantId" + merchantId + "ReferenceId=" + ReferenceId + "Token=" + Token.ToString();
                }
                PaymentSucced = false;
            }
            if (Utility.ShowExceptionError())
            {
                lblError.Visible = true;
                lblError.Text += "SetpEpayUserControl3Parsian:ResualtCode=" + ResultCode + "ConfirmTransactionResultCode=" + ConfirmTransactionResultCode + "merchantId" + merchantId + "ReferenceId=" + ReferenceId + "Token=" + Token.ToString();
            }
            if (PaymentSucced)
            {
                EPaymentResualtMessage = TSP.Utility.OnlinePayment.GetResultCodeTextForParsian(int.Parse(ResultCode));
                PayedAmount = ConfirmTransactionResultCode;
                SetPaymentResualMode(EPaymentResualtMessage);

                TransactionManager.EndSave();
                string MobileNo = GetPayerMobileNo(_AccType, Convert.ToInt32(AccountingManager[AccountingManager.Count - 1]["TableTypeId"]));
                if (!Utility.IsDBNullOrNullValue(AccountingManager[AccountingManager.Count - 1]["Description"]) && MobileNo != "")
                    SendSMSNotification(Utility.Notifications.NotificationTypes.Epayment, AccountingManager[AccountingManager.Count - 1]["Description"].ToString(), MobileNo);
            }
            else
            {
                PaymentSucced = false;
                TransactionManager.CancelSave();
            }
            FillFishInfo();
        }
        catch (Exception err)
        {
            if (Utility.ShowExceptionError())
            {
                lblError.Visible = true;
                lblError.Text += "SetpEpayUserControlCachParsian:ResualtCode=" + ResultCode + "merchantId" + merchantId + "ReferenceId=" + ReferenceId + "Token=" + Token + "ERR:" + err.Message;
            }
            TransactionManager.CancelSave();
            PaymentSucced = false;
            Utility.SaveWebsiteError(err);
        }
        return PaymentSucced;
    }
    #region UseFulMethods
    private void ShowError(string Message)
    {
        lblError.Visible = true;
        lblError.Text = Message;
        GridViewAccounting.Visible = false;
        PanelPaymentInfo.Visible = false;
    }

    private void SendSMSNotification(Utility.Notifications.NotificationTypes NotificationType, string AccountingDescription, string SMSMobileNo)
    {
        SendSMSNotification SMSNotifications = new SendSMSNotification(NotificationType);

        String SMSMeId = (Utility.GetCurrentUser_MeId()).ToString(), SMSUltId = (Utility.GetCurrentUser_LoginType()).ToString(), SMSResult = "";

        if (String.IsNullOrWhiteSpace(SMSMobileNo) == false)
        {
            DataRow dr = SMSNotifications.NotificationData.NewRow();
            dr["SMSMobileNo"] = SMSMobileNo;
            dr["SMSResult"] = SMSResult;
            dr["SMSMeId"] = SMSMeId;
            dr["SMSUltId"] = SMSUltId;
            dr["PaymentDescription"] = AccountingDescription;
            SMSNotifications.NotificationData.Rows.Add(dr);
            SMSNotifications.NotificationData.AcceptChanges();

            switch (Utility.GetCurrentSMSWebService())
            {
                case (int)TSP.DataManager.SMSWebServiceType.Magfa:
                    SMSNotifications.SendSMSByMagfa();
                    break;
                case (int)TSP.DataManager.SMSWebServiceType.AsreFaraErtebat:
                    SMSNotifications.SendSMS();
                    break;
            }
        }
    }

    private string GetPayerMobileNo(int AccType, int AccTableTypeId)
    {
        string MobileNo = "";
        switch (AccType)
        {
            case (int)TSP.DataManager.TSAccountingAccType.PeriodRegister:
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                MemberManager.FindByCode(AccTableTypeId);
                if (MemberManager.Count != 1)
                    return "";
                MobileNo = MemberManager[0]["MobileNo"].ToString();
                break;
        }
        return MobileNo;
    }
    #endregion
    #endregion
}