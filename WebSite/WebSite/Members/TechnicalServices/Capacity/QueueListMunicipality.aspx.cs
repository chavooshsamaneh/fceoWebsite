using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WorkRequestsrvEngineerToOthersTest;

public partial class Members_TechnicalServices_Capacity_QueueListMunicipality : System.Web.UI.Page
{
    string Address = System.Web.HttpContext.Current.Request.UserHostAddress;
    string _Year
    {
        get
        {
            return HiddenFieldPage["Year"].ToString();
        }
        set
        {
            HiddenFieldPage["Year"] = value;
        }
    }

    int _MfMjParentId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["MfMjParentId"]);
        }
        set
        {
            HiddenFieldPage["MfMjParentId"] = value;
        }
    }
    string _ObsDate
    {
        get
        {
            return HiddenFieldPage["ObsDate"].ToString();
        }
        set
        {
            HiddenFieldPage["ObsDate"] = value;
        }
    }
    string _DesignDate
    {
        get
        {
            return HiddenFieldPage["DesignDate"].ToString();
        }
        set
        {
            HiddenFieldPage["DesignDate"] = value;
        }
    }
    string _UrbenismDate
    {
        get
        {
            return HiddenFieldPage["UrbenismDate"].ToString();
        }
        set
        {
            HiddenFieldPage["UrbenismDate"] = value;
        }
    }
    int _ObsShirazMunicipality
    {

        get
        {
            return Convert.ToInt32(HiddenFieldPage["ObsShirazMunicipality"]);
        }
        set
        {
            HiddenFieldPage["ObsShirazMunicipality"] = value;
        }
    }
    int _DesignShirazMunicipality
    {

        get
        {
            return Convert.ToInt32(HiddenFieldPage["DesignShirazMunicipality"]);
        }
        set
        {
            HiddenFieldPage["DesignShirazMunicipality"] = value;
        }
    }
    int _ShirazMunicipulityUrbenismTarh
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["ShirazMunicipulityUrbenismTarh"]);
        }
        set
        {
            HiddenFieldPage["ShirazMunicipulityUrbenismTarh"] = value;
        }
    }
    int _ShirazMunicipulityUrbenismEntebaghShahri
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPage["ShirazMunicipulityUrbenismEntebaghShahri"]);
        }
        set
        {
            HiddenFieldPage["ShirazMunicipulityUrbenismEntebaghShahri"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.Compare(Utility.GetDateOfToday(), Utility.TSQueueOpenDate()) < 0)
            {
                ShowMessage("زمان بازگشایی سامانه اولویت بندی ارجاع کار نظارت شهرداری شیراز ، راس ساعت 00:00 جمعه 1399/05/24 می باشد");
                BtnNew.Enabled = btnNew2.Enabled = false;
                return;
            }
            if (Utility.GetCurrentUser_AgentId() != Utility.GetCurrentAgentCode())
            {
                ShowMessage("تنها اعضای نمایندگی شیراز قادر به دریافت اولیت ارجاع نظارت شهرداری شیراز می باشند");
                BtnNew.Enabled = btnNew2.Enabled = false;
                return;
            }
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())
                CapacityAssignmentManager.SelectCurrentYearAndStage(1);
            else
                CapacityAssignmentManager.SelectCurrentYearAndStage(0);
            _Year = "";
            if (CapacityAssignmentManager.Count > 0)
            {
                _Year = CapacityAssignmentManager[0]["Year"].ToString();
            }
            TSP.DataManager.TechnicalServices.TSQueueListMunicipalityManager TSQueueListMunicipalityManager = new TSP.DataManager.TechnicalServices.TSQueueListMunicipalityManager();
            TSQueueListMunicipalityManager.FindByID(Utility.GetCurrentUser_MeId(), _Year);
            if (TSQueueListMunicipalityManager.Count > 0)
            {
                DateTime SubmitDateTime = Convert.ToDateTime(TSQueueListMunicipalityManager[0]["SubmitDateTime"]);
                System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                string msg = pc.GetYear(SubmitDateTime) + "/" +
                          pc.GetMonth(SubmitDateTime) + "/" +
                          pc.GetDayOfMonth(SubmitDateTime) +

                          "در ساعت" +
                          pc.GetHour(SubmitDateTime) + ":" +
                          pc.GetMinute(SubmitDateTime) + ":" +
                          pc.GetSecond(SubmitDateTime);
                ShowMessage("شما در سال کاری " + _Year + " تاریخ  اولیت ارجاع کار نظارت شهرداری شیراز را در" + msg + " ثبت کرده اید");
                BtnNew.Enabled = btnNew2.Enabled = false;

            }
            else
            {
                BtnNew.Enabled = BtnNew.Enabled == true;
                lblWarning.Visible = false;
            }

            TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
            ObserverWorkRequestManager.FindByMeId(Utility.GetCurrentUser_MeId());
            if (ObserverWorkRequestManager.Count == 0)
            {
                ShowMessage("شما دارای آماده بکاری در سامانه نمی باشید.ابتدا اقدام به ثبت آماده بکاری نمایید.در صورتی که شهر شما شیراز باشد مجاز به دریافت اولیت ارجاع نظارت شهرداری شیراز خواهید بود");
                BtnNew.Enabled = btnNew2.Enabled = false;
                return;
            }
            if (Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["City1"]) || Convert.ToInt32(ObserverWorkRequestManager[0]["City1"]) != (int)TSP.DataManager.CityCode.Shiraz)
            {
                ShowMessage("در صورتی که شهر انتخابی شما در آماده بکاری، شیراز باشد مجاز به دریافت اولیت ارجاع نظارت شهرداری شیراز خواهید بود");
                BtnNew.Enabled = btnNew2.Enabled = false;
                return;
            }
            if (Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"]) || Convert.ToInt32(ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"]) == 0)
            {
                ShowMessage("متراژ نظارت شهرداری شیراز در آماده بکاری صفر ثبت شده است و مجاز به دریافت اولیت  ارجاع نظارت شهرداری شیراز نمی باشید");
                BtnNew.Enabled = btnNew2.Enabled = false;
            }
            lblObsShirazMunicipality.Text = ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"].ToString();
            lblShirazMunicipalityDesignMeter.Text = ObserverWorkRequestManager[0]["ShirazMunicipalityDesignMeter"].ToString();  
            if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["MasterMfMjParentId"]))
                _MfMjParentId = Convert.ToInt32(ObserverWorkRequestManager[0]["MasterMfMjParentId"]);
            else
                _MfMjParentId = -2;
            int LastMfId = -2;
            if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["MfId"]))
                LastMfId = Convert.ToInt32(ObserverWorkRequestManager[0]["MfId"]);
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            System.Data.DataTable dtMeDetailDesign = DocMemberFileDetailManager.FindByResponsibility(LastMfId, Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.DocumentResponsibilityType.Design, -1, _MfMjParentId);
            System.Data.DataTable dtMeDetailUrbanism = DocMemberFileDetailManager.FindByResponsibility(LastMfId, Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.DocumentResponsibilityType.Urbanism, -1, _MfMjParentId);
            if (dtMeDetailDesign.Rows.Count != 0)
                _DesignDate = Utility.IsDBNullOrNullValue(dtMeDetailDesign.Rows[0]["Date"]) ? "" : dtMeDetailDesign.Rows[0]["Date"].ToString();
            else
                _DesignDate = "";
            if (dtMeDetailUrbanism.Rows.Count != 0)
                _UrbenismDate = Utility.IsDBNullOrNullValue(dtMeDetailUrbanism.Rows[0]["Date"]) ? "" : dtMeDetailUrbanism.Rows[0]["Date"].ToString();
            else
                _UrbenismDate = "";
            _ObsDate = Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ObsDate"]) ? "" : ObserverWorkRequestManager[0]["ObsDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"]))
                _ObsShirazMunicipality = Convert.ToInt32(ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"]);
            if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ShirazMunicipalityDesignMeter"]))
                _DesignShirazMunicipality = Convert.ToInt32(ObserverWorkRequestManager[0]["ShirazMunicipalityDesignMeter"]);
            if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismTarh"]))
                _ShirazMunicipulityUrbenismTarh = Convert.ToInt32(ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismTarh"]);
            if (!Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismEntebaghShahri"]))
                _ShirazMunicipulityUrbenismEntebaghShahri = Convert.ToInt32(ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismEntebaghShahri"]);

            if (Utility.IsWorkREquestInfoSendToShahrdari())
            {
                if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())
                {
                    SendBaseInfoToShahrdari(_MfMjParentId, _ObsDate, _DesignDate, _UrbenismDate);
                }
            }
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        try
        {
            TSP.DataManager.TechnicalServices.TSQueueListMunicipalityManager TSQueueListMunicipalityManager = new TSP.DataManager.TechnicalServices.TSQueueListMunicipalityManager();
            TSQueueListMunicipalityManager.FindByID(Utility.GetCurrentUser_MeId(), _Year);
            if (TSQueueListMunicipalityManager.Count > 0)
            {
                DateTime SubmitDateTime1 = Convert.ToDateTime(TSQueueListMunicipalityManager[0]["SubmitDateTime"]);
                System.Globalization.PersianCalendar pc1 = new System.Globalization.PersianCalendar();
                string msg1 = pc1.GetYear(SubmitDateTime1) + "/" +
                          pc1.GetMonth(SubmitDateTime1) + "/" +
                          pc1.GetDayOfMonth(SubmitDateTime1) +

                          "در ساعت" +
                          pc1.GetHour(SubmitDateTime1) + ":" +
                          pc1.GetMinute(SubmitDateTime1) + ":" +
                          pc1.GetSecond(SubmitDateTime1);
                ShowMessage("شما در سال کاری " + _Year + " تاریخ  اولیت ارجاع کار نظارت شهرداری شیراز را در" + msg1 + " ثبت کرده اید");

                BtnNew.Enabled = btnNew2.Enabled = false;
                return;
            }
            TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
            System.Data.DataTable dtObsReqChange = ObserverWorkRequestChangesManager.SelectLastConfirmRequest(-1, Utility.GetCurrentUser_MeId());
            if (dtObsReqChange.Rows.Count != 1)
            {
                ShowMessage("شما آماده بکاری در سامانه نظام مهندسی ساختمان ثبت نکرده ایت");
                return;
            }
            System.Data.DataRow drQueue = TSQueueListMunicipalityManager.NewRow();
            drQueue["MeId"] = Utility.GetCurrentUser_MeId();
            DateTime SubmitDateTime = DateTime.Now;
            drQueue["SubmitDateTime"] = SubmitDateTime;
            drQueue["Year"] = _Year;
            drQueue["UserId"] = Utility.GetCurrentUser_UserId();
            drQueue["ModifiedDate"] = SubmitDateTime;
            drQueue["InActive"] = 0;
            drQueue["Ip"] = Address;
            drQueue["ObsWorkReqChangeId"] = Convert.ToInt32(dtObsReqChange.Rows[0]["ObsWorkReqChangeId"]);
            TSQueueListMunicipalityManager.AddRow(drQueue);
            TSQueueListMunicipalityManager.Save();
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
            string msg = pc.GetYear(SubmitDateTime) + "/" +
                         pc.GetMonth(SubmitDateTime) + "/" +
                         pc.GetDayOfMonth(SubmitDateTime) +
                         "در ساعت" +
                         pc.GetHour(SubmitDateTime) + ":" +
                         pc.GetMinute(SubmitDateTime) + ":" +
                         pc.GetSecond(SubmitDateTime);
            ShowMessage("اولویت شما در تاریخ " + msg + "با موفقیت ذخیره شد");
            BtnNew.Enabled = btnNew2.Enabled = false;
            string time = SubmitDateTime.TimeOfDay.ToString().Substring(0, 8);
            if (Utility.IsWorkREquestInfoSendToShahrdari())
            {
                if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())
                {
                    ShahrdariWebservice(_MfMjParentId, _ObsDate, _DesignDate, _UrbenismDate
                      , Convert.ToDecimal(_ObsShirazMunicipality)
                     , Convert.ToDecimal(_DesignShirazMunicipality)
                     , Convert.ToDecimal(_ShirazMunicipulityUrbenismTarh) + Convert.ToDecimal(_ShirazMunicipulityUrbenismEntebaghShahri), time, true);
                }
            }
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }

    }
    #region Methods
    void ShowMessage(string str)
    {
        lblWarning.Text = str;
        lblWarning.Visible = true;
    }
    #region WebService Shahrdari
    private void SendBaseInfoToShahrdari(int MjParentId, string ObsDate, string DesignDate, string UrbenismDate)
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
            DataTable dtMember = MemberManager.SelectMemberInfoForWorkRequest(Utility.GetCurrentUser_MeId());
            if (dtMember.Rows.Count != 1)
                return;
            string LastDocRegDate = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["LastDocRegDate"]) ? "" : dtMember.Rows[0]["LastDocRegDate"].ToString();
            MasterMfMjParentId = Convert.ToInt32(dtMember.Rows[0]["MasterMfMjParentId"]);
            ObsId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["ObsId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["ObsId"]);
            DesId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["DesId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["DesId"]);
            MappingId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["MappingId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["MappingId"]);
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
            ClsEngineer1Info.Eng_Info.IdentityCode = Utility.GetCurrentUser_MeId().ToString();//کد عضویت
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

            string Message = "";
            TSP.DataManager.TSEsupWebserviceCallingLogStatus TSEsupWebserviceCallingLogStatus1;
            TSP.DataManager.TSEsupWebserviceCallingLogType TSEsupWebserviceCallingLogType = TSP.DataManager.TSEsupWebserviceCallingLogType.SaveEngInfoQueList;
            try
            {
                WorkRequestsrvEngineerToOthers.ClsEngineer1 ErrorResult = IsrvEngineerToOthersClient.SaveEng_Info("gdyFlNN847tyCqSLnUkm5w==", Utility.GetCurrentUser_MeId().ToString(), ClsEngineer1Info);
                if (ErrorResult.ErrorResult.BizErrors[0] !=null)
                {
                    //ok;     
                    TSEsupWebserviceCallingLogStatus1 = TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
                }
                else
                {
                    TSEsupWebserviceCallingLogStatus1 = TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                    ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
                    BtnNew.Enabled = btnNew2.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                TSEsupWebserviceCallingLogStatus1 = TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                Message = ex.Message;
                Utility.SaveWebsiteError(ex);
                ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
                BtnNew.Enabled = btnNew2.Enabled = false;
            }
            SaveResultCallingWebservice(TSEsupWebserviceCallingLogType, TSEsupWebserviceCallingLogStatus1, Message);
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
            TSP.DataManager.TSEsupWebserviceCallingLogStatus TSEsupWebserviceCallingLogStatus2;
            TSEsupWebserviceCallingLogType = TSP.DataManager.TSEsupWebserviceCallingLogType.SaveJobAgreeMentQueList;
            try
            {
                WorkRequestsrvEngineerToOthers.Eng_JobAgreement ResultJobAgreement = IsrvEngineerToOthersClient.SaveEng_JobAgreement("gdyFlNN847tyCqSLnUkm5w==", Utility.GetCurrentUser_MeId().ToString(), PEngjob);
                if (ResultJobAgreement != null)
                {
                    //ok;
                    TSEsupWebserviceCallingLogStatus2 = TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
                }
                else
                {
                    TSEsupWebserviceCallingLogStatus2 = TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                    ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
                    BtnNew.Enabled = btnNew2.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                TSEsupWebserviceCallingLogStatus2 = TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                Utility.SaveWebsiteError(ex);
                ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
                BtnNew.Enabled = btnNew2.Enabled = false;
            }
            SaveResultCallingWebservice(TSEsupWebserviceCallingLogType, TSEsupWebserviceCallingLogStatus2, Message);
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
            TSP.DataManager.TSEsupWebserviceCallingLogStatus TSEsupWebserviceCallingLogStatus3;
            TSEsupWebserviceCallingLogType = TSP.DataManager.TSEsupWebserviceCallingLogType.SaveCompetenceQueList;
            try
            {
                WorkRequestsrvEngineerToOthers.Eng_Competence[] ResultCompetence = IsrvEngineerToOthersClient.SaveEng_Competence("gdyFlNN847tyCqSLnUkm5w==", Utility.GetCurrentUser_MeId().ToString(), ListengCompetence);
                if (ResultCompetence.Length > 0)
                {
                    //ok;
                    TSEsupWebserviceCallingLogStatus3 = TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
                }
                else
                {
                    TSEsupWebserviceCallingLogStatus3 = TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                    ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
                    BtnNew.Enabled = btnNew2.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                TSEsupWebserviceCallingLogStatus3 = TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                Utility.SaveWebsiteError(ex);
                ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
                BtnNew.Enabled = btnNew2.Enabled = false;
            }
            SaveResultCallingWebservice(TSEsupWebserviceCallingLogType, TSEsupWebserviceCallingLogStatus3, Message);
            #endregion
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            ShowMessage(ex.ToString());
        }
        #endregion

    }
    private void SaveResultCallingWebservice(TSP.DataManager.TSEsupWebserviceCallingLogType TSEsupWebserviceCallingLogType, TSP.DataManager.TSEsupWebserviceCallingLogStatus TSEsupWebserviceCallingLogStatus, string Message)
    {
        try
        {
            TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
            DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
            dr["MeId"] = Utility.GetCurrentUser_MeId();
            dr["ModifiedDate"] = DateTime.Now;
            dr["Type"] = (int)TSEsupWebserviceCallingLogType;
            dr["Status"] = (int)TSEsupWebserviceCallingLogStatus;
            dr["Message"] = Message;
            TSEsupWebserviceCallingLogManager.AddRow(dr);
            TSEsupWebserviceCallingLogManager.Save();
            TSEsupWebserviceCallingLogManager.DataTable.AcceptChanges();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }
    private void ShahrdariWebservice(int MjParentId, string ObsDate, string DesignDate, string UrbenismDate, decimal MunObsCapacity, decimal MunDesignCapacity, decimal MunUrbenismCapacity, string time, Boolean CallSaveQtaInfoMethods)
    {

        //        لیست ClsQtaInputs دارای اطلاعات زیر می باشد
        //CI_Ability : در صورتی که صلاحیت طراحی باشد عدد 2 و صلاحیت نظارت عدد 1 ، صلاحیت مجری عدد 3 ، صلاحیت محاسبه عدد 4
        //Meter: متراژ ظرفیت شیراز
        //date : تاریخ بازگشایی ظرفیت
        //Time  :  ساعت بازگشلیی ظرفیت
        //ChangeBaseDate  : تاریخ تغییر پایه
        try
        {
            if (Utility.IsesupTestServerUse())
            {
                #region TestServer
                WorkRequestsrvEngineerToOthersTest.IsrvEngineerToOthersClient IsrvEngineerToOthersClient = new WorkRequestsrvEngineerToOthersTest.IsrvEngineerToOthersClient();
                #region SendMeInfo           
                int MasterMfMjParentId = -2; int ObsId = -2; ; int DesId = 0; int UrbanismId = 0;
                Int16 ObsGrad = 0; Int16 DesGrad = 0; Int16 UrbanismGrad = 0;
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                DataTable dtMember = MemberManager.SelectMemberInfoForWorkRequest(Utility.GetCurrentUser_MeId());
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
                #region اطلاعات پایه مهندس
                WorkRequestsrvEngineerToOthersTest.ClsEngineer1 ClsEngineer1Info = new WorkRequestsrvEngineerToOthersTest.ClsEngineer1();
                ClsEngineer1Info.Eng_Info = new WorkRequestsrvEngineerToOthersTest.Eng_Info();
                ClsEngineer1Info.Eng_Info.EngName = dtMember.Rows[0]["FirstName"].ToString();
                ClsEngineer1Info.Eng_Info.EngFamily = dtMember.Rows[0]["LastName"].ToString();
                ClsEngineer1Info.Eng_Info.MunicipalityCode = dtMember.Rows[0]["MeNo"].ToString();// کد عضویت نظام مهندسی
                ClsEngineer1Info.Eng_Info.FatherName = dtMember.Rows[0]["FatherName"].ToString();// نام پدر
                ClsEngineer1Info.Eng_Info.BirthDate = dtMember.Rows[0]["BirhtDate"].ToString(); //تاریخ تولد
                ClsEngineer1Info.Eng_Info.IdNo = dtMember.Rows[0]["IdNo"].ToString();//شماره شناسنامه
                ClsEngineer1Info.Eng_Info.Tel = dtMember.Rows[0]["HomeTel"].ToString();// تلفن
                ClsEngineer1Info.Eng_Info.Address = dtMember.Rows[0]["HomeAdr"].ToString();// آدرس
                ClsEngineer1Info.Eng_Info.NationalCode = dtMember.Rows[0]["SSN"].ToString();// کد ملی
                ClsEngineer1Info.Eng_Info.IdentityCode = Utility.GetCurrentUser_MeId().ToString();//کد عضویت
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
                IsrvEngineerToOthersClient.SaveEng_InfoCompleted += new EventHandler<SaveEng_InfoCompletedEventArgs>(GetResultEngineerToOthersTest);
                IsrvEngineerToOthersClient.SaveEng_InfoAsync("gdyFlNN847tyCqSLnUkm5w==", Utility.GetCurrentUser_MeId().ToString(), ClsEngineer1Info);
                #endregion
                #endregion
                int Count = 1;
                if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
                    Count = 2;

                WorkRequestsrvEngineerToOthersTest.ClsQtaInputs[] ListclsQtaInputs = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs[Count];
                //CI_Ability: در صورتی که صلاحیت طراحی باشد عدد 2 و صلاحیت نظارت عدد 1 ، صلاحیت مجری عدد 3 ، صلاحیت محاسبه عدد 4 

                if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
                {
                    #region نظارت و طراحی
                    //**طراحی
                    WorkRequestsrvEngineerToOthersTest.ClsQtaInputs ClsQtaInputsde = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs();
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
                    WorkRequestsrvEngineerToOthersTest.ClsQtaInputs ClsQtaInputsOb = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs();
                    ClsQtaInputsOb.CI_Ability = 1;
                    ClsQtaInputsOb.Date = Utility.GetDateOfToday();
                    ClsQtaInputsOb.Meter = MunObsCapacity;
                    ClsQtaInputsOb.Time = time;
                    ClsQtaInputsOb.ChangeBaseDate = ObsDate;
                    ListclsQtaInputs[1] = ClsQtaInputsOb;
                    #endregion
                }
                else if ((MunDesignCapacity != 0 || MunUrbenismCapacity != 0) && string.IsNullOrEmpty(ObsDate))
                {
                    #region فقظ طراحی یا شهرسازی
                    WorkRequestsrvEngineerToOthersTest.ClsQtaInputs ClsQtaInputsDes = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs();
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
                    WorkRequestsrvEngineerToOthersTest.ClsQtaInputs ClsQtaInputsObs = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs();
                    ClsQtaInputsObs.CI_Ability = 1;
                    ClsQtaInputsObs.Date = Utility.GetDateOfToday();
                    ClsQtaInputsObs.Meter = MunObsCapacity;
                    ClsQtaInputsObs.Time = time;
                    ClsQtaInputsObs.ChangeBaseDate = ObsDate;
                    ListclsQtaInputs[0] = ClsQtaInputsObs;
                    #endregion
                }
                IsrvEngineerToOthersClient.SaveQtaInfoCompleted += new EventHandler<WorkRequestsrvEngineerToOthersTest.SaveQtaInfoCompletedEventArgs>(GetGetResultEngineerToOthersClientTest);
                IsrvEngineerToOthersClient.SaveQtaInfoAsync("gdyFlNN847tyCqSLnUkm5w==", Utility.GetCurrentUser_MeId().ToString(), ListclsQtaInputs, 1, Convert.ToInt16(_Year), (int)TSP.DataManager.TSSafarayanehWebServiceCallingRefrenceType.QueueListForm);


                #endregion
            }
            else
            {
                #region MainServer
                WorkRequestsrvEngineerToOthers.IsrvEngineerToOthersClient IsrvEngineerToOthersClient = new WorkRequestsrvEngineerToOthers.IsrvEngineerToOthersClient();

                int MasterMfMjParentId = -2; int ObsId = -2; ; int DesId = 0; int UrbanismId = 0;
                Int16 ObsGrad = 0; Int16 DesGrad = 0; Int16 UrbanismGrad = 0;
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                DataTable dtMember = MemberManager.SelectMemberInfoForWorkRequest(Utility.GetCurrentUser_MeId());
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
                ClsEngineer1Info.Eng_Info.IdentityCode = Utility.GetCurrentUser_MeId().ToString();//کد عضویت
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
                IsrvEngineerToOthersClient.SaveEng_InfoAsync("gdyFlNN847tyCqSLnUkm5w==", Utility.GetCurrentUser_MeId().ToString(), ClsEngineer1Info);


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
                IsrvEngineerToOthersClient.SaveEng_JobAgreementAsync("gdyFlNN847tyCqSLnUkm5w==", Utility.GetCurrentUser_MeId().ToString(), PEngjob);

                #endregion
                #region اطلاعات جدول صلاحیت
                int Len = 1;
                if (DesGrad > 0 || UrbanismGrad > 0)
                    Len = 2;
                WorkRequestsrvEngineerToOthers.Eng_Competence[] ListengCompetence = new WorkRequestsrvEngineerToOthers.Eng_Competence[Len];
                if (ObsId > 0)
                {

                    WorkRequestsrvEngineerToOthers.Eng_Competence engCompetenceObj = new WorkRequestsrvEngineerToOthers.Eng_Competence();
                    //engCompetenceObj.NIdEng = ClsEngineer1Info.NidEngineer;
                    engCompetenceObj.CI_Ability = 1;
                    engCompetenceObj.CI_Base = ObsGrad;
                    engCompetenceObj.IsActive = true;
                    ListengCompetence[0] = engCompetenceObj;

                }
                if (DesGrad > 0 || UrbanismGrad > 0)
                {
                    WorkRequestsrvEngineerToOthers.Eng_Competence engCompetenceObj = new WorkRequestsrvEngineerToOthers.Eng_Competence();
                    // engCompetenceObj.NIdEng = ClsEngineer1Info.NidEngineer;
                    if (MasterMfMjParentId == (int)TSP.DataManager.MainMajors.Civil)
                        engCompetenceObj.CI_Ability = 4;
                    else
                        engCompetenceObj.CI_Ability = 2;

                    engCompetenceObj.CI_Base = DesGrad > 0 ? DesGrad : UrbanismGrad;
                    engCompetenceObj.IsActive = true;
                    ListengCompetence[1] = engCompetenceObj;
                }
                IsrvEngineerToOthersClient.SaveEng_CompetenceCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveEng_CompetenceCompletedEventArgs>(GetResultCompetence);
                IsrvEngineerToOthersClient.SaveEng_Competence("gdyFlNN847tyCqSLnUkm5w==", Utility.GetCurrentUser_MeId().ToString(), ListengCompetence);

                #endregion
                if (CallSaveQtaInfoMethods)
                {
                    #region اطلاعات ظرفیت مهندس در آماده بکاری
                    int Count = 1;
                    if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
                        Count = 2;
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
                        ClsQtaInputsOb.Date = Utility.GetDateOfToday();
                        ClsQtaInputsOb.Meter = MunObsCapacity;
                        ClsQtaInputsOb.Time = time;
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
                        ClsQtaInputsObs.Date = Utility.GetDateOfToday();
                        ClsQtaInputsObs.Meter = MunObsCapacity;
                        ClsQtaInputsObs.Time = time;
                        ClsQtaInputsObs.ChangeBaseDate = ObsDate;
                        ListclsQtaInputs[0] = ClsQtaInputsObs;
                        #endregion
                    }

                    IsrvEngineerToOthersClient.SaveQtaInfoCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveQtaInfoCompletedEventArgs>(GetGetResultEngineerToOthersClient);
                    IsrvEngineerToOthersClient.SaveQtaInfoAsync("gdyFlNN847tyCqSLnUkm5w==", Utility.GetCurrentUser_MeId().ToString(), ListclsQtaInputs, 1, Convert.ToInt16(_Year), (int)TSP.DataManager.TSSafarayanehWebServiceCallingRefrenceType.QueueListForm);
                    #endregion
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            ShowMessage(ex.ToString());
        }
    }
    #region Test Server Result
    private void GetResultEngineerToOthersTest(object sender, SaveEng_InfoCompletedEventArgs e)
    {
        WorkRequestsrvEngineerToOthersTest.ClsEngineer1 ResultClsEngineer1 = e.Result;
        if (ResultClsEngineer1.ErrorResult.BizErrors[0] != null)
        {
            //ok;
        }
        else
        {
            ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
        }
    }

    private void GetGetResultEngineerToOthersClientTest(object sender, SaveQtaInfoCompletedEventArgs e)
    {
        WorkRequestsrvEngineerToOthersTest.ClsErrorResult ErrorResult = e.Result;
        if (ErrorResult.BizErrors.Length == 0)
        {
            //ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.ارسال اطلاعات به شهرداری انجام شد ");
            //ok;
        }
        else
        {
            ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
        }

    }
    #endregion

    private void GetGetResultEngineerToOthersClient(object sender, WorkRequestsrvEngineerToOthers.SaveQtaInfoCompletedEventArgs e)
    {
        try
        {
            TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
            DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
            try
            {
                dr["MeId"] = Utility.GetCurrentUser_MeId();
                dr["ModifiedDate"] = DateTime.Now;
                dr["Type"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogType.SaveQtaInfoQueList;
                WorkRequestsrvEngineerToOthers.ClsErrorResult ErrorResult = e.Result;
                if (ErrorResult.BizErrors.Length == 0)
                {
                    //ok;     
                    dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
                }
                else
                {
                    dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                    ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
                }
            }
            catch (Exception ex)
            {
                dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                try
                {
                    if (!Utility.IsDBNullOrNullValue(e.Error) && !Utility.IsDBNullOrNullValue(e.Error.InnerException) && !Utility.IsDBNullOrNullValue(e.Error.InnerException.Message))
                        dr["Message"] = e.Error.InnerException.Message;
                }
                catch (Exception exp)
                {

                    Utility.SaveWebsiteError(exp);
                    ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
                }
                Utility.SaveWebsiteError(ex);
                ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
            }
            TSEsupWebserviceCallingLogManager.AddRow(dr);
            TSEsupWebserviceCallingLogManager.Save();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }

    private void GetResultEngineerToOthers(object sender, WorkRequestsrvEngineerToOthers.SaveEng_InfoCompletedEventArgs e)
    {
        try
        {
            TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
            DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
            try
            {
                dr["MeId"] = Utility.GetCurrentUser_MeId();
                dr["ModifiedDate"] = DateTime.Now;
                dr["Type"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogType.SaveEngInfoQueList;
                WorkRequestsrvEngineerToOthers.ClsEngineer1 ResultClsEngineer1 = e.Result;
                if (ResultClsEngineer1.ErrorResult.BizErrors[0] != null)
                {
                    //ok;
                    dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
                }
                else
                {
                    dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                    ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
                }
            }
            catch (Exception ex)
            {
                dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                try
                {
                    if (!Utility.IsDBNullOrNullValue(e.Error) && !Utility.IsDBNullOrNullValue(e.Error.InnerException) && !Utility.IsDBNullOrNullValue(e.Error.InnerException.Message))
                        dr["Message"] = e.Error.InnerException.Message;
                }
                catch (Exception exp)
                {

                    Utility.SaveWebsiteError(exp);
                    ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
                }
                Utility.SaveWebsiteError(ex);
            }
            TSEsupWebserviceCallingLogManager.AddRow(dr);
            TSEsupWebserviceCallingLogManager.Save();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
        }
    }

    private void GetResultJobAgreement(object sender, WorkRequestsrvEngineerToOthers.SaveEng_JobAgreementCompletedEventArgs e)
    {
        TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
        DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
        try
        {
            dr["MeId"] = Utility.GetCurrentUser_MeId();
            dr["ModifiedDate"] = DateTime.Now;
            dr["Type"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogType.SaveJobAgreeMentQueList;
            WorkRequestsrvEngineerToOthers.Eng_JobAgreement ResultJobAgreement = e.Result;
            if (ResultJobAgreement != null)
            {
                //ok;
                dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
            }
            else
            {
                dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
            }
        }
        catch (Exception ex)
        {
            dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
            try
            {
                if (!Utility.IsDBNullOrNullValue(e.Error) && !Utility.IsDBNullOrNullValue(e.Error.InnerException) && !Utility.IsDBNullOrNullValue(e.Error.InnerException.Message))
                    dr["Message"] = e.Error.InnerException.Message;
            }
            catch (Exception exp)
            {

                Utility.SaveWebsiteError(exp);
                ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
            }
            Utility.SaveWebsiteError(ex);
            ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
        }
        TSEsupWebserviceCallingLogManager.AddRow(dr);
        TSEsupWebserviceCallingLogManager.Save();
    }

    private void GetResultCompetence(object sender, WorkRequestsrvEngineerToOthers.SaveEng_CompetenceCompletedEventArgs e)
    {
        TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
        DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
        try
        {
            dr["MeId"] = Utility.GetCurrentUser_MeId();
            dr["ModifiedDate"] = DateTime.Now;
            dr["Type"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogType.SaveCompetenceQueList;
            WorkRequestsrvEngineerToOthers.Eng_Competence[] ResultCompetence = e.Result;
            if (ResultCompetence.Length > 0)
            {
                //ok;
                dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
            }
            else
            {
                dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
            }
        }
        catch (Exception ex)
        {
            dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
            try
            {
                if (!Utility.IsDBNullOrNullValue(e.Error) && !Utility.IsDBNullOrNullValue(e.Error.InnerException) && !Utility.IsDBNullOrNullValue(e.Error.InnerException.Message))
                    dr["Message"] = e.Error.InnerException.Message;
            }
            catch (Exception exp)
            {

                Utility.SaveWebsiteError(exp);
                ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
            }
            Utility.SaveWebsiteError(ex);
            ShowMessage("خطا در ارسال اطلاعات در وب سرویس شهرداری ایجاد شده است.لطفا زمانی دیگر مجددا تلاش نمایید");
        }
        TSEsupWebserviceCallingLogManager.AddRow(dr);
        TSEsupWebserviceCallingLogManager.Save();
    }
    #endregion
    #endregion
}