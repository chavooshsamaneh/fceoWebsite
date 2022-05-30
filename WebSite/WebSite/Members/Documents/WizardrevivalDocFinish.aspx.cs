using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Members_Documents_WizardrevivalDocFinish : System.Web.UI.Page
{
    DataTable dtMemberFileMajor = null;
    DataTable dtExams = null;
    DataTable dtDocResponsblity = null;
    DataTable dtDocJob = null;
    DataTable dtDocJobConfirm = null;
    private System.Collections.ArrayList ArrayListJobConfirm;

    private string _PageMode = "";
    private string PageMode
    {
        get
        {
            return HiddenFieldDocMemberFile["PageMode"].ToString();
        }
        set
        {
            HiddenFieldDocMemberFile["PageMode"] = value;
        }
    }

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        SetWarningLableDisable();
        SetKey();
        if (!IsPostBack)
        {
            if (CheckTsTimeOut())
            {
                btnSave.Enabled = false;
            }
            SetMenueImage();
        }

        btnPrint.Visible = btnPrePrint.Visible = false;
    }

    protected void btnDocMemberFile_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberFiles.aspx");
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardrevivalDocSummary.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckTsTimeOut())
            return;
        if (!CheckConditions())
            return;
        InsertAndPay();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetDocSessions();
        Response.Redirect("../MemberHome.aspx");
    }

    #endregion

    #region Methods
    private void SendSms(DataRow dr)
    {
        string SMSMeId = dr["MeId"].ToString();
        int MeId = 0;
        int.TryParse(SMSMeId, out MeId);
        string DocDescription = "";

        TSP.DataManager.MemberManager member = new TSP.DataManager.MemberManager();
        member.FindByCode(MeId);
        string SMSMobileNo = member[0]["MobileNo"].ToString();
        string name = member[0]["FirstName"].ToString() + " " + member[0]["LastName"].ToString();
        DocDescription = "آقای/خانم " + name + "\n"
            + Utility.GetCurrentUser_FullName() + " " + "عضو محترم سازمان نظام مهندسی استان فارس؛ متقاضی تمدید پروانه اشتغال به کار می باشند و جنابعالی را به عنوان تایید کننده سوابق کاری خود معرفی نموده اند. در صورت هرگونه اعتراض به واحد پروانه اشتغال سازمان تماس حاصل فرمایید";
        if (Convert.ToInt32(dr["DocExpired"]) == 1)
        {
            DocDescription += "ضمنا تاریخ اعتبار پروانه اشتغال به کار شما به پایان رسیده است.درصورتی که تا این لحضه جهت تمدید آن اقدام ننموده اید، اقدامات لازم را انجام نمایید.";
        }
        SendSMSNotification(Utility.Notifications.NotificationTypes.GetDocFile, DocDescription, SMSMobileNo, SMSMeId);
    }

    private void SendSMSNotification(Utility.Notifications.NotificationTypes NotificationType, string Description, string SMSMobileNo, string SMSMeId)
    {

        if (String.IsNullOrWhiteSpace(SMSMobileNo) == false)
        {

            DataTable dtRecivers = new DataTable();
            dtRecivers.Columns.Add("SMSMobileNo");
            dtRecivers.Columns.Add("SMSMeId");
            dtRecivers.Columns.Add("SMSUltId");
            dtRecivers.Columns.Add("Description");

            DataRow dr = dtRecivers.NewRow();
            dr["SMSMobileNo"] = SMSMobileNo;
            dr["SMSMeId"] = SMSMeId;
            dr["SMSUltId"] = ((int)TSP.DataManager.UserType.Member).ToString();
            dr["Description"] = Description;

            dtRecivers.Rows.Add(dr);
            dtRecivers.AcceptChanges();

            //send sms contain TempPass
            SendSMSNotification SMSNotifications = new SendSMSNotification(NotificationType);
            SMSNotifications.SendSMSNote(dtRecivers, Description);
        }
    }

    private void SetWarningLableDisable()
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetMenueImage()
    {
        if (Session["WizardDocRevivalOath"] != null && (Boolean)Session["WizardDocRevivalOath"] == true)
        {
            MenuSteps.Items.FindByName("Oath").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Oath").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Oath").Image.Height = Unit.Pixel(15);
        }
       
        if (Session["WizardRevivalJobConfirm"] != null && ((DataTable)Session["WizardRevivalJobConfirm"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("JobConfirm").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("JobConfirm").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("JobConfirm").Image.Height = Unit.Pixel(15);
        }

        if (Session["WizardDocRevivalSummary"] != null && (Boolean)Session["WizardDocRevivalSummary"] == true)
        {
            MenuSteps.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }

        if ( Session["WizardRevivalImgfrontDoc"] != null || Session["WizardRevivalImgTaxOfficeLetter"] != null)
        {
            MenuSteps.Items.FindByName("Kardan").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Kardan").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Kardan").Image.Height = Unit.Pixel(15);
        }
    }

    private Boolean CheckTsTimeOut()
    {
        if ( Session["WizardDocRevivalSummary"] == null && Session["WizardDocRevivalOath"] == null
         && Session["WizardRevivalJobConfirm"] == null
                 )
        {
            ShowMessage("مدت زمان اعتبار صفحه به پایان رسیده است");
            return true;
        }

        if (Session["WizardDocRevivalOath"] == null)
        {
            ShowMessage("مدارک لازم مطالعه نشده است و تعهدات را تایید ننموده اید");
            return true;
        }
        return false;
    }

    private void ResetDocSessions()
    {
        Session["WizardDocRevivalSummary"] =
       Session["WizardDocRevivalOath"] =
       Session["WizardRevivalJobConfirm"] =
       Session["DocRevivalJobFileURL"] =
       Session["DocRevivalJobGrdURL"] =
       Session["WizardRevivalImgTaxOfficeLetter"] =
       Session["WizardRevivalImgfrontDoc"] =
       Session["WizardRevivalImgBackDoc"] =
       Session["ImgPeriodImage"] =
       Session["HseFileURL"]=
       //Session["ImgTaxOfficeLetter"] =
       Session["WizardRevivalCivilLicence"] = null;
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                ShowMessage("اطلاعات تکراری می باشد");
            }
            else
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void SetKey()
    {
        //if (!string.IsNullOrEmpty(Request.QueryString["QS"]))
        //{
        //    SetBankReplySetKey();
        //}
        //else
        //{
        //    SetOtherKey();
        //}
        if (string.IsNullOrEmpty(Request.QueryString["PgMode"]))
        {            
            ShowMessage("PgMode=Null");
            return;
        }
        PageMode = Utility.DecryptQS(Request.QueryString["PgMode"]);

        EPaymentUC.SetEPaymentParameters((int)TSP.DataManager.TSAccountingAccType.DocMemberFile
                                      , TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile)
                                      , PageMode, Request.Form["paymentId"] != null ? Convert.ToInt32(Request.Form["paymentId"]) : -1
                                      , Request.Form["resultCode"] != null ? Request.Form["resultCode"] : "-1"
                                      , Request.Form["referenceId"] != null ? Request.Form["referenceId"] : "-1", Request.Form["token"] != null ? Request.Form["token"] : "");
        //EPaymentUC.ResultCode = "100";
        //EPaymentUC.PaymentId = 98171;
        //EPaymentUC.ReferenceId = "000000122802";
    }

    //private void SetBankReplySetKey()
    //{
    //    string Qs = Utility.DecryptQS(Request.QueryString["QS"]);
    //    string[] ArrayQS = Qs.Split(';');
    //    PageMode = ArrayQS[0];
    //    EPaymentUC.AccType = int.Parse(ArrayQS[1].ToString());
    //    EPaymentUC.TableId = int.Parse(ArrayQS[2].ToString());
    //}

    //private void SetOtherKey()
    //{
    //    if (string.IsNullOrEmpty(Request.QueryString["PgMode"]))
    //    {
    //        // this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
    //        ShowMessage("PgMode=Null");
    //        return;
    //    }
    //    PageMode = Utility.DecryptQS(Request.QueryString["PgMode"]);
    //}

    //private void SetPageMode()
    //{

    //    switch (PageMode)
    //    {
    //        case "BankReply":
    //            SetBankReplyMode();
    //            break;
    //        case "EPayment":
    //            SetEPaymentMode();
    //            break;
    //    }
    //}

    //private void SetEPaymentMode()
    //{
    //    if (CheckTsTimeOut())
    //    {
    //        btnSave.Enabled = false;
    //    }
    //    SetMenueImage();
    //}

    //private void SetBankReplyMode()
    //{
    //    #region SetMenueIamge
    //    SetMenueImage();
    //    #endregion
    //    if (!EPaymentUC.DoNextTaskOfBankReply(Utility.GetCurrentUser_UserId(), Utility.GetCurrentUser_LoginType(),EPaymentUC.Token))
    //    {
    //        btnSave.Visible = false;
    //        btnPre.Visible = false;
    //        btnCancel.Visible = false;
    //        btnPrint.Visible = false;
    //        btnDocMemberFile.Visible = false;
    //        btnPrePrint.Visible = false;
    //        //BankHelp2.Visible = false;
    //        // BankHelp1.Visible = false;
    //        return;
    //    }

    //    btnSave.Visible = false;
    //    btnPre.Visible = false;
    //    btnCancel.Visible = false;
    //    btnPrint.Visible = true;
    //    btnDocMemberFile.Visible = true;
    //    btnPrePrint.Visible = false;

    //    int MFId = EPaymentUC.TableId;
    //    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
    //    DocMemberFileManager.FindByCode(MFId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
    //    if (DocMemberFileManager.Count != 1)
    //    {
    //        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
    //        return;
    //    }
    //    HiddenFieldDocMemberFile["PrintdocMeFile"] = "../../ReportForms/DocMemberFileReport.aspx?MeId="
    //                        + Utility.EncryptQS(DocMemberFileManager[0]["MeId"].ToString()) + "&MFId=" + Utility.EncryptQS(MFId.ToString());
    //    HiddenFieldDocMemberFile["PrePrintdocMeFile"] = "../../ReportForms/ReportMemberFilePrePrint.aspx?MeId=" + Utility.EncryptQS(DocMemberFileManager[0]["MeId"].ToString());
    //}

    #region Insert
    private void InsertAndPay()
    {
        #region Define Managers
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager = new TSP.DataManager.DocMemberFileJobConfirmationManager();

        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(DocMemberFileJobConfirmationManager);

        #endregion

        try
        {
            Boolean IsJobNessecery = true;
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            DataTable dtRes = DocMemberFileDetailManager.SelectMeFileDetailMaxGrad(-1, Utility.GetCurrentUser_MeId(), 0);

            if (dtRes.Rows.Count > 0)
            {
                dtRes.DefaultView.RowFilter = "MaxGradeId=" + ((int)TSP.DataManager.DocumentGrads.Grade1).ToString();
                if (dtRes.DefaultView.Count > 0)
                    IsJobNessecery = false;
            }

            
            TransactionManager.BeginSave();
            int MeId = Utility.GetCurrentUser_MeId();

            if (!InsertMeDocRequest(DocMemberFileManager,ref IsJobNessecery))
            {
                TransactionManager.CancelSave();
                return;
            }
            int MFId = Convert.ToInt32(DocMemberFileManager[DocMemberFileManager.Count - 1]["MFId"]);
            if (IsJobNessecery)
            {
                if (!InsertJobConfirm(MFId, DocMemberFileJobConfirmationManager))
                {
                    TransactionManager.CancelSave();
                    return;
                }
            }

            if (!InsertWorkFlow(MFId, DocMemberFileManager, WorkFlowTaskManager, WorkFlowStateManager))
            {
                TransactionManager.CancelSave();
                return;
            }
            if (EPaymentUC.SaveFish(TransactionManager, MFId, Utility.GetCurrentUser_UserId(), TSP.DataManager.EpaymentType.WizardNewMemberDoc) <= 0)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                TransactionManager.CancelSave();
                return;
            }
            TransactionManager.EndSave();
            try
            {
                for (int i = 0; i < ArrayListJobConfirm.Count; i++)
                {
                    SendSms((DataRow)ArrayListJobConfirm[i]);
                }

            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
            }
            ResetDocSessions();
            Response.Redirect("~/EPayment/EpaymentParsian.aspx?InvoiceNo=" + Utility.EncryptQS(EPaymentUC.InvoiceNumber.ToString()) + "&Cit=" + Utility.EncryptQS("-2"), false);
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
            return;
        }

    }

    private Boolean InsertMeDocRequest(TSP.DataManager.DocMemberFileManager DocMemberFileManager,ref Boolean IsJobNessecery)
    {
        DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(Utility.GetCurrentUser_MeId(), 0, 1);
        if (dtDocMeFile.Rows.Count <= 0)
        {
            ShowMessage("بدلیل نامشخص بودن وضعیت پروانه امکان ثبت درخواست جدید وجود ندارد.");
            return false;
        }
        if (Convert.ToBoolean(dtDocMeFile.Rows[0]["IsTemporary"]))
            IsJobNessecery = false;
        int MfId = (int)dtDocMeFile.Rows[0]["MfId"];
        #region DocMe Insert
        DataRow MeFileRow = DocMemberFileManager.NewRow();
        MeFileRow["RequesterType"] = (int)TSP.DataManager.DocumentRequesterType.Member;
        MeFileRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.MemberDocument);
        MeFileRow["MeId"] = Utility.GetCurrentUser_MeId();

        if (Session["WizardRevivalImgTaxOfficeLetter"] != null)
        {
            MeFileRow["TaxOfficeLetterURL"] = Session["WizardRevivalImgTaxOfficeLetter"].ToString();
        }

        if (Session["WizardRevivalImgfrontDoc"] != null)
        {
            MeFileRow["ImgOldDocFrontURL"] = Session["WizardRevivalImgfrontDoc"].ToString();
        }

        if (Session["WizardRevivalImgBackDoc"] != null)
        {
            MeFileRow["ImgOldDocBackURL"] = Session["WizardRevivalImgBackDoc"].ToString();
        }

        if (Session["ImgPeriodImage"] != null)
        {
            MeFileRow["PeriodImageURL"] = Session["ImgPeriodImage"].ToString();
        }
        if (Session["HseFileURL"] != null)
        {
            MeFileRow["ImgHSEURL"] = Session["HseFileURL"].ToString();
        }

        if (!Utility.IsDBNullOrNullValue(dtDocMeFile.Rows[0]["MasterMfMjId"]))
            MeFileRow["MasterMfMjId"] = dtDocMeFile.Rows[0]["MasterMfMjId"].ToString();

        if (!Utility.IsDBNullOrNullValue(dtDocMeFile.Rows[0]["MFSerialNo"]))
            MeFileRow["MFSerialNo"] = dtDocMeFile.Rows[0]["MFSerialNo"].ToString();
        MeFileRow["RegDate"] = Utility.GetDateOfToday();
        MeFileRow["ExpireDate"] = GetMeDocDefualtRegisterDate();
        MeFileRow["Type"] = (int)TSP.DataManager.DocumentOfMemberRequestType.Revival;
        if (!Utility.IsDBNullOrNullValue(dtDocMeFile.Rows[0]["PrId"]))
            MeFileRow["PrId"] = dtDocMeFile.Rows[0]["PrId"].ToString();
        if (!Utility.IsDBNullOrNullValue(dtDocMeFile.Rows[0]["PrIdOrigin"]))
            MeFileRow["PrIdOrigin"] = dtDocMeFile.Rows[0]["PrIdOrigin"].ToString();

        if (!Utility.IsDBNullOrNullValue(dtDocMeFile.Rows[0]["MFNo"]))
            MeFileRow["MFNo"] = dtDocMeFile.Rows[0]["MFNo"].ToString();

        if (!Utility.IsDBNullOrNullValue(dtDocMeFile.Rows[0]["MFNoOrigin"]))
            MeFileRow["MFNoOrigin"] = dtDocMeFile.Rows[0]["MFNoOrigin"].ToString();
        MeFileRow["CreateDate"] = Utility.GetDateOfToday();
        MeFileRow["IsConfirm"] = 0;
        MeFileRow["InActive"] = 0;
        MeFileRow["UserId"] = Utility.GetCurrentUser_UserId();
        MeFileRow["ModifiedDate"] = DateTime.Now;

        DocMemberFileManager.AddRow(MeFileRow);
        if (DocMemberFileManager.Save() <= 0)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return false;
        }
        DocMemberFileManager.DataTable.AcceptChanges();
        return true;
        #endregion
    }

    private Boolean InsertWorkFlow(int TableId, TSP.DataManager.DocMemberFileManager DocMemberFileManager, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager, TSP.DataManager.WorkFlowStateManager WorkFlowStateManager)
    {

        int TaskId = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count != 1)
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            return false;
        }
        TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

        int CurrentMeId = Utility.GetCurrentUser_MeId();
        int CurrentNmcId = Utility.GetCurrentUser_MeId();
        if (CurrentNmcId == -1)
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            return false;
        }
        string Description = "شروع گردش کار درخواست تمدید پروانه اشتغال به کار توسط شخص حقیقی";
        int StateId = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId, Description);
        if (StateId <= 0)
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            return false;
        }
        WorkFlowStateManager.DataTable.AcceptChanges();
        if (Convert.ToInt32(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"]) != TableId)
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            return false;
        }
        DocMemberFileManager[DocMemberFileManager.Count - 1].BeginEdit();
        DocMemberFileManager[DocMemberFileManager.Count - 1]["CurrentWFStateId"] = StateId;
        DocMemberFileManager[DocMemberFileManager.Count - 1].EndEdit();
        DocMemberFileManager.Save();
        DocMemberFileManager.DataTable.AcceptChanges();

        return true;
    }  

    private Boolean InsertJobConfirm(int MFID, TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager)
    {
        if (Session["WizardRevivalJobConfirm"] == null)
        {
            ShowMessage("تایید کنندگان سوابق کاری ثبت نشده است.");
            return false;
        }
        dtDocJobConfirm = (DataTable)Session["WizardRevivalJobConfirm"];
        if (dtDocJobConfirm.Rows.Count == 0)
        {
            ShowMessage("تایید کنندگان سوابق کاری ثبت نشده است.");
            return false;
        }
        ArrayListJobConfirm = new System.Collections.ArrayList();
        DataTable dtJobConfInfo = new DataTable();
        dtJobConfInfo.Columns.Add("MeId");
        dtJobConfInfo.Columns.Add("DocExpired");
        for (int i = 0; i < dtDocJobConfirm.Rows.Count; i++)
        {
            DataRow dr = DocMemberFileJobConfirmationManager.NewRow();
            dr["MfId"] = MFID;

            dr["FromDate"] = dtDocJobConfirm.Rows[i]["DateFrom"];
            dr["Position"] = dtDocJobConfirm.Rows[i]["Position"];
            dr["ToDate"] = dtDocJobConfirm.Rows[i]["DateTo"];

            dr["ConfirmType"] = dtDocJobConfirm.Rows[i]["ConfirmTypeId"];
            if (!Utility.IsDBNullOrNullValue(dtDocJobConfirm.Rows[i]["MeId"]))
            {
                dr["MeId"] = dtDocJobConfirm.Rows[i]["MeId"];

                DataRow drJobConfInfo = dtJobConfInfo.NewRow();
                drJobConfInfo["MeId"] = dtDocJobConfirm.Rows[i]["MeId"];
                drJobConfInfo["DocExpired"] = dtDocJobConfirm.Rows[i]["DocExpired"];
                ArrayListJobConfirm.Add(drJobConfInfo);
            }

            dr["Name"] = dtDocJobConfirm.Rows[i]["Name"];

            dr["FileURL"] = dtDocJobConfirm.Rows[i]["FileURL"].ToString();

            if (!Utility.IsDBNullOrNullValue(dtDocJobConfirm.Rows[i]["GradeURL"]))
            {
                dr["GrdURL"] = dtDocJobConfirm.Rows[i]["GradeURL"].ToString();
            }

            dr["InActive"] = 0;
            if (!Utility.IsDBNullOrNullValue(dtDocJobConfirm.Rows[i]["Description"]))
                dr["Description"] = dtDocJobConfirm.Rows[i]["Description"];
            dr["IsConfirmed"] = 1;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            DocMemberFileJobConfirmationManager.AddRow(dr);

        }
        DocMemberFileJobConfirmationManager.Save();
        Session["JobConfirmFileURL"] = dtDocJobConfirm;
        return true;
    }
    #endregion

    private string GetMeDocDefualtRegisterDate()
    {
        Utility.Date Date = new Utility.Date();
        int MonthCount = 12;
        MonthCount = 12 * Utility.GetDefaultPermanentMemberDocExpireDate();
        return (Date.AddMonths(MonthCount));
    }

    private Boolean CheckConditions()
    {
        try
        {
            System.Collections.ArrayList Result = TSP.DataManager.DocMemberFileManager.CheckConditionForDocumentRevival(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
            if (!Convert.ToBoolean(Result[0]))
            {
                ShowMessage(Result[1].ToString());
                return false;
            }
        }
        catch (Exception err)
        {
            ShowMessage("خطایی در بازیابی اطلاعات تصاویر مربوطه ایجاد شده است.");
            return false;
        }

        return true;
    }

    private Boolean CbIAgree()
    {
        if (Session["WizardRevivalchbIAgree"] != null)
        {
            if (Convert.ToBoolean(Session["WizardRevivalchbIAgree"]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    #endregion
}