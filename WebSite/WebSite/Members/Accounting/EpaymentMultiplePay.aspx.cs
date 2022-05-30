using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Members_Accounting_EpaymentMultiplePay : System.Web.UI.Page
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
        SetKey();
        if (!IsPostBack)
        {
            // SetMode();
            FillAccInfo();
        }
        if (Utility.ShowExceptionError())
        {
            lblError.Visible = true;
            lblError.Text = "step:3";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["MeEPayPeriodRegister"] = null;
        Response.Redirect("../MemberHome.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        InsertAndPayment();
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        switch (MultiplePaymentType)
        {
            case "PeriodRegister":
                Session["MeEPayPeriodRegister"] = null;
                Response.Redirect("../Amoozesh/PeriodRegister.aspx");
                break;
            case "SeminarRegister":
                Response.Redirect("../Amoozesh/SeminarRegister.aspx");
                break;
        }
    }

    #endregion

    #region Methods
    protected void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetKey()
    {       
        if (string.IsNullOrEmpty(Request.QueryString["MPt"]))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        MultiplePaymentType = Utility.DecryptQS(Request.QueryString["MPt"]);
        PageMode = "MultipleEPayment";
        EPaymentUC.Amount = SumMutipleCost = FindPaymentCost();

        if (Utility.ShowExceptionError())
        {
            lblError.Visible = true;
            lblError.Text = "step:0paymentId=" + Request.Form["paymentId"]
                            + "resultCode=" + Request.Form["resultCode"]
                            + "referenceId=" + Request.Form["referenceId"] + "AccType:" + EPaymentUC.AccType.ToString();
        }
        int AccType = -2;
        switch (MultiplePaymentType)
        {

            case "PeriodRegister":
                AccType = (int)TSP.DataManager.TSAccountingAccType.PeriodRegister;
                break;
            case "SeminarRegister":
                AccType = (int)TSP.DataManager.TSAccountingAccType.SeminarRegister;
                break;
        }
        EPaymentUC.SetEPaymentParameters(AccType,
                                      TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister)
                                       , PageMode, Request.Form["paymentId"] != null ? Convert.ToInt32(Request.Form["paymentId"]) : -1
                                       , Request.Form["resultCode"] != null ? Request.Form["resultCode"] : "-1"
                                       , Request.Form["referenceId"] != null ? Request.Form["referenceId"] : "-1", Request.Form["Token"] != null ? Request.Form["Token"].ToString() : "");
        if (Utility.ShowExceptionError())
        {
            lblError.Visible = true;
            lblError.Text = "step:1paymentId=" + Request.Form["paymentId"]
                            + "resultCode=" + Request.Form["resultCode"]
                            + "referenceId=" + Request.Form["referenceId"];
        }
    }

    #region FillInfo
    private void FillAccInfo()
    {
        switch (MultiplePaymentType)
        {
            case "PeriodRegister":
                if (Session["MeEPayPeriodRegister"] == null) return;
                AddPeriodRegisterFish();
                break;
            case "SeminarRegister":
                if (Utility.IsDBNullOrNullValue(Request.QueryString["SeId"])) return;
                AddSeminarFish();
                break;
        }
    }

    private void AddPeriodRegisterFish()
    {
        double SumAmount = 0;
        DataTable dtPeriodRegister = (DataTable)Session["MeEPayPeriodRegister"];
        for (int i = 0; i < dtPeriodRegister.Rows.Count; i++)
        {
            int PPId = Convert.ToInt32(dtPeriodRegister.Rows[i]["PPId"]);
            string CrsName = dtPeriodRegister.Rows[i]["CrsName"].ToString();
            string PPCode = dtPeriodRegister.Rows[i]["PPCode"].ToString();
            double Amount = Convert.ToDouble(dtPeriodRegister.Rows[i]["RegCost"]);
            SumAmount += Convert.ToDouble(dtPeriodRegister.Rows[i]["RegCost"]);
            EPaymentUC.AddAccountingRow(PPId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister), CrsName + "-" + PPCode, (int)Amount);
        }
        EPaymentUC.Amount = SumMutipleCost = (int)SumAmount;
    }

    private void AddSeminarFish()
    {
        double SumAmount = 0;
        int SeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["SeId"]));
        TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();
        SeminarManager.FindByCode(SeId);
        if (SeminarManager.Count != 1)
            return;
        SumAmount = Convert.ToInt32(SeminarManager[0]["SeminarCost"]);
        EPaymentUC.AddAccountingRow(SeId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodRegister), SeminarManager[0]["Subject"].ToString(), (int)SumAmount);
        EPaymentUC.Amount = SumMutipleCost = (int)SumAmount;
    }
    #endregion

    #region FindPaymentCost
    private int FindPaymentCost()
    {

        int Cost = -1;
        switch (MultiplePaymentType)
        {
            case "PeriodRegister":
                if (Session["MeEPayPeriodRegister"] == null) return -1;
                double SumAmount = 0;
                DataTable dtPeriodRegister = (DataTable)Session["MeEPayPeriodRegister"];
                for (int i = 0; i < dtPeriodRegister.Rows.Count; i++)
                {
                    SumAmount += Convert.ToDouble(dtPeriodRegister.Rows[i]["RegCost"]);
                }
                Cost = EPaymentUC.Amount = SumMutipleCost = (int)SumAmount;
                break;
            case "SeminarRegister":
                if (Utility.IsDBNullOrNullValue(Request.QueryString["SeId"])) return -1;
                int SeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["SeId"]));
                TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();
                SeminarManager.FindByCode(SeId);
                if (SeminarManager.Count != 1)
                    return -1;
                Cost = Convert.ToInt32(SeminarManager[0]["SeminarCost"]);
                break;
        }
        return Cost;
    }

    #endregion

    #region Insert
    private void InsertAndPayment()
    {
        switch (MultiplePaymentType)
        {
            case "PeriodRegister":
                InsertPeriodRegister();
                break;
            case "SeminarRegister":
                InsertPeriodRegisterForSeminar();
                break;
        }
    }

    private void InsertPeriodRegister()
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        try
        {
            if (IsPageExpired())
                return;
            DataTable dtPeriodRegister = (DataTable)Session["MeEPayPeriodRegister"];
            TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
            TransactionManager.Add(PeriodRegisterManager);
            TransactionManager.BeginSave();            
            int RegCost = 0;
            for (int i = 0; i < dtPeriodRegister.Rows.Count; i++)
            {
                int PrePRId = Convert.ToInt32(dtPeriodRegister.Rows[i]["PRId"]);
                if (PrePRId == -1)
                {
                    RegCost = Convert.ToInt32(dtPeriodRegister.Rows[i]["RegCost"]);
                    DataRow dr = PeriodRegisterManager.NewRow();
                    dr["PPId"] = dtPeriodRegister.Rows[i]["PPId"];
                    dr["IsSeminar"] = dtPeriodRegister.Rows[i]["IsSeminar"];
                    dr["MeId"] = Utility.GetCurrentUser_MeId();
                    dr["RegisterType"] = dtPeriodRegister.Rows[i]["RegisterType"];
                    dr["PaymentType"] = dtPeriodRegister.Rows[i]["PaymentType"];
                    dr["IsMember"] = 1;
                    dr["RegisterDate"] = Utility.GetDateOfToday();
                    //dr["SerialNo"] = dtPeriodRegister.Rows[i][""];
                    dr["InActive"] = 0;
                    dr["IsConfirm"] = 0;
                    dr["IsPassed"] = 0;
                    dr["UserId"] = Utility.GetCurrentUser_UserId();
                    dr["ModifiedDate"] = DateTime.Now;
                    PeriodRegisterManager.AddRow(dr);
                    PeriodRegisterManager.Save();
                    PeriodRegisterManager.DataTable.AcceptChanges();
                    int PRId = Convert.ToInt32(PeriodRegisterManager[0]["PRId"]);
                    int RegisterPaymentId = Convert.ToInt32(PeriodRegisterManager[0]["RegisterPaymentId"]);
                    string TafziliCode = TSP.DataManager.PeriodRegisterManager.GetPeriodREgisterTafziliCode(Utility.GetCurrentUser_MeId(), RegisterPaymentId);
                    if (EPaymentUC.SaveFish(TransactionManager, Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_UserId(), TSP.DataManager.EpaymentType.MemberMultiplePayment, TafziliCode, PRId) <= 0)
                    {
                        ShowMessage("خطایی در ذخیره انجام گرفته است.");
                        TransactionManager.CancelSave();
                        return;
                    }
                  
                }                
            }
            TransactionManager.EndSave();
            Session["MeEPayPeriodRegister"] = null;
            Response.Redirect("~/EPayment/Epayment.aspx?InvoiceNo=" + Utility.EncryptQS(EPaymentUC.InvoiceNumber.ToString()));
        }
        catch (Exception ex)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(ex);
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }

    private void InsertPeriodRegisterForSeminar()
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        try
        {
            if (IsPageExpired())
                return;
            TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
            TransactionManager.Add(PeriodRegisterManager);
            TransactionManager.BeginSave();
            int RegCost = 0;
            int SeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["SeId"]));
            if (CheckIfRepititiveRegister(SeId))
            {
                ShowMessage("شما قبلا در این سمینار ثبت نام کرده اید.در صورت عدم پرداخت از منوی مدیریت مالی-فیش های پرداخت نشده اقدام فرمایید.");
                return;
            }
            TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();
            SeminarManager.FindByCode(SeId);
            if (SeminarManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                TransactionManager.CancelSave();
                return;
            }
            int AccountingId = -2;
            EPaymentUC.Amount = RegCost = Convert.ToInt32(SeminarManager[0]["SeminarCost"]);

            DataRow dr = PeriodRegisterManager.NewRow();
            dr["PPId"] = SeId;
            dr["IsSeminar"] = 1;
            dr["MeId"] = Utility.GetCurrentUser_MeId();
            dr["RegisterType"] = (int)TSP.DataManager.PeriodRegisterType.PeriodAndExam;
            dr["PaymentType"] = (int)TSP.DataManager.PeriodRegisterPaymentType.EPayment;
            dr["IsMember"] = 1;
            dr["RegisterDate"] = Utility.GetDateOfToday();
            dr["InActive"] = 0;
            if (RegCost != 0)
                dr["IsConfirm"] = 0;
            else
                dr["IsConfirm"] = 1;
            dr["IsPassed"] = 0;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            PeriodRegisterManager.AddRow(dr);
            PeriodRegisterManager.Save();
            PeriodRegisterManager.DataTable.AcceptChanges();
            int PRId = Convert.ToInt32(PeriodRegisterManager[0]["PRId"]);
            int RegisterPaymentId = Convert.ToInt32(PeriodRegisterManager[0]["RegisterPaymentId"]);
            if (RegCost != 0)
            {
                string TafziliCode = TSP.DataManager.PeriodRegisterManager.GetPeriodREgisterTafziliCode(Utility.GetCurrentUser_MeId(), RegisterPaymentId);
                if (EPaymentUC.SaveFish(TransactionManager, Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_UserId(), TSP.DataManager.EpaymentType.MemberMultiplePayment, TafziliCode, PRId) <= 0)
                {
                    ShowMessage("خطایی در ذخیره انجام گرفته است.");
                    TransactionManager.CancelSave();
                    return;
                }   
                TransactionManager.EndSave();
                Response.Redirect("~/EPayment/Epayment.aspx?InvoiceNo=" + Utility.EncryptQS(EPaymentUC.InvoiceNumber.ToString()));
            }
            TransactionManager.EndSave();
            Session["MeEPayPeriodRegister"] = null;
            if (RegCost != 0)
                Response.Redirect(EPaymentUC.BankURL);
            else
            {
                btnSave.Visible = btnCancel.Visible = btnPre.Visible = false;
                ShowMessage("عملیات با موفقیت انجام شد.");
            }
        }
        catch (Exception ex)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(ex);
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
        }
    }
    #endregion


    private Boolean CheckIfRepititiveRegister(int SeId)
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        DataTable dtRegister = PeriodRegisterManager.SelectPeriodRegisterForSeminar(-1, SeId, Utility.GetCurrentUser_MeId());
        //int count = dtRegister.Rows.Count;
        if (dtRegister.Rows.Count == 0)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < dtRegister.Rows.Count; i++)
            {
                if (!Convert.ToBoolean(dtRegister.Rows[i]["InActive"]))
                {
                    ShowMessage("شما قبلا در این سمینار ثبت نام کرده اید.");
                    return true;
                }
            }
            return false;
        }
    }


    private Boolean IsPageExpired()
    {
        if (PageMode != "BankReply")
            return false;
        Boolean IsExpired = false;
        switch (MultiplePaymentType)
        {
            case "PeriodRegister":
                if (Session["MeEPayPeriodRegister"] == null)
                    IsExpired = true;
                break;
        }
        if (IsExpired)
            ShowMessage("مدت زمان اعتبار صفحه به پایان رسیده است ");
        return IsExpired;
    }
    #endregion
}