using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

public partial class Employee_TechnicalServices_Project_ObserverInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    private int _PrjReqId
    {
        get
        {
            return Convert.ToInt32(RequestId.Value);
        }
        set
        {
            RequestId.Value = value.ToString();
        }
    }

    private int _PrjId
    {
        get
        {
            return Convert.ToInt32(HDProjectId.Value);
        }
        set
        {
            HDProjectId.Value = value.ToString();
        }
    }

    private int _Foundation
    {
        get
        {
            return Convert.ToInt32(HiddenFieldObserver["Foundation"]);
        }
        set
        {
            HiddenFieldObserver["Foundation"] = value.ToString();
        }
    }

    private int _GroupId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldObserver["GroupId"]);
        }
        set
        {
            HiddenFieldObserver["GroupId"] = value.ToString();
        }
    }

    private int _CitId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldObserver["CitId"]);
        }
        set
        {
            HiddenFieldObserver["CitId"] = value.ToString();
        }
    }

    private int _AgentId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldObserver["AgentId"]);
        }
        set
        {
            HiddenFieldObserver["AgentId"] = value.ToString();
        }
    }

    private int _IsCharity
    {
        get
        {
            return Convert.ToInt32(HiddenFieldObserver["IsCharity"]);
        }
        set
        {
            HiddenFieldObserver["IsCharity"] = value.ToString();
        }
    }

    private int _IsBonyadMaskan
    {
        get
        {
            return Convert.ToInt32(HiddenFieldObserver["IsBonyadMaskan"]);
        }
        set
        {
            HiddenFieldObserver["IsBonyadMaskan"] = value.ToString();
        }
    }
    private int _ObsWorkReqChangeId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldObserver["ObsWorkReqChangeId"]);
        }
        set
        {
            HiddenFieldObserver["ObsWorkReqChangeId"] = value.ToString();
        }
    }

    private int _DiscountPercentId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldObserver["DiscountPercentId"]);
        }
        set
        {
            HiddenFieldObserver["DiscountPercentId"] = value.ToString();
        }
    }

    private string _CurrentCapacityAssignmentYear
    {
        get
        {
            return HiddenFieldObserver["CurrentCapacityAssignmentYear"].ToString();
        }
        set
        {
            HiddenFieldObserver["CurrentCapacityAssignmentYear"] = value.ToString();
        }
    }
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

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

        if (!IsPostBack)
        {
            WorkRequestUserControl.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Observer;
            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Project.aspx");
            }

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Visible = BtnNew2.Visible = per.CanNew;
            btnEdit.Visible = btnEdit2.Visible = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode2"])) || (string.IsNullOrEmpty(Request.QueryString["PrjObsId"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode2"].ToString())) != "New"))
            {
                Response.Redirect("Observers.aspx?ProjectId=" + Server.HtmlDecode(Request.QueryString["ProjectId"]) + "&PageMode=" + Request.QueryString["PageMode"] + "&PrjReId=" + Server.HtmlDecode(Request.QueryString["PrjReId"]));
                return;
            }

            SetKeys();
            TSP.DataManager.Permission perComboPriceArchive = TSP.DataManager.TechnicalServices.PriceArchiveManager.ChoosePriceArchiveForObserverAndDesign(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            if (cmbPriceArchive.Enabled && perComboPriceArchive.CanEdit)
            {
                this.ViewState["comboArchive"] = cmbPriceArchive.Enabled = perComboPriceArchive.CanEdit;
            }
            else
                this.ViewState["comboArchive"] = cmbPriceArchive.Enabled = false;
            TSP.DataManager.Permission perTSSaveObserverWithOutCondition = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermissionTSSaveObserverWithOutCondition(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            if (CheckBoxSaveWithOutCondition.Enabled && perTSSaveObserverWithOutCondition.CanEdit)
            {
                this.ViewState["SaveObserverWithOutCondition"] = CheckBoxSaveWithOutCondition.Visible = perTSSaveObserverWithOutCondition.CanEdit;
            }
            else
                this.ViewState["SaveObserverWithOutCondition"] = CheckBoxSaveWithOutCondition.Visible = false;

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Visible;
            this.ViewState["BtnNew"] = BtnNew.Visible;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Visible = this.btnEdit2.Visible = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Visible = this.BtnNew2.Visible = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["comboYear"] != null)
            this.comboYear.Enabled = (bool)this.ViewState["comboYear"];
        if (this.ViewState["comboArchive"] != null)
            this.cmbPriceArchive.Enabled = (bool)this.ViewState["comboArchive"];
        if (this.ViewState["SaveDesignerWithOutCondition"] != null)
            this.CheckBoxSaveWithOutCondition.Visible = (bool)this.ViewState["SaveDesignerWithOutCondition"];
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("Edit");
        SetControlsEditMode();
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        HDObsId.Value = Utility.EncryptQS("-1");
        PgMode.Value = Utility.EncryptQS("New");
        SetControlsNewMode();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        if (ChbMother.Checked && cmbObsType.Value != null && Convert.ToInt32(cmbObsType.Value) != (int)TSP.DataManager.TSObserversType.Sazeh && Convert.ToInt32(cmbObsType.Value) != (int)TSP.DataManager.TSObserversType.Memari)
        {
            SetLabelWarning("ناظر هماهنگ کننده باید ناظر سازه یا معماری باشد");
            return;
        }
        if (cmbPriceArchive.Value == null)
        {
            SetLabelWarning("ثبت تعرفه خدمات مهندسی اجباری می باشد.");
            return;
        }
        switch (PageMode)
        {
            case "New":
            case "NewKardan":
                Insert();
                break;

            case "Edit":
                Update();
                break;
        }
    }

    protected void btnObsAcc_Click(object sender, EventArgs e)
    {

        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        if (!CheckNewAccountingConditions()) return;
        Response.Redirect("ProjectAccountingInsert.aspx?ProjectId=" + Utility.EncryptQS(_PrjId.ToString())// HDProjectId.Value
                  + "&AccountingId=" + Utility.EncryptQS("-1")
                  + "&PageMode=" + Request.QueryString["PageMode"]
                  + "&PageMode2=" + Utility.EncryptQS("New")
                  + "&PrjReId=" + Utility.EncryptQS(_PrjReqId.ToString())// RequestId.Value
                  + "&IngT=" + Utility.EncryptQS(((int)TSP.DataManager.TSProjectIngridientType.Observer).ToString())
                  + "&PlnId=" + Utility.EncryptQS("-1") +
                   "&PlnTypeId=" + Utility.EncryptQS("-1") +
                   "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);

    }

    bool CheckNewAccountingConditions()
    {
        TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();


        if (CheckCurrentTaskCode((int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject))
        {
            if (!IfObserverExist())
            {
                SetLabelWarning("امکان ثبت فیش ناظر وجود ندارد.برای این پروژه ناظر تعریف نشده است..");
                return false;
            }
        }
        else
        {
            SetLabelWarning("امکان ثبت فیش ناظر با توجه به مرحله گردش کار وجود ندارد.");
            return false;
        }
        return true;
    }

    private bool CheckCurrentTaskCode(int TaskCode)
    {
        ////****TableId        
        //PrjReId = Utility.DecryptQS(HiddenFieldPrjDes["PrjReId"].ToString());        
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.TSProjectRequest;

        TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
        return WorkFlowPermission.CheckCurrentTaskCode(TaskCode, TableType, _PrjReqId);
    }

    private bool IfObserverExist()
    {
        TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        ProjectObserversManager.FindByProjectId(_PrjId);
        if (ProjectObserversManager.Count > 0)
            return true;
        else
            return false;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect("Observers.aspx?ProjectId=" + Utility.EncryptQS(_PrjId.ToString())
            + "&PageMode=" + Request.QueryString["PageMode"]
            + "&PrjReId=" + Utility.EncryptQS(_PrjReqId.ToString())
            + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
    }

    protected void txtID_TextChanged(object sender, EventArgs e)
    {

        try
        {
            int MeOfOthId = -1;
            if (string.IsNullOrEmpty(txtID.Text))
            {
                SetLabelWarning("کد عضویت را وارد نمایید");
                return;
            }
            MeOfOthId = int.Parse(txtID.Text);
            if (CmbType.Value == null)
            {
                SetLabelWarning("نوع ناظر را انتخاب نمایید");
                return;
            }
            FillMeOfOthInfo(MeOfOthId, (TSP.DataManager.TSMemberType)(Convert.ToInt32(CmbType.Value)));
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }

    protected void txtFileNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string FileNo = "";
            if (!string.IsNullOrEmpty(txtFileNo.Text))
            {
                FileNo = txtFileNo.Text;
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DataTable dt = DocMemberFileManager.SelectMainRequestByMfNo(FileNo, 0);
                if (dt.Rows.Count > 0)
                {
                    int MeId = Convert.ToInt32(dt.Rows[0]["MeId"]);
                    txtID.Text = MeId.ToString();
                    FillMeOfOthInfo(MeId, TSP.DataManager.TSMemberType.Member);
                }
            }
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }
    protected void txtMeDocSerialNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string MFNoWithOutSerial = ""; int MFSerialNo = -2;
            if (!string.IsNullOrEmpty(txtMeDocSerialNo.Text) && !string.IsNullOrEmpty(txtMeDocProvCode.Text) && !string.IsNullOrEmpty(txtMeMjCode.Text))
            {
                MFNoWithOutSerial = txtMeDocProvCode.Text + "-" + txtMeMjCode.Text;
                MFSerialNo = int.Parse(txtMeDocSerialNo.Text);
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DataTable dt = DocMemberFileManager.SearchMemberFileBySepratedMfNo(MFNoWithOutSerial, MFSerialNo);
                if (dt.Rows.Count > 0)
                {
                    int MeId = Convert.ToInt32(dt.Rows[0]["MeId"]);
                    txtID.Text = MeId.ToString();
                    FillMeOfOthInfo(MeId, TSP.DataManager.TSMemberType.Member);
                }
                else
                {
                    ClearForm();
                    SetMember();
                    SetLabelWarning("چنین  شماره پروانه ای وجود ندارد.مجدداً وارد نمایید");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }

    protected void comboObsTypeByProjectInfo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btlSelectObserver_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Methods
    #region SetKey-Method
    private void SetKeys()
    {
        try
        {
            ResetHiddenFeild();
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            _PrjId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["ProjectId"]));
            HDObsId.Value = Server.HtmlDecode(Request.QueryString["PrjObsId"]).ToString();
            _PrjReqId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrjReId"]));

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string PrjObsId = Utility.DecryptQS(HDObsId.Value);
            string PrjPageMode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode2"].ToString()));

            if (string.IsNullOrEmpty(PageMode) || Utility.IsDBNullOrNullValue(_PrjId) || string.IsNullOrEmpty(PrjObsId) || Utility.IsDBNullOrNullValue(_PrjReqId) || string.IsNullOrEmpty(PrjPageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            FillProjectInfo(_PrjReqId);

            ObjectCapacityAssignment.SelectParameters["IsMainAgent"].DefaultValue = "-1";
            comboYear.DataBind();
            comboYear.SelectedIndex = 0;
            TSP.DataManager.Permission perComboYear = TSP.DataManager.TechnicalServices.Designer_PlansManager.GetUserPermissionChooseWorkYearForObserverAndDesign(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            this.ViewState["comboYear"] = comboYear.Enabled = perComboYear.CanEdit;

            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            if (_AgentId == Utility.GetCurrentAgentCode())
                CapacityAssignmentManager.SelectCurrentYearAndStage(1);
            else
                CapacityAssignmentManager.SelectCurrentYearAndStage(0);
            if (CapacityAssignmentManager.Count > 0)
            {
                _CurrentCapacityAssignmentYear = CapacityAssignmentManager[0]["Year"].ToString();
            }
            FillCapacityInfo();
            SetMode(PageMode);
            CheckWorkFlowPermission();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    private void SetMode(string PageMode)
    {
        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "New":
            case "NewKardan":
                SetNewModeKeys();
                break;

            case "Edit":
                SetEditModeKeys();
                break;
        }
    }

    private void SetNewModeKeys()
    {
        SetControlsNewMode();
    }

    private void SetEditModeKeys()
    {
        FillForm();
        SetControlsEditMode();
        this.ViewState["comboYear"] = comboYear.Enabled = false;//امکان ویرایش سال کاری در ویرایش حتی برای کسانی که دسترسی دارند وجود ندارد
    }

    private void SetViewModeKeys()
    {
        FillForm();
        SetControlsViewMode();
    }

    private void SetControlsNewMode()
    {
        ResetHiddenFeild();
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanNew)
        {
            BtnNew.Visible = BtnNew2.Visible = true;
            btnSave.Enabled = btnSave2.Enabled = true;
        }
        else
        {
            BtnNew.Visible = BtnNew2.Visible =
            btnSave.Enabled = btnSave2.Enabled = false;
        }

        btnEdit.Visible = btnEdit2.Visible = false;
        CheckAccess();

        SetEnabl(true);
        CapacityUserControl.Visible = true;
        ClearForm();
        ClearCapacity();
        SetDefualtPriceArchive();
        ASPxRoundPanel2.HeaderText = "جدید";
        string PageMode = Utility.DecryptQS(PgMode.Value);
        if (PageMode == "NewKardan")
        {
            CmbType.ClientEnabled = false;
            CmbType.DataBind();
            CmbType.SelectedIndex = cmbObsType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.OtherPerson).ToString()).Index;
            PanelDocumentFileNo.ClientVisible = false;
            lbltxtMeIdSearchTitle.Text = "كد عضويت كانون كاردان ها";
        }
    }

    private void SetControlsEditMode()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanNew)
            BtnNew.Visible = BtnNew2.Visible = true;
        else
            BtnNew.Visible = BtnNew2.Visible = false;
        if (per.CanEdit)
        {
            btnSave.Enabled = btnSave2.Enabled = true;
        }
        else
        {
            btnSave.Enabled = btnSave2.Enabled = false;
        }
        btnEdit.Visible = btnEdit2.Visible = false;
        CheckAccess();

        SetEnabl(false);
        Boolean CanEdit = true;
        if (!Utility.IsDBNullOrNullValue(Request.QueryString["PrAId"]))
            CanEdit = false;
        ChbMother.ClientEnabled =
        CheckBoxFivePercentPayment.ClientEnabled =
        ChbIsExteraFloor.Enabled =
        cmbObsType.ClientEnabled =
        txtArchitectorCode.ClientEnabled = ChbIsExteraFloor.Enabled = CanEdit;
        CheckBoxSaveWithOutCondition.Enabled = cmbPriceArchive.ClientEnabled =
        txtArchitectorCode.ClientEnabled =
        CapacityUserControl.Visible = true;
        SetCapcityUserControlEnable(true);

        ASPxRoundPanel2.HeaderText = "ویرایش";
    }

    private void SetControlsViewMode()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (per.CanNew)
            BtnNew.Visible = BtnNew2.Visible = true;
        else
            BtnNew.Visible = BtnNew2.Visible = false;
        if (per.CanEdit)
            btnEdit.Visible = btnEdit2.Visible = true;
        else
            btnEdit.Visible = btnEdit2.Visible = false;
        btnSave.Enabled = btnSave2.Enabled = false;
        CheckAccess();

        SetEnabl(false);
        CapacityUserControl.Visible = true;

        ASPxRoundPanel2.HeaderText = "مشاهده";
    }
    #endregion

    #region FillForm
    private void FillForm()
    {
        int PrjObsId = Convert.ToInt32(Utility.DecryptQS(HDObsId.Value));

        TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObsManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager CapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();

        try
        {
            ProjectObsManager.FindByProjectObserversId(PrjObsId);
            if (ProjectObsManager.Count > 0)
            {
                string TypeValue = ProjectObsManager[0]["MemberTypeId"].ToString();
                CmbType.DataBind();
                CmbType.SelectedIndex = CmbType.Items.FindByValue(TypeValue).Index;
                txtID.Text = ProjectObsManager[0]["MeOfficeOthPEngOId"].ToString();
                txtFileNo.Text = ProjectObsManager[0]["No"].ToString();

                txtMeDocProvCode.Text = ProjectObsManager[0]["No"].ToString().Substring(0, 2);
                txtMeMjCode.Text = ProjectObsManager[0]["No"].ToString().Substring(3, 3);



                ChbMother.Checked = Convert.ToBoolean(ProjectObsManager[0]["IsMother"]);
                if (!Utility.IsDBNullOrNullValue(ProjectObsManager[0]["PayFivePercent"]))
                    CheckBoxFivePercentPayment.Checked = Convert.ToBoolean(ProjectObsManager[0]["PayFivePercent"]);
                else
                    CheckBoxFivePercentPayment.Checked = false;
                if (!Utility.IsDBNullOrNullValue(ProjectObsManager[0]["IsExteraFloor"]))
                    ChbIsExteraFloor.Checked = Convert.ToBoolean(ProjectObsManager[0]["IsExteraFloor"]);
                else
                    ChbIsExteraFloor.Checked = false;

                if (!Utility.IsDBNullOrNullValue(ProjectObsManager[0]["PriceArchiveId"]))
                {
                    cmbPriceArchive.DataBind();
                    cmbPriceArchive.SelectedIndex = cmbPriceArchive.Items.FindByValue(ProjectObsManager[0]["PriceArchiveId"].ToString()).Index;
                }
                txtArchitectorCode.Text = ProjectObsManager[0]["ArchitectorCode"].ToString();

                cmbObsType.DataBind();
                cmbObsType.SelectedIndex = cmbObsType.Items.IndexOfValue(ProjectObsManager[0]["ObserversTypeId"].ToString());

                switch (Convert.ToUInt32(ProjectObsManager[0]["MemberTypeId"]))
                {
                    case (int)TSP.DataManager.TSMemberType.Member:
                        PanelDocumentFileNo.ClientVisible = true;
                        txtMeDocSerialNo.Text = Convert.ToInt32(ProjectObsManager[0]["No"].ToString().Substring(7)).ToString();
                        FillCapacity((int)TSP.DataManager.TSMemberType.Member, Convert.ToInt32(ProjectObsManager[0]["MeOfficeOthPEngOId"]));
                        SetMember();
                        break;
                    case (int)TSP.DataManager.TSMemberType.OtherPerson:
                        SetKardan();
                        #region عضوی نظام کاردان ها
                        PanelDocumentFileNo.ClientVisible = false;
                        TSP.DataManager.OtherPersonManager OthpManager = new TSP.DataManager.OtherPersonManager();
                        OthpManager.FindByCode(Convert.ToInt32(ProjectObsManager[0]["MeOfficeOthPEngOId"]));
                        txtID.Text = OthpManager[0]["OtpCode"].ToString();
                        #endregion
                        break;
                    case (int)TSP.DataManager.TSMemberType.ExpArchitect:
                        SetMemar();
                        break;
                }
                FillProjectIngridientCapacityInfo((TSP.DataManager.TSMemberType)Convert.ToUInt32(ProjectObsManager[0]["MemberTypeId"]), txtID.Text);

                if (!Utility.IsDBNullOrNullValue(ProjectObsManager[0]["Year"]))
                    comboYear.SelectedIndex = comboYear.Items.FindByText(ProjectObsManager[0]["Year"].ToString()).Index;
                else
                    comboYear.SelectedIndex = -1;
                CapacityDecrementManager.FindByPrjImpObsDsgnIdAndIngridientTypeId(PrjObsId, (int)TSP.DataManager.TSProjectIngridientType.Observer);
                if (CapacityDecrementManager.Count > 0)
                {
                    SetCapacityDecrement(CapacityDecrementManager[0]["CapacityDecrement"].ToString());
                    SetCapacityWage(CapacityDecrementManager[0]["Wage"].ToString());
                    CheckBoxSaveWithOutCondition.Checked = Utility.IsDBNullOrNullValue(CapacityDecrementManager[0]["SaveWithOutCondition"]) ? false : Convert.ToBoolean(CapacityDecrementManager[0]["SaveWithOutCondition"]);
                }

            }
            else
            {
                SetLabelWarning("امکان مشاهده اطلاعات وجود ندارد.اطلاعات توسط کاربر دیگری تغییر یافته است");
            }
        }
        catch (Exception)
        {
            SetLabelWarning("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }

    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);

        _IsCharity = prjInfo.IsCharity;
        _IsBonyadMaskan = prjInfo.IsBonyadMaskan;
        _CitId = prjInfo.CitId;
        _GroupId = prjInfo.GroupId;
        _Foundation = prjInfo.Foundation;
        _DiscountPercentId = prjInfo.DiscountPercentId;
        _AgentId = prjInfo.AgentId;
    }

    private void FillCapacity(int MemberTypeId, int MeId)
    {
        Capacity capacity = new Capacity();
        capacity.GetCapacityInformation((int)TSP.DataManager.TSProjectIngridientType.Observer, MemberTypeId, MeId, true);
    }

    private void FillMeOfOthInfo(int MeOfOthId, TSP.DataManager.TSMemberType TSMemberType)
    {
        WorkRequestUserControl.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Observer;
        WorkRequestUserControl.NeedCheckConditions = !CheckBoxSaveWithOutCondition.Checked;


        if (!CheckBoxSaveWithOutCondition.Checked)
        {
            WorkRequestUserControl.SetUserControlVisible(true);
            WorkRequestUserControl.UserControlvisible = true;
            if (TSMemberType == TSP.DataManager.TSMemberType.Member && !CheckWorkRequestAndDocument(MeOfOthId))
            {
                ClearForm();
                SetMember();
                return;
            }
        }
        if (!WorkRequestUserControl.FillForm(MeOfOthId.ToString(), TSMemberType))
        {
            SetLabelWarning(WorkRequestUserControl.ErrorMessage);
            ClearForm();
            SetMember();
            string PageMode = Utility.DecryptQS(PgMode.Value);
            if (PageMode == "NewKardan")
            {
                CmbType.ClientEnabled = false;
                CmbType.DataBind();
                CmbType.SelectedIndex = cmbObsType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.OtherPerson).ToString()).Index;
                PanelDocumentFileNo.ClientVisible = false;
            }
            return;
        }
        if (WorkRequestUserControl.AgentId == Utility.GetCurrentAgentCode())
            ObjectCapacityAssignment.SelectParameters["IsMainAgent"].DefaultValue = "1";
        else
            ObjectCapacityAssignment.SelectParameters["IsMainAgent"].DefaultValue = "0";
        comboYear.DataBind();
        comboYear.SelectedIndex = 0;
        switch (WorkRequestUserControl.MajorParentIdInWorkReq)
        {
            case (int)TSP.DataManager.MainMajors.Mapping:
                cmbObsType.SelectedIndex = cmbObsType.Items.FindByValue(((int)TSP.DataManager.TSObserversType.Mapping).ToString()).Index;
                break;
            case (int)TSP.DataManager.MainMajors.Architecture:
                cmbObsType.SelectedIndex = cmbObsType.Items.FindByValue(((int)TSP.DataManager.TSObserversType.Memari).ToString()).Index;
                break;
            case (int)TSP.DataManager.MainMajors.Civil:
                cmbObsType.SelectedIndex = cmbObsType.Items.FindByValue(((int)TSP.DataManager.TSObserversType.Sazeh).ToString()).Index;
                break;
            case (int)TSP.DataManager.MainMajors.Electronic:
                cmbObsType.SelectedIndex = cmbObsType.Items.FindByValue(((int)TSP.DataManager.TSObserversType.TasisatBargh).ToString()).Index;
                break;
            case (int)TSP.DataManager.MainMajors.Mechanic:
                cmbObsType.SelectedIndex = cmbObsType.Items.FindByValue(((int)TSP.DataManager.TSObserversType.TasisatMechanic).ToString()).Index;
                break;
        }
        txtFileNo.Text = WorkRequestUserControl.FileNo;
        if (TSMemberType == TSP.DataManager.TSMemberType.Member)
        {
            txtArchitectorCode.Text = WorkRequestUserControl.MeArchitectorCode;
            txtMeDocProvCode.Text = WorkRequestUserControl.FileNo.Substring(0, 2);
            txtMeMjCode.Text = WorkRequestUserControl.FileNo.Substring(3, 3);
            txtMeDocSerialNo.Text = Convert.ToInt32(WorkRequestUserControl.FileNo.Substring(7)).ToString();
            if (TSMemberType == TSP.DataManager.TSMemberType.Member)
                FillProjectIngridientCapacityInfo(TSP.DataManager.TSMemberType.Member, MeOfOthId.ToString());
            PanelDocumentFileNo.ClientVisible = true;
        }
        else if (TSMemberType == TSP.DataManager.TSMemberType.OtherPerson)
        {
            PanelDocumentFileNo.ClientVisible = false;
        }
    }
    #endregion

    #region Fill Capacity Info
    /// <summary>
    /// پر کردن اطلاعات ظرفیت شرکت یا دفتر
    /// </summary>
    /// <param name="TSMemberTypeId"></param>
    /// <param name="ProjectIngridientId">MeId</param>
    private void FillProjectIngridientCapacityInfo(TSP.DataManager.TSMemberType TSMemberTypeId, string ProjectIngridientId)
    {
        WorkRequestUserControl.MeId = Convert.ToInt32(ProjectIngridientId);
        CapacityUserControl.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Observer;
        WorkRequestUserControl.FillForm(ProjectIngridientId, TSMemberTypeId);
        FillCapacityInfo();
        if (TSMemberTypeId != TSP.DataManager.TSMemberType.OtherPerson)
        {
            txtFileNo.Text = WorkRequestUserControl.FileNo;
            txtMeDocProvCode.Text = WorkRequestUserControl.FileNo.Substring(0, 2);
            txtMeMjCode.Text = WorkRequestUserControl.FileNo.Substring(3, 3);
            txtMeDocSerialNo.Text = Convert.ToInt32(WorkRequestUserControl.FileNo.Substring(7)).ToString();
            FillCapacityInfo();
            CapacityUserControl.FillProjectIngridienCapacityInfo(TSMemberTypeId, Convert.ToInt32(ProjectIngridientId), Convert.ToInt32(ProjectIngridientId));
        }
        if (WorkRequestUserControl.AgentId == Utility.GetCurrentAgentCode())
            ObjectCapacityAssignment.SelectParameters["IsMainAgent"].DefaultValue = "1";
        else
            ObjectCapacityAssignment.SelectParameters["IsMainAgent"].DefaultValue = "0";
        comboYear.DataBind();
        comboYear.SelectedIndex = 0;
    }

    private void FillCapacityInfo()
    {
        CapacityUserControl.ProjectId = _PrjId;
        CapacityUserControl.PrjReqId = _PrjReqId;
        CapacityUserControl.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Observer;
    }
    #endregion

    #region Set Controls Visible-Enabled

    private void SetEnabl(bool Enabled)
    {
        SetCapcityUserControlEnable(Enabled);
        txtID.ClientEnabled =
        CmbType.ClientEnabled =
        txtFileNo.ClientEnabled =
        ChbMother.ClientEnabled =
        cmbObsType.ClientEnabled =
        txtArchitectorCode.ClientEnabled =
        cmbPriceArchive.ClientEnabled =
        txtMeDocProvCode.ClientEnabled =
        txtMeMjCode.ClientEnabled =
        txtMeDocSerialNo.ClientEnabled =
        CheckBoxFivePercentPayment.ClientEnabled = ChbIsExteraFloor.Enabled =
        comboYear.ClientEnabled = CheckBoxSaveWithOutCondition.Enabled = Enabled;
    }

    private void SetMember()
    {

        lblMeDocProvCode.ClientVisible = true;
        lblMeMjCode.ClientVisible = true;
        lblMeDocSerialNo.ClientVisible = true;

        txtMeDocProvCode.ClientVisible = true;
        txtMeMjCode.ClientVisible = true;
        txtMeDocSerialNo.ClientVisible = true;

        CmbType.SelectedIndex = CmbType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.Member).ToString()).Index;

    }

    private void SetKardan()
    {
        CmbType.SelectedIndex = CmbType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.OtherPerson).ToString()).Index;

    }

    private void SetMemar()
    {

        CmbType.SelectedIndex = CmbType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.Member).ToString()).Index;

    }

    #endregion

    #region Clear Form
    private void ClearForm()
    {
        txtID.Text = "";
        CmbType.DataBind();
        CmbType.SelectedIndex = 0;
        SetMember();
        txtFileNo.Text = "";
        txtArchitectorCode.Text = "";
        ChbMother.Checked = false;
        cmbObsType.DataBind();
        cmbObsType.SelectedIndex = -1;
        txtMeDocProvCode.Text = "";
        txtMeMjCode.Text = "";
        txtMeDocSerialNo.Text = "";
        CheckBoxFivePercentPayment.Checked = false;
        ChbIsExteraFloor.Checked = false;
        // comboYear.SelectedIndex = -1;
        _ObsWorkReqChangeId = -1;
        WorkRequestUserControl.SetUserControlVisible(false);
        WorkRequestUserControl.UserControlvisible = false;
        WorkRequestUserControl.ClearForm();
    }

    private void ClearCapacity()
    {
        SetCapacityDecrement("");
        SetCapacityWage("");
        CapacityUserControl.ClearControlsIngridienCapacityInfo();
    }
    #endregion

    #region Insert-Update
    private void Insert()
    {
        #region Define Managers
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObsManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager CapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OtherPersonManager OthpManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocOffMemberAcceptedGradeManager MemberGradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        TSP.DataManager.TechniciansActivityAreasManager TechniciansActivityAreasManager = new TSP.DataManager.TechniciansActivityAreasManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        trans.Add(ProjectObsManager);
        trans.Add(CapacityDecrementManager);
        trans.Add(OfMeManager);
        trans.Add(OffManager);
        trans.Add(MeManager);
        #endregion        
        Nullable<int> OfId = null;

        int TypeValue = Convert.ToInt32(CmbType.Value);
        int MeOfOthId = int.Parse(txtID.Text);
        int OtpId = -1;
        Boolean CapacityRelatedToPreviousYears = false;
        try
        {
            #region Check Conditions
            if (Utility.IsDBNullOrNullValue(CmbType.Value))
            {
                SetLabelWarning("نوع ناظر را انتخاب نمایید");
                return;
            }
            if (string.IsNullOrEmpty(MeOfOthId.ToString()))
            {
                SetLabelWarning("کد ناظر را وارد نمایید");
                return;
            }
            //////////////////////////
            if (Utility.IsDBNullOrNullValue(comboYear.SelectedItem))
            {
                SetLabelWarning("سال کاری انتخاب نشده است");
                return;
            }

            switch (TypeValue)
            {
                case (int)TSP.DataManager.TSMemberType.Member://Member
                    #region Member
                    MeManager.FindByCode(MeOfOthId);

                    #region CheckMe Membership And Document Conditions

                    if (Convert.ToInt32(MeManager[0]["MrsId"]) != 1)
                    {
                        SetLabelWarning("وضعیت عضو مورد نظر تایید شده نمی باشد");
                        return;
                    }
                    if (Convert.ToBoolean(MeManager[0]["InActive"]))
                    {
                        SetLabelWarning("عضو مورد نظر غیر فعال می باشد");
                        return;
                    }
                    if (string.Compare(_CurrentCapacityAssignmentYear, comboYear.SelectedItem.Text) > 0)//اگر سال کاری کوچکتر از سال کای جاری باشد.ظرفیت ها از امسال کسر نمی شود و فقط ثبت می شود
                    {
                        CapacityRelatedToPreviousYears = true;
                    }
                    else if (!CheckBoxSaveWithOutCondition.Checked)
                    {

                        if (!CheckWorkRequestAndDocument(MeOfOthId))
                            return;
                    }
                    #endregion
                    #region Check Capacity
                    if (!CapacityRelatedToPreviousYears && !CheckBoxSaveWithOutCondition.Checked)
                    {
                        if (!CheckCapacityBasedOnWorkRequest(MeOfOthId, int.Parse(GetCapacityDecrement())))
                            return;
                    }
                    #endregion
                    OfMeManager.FindOffMemberByPersonId(MeOfOthId, 2);
                    if (OfMeManager.Count > 0)
                    {
                        OffManager.FindByCode(Convert.ToInt32(OfMeManager[0]["OfId"]));
                        if (OffManager.Count > 0)
                        {
                            if (!Utility.IsDBNullOrNullValue(OffManager[0]["MFType"]))
                            {
                                if ((Convert.ToInt32(OffManager[0]["MFType"]) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign) || (Convert.ToInt32(OffManager[0]["MFType"]) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesignAndImplement))
                                {
                                    OfId = Convert.ToInt32(OfMeManager[0]["OfId"]);
                                }
                            }
                        }
                    }

                    if (Utility.IsDBNullOrNullValue(txtArchitectorCode.Text))
                    {
                        SetLabelWarning("ثبت کد نظارت شهرداری جهت چاپ  گزارش نامه به شهرداری الزامی می باشد ");
                        return;
                    }
                    #endregion
                    break;
                case (int)TSP.DataManager.TSMemberType.OtherPerson://Kardan                   
                    #region Kardan
                    OthpManager.FindKardanAndMemarByOtpCode(MeOfOthId.ToString(), (int)TSP.DataManager.OtherPersonType.Kardan);
                    OtpId = Convert.ToInt32(OthpManager[0]["OtpId"]);
                    #region CheckFileNo
                    if (!CheckBoxSaveWithOutCondition.Checked)
                    {
                        if (Utility.IsDBNullOrNullValue(OthpManager[0]["FileNo"]))
                        {
                            SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان ناظر وجود ندارد.شخص انتخاب شده دارای پروانه اشتغال به کار نمی باشد.");
                            return;
                        }
                        DataTable dt = MemberGradeManager.FindByOtpIdAndResId(OtpId, (int)TSP.DataManager.DocumentResponsibilityType.Observation, 0);
                        if (dt.Rows.Count == 0)
                        {
                            SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان ناظر وجود ندارد.شخص انتخاب شده دارای صلاحیت نظارت نمی باشد.");
                            return;
                        }
                    }
                    #endregion

                    #endregion
                    break;

                case (int)TSP.DataManager.TSMemberType.ExpArchitect://Memar                   
                    #region Memar
                    OthpManager.FindKardanAndMemarByOtpCode(MeOfOthId.ToString(), (int)TSP.DataManager.OtherPersonType.Memar);
                    OtpId = Convert.ToInt32(OthpManager[0]["OtpId"]);
                    #region CheckFileNo
                    if (!CheckBoxSaveWithOutCondition.Checked)
                    {
                        if (Utility.IsDBNullOrNullValue(OthpManager[0]["FileNo"]))
                        {
                            SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان ناظر وجود ندارد.شخص انتخاب شده دارای پروانه اشتغال به کار نمی باشد.");
                            return;
                        }
                        DataTable dtOtMemberGrade = MemberGradeManager.FindByOtpIdAndResId(OtpId, (int)TSP.DataManager.DocumentResponsibilityType.Observation, 0);
                        if (dtOtMemberGrade.Rows.Count == 0)
                        {
                            SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان ناظر وجود ندارد.شخص انتخاب شده دارای صلاحیت نظارت نمی باشد.");
                            return;
                        }
                    }
                    #endregion
                    #endregion
                    break;
            }

            #endregion
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
        try
        {
            trans.BeginSave();

            DataRow drPrjObs = ProjectObsManager.NewRow();
            #region Insert Observer
            drPrjObs["ProjectId"] = _PrjId;
            drPrjObs["PrjReId"] = _PrjReqId;
            int MemberTypeId = -2;
            switch (TypeValue)
            {
                case (int)TSP.DataManager.TSMemberType.Member:
                    drPrjObs["MeOfficeOthPEngOId"] = MeOfOthId;
                    drPrjObs["MemberTypeId"] = MemberTypeId = (int)TSP.DataManager.TSMemberType.Member;
                    break;
                case (int)TSP.DataManager.TSMemberType.OtherPerson:
                    drPrjObs["MeOfficeOthPEngOId"] = OtpId;
                    MeOfOthId = OtpId;
                    drPrjObs["MemberTypeId"] = MemberTypeId = (int)TSP.DataManager.TSMemberType.OtherPerson;
                    break;
                case (int)TSP.DataManager.TSMemberType.ExpArchitect:
                    drPrjObs["MeOfficeOthPEngOId"] = OtpId;
                    MeOfOthId = OtpId;
                    drPrjObs["MemberTypeId"] = MemberTypeId = (int)TSP.DataManager.TSMemberType.ExpArchitect;
                    break;
            }

            drPrjObs["IsMother"] = ChbMother.Checked;
            drPrjObs["PayFivePercent"] = CheckBoxFivePercentPayment.Checked;
            drPrjObs["IsExteraFloor"] = ChbIsExteraFloor.Checked;
            drPrjObs["ObserversTypeId"] = cmbObsType.Value;
            drPrjObs["PriceArchiveId"] = cmbPriceArchive.Value;
            drPrjObs["Year"] = comboYear.Text;
            drPrjObs["CreateDate"] = Utility.GetDateOfToday();
            drPrjObs["UserId"] = Utility.GetCurrentUser_UserId();
            drPrjObs["ModifiedDate"] = DateTime.Now;
            ProjectObsManager.AddRow(drPrjObs);
            if (ProjectObsManager.Save() <= 0)
            {
                trans.CancelSave();
                SetLabelWarning("خطایی در ذخیره اطلاعات انجام گرفته است");
                return;
            }
            #endregion
            int PrjObsId = Convert.ToInt32(ProjectObsManager[0]["ProjectObserversId"]);
            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            CapacityCalculations CapacityCalculations = new CapacityCalculations();
            int IsWorkFree = (Convert.ToBoolean(_IsCharity)|| CapacityRelatedToPreviousYears == true) ? 1 : 0;
            int IsFree = CapacityRelatedToPreviousYears == true ? 1 : 0;
            CapacityCalculations.InsertProjectCapacityDecrement(CapacityDecrementManager, GetCapacityDecrement()
                , GetCapacityWage()
                , (Int16)TSP.DataManager.TSProjectIngridientType.Observer
                , (int)TSP.DataManager.TSProjectIngridientType.Observer, PrjObsId, OfId
                , Utility.GetCurrentUser_UserId(), MeOfOthId, MemberTypeId, _PrjId, IsFree
                , CapacityRelatedToPreviousYears == true ? comboYear.SelectedItem.Text + "/12/28" : Utility.GetDateOfToday()
                , CheckBoxSaveWithOutCondition.Checked, IsWorkFree);


            if (TypeValue == (int)TSP.DataManager.TSMemberType.Member)
            {
                ProjectRequestManager.UpdateObserverPriceByObserverId(PrjObsId, trans, ProjectObsManager);
            }

            //****************************
            if (TypeValue == (int)TSP.DataManager.TSMemberType.Member)
            {
                if (MeManager[0]["ArchitectorCode"].ToString() != txtArchitectorCode.Text.Trim())
                {
                    MeManager[0].BeginEdit();
                    MeManager[0]["ArchitectorCode"] = txtArchitectorCode.Text.Trim();
                    MeManager[0].EndEdit();
                    MeManager.Save();
                }
                if (!CapacityRelatedToPreviousYears)
                {
                    TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
                    trans.Add(ObserverWorkRequestManager);
                    int CapacityErr = CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, CapacityDecrementManager, MeOfOthId, Utility.GetCurrentUser_UserId(), _PrjId, _CitId, Convert.ToBoolean(_IsCharity), TSP.DataManager.TSProjectIngridientType.Observer, null, false, false);
                    if (CapacityErr != 0)
                    {
                        trans.CancelSave();
                        SetLabelWarning(CapacityCalculations.FindErrorMessage(CapacityErr));
                        return;
                    }
                }
            }
            //****************************
            trans.EndSave();
            HDObsId.Value = Utility.EncryptQS(PrjObsId.ToString());
            PgMode.Value = Utility.EncryptQS("Edit");
            SetControlsEditMode();

            TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
            Project_ObserversManager.FindObsMother(_PrjId);
            if (Project_ObserversManager.Count > 1)
            {
                SetLabelWarning("ذخیره انجام شد." + "این پروژه " + Project_ObserversManager.Count.ToString() + " ناظر هماهنگ کننده دارد.قبل از ثبت فیش از صحت  ناظران هماهنگ کننده اطمینان حاصل کنید");
            }
            else
                SetLabelWarning("ذخیره انجام شد");
            WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.TSProjectConfirming, _PrjReqId, (int)TSP.DataManager.TableType.TSProject_Observers, "ثبت ناظر جدید خارج از سیستم ارجاع " + (CheckBoxSaveWithOutCondition.Checked == true ? "گزینه ''ثبت اطلاعات بدون در نظر گرفتن کلیه پیش شرط ها'' انتخاب شده است " : ""), Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.InsertNewRow);
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
            SetLabelWarning("خطا در ذخیره انجام شد");
        }

        if (Convert.ToUInt32(CmbType.Value) == (int)TSP.DataManager.TSMemberType.Member)
            SetMember();
        else if (Convert.ToUInt32(CmbType.Value) == (int)TSP.DataManager.TSMemberType.OtherPerson)
            SetKardan();
        else if (Convert.ToUInt32(CmbType.Value) == (int)TSP.DataManager.TSMemberType.ExpArchitect)
            SetMemar();

    }

    private void Update()
    {
        int PrjObsId = Convert.ToInt32(Utility.DecryptQS(HDObsId.Value));

        if (IsPageRefresh)
            return;
        #region Define Managers
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObsManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager CapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        trans.Add(ProjectObsManager);
        trans.Add(CapacityDecrementManager);
        trans.Add(OfMeManager);
        trans.Add(OffManager);
        trans.Add(MeManager);
        #endregion

        Nullable<int> OfId = null;        

        try
        {
            if (cmbPriceArchive.Value == null)
            {
                SetLabelWarning("ثبت تعرفه خدمات مهندسی اجباری می باشد.");
                return;
            }

            int MeOfOthId = int.Parse(txtID.Text);
            Boolean CapacityRelatedToPreviousYears = false;
            switch (Convert.ToInt32(CmbType.Value))
            {
                case (int)TSP.DataManager.TSMemberType.Member:
                    if (string.Compare(_CurrentCapacityAssignmentYear, comboYear.SelectedItem.Text) > 0)//اگر سال کاری کوچکتر از سال کاری جاری باشد.ظرفیت ها از امسال کسر نمی شود و فقط ثبت می شود
                    {
                        CapacityRelatedToPreviousYears = true;
                    }
                    else if (!CheckBoxSaveWithOutCondition.Checked)
                    {
                        int DiffrenceCapacity1 = 0;
                        CapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(_PrjId, PrjObsId, (int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer);
                        if (CapacityDecrementManager.Count > 0)
                        {
                            DiffrenceCapacity1 = Convert.ToInt32(GetCapacityDecrement()) - Convert.ToInt32(CapacityDecrementManager[0]["CapacityDecrement"]);
                            if (DiffrenceCapacity1 < 0 || string.Compare(CapacityDecrementManager[0]["DecreasedDate"].ToString(), "1398/03/25") < 0)
                                DiffrenceCapacity1 = 0;
                        }
                        if (DiffrenceCapacity1 != 0 && !CheckCapacityBasedOnWorkRequest(MeOfOthId, DiffrenceCapacity1))
                            return;
                    }
                    break;
            }
            trans.BeginSave();

            ProjectObsManager.FindByProjectObserversId(PrjObsId);
            if (ProjectObsManager.Count != 1)
            {
                trans.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            ProjectObsManager[0].BeginEdit();
            ProjectObsManager[0]["ObserversTypeId"] = cmbObsType.Value;
            ProjectObsManager[0]["IsMother"] = ChbMother.Checked;
            ProjectObsManager[0]["PayFivePercent"] = CheckBoxFivePercentPayment.Checked;
            ProjectObsManager[0]["IsExteraFloor"] = ChbIsExteraFloor.Checked;
            ProjectObsManager[0]["PriceArchiveId"] = cmbPriceArchive.Value;
            ProjectObsManager[0]["Year"] = comboYear.Text;
            ProjectObsManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ProjectObsManager[0]["ModifiedDate"] = DateTime.Now;
            ProjectObsManager[0].EndEdit();

            if (ProjectObsManager.Save() <= 0)
            {
                trans.CancelSave();
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
                return;
            }

            if (Convert.ToInt32(ProjectObsManager[0]["MemberTypeId"]) == (int)TSP.DataManager.TSMemberType.Member)
            {
                int MeId = Convert.ToInt32(ProjectObsManager[0]["MeOfficeOthPEngOId"]);
                if (Utility.IsDBNullOrNullValue(txtArchitectorCode.Text))
                {
                    SetLabelWarning("ثبت کد نظارت شهرداری جهت چاپ  گزارش نامه به شهرداری الزامی می باشد ");
                    return;
                }
                MeManager.FindByCode(MeId);
                if (MeManager.Count != 1)
                {
                    SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                    trans.CancelSave();
                    return;
                }
                if (MeManager[0]["ArchitectorCode"].ToString() != txtArchitectorCode.Text.Trim())
                {
                    MeManager[0].BeginEdit();
                    MeManager[0]["ArchitectorCode"] = txtArchitectorCode.Text.Trim();
                    MeManager[0].EndEdit();
                    MeManager.Save();
                }

                OfMeManager.FindOffMemberByPersonId(MeId, 2);
                if (OfMeManager.Count > 0)
                {
                    OffManager.FindByCode(Convert.ToInt32(OfMeManager[0]["OfId"]));
                    if (OffManager.Count > 0)
                    {
                        if (!Utility.IsDBNullOrNullValue(OffManager[0]["MFType"]))
                        {
                            if ((Convert.ToInt32(OffManager[0]["MFType"]) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign) || (Convert.ToInt32(OffManager[0]["MFType"]) == (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesignAndImplement))
                            {
                                OfId = Convert.ToInt32(OfMeManager[0]["OfId"]);
                            }
                        }
                    }
                }
            }

            CapacityCalculations CapacityCalculations = new CapacityCalculations();
            int DiffrenceCapacity = CapacityCalculations.UpdateProjectCapacityDecrement(CapacityDecrementManager, GetCapacityDecrement(), GetCapacityWage(), OfId
                , (Int16)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer, PrjObsId, CapacityRelatedToPreviousYears == true ? 1 : 0, 1, Utility.GetCurrentUser_UserId(), _PrjId, CheckBoxFivePercentPayment.Checked);


            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            ProjectRequestManager.UpdateObserverPriceByObserverId(PrjObsId, trans, ProjectObsManager);
            if (!CapacityRelatedToPreviousYears)
            {
                if (Convert.ToInt32(ProjectObsManager[0]["MemberTypeId"]) == (int)TSP.DataManager.TSMemberType.Member)
                {
                    TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
                    trans.Add(ObserverWorkRequestManager);
                    int CapacityErr = CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, CapacityDecrementManager, MeOfOthId, Utility.GetCurrentUser_UserId(), _PrjId, _CitId, Convert.ToBoolean(_IsCharity), TSP.DataManager.TSProjectIngridientType.Observer, null, false, false,false);
                    if (CapacityErr != 0)
                    {
                        trans.CancelSave();
                        SetLabelWarning(CapacityCalculations.FindErrorMessage(CapacityErr));
                        return;
                    }
                }
            }
            trans.EndSave();
            TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
            Project_ObserversManager.FindObsMother(_PrjId);
            if (Project_ObserversManager.Count > 1)
            {
                SetLabelWarning("ذخیره انجام شد." + "این پروژه " + Project_ObserversManager.Count.ToString() + " ناظر هماهنگ کننده دارد.قبل از ثبت فیش از صحت  ناظران هماهنگ کننده اطمینان حاصل کنید");
            }
            else
                SetLabelWarning("ذخیره انجام شد");
            WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.TSProjectConfirming, _PrjReqId, (int)TSP.DataManager.TableType.TSProject_Observers, "ویرایش اطلاعات ناظر " + (CheckBoxSaveWithOutCondition.Checked == true ? "گزینه ''ثبت اطلاعات بدون در نظر گرفتن کلیه پیش شرط ها'' انتخاب شده است " : ""), Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.UpdateInfo);
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }

        if (Convert.ToUInt32(CmbType.Value) == (int)TSP.DataManager.TSMemberType.Member)
            SetMember();
        else if (Convert.ToUInt32(CmbType.Value) == (int)TSP.DataManager.TSMemberType.OtherPerson)
            SetKardan();
        else if (Convert.ToUInt32(CmbType.Value) == (int)TSP.DataManager.TSMemberType.ExpArchitect)
            SetMemar();

    }
    #endregion

    #region Check Conditions
    private bool IsDecreased()
    {
        int PrjObsId = Convert.ToInt32(Utility.DecryptQS(HDObsId.Value));
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager CapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        CapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(_PrjId, PrjObsId, (int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer);
        if (CapacityDecrementManager.Count > 0)
            return Convert.ToBoolean(CapacityDecrementManager[0]["IsDecreased"]);
        return false;
    }

    private bool CheckCapacityBasedOnWorkRequest(int MeId, int CapacityValue)
    {
        CapacityCalculations CapacityCalculations = new CapacityCalculations();
        string Err = CapacityCalculations.CheckCapacityAndJobCount(MeId, CapacityValue, _CitId, TSP.DataManager.TSProjectIngridientType.Observer, null, false, Convert.ToBoolean(_IsBonyadMaskan), _PrjId);
        if (Err != "")
        {
            SetLabelWarning(Err);
            return false;
        }
        return true;

    }

    private Boolean CheckWorkRequestAndDocument(int MeId)
    {
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();

        DataTable dtObWorkRequest = ObserverWorkRequestManager.SelectTSObserverWorkRequestByMember(MeId, TSP.DataManager.TSObserverWorkRequestStatus.Confirm);
        if (dtObWorkRequest.Rows.Count == 0)
        {
            SetLabelWarning("عضو انتخاب شده دارای آماده به کاری نظارت  تایید شده نمی باشد.");
            return false;
        }

        string DocMeFileExpireDate = dtObWorkRequest.Rows[0]["DocMeFileExpireDate"].ToString();
        if (Convert.ToInt32(dtObWorkRequest.Rows[0]["ObsId"]) == -2)
        {
            SetLabelWarning("امکان ثبت عضو مورد نظر به عنوان ناظر وجود ندارد.براساس آخرین درخواست تایید شده آماده بکاری ایشان دارای صلاحیت نظارت/ صلاحیت نقشه برداری نمی باشد");
            return false;
        }
        if (string.IsNullOrEmpty(DocMeFileExpireDate))
        {
            SetLabelWarning("امکان ثبت عضو مورد نظر به عنوان ناظر وجود ندارد.مدت زمان اعتبار پروانه اشتغال عضو به نامشحص می باشد.");
            return false;
        }

        if (DocMeFileExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
        {
            if (!(string.Compare(DocMeFileExpireDate, "1398/11/01") > 0 && string.Compare(DocMeFileExpireDate, "1400/04/31") <= 0))
            {
                if (Utility.IsObserverDocExpireDateChecked())
                {
                    SetLabelWarning("امکان ثبت عضو مورد نظر به عنوان ناظر وجود ندارد.مدت زمان اعتبار پروانه اشتغال عضو به پایان رسیده است.");
                    return false;
                }
                else
                {
                    if (HiddenFieldObserver["ShowAlert"].ToString() == "0")
                    {
                        SetLabelWarning("هشدار: مدت زمان اعتبار پروانه اشتغال عضو به پایان رسیده است.");
                        HiddenFieldObserver["ShowAlert"] = 1;
                        HiddenFieldObserver["AlertMsg"] = "هشدار: مدت زمان اعتبار پروانه اشتغال عضو به پایان رسیده است.";
                        return false;
                    }
                    else
                        return true;
                }
            }
        }

        #region CheckCity

        bool IsInCity = false;

        if ((!Utility.IsDBNullOrNullValue(dtObWorkRequest.Rows[0]["City1"]) && (Convert.ToInt32(dtObWorkRequest.Rows[0]["City1"]) == _CitId))
        || (!Utility.IsDBNullOrNullValue(dtObWorkRequest.Rows[0]["City2"]) && Convert.ToInt32(dtObWorkRequest.Rows[0]["City2"]) == _CitId)
        || (_CitId == (int)TSP.DataManager.CityCode.KhanZenyan && Convert.ToInt32(dtObWorkRequest.Rows[0]["KhanZenyanObserveMeter"]) != 0)
        || (_CitId == (int)TSP.DataManager.CityCode.Dareyon && Convert.ToInt32(dtObWorkRequest.Rows[0]["DareyonObserveMeter"]) != 0)
        || (_CitId == (int)TSP.DataManager.CityCode.Zarghan && Convert.ToInt32(dtObWorkRequest.Rows[0]["ZarghanObserveMeter"]) != 0)
        || (_CitId == (int)TSP.DataManager.CityCode.Lapooy && Convert.ToInt32(dtObWorkRequest.Rows[0]["LapooyObserveMeter"]) != 0
        || (Convert.ToBoolean(dtObWorkRequest.Rows[0]["WantShahrakSanatiMeter"]) && _DiscountPercentId == (int)TSP.DataManager.TSDiscountPercent.Industrial))
        )
        {
            IsInCity = true;
        }
        if (!IsInCity)
        {
            SetLabelWarning("شهرهای انتخاب شده توسط عضو در فرم آماده به کاری با شهر پروژه همخوانی ندارد.");
            return false;
        }

        #endregion

        return true;
    }

    //private bool CheckIsMother()
    //{
    //    int PrjObsId = Convert.ToInt32(Utility.DecryptQS(HDObsId.Value));

    //    TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObsManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();

    //    if (ChbMother.Checked)
    //    {
    //        ProjectObsManager.FindByProjectId(_PrjId);
    //        for (int i = 0; i < ProjectObsManager.Count; i++)
    //        {
    //            if (Convert.ToBoolean(ProjectObsManager[i]["IsMother"]))
    //            {
    //                if (!Convert.ToBoolean(ProjectObsManager[i]["InActive"]) && PrjObsId != Convert.ToInt32(ProjectObsManager[i]["ProjectObserversId"]))
    //                {
    //                    SetLabelWarning("ناظر هماهنگ کننده قبلاً انتخاب شده است");
    //                    return false;
    //                }
    //            }
    //        }
    //    }
    //    return true;
    //}
    #endregion

    #region WF
    private void CheckWorkFlowPermission()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        CheckWorkFlowPermissionForEdit(PageMode);
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        //*******Editing Task Code
        int ProjectWFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject;

        int ChangeWFCode = (int)TSP.DataManager.WorkFlows.TSObserverChangesConfirming;
        int ChangeTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveChangeProjectObserverRequestInfo;

        TSP.DataManager.WFPermission SaveWFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(SaveTaskCode, ProjectWFCode, _PrjReqId, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission ChangeWFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(ChangeTaskCode, ChangeWFCode, _PrjReqId, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.Permission perObsSaving = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        this.ViewState["BtnNew"] = BtnNew.Visible = BtnNew2.Visible = perObsSaving.CanNew && (SaveWFPer.BtnNew || ChangeWFPer.BtnNew);
        this.ViewState["BtnEdit"] = btnEdit.Visible = btnEdit2.Visible = perObsSaving.CanEdit && (SaveWFPer.BtnEdit || ChangeWFPer.BtnEdit);
        this.ViewState["BtnSave"] = btnSave.Enabled = btnSave2.Enabled = SaveWFPer.BtnSave || ChangeWFPer.BtnSave;
    }
    #endregion

    #region Set Warning-Error
    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        //if (!string.IsNullOrEmpty(this.LabelWarning.Text))
        //    this.LabelWarning.Text += Warning;
        //else
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

    private void CheckAccess()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        if (BtnNew.Visible == true)
        {
            BtnNew.Visible = BtnNew2.Visible = per.CanNew;
        }

        if (btnEdit.Visible == true)
        {
            btnEdit.Visible = btnEdit2.Visible = per.CanEdit;
        }

        if (PageMode == "New" && btnSave.Enabled == true)
        {
            btnSave.Enabled = btnSave2.Enabled = per.CanNew;
        }
        if (PageMode == "Edit" && btnSave.Enabled == true)
        {
            btnSave.Enabled = btnSave2.Enabled = per.CanEdit;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Visible;
        this.ViewState["BtnNew"] = BtnNew.Visible;
    }

    #region بدست آوردن و یا تنظیم مقادیر متراژ کسر ظرفیت ناظر/متراژ دستمزد ناظر وارد شده توسط کاربر
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
        CapacityUserControl.CapacityWageEnable = Enable;
    }
    #endregion

    private void ResetHiddenFeild()
    {
        HiddenFieldObserver["ShowAlert"] = 0;
        HiddenFieldObserver["AlertMsg"] = "";
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
}
