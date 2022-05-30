using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Members_Documents_WizardQualificationFinish : System.Web.UI.Page
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
        Response.Redirect("WizardQualificationSummary.aspx");
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardQualificationSummary.aspx");
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
            + Utility.GetCurrentUser_FullName() + " " + "عضو محترم سازمان نظام مهندسی استان فارس؛ متقاضی درج صلایت جدید پروانه اشتغال به کار می باشند و جنابعالی را به عنوان تایید کننده سوابق کاری خود معرفی نموده اند. در صورت هرگونه اعتراض به واحد پروانه اشتغال سازمان تماس حاصل فرمایید";
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
        if (Session["WizardDocQualificationOath"] != null && (Boolean)Session["WizardDocQualificationOath"] == true)
        {
            MenuSteps.Items.FindByName("Oath").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Oath").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Oath").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardDocQualificationExam"] != null && ((DataTable)Session["WizardDocQualificationExam"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("Exams").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Exams").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Exams").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardQualificationJobConfirm"] != null && ((DataTable)Session["WizardQualificationJobConfirm"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("JobConfirm").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("JobConfirm").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("JobConfirm").Image.Height = Unit.Pixel(15);
        }

        if (Session["WizardDocQualificationSummary"] != null && (Boolean)Session["WizardDocQualificationSummary"] == true)
        {
            MenuSteps.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }

        if (Session["WizardQualificationImgfrontDoc"] != null)
        {
            MenuSteps.Items.FindByName("Kardan").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Kardan").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Kardan").Image.Height = Unit.Pixel(15);
        }
    }

    private Boolean CheckTsTimeOut()
    {
        if (Session["WizardDocQualificationExam"] == null && Session["WizardDocQualificationSummary"] == null && Session["WizardDocQualificationOath"] == null
         && Session["WizardQualificationJobConfirm"] == null
                 )
        {
            ShowMessage("مدت زمان اعتبار صفحه به پایان رسیده است");
            return true;
        }

        if (Session["WizardDocQualificationOath"] == null)
        {
            ShowMessage("سوگند نامه و تعهدات را تایید ننموده اید");
            return true;
        }
        return false;
    }

    private void ResetDocSessions()
    {
        Session["WizardDocQualificationExam"] =
        Session["WizardDocQualificationSummary"] =
        Session["WizardDocQualificationOath"] =
        Session["WizardQualificationJobConfirm"] =
        Session["DocQualificationJobFileURL"] =
        Session["DocQualificationJobGrdURL"] =
        Session["WizardQualificationchbIAgree"] =
        Session["WizardQualificationImgfrontDoc"] =
        Session["WizardQualificationImgBackDoc"] =
        Session["WizardQualificationImgTaxOfficeLetter"] =
       Session["HSEFileURL"] = Session["ImgPeriodImage"]=
        null;
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
            // this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
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

    #region Insert
    private void InsertAndPay()
    {
        #region Define Managers
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.DocMemberExamDetailManager DocMemberExamDetailManager = new TSP.DataManager.DocMemberExamDetailManager();
        TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager = new TSP.DataManager.DocMemberFileJobConfirmationManager();

        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(DocMemberExamDetailManager);
        TransactionManager.Add(DocMemberFileJobConfirmationManager);

        #endregion

        try
        {
            Boolean IsJobNessecery = true;
            if (Session["WizardDocQualificationExam"] == null) { ShowMessage("اطلاعات آزمون ثبت نشده است."); return; }
            DataTable dtExamDetail = (DataTable)Session["WizardDocQualificationExam"];
            int cntAll = dtExamDetail.Rows.Count;
            dtExamDetail.DefaultView.RowFilter = "TTypeId =" + ((int)TSP.DataManager.DocTestType.Implement).ToString();
            if (cntAll == dtExamDetail.DefaultView.Count)
            {
                dtExamDetail.DefaultView.RowFilter = "";
                TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
                if (DocMemberFileDetailManager.FindActiveResByResponsibility(Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.DocumentResponsibilityType.Observation).Count > 0)
                    IsJobNessecery = false;  //برای وقتی که قبلا نظارت گرفته باشد
            }
            else
            {
                dtExamDetail.DefaultView.RowFilter = "TTypeId =" + ((int)TSP.DataManager.DocTestType.Observation).ToString();
                if (cntAll == dtExamDetail.DefaultView.Count)
                {
                    dtExamDetail.DefaultView.RowFilter = "";
                    TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
                    if (DocMemberFileDetailManager.FindActiveResByResponsibility(Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.DocumentResponsibilityType.Implement).Count > 0)
                        IsJobNessecery = false; //برای وقتی که قبلا اجرا گرفته باشد                    
                }
            }
            dtExamDetail.DefaultView.RowFilter = "";
            TransactionManager.BeginSave();
            int MeId = Utility.GetCurrentUser_MeId();

            if (!InsertMeDocRequest(DocMemberFileManager))
            {
                TransactionManager.CancelSave();
                return;
            }
            int MFId = Convert.ToInt32(DocMemberFileManager[DocMemberFileManager.Count - 1]["MFId"]);

            if (!InsertExams(MFId, DocMemberExamDetailManager))
            {
                TransactionManager.CancelSave();
                return;
            }

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

    private Boolean InsertMeDocRequest(TSP.DataManager.DocMemberFileManager DocMemberFileManager)
    {
        DataTable dtDocMeFile = DocMemberFileManager.SelectLastVersion(Utility.GetCurrentUser_MeId(), 0, 1);
        if (dtDocMeFile.Rows.Count <= 0)
        {
            ShowMessage("بدلیل نامشخص بودن وضعیت پروانه امکان ثبت درخواست جدید وجود ندارد.");
            return false;
        }
        int MfId = (int)dtDocMeFile.Rows[0]["MfId"];
        #region DocMe Insert
        DataRow MeFileRow = DocMemberFileManager.NewRow();
        MeFileRow["RequesterType"] = (int)TSP.DataManager.DocumentRequesterType.Member;
        MeFileRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.MemberDocument);
        MeFileRow["MeId"] = Utility.GetCurrentUser_MeId();

        if (Session["WizardQualificationImgTaxOfficeLetter"] != null)
        {
            MeFileRow["TaxOfficeLetterURL"] = Session["WizardQualificationImgTaxOfficeLetter"].ToString();
        }
        if (Session["WizardQualificationImgfrontDoc"] != null)
        {
            MeFileRow["ImgOldDocFrontURL"] = Session["WizardQualificationImgfrontDoc"].ToString();
        }

        if (Session["WizardQualificationImgBackDoc"] != null)
        {
            MeFileRow["ImgOldDocBackURL"] = Session["WizardQualificationImgBackDoc"].ToString();
        }
         if (Session["HseFileURL"] != null && ValidImpAndMajor())
        {
            MeFileRow["ImgHSEURL"] = Session["HseFileURL"].ToString();
        }
        if (Session["ImgPeriodImage"] != null)
        {
            MeFileRow["PeriodImageURL"] = Session["ImgPeriodImage"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(dtDocMeFile.Rows[0]["MasterMfMjId"]))
            MeFileRow["MasterMfMjId"] = dtDocMeFile.Rows[0]["MasterMfMjId"].ToString();

        if (!Utility.IsDBNullOrNullValue(dtDocMeFile.Rows[0]["MFSerialNo"]))
            MeFileRow["MFSerialNo"] = dtDocMeFile.Rows[0]["MFSerialNo"].ToString();
        MeFileRow["RegDate"] = Utility.GetDateOfToday();
        MeFileRow["ExpireDate"] = GetMeDocDefualtRegisterDate();
        MeFileRow["Type"] = (int)TSP.DataManager.DocumentOfMemberRequestType.Qualification;
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
        // int TableId = int.Parse(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"].ToString());
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;//DocumentUnitEmployeeConfirmingDocument;// AccountingUnitEmployeeConfirmingDocument; 
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
        string Description = "شروع گردش کار درخواست درج صلاحیت جدید پروانه اشتغال به کار توسط شخص حقیقی";
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

    private Boolean InsertExams(int MFId, TSP.DataManager.DocMemberExamDetailManager DocMemberExamDetailManager)
    {
        TSP.DataManager.DocTestConditionDetailManager DocTestConditionDetailManager = new TSP.DataManager.DocTestConditionDetailManager();
        if (Session["WizardDocQualificationExam"] == null)
        {
            ShowMessage("اطلاعات آزمون ثبت نشده است.");
            return false;
        }
        if (Session["WizardDocQualificationExam"] != null && !(((DataTable)Session["WizardDocQualificationExam"]).Rows.Count > 0))
        {
            ShowMessage("اطلاعات آزمون ثبت نشده است. ورود اطلاعات آزمون اجباری می باشد");
            return false;
        }

        dtExams = (DataTable)Session["WizardDocQualificationExam"];
        dtExams.DefaultView.RowFilter = "TTypeId=" + (int)TSP.DataManager.DocTestType.Implement;
        if (dtExams.DefaultView.Count > 0)
        {
            dtExams.DefaultView.RowFilter += "AND (MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran + "OR MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari + ")";
            if (dtExams.DefaultView.Count > 0 && Session["HseFileURL"] == null)
            {
                dtExams.DefaultView.RowFilter = "";
                ShowMessage("امکان ثبت زمینه آزمون اجرا بدون ثبت گواهی HSE وجود ندارد. ");
                return false;
            }
        }
        dtExams.DefaultView.RowFilter = "";
        if (dtExams.Rows.Count == 0)
        {
            ShowMessage("اطلاعات آزمون ثبت نشده است.");
            return false;
        }

        for (int i = 0; i < dtExams.Rows.Count; i++)
        {
            DataRow MemberExamRow = DocMemberExamDetailManager.NewRow();
            MemberExamRow["MFId"] = MFId;
            MemberExamRow["TCondId"] = dtExams.Rows[i]["TCondId"];
            MemberExamRow["TTypeId"] = dtExams.Rows[i]["TTypeId"].ToString();
            MemberExamRow["GrdId"] = dtExams.Rows[i]["GrdId"].ToString();
            MemberExamRow["Point"] = dtExams.Rows[i]["Point"].ToString();
            MemberExamRow["FileURL"] = dtExams.Rows[i]["FileURL"].ToString();
            if (!Utility.IsDBNullOrNullValue(dtExams.Rows[i]["PeriodImgURL"]))
                MemberExamRow["PeriodImgURL"] = dtExams.Rows[i]["PeriodImgURL"].ToString();
            MemberExamRow["UserCode"] = dtExams.Rows[i]["UserCode"].ToString();
            MemberExamRow["EntranceCode"] = dtExams.Rows[i]["EntranceCode"].ToString();
            MemberExamRow["UserId"] = Utility.GetCurrentUser_UserId();
            MemberExamRow["ModifiedDate"] = DateTime.Now;

            DocMemberExamDetailManager.AddRow(MemberExamRow);
            DocMemberExamDetailManager.Save();
            DocMemberExamDetailManager.DataTable.AcceptChanges();
        }
        return true;
    }

    private Boolean InsertJobConfirm(int MFID, TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager)
    {
        if (Session["WizardQualificationJobConfirm"] == null)
        {
            ShowMessage("تایید کنندگان سوابق کاری ثبت نشده است.");
            return false;
        }
        dtDocJobConfirm = (DataTable)Session["WizardQualificationJobConfirm"];
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
            System.Collections.ArrayList Result = TSP.DataManager.DocMemberFileManager.CheckConditionForDocumentQualification(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
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
        if (Session["WizardQualificationchbIAgree"] != null)
        {
            if (Convert.ToBoolean(Session["WizardQualificationchbIAgree"]))
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
    private bool ValidImpAndMajor()
    {
        DataTable dtExams = (DataTable)Session["WizardDocQualificationExam"];
        dtExams.DefaultView.RowFilter = "TTypeId=" + (int)TSP.DataManager.DocTestType.Implement + " AND (MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran + " OR MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari + ")";
        if (dtExams.DefaultView.Count > 0)
        {
            dtExams.DefaultView.RowFilter = "";
            return true;
        }
        dtExams.DefaultView.RowFilter = "";
        return false;
    }

    #endregion
}