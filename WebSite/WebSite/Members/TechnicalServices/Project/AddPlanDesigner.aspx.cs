using DevExpress.Web;
using System;
using System.Collections;
using System.Data;
using System.IO;

public partial class Members_TechnicalServices_Project_AddPlanDesigner : System.Web.UI.Page
{
    #region Properties
    Boolean IsPageRefresh = false;
    private int _AccType
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["AccType"]);
        }
        set
        {
            HiddenFieldPrjDes["AccType"] = value.ToString();
        }
    }
    private int _PlansId
    {
        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldPrjDes["PlansId"]);
            }
            catch (Exception ex)
            {
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PlnId"]));
            }
        }

        set
        {
            HiddenFieldPrjDes["PlansId"] = value.ToString();
        }
    }
    private int _PlansTypeId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["PlansTypeId"]);
        }
        set
        {
            HiddenFieldPrjDes["PlansTypeId"] = value.ToString();
        }
    }

    private int _DesignerPlansId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["DesignerPlansId"]);
        }
        set
        {
            HiddenFieldPrjDes["DesignerPlansId"] = value.ToString();
        }
    }
    string _PageMode
    {
        get
        {
            return HiddenFieldPrjDes["PageMode"].ToString();
        }
        set
        {
            HiddenFieldPrjDes["PageMode"] = value;
        }
    }
    private int _PrjDesignerId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["PrjDesignerId"]);
        }
        set
        {
            HiddenFieldPrjDes["PrjDesignerId"] = value.ToString();
        }
    }
    private int _PrjReqId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["PrjReqId"]);
        }
        set
        {
            HiddenFieldPrjDes["PrjReqId"] = value.ToString();
        }
    }

    private int _PrjId
    {

        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldPrjDes["PrjId"]);
            }
            catch (Exception ex)
            {
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["ProjectId"]));
            }
        }
        set
        {
            HiddenFieldPrjDes["PrjId"] = value.ToString();
        }

    }
    private int _Foundation
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["Foundation"]);
        }
        set
        {
            HiddenFieldPrjDes["Foundation"] = value.ToString();
        }
    }
    private int _FundationDifference
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["FundationDifference"]);
        }
        set
        {
            HiddenFieldPrjDes["FundationDifference"] = value.ToString();
        }
    }
    private int _FoundationMixSkeletonSaze
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["FoundationMixSkeletonSaze"]);
        }
        set
        {
            HiddenFieldPrjDes["FoundationMixSkeletonSaze"] = value.ToString();
        }
    }

    private int _GroupId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["GroupId"]);
        }
        set
        {
            HiddenFieldPrjDes["GroupId"] = value.ToString();
        }
    }

    private int _StructureSkeletonId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["StructureSkeletonId"]);
        }
        set
        {
            HiddenFieldPrjDes["StructureSkeletonId"] = value.ToString();
        }
    }
    private int _CitId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["CitId"]);
        }
        set
        {
            HiddenFieldPrjDes["CitId"] = value.ToString();
        }
    }

    private int _IsCharity
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["IsCharity"]);
        }
        set
        {
            HiddenFieldPrjDes["IsCharity"] = value.ToString();
        }
    }
    private int _IsBonyadMaskan
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["IsBonyadMaskan"]);
        }
        set
        {
            HiddenFieldPrjDes["IsBonyadMaskan"] = value.ToString();
        }
    }
    private int _ObsWorkReqChangeId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["ObsWorkReqChangeId"]);
        }
        set
        {
            HiddenFieldPrjDes["ObsWorkReqChangeId"] = value.ToString();
        }
    }

    private Boolean _HasOffice
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldPrjDes["HasOffice"]);
        }
        set
        {
            HiddenFieldPrjDes["HasOffice"] = value.ToString();
        }
    }
    private Boolean _HasEngOffice
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldPrjDes["HasEngOffice"]);
        }
        set
        {
            HiddenFieldPrjDes["HasEngOffice"] = value.ToString();
        }
    }
    private int _OfId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["OfId"]);
        }
        set
        {
            HiddenFieldPrjDes["OfId"] = value.ToString();
        }
    }
    private int _EngOfId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["EngOfId"]);
        }
        set
        {
            HiddenFieldPrjDes["EngOfId"] = value.ToString();
        }
    }

    private string _CurrentCapacityAssignmentYear
    {
        get
        {
            return HiddenFieldPrjDes["CurrentCapacityAssignmentYear"].ToString();
        }
        set
        {
            HiddenFieldPrjDes["CurrentCapacityAssignmentYear"] = value.ToString();
        }
    }

    private int _CurrentPrjTaskCode
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["CurrentPrjTaskCode"]);
        }
        set
        {
            HiddenFieldPrjDes["CurrentPrjTaskCode"] = value.ToString();
        }
    }
    private int _MeMajorParentIdInWorkReq
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["MeMajorParentIdInWorkReq"]);
        }
        set
        {
            HiddenFieldPrjDes["MeMajorParentIdInWorkReq"] = value.ToString();
        }
    }
    private Boolean _IsCiviObserver
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldPrjDes["IsCiviObserver"]);
        }
        set
        {
            HiddenFieldPrjDes["IsCiviObserver"] = value.ToString();
        }
    }
    private int _AgentId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["AgentId"]);
        }
        set
        {
            HiddenFieldPrjDes["AgentId"] = value.ToString();
        }
    }
    private Boolean _CanObserverBeDesigner
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldPrjDes["CanObserverBeDesigner"]);
        }
        set
        {
            HiddenFieldPrjDes["CanObserverBeDesigner"] = value.ToString();
        }
    }

    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        #region Page Refresh
        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }
        #endregion
        if (!IsPostBack)
        {
            Session["SendBackDataTable_EmpPrjAddDes"] = "";
            Session["PlansAttachName"] = null;
            Session["PlanAttachStatus"] = null;
            Session["AttachmentsManager"] = CreateAttachmentsManager();
            SetKey();
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["btnSave"] = btnSave.Enabled;
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["btnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["btnSave"];

        this.comboYear.Enabled = false;
        cmbDesMeType.Enabled = false;
        ChbIsExteraFloor.Visible = false;//طراح اضافه اشکوب ندارد.در صورتی که اضافه شد.از آن استفاده می کنیم.:)
        RoundPanelMemberEngOfficeInfo.Visible = UserControlMeEngOfficeInfoUserControl.Visible = UserControlMeOfficeInfoUserControl.Visible = false;
        SetAttachTypeFilterExpression();
    }

    #region Btn Click

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        _PageMode = "Edit";
        SetEditModeKeys();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (cmbPriceArchive.Value == null)
        {
            SetLabelWarning("ثبت تعرفه خدمات مهندسی اجباری می باشد.");
            return;
        }
        switch (_PageMode)
        {
            case "New":
                Insert();
                break;
            case "Edit":
                Update();
                break;
            case "NewPlanReq":
                Update();
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberPlans.aspx");
    }
    #endregion

    protected void GridViewAttachment_PageIndexChanged(object sender, EventArgs e)
    {
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager = (TSP.DataManager.TechnicalServices.AttachmentsManager)Session["AttachmentsManager"];
        GridViewAttachment.DataSource = AttachmentsManager.DataTable;
        GridViewAttachment.KeyFieldName = "AttachmentId";
        GridViewAttachment.DataBind();
    }

    #region Plan
    protected void cmbAttachType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["PlanAttachStatus"] = "";
        Session["PlansAttachName"] = "";
        switch ((int)cmbAttachType.Value)
        {
            case (int)TSP.DataManager.TSAttachType.Plan:
                AllowedFileExt.InnerText = "ساختار فایل ها (format) جهت نقشه باید pdf یا dwg یا zip باشد";
                flpFile.ValidationSettings.AllowedFileExtensions = new string[] { ".pdf,.dwg" };
                break;
            case (int)TSP.DataManager.TSAttachType.PlansMethod:
                AllowedFileExt.InnerText = "ساختار فایل (file format) دستور نقشه باید pdf یا jpg باشد.";
                flpFile.ValidationSettings.AllowedFileExtensions = new string[] { ".pdf,.jpg" };
                break;
            case (int)TSP.DataManager.TSAttachType.ArchContract:
                AllowedFileExt.InnerText = "ساختار فایل (file format) قرارداد طراحی معماری باید pdf باشد.";
                flpFile.ValidationSettings.AllowedFileExtensions = new string[] { ".pdf" };
                break;
            case (int)TSP.DataManager.TSAttachType.FormNo5:
                AllowedFileExt.InnerText = "ساختار فایل (file format) فرم شماره 5 باید pdf یا jpg باشد.";
                flpFile.ValidationSettings.AllowedFileExtensions = new string[] { ".pdf,.jpg" };
                break;
            case (int)TSP.DataManager.TSAttachType.ElectInstalContract:
                AllowedFileExt.InnerText = "ساختار فایل (file format) قرارداد طراحی تاسیسات باید pdf باشد.";
                flpFile.ValidationSettings.AllowedFileExtensions = new string[] { ".pdf" };
                break;
            case (int)TSP.DataManager.TSAttachType.FormNo6:
                AllowedFileExt.InnerText = "ساختار فایل (file format) فرم شماره 6 باید pdf یا jpg باشد.";
                flpFile.ValidationSettings.AllowedFileExtensions = new string[] { ".pdf,.jpg" };
                break;
            case (int)TSP.DataManager.TSAttachType.MechanInstalContract:
                AllowedFileExt.InnerText = "ساختار فایل (file format) قرارداد طراحی تاسیسات باید pdf باشد.";
                flpFile.ValidationSettings.AllowedFileExtensions = new string[] { ".pdf" };
                break;
            case (int)TSP.DataManager.TSAttachType.PlanBooklet:
                AllowedFileExt.InnerText = "ساختار فایل (file format) خلاصه دفترچه محاسباتی باید pdf باشد.";
                flpFile.ValidationSettings.AllowedFileExtensions = new string[] { ".pdf" };
                break;
            case (int)TSP.DataManager.TSAttachType.StructureContract:
                AllowedFileExt.InnerText = "ساختار فایل (file format) قرارداد طراحی و محاسبه سازه باید pdf باشد.";
                flpFile.ValidationSettings.AllowedFileExtensions = new string[] { ".pdf" };
                break;
            case (int)TSP.DataManager.TSAttachType.CalculationFile:
                AllowedFileExt.InnerText = "ساختار فایل های (file format) محاسباتی سازه باید edb یا e2k یا fdb یا f2k باشد.";
                flpFile.ValidationSettings.AllowedFileExtensions = new string[] { ".edb,.e2k,.fdb,.f2k" };
                break;
        }
    }
    protected void btnSaveAttachment_Click(object sender, EventArgs e)
    {
        AttachRowInserting();
    }
    protected void flpFile_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {

        try
        {
            e.CallbackData = SaveImage(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected void GridViewAttachment_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager = (TSP.DataManager.TechnicalServices.AttachmentsManager)Session["AttachmentsManager"];

        DataRow AttachmentRow = AttachmentsManager.DataTable.Rows.Find(e.Keys["AttachmentId"]);
        AttachmentRow.Delete();

        e.Cancel = true;

        GridViewAttachment.CancelEdit();

        GridViewAttachment.DataSource = AttachmentsManager.DataTable;
        GridViewAttachment.KeyFieldName = "AttachmentId";
        GridViewAttachment.DataBind();
    }
    #endregion
    #endregion

    #region Methods

    #region SetKeys
    private void SetKey()
    {
        try
        {
            _AccType = -2;
            _PrjId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["ProjectId"]));
            _PrjReqId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrjReId"]));
            _PrjDesignerId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrjDesignerId"]));
            _PlansId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PlnId"].ToString()));
            _PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);
            _DesignerPlansId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["DsPId"]));
            _PlansTypeId = -1;
            _IsCiviObserver = false;
            if (string.IsNullOrEmpty(_PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            FillProjectInfo(_PrjReqId);
            #region تنظیم سال کاری و تعرفه به آخرین سال و تعرفه   
            if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())
                ObjectCapacityAssignment.SelectParameters["IsMainAgent"].DefaultValue = "1";
            else
                ObjectCapacityAssignment.SelectParameters["IsMainAgent"].DefaultValue = "0";
            comboYear.DataBind();
            comboYear.SelectedIndex = 0;
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            if (Utility.GetCurrentUser_AgentId() == Utility.GetCurrentAgentCode())
                CapacityAssignmentManager.SelectCurrentYearAndStage(1);
            else
                CapacityAssignmentManager.SelectCurrentYearAndStage(0);
            if (CapacityAssignmentManager.Count > 0)
            {
                _CurrentCapacityAssignmentYear = CapacityAssignmentManager[0]["Year"].ToString();
            }
            #endregion

            FillCapacityInfo();

            ObjectDataSourceMemberType.FilterExpression = "MemberTypeId=" + (int)TSP.DataManager.TSMemberType.Office
                + " OR MemberTypeId=" + (int)TSP.DataManager.TSMemberType.EngOffice
                + " OR MemberTypeId=" + (int)TSP.DataManager.TSMemberType.Member
                + " OR MemberTypeId=" + (int)TSP.DataManager.TSMemberType.OtherPerson;

            SetMode(_PageMode);
            CheckWorkFlowPermissionForSave(_PageMode, _CurrentPrjTaskCode);
            cmbPlanType2.Visible = cmbPlanType.Visible = true;
            lblcmbPlanType.Visible = true;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetLabelWarning("خطا در بازیابی اطلاعات ایجاد شده است");
            return;
        }

        EPaymentUC.SetEPaymentParameters(_AccType
                                      , TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Designer)
                                      , "EPayment", Request.Form["paymentId"] != null ? Convert.ToInt32(Request.Form["paymentId"]) : -1
                                      , Request.Form["resultCode"] != null ? Request.Form["resultCode"] : "-1"
                                      , Request.Form["referenceId"] != null ? Request.Form["referenceId"] : "-1", Request.Form["token"] != null ? Request.Form["token"] : "", _CitId);

    }

    private void SetMode(string PageMode)
    {
        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "New":
                SetNewModeKeys();
                break;
            case "Edit":
                SetEditModeKeys();
                break;
            case "NewPlanReq":
                SetNewPlanReqModeKeys();
                break;
        }
    }
    private void SetNewModeKeys()
    {
        ClearAllFormInfo();
        SetControlsEnable(true);
        PanelControlerVoiewPoint.Visible = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        RoundPanelDes.HeaderText = "جدید";
        if (!FillMemberInfo(Utility.GetCurrentUser_MeId()))
            return;
        if (!SetPlanTypeIdAndPlanInfo())
            return;
        setAccTypeByPlansTypeId();
    }

    private void SetEditModeKeys()
    {
        FillForm(_PrjDesignerId);
        SetControlsEnable(true);
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        cmbDesMeType.Enabled = false;
        RoundPanelDes.HeaderText = "ویرایش";
        setAccTypeByPlansTypeId();

    }
    private void SetNewPlanReqModeKeys()
    {
        FillForm(_PrjDesignerId);
        SetControlsEnable(true);
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        cmbDesMeType.Enabled = false;
        RoundPanelDes.HeaderText = "نسخه جدید نقشه";
        if (!FillMemberInfo(Utility.GetCurrentUser_MeId()))
            return;
        if (!SetPlanTypeIdAndPlanInfo())
            return;
        setAccTypeByPlansTypeId();
    }

    private void SetViewModeKeys()
    {
        FillForm(_PrjDesignerId);
        SetControlsEnable(false);
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        RoundPanelDes.HeaderText = "مشاهده";
        setAccTypeByPlansTypeId();
    }

    private void SetAttachTypeFilterExpression()
    {
        switch (_PlansTypeId)
        {
            case (int)TSP.DataManager.TSPlansType.Memari:
                ObjdsAttachType.FilterExpression =
                "AttachTypeId=" + (int)TSP.DataManager.TSAttachType.Plan
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.PlansMethod
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.ArchContract
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.FormNo5;
                TasisatNotification.Visible = false;
                ArchNotification.Visible = true;
                break;

            case (int)TSP.DataManager.TSPlansType.TasisatBargh:
                ObjdsAttachType.FilterExpression =
                "AttachTypeId=" + (int)TSP.DataManager.TSAttachType.Plan
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.ElectInstalContract
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.FormNo6;
                TasisatNotification.Visible = true;
                ArchNotification.Visible = false;

                break;

            case (int)TSP.DataManager.TSPlansType.TasisatMechanic:
                ObjdsAttachType.FilterExpression =
                "AttachTypeId=" + (int)TSP.DataManager.TSAttachType.Plan
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.MechanInstalContract
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.FormNo6;
                TasisatNotification.Visible = true;
                ArchNotification.Visible = false;
                break;

            case (int)TSP.DataManager.TSPlansType.Sazeh:
                ObjdsAttachType.FilterExpression =
                "AttachTypeId=" + (int)TSP.DataManager.TSAttachType.Plan
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.PlanBooklet
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.StructureContract
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.CalculationFile;
                TasisatNotification.Visible = false;
                ArchNotification.Visible = false;
                break;

            default:
                ObjdsAttachType.FilterExpression = "AttachTypeId=-2";
                TasisatNotification.Visible = false;
                ArchNotification.Visible = false;
                break;
        }
    }

    private Boolean SetPlanTypeIdAndPlanInfo()
    {
        #region تنظیم نوع نقشه Set the Type of plan by current state of ProjectWF====>in Employee's Portal,it is a method and it's name is CheckAndSetPlanTypeValue(_CurrentPrjTaskCode);
        switch (_MeMajorParentIdInWorkReq)
        {
            case (int)TSP.DataManager.MainMajors.Architecture:
                if (_CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject)
                {
                    _PlansTypeId = -1;
                    SetLabelWarning("با توجه به رشته آماده بکاری شما در این مرحله از گردش کار پروژه قادر به ثبت نقشه و کارکرد طراحی خود نمی باشید.مرحله گردش کار پروژه بایستی با نوع نقشه شما تناسب داشته باشد.");
                    return false;
                }
                _PlansTypeId = (int)TSP.DataManager.TSPlansType.Memari;
                break;
            case (int)TSP.DataManager.MainMajors.Civil:
                if (_IsCiviObserver)
                {
                    if (_CurrentPrjTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject)
                    {

                        _PlansTypeId = (int)TSP.DataManager.TSPlansType.Memari;

                    }
                    else if (_CurrentPrjTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject || _CurrentPrjTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject)
                    {

                        _PlansTypeId = (int)TSP.DataManager.TSPlansType.Sazeh;
                    }
                    else
                    {
                        _PlansTypeId = -1;
                        SetLabelWarning("با توجه به رشته آماده بکاری شما در این مرحله از گردش کار پروژه قادر به ثبت نقشه و کارکرد طراحی خود نمی باشید.مرحله گردش کار پروژه بایستی با نوع نقشه شما تناسب داشته باشد.");
                        return false;
                    }
                }
                else
                {
                    if (_CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject && _CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject)
                    {
                        _PlansTypeId = -1;
                        SetLabelWarning("با توجه به رشته آماده بکاری شما در این مرحله از گردش کار پروژه قادر به ثبت نقشه و کارکرد طراحی خود نمی باشید.مرحله گردش کار پروژه بایستی با نوع نقشه شما تناسب داشته باشد.");
                        return false;
                    }
                    _PlansTypeId = (int)TSP.DataManager.TSPlansType.Sazeh;

                }
                break;
            case (int)TSP.DataManager.MainMajors.Electronic:
                if (_CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject && _CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject)
                {
                    _PlansTypeId = -1;
                    SetLabelWarning("با توجه به رشته آماده بکاری شما در این مرحله از گردش کار پروژه قادر به ثبت نقشه و کارکرد طراحی خود نمی باشید.مرحله گردش کار پروژه بایستی با نوع نقشه شما تناسب داشته باشد.");
                    return false;
                }
                _PlansTypeId = (int)TSP.DataManager.TSPlansType.TasisatBargh;
                break;
            case (int)TSP.DataManager.MainMajors.Mechanic:
                if (_CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject && _CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject)
                {
                    _PlansTypeId = -1;
                    SetLabelWarning("با توجه به رشته آماده بکاری شما در این مرحله از گردش کار پروژه قادر به ثبت نقشه و کارکرد طراحی خود نمی باشید.مرحله گردش کار پروژه بایستی با نوع نقشه شما تناسب داشته باشد.");
                    return false;
                }
                _PlansTypeId = (int)TSP.DataManager.TSPlansType.TasisatMechanic;
                break;
            default:
                _PlansTypeId = -1;
                SetLabelWarning("رشته آماده بکاری شما در سیستم یافت نشد");
                return false;
                break;
        }
        if (_PlansTypeId != -1)
        {
            cmbPlanType.DataBind();
            cmbPlanType2.DataBind();
            cmbPlanType2.SelectedIndex = cmbPlanType.SelectedIndex = cmbPlanType.Items.FindByValue(_PlansTypeId.ToString()).Index;
        }

        #endregion
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        PlansManager.SelectTSPlansByProjectAndRequest(_PrjId, _PlansTypeId, 0, _PrjReqId);
        if (PlansManager.Count > 0)
        {
            _PlansId = Convert.ToInt32(PlansManager[0]["PlansId"]);
            FillPlan(_PlansId);
        }
        return true;
    }
    private void setAccTypeByPlansTypeId()
    {
        switch (_PlansTypeId)
        {
            case (int)TSP.DataManager.TSPlansType.Memari:
                _AccType = (int)TSP.DataManager.TSAccountingAccType.Designing5Percent;
                break;
            case (int)TSP.DataManager.TSPlansType.Sazeh:
                _AccType = (int)TSP.DataManager.TSAccountingAccType.Designing5PercentStructure;
                break;
            case (int)TSP.DataManager.TSPlansType.TasisatBargh:
            case (int)TSP.DataManager.TSPlansType.TasisatMechanic:
                _AccType = (int)TSP.DataManager.TSAccountingAccType.Designing5PercentInstalation;
                break;
            default:
                _AccType = -2;
                SetLabelWarning("خطا در بازخوانی اطلاعات فیش ایجاد شده است");
                break;
        }
    }
    #endregion

    #region Fill Designer Info (*****FillForm*****)
    private Boolean FillMemberInfo(int MeId)
    {
        try
        {
            RoundPanelMemberEngOfficeInfo.Visible = UserControlMeEngOfficeInfoUserControl.Visible = UserControlMeOfficeInfoUserControl.Visible = false;
            if (Utility.IsDBNullOrNullValue(comboYear.SelectedItem))
            {
                SetLabelWarning("سال کاری انتخاب نشده است");
                return false;
            }
            if (string.Compare(_CurrentCapacityAssignmentYear, comboYear.SelectedItem.Text) <= 0)//اگر سال کاری کوچکتر از سال کاری جاری باشد.ظرفیت ها از امسال کسر نمی شود و فقط ثبت می شود
            {
                WorkRequestUserControl.SetUserControlVisible(true);
                WorkRequestUserControl.UserControlvisible = true;
                if (!WorkRequestUserControl.CheckConditions(MeId, TSP.DataManager.TSProjectIngridientType.Designer, -1))
                {
                    ClearAllFormInfo();
                    SetLabelWarning(WorkRequestUserControl.ErrorMessage);
                    return false;
                }

            }
            WorkRequestUserControl.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Designer;
            if (!WorkRequestUserControl.FillForm(MeId.ToString(), (TSP.DataManager.TSMemberType)(Convert.ToInt16(CmbMembershipType.SelectedItem.Value))))
            {
                ClearAllFormInfo();
                SetLabelWarning(WorkRequestUserControl.ErrorMessage);
                return false;
            }
            if (_CanObserverBeDesigner
                   && WorkRequestUserControl.IsCiviObserver//***** رشته عمران می تواند طراحی معماری الف بنایی را انجام بدهد 
                    && ((_GroupId == (int)TSP.DataManager.TSStructureGroups.A && _StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Ajory)
                    || (_GroupId == (int)TSP.DataManager.TSStructureGroups.A && _StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Mix)))
            {
                _IsCiviObserver = true;
            }
            if (CmbMembershipType.SelectedIndex == 0)//عضو حقیقی
            {
                _MeMajorParentIdInWorkReq = WorkRequestUserControl.MajorParentIdInWorkReq;
                txtSearchFileNo.Text = WorkRequestUserControl.FileNo;
                txtDocProvCode.Text = WorkRequestUserControl.FileNo.Substring(0, 2);
                txtMjCode.Text = WorkRequestUserControl.FileNo.Substring(3, 3);
                txtDocSerialNo.Text = Convert.ToInt32(WorkRequestUserControl.FileNo.Substring(7)).ToString();
                PanelDocumentFileNo.ClientVisible = true;
                #region اگر عضو دفتر باشد
                UserControlMeEngOfficeInfoUserControl.FillInfo(Convert.ToInt32(MeId));
                Boolean IsEngOfficeIsExpired = UserControlMeEngOfficeInfoUserControl.IsExpired;
                Boolean HasEngOffice = _HasEngOffice = UserControlMeEngOfficeInfoUserControl.HasEngOffice;
                int EngOfId = _EngOfId = UserControlMeEngOfficeInfoUserControl.EngOfId;
                RoundPanelMemberEngOfficeInfo.Visible = UserControlMeEngOfficeInfoUserControl.Visible = HasEngOffice;
                if (HasEngOffice)
                {
                    txtMeIdSearch.Text = MeId.ToString();
                    cmbDesMeType.DataBind();
                    cmbDesMeType.SelectedIndex = cmbDesMeType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.EngOffice).ToString()).Index;
                    FillProjectIngridientCapacityInfo(TSP.DataManager.TSMemberType.Member, EngOfId, Convert.ToInt32(MeId));
                    return true;
                }
                #endregion
                #region اگر عضو شرکت باشد            
                UserControlMeOfficeInfoUserControl.FillInfo(Convert.ToInt32(MeId));
                Boolean IsOfficeIsExpired = UserControlMeOfficeInfoUserControl.IsExpired;
                Boolean HasOffice = _HasOffice = UserControlMeOfficeInfoUserControl.HasOffice;
                int OfId = _OfId = UserControlMeOfficeInfoUserControl.OfId;
                RoundPanelMemberEngOfficeInfo.Visible = UserControlMeOfficeInfoUserControl.Visible = HasOffice;
                if (HasOffice)
                {
                    txtMeIdSearch.Text = MeId.ToString();
                    cmbDesMeType.DataBind();
                    cmbDesMeType.SelectedIndex = cmbDesMeType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.Office).ToString()).Index;
                    FillProjectIngridientCapacityInfo(TSP.DataManager.TSMemberType.Member, OfId, Convert.ToInt32(MeId));
                    return true;
                }
                #endregion
                #region اگر عضو ناظر رشته عمران باشد 
                if (WorkRequestUserControl.IsCiviObserver//***** رشته عمران می تواند طراحی معماری الف بنایی را انجام بدهد  
                    && ((_GroupId == (int)TSP.DataManager.TSStructureGroups.A && _StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Ajory)
                    || (_GroupId == (int)TSP.DataManager.TSStructureGroups.A && _StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Mix)))
                {
                    txtMeIdSearch.Text = MeId.ToString();
                    cmbDesMeType.DataBind();
                    cmbDesMeType.SelectedIndex = cmbDesMeType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.Member).ToString()).Index;
                    FillProjectIngridientCapacityInfo(TSP.DataManager.TSMemberType.Member, -1, Convert.ToInt32(MeId));
                    _IsCiviObserver = true;
                    return true;
                }
                #endregion
                SetLabelWarning("عضو انتخاب شده عضو دفتر و یا شرکت و یا رشته عمران/معماری با صلاحیت مورد نیاز نمی باشد");
                ClearAllFormInfo();
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInProcess));
        }
        return false;
    }
    /// <summary>
    /// پر کردن اطلاعات طراح
    /// Call in "SetEditModeKeys" & "SetViewModeKeys"
    /// </summary>
    private void FillForm(int PrjDesignerId)
    {
        int MeId = -2;

        RoundPanelMemberEngOfficeInfo.Visible = UserControlMeEngOfficeInfoUserControl.Visible = UserControlMeOfficeInfoUserControl.Visible = false;
        TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        ProjectDesignerManager.FindByPrjDesignerId(PrjDesignerId);
        if (ProjectDesignerManager.Count <= 0)
            return;
        cmbDesMeType.Value = ProjectDesignerManager[0]["MemberTypeId"].ToString();
        if (!Utility.IsDBNullOrNullValue(ProjectDesignerManager[0]["PriceArchiveId"]))
        {
            cmbPriceArchive.DataBind();
            cmbPriceArchive.SelectedIndex = cmbPriceArchive.Items.FindByValue(ProjectDesignerManager[0]["PriceArchiveId"].ToString()).Index;
        }
        if (!Utility.IsDBNullOrNullValue(ProjectDesignerManager[0]["IsExteraFloor"]))
        {
            ChbIsExteraFloor.Checked = Convert.ToBoolean(ProjectDesignerManager[0]["IsExteraFloor"]);
        }
        else
            ChbIsExteraFloor.Checked = false;
        if (!Utility.IsDBNullOrNullValue(ProjectDesignerManager[0]["Year"]))
            comboYear.SelectedIndex = comboYear.Items.FindByText(ProjectDesignerManager[0]["Year"].ToString()).Index;
        else
            comboYear.SelectedIndex = 0;
        int DesignerType = Convert.ToInt32(cmbDesMeType.Value);
        int OfficeEngOId = Convert.ToInt32(ProjectDesignerManager[0]["OfficeEngOId"]);
        #region عضو نظام مهندسی
        PanelDocumentFileNo.ClientVisible = true;
        CmbMembershipType.SelectedIndex = CmbMembershipType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.Member).ToString()).Index;
        if (DesignerType != (int)TSP.DataManager.TSMemberType.Member)
        {
            TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
            ProjectOfficeMembersManager.FindByIngridientTypeAndPrjImpObsDsgnId((int)TSP.DataManager.TSProjectIngridientType.Designer, _PrjDesignerId);
            if (ProjectOfficeMembersManager.Count <= 0)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            MeId = Convert.ToInt32(ProjectOfficeMembersManager[0]["MeOthPId"]);
        }
        else
        {
            MeId = OfficeEngOId;
        }
        txtMeIdSearch.Text = MeId.ToString();

        #region اگر عضو دفتر باشد
        UserControlMeEngOfficeInfoUserControl.FillInfo(MeId);
        Boolean IsEngOfficeIsExpired = UserControlMeEngOfficeInfoUserControl.IsExpired;
        Boolean HasEngOffice = _HasEngOffice = UserControlMeEngOfficeInfoUserControl.HasEngOffice;
        int EngOfId = _EngOfId = UserControlMeEngOfficeInfoUserControl.EngOfId;
        UserControlMeEngOfficeInfoUserControl.Visible = HasEngOffice;
        #endregion
        #region اگر عضو شرکت باشد            
        UserControlMeOfficeInfoUserControl.FillInfo(MeId);
        Boolean IsOfficeIsExpired = UserControlMeOfficeInfoUserControl.IsExpired;
        Boolean HasOffice = _HasOffice = UserControlMeOfficeInfoUserControl.HasOffice;
        int OfId = _OfId = UserControlMeOfficeInfoUserControl.OfId;
        UserControlMeOfficeInfoUserControl.Visible = HasOffice;
        RoundPanelMemberEngOfficeInfo.Visible = HasOffice || HasEngOffice;
        #endregion
        #endregion

        FillProjectIngridientCapacityInfo(DesignerType == (int)TSP.DataManager.TSMemberType.OtherPerson ? TSP.DataManager.TSMemberType.OtherPerson : TSP.DataManager.TSMemberType.Member, OfficeEngOId, Convert.ToInt32(txtMeIdSearch.Text));

        #region FillCapacityDecrement
        if (DesignerType != (int)TSP.DataManager.TSMemberType.ConsultantCompany)
        {
            TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
            ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(_PrjId, Convert.ToInt32(PrjDesignerId), -1, (int)TSP.DataManager.TSProjectIngridientType.Designer);
            if (ProjectCapacityDecrementManager.Count > 0)
            {
                SetCapacityDecrement(ProjectCapacityDecrementManager[0]["CapacityDecrement"].ToString());
                SetCapacityWage(ProjectCapacityDecrementManager[0]["Wage"].ToString());
            }
        }
        #endregion

        #region Fill Plan Info  
        if (_PlansId == -1) return;
        TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
        DesignerPlansManager.FindByPrjDesignerIdAndPlansId(_PlansId, PrjDesignerId);
        if (DesignerPlansManager.Count > 0)
            chbIsMaster.Checked = Convert.ToBoolean(DesignerPlansManager[0]["IsMaster"]);
        FillPlan(_PlansId);
        #endregion
    }

    private void FillPlan(int PlansId)
    {
        if (PlansId == -1) return;
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        DataTable dtPlan = PlansManager.SelectById(Convert.ToInt32(PlansId), -1);

        if (dtPlan.Rows.Count == 1)
        {
            txtPlanDes.Text = dtPlan.Rows[0]["Description"].ToString();
            txtPlanNo.Text = dtPlan.Rows[0]["No"].ToString();

            txtFollowCode.Text = dtPlan.Rows[0]["FollowCode"].ToString();
            cmbPlanType.DataBind();
            cmbPlanType2.DataBind();
            if (!Utility.IsDBNullOrNullValue(dtPlan.Rows[0]["PlansTypeId"]))
            {
                cmbPlanType2.SelectedIndex = cmbPlanType.SelectedIndex = cmbPlanType.Items.FindByValue(dtPlan.Rows[0]["PlansTypeId"].ToString()).Index;
                _PlansTypeId = Convert.ToInt32(dtPlan.Rows[0]["PlansTypeId"]);
            }
            string[] DateArr = (dtPlan.Rows[0]["Date"].ToString()).Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string Year = DateArr[0];
            HiddenFieldPrjDes["Year"] = Year; if (!Utility.IsDBNullOrNullValue(dtPlan.Rows[0]["TaskName"]))
                lblWorkFlowState.Text = "وضعیت درخواست ثبت طراح و نقشه مربوطه: " + dtPlan.Rows[0]["TaskName"].ToString();
            else
                lblWorkFlowState.Text = "وضعیت درخواست ثبت طراح و نقشه مربوطه: " + "نامشخص";

            TSP.DataManager.TechnicalServices.AttachmentsManager Manager = (TSP.DataManager.TechnicalServices.AttachmentsManager)Session["AttachmentsManager"];
            Manager.FindByTableTypeId(PlansId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans), -1);
            GridViewAttachment.DataSource = Manager.DataTable;
            GridViewAttachment.DataBind();

            ObjectDataSourceViewPoint.SelectParameters["PlansId"].DefaultValue = PlansId.ToString();
            GridViewViewPoint.DataBind();
        }
    }
    #endregion

    #region Fill PrjInfo & Capacity Info
    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Convert.ToInt32(Id));
        _IsCharity = prjInfo.IsCharity;
        _IsBonyadMaskan = prjInfo.IsBonyadMaskan;
        _CitId = prjInfo.CitId;
        _GroupId = prjInfo.GroupId;
        _Foundation = prjInfo.Foundation;
        _FoundationMixSkeletonSaze = prjInfo.FoundationMixSkeletonSaze;
        _FundationDifference = prjInfo.FundationDifference;
        _StructureSkeletonId = prjInfo.StructureSkeletonId;
        _AgentId = prjInfo.AgentId;
        TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
        CityManager.FindByCode(_CitId);
        if (CityManager.Count != 0)
        {
            _CanObserverBeDesigner = Convert.ToBoolean(CityManager[0]["CanObserverBeDesigner"]);
        }
        else
            _CanObserverBeDesigner = false;
    }
    /// <summary>
    /// پر کردن اطلاعات ظرفیت شرکت یا دفتر
    /// </summary>
    /// <param name="TSMemberTypeId"></param>
    /// <param name="ProjectIngridientId"></param>
    ///  <param name="MemberId"></param>
    private void FillProjectIngridientCapacityInfo(TSP.DataManager.TSMemberType TSMemberTypeId, int ProjectIngridientId, int MemberId)
    {
        WorkRequestUserControl.MeId = MemberId;
        WorkRequestUserControl.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Designer;
        CapacityUserControl.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Designer;
        WorkRequestUserControl.FillForm(MemberId.ToString(), TSMemberTypeId);
        if (TSMemberTypeId != TSP.DataManager.TSMemberType.OtherPerson)
        {
            txtSearchFileNo.Text = WorkRequestUserControl.FileNo;
            txtDocProvCode.Text = WorkRequestUserControl.FileNo.Substring(0, 2);
            txtMjCode.Text = WorkRequestUserControl.FileNo.Substring(3, 3);
            txtDocSerialNo.Text = Convert.ToInt32(WorkRequestUserControl.FileNo.Substring(7)).ToString();
            FillCapacityInfo();
            CapacityUserControl.FillProjectIngridienCapacityInfo(TSMemberTypeId, ProjectIngridientId, MemberId);
        }
    }

    private void FillCapacityInfo()
    {
        CapacityUserControl.ProjectId = _PrjId;
        CapacityUserControl.PrjReqId = _PrjReqId;
        CapacityUserControl.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Designer;

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dtWorkFlowState = new DataTable();
        _CurrentPrjTaskCode = -2;
        dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode((int)TSP.DataManager.WorkFlows.TSProjectConfirming, _PrjReqId);
        if (dtWorkFlowState.Rows.Count > 0)
        {
            _CurrentPrjTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
        }
    }

    #endregion   

    #region ClearForm
    private void ClearAllFormInfo()
    {
        _PlansId = -1;
        _OfId = -1; _EngOfId = -1;
        _ObsWorkReqChangeId = -1;
        _HasEngOffice = _HasOffice = false;
        RoundPanelMemberEngOfficeInfo.Visible = UserControlMeEngOfficeInfoUserControl.Visible = UserControlMeOfficeInfoUserControl.Visible = false;
        WorkRequestUserControl.SetUserControlVisible(false);
        WorkRequestUserControl.UserControlvisible = false;
        WorkRequestUserControl.ClearForm();
        cmbDesMeType.SelectedIndex = -1;
        lblWarningsearchOfEngInfo.Visible = false;
        txtMeIdSearch.Text = "";
        txtSearchFileNo.Text = "";
        chbIsMaster.Checked = false;
        ChbIsExteraFloor.Checked = false;
        SetCapacityDecrement("");
        SetCapacityWage("");
        SetDefualtPriceArchive();
        ClearCapacityInfo();
        CapacityUserControl.ClearControlsIngridienCapacityInfo();
        txtSearchFileNo.Text =
        txtDocProvCode.Text =
        txtMjCode.Text =
        txtDocSerialNo.Text =
        txtPlanDes.Text =
        txtPlanNo.Text =
        txtFollowCode.Text = "";
        Session["PlansAttachName"] = null;
        Session["PlanAttachStatus"] = null;
    }

    private void ClearCapacityInfo()
    {
        SetCapacityDecrement("");
        SetCapacityWage("");

    }

    private void SetDefualtPriceArchive()
    {
        TSP.DataManager.TechnicalServices.PriceArchiveManager PriceArchiveManager = new TSP.DataManager.TechnicalServices.PriceArchiveManager();
        PriceArchiveManager.FindLastPriceArchive();
        if (PriceArchiveManager.Count == 0)
            return;
        cmbPriceArchive.DataBind();
        cmbPriceArchive.SelectedIndex = cmbPriceArchive.Items.FindByValue(PriceArchiveManager[0]["PriceArchiveId"].ToString()).Index;
    }


    #endregion

    #region Set Enables
    private void SetControlsEnable(bool Enable)
    {
        cmbPlanType2.Enabled = cmbPlanType.Enabled = false;
        txtDocSerialNo.Enabled =
        txtMeIdSearch.Enabled =
        txtMjCode.Enabled =
        txtSearchFileNo.Enabled =
        txtDocProvCode.Enabled =
        RoundPanelSearch.Enabled =
        chbIsMaster.Enabled =
        cmbPriceArchive.Enabled = ChbIsExteraFloor.Enabled = false;
        SetCapcityUserControlEnable(Enable);
        TblPlanAttachmentAddInfo.Visible =
        txtPlanDes.Enabled =
        txtPlanNo.Enabled =
        cmbDesMeType.Enabled = Enable;
    }

    #endregion

    private bool CheckMajor(int MeId, int CapacityDecreament)
    {
        if (_CitId == 317)
            return true;
        string ErrMsg = "";
        TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager ProjectIngridientMajorsManager = new TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager();
        int PlansTypeId = Convert.ToInt32(HiddenFieldPrjDes["PlansTypeId"]);
        if (string.IsNullOrEmpty(PlansTypeId.ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
        int MeDesGrdId = -2;
        int MeObsGrdId = -2;
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count != 0)
        {
            MeDesGrdId = !Utility.IsDBNullOrNullValue(MemberManager[0]["DesId"]) ? Convert.ToInt32(MemberManager[0]["DesId"]) : -2;
            MeObsGrdId = !Utility.IsDBNullOrNullValue(MemberManager[0]["ObsId"]) ? Convert.ToInt32(MemberManager[0]["ObsId"]) : -2;
        }


        DataTable dt = ProjectIngridientMajorsManager.SelectTSProjectIngridientMajorsById((int)TSP.DataManager.TSProjectIngridientType.Designer, PlansTypeId, _GroupId, -1, MeDesGrdId != -2 ? MeDesGrdId : MeObsGrdId, _StructureSkeletonId, CapacityDecreament, _MeMajorParentIdInWorkReq);
        if (dt.Rows.Count > 0)
            return true;

        ErrMsg = " می باشد و با توجه به متراژ و گروه ساختمانی و رشته شخص در آماده بکاری  و پایه ، شما مجاز برای طراحی این نوع نقشه نمی باشید.";
        SetLabelWarning(ErrMsg);
        return false;
    }


    #region InsertUpdate
    #region Insert
    private void Insert()
    {
        if (IsPageRefresh)
        {
            return;
        }

        #region Define Managers
        TSP.DataManager.TechnicalServices.Project_DesignerManager Project_DesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        TSP.DataManager.TechnicalServices.Designer_PlansManager Designer_PlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();

        TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.OtherPersonManager OthpManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager = (TSP.DataManager.TechnicalServices.AttachmentsManager)Session["AttachmentsManager"];
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(Project_DesignerManager);
        transact.Add(Designer_PlansManager);
        transact.Add(ProjectOfficeMembersManager);
        transact.Add(ProjectCapacityDecrementManager);
        transact.Add(PlansManager);
        transact.Add(AttachmentsManager);
        transact.Add(WorkFlowStateManager);
        transact.Add(WorkFlowTaskManager);
        #endregion


        try
        {
            #region Check Conditions
            if (Convert.ToInt32(GetCapacityDecrement()) < 0)
            {
                SetLabelWarning("متراژ کارکرد نمی تواند منفی باشد.");
                return;
            }
            if (Convert.ToInt32(GetCapacityDecrement()) > (_FundationDifference > 0 ? _FundationDifference : _Foundation))
            {
                SetLabelWarning("متراژ کارکرد وارد شده بیشتر از متراژ کل پروژه می باشد.");
                return;
            }
            if (!FillMemberInfo(Utility.GetCurrentUser_MeId()))
                return;
            int MeOfOthId = -2;
            if (Convert.ToInt32(cmbDesMeType.Value) == (int)TSP.DataManager.TSMemberType.OtherPerson)
            {
                OthpManager.FindKardanAndMemarByOtpCode(txtMeIdSearch.Text, (int)TSP.DataManager.OtherPersonType.Kardan);
                MeOfOthId = Convert.ToInt32(OthpManager[0]["OtpId"]);
            }
            else
            {
                MeOfOthId = int.Parse(txtMeIdSearch.Text);
            }
            if (!CheckMajor(Convert.ToInt32(txtMeIdSearch.Text), int.Parse(GetCapacityDecrement())))
                return;
            Boolean CapacityRelatedToPreviousYears = false;
            #region Check Capacity
            if (Convert.ToInt32(cmbDesMeType.Value) != (int)TSP.DataManager.TSMemberType.OtherPerson)
            {
                if (Utility.IsDBNullOrNullValue(comboYear.SelectedItem))
                {
                    SetLabelWarning("سال کاری انتخاب نشده است");
                    return;
                }
                if (string.Compare(_CurrentCapacityAssignmentYear, comboYear.SelectedItem.Text) > 0)//اگر سال کاری کوچکتر از سال کای جاری باشد.ظرفیت ها از امسال کسر نمی شود و فقط ثبت می شود
                {
                    CapacityRelatedToPreviousYears = true;
                }
                else
                {
                    if (!CheckCapacityBasedOnWorkRequest(MeOfOthId, int.Parse(GetCapacityDecrement())))
                        return;
                }
            }
            if (AttachmentsManager.Count == 0)
            {
                SetLabelWarning("فایل های پیوست مربوط به نقشه را انتخاب نمایید.");
                return;
            }

            if (!CheckPlansAttachType(AttachmentsManager))
                return;

            Boolean ISAllDesignersInserted = false;
            Boolean IsDesignerMaxIs50Percent = false;
            if (_GroupId != (int)TSP.DataManager.TSStructureGroups.A && (_PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatBargh || _PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatMechanic))
            {
                IsDesignerMaxIs50Percent = true;
                if (Convert.ToInt32(GetCapacityDecrement()) > Math.Round((_FundationDifference > 0 ? _FundationDifference : _Foundation) / 2.0, MidpointRounding.AwayFromZero))
                {
                    SetLabelWarning("متراژ کسر ظرفیت طراح تاسیسات پروژه های گروه ساختمانی ب /ج /د نمی تواند بیشتر از 50 درصد زیربنای کل پروژه باشد.");
                    return;
                }
            }
            if (_PlansId == -1)
            {
                if (IsDesignerMaxIs50Percent)
                {
                    if (Convert.ToInt32(GetCapacityDecrement()) == (_FundationDifference > 0 ? _FundationDifference : _Foundation) / 2 || Convert.ToInt32(GetCapacityDecrement()) == Math.Round((_FundationDifference > 0 ? _FundationDifference : _Foundation) / 2.0, MidpointRounding.AwayFromZero))
                        ISAllDesignersInserted = true;
                }
                else if ((_GroupId == (int)TSP.DataManager.TSStructureGroups.A || _GroupId == (int)TSP.DataManager.TSStructureGroups.B) && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Sazeh && _StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Mix)
                {
                    if (Convert.ToInt32(GetCapacityDecrement()) == _FoundationMixSkeletonSaze)
                        ISAllDesignersInserted = true;
                }
                else
                {
                    if (Convert.ToInt32(GetCapacityDecrement()) == (_FundationDifference > 0 ? _FundationDifference : _Foundation))
                        ISAllDesignersInserted = true;
                }
            }
            else
            {
                int SumDecreament = 0;
                DataTable dtDesinger = Designer_PlansManager.SelectActiveTSDesignerPlansForByPlanId(_PlansId);
                for (int i = 0; i < dtDesinger.Rows.Count; i++)
                {
                    SumDecreament += Convert.ToInt32(dtDesinger.Rows[i]["CapacityDecrement"]);
                }
                if ((_GroupId == (int)TSP.DataManager.TSStructureGroups.A || _GroupId == (int)TSP.DataManager.TSStructureGroups.B) && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Sazeh && _StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Mix)
                {
                    if (SumDecreament + Convert.ToInt32(GetCapacityDecrement()) > _FoundationMixSkeletonSaze)
                    {
                        SetLabelWarning("مجموع متراژ کارکرد وارد شده  شما و سایر طراحان این نوع نقشه بیشتر از متراژ طراح سازه برای این پروژه ترکیبی می باشد.متراژ کارکرد را بررسی نمایید");
                        return;
                    }
                }
                else if (SumDecreament + Convert.ToInt32(GetCapacityDecrement()) > (_FundationDifference > 0 ? _FundationDifference : _Foundation))
                {
                    SetLabelWarning("مجموع متراژ کارکرد وارد شده  شما و سایر طراحان این نوع نقشه بیشتر از  متراژ کل پروژه می باشد.متراژ کارکرد را بررسی نمایید");
                    return;
                }
                if (IsDesignerMaxIs50Percent)
                {
                    if (SumDecreament + Convert.ToInt32(GetCapacityDecrement()) > Math.Round((_FundationDifference > 0 ? _FundationDifference : _Foundation) / 2.0, MidpointRounding.AwayFromZero))
                    {
                        SetLabelWarning("مجموع متراژ کارکرد وارد شده  شما و سایر طراحان این نوع نقشه بیشتر از 50 درصد متراژ کل پروژه می باشد.متراژ کارکرد را بررسی نمایید");
                        return;
                    }
                    if (SumDecreament + Convert.ToInt32(GetCapacityDecrement()) == (_FundationDifference > 0 ? _FundationDifference : _Foundation) / 2 || SumDecreament + Convert.ToInt32(GetCapacityDecrement()) == Math.Round((_FundationDifference > 0 ? _FundationDifference : _Foundation) / 2.0, MidpointRounding.AwayFromZero))
                    {
                        ISAllDesignersInserted = true;
                    }
                }
                else if ((_GroupId == (int)TSP.DataManager.TSStructureGroups.A || _GroupId == (int)TSP.DataManager.TSStructureGroups.B) && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Sazeh && _StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Mix)
                {
                    if (SumDecreament + Convert.ToInt32(GetCapacityDecrement()) == _FoundationMixSkeletonSaze)
                        ISAllDesignersInserted = true;
                }
                else if (SumDecreament + Convert.ToInt32(GetCapacityDecrement()) == (_FundationDifference > 0 ? _FundationDifference : _Foundation))
                {
                    ISAllDesignersInserted = true;
                }


            }
            decimal DesPrice = PlansManager.Get5PercentPriceByStepForMemberEpayment(_PrjId, _PlansTypeId, _PrjReqId, Convert.ToInt32(cmbPriceArchive.Value), Convert.ToDouble(GetCapacityWage()));
            if (DesPrice == 0 && !Convert.ToBoolean(_IsCharity))
            {
                SetLabelWarning("خطا در محاسبه مبلغ فیش طراحی ایجاد شده است.");
                return;
            }

            EPaymentUC.AccType = _AccType;
            EPaymentUC.Amount = Convert.ToInt32(DesPrice.ToString("0"));
            EPaymentUC.TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Designer);
            #endregion 
            if (chbIsMaster.Checked && !CheckPlansMaster())
                return;
            #endregion

            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SavePlanInfo);
            if (WorkFlowTaskManager.Count != 1)
            {
                SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است.");
                return;
            }
            int TaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);

            transact.BeginSave();

            int IsDecreased = 1;
            if (!InsertPlanORUpdate(PlansManager))
            {
                transact.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }

            Inser_UpdatetAttachment(AttachmentsManager);
            if (!InsertProjectDesigner(Project_DesignerManager, MeOfOthId))
            {
                transact.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            if (!InsertDesignerPlans(Designer_PlansManager))
            {
                transact.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }

            int ProjectCapacityDecrementId;

            TSP.DataManager.TSProjectIngridientType DecreasType = TSP.DataManager.TSProjectIngridientType.Designer;
            if (Convert.ToInt32(cmbDesMeType.Value) == (int)TSP.DataManager.TSMemberType.Member)
            {
                DecreasType = TSP.DataManager.TSProjectIngridientType.Observer;
            }
            CapacityCalculations CapacityCalculations = new CapacityCalculations();
            int IsWorkFree = Convert.ToBoolean(_IsCharity) ? 1 : 0;
            int IsFree = CapacityRelatedToPreviousYears == true ? 1 : 0;
            ProjectCapacityDecrementId = CapacityCalculations.InsertProjectCapacityDecrement(ProjectCapacityDecrementManager
                , GetCapacityDecrement(), GetCapacityWage()
                , (Int16)DecreasType, (int)TSP.DataManager.TSProjectIngridientType.Designer, _PrjDesignerId, null
                , Utility.GetCurrentUser_UserId(), MeOfOthId
                , Convert.ToInt32(cmbDesMeType.Value) == (int)TSP.DataManager.TSMemberType.OtherPerson ? (int)TSP.DataManager.TSMemberType.OtherPerson : (int)TSP.DataManager.TSMemberType.Member
                , _PrjId, IsFree
                , CapacityRelatedToPreviousYears == true ? comboYear.SelectedItem.Text + "/12/28" : Utility.GetDateOfToday(), false, IsWorkFree);
            if (Convert.ToInt32(cmbDesMeType.Value) != (int)TSP.DataManager.TSMemberType.OtherPerson && !CapacityRelatedToPreviousYears)
            {
                TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
                transact.Add(ObserverWorkRequestManager);
                int CapacityErr = CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, ProjectCapacityDecrementManager, MeOfOthId, Utility.GetCurrentUser_UserId(), _PrjId, _CitId, Convert.ToBoolean(_IsCharity), DecreasType, null, false, false);
                if (CapacityErr != 0)
                {
                    transact.CancelSave();
                    SetLabelWarning(CapacityCalculations.FindErrorMessage(CapacityErr));
                    return;
                }
            }
            if (Convert.ToInt32(cmbDesMeType.Value) != (int)TSP.DataManager.TSMemberType.Member && Convert.ToInt32(cmbDesMeType.Value) != (int)TSP.DataManager.TSMemberType.OtherPerson &&
                !InsertProjectOfficeMembers(ProjectOfficeMembersManager, IsDecreased))
            {
                transact.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            int WfStateId = InsertWF(WorkFlowStateManager, TaskId);
            if (WfStateId <= 0)
            {
                transact.CancelSave();
                return;
            }
            if (PlansManager.UpdateRequestTaskId(_PlansId, TaskId, WfStateId) != 0)
            {
                transact.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            if (EPaymentUC.Amount == 0 && !Convert.ToBoolean(_IsCharity))
            {
                SetLabelWarning("خطایی در محاسبه مبلغ فیش پنج درصد ایجاد شده انجام گرفته است.");
                transact.CancelSave();
                return;
            }
            EPaymentUC.TableId = _PrjDesignerId;
            if (!Convert.ToBoolean(_IsCharity))
            {
                if (EPaymentUC.SaveFish(transact, _PrjDesignerId, Utility.GetCurrentUser_UserId(), TSP.DataManager.EpaymentType.ParsianGetWay) <= 0)
                {
                    SetLabelWarning("خطایی در محاسبه مبلغ فیش پنج درصد ایجاد شده انجام گرفته است.");
                    transact.CancelSave();
                    return;
                }
            }
            lblWorkFlowState.Text = "وضعیت درخواست ثبت طراح و نقشه مربوطه" + WorkFlowTaskManager[WorkFlowTaskManager.Count - 1]["TaskName"].ToString();
            transact.EndSave();
            if (!Convert.ToBoolean(_IsCharity))
                Response.Redirect("~/Epayment/EpaymentParsian.aspx?InvoiceNo=" + Utility.EncryptQS(EPaymentUC.InvoiceNumber.ToString()) + "&Cit=" + Utility.EncryptQS(_CitId.ToString()), false);
            else
            {
                _PageMode = "Edit";
                SetEditModeKeys();
                SetLabelWarning("ذخیره انجام شد.");
            }
        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private Boolean InsertPlanORUpdate(TSP.DataManager.TechnicalServices.PlansManager PlansManager)
    {
        if (_PlansId != -1)
        {
            PlansManager.FindByPlansId(_PlansId);
            PlansManager[0].BeginEdit();
            PlansManager[0]["PrjReId"] = _PrjReqId;
            PlansManager[0]["ProjectId"] = _PrjId;
            PlansManager[0]["Status"] = (int)TSP.DataManager.TSPlanRequestType.SavePlanInfoInsideProjectConfirming;
            PlansManager[0]["PlansTypeId"] = cmbPlanType.SelectedItem.Value.ToString();
            PlansManager[0]["No"] = txtPlanNo.Text.Trim();
            PlansManager[0]["Description"] = txtPlanDes.Text.Trim();
            PlansManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            PlansManager[0].EndEdit();
            if (PlansManager.Save() <= 0)
                return false;
            PlansManager.DataTable.AcceptChanges();
        }
        else
        {
            DataRow PlanRow = PlansManager.NewRow();
            PlanRow.BeginEdit();
            PlanRow["PrjReId"] = _PrjReqId;
            PlanRow["ProjectId"] = _PrjId;
            PlanRow["Status"] = (int)TSP.DataManager.TSPlanRequestType.SavePlanInfoInsideProjectConfirming;
            PlanRow["PlansTypeId"] = cmbPlanType.SelectedItem.Value.ToString();
            PlanRow["No"] = txtPlanNo.Text.Trim();
            PlanRow["Description"] = txtPlanDes.Text.Trim();
            PlanRow["PlanVersion"] = 1;
            PlanRow["Date"] = Utility.GetDateOfToday();
            PlanRow["IsConfirmed"] = 0;
            PlanRow["IsDesAccepted"] = (int)TSP.DataManager.TSDesignerAcceptance.Pending;
            PlanRow["InActive"] = 0;
            PlanRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.TSPlan);
            PlanRow["UserId"] = Utility.GetCurrentUser_UserId();
            PlanRow["ModifiedDate"] = DateTime.Now;
            PlanRow.EndEdit();
            PlansManager.AddRow(PlanRow);
            if (PlansManager.Save() <= 0)
                return false;
            PlansManager.DataTable.AcceptChanges();
            _PlansId = Convert.ToInt32(PlansManager[0]["PlansId"]);
        }

        return true;
    }

    private void Inser_UpdatetAttachment(TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager)
    {
        for (int i = 0; i < AttachmentsManager.Count; i++)
        {
            if (Utility.IsDBNullOrNullValue(AttachmentsManager[i]["TableTypeId"]) || Convert.ToInt32(AttachmentsManager[i]["TableTypeId"]) == -1)
            {
                AttachmentsManager[i]["TableTypeId"] = _PlansId;
                AttachmentsManager.Save();
                AttachmentsManager.DataTable.AcceptChanges();
            }
        }

    }
    private Boolean InsertProjectDesigner(TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager, int MeId)
    {
        DataRow PrjDesRow = ProjectDesignerManager.NewRow();

        PrjDesRow.BeginEdit();
        PrjDesRow["PrjReId"] = _PrjReqId;
        PrjDesRow["ProjectId"] = _PrjId;
        PrjDesRow["MeOthId"] = MeId;
        PrjDesRow["MeOthIdType"] = CmbMembershipType.Value;
        PrjDesRow["MemberTypeId"] = cmbDesMeType.Value;
        int OfficeEngOId = -1;
        if (_HasOffice)
            OfficeEngOId = _OfId;
        else
        if (_HasEngOffice)
            OfficeEngOId = _EngOfId;
        else
            OfficeEngOId = MeId;
        PrjDesRow["OfficeEngOId"] = OfficeEngOId;
        PrjDesRow["DesignerTypeId"] = GetDesignerTypeId();
        PrjDesRow["PriceArchiveId"] = cmbPriceArchive.Value;
        PrjDesRow["IsExteraFloor"] = ChbIsExteraFloor.Checked;
        PrjDesRow["Year"] = comboYear.Text;
        PrjDesRow["CreateDate"] = Utility.GetDateOfToday();
        PrjDesRow["InActive"] = 0;
        PrjDesRow["UserId"] = Utility.GetCurrentUser_UserId();
        PrjDesRow["ModifiedDate"] = DateTime.Now;
        PrjDesRow.EndEdit();

        ProjectDesignerManager.AddRow(PrjDesRow);
        ProjectDesignerManager.Save();

        ProjectDesignerManager.DataTable.AcceptChanges();
        _PrjDesignerId = Convert.ToInt32(ProjectDesignerManager[0]["PrjDesignerId"]);
        return true;
    }

    private Boolean InsertDesignerPlans(TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager)
    {
        DataRow PlanDesRow = DesignerPlansManager.NewRow();
        PlanDesRow["PrjDesignerId"] = _PrjDesignerId;
        PlanDesRow["PlansId"] = _PlansId;
        PlanDesRow["IsMaster"] = chbIsMaster.Checked;
        PlanDesRow["UserId"] = Utility.GetCurrentUser_UserId();
        PlanDesRow["ModifiedDate"] = DateTime.Now;
        DesignerPlansManager.AddRow(PlanDesRow);
        DesignerPlansManager.Save();

        DesignerPlansManager.DataTable.AcceptChanges();
        _DesignerPlansId = Convert.ToInt32(DesignerPlansManager[0]["DesignerPlansId"]);
        return true;
    }

    private Boolean InsertProjectOfficeMembers(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, int IsDecreased)
    {
        DataRow rowProjectOfficeMembers = ProjectOfficeMembersManager.NewRow();
        rowProjectOfficeMembers["PrjImpObsDsgnId"] = _PrjDesignerId;
        rowProjectOfficeMembers["ProjectIngridientTypeId"] = (int)TSP.DataManager.TSProjectIngridientType.Designer;
        rowProjectOfficeMembers["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.Member;
        rowProjectOfficeMembers["MeOthPId"] = txtMeIdSearch.Text;//cmbOfficeMe.Value;
                                                                 //************????????
                                                                 ///////////rowProjectOfficeMembers["MeOthPName"] = txtMeNameSearch.Text;
                                                                 /////************????????
        rowProjectOfficeMembers["CapacityDecrement"] = GetCapacityDecrement();// txtMeCapacityDecreament.Text;
        rowProjectOfficeMembers["Wage"] = GetCapacityWage();// txtMeWage.Text;
        rowProjectOfficeMembers["IsFree"] = 0;
        rowProjectOfficeMembers["IsDecreased"] = IsDecreased;
        if (IsDecreased == 1)
            rowProjectOfficeMembers["DecreasedDate"] = Utility.GetDateOfToday();
        rowProjectOfficeMembers["OfficeMemberTypeId"] = cmbDesMeType.Value;
        rowProjectOfficeMembers["UserId"] = Utility.GetCurrentUser_UserId();
        rowProjectOfficeMembers["ModifiedDate"] = DateTime.Now;

        ProjectOfficeMembersManager.AddRow(rowProjectOfficeMembers);
        ProjectOfficeMembersManager.Save();
        return true;
    }

    private int InsertWF(TSP.DataManager.WorkFlowStateManager WorkFlowStateManager, int TaskId)
    {
        int TableId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldPrjDes["PlansId"].ToString()));
        String Description1 = "آغاز گردش کار اتوماتیک سیستم جهت ثبت نقشه پروژه توسط طراح";
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans);


        int WfStateId = WorkFlowStateManager.InsertWorkFlowState(TableType, _PlansId, TaskId, Description1, Utility.GetCurrentUser_MeId(),
                 (int)TSP.DataManager.WorkFlowStateNmcIdType.MeId, Utility.GetCurrentUser_UserId(), 1, Utility.GetDateOfToday());

        if (WfStateId <= 0)
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است.");
            return -1;
        }
        return WfStateId;

    }

    #endregion

    #region Update
    private void Update()
    {

        if (IsPageRefresh)
        {
            return;
        }
        #region Define Managers
        TSP.DataManager.TechnicalServices.Project_DesignerManager Project_DesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        TSP.DataManager.TechnicalServices.Designer_PlansManager Designer_PlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
        TSP.DataManager.TechnicalServices.ConsultantCompanyManager ConsultantCompanyManager = new TSP.DataManager.TechnicalServices.ConsultantCompanyManager();
        TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager(); //(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager)Session["ProjectOfficeMembersManager"];
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager CapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager = (TSP.DataManager.TechnicalServices.AttachmentsManager)Session["AttachmentsManager"];
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(PlansManager);
        transact.Add(AttachmentsManager);
        transact.Add(Project_DesignerManager);
        transact.Add(CapacityDecrementManager);
        transact.Add(Designer_PlansManager);
        transact.Add(ConsultantCompanyManager);
        transact.Add(ProjectOfficeMembersManager);
        transact.Add(ProjectCapacityDecrementManager);
        transact.Add(WorkFlowStateManager);
        transact.Add(WorkFlowTaskManager);
        #endregion

        #region Check Condition
        Project_DesignerManager.FindByPrjDesignerId(_PrjDesignerId);

        if (Project_DesignerManager.Count > 0 && Convert.ToInt32(Project_DesignerManager[0]["MeOthId"]) != Utility.GetCurrentUser_MeId())
        {
            SetLabelWarning("با توجه به کد عضویت طراح ثبت شده برای این نقشه، شما طراح این نقشه نمی باشید. نام کاربری که با آن در سیستم وارد شده اید را بررسی نمایید");
            return;
        }        
        Boolean CapacityRelatedToPreviousYears = false;

        int TSMemberType = Convert.ToInt32(cmbDesMeType.Value);
        if (Convert.ToInt32(GetCapacityDecrement()) < 0)
        {
            SetLabelWarning("متراژ کارکرد نمی تواند منفی باشد.");
            return;
        }
        if (Convert.ToInt32(GetCapacityDecrement()) > (_FundationDifference > 0 ? _FundationDifference : _Foundation))
        {
            SetLabelWarning("متراژ کارکرد وارد شده بیشتر از متراژ کل پروژه می باشد.");
            return;
        }
        if (!FillMemberInfo(Utility.GetCurrentUser_MeId()))
            return;
        int MeOfOthId = int.Parse(txtMeIdSearch.Text);
        try
        {
            int DiffrenceCapacity1 = 0;
            int CapacityDecrementOrigin = 0;
            CapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(_PrjId, _PrjDesignerId, -1, (int)TSP.DataManager.TSProjectIngridientType.Designer);
            if (CapacityDecrementManager.Count > 0)
            {
                CapacityDecrementOrigin = Convert.ToInt32(CapacityDecrementManager[0]["CapacityDecrement"]);
                DiffrenceCapacity1 = Convert.ToInt32(GetCapacityDecrement()) - Convert.ToInt32(CapacityDecrementManager[0]["CapacityDecrement"]);
                if (DiffrenceCapacity1 < 0 && string.Compare(CapacityDecrementManager[0]["DecreasedDate"].ToString(), "1398/03/25") < 0)
                    DiffrenceCapacity1 = 0;
            }
            if (Utility.IsDBNullOrNullValue(comboYear.SelectedItem))
            {
                SetLabelWarning("سال کاری انتخاب نشده است");
                return;
            }
            if (string.Compare(_CurrentCapacityAssignmentYear, comboYear.SelectedItem.Text) > 0)//اگر سال کاری کوچکتر از سال کای جاری باشد.ظرفیت ها از امسال کسر نمی شود و فقط ثبت می شود
            {
                CapacityRelatedToPreviousYears = true;
            }
            else
            {
                if (DiffrenceCapacity1 != 0 && !CheckCapacityBasedOnWorkRequest(MeOfOthId, DiffrenceCapacity1))
                    return;

            }
            if (chbIsMaster.Checked && !CheckPlansMaster())
                return;
            if (!CheckMajor(Convert.ToInt32(txtMeIdSearch.Text), int.Parse(GetCapacityDecrement())))
                return;
            Boolean ISAllDesignersInserted = false;
            int SumDecreament = 0;
            Boolean IsDesignerMaxIs50Percent = false;
            if (_GroupId != (int)TSP.DataManager.TSStructureGroups.A && (_PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatBargh || _PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatMechanic))
            {
                IsDesignerMaxIs50Percent = true;
                if (Convert.ToInt32(GetCapacityDecrement()) > Math.Round((_FundationDifference > 0 ? _FundationDifference : _Foundation) / 2.0, MidpointRounding.AwayFromZero))
                {
                    SetLabelWarning("متراژ کسر ظرفیت طراح تاسیسات پروژه های گروه ساختمانی ب /ج /د نمی تواند بیشتر از 50 درصد زیربنای کل پروژه باشد.");
                    return;
                }
            }
            DataTable dtDesinger = Designer_PlansManager.SelectActiveTSDesignerPlansForByPlanId(_PlansId);
            for (int i = 0; i < dtDesinger.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtDesinger.Rows[i]["PrjDesignerId"]) != _PrjDesignerId)
                    SumDecreament += Convert.ToInt32(dtDesinger.Rows[i]["CapacityDecrement"]);
            }
            if ((_GroupId == (int)TSP.DataManager.TSStructureGroups.A || _GroupId == (int)TSP.DataManager.TSStructureGroups.B) && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Sazeh && _StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Mix)
            {
                if (SumDecreament + Convert.ToInt32(GetCapacityDecrement()) > _FoundationMixSkeletonSaze)
                {
                    SetLabelWarning("مجموع متراژ کارکرد وارد شده  شما و سایر طراحان این نوع نقشه بیشتر از متراژ طراح سازه برای این پروژه ترکیبی می باشد.متراژ کارکرد را بررسی نمایید");
                    return;
                }
            }
            else if (SumDecreament + Convert.ToInt32(GetCapacityDecrement()) > (_FundationDifference > 0 ? _FundationDifference : _Foundation))
            {
                SetLabelWarning("مجموع متراژ کارکرد وارد شده  شما و سایر طراحان این نوع نقشه بیشتر از  متراژ کل پروژه می باشد.متراژ کارکرد را بررسی نمایید");
                return;
            }
            if (IsDesignerMaxIs50Percent)
            {
                if (SumDecreament + Convert.ToInt32(GetCapacityDecrement()) > Math.Round((_FundationDifference > 0 ? _FundationDifference : _Foundation) / 2.0, MidpointRounding.AwayFromZero))
                {
                    SetLabelWarning("مجموع متراژ کارکرد وارد شده  شما و سایر طراحان این نوع نقشه بیشتر از 50 درصد متراژ کل پروژه می باشد.متراژ کارکرد را بررسی نمایید");
                    return;
                }
                if (SumDecreament + Convert.ToInt32(GetCapacityDecrement()) == (_FundationDifference > 0 ? _FundationDifference : _Foundation) / 2 || SumDecreament + Convert.ToInt32(GetCapacityDecrement()) == Math.Round((_FundationDifference > 0 ? _FundationDifference : _Foundation) / 2.0, MidpointRounding.AwayFromZero))
                {
                    ISAllDesignersInserted = true;
                }
            }
            else if (_GroupId == (int)TSP.DataManager.TSStructureGroups.A && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Sazeh && _StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Mix)
            {
                if (Convert.ToInt32(GetCapacityDecrement()) == _FoundationMixSkeletonSaze)
                    ISAllDesignersInserted = true;
            }
            else if (SumDecreament + Convert.ToInt32(GetCapacityDecrement()) == (_FundationDifference > 0 ? _FundationDifference : _Foundation))
            {
                ISAllDesignersInserted = true;
            }

            decimal DesPrice = PlansManager.Get5PercentPriceByStepForMemberEpayment(_PrjId, _PlansTypeId, _PrjReqId, Convert.ToInt32(cmbPriceArchive.Value), Convert.ToDouble(GetCapacityWage()));
            if (DesPrice == 0 && !Convert.ToBoolean(_IsCharity))
            {
                SetLabelWarning("خطا در محاسبه مبلغ فیش طراحی ایجاد شده است.");
                return;
            }
            EPaymentUC.AccType = _AccType;
            EPaymentUC.Amount = Convert.ToInt32(DesPrice.ToString("0"));
            EPaymentUC.TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Designer);
        }
        catch (Exception ex)
        {
            SetLabelWarning("خطا در بررسی اطلاعات ایجاد شده است.");
            Utility.SaveWebsiteError(ex);
            return;
        }
        #endregion
        try
        {
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SavePlanInfo);
            if (WorkFlowTaskManager.Count != 1)
            {
                SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است.");
                return;
            }
            int TaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);

            transact.BeginSave();
            Boolean SaveWF = false;
            if (_PlansId == -1)
            {
                SaveWF = true;
            }
            if (!InsertPlanORUpdate(PlansManager))
            {
                transact.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            if (SaveWF)
            {
                int WfStateId = InsertWF(WorkFlowStateManager, TaskId);
                if (WfStateId <= 0)
                {
                    transact.CancelSave();
                    return;
                }
                if (PlansManager.UpdateRequestTaskId(_PlansId, TaskId, WfStateId) != 0)
                {
                    transact.CancelSave();
                    SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    return;
                }
            }

            Inser_UpdatetAttachment(AttachmentsManager);
            //*****
            if (!UpdateProjectDesigner(Project_DesignerManager, MeOfOthId, TSMemberType))
            {
                transact.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }

            if (_DesignerPlansId == -1)
            {
                if (!InsertDesignerPlans(Designer_PlansManager))
                {
                    transact.CancelSave();
                    SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    return;
                }
            }
            else
            {
                if (!UpdateDesignerPlans(Designer_PlansManager))
                {
                    transact.CancelSave();
                    SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                    return;
                }
            }
            ///*********
            TSP.DataManager.TSProjectIngridientType DecreasType = TSP.DataManager.TSProjectIngridientType.Designer;
            if (TSMemberType == (int)TSP.DataManager.TSMemberType.Member)
            {
                DecreasType = TSP.DataManager.TSProjectIngridientType.Observer;
            }
            CapacityCalculations CapacityCalculations = new CapacityCalculations();
            int DiffrenceCapacity = CapacityCalculations.UpdateProjectCapacityDecrement(ProjectCapacityDecrementManager, GetCapacityDecrement(), GetCapacityWage(), null, (Int16)DecreasType, (int)TSP.DataManager.TSProjectIngridientType.Designer, _PrjDesignerId, CapacityRelatedToPreviousYears == true ? 1 : 0, 1, Utility.GetCurrentUser_UserId(), _PrjId, false);
            if (!CapacityRelatedToPreviousYears && DiffrenceCapacity != 0)
            {
                TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
                transact.Add(ObserverWorkRequestManager);
                if (CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, ProjectCapacityDecrementManager, MeOfOthId, Utility.GetCurrentUser_UserId(), _PrjId, _CitId, Convert.ToBoolean(_IsCharity), DecreasType, null, false, false) != 0)
                {
                    transact.CancelSave();
                    SetLabelWarning("خطایی در بروزرسانی اطلاعات ظرفیت عضو انجام گرفته است");
                    return;
                }
            }
            int IsDecreased = 1;
            if (TSMemberType != (int)TSP.DataManager.TSMemberType.Member &&
           !UpdateProjectOfficeMembers(ProjectOfficeMembersManager, IsDecreased, TSMemberType))
            {
                transact.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }

            if (EPaymentUC.Amount == 0 && !Convert.ToBoolean(_IsCharity))
            {
                SetLabelWarning("خطایی در محاسبه مبلغ فیش پنج درصد ایجاد شده انجام گرفته است.");
                transact.CancelSave();
                return;
            }
            EPaymentUC.TableId = _PrjDesignerId;
            if (!Convert.ToBoolean(_IsCharity))
            {
                if (EPaymentUC.UpdateFishAmount(transact, _PrjDesignerId, Utility.GetCurrentUser_UserId(), TSP.DataManager.EpaymentType.ParsianGetWay) < 0)
                {
                    SetLabelWarning("خطایی در محاسبه مبلغ فیش پنج درصد ایجاد شده انجام گرفته است.");
                    transact.CancelSave();
                    return;
                }
            }
            transact.EndSave();
            if (!Convert.ToBoolean(_IsCharity) && EPaymentUC.InvoiceNumber > 0)
                Response.Redirect("~/Epayment/EpaymentParsian.aspx?InvoiceNo=" + Utility.EncryptQS(EPaymentUC.InvoiceNumber.ToString()) + "&Cit=" + Utility.EncryptQS(_CitId.ToString()), false);

            _PageMode = "Edit";
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد.");
        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private Boolean UpdateProjectDesigner(TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager, int MeId, int MemberTypeId)
    {
        ProjectDesignerManager.FindByPrjDesignerId(_PrjDesignerId);

        if (ProjectDesignerManager.Count > 0)
        {
            ProjectDesignerManager[0].BeginEdit();
            ProjectDesignerManager[0]["PrjReId"] = _PrjReqId;
            ProjectDesignerManager[0]["ProjectId"] = _PrjId;
            ProjectDesignerManager[0]["MeOthId"] = MeId;
            ProjectDesignerManager[0]["MeOthIdType"] = CmbMembershipType.Value;
            ProjectDesignerManager[0]["MemberTypeId"] = MemberTypeId;
            int OfficeEngOId = -1;
            if (_HasOffice)
                OfficeEngOId = _OfId;
            else
            if (_HasEngOffice)
                OfficeEngOId = _EngOfId;
            else
                OfficeEngOId = MeId;
            ProjectDesignerManager[0]["OfficeEngOId"] = OfficeEngOId;
            ProjectDesignerManager[0]["PriceArchiveId"] = cmbPriceArchive.Value;
            ProjectDesignerManager[0]["IsExteraFloor"] = ChbIsExteraFloor.Checked;
            ProjectDesignerManager[0]["Year"] = comboYear.Text;
            ProjectDesignerManager[0]["CreateDate"] = Utility.GetDateOfToday();
            ProjectDesignerManager[0]["InActive"] = 0;
            ProjectDesignerManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ProjectDesignerManager[0]["ModifiedDate"] = DateTime.Now;
            ProjectDesignerManager[0].EndEdit();

            ProjectDesignerManager.Save();
        }
        return true;
    }

    private Boolean UpdateDesignerPlans(TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager)
    {
        if (Utility.IsDBNullOrNullValue(_DesignerPlansId) || Utility.IsDBNullOrNullValue(_PlansId))
            return false;


        DesignerPlansManager.FindByDesignerPlansId(_DesignerPlansId);

        if (DesignerPlansManager.Count > 0)
        {
            DesignerPlansManager[0].BeginEdit();
            DesignerPlansManager[0]["PrjDesignerId"] = _PrjDesignerId;
            DesignerPlansManager[0]["PlansId"] = _PlansId;
            DesignerPlansManager[0]["IsMaster"] = chbIsMaster.Checked;
            DesignerPlansManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            DesignerPlansManager[0]["ModifiedDate"] = DateTime.Now;
            DesignerPlansManager[0].EndEdit();

            DesignerPlansManager.Save();
        }
        return true;
    }

    private Boolean UpdateProjectOfficeMembers(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, int IsDecreased, int TSMemberType)
    {

        ProjectOfficeMembersManager.FindByIngridientTypeAndPrjImpObsDsgnId((int)TSP.DataManager.TSProjectIngridientType.Designer, _PrjDesignerId);
        if (ProjectOfficeMembersManager.Count <= 0)
            return false;
        ProjectOfficeMembersManager[0].BeginEdit();
        ProjectOfficeMembersManager[0]["PrjImpObsDsgnId"] = _PrjDesignerId;
        ProjectOfficeMembersManager[0]["ProjectIngridientTypeId"] = (int)TSP.DataManager.TSProjectIngridientType.Designer;
        ProjectOfficeMembersManager[0]["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.Member;
        ProjectOfficeMembersManager[0]["MeOthPId"] = txtMeIdSearch.Text;
        //************????????
        ////ProjectOfficeMembersManager[0]["MeOthPName"] = txtMeNameSearch.Text;
        /////************????????
        ProjectOfficeMembersManager[0]["CapacityDecrement"] = GetCapacityDecrement();
        ProjectOfficeMembersManager[0]["Wage"] = GetCapacityWage();
        ProjectOfficeMembersManager[0]["IsFree"] = 0;
        ProjectOfficeMembersManager[0]["IsDecreased"] = IsDecreased;
        if (IsDecreased == 1)
            ProjectOfficeMembersManager[0]["DecreasedDate"] = Utility.GetDateOfToday();
        ProjectOfficeMembersManager[0]["OfficeMemberTypeId"] = TSMemberType;
        ProjectOfficeMembersManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
        ProjectOfficeMembersManager[0]["ModifiedDate"] = DateTime.Now;
        ProjectOfficeMembersManager[0].EndEdit();

        ProjectOfficeMembersManager.Save();
        return true;
    }
    #endregion
    #endregion

    #region OtherMethods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MeId"></param>
    /// <returns>ArrayList[0]:MjId ; ArrayList[1]:MjName</returns>
    private ArrayList GetMajorIdWithName(int MeId)
    {
        ArrayList MajorArr = new ArrayList();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
        if (DocMemberFileMajorManager.Count != 0)
        {
            MajorArr.Add(DocMemberFileMajorManager[0]["MjId"]);
            MajorArr.Add(DocMemberFileMajorManager[0]["MjName"]);
        }
        return MajorArr;
    }

    private int GetMajor(int MeId)
    {
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
        if (DocMemberFileMajorManager.Count != 0)
            return Convert.ToInt32(DocMemberFileMajorManager[0]["MjId"]);
        else
            return -1;
    }

    private int GetDesignerTypeId()
    {
        switch (_PlansTypeId)
        {
            case (int)TSP.DataManager.TSPlansType.Sazeh:
                return (int)TSP.DataManager.TSDesignerType.Sazeh;

            case (int)TSP.DataManager.TSPlansType.TasisatBargh:
                return (int)TSP.DataManager.TSDesignerType.TasisatBargh;

            case (int)TSP.DataManager.TSPlansType.TasisatMechanic:
                return (int)TSP.DataManager.TSDesignerType.TasisatMechanic;

            case (int)TSP.DataManager.TSPlansType.Memari:
                return (int)TSP.DataManager.TSDesignerType.Memari;

            case (int)TSP.DataManager.TSPlansType.Shahrsazi:
                return (int)TSP.DataManager.TSDesignerType.Shahrsazi;
        }
        return -1;
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Attributes.Add("Style", "display:block");
        this.LabelWarning.Text = Warning;
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }
    }
    #endregion
    #region WF

    private int GetCurrentTaskCode(int WfCode, int TableId)
    {
        int CurrentTaskOrder = -2;
        int CurrentTaskCode = -2;

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        if (dtWorkFlowState.Rows.Count > 0)
        {
            CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
            CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
        }

        return CurrentTaskCode;
    }

    #region CheckWorkFlowPermissionForDesignerMode    

    private void CheckWorkFlowPermissionForSave(string PageMode, int ProjectTaskCode)
    {

        Boolean per = false;
        if (ProjectTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject
            || ProjectTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject
            || ProjectTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject
            || ProjectTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject
            || ProjectTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject)
        {

            switch (PageMode)
            {
                case "New":
                case "Edit":
                case "NewPlanReq":
                    per = true;
                    break;
                case "View":
                    per = false;
                    break;
            }
            if (_PlansId > 0)
            {
                TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
                DataTable dtPlan = PlansManager.SelectById(_PlansId, -1, -1, (int)TSP.DataManager.WorkFlowTask.SavePlanInfo);
                if (dtPlan.Rows.Count <= 0)
                {
                    per = false;
                }
            }
        }
        btnSave.Enabled = btnSave2.Enabled =
        btnEdit.Enabled = btnEdit2.Enabled = per;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    #endregion
    #endregion
    #region Check Insert Condittions
    private bool CheckCapacityBasedOnWorkRequest(int MeId, int CapacityValue)
    {
        CapacityCalculations CapacityCalculations = new CapacityCalculations();
        string Err = CapacityCalculations.CheckCapacityAndJobCount(MeId, CapacityValue, _CitId, TSP.DataManager.TSProjectIngridientType.Designer, null, _HasEngOffice || _HasOffice, Convert.ToBoolean(_IsBonyadMaskan), _PrjId);
        if (Err != "")
        {
            SetLabelWarning(Err);
            return false;
        }
        return true;

    }

    private bool CheckPlansMaster()
    {
        if (Convert.ToInt32(cmbDesMeType.Value) == (int)TSP.DataManager.TSMemberType.OtherPerson)
        {
            SetLabelWarning("کاردان نمی تواند نماینده باشد.");
            return false;
        }
        if (_PlansId == -1) return true;
        if (Utility.IsDBNullOrNullValue(_DesignerPlansId) || Utility.IsDBNullOrNullValue(_PlansId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }

        TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
        DesignerPlansManager.FindPlansMaster(_PlansId);
        if (DesignerPlansManager.Count > 0 && Convert.ToInt32(DesignerPlansManager[0]["DesignerPlansId"]) == _DesignerPlansId)
            return true;
        else if (DesignerPlansManager.Count > 0)
        {
            SetLabelWarning("این طراح نمی تواند نماینده باشد. برای این نقشه نماینده طراح انتخاب شده است.");
            return false;
        }
        return true;
    }

    private bool CheckPlansAttachType(TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager)
    {
        bool PlanBooklet = true; bool PlansMethod = true; bool ArchContract = true; bool FormNo5 = true; bool ElectInstalContract = true; bool FormNo6 = true; bool MechanInstalContract = true; bool StructureContract = true; bool CalculationFile = true;
        bool MechanicalPlan = true;
        bool ElectricalPlan = true;
        bool StructurePlan = true;
        bool ArchPlan = true;

        int GroupId = Convert.ToInt32(HiddenFieldPrjDes["GroupId"]);


        for (int i = 0; i < AttachmentsManager.Count; i++)
        {
            if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.PlanBooklet && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Sazeh)
                PlanBooklet = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.Plan && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Sazeh)
                StructurePlan = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.StructureContract && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Sazeh)
                StructureContract = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.CalculationFile && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Sazeh)
                CalculationFile = false;


            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.Plan && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Memari)
                ArchPlan = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.ArchContract && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Memari)
                ArchContract = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.FormNo5 && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Memari)
                FormNo5 = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.PlansMethod && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Memari)
                PlansMethod = false;


            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.FormNo6 && (_PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatMechanic || _PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatBargh))
                FormNo6 = false;


            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.Plan && _PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatBargh)
                ElectricalPlan = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.ElectInstalContract && _PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatBargh)
                ElectInstalContract = false;

            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.Plan && _PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatMechanic)
                MechanicalPlan = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.MechanInstalContract && _PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatMechanic)
                MechanInstalContract = false;



        }
        switch (_PlansTypeId)
        {
            case (int)TSP.DataManager.TSPlansType.Memari:

                if (ArchPlan || FormNo5)
                {
                    SetLabelWarning("مستندات پیوست معماری باید شامل حداقل یک نسخه از هر کدام از نقشه معماری و فرم شماره 5 باشد بارگذاری قرارداد و اصل دستور نقشه شهرداری فعلا اختیاری است.");
                    return false;
                }
                break;

            case (int)TSP.DataManager.TSPlansType.TasisatBargh:
                if (ElectricalPlan || FormNo6)
                {
                    SetLabelWarning("مستندات پیوست تاسیسات الکتریکی باید شامل حداقل یک نسخه از هر کدام از نقشه تاسیسات الکتریکی و فرم شماره 6 باشد بارگذاری قرارداد فعلا اختیاری است.");
                    return false;
                }

                break;

            case (int)TSP.DataManager.TSPlansType.TasisatMechanic:
                if (MechanicalPlan || FormNo6)
                {
                    SetLabelWarning("مستندات پیوست تاسیسات مکانیکی باید شامل حداقل یک نسخه از هر کدام از نقشه تاسیسات مکانیکی و فرم شماره 6 باشد بارگذاری قرارداد فعلا اختیاری است.");
                    return false;
                }
                break;

            case (int)TSP.DataManager.TSPlansType.Sazeh:
                if (StructurePlan || PlanBooklet || CalculationFile)
                {
                    SetLabelWarning("مستندات پیوست سازه باید شامل حداقل یک نسخه از هر کدام از نقشه سازه و فایل های محاسباتی و خلاصه دفترچه محاسباتی باشد بارگذاری قرارداد فعلا اختیاری است.");
                    return false;
                }
                break;
        }


        return true;
    }
    #endregion
    #region بدست آوردن و یا تنظیم مقادیر متراژ کسر ظرفیت طراح/متراژ دستمزد طراح وارد شده توسط کاربر
    private void SetCapacityWage(string CapacityWage)
    {
        CapacityUserControl.CapacityWage = CapacityWage;
    }

    private string GetCapacityWage()
    {
        return CapacityUserControl.CapacityWage;
    }

    private void SetCapacityDecrement(string CapacityDecrement)
    {
        CapacityUserControl.CapacityDecrement = CapacityDecrement;
    }

    private string GetCapacityDecrement()
    {
        return CapacityUserControl.CapacityDecrement;
    }

    private void SetCapcityUserControlEnable(Boolean Enable)
    {
        CapacityUserControl.CapacityDecrementEnable = Enable;
        CapacityUserControl.CapacityWageEnable = false;
        CapacityUserControl.txtcCapacityDecrementClientEnabled = false;
        CapacityUserControl.txtcWageClientEnabled = false;
    }
    #endregion    
    #region شرایط ثبت فیش جدید
    bool CheckNewAccountingConditions()
    {
        int PlanId = -1;

        TSP.DataManager.TechnicalServices.Designer_PlansManager Designer_PlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
        //TSP.DataManager.TechnicalServices.Project_ImplementerManager Project_ImplementerManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        //TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
        int CurrentTaskCode = CheckCurrentTaskCode();
        if (CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SavePlanAndDesignerInfo
                 || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject
                 || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject
                 || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject
                 || CurrentTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject)
        {
            if (!IsDesignerExist())
            {
                SetLabelWarning("برای این پروژه طراح تعریف نشده است.");
                return false;
            }

            if (PlanId == -1 || Utility.IsDBNullOrNullValue(PlanId))
            {
                PlanId = SetPlansIdAndPlanType();
                if (PlanId == -1 || _PlansTypeId == -1)
                {
                    SetLabelWarning("امکان ثبت فیش وجود ندارد.طراح متناسب با این مرحله از گردش کارثبت نشده است");
                    return false;
                }
            }
            HiddenFieldPrjDes["PlansIdForPrint"] = PlanId;
        }
        else
        {
            SetLabelWarning("امکان ثبت فیش طراح با توجه به مرحله گردش کار وجود ندارد.");
            return false;
        }

        return true;
    }

    private int CheckCurrentTaskCode()
    {
        int TableType = (int)TSP.DataManager.TableCodes.TSProjectRequest;

        TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
        return TSP.DataManager.WorkFlowPermission.GetCurrentTaskCode_StaticFunc(TableType, _PrjReqId);
    }

    private bool IsDesignerExist()
    {
        TSP.DataManager.TechnicalServices.Project_DesignerManager Project_DesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        Project_DesignerManager.FindActivesByProjectId(_PrjId);
        if (Project_DesignerManager.Count > 0)
            return true;
        else
            return false;
    }

    private int SetPlansIdAndPlanType()
    {

        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        int PlansId = -1;
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int CurrentTaskCode = GetCurrentTaskCode(WFCode, _PrjReqId);
        int PlansTypeId = -2;
        switch (CurrentTaskCode)
        {
            case (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject:
                PlansTypeId = (int)TSP.DataManager.TSPlansType.Memari;
                break;

            case (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject:
                PlansTypeId = (int)TSP.DataManager.TSPlansType.Sazeh;
                break;

            case (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject:
                PlansTypeId = (int)TSP.DataManager.TSPlansType.TasisatBargh;
                break;

            case (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject:
                PlansTypeId = (int)TSP.DataManager.TSPlansType.TasisatMechanic;
                break;
        }
        HiddenFieldPrjDes["PlansTypeIdForPrint"] = PlansTypeId;
        PlansManager.SelectMaxVersionForFish(_PrjId, 0, PlansTypeId);
        if (PlansManager.Count > 0)
            PlansId = Convert.ToInt32(PlansManager[0]["PlansId"]);
        return PlansId;
    }


    #endregion
    #region Plans Attachments

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                Session["PlansAttachName"] = Path.GetFileName(uploadedFile.PostedFile.FileName);
                ret = "MeId-" + Utility.GetCurrentUser_MeId().ToString() + "-PlansId-" + _PlansId.ToString() + "-ProjectId-" + _PrjId.ToString() + "-" + Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/TechnicalServices/Plans/") + ret) == true);
            string tempFileName = MapPath("~/Image/TechnicalServices/Plans/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["PlanAttachStatus"] = tempFileName;
        }
        return ret;
    }

    private TSP.DataManager.TechnicalServices.AttachmentsManager CreateAttachmentsManager()
    {
        TSP.DataManager.TechnicalServices.AttachmentsManager manager = new TSP.DataManager.TechnicalServices.AttachmentsManager();
        return manager;
    }

    private void AttachRowInserting()
    {

        if (Session["PlansAttachName"] == null || Session["PlanAttachStatus"] == null)
        {
            SetLabelWarning("خطایی در اضافه کردن رخ داده است");
            return;
        }
        if (cmbAttachType.SelectedIndex == -1)
        {
            SetLabelWarning("نوع فایل پیوست را انتخاب نمایید.");
            return;
        }

        HyperLinkFile.ClientVisible = false;

        try
        {

            TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager = (TSP.DataManager.TechnicalServices.AttachmentsManager)Session["AttachmentsManager"];


            DataRow AttachmentRow = AttachmentsManager.NewRow();

            AttachmentRow.BeginEdit();
            AttachmentRow["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans);
            AttachmentRow["TableTypeId"] = -1;

            AttachmentRow["AttachTypeId"] = cmbAttachType.Value;
            AttachmentRow["Title"] = cmbAttachType.Text;

            AttachmentRow["FilePath"] = "~/Image/TechnicalServices/Plans/" + Path.GetFileName(Session["PlanAttachStatus"].ToString());
            AttachmentRow["FileName"] = Session["PlansAttachName"].ToString();
            System.IO.FileInfo f = new FileInfo(MapPath("~/Image/TechnicalServices/Plans/" + Path.GetFileName(Session["PlanAttachStatus"].ToString())));
            long size = f.Length / 1024;
            AttachmentRow["FileSize"] = size.ToString() + " KB";
            AttachmentRow["UserId"] = Utility.GetCurrentUser_UserId();
            AttachmentRow["ModifiedDate"] = DateTime.Now;
            AttachmentRow["CreateDate"] = Utility.GetDateOfToday();

            AttachmentRow.EndEdit();

            AttachmentsManager.AddRow(AttachmentRow);

            GridViewAttachment.CancelEdit();
            GridViewAttachment.DataSource = AttachmentsManager.DataTable;
            GridViewAttachment.KeyFieldName = "AttachmentId";
            GridViewAttachment.DataBind();
            ClearAttachment();
        }
        catch (Exception err)
        {
            SetLabelWarning("خطایی در اضافه کردن رخ داده است");
        }
    }

    private void ClearAttachment()
    {
        Session["PlanAttachStatus"] = "";
        Session["PlansAttachName"] = "";

        SetAttachTypeFilterExpression();
        cmbAttachType.DataBind();
        cmbAttachType.SelectedIndex = -1;
    }

    #endregion
    #endregion

}

