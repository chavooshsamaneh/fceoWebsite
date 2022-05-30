using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Members_Documents_WizardDocFinish : System.Web.UI.Page
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
        Response.Redirect("WizardDocSummary.aspx");
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardDocSummary.aspx");
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
    private bool ValidImpAndMajor()
    {
        DataTable dtExams = (DataTable)Session["WizardDocExam"];
        dtExams.DefaultView.RowFilter = "TTypeId=" + (int)TSP.DataManager.DocTestType.Implement + " AND (MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Omran + " OR MajorId=" + (int)TSP.DataManager.MajorManager.ParentMajors.Memari + ")";
        if (dtExams.DefaultView.Count > 0)
        {
            dtExams.DefaultView.RowFilter = "";
            return true;
        }
        dtExams.DefaultView.RowFilter = "";
        return false;
    }
    private void SendSms(DataRow dr)//string SMSMeId)
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
            + Utility.GetCurrentUser_FullName() + " " + "عضو محترم سازمان نظام مهندسی استان فارس؛ متقاضی اخذ پروانه اشتغال به کار می باشند و جنابعالی را به عنوان تایید کننده سوابق کاری خود معرفی نموده اند. در صورت هرگونه اعتراض به واحد پروانه اشتغال سازمان تماس حاصل فرمایید";
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
        if (Session["WizardDocOath"] != null && (Boolean)Session["WizardDocOath"] == true)
        {
            MenuSteps.Items.FindByName("Oath").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Oath").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Oath").Image.Height = Unit.Pixel(15);
        }
        if (Session["HseFileURL"] != null  || (Session["ImgTaxOfficeLetter"] != null || CbIAgree()))
        {
            MenuSteps.Items.FindByName("AccConfirm").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("AccConfirm").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("AccConfirm").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardDocExam"] != null && ((DataTable)Session["WizardDocExam"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("Exams").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Exams").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Exams").Image.Height = Unit.Pixel(15);
        }
        //if (Session["WizardDocMajor"] != null && ((DataTable)Session["WizardDocMajor"]).Rows.Count > 0)
        //{
        //    MenuSteps.Items.FindByName("Major").Image.Url = "~/Images/icons/button_ok.png";
        //    MenuSteps.Items.FindByName("Major").Image.Width = Unit.Pixel(15);
        //    MenuSteps.Items.FindByName("Major").Image.Height = Unit.Pixel(15);
        //}
        if (Session["WizardDocJob"] != null && ((DataTable)Session["WizardDocJob"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("Job").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Job").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Job").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardDocJobConfirm"] != null && ((DataTable)Session["WizardDocJobConfirm"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("JobConfirm").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("JobConfirm").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("JobConfirm").Image.Height = Unit.Pixel(15);
        }
        //if (Session["WizardDocResposblity"] != null && ((DataTable)Session["WizardDocResposblity"]).Rows.Count > 0)
        //{
        //    MenuSteps.Items.FindByName("DocRes").Image.Url = "~/Images/icons/button_ok.png";
        //    MenuSteps.Items.FindByName("DocRes").Image.Width = Unit.Pixel(15);
        //    MenuSteps.Items.FindByName("DocRes").Image.Height = Unit.Pixel(15);
        //}
        //if (Session["WizardDocPeriods"] != null && (Boolean)Session["WizardDocPeriods"] == true)
        //{
        //    MenuSteps.Items.FindByName("Periods").Image.Url = "~/Images/icons/button_ok.png";
        //    MenuSteps.Items.FindByName("Periods").Image.Width = Unit.Pixel(15);
        //    MenuSteps.Items.FindByName("Periods").Image.Height = Unit.Pixel(15);
        //}
        if (Session["WizardDocSummary"] != null && (Boolean)Session["WizardDocSummary"] == true)
        {
            MenuSteps.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }
    }

    private Boolean CheckTsTimeOut()
    {
        if (Session["WizardDocExam"] == null && Session["WizardDocSummary"] == null && Session["WizardDocOath"] == null
     && Session["WizardDocJobConfirm"] == null
            //&& Session["WizardDocMajor"] == null && Session["WizardDocJob"] == null
            // && Session["WizardDocResposblity"] == null && Session["WizardDocPeriods"] == null
                 )
        {
            ShowMessage("مدت زمان اعتبار صفحه به پایان رسیده است");
            return true;
        }

        if (Session["WizardDocOath"] == null)
        {
            ShowMessage("سوگند نامه و تعهدات را تایید ننموده اید");
            return true;
        }
        return false;
    }

    private void ResetDocSessions()
    {
        Session["WizardDocOath"] = 
        Session["WizardDocExam"] = 
        Session["WizardDocMajor"] =
        Session["WizardDocResposblity"] = 
        Session["WizardDocPeriods"] = 
        Session["WizardDocJob"] = 
        Session["WizardDocSummary"] = 
        Session["WizardDocJobConfirm"] = 
        Session["JobGrdURL"] = 
        Session["ACCFileURL"] = 
        Session["HSEFileURL"] = 
        Session["FishFileURL"] =
        Session["JobFileURL"] =
        Session["chbIAgree"] =
        Session["ImgTaxOfficeLetter"] =
        Session["ExamFileURL"] = null;
       
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
    }    

    private void InsertAndPay()
    {
        #region Define Managers
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
        TSP.DataManager.DocMemberExamDetailManager DocMemberExamDetailManager = new TSP.DataManager.DocMemberExamDetailManager();
        TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager = new TSP.DataManager.DocMemberFileJobConfirmationManager();

        TransactionManager.Add(DocMemberFileMajorManager);
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(TransferMemberManager);
        TransactionManager.Add(DocMemberExamDetailManager);
        TransactionManager.Add(DocMemberFileJobConfirmationManager);

        #endregion
        Boolean IsTransfer = false;
        string PreMFNo = "";
        string PrCode = "";
        PrCode = Utility.GetCurrentProvinceNezamCodeFromTblProvince().ToString();
        if (string.IsNullOrEmpty(PrCode))
        {
            ShowMessage("کد نظام مهندسی (کد استان) مشخص نمی باشد");
            return;
        }

        try
        {
            TransactionManager.BeginSave();
            int MeId = Utility.GetCurrentUser_MeId();

            if (!InsertMemberFile(TransferMemberManager, DocMemberFileManager))
            {
                TransactionManager.CancelSave();
                return;
            }
            int MFId = Convert.ToInt32(DocMemberFileManager[0]["MFId"]);
            System.Collections.ArrayList DocMajorResult = InsertDocMemberFileMajor(MFId, DocMemberFileMajorManager, DocMemberFileManager);
            if (!Convert.ToBoolean(DocMajorResult[0]))
            {
                TransactionManager.CancelSave();
                return;
            }

            if (!CreateAndSetMfNo(DocMajorResult[1].ToString(), Convert.ToInt32(DocMemberFileMajorManager[0]["FMjId"]), IsTransfer, PreMFNo, PrCode, DocMemberFileManager))
            {
                TransactionManager.CancelSave();
                return;
            }

            if (!InsertExams(MFId, DocMemberExamDetailManager))
            {
                TransactionManager.CancelSave();
                return;
            }

            if (!InsertJobConfirm(MFId, DocMemberFileJobConfirmationManager))
            {
                TransactionManager.CancelSave();
                return;
            }

            if (!InsertWorkFlow(DocMemberFileManager, WorkFlowTaskManager, WorkFlowStateManager))
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
        }    
    }

    private Boolean InsertMemberFile(TSP.DataManager.TransferMemberManager TransferMemberManager, TSP.DataManager.DocMemberFileManager DocMemberFileManager)
    {
        #region Insert DocMemberFile
        DataRow MemberFileRow = DocMemberFileManager.NewRow();
        MemberFileRow["RequesterType"] = (int)TSP.DataManager.DocumentRequesterType.Member;
        MemberFileRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.MemberDocument);
        MemberFileRow["MeId"] = Utility.GetCurrentUser_MeId();
        MemberFileRow["PrId"] = Utility.GetCurrentProvinceId();

        TransferMemberManager.FindByMemberId(Utility.GetCurrentUser_MeId(), TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
        if (TransferMemberManager.Count > 0 && !Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
        {
            //?????????????????????????????????????????????????
            #region TransferMember
            //TransferMemberManager[0].BeginEdit();
            //if (!string.IsNullOrWhiteSpace(txtFirstDocRegDate.Text))
            //    TransferMemberManager[0]["FirstDocRegDate"] = txtFirstDocRegDate.Text;
            //if (!string.IsNullOrWhiteSpace(txtFirstDocRegDate.Text))
            //    TransferMemberManager[0]["CurrentDocRegDate"] = txtCurrentDocRegDate.Text;
            //if (!string.IsNullOrWhiteSpace(txtFirstDocRegDate.Text))
            //    TransferMemberManager[0]["CurrentDocExpDate"] = txtCurrentDocExpDate.Text;

            //if (ComboDocPr.SelectedItem != null && ComboDocPr.SelectedItem.Value != null)
            //    TransferMemberManager[0]["DocPrId"] = ComboDocPr.SelectedItem.Value;

            //TransferMemberManager[0].EndEdit();
            //if (TransferMemberManager.Save() <= 0)
            //{
            //    TransactionManager.CancelSave();
            //    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            //    return;
            //}
            //if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["DocPrId"]))
            //    MemberFileRow["PrIdOrigin"] = Convert.ToInt32(TransferMemberManager[0]["DocPrId"]);
            //MemberFileRow["MFNoOrigin"] = TransferMemberManager[0]["FileNo"].ToString();
            //PreMFNo = TransferMemberManager[0]["FileNo"].ToString();
            //PrCode = TransferMemberManager[0]["FileNo"].ToString().Remove(2);
            //if (cmbTransferType.Value != null)
            //    MemberFileRow["Type"] = Convert.ToInt32(cmbTransferType.Value);// TSP.DataManager.DocumentOfMemberRequestType.Transfer;//*****انتقالی
            //IsTransfer = true;
            #endregion
            //?????????????????????????????????????????????????
        }
        else
        {
            //*****صدور
            MemberFileRow["Type"] = (int)TSP.DataManager.DocumentOfMemberRequestType.New;
        }       

        MemberFileRow["RegDate"] = Utility.GetDateOfToday();
        MemberFileRow["ExpireDate"] = GetMeDocDefualtRegisterDate();
        MemberFileRow["IsTemporary"] = (int)TSP.DataManager.DocumentSetExpireDateType.Permanent;
        MemberFileRow["CreateDate"] = Utility.GetDateOfToday();
        MemberFileRow["IsConfirm"] = 0;
        MemberFileRow["InActive"] = 0;
        MemberFileRow["Description"] = "";
        MemberFileRow["UserId"] = Utility.GetCurrentUser_UserId();
        MemberFileRow["ModifiedDate"] = DateTime.Now;
        
        if (Session["ImgTaxOfficeLetter"] != null)
        {
            MemberFileRow["TaxOfficeLetterURL"] = Session["ImgTaxOfficeLetter"].ToString();
        }
        if (Session["HseFileURL"] != null && ValidImpAndMajor())
        {
            MemberFileRow["ImgHSEURL"] = Session["HseFileURL"].ToString();
        }
        DocMemberFileManager.AddRow(MemberFileRow);
        int cn = DocMemberFileManager.Save();
        DocMemberFileManager.DataTable.AcceptChanges();
        #endregion
        if (cn <= 0)
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است");
            return false;
        }
        return true;
    }

    private System.Collections.ArrayList InsertDocMemberFileMajor(int MFId, TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager, TSP.DataManager.DocMemberFileManager DocMemberFileManager)
    {
        System.Collections.ArrayList ArrayResult = new System.Collections.ArrayList();
        ArrayResult.Add(false);
        ArrayResult.Add("");
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        dtMemberFileMajor = MemberLicenceManager.SelectByMemberId(Utility.GetCurrentUser_MeId(), 0);
        dtMemberFileMajor.DefaultView.RowFilter = "DefaultValue=1";
        if (dtMemberFileMajor.DefaultView.Count != 1)
        {
            ShowMessage("رشته تحصیلی پیش فرض در واحد عضویت برای شما نامشخص می باشد.");
            return ArrayResult;
        }
        TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
        DataTable dt = MajorManager.FindMajorParent(Convert.ToInt32(dtMemberFileMajor.DefaultView[0]["MjId"]));
        if (dt.Rows.Count == 0)
        {
            ShowMessage("خطایی در بازیابی اطلاعات ایجاد شده است.");
            return ArrayResult;
        }
        dt.DefaultView.RowFilter = "MjCode=" + dtMemberFileMajor.DefaultView[0]["MjCode"].ToString();
        string MjCode = dt.DefaultView[0]["MjCode"].ToString();
        ArrayResult[1] = MjCode;
        DataRow MFMajorRow = DocMemberFileMajorManager.NewRow();
        MFMajorRow["MFId"] = MFId;
        MFMajorRow["MlId"] = dtMemberFileMajor.DefaultView[0]["MlId"].ToString();
        MFMajorRow["FMjId"] = dt.DefaultView[0]["MjId"].ToString();
        MFMajorRow["IsMaster"] = 1;
        MFMajorRow["MailDate"] = "";
        MFMajorRow["MailNo"] = "";
        MFMajorRow["IsPrinted"] = 1;
        MFMajorRow["UserId"] = Utility.GetCurrentUser_UserId();
        MFMajorRow["ModifiedDate"] = DateTime.Now;
        DocMemberFileMajorManager.AddRow(MFMajorRow);
        int cnt = DocMemberFileMajorManager.Save();
        DocMemberFileMajorManager.DataTable.AcceptChanges();
        if (cnt < 0)
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            return ArrayResult;
        }

        //---------update mfmjid in docmemberfile----------------------------
        if (!Utility.IsDBNullOrNullValue(DocMemberFileMajorManager[0]["MFMjId"]))
        {
            DocMemberFileManager[0].BeginEdit();
            DocMemberFileManager[0]["MasterMfMjId"] = DocMemberFileMajorManager[0]["MFMjId"];
            DocMemberFileManager[0].EndEdit();
        }
        ArrayResult[0] = true;
        return ArrayResult;
    }

    private Boolean CreateAndSetMfNo(string MjCode, int MjId, Boolean IsTransfer, string PreMFNo, string PrCode, TSP.DataManager.DocMemberFileManager DocMemberFileManager)
    {
        #region Create MfNo
        //**********MFNo=PrCode +MjCode+MFSerialNo*********        
        string MFSerialNo = "";
        string MfNo = "";
        // = -1;
        // int MajorCount = dtMemberFileMajor.Rows.Count;
        //for (int j = 0; j < MajorCount; j++)
        //{
        //    int IsMaster = 1;// int.Parse(dtMemberFileMajor.Rows[j]["IsMaster"].ToString());
        //    if (IsMaster == 1)
        //    {
        //  MjId = int.Parse(dtMemberFileMajor.Rows[j]["FMjId"].ToString());

        //MjCode = dtMemberFileMajor.Rows[j]["MjCode"].ToString();
        //        break;
        //    }
        //}
        int NewSerialNo = DocMemberFileManager.GenerateNewMemberFileSerialNo(Utility.GetCurrentUser_MeId(), MjCode);
        if (NewSerialNo <= 0)
        {
            ShowMessage(DocMemberFileManager.FindErrorMessage(NewSerialNo));
            return false;
        }
        MFSerialNo = NewSerialNo.ToString();
        int SerialLen = MFSerialNo.Length;
        int t = 5 - SerialLen;
        for (int i = 0; i < t; i++)
        {
            MFSerialNo = "0" + MFSerialNo;
        }
        MfNo = PrCode + "-" + MjCode + "-" + MFSerialNo;
        #endregion

        #region Set MfNo
        if (DocMemberFileManager.CheckIfMfNoRepitetive(MfNo, Utility.GetCurrentUser_MeId()))
        {
            ShowMessage("امکان ذخیره وجود ندارد.شماره پروانه ایجاد شده تکراری می باشد.لطفا پیگیری لازم را انجام دهید.");
            return false;
        }
        DocMemberFileManager[0].BeginEdit();

        if (!IsTransfer)
        {
            DocMemberFileManager[0]["MFNo"] = MfNo;
            DocMemberFileManager[0]["MFSerialNo"] = MFSerialNo;
        }
        else
            DocMemberFileManager[0]["MFNo"] = PreMFNo;
        DocMemberFileManager[0].EndEdit();
        int SaveCount = DocMemberFileManager.Save();
        DocMemberFileManager.DataTable.AcceptChanges();

        if (SaveCount == 0)
        {
            ShowMessage("خطایی در ذخیره انجام گرفته است.");
            return false;
        }
        #endregion

        return true;
    }

    private Boolean InsertWorkFlow(TSP.DataManager.DocMemberFileManager DocMemberFileManager, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager, TSP.DataManager.WorkFlowStateManager WorkFlowStateManager)
    {

        int TaskId = -1;
        int TableId = int.Parse(DocMemberFileManager[0]["MfId"].ToString());
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
            return false;
        }
        string Description = "شروع گردش کار درخواست صدور پروانه اشتغال به کار توسط شخص حقیقی";
        int StateId = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId, Description);
        if (StateId <= 0)
        {
            return false;
        }
        WorkFlowStateManager.DataTable.AcceptChanges();
        DocMemberFileManager[0].BeginEdit();
        DocMemberFileManager[0]["CurrentWFStateId"] = StateId;
        DocMemberFileManager[0].EndEdit();
        DocMemberFileManager.Save();
        DocMemberFileManager.DataTable.AcceptChanges();

        //int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
        //int NextTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument;
        //int NextStepTaskId = -1;
        //WorkFlowTaskManager.FindByTaskCode(NextTaskCode);
        //if (WorkFlowTaskManager.Count != 1)
        //{
        //    ShowMessage("خطایی در ذخیره انجام گرفته است.");
        //    return false;
        //}
        //NextStepTaskId = int.Parse(WorkFlowTaskManager[WorkFlowTaskManager.Count - 1]["TaskId"].ToString());

        //int NmcId = Utility.GetCurrentUser_MeId();
        //int NmcIdType = (int)TSP.DataManager.WorkFlowStateNmcIdType.TempMember;
        //string Url = "";// "<a href='../Employee/Document/AddMemberFile.aspx?MeId=" + Utility.EncryptQS(MemberId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "' target=_blank>اینجا کلیک کنید</a>";
        //string MsgContent = "";
        //if (WorkFlowStateManager.SendDocToNextStep(TableType, TableId, NextStepTaskId, "ارسال پرونده پروانه اشتغال به واحد مربوطه توسط عضو", NmcId, NmcIdType, Utility.GetCurrentUser_UserId(), MsgContent, Url) <= 0)
        //{
        //    ShowMessage("خطایی در ذخیره انجام گرفته است.");
        //    return false;
        //}
        return true;
    }

    private Boolean InsertExams(int MFId, TSP.DataManager.DocMemberExamDetailManager DocMemberExamDetailManager)
    {
        TSP.DataManager.DocTestConditionDetailManager DocTestConditionDetailManager = new TSP.DataManager.DocTestConditionDetailManager();
        if (Session["WizardDocExam"] == null)
        {
            ShowMessage("اطلاعات آزمون ثبت نشده است.");
            return false;
        }
        if (Session["WizardDocExam"] != null && !(((DataTable)Session["WizardDocExam"]).Rows.Count > 0))
        {
            ShowMessage("اطلاعات آزمون ثبت نشده است. ورود اطلاعات آزمون اجباری می باشد");
            return false;
        }

        dtExams = (DataTable)Session["WizardDocExam"];      

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
        if (Session["WizardDocJobConfirm"] == null)
        {
            ShowMessage("تایید کنندگان سوابق کاری ثبت نشده است.");
            return false;
        }
        dtDocJobConfirm = (DataTable)Session["WizardDocJobConfirm"];
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

        return true;
    }

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
            System.Collections.ArrayList Result = TSP.DataManager.DocMemberFileManager.CheckConditionForNewDocument(Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_LoginType());
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
        if (Session["chbIAgree"] != null)
        {
            if (Convert.ToBoolean(Session["chbIAgree"]))
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