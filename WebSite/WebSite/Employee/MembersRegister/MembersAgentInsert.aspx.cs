using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_MembersRegister_MembersAgentInsert : System.Web.UI.Page
{
    public int MeId
    {
        get
        {
            return int.Parse(Utility.DecryptQS(HiddenFieldPage["MeId"].ToString()));
        }
        set
        {
            HiddenFieldPage["MeId"] = Utility.EncryptQS(value.ToString());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!Page.IsPostBack)
        {
            try
            {
                if (Request.QueryString.Count != 0)
                {
                    MeId = int.Parse(Utility.DecryptQS(Request.QueryString["MeId"].ToString()));
                    LoadMemberInfo();
                }
                else
                {
                    Response.Redirect("MembersAgent.aspx");
                }
            }
            catch
            {
                Response.Redirect("MembersAgent.aspx");
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MembersAgent.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();

        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager(trans);
        TSP.DataManager.TransferMemberManager transferManager = new TSP.DataManager.TransferMemberManager();
        trans.Add(transferManager);
        trans.Add(MemberManager);
        trans.Add(MemberRequestManager);


        if (Utility.IsDBNullOrNullValue(MeId))
        {
            Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        try
        {
            int LastMfId = -1;
            Boolean SendWorkReqInfoToShahrdari = false;
            Boolean HasWorkReq = false;
            string Msg = "";
            if (!TSP.DataManager.MemberManager.CheckMembershipValidation(MeId, ref Msg))
            {
                ShowMessage(Msg);
                return;
            }
            System.Data.DataTable dtWorkReq = ObserverWorkRequestManager.SelectTSObserverWorkRequestByMember(MeId, TSP.DataManager.TSObserverWorkRequestStatus.Confirm);
            if (dtWorkReq.Rows.Count > 0)
            {
                HasWorkReq = true;
                trans.Add(ObserverWorkRequestChangesManager);
                trans.Add(DocMemberFileManager);
            }
            TSP.DataManager.WorkFlowTask WorkFlowTask = TSP.DataManager.WorkFlowTask.ConfirmMemberAndEndProccess;
            trans.BeginSave();
            MemberManager.FindByCode(MeId);
            string FileDate = "%";
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["FileDate"]))
                FileDate = MemberManager[0]["FileDate"].ToString();
            int per = MemberRequestManager.DoAutomaticConfirmChangeMemberData(MeId, Utility.GetCurrentUser_UserId(), drdAgent.Value.ToString(), MemberManager, transferManager, txtHomeAdr.Text, WorkFlowTask);
            if (per < 0)
            {
                trans.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }

            if (HasWorkReq)
            {
                System.Data.DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
                if (dtMeFile.Rows.Count > 0)
                    LastMfId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
                if (LastMfId != -1)
                    per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست تغییرات آماده بکاری به دلیل تغییر نمایندگی در عضویت", "تایید اتوماتیک درخواست تغییرات آماده بکاری به دلیل تایید درخواست تغییرات عضویت توسط سیستم"
               , TSP.DataManager.TSObserverWorkRequestChangeType.AgentChange, Utility.GetCurrentUser_UserId(), LastMfId, FileDate, Convert.ToInt32(drdAgent.Value), trans);
                if (per < 0)
                {
                    trans.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                    return;
                }
                SendWorkReqInfoToShahrdari = true;
            }
            trans.EndSave();
            ShowMessage("ذخیره انجام شد");
            try
            {
                if (Utility.IsWorkREquestInfoSendToShahrdari() && SendWorkReqInfoToShahrdari)
                {
                    if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())
                    {
                        System.Data.DataTable dtWorkReqInfo = ObserverWorkRequestManager.SelectTSObserverWorkRequestByMember(MeId, TSP.DataManager.TSObserverWorkRequestStatus.Confirm);
                        if (dtWorkReqInfo.Rows.Count > 0)
                        {
                            int MjParentId = Convert.ToInt32(dtWorkReqInfo.Rows[0]["MasterMfMjParentId"]);
                            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
                            int AgentId = Convert.ToInt32(dtWorkReqInfo.Rows[0]["MeAgentId"]);
                            string Year = "";
                            if (AgentId == Utility.GetCurrentAgentCode())
                                CapacityAssignmentManager.SelectCurrentYearAndStage(1);
                            else
                                CapacityAssignmentManager.SelectCurrentYearAndStage(0);
                            if (CapacityAssignmentManager.Count > 0)
                            {
                                Year = CapacityAssignmentManager[0]["Year"].ToString();
                            }
                            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
                            System.Data.DataTable dtMeDetailDesign = DocMemberFileDetailManager.FindByResponsibility(LastMfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design, -1, MjParentId);
                            System.Data.DataTable dtMeDetailUrbanism = DocMemberFileDetailManager.FindByResponsibility(LastMfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Urbanism, -1, MjParentId);
                            System.Data.DataTable dtMeDetailMapping = DocMemberFileDetailManager.FindByResponsibility(LastMfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Mapping, -1, MjParentId);
                            string MappingDate = dtMeDetailMapping.Rows.Count > 0 ? (Utility.IsDBNullOrNullValue(dtMeDetailMapping.Rows[0]["Date"]) ? "" : dtMeDetailMapping.Rows[0]["Date"].ToString()) : "";
                            ShahrdariWebservice(MjParentId,
                                dtWorkReqInfo.Rows[0]["ObsDate"].ToString()
                            , dtMeDetailDesign.Rows.Count>0? (Utility.IsDBNullOrNullValue(dtMeDetailDesign.Rows[0]["Date"]) ? "" : dtMeDetailDesign.Rows[0]["Date"].ToString()):""
                            , dtMeDetailUrbanism.Rows.Count>0? (Utility.IsDBNullOrNullValue(dtMeDetailUrbanism.Rows[0]["Date"]) ? "" : dtMeDetailUrbanism.Rows[0]["Date"].ToString()):""
                            , dtMeDetailMapping.Rows.Count > 0 ? (Utility.IsDBNullOrNullValue(dtMeDetailMapping.Rows[0]["Date"]) ? "" : dtMeDetailMapping.Rows[0]["Date"].ToString()) : ""                            
                             , Convert.ToDecimal(0)
                            , Convert.ToDecimal(0)
                            , Convert.ToDecimal(0) + Convert.ToDecimal(0), Convert.ToInt16(Year), MeId);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                ShowMessage("خطایی در ارسال اطلاعات به وب سرویس شهرداری ایجاد شده است");
            }

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            trans.CancelSave();
            ShowMessage("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void LoadMemberInfo()
    {
        if (Utility.IsDBNullOrNullValue(MeId))
        {
            Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count > 0)
        {
            lblFirstName.Text = MemberManager[0]["FirstName"].ToString();
            lblLastName.Text = MemberManager[0]["LastName"].ToString();
            lblMeId.Text = MemberManager[0]["MeId"].ToString();
            lblSSN.Text = MemberManager[0]["SSN"].ToString();
            if ((!string.IsNullOrEmpty(MemberManager[0]["ImageUrl"].ToString())))
            {
                imgMember.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
            }
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["AgentId"]))
            {
                drdAgent.DataBind();
                drdAgent.SelectedIndex = drdAgent.Items.FindByValue(MemberManager[0]["AgentId"].ToString()).Index;
            }
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["HomeAdr"]))
                txtHomeAdr.Text = MemberManager[0]["HomeAdr"].ToString();
            TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
            System.Data.DataTable dtWorkReq = ObserverWorkRequestManager.SelectTSObserverWorkRequestByMember(MeId, TSP.DataManager.TSObserverWorkRequestStatus.Confirm);
            if (dtWorkReq.Rows.Count > 0 && Convert.ToInt32(dtWorkReq.Rows[0]["WantedWorkType"]) != (int)TSP.DataManager.TSWorkRequestWantedWorkType.Design)
            {
                lblWarningWorkRequest.Visible = true;
                lblWarningWorkRequest.Text = "توجه!! این عضویت دارای آماده بکاری تایید شده در واحد خدمات مهندسی می باشد و دارای زمینه کاری نظارت می باشد. با ذخیره این درخواست به صورت اتوماتیک درخواست تغییرات در تایید شده در آماده بکاری وی ثبت می شود ";
            }
            else
            {
                lblWarningWorkRequest.Visible = true;
            }
        }
    }

    private void ShowMessage(string Msg)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Msg;
    }
    private void ShahrdariWebservice(int MjParentId, string ObsDate, string DesignDate, string UrbenismDate, string MappingDate, decimal MunObsCapacity, decimal MunDesignCapacity, decimal MunUrbenismCapacity, Int16 Year, int _MeId)
    {
        //        لیست ClsQtaInputs دارای اطلاعات زیر می باشد
        //CI_Ability : در صورتی که صلاحیت طراحی باشد عدد 2 و صلاحیت نظارت عدد 1 ، صلاحیت مجری عدد 3 ، صلاحیت محاسبه عدد 4
        //Meter: متراژ ظرفیت شیراز
        //date : تاریخ بازگشایی ظرفیت
        //Time  :  ساعت بازگشلیی ظرفیت
        //ChangeBaseDate  : تاریخ تغییر پایه
        try
        {
            #region MainServer

            WorkRequestsrvEngineerToOthers.IsrvEngineerToOthersClient IsrvEngineerToOthersClient = new WorkRequestsrvEngineerToOthers.IsrvEngineerToOthersClient();
            int MasterMfMjParentId = -2; int ObsId = -2; ; int DesId = 0; int UrbanismId = 0; int MappingId = 0;
            Int16 ObsGrad = 0; Int16 DesGrad = 0; Int16 UrbanismGrad = 0; Int16 MappingGrad = 0;
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            DataTable dtMember = MemberManager.SelectMemberInfoForWorkRequest(_MeId);
            if (dtMember.Rows.Count != 1)
                return;
            string LastDocRegDate = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["LastDocRegDate"]) ? "" : dtMember.Rows[0]["LastDocRegDate"].ToString();
            MasterMfMjParentId = Convert.ToInt32(dtMember.Rows[0]["MasterMfMjParentId"]);
            ObsId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["ObsId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["ObsId"]);
            DesId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["DesId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["DesId"]);
            UrbanismId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["UrbanismId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["UrbanismId"]);
            switch (ObsId)
            {
                case (int)TSP.DataManager.DocumentGrads.Grade1:
                    ObsGrad = 1;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade2:
                    ObsGrad = 2;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade3:
                    ObsGrad = 3;
                    break;
            }
            switch (DesId)
            {
                case (int)TSP.DataManager.DocumentGrads.Grade1:
                    ObsGrad = 1;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade2:
                    DesGrad = 2;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade3:
                    DesGrad = 3;
                    break;
            }
            switch (UrbanismId)
            {
                case (int)TSP.DataManager.DocumentGrads.Grade1:
                    UrbanismGrad = 1;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade2:
                    UrbanismGrad = 2;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade3:
                    UrbanismGrad = 3;
                    break;
            }
            switch (MappingId)
            {
                case (int)TSP.DataManager.DocumentGrads.Grade1:
                    MappingGrad = 1;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade2:
                    MappingGrad = 2;
                    break;
                case (int)TSP.DataManager.DocumentGrads.Grade3:
                    MappingGrad = 3;
                    break;
            }
            if (string.IsNullOrWhiteSpace(ObsDate))
                ObsDate = MappingDate;
            #region اطلاعات پایه مهندس
            WorkRequestsrvEngineerToOthers.ClsEngineer1 ClsEngineer1Info = new WorkRequestsrvEngineerToOthers.ClsEngineer1();
            ClsEngineer1Info.Eng_Info = new WorkRequestsrvEngineerToOthers.Eng_Info();
            ClsEngineer1Info.Eng_Info.EngName = dtMember.Rows[0]["FirstName"].ToString();
            ClsEngineer1Info.Eng_Info.EngFamily = dtMember.Rows[0]["LastName"].ToString();
            ClsEngineer1Info.Eng_Info.MunicipalityCode = dtMember.Rows[0]["MeNo"].ToString();// کد عضویت نظام مهندسی
            ClsEngineer1Info.Eng_Info.FatherName = dtMember.Rows[0]["FatherName"].ToString();// نام پدر
            ClsEngineer1Info.Eng_Info.BirthDate = dtMember.Rows[0]["BirhtDate"].ToString(); //تاریخ تولد
            ClsEngineer1Info.Eng_Info.IdNo = dtMember.Rows[0]["IdNo"].ToString();//شماره شناسنامه
            ClsEngineer1Info.Eng_Info.Tel = dtMember.Rows[0]["HomeTel"].ToString();// تلفن
            ClsEngineer1Info.Eng_Info.Address = dtMember.Rows[0]["HomeAdr"].ToString();// آدرس
            ClsEngineer1Info.Eng_Info.NationalCode = dtMember.Rows[0]["SSN"].ToString();// کد ملی
            ClsEngineer1Info.Eng_Info.IdentityCode = _MeId.ToString();//کد عضویت
            ClsEngineer1Info.Eng_Info.AddressWork = ""; //آدرس محل کار
            ClsEngineer1Info.Eng_Info.Email = dtMember.Rows[0]["Email"].ToString();// آدرس ایمیل
            ClsEngineer1Info.Eng_Info.BirthPlace = dtMember.Rows[0]["BirthPlace"].ToString();// محل تولد
            ClsEngineer1Info.Eng_Info.MobileNo = dtMember.Rows[0]["MobileNo"].ToString(); //شماره همراه
            ClsEngineer1Info.Eng_Info.PostalCode = "";//کد پستی
            ClsEngineer1Info.Eng_Info.ArchitectureCode = "";// کد شهرداری
            ClsEngineer1Info.Eng_Info.PostalCodeWork = ""; //کد پستی محل کار
            switch (MjParentId)
            {
                case 1:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 100;
                    break;
                case 2:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 210;
                    break;
                case 3:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 300;
                    break;
                case 4:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 400;
                    break;
                case 5:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 500;
                    break;
                case 6:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 600;
                    break;
                case 7:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 700;
                    break;
            }
            IsrvEngineerToOthersClient.SaveEng_InfoCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveEng_InfoCompletedEventArgs>(GetResultEngineerToOthers);
            IsrvEngineerToOthersClient.SaveEng_InfoAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ClsEngineer1Info);
            #endregion
            #region اطلاعات پروانه
            WorkRequestsrvEngineerToOthers.Eng_JobAgreement PEngjob = new WorkRequestsrvEngineerToOthers.Eng_JobAgreement();
            PEngjob.JobAgreementExportDate = LastDocRegDate;// تاریخ صدور پروانه اشتغال جاری
            PEngjob.JobAgreementExpireDate = dtMember.Rows[0]["FileDate"].ToString();//تاریخ اعتبار پروانه اشتغال جاری
                                                                                     //PEngjob.NIdEng = ClsEngineer1Info.NidEngineer;
            PEngjob.NIdJobAgreement_tmp = 0;
            PEngjob.CI_JobAgreementType = 1;
            PEngjob.CI_JobAgreementBaseExport = 0;
            PEngjob.CI_Region = 1;

            IsrvEngineerToOthersClient.SaveEng_JobAgreementCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveEng_JobAgreementCompletedEventArgs>(GetResultJobAgreement);
            IsrvEngineerToOthersClient.SaveEng_JobAgreementAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), PEngjob);

            #endregion
            #region اطلاعات جدول صلاحیت

            int Len = 1;
            if ((DesGrad > 0 || UrbanismGrad > 0) && (ObsGrad > 0 || MappingGrad > 0))
                Len = 2;
            WorkRequestsrvEngineerToOthers.Eng_Competence[] ListengCompetence = new WorkRequestsrvEngineerToOthers.Eng_Competence[Len];
            if (ObsGrad > 0 || MappingGrad > 0)
            {
                WorkRequestsrvEngineerToOthers.Eng_Competence engCompetenceObj = new WorkRequestsrvEngineerToOthers.Eng_Competence();
                engCompetenceObj.CI_Ability = 1;
                engCompetenceObj.CI_Base = ObsGrad > 0 ? ObsGrad : MappingGrad;
                engCompetenceObj.IsActive = true;
                ListengCompetence[0] = engCompetenceObj;

            }
            if (DesGrad > 0 || UrbanismGrad > 0)
            {
                WorkRequestsrvEngineerToOthers.Eng_Competence engCompetenceObj = new WorkRequestsrvEngineerToOthers.Eng_Competence();
                if (MasterMfMjParentId == (int)TSP.DataManager.MainMajors.Civil)
                    engCompetenceObj.CI_Ability = 4;
                else
                    engCompetenceObj.CI_Ability = 2;

                engCompetenceObj.CI_Base = DesGrad > 0 ? DesGrad : UrbanismGrad;
                engCompetenceObj.IsActive = true;
                if (Len == 1)
                    ListengCompetence[0] = engCompetenceObj;
                else if (Len == 2)
                    ListengCompetence[1] = engCompetenceObj;
            }
            IsrvEngineerToOthersClient.SaveEng_CompetenceCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveEng_CompetenceCompletedEventArgs>(GetResultCompetence);
            IsrvEngineerToOthersClient.SaveEng_Competence("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ListengCompetence);

            #endregion
            #region اطلاعات ظرفیت مهندس در آماده بکاری
            int Count = 1;
            if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
                Count = 2;
            string time = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
            WorkRequestsrvEngineerToOthers.ClsQtaInputs[] ListclsQtaInputs = new WorkRequestsrvEngineerToOthers.ClsQtaInputs[Count];
            //CI_Ability: در صورتی که صلاحیت طراحی باشد عدد 2 و صلاحیت نظارت عدد 1 ، صلاحیت مجری عدد 3 ، صلاحیت محاسبه عدد 4 
            if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
            {
                #region نظارت و طراحی
                WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsde = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                if (MjParentId == (int)TSP.DataManager.MainMajors.Civil)
                    ClsQtaInputsde.CI_Ability = 4;
                else
                    ClsQtaInputsde.CI_Ability = 2;
                ClsQtaInputsde.Date = Utility.GetDateOfToday();
                if (MjParentId == (int)TSP.DataManager.MainMajors.Urbanism)
                    ClsQtaInputsde.Meter = MunUrbenismCapacity;
                else
                    ClsQtaInputsde.Meter = MunDesignCapacity;
                ClsQtaInputsde.Time = time;
                ClsQtaInputsde.ChangeBaseDate = DesignDate;
                ListclsQtaInputs[0] = ClsQtaInputsde;
                //**نظارت
                WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsOb = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                ClsQtaInputsOb.CI_Ability = 1;
                //ClsQtaInputsOb.Date = Utility.GetDateOfToday();
                ClsQtaInputsOb.Meter = MunObsCapacity;
                //ClsQtaInputsOb.Time = time;
                ClsQtaInputsOb.ChangeBaseDate = ObsDate;
                ListclsQtaInputs[1] = ClsQtaInputsOb;
                #endregion
            }
            else if ((MunDesignCapacity != 0 || MunUrbenismCapacity != 0) && string.IsNullOrEmpty(ObsDate))
            {
                #region فقظ طراحی یا شهرسازی
                WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsDes = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                if (MjParentId == (int)TSP.DataManager.MainMajors.Civil)
                    ClsQtaInputsDes.CI_Ability = 4;
                else
                    ClsQtaInputsDes.CI_Ability = 2;
                ClsQtaInputsDes.Date = Utility.GetDateOfToday();
                if (MjParentId == (int)TSP.DataManager.MainMajors.Urbanism)
                    ClsQtaInputsDes.Meter = MunUrbenismCapacity;
                else
                    ClsQtaInputsDes.Meter = MunDesignCapacity;
                ClsQtaInputsDes.Time = time;
                ClsQtaInputsDes.ChangeBaseDate = DesignDate;
                ListclsQtaInputs[0] = ClsQtaInputsDes;
                #endregion
            }
            else if (!string.IsNullOrEmpty(ObsDate))
            {
                #region فقظ نظارت
                //**نظارت
                WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsObs = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                ClsQtaInputsObs.CI_Ability = 1;
                //ClsQtaInputsObs.Date = Utility.GetDateOfToday();
                ClsQtaInputsObs.Meter = MunObsCapacity;
                //ClsQtaInputsObs.Time = time;
                ClsQtaInputsObs.ChangeBaseDate = ObsDate;
                ListclsQtaInputs[0] = ClsQtaInputsObs;
                #endregion
            }

            IsrvEngineerToOthersClient.SaveQtaInfoCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveQtaInfoCompletedEventArgs>(GetGetResultEngineerToOthersClient);
            IsrvEngineerToOthersClient.SaveQtaInfoAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ListclsQtaInputs, 1, Year, (int)TSP.DataManager.TSSafarayanehWebServiceCallingRefrenceType.WorkRequest);
            #endregion

            #endregion
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            ShowMessage(ex.ToString());
        }
    }

    private void GetGetResultEngineerToOthersClient(object sender, WorkRequestsrvEngineerToOthers.SaveQtaInfoCompletedEventArgs e)
    {
        WorkRequestsrvEngineerToOthers.ClsErrorResult ErrorResult = e.Result;
        if (ErrorResult.BizErrors.Length == 0)
        {
            //ok;
        }
        else
        {
            ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
        }
    }
    private void GetResultEngineerToOthers(object sender, WorkRequestsrvEngineerToOthers.SaveEng_InfoCompletedEventArgs e)
    {
        WorkRequestsrvEngineerToOthers.ClsEngineer1 ResultClsEngineer1 = e.Result;
        if (ResultClsEngineer1.ErrorResult.BizErrors[0] != null)
        {
            //ok;
        }
        else
        {

            ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
        }
    }

    private void GetResultJobAgreement(object sender, WorkRequestsrvEngineerToOthers.SaveEng_JobAgreementCompletedEventArgs e)
    {
        try
        {           
            WorkRequestsrvEngineerToOthers.Eng_JobAgreement ResultJobAgreement = e.Result;
            if (ResultJobAgreement != null)
            {
                //ok;
            }
            else
            {
                ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
            }
        }
        catch (Exception ex)
        {
           
            Utility.SaveWebsiteError(ex);
        }
    }

    private void GetResultCompetence(object sender, WorkRequestsrvEngineerToOthers.SaveEng_CompetenceCompletedEventArgs e)
    {
        try
        {
           WorkRequestsrvEngineerToOthers.Eng_Competence[] ResultCompetence = e.Result;
            if (ResultCompetence.Length > 0)
            {
                //ok;
            }
            else
            {               
                ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
            }
        }
        catch (Exception ex)
        {
          
            Utility.SaveWebsiteError(ex);
        }
    }

}