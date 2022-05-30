using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EPayment_DebtPayment : System.Web.UI.Page
{

    int _MeId
    {
        get
        {
            return Convert.ToInt32(HiddenfieldPage["MeId"]);
        }
        set
        {
            HiddenfieldPage["MeId"] = value.ToString();
        }
    }

    int _UserId
    {
        get
        {
            return Convert.ToInt32(HiddenfieldPage["UserId"]);
        }
        set
        {
            HiddenfieldPage["UserId"] = value.ToString();
        }
    }
    int _Amount
    {
        get
        {
            return Convert.ToInt32(HiddenfieldPage["Amount"]);
        }
        set
        {
            HiddenfieldPage["Amount"] = value.ToString();
        }
    }
    string _FullName
    {
        get
        {
            return HiddenfieldPage["FullName"].ToString();
        }
        set
        {
            HiddenfieldPage["FullName"] = value.ToString();
        }
    }

    private int _AccountingId
    {
        set
        {
            HiddenfieldPage["AccountingId"] = value;
        }
        get
        {
            return int.Parse(HiddenfieldPage["AccountingId"].ToString());
        }
    }
    /// <summary>
    /// InvoiceNo
    /// </summary>
    private int _AccDetailId
    {
        set
        {
            HiddenfieldPage["AccDetailId"] = value;
        }
        get
        {
            return int.Parse(HiddenfieldPage["AccDetailId"].ToString());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txtbDbtAmount.ReadOnly = txtUserName.ReadOnly = true;
        if (!IsPostBack)
        {
            //lblMessage.InnerText = "عضویت شما بدلیل بدهی قطع شده است.با پرداخت الکترونیکی بدهی، عضویت شما مجددا فعال می گردد";
            //lblMessage.Visible = true;
            EPaymentUC.Visible = false;
            panelInfoCheck.Visible = true;
            panelPayment.Visible = false;
            if (Request.QueryString["MeId"] == null)
            {
                return;
            }
            _MeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["MeId"]));

            try
            {
                string amount = TSP.DataManager.Utility.CheckMemberOfflineDebt(_MeId);
                _Amount = Convert.ToInt32(amount.Replace(",", ""));
                txtbDbtAmount.Text = amount + "ریال";
            }
            catch (Exception ex)
            {
                txtbDbtAmount.Text = "خطا در ارتباط با وب سرویس پرداخت بدهی";
                Utility.SaveWebsiteError(ex);
            }
            txtUserName.Text = _MeId.ToString();

            TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
            LogManager.FindByMeIdUltId(_MeId, (int)TSP.DataManager.UserType.Member);
            if (LogManager.Count != 1)
            {
                ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است لطفا مجددا تلاش نمایید");
                return;
            }
            _UserId = Convert.ToInt32(LogManager[0]["UserId"]);
            _FullName = LogManager[0]["FullName"].ToString();
            panelInfoCheck.Visible = false;
            panelPayment.Visible = true;
            CheckHasActiveRequest();

        }
    }

    protected void btnPayment_Click(object sender, EventArgs e)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();


        try
        {

            int AccountingId = CheckHasActiveRequest();
            if (AccountingId == -1)
                return;
            else if (AccountingId > 0)
            {
                #region Insert New AccDetail to generate New InvoiceId
                TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
                TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
                AccountingManager.FindByAccountingId(AccountingId);
                if (AccountingManager.Count == 0)
                {
                    ShowMessage("خطایی در ثبت درخواست ایجاد شده است.");
                    return;
                }
                AccountingManager[0].BeginEdit();
                string Description = "سند پرداخت الکترونیکی مبلغ " + Convert.ToDecimal(AccountingManager[0]["Amount"]).ToString("#,#") + "ريال" + " بابت " + TSP.DataManager.TechnicalServices.AccTypeManager.FindAccTypeName((int)TSP.DataManager.MembershipRegistrationStatus.CancelDebtorMember) + "  در تاریخ" + Utility.GetDateOfToday() + " توسط " + _FullName + " ثبت گردید";
                AccountingManager[0]["Description"] = Description;
                AccountingManager[0]["Date"] = Utility.GetDateOfToday();
                AccountingManager[0]["Time"] = DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":" + DateTime.Now.TimeOfDay.Seconds;// DateTime.Today.TimeOfDay.ToString();
                AccountingManager[0].EndEdit();
                AccountingManager.Save();
                AccountingDetailManager.FindByAccountingId(AccountingId);
                if (AccountingDetailManager.Count == 0)
                {
                    ShowMessage("خطایی در ثبت درخواست ایجاد شده است.");
                    return;
                }
                System.Data.DataRow dr = AccountingDetailManager.NewRow();
                dr["AccountingId"] = AccountingId;
                dr["TableId"] = Convert.ToInt32(AccountingDetailManager[0]["TableId"]);
                dr["TableType"] = Convert.ToInt32(AccountingDetailManager[0]["TableType"]);
                dr["Amount"] = AccountingDetailManager[0]["Amount"];
                dr["Description"] = Description;
                dr["UserId"] = _UserId;
                dr["InActive"] = 0;
                dr["ModifedDate"] = DateTime.Now;
                AccountingDetailManager.AddRow(dr);
                AccountingDetailManager.Save();
                AccountingDetailManager.DataTable.AcceptChanges();

                EPaymentUC.InvoiceNumber = Convert.ToInt32(AccountingDetailManager[0]["AccDetailId"]);
                #endregion
            }
            else
            {
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager(TransactionManager);
                TSP.DataManager.TransferMemberManager transferManager = new TSP.DataManager.TransferMemberManager();
                TransactionManager.Add(transferManager);
                TransactionManager.Add(MemberManager);
                TransactionManager.Add(MemberRequestManager);
                #region  Insert New Request
                TransactionManager.BeginSave();
                EPaymentUC.Amount = _Amount;
                EPaymentUC.FullNamePayer = _FullName;
                EPaymentUC.SetEPaymentParameters((int)TSP.DataManager.TSAccountingAccType.MemberDebpt
                                      , TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Member)
                                      , "EPayment", -1, "-1", "-1", "");
                int MReId = -2;

                MemberManager.FindByCode(_MeId);
                if (MemberManager.Count == 0)
                {
                    TransactionManager.CancelSave();
                    ShowMessage("خطایی در ثبت درخواست ایجاد شده است.");
                    TransactionManager.CancelSave();
                    return;
                }
                MemberRequestManager.DoAutomaticConfirmChangeMemberData(_MeId, TSP.DataManager.MemberRequestType.ActivateDebtorMember, TSP.DataManager.MembershipRequest.Member
                , "آغاز گردش کار اتوماتیک سیستم جهت فعالسازی عضویت شخص حقیقی", ""
                           , _UserId, MemberManager, new TSP.DataManager.TransferMemberManager(), false, ref MReId);
                if (MReId == -2)
                {
                    TransactionManager.CancelSave();
                    ShowMessage("خطایی در ثبت درخواست ایجاد شده است.");
                    TransactionManager.CancelSave();
                    return;
                }
                if (EPaymentUC.SaveFish(TransactionManager, MReId, _UserId, TSP.DataManager.EpaymentType.EpaymentForAllSite, _MeId.ToString()) <= 0)
                {
                    TransactionManager.CancelSave();
                    ShowMessage("خطایی در ثبت درخواست ایجاد شده است.");
                    TransactionManager.CancelSave();
                    return;
                }
                TransactionManager.EndSave();
                #endregion
            }
            Response.Redirect("Epayment.aspx?InvoiceNo=" + Utility.EncryptQS(EPaymentUC.InvoiceNumber.ToString()) + "&TMeId=" + Utility.EncryptQS(_MeId.ToString()));
        }
        catch (Exception ex)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(ex);
            ShowMessage("خطایی در ثبت درخواست ایجاد شده است.");
        }
    }
    protected void btnCheckDebpt_Click(object sender, EventArgs e)
    {
        if (Utility.IsDBNullOrNullValue(txtMeId.Text))
        {
            ShowMessage("کد عضویت را وارد نمایید");
            return;
        }
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(Convert.ToInt32(txtMeId.Text));
        if (MemberManager.Count == 0)
        {
            ShowMessage("کد عضویت وارد شده معتبر نمی باشد");
            return;
        }
        if (Utility.IsDBNullOrNullValue(MemberManager[0]["Idno"]) || MemberManager[0]["Idno"].ToString() != txtIdno.Text)
        {
            ShowMessage("شماره شناسنامه وارد شده نامعتبر می باشد");
            return;
        }
        if (Convert.ToInt32(MemberManager[0]["MrsId"]) != (int)TSP.DataManager.MembershipRegistrationStatus.CancelDebtorMember)
        {
            ShowMessage("وضعیت عضویت شما قطع عضویت به دلیل بدهی نمی باشد");
            return;
        }
        panelInfoCheck.Visible = false;
        panelPayment.Visible = true;
        _MeId = Convert.ToInt32(txtMeId.Text);

        try
        {
            string amount = TSP.DataManager.Utility.CheckMemberOfflineDebt(_MeId);
            _Amount = Convert.ToInt32(amount.Replace(",", ""));
            txtbDbtAmount.Text = amount + "ریال";
        }
        catch (Exception ex)
        {
            txtbDbtAmount.Text = "خطا در ارتباط با وب سرویس پرداخت بدهی";
            Utility.SaveWebsiteError(ex);
        }
        txtUserName.Text = _MeId.ToString();

        TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
        LogManager.FindByMeIdUltId(_MeId, (int)TSP.DataManager.UserType.Member);
        if (LogManager.Count != 1)
        {
            ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است لطفا مجددا تلاش نمایید");
            return;
        }
        _UserId = Convert.ToInt32(LogManager[0]["UserId"]);
        _FullName = LogManager[0]["FullName"].ToString();
    }
    private void ShowMessage(string Message)
    {
        lblError.Text = Message;
        lblError.Visible = true;
        panelPayment.Visible = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>0:درخواست  درگردش ندارد
    /// -1 : خطا
    /// >0 AcoountingId</returns>
    private int CheckHasActiveRequest()
    {

        int Result = 0;
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
        MemberRequestManager.FindLastReqByMemberId(_MeId, 0, 0);
        if (MemberRequestManager.Count == 1)
        {
            TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
            AccountingDetailManager.FindByAccountingTableId(Convert.ToInt32(MemberRequestManager[0]["MReId"]), (int)TSP.DataManager.TSAccountingAccType.MemberDebpt);
            if (AccountingDetailManager.Count == 0)
            {
                ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است لطفا مجددا تلاش نمایید");
                return -1;
            }
            return Convert.ToInt32(AccountingDetailManager[0]["AccountingId"]);
        }
        return Result;
    }
}