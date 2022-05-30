using System;
using System.Data;

public partial class Employee_TechnicalServices_Project_AddPlanDesigner : System.Web.UI.Page
{
    #region Properties
    Boolean IsPageRefresh = false;
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
    private string _PageMode
    {
        get { return HiddenFieldPrjDes["PageMode"].ToString(); }
        set { HiddenFieldPrjDes["PageMode"] = value; }
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
            return Convert.ToInt32(HiddenFieldPrjDes["PrjId"]);
        }
        set
        {
            HiddenFieldPrjDes["PrjId"] = value.ToString();
        }
    }
    private int _PlansId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["PlansId"]);
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
            TSP.DataManager.Permission perPlanDes = TSP.DataManager.TechnicalServices.Designer_PlansManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = perPlanDes.CanNew;
            btnNew2.Enabled = perPlanDes.CanNew;
            btnSave.Enabled = perPlanDes.CanNew || perPlanDes.CanEdit;
            btnSave2.Enabled = perPlanDes.CanNew || perPlanDes.CanEdit;
            btnEdit.Enabled = perPlanDes.CanEdit;
            btnEdit2.Enabled = perPlanDes.CanEdit;

            if (Utility.IsDBNullOrNullValue(Request.QueryString["GrdFlt"]) || Utility.IsDBNullOrNullValue(Request.QueryString["SrchFlt"].ToString()))
            {
                Response.Redirect("Project.aspx");
                return;
            }
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();

            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Project.aspx?" + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
            }

            if (Request.QueryString["DsPId"] == null || Request.QueryString["PgMd"] == null || Request.QueryString["PrjDesignerId"] == null || (!perPlanDes.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PgMd"].ToString())) != "New"))
            {
                string QS = "ProjectId=" + Request.QueryString["ProjectId"].ToString() +
                    "&PrjReId=" + Request.QueryString["PrjReId"].ToString() +
                    "&PageMode=" + Request.QueryString["PageMode"].ToString() +
                    "&PlanPageMode=" + Request.QueryString["PlanPageMode"].ToString() +
                    "&PlnId=" + Request.QueryString["PlnId"].ToString() +
                    "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

                Response.Redirect("PlanDesigner.aspx?" + QS);
            }
            SetKey();

            TSP.DataManager.Permission perComboPriceArchive = TSP.DataManager.TechnicalServices.PriceArchiveManager.ChoosePriceArchiveForObserverAndDesign(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            if (cmbPriceArchive.Enabled && perComboPriceArchive.CanEdit)
            {
                this.ViewState["comboArchive"] = cmbPriceArchive.Enabled = perComboPriceArchive.CanEdit;
            }
            else
                this.ViewState["comboArchive"] = cmbPriceArchive.Enabled = false;
            TSP.DataManager.Permission perTSSaveDesignerWithOutCondition = TSP.DataManager.TechnicalServices.Designer_PlansManager.GetUserPermissionTSSaveDesignerWithOutCondition(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            if (CheckBoxSaveWithOutCondition.Enabled && perTSSaveDesignerWithOutCondition.CanEdit)
            {
                this.ViewState["SaveDesignerWithOutCondition"] = CheckBoxSaveWithOutCondition.Visible = perTSSaveDesignerWithOutCondition.CanEdit;
            }
            else
                this.ViewState["SaveDesignerWithOutCondition"] = CheckBoxSaveWithOutCondition.Visible = false;

            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["btnSave"] = btnSave.Enabled;
        }

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["btnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["btnSave"];
        if (this.ViewState["comboYear"] != null)
            this.comboYear.Enabled = (bool)this.ViewState["comboYear"];
        if (this.ViewState["comboArchive"] != null)
            this.cmbPriceArchive.Enabled = (bool)this.ViewState["comboArchive"];
        if (this.ViewState["SaveDesignerWithOutCondition"] != null)
            this.CheckBoxSaveWithOutCondition.Visible = (bool)this.ViewState["SaveDesignerWithOutCondition"];
        cmbDesMeType.Enabled = false;
        ChbIsExteraFloor.Visible = false;//طراح اضافه اشکوب ندارد.در صورتی که اضافه شد.از آن استفاده می کنیم.:)
    }

    #region Btn Click
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        _PageMode = "New";
        _PlansId = -1;
        SetNewModeKeys();
        CheckWorkFlowPermissionForDesignerMode();
    }

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
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "";
        if (Request.QueryString["PageSender"] != null && Utility.DecryptQS(Request.QueryString["PageSender"]) == "PlanDesigner")
        {
            QS = "ProjectId=" + Utility.EncryptQS(_PrjId.ToString()) +
                          "&PrjReId=" + Utility.EncryptQS(_PrjReqId.ToString()) +
                          "&PageMode=" + Request.QueryString["PageMode"] +
                          "&PlnId=" + Utility.EncryptQS(_PlansId.ToString()) +
                          "&PlnPgMd=" + Request.QueryString["PlnPgMd"]
                           + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
            Response.Redirect("PlanDesigner.aspx?" + QS);
        }
        else
        {
            QS = "ProjectId=" + Utility.EncryptQS(_PrjId.ToString()) +
         "&PrjReId=" + Utility.EncryptQS(_PrjReqId.ToString()) +
         "&PageMode=" + Request.QueryString["PageMode"] +
         "&PlnId=" + Utility.EncryptQS(_PlansId.ToString()) +
         "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
            Response.Redirect("Designers.aspx?" + QS);
        }
    }

    protected void btnDesAcc_Click(object sender, EventArgs e)
    {

        try
        {
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();

            if (_CurrentPrjTaskCode == (int)TSP.DataManager.WorkFlowTask.SavePlanAndDesignerInfo
                      || _CurrentPrjTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject
                      || _CurrentPrjTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject
                      || _CurrentPrjTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject
                      || _CurrentPrjTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject
                      || _CurrentPrjTaskCode == (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject)
            {

                HiddenFieldPrjDes["PlansIdForPrint"] = -1;
                Response.Redirect("ProjectAccountingDesignerInsert.aspx?ProjectId=" + Utility.EncryptQS(_PrjId.ToString())
                      + "&AccountingId=" + Utility.EncryptQS("-1")
                      + "&PageMode=" + Request.QueryString["PageMode"]
                      + "&PageMode2=" + Utility.EncryptQS("New")
                      + "&PrjReId=" + Utility.EncryptQS(_PrjReqId.ToString())
                      + "&IngT=" + Utility.EncryptQS(((int)TSP.DataManager.TSProjectIngridientType.Designer).ToString())
                      + "&PlnId=" + Utility.EncryptQS(_PlansId.ToString())
                      + "&PlnTypeId=" + Utility.EncryptQS(_PlansTypeId.ToString())
                      + "&GrdFlt=" + GrdFlt
                      + "&SrchFlt=" + SrchFlt);
            }
            else
            {
                SetLabelWarning("امکان ثبت فیش طراح با توجه به مرحله گردش کار وجود ندارد.");
                return;
            }
        }
        catch (Exception ex) { }


    }
    #endregion

    protected void txtMeIdSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string MemberId = "-1";
            if (string.IsNullOrEmpty(txtMeIdSearch.Text))
            {
                SetLabelWarning("کد عضویت را وارد نمایید");
                return;
            }
            MemberId = txtMeIdSearch.Text;
            ClearAllFormInfo();
            FillAllFormInfoByMeIdForOffMeSearch(MemberId);
        }
        catch (Exception ex)
        {
            SetLabelWarning("خطا در بازیابی اطلاعات ایجاد شده است");
            Utility.SaveWebsiteError(ex);
        }
    }

    protected void txtDocSerialNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            #region FindAllInfoByMFNo
            string MFNoWithOutSerial = ""; int MFSerialNo = -2;
            if (string.IsNullOrEmpty(txtDocSerialNo.Text) || string.IsNullOrEmpty(txtDocProvCode.Text) || string.IsNullOrEmpty(txtMjCode.Text))
            {

                ClearAllFormInfo();
                SetLabelWarning("شماره سریال پروانه را وارد نمایید");
                return;
            }
            //if (string.IsNullOrWhiteSpace(txtSearchFileNo.Text))
            //{
            //    SetLabelWarning("شماره سریال پروانه را وارد نمایید");
            //    ClearAllFormInfo();
            //    return;
            //}
            MFNoWithOutSerial = txtDocProvCode.Text + "-" + txtMjCode.Text;
            MFSerialNo = int.Parse(txtDocSerialNo.Text);
            TSP.DataManager.DocMemberFileManager DocMemberFileManager2 = new TSP.DataManager.DocMemberFileManager();
            DataTable dtMf = DocMemberFileManager2.SearchMemberFileBySepratedMfNo(MFNoWithOutSerial, MFSerialNo);
            if (dtMf.Rows.Count > 0)
            {
                string MemberId = dtMf.Rows[0]["MeId"].ToString();
                txtMeIdSearch.Text = MemberId.ToString();
                txtSearchFileNo.Text = dtMf.Rows[0]["MFNo"].ToString();
                FillAllFormInfoByMeIdForOffMeSearch(MemberId);
            }
            else
            {
                ClearAllFormInfo();
                SetLabelWarning("عضوی با شماره پروانه وارد شده یافت نشد");
                return;
            }
            #endregion

        }
        catch (Exception ex)
        {
            SetLabelWarning("خطا در بازیابی اطلاعات ایجاد شده است");
            Utility.SaveWebsiteError(ex);
        }
    }


    protected void txtSearchFileNo_TextChanged(object sender, EventArgs e)
    {
        #region FindAllInfoByMFNo
        if (string.IsNullOrWhiteSpace(txtSearchFileNo.Text))
        {
            txtMeIdSearch.Text = "";
            ClearAllFormInfo();
            SetLabelWarning("عضوی با شماره پروانه وارد شده یافت نشد");
            return;
        }
        string FileNo = txtSearchFileNo.Text;
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DataTable dt = DocMemberFileManager.SelectMainRequestByMfNo(FileNo, 0);
        if (dt.Rows.Count > 0)
        {
            string MemberId = dt.Rows[0]["MeId"].ToString();
            txtMeIdSearch.Text = MemberId.ToString();
            FillAllFormInfoByMeIdForOffMeSearch(MemberId);
        }
        else
        {
            ClearAllFormInfo();
            SetLabelWarning("عضوی با شماره پروانه وارد شده یافت نشد");
            return;
        }
        #endregion
    }

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int ProjectReTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest);
        int WfCode = -1;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dt = WorkFlowStateManager.SelectLastState(ProjectReTableType, _PrjReqId);
        if (dt.Rows.Count > 0)
            WfCode = Convert.ToInt32(dt.Rows[0]["WorkFlowCode"]);

        if (WfCode == -1)
        {
            WFUserControl.SetMsgText("گردش کار درخواست جاری نامشخص است");
            return;
        }
        string QS = "~/Employee/TechnicalServices/Project/AddPlanDesigner.aspx?DsPId=" + Utility.EncryptQS(_DesignerPlansId.ToString()) +
          "&PgMd=" + Utility.EncryptQS(_PageMode) +
          "&ProjectId=" + Utility.EncryptQS(_PrjId.ToString()) +
          "&PrjReId=" + Utility.EncryptQS(_PrjReqId.ToString()) +
          "&PlnId=" + Utility.EncryptQS(_PlansId.ToString()) +
          "&PageMode=" + HiddenFieldPrjDes["PrjPgMd"].ToString() +
          "&PrjDesignerId=" + Utility.EncryptQS(_PrjDesignerId.ToString()) +
          "&GrdFlt=" + Request.QueryString["GrdFlt"].ToString()
          + "&SrchFlt=" + Request.QueryString["SrchFlt"].ToString();

        if (Request.QueryString["PlnPgMd"] != null)
        {
            QS += "&PlnPgMd=" + Request.QueryString["PlnPgMd"] + "&PageSender=" + Utility.EncryptQS("PlanDesigner");
        }

        WFUserControl.QueryStringForRedirect = QS;
        WFUserControl.PerformCallback(_PrjReqId, ProjectReTableType, WfCode, e);
    }
    #endregion

    #region Methods

    #region SetKeys
    private void SetKey()
    {
        try
        {

            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.TSPlansConfirming).ToString();
            HiddenFieldPrjDes["PrjPgMd"] = Request.QueryString["PageMode"].ToString();
            _PlansTypeId = -2;
            _DesignerPlansId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["DsPId"]));
            _PageMode = Utility.DecryptQS(Request.QueryString["PgMd"]);
            _PlansId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PlnId"]));
            _PrjId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["ProjectId"]));
            _PrjReqId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrjReId"]));
            _PrjDesignerId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrjDesignerId"]));
            ClearAllFormInfo();
            FillProjectInfo(_PrjReqId);
            TSP.DataManager.Permission perComboYear = TSP.DataManager.TechnicalServices.Designer_PlansManager.GetUserPermissionChooseWorkYearForObserverAndDesign(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            this.ViewState["comboYear"] = comboYear.Enabled = perComboYear.CanEdit;
            FillCapacityInfo();
            ObjectDataSourceMemberType.FilterExpression = "MemberTypeId=" + (int)TSP.DataManager.TSMemberType.Office
                + " OR MemberTypeId=" + (int)TSP.DataManager.TSMemberType.EngOffice
                + " OR MemberTypeId=" + (int)TSP.DataManager.TSMemberType.Member
                + " OR MemberTypeId=" + (int)TSP.DataManager.TSMemberType.OtherPerson;

            SetMode(_PageMode);
            CheckWorkFlowPermissionForDesignerMode();
            cmbPlanType.Visible = true;
            lblcmbPlanType.Visible = true;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetLabelWarning("خطا در بازیابی اطلاعات ایجاد شده است");
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
                SetNewModeKeys();
                break;

            case "Edit":
                SetEditModeKeys();
                break;
        }
    }

    private void SetNewModeKeys()
    {
        ClearAllFormInfo();
        SetControlsEnable(true);

        BtnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        CheckAccess();

        RoundPanelDes.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        FillForm(_PrjDesignerId);
        SetControlsEnable(true);
        CheckBoxSaveWithOutCondition.Enabled = false;
        SetControlsEditMode();
        this.ViewState["comboYear"] = comboYear.Enabled = false;//امکان ویرایش سال کاری در ویرایش حتی برای کسانی که دسترسی دارند وجود ندارد

    }

    private void SetViewModeKeys()
    {
        FillForm(_PrjDesignerId);
        SetControlsEnable(false);
        BtnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        CheckAccess();
        RoundPanelDes.HeaderText = "مشاهده";
    }

    private void SetControlsEditMode()
    {
        BtnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        CheckAccess();
        cmbDesMeType.Enabled = false;
        RoundPanelSearch.Enabled = false;
        RoundPanelDes.HeaderText = "ویرایش";
    }

    private void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.Designer_PlansManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perPrjOffMembers = TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());


        if (BtnNew.Enabled == true)
        {
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
        }

        if (btnEdit.Enabled == true)
        {
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
        }

        if (_PageMode == "New" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;
        }
        if (_PageMode == "Edit" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
        }


        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["btnSave"] = btnSave.Enabled;
    }
    #endregion    
    #region ClearForm
    private void ClearAllFormInfo()
    {
        _MeMajorParentIdInWorkReq = -1; _IsCiviObserver = false;
        _OfId = -1; _EngOfId = -1; _HasEngOffice = _HasOffice = false;
        _ObsWorkReqChangeId = -1;
        RoundPanelMemberEngOfficeInfo.Visible = UserControlMeEngOfficeInfoUserControl.Visible = UserControlMeOfficeInfoUserControl.Visible = false;
        WorkRequestUserControl.SetUserControlVisible(false);
        WorkRequestUserControl.UserControlvisible = false;
        WorkRequestUserControl.ClearForm();
        cmbDesMeType.SelectedIndex = -1;
        lblWarningsearchOfEngInfo.Visible = false;
        txtMeIdSearch.Text = "";
        txtSearchFileNo.Text = "";
        chbIsMaster.Checked = true;
        ChbIsExteraFloor.Checked = false;
        SetCapacityDecrement("");
        SetCapacityWage("");
        CapacityUserControl.ClearControlsIngridienCapacityInfo();
        txtSearchFileNo.Text =
        txtDocProvCode.Text =
        txtMjCode.Text =
        txtDocSerialNo.Text = "";


        #region تنظیم سال کاری و تعرفه به آخرین سال و تعرفه 

        ObjectCapacityAssignment.SelectParameters["IsMainAgent"].DefaultValue = "-1";
        comboYear.DataBind();
        comboYear.SelectedIndex = 0;
        _CurrentCapacityAssignmentYear = "";
        TSP.DataManager.TechnicalServices.PriceArchiveManager PriceArchiveManager = new TSP.DataManager.TechnicalServices.PriceArchiveManager();
        PriceArchiveManager.FindLastPriceArchive();
        if (PriceArchiveManager.Count == 0)
            return;
        cmbPriceArchive.DataBind();
        cmbPriceArchive.SelectedIndex = cmbPriceArchive.Items.FindByValue(PriceArchiveManager[0]["PriceArchiveId"].ToString()).Index;
        #endregion
    }
    #endregion

    #region FillForm
    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Convert.ToInt32(Id));
        _IsCharity = prjInfo.IsCharity;
        _IsBonyadMaskan = prjInfo.IsBonyadMaskan;
        _CitId = prjInfo.CitId;
        _GroupId = prjInfo.GroupId;
        _Foundation = prjInfo.Foundation;
        _FundationDifference = prjInfo.FundationDifference;
        _StructureSkeletonId = prjInfo.StructureSkeletonId;
        _AgentId = prjInfo.AgentId;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dtWorkFlowState = new DataTable();
        _CurrentPrjTaskCode = -2;
        dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode((int)TSP.DataManager.WorkFlows.TSProjectConfirming, _PrjReqId);
        if (dtWorkFlowState.Rows.Count > 0)
        {
            _CurrentPrjTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
        }
        TSP.DataManager.CityManager CityManager = new TSP.DataManager.CityManager();
        CityManager.FindByCode(_CitId);
        if (CityManager.Count != 0)
        {
            _CanObserverBeDesigner = Convert.ToBoolean(CityManager[0]["CanObserverBeDesigner"]);
        }
        else
            _CanObserverBeDesigner = false;
    }
    private void FillAllFormInfoByMeIdForOffMeSearch(string MeId)
    {
        try
        {
            RoundPanelMemberEngOfficeInfo.Visible = UserControlMeEngOfficeInfoUserControl.Visible = UserControlMeOfficeInfoUserControl.Visible = false;
            if (CmbMembershipType.SelectedIndex == 0)//عضو حقیقی
            {
                WorkRequestUserControl.NeedCheckConditions = !CheckBoxSaveWithOutCondition.Checked;
                WorkRequestUserControl.SetUserControlVisible(true);
                WorkRequestUserControl.UserControlvisible = true;
            }
            WorkRequestUserControl.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Designer;
            if (!WorkRequestUserControl.FillForm(MeId, (TSP.DataManager.TSMemberType)(Convert.ToInt16(CmbMembershipType.SelectedItem.Value))))
            {
                ClearAllFormInfo();
                SetLabelWarning(WorkRequestUserControl.ErrorMessage);
                return;
            }
            if (_CanObserverBeDesigner
                   && WorkRequestUserControl.IsCiviObserver//***** رشته عمران می تواند طراحی معماری الف بنایی را انجام بدهد  
                   && (
                   (_GroupId == (int)TSP.DataManager.TSStructureGroups.A && _StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Ajory)
                   ||( _GroupId == (int)TSP.DataManager.TSStructureGroups.A && _StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Mix)))
            {
                _IsCiviObserver = true;
            }
            int MemberEngOfId = -1;
            if (CmbMembershipType.SelectedIndex == 0)//عضو حقیقی
            {
                _MeMajorParentIdInWorkReq = WorkRequestUserControl.MajorParentIdInWorkReq;
                txtSearchFileNo.Text = WorkRequestUserControl.FileNo;
                txtDocProvCode.Text = WorkRequestUserControl.FileNo.Substring(0, 2);
                txtMjCode.Text = WorkRequestUserControl.FileNo.Substring(3, 3);
                txtDocSerialNo.Text = Convert.ToInt32(WorkRequestUserControl.FileNo.Substring(7)).ToString();
                FillCapacityInfo();
                CapacityUserControl.FillProjectIngridienCapacityInfo(TSP.DataManager.TSMemberType.Member, MemberEngOfId, Convert.ToInt32(MeId));
                if (WorkRequestUserControl.AgentId == Utility.GetCurrentAgentCode())
                    ObjectCapacityAssignment.SelectParameters["IsMainAgent"].DefaultValue = "1";
                else
                    ObjectCapacityAssignment.SelectParameters["IsMainAgent"].DefaultValue = "0";
                comboYear.DataBind();
                comboYear.SelectedIndex = 0;
                TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
                if (WorkRequestUserControl.AgentId == Utility.GetCurrentAgentCode())
                    CapacityAssignmentManager.SelectCurrentYearAndStage(1);
                else
                    CapacityAssignmentManager.SelectCurrentYearAndStage(0);
                if (CapacityAssignmentManager.Count > 0)
                {
                    _CurrentCapacityAssignmentYear = CapacityAssignmentManager[0]["Year"].ToString();
                }

                PanelDocumentFileNo.ClientVisible = true;
                if (!CheckBoxSaveWithOutCondition.Checked)
                { //*****اگر عضو دفتر باشد
                    UserControlMeEngOfficeInfoUserControl.FillInfo(Convert.ToInt32(MeId));
                    Boolean IsEngOfficeIsExpired = UserControlMeEngOfficeInfoUserControl.IsExpired;
                    Boolean HasEngOffice = _HasEngOffice = UserControlMeEngOfficeInfoUserControl.HasEngOffice;
                    int EngOfId = _EngOfId = UserControlMeEngOfficeInfoUserControl.EngOfId;
                    RoundPanelMemberEngOfficeInfo.Visible = UserControlMeEngOfficeInfoUserControl.Visible = HasEngOffice;


                    if (HasEngOffice)//اگر عضو دفتر باشد
                    {
                        #region اگر عضو دفتر باشد
                        txtMeIdSearch.Text = MeId.ToString();
                        cmbDesMeType.DataBind();
                        cmbDesMeType.SelectedIndex = cmbDesMeType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.EngOffice).ToString()).Index;
                        MemberEngOfId = EngOfId;

                        #endregion
                    }
                    else
                    {
                        //****اگر عضو شرکت باشد
                        UserControlMeOfficeInfoUserControl.FillInfo(Convert.ToInt32(MeId));
                        Boolean IsOfficeIsExpired = UserControlMeOfficeInfoUserControl.IsExpired;
                        Boolean HasOffice = _HasOffice = UserControlMeOfficeInfoUserControl.HasOffice;
                        int OfId = _OfId = UserControlMeOfficeInfoUserControl.OfId;
                        RoundPanelMemberEngOfficeInfo.Visible = UserControlMeOfficeInfoUserControl.Visible = HasOffice;
                        if (HasOffice)
                        {
                            #region اگر عضو شرکت باشد       
                            txtMeIdSearch.Text = MeId.ToString();
                            cmbDesMeType.DataBind();
                            cmbDesMeType.SelectedIndex = cmbDesMeType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.Office).ToString()).Index;
                            MemberEngOfId = OfId;
                            #endregion
                        }
                        else
                        {

                            if (WorkRequestUserControl.IsCiviObserver &&
                                ((_GroupId == (int)TSP.DataManager.TSStructureGroups.A && _StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Ajory)
                                || (_GroupId == (int)TSP.DataManager.TSStructureGroups.A && _StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Mix)))
                            {
                                #region اگر عضو ناظر رشته عمران باشد 
                                txtMeIdSearch.Text = MeId.ToString();
                                cmbDesMeType.DataBind();
                                cmbDesMeType.SelectedIndex = cmbDesMeType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.Member).ToString()).Index;
                                MemberEngOfId = -1;
                                _IsCiviObserver = true;
                                #endregion

                            }
                            else
                            {

                                SetLabelWarning("عضو انتخاب شده عضو دفتر و یا شرکت و یا رشته عمران/معماری با صلاحیت مورد نیاز نمی باشد");
                                ClearAllFormInfo();
                            }

                        }
                    }
                }
                else
                {
                    MemberEngOfId = -1;
                    txtMeIdSearch.Text = MeId.ToString();
                    cmbDesMeType.DataBind();
                    cmbDesMeType.SelectedIndex = cmbDesMeType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.Member).ToString()).Index;
                }
            }
            else if (CmbMembershipType.SelectedIndex == 1)//کاردان
            {
                PanelDocumentFileNo.ClientVisible = false;
                txtMeIdSearch.Text = MeId.ToString();
                cmbDesMeType.DataBind();
                cmbDesMeType.SelectedIndex = cmbDesMeType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.OtherPerson).ToString()).Index;
                if (WorkRequestUserControl.AgentId == Utility.GetCurrentAgentCode())
                    ObjectCapacityAssignment.SelectParameters["IsMainAgent"].DefaultValue = "1";
                else
                    ObjectCapacityAssignment.SelectParameters["IsMainAgent"].DefaultValue = "0";
                comboYear.DataBind();
                comboYear.SelectedIndex = 0;
            }

            #region تنظیم نوع نقشه Set the Type of plan by current state of ProjectWF
            if (!CheckBoxSaveWithOutCondition.Checked)
            {
                switch (_MeMajorParentIdInWorkReq)
                {
                    case (int)TSP.DataManager.MainMajors.Architecture:
                        _PlansTypeId = (int)TSP.DataManager.TSPlansType.Memari;
                        if (_CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject)
                        {
                            _PlansTypeId = -1;
                            SetLabelWarning("با توجه به رشته آماده بکاری عضو در این مرحله از گردش کار پروژه، قادر به ثبت نقشه و کارکرد طراحی این عضو نمی باشید");
                        }
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
                                SetLabelWarning("با توجه به رشته آماده بکاری شما در این مرحله از گردش کار پروژه قادر به ثبت نقشه و کارکرد طراحی خود نمی باشید");
                                return;
                            }
                        }
                        else
                        {
                            if (_CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject && _CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject)
                            {
                                _PlansTypeId = -1;
                                SetLabelWarning("با توجه به رشته آماده بکاری شما در این مرحله از گردش کار پروژه قادر به ثبت نقشه و کارکرد طراحی خود نمی باشید");
                                return;
                            }
                            _PlansTypeId = (int)TSP.DataManager.TSPlansType.Sazeh;

                        }
                        break;
                    case (int)TSP.DataManager.MainMajors.Electronic:
                        _PlansTypeId = (int)TSP.DataManager.TSPlansType.TasisatBargh;
                        if (_CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject && _CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject)
                        {
                            _PlansTypeId = -1;
                            SetLabelWarning("با توجه به رشته آماده بکاری عضو در این مرحله از گردش کار پروژه، قادر به ثبت نقشه و کارکرد طراحی این عضو نمی باشید");
                        }
                        break;
                    case (int)TSP.DataManager.MainMajors.Mechanic:
                        _PlansTypeId = (int)TSP.DataManager.TSPlansType.TasisatMechanic;
                        if (_CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject && _CurrentPrjTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject)
                        {
                            _PlansTypeId = -1;
                            SetLabelWarning("با توجه به رشته آماده بکاری عضو در این مرحله از گردش کار پروژه، قادر به ثبت نقشه و کارکرد طراحی این عضو نمی باشید");
                            return;
                        }
                        break;
                    default:
                        if (CmbMembershipType.SelectedIndex != 0)
                        {
                            switch (_CurrentPrjTaskCode)
                            {
                                case (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject:
                                    _PlansTypeId = (int)TSP.DataManager.TSPlansType.Memari;
                                    break;

                                case (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject:
                                    _PlansTypeId = (int)TSP.DataManager.TSPlansType.TasisatBargh;
                                    break;

                                case (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject:
                                    _PlansTypeId = (int)TSP.DataManager.TSPlansType.TasisatMechanic;
                                    break;

                                case (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject:
                                    _PlansTypeId = (int)TSP.DataManager.TSPlansType.Sazeh;
                                    break;
                                default:
                                    _PlansTypeId = -1;
                                    SetLabelWarning("با توجه به مرحله گردش کار پروژه امکان تنظیمات نوع نقشه وجود ندارد. ");
                                    break;
                            }
                        }
                        else
                        {
                            _PlansTypeId = -1;
                        }
                        break;
                }
            }
            else
            {
                switch (_CurrentPrjTaskCode)
                {
                    case (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject:
                        _PlansTypeId = (int)TSP.DataManager.TSPlansType.Memari;
                        break;

                    case (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject:
                        _PlansTypeId = (int)TSP.DataManager.TSPlansType.TasisatBargh;
                        break;

                    case (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject:
                        _PlansTypeId = (int)TSP.DataManager.TSPlansType.TasisatMechanic;
                        break;

                    case (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject:
                        _PlansTypeId = (int)TSP.DataManager.TSPlansType.Sazeh;
                        break;
                    default:
                        _PlansTypeId = -1;
                        SetLabelWarning("با توجه به مرحله گردش کار پروژه امکان تنظیمات نوع نقشه وجود ندارد. ");
                        break;
                }
            }
            if (_PlansTypeId == -1)
            {
                ClearAllFormInfo();
                return;
            }
            cmbPlanType.DataBind();
            cmbPlanType.SelectedIndex = cmbPlanType.Items.FindByValue(_PlansTypeId.ToString()).Index;
            if (_PlansId == -1)
            {
                TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
                PlansManager.SelectTSPlansByProjectAndRequest(_PrjId, _PlansTypeId, -1, _PrjReqId);
                if (PlansManager.Count != 0)
                {
                    _PlansId = Convert.ToInt32(PlansManager[0]["PlansId"]);

                }
            }
            FillPlanGridView();

            #endregion
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInProcess));
        }
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

        int DesignerType = Convert.ToInt32(ProjectDesignerManager[0]["MemberTypeId"]);
        int OfficeEngOId = Convert.ToInt32(ProjectDesignerManager[0]["OfficeEngOId"]);
        int MeOthIdType = Convert.ToInt32(ProjectDesignerManager[0]["MeOthIdType"]);
        cmbDesMeType.DataBind();
        cmbDesMeType.SelectedIndex = cmbDesMeType.Items.FindByValue(DesignerType.ToString()).Index;
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

        #region FillCapacityDecrement     
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(_PrjId, Convert.ToInt32(PrjDesignerId), -1, (int)TSP.DataManager.TSProjectIngridientType.Designer);
        if (ProjectCapacityDecrementManager.Count > 0)
        {
            SetCapacityDecrement(ProjectCapacityDecrementManager[0]["CapacityDecrement"].ToString());
            SetCapacityWage(ProjectCapacityDecrementManager[0]["Wage"].ToString());
            CheckBoxSaveWithOutCondition.Checked = Utility.IsDBNullOrNullValue(ProjectCapacityDecrementManager[0]["SaveWithOutCondition"]) ? false : Convert.ToBoolean(ProjectCapacityDecrementManager[0]["SaveWithOutCondition"]);
        }
        #endregion

        if (MeOthIdType == (int)TSP.DataManager.TSMemberType.Member)
        {
            #region عضوی نظام مهندسی
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
        }
        else if (MeOthIdType == (int)TSP.DataManager.TSMemberType.OtherPerson)
        {
            #region عضوی نظام کاردان ها
            PanelDocumentFileNo.ClientVisible = false;
            CmbMembershipType.SelectedIndex = CmbMembershipType.Items.FindByValue(((int)TSP.DataManager.TSMemberType.OtherPerson).ToString()).Index;
            TSP.DataManager.OtherPersonManager OthpManager = new TSP.DataManager.OtherPersonManager();
            OthpManager.FindByCode(OfficeEngOId);
            txtMeIdSearch.Text = OthpManager[0]["OtpCode"].ToString();
            #endregion
        }
        WorkRequestUserControl.NeedCheckConditions = !CheckBoxSaveWithOutCondition.Checked;
        WorkRequestUserControl.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Designer;
        WorkRequestUserControl.FillForm(txtMeIdSearch.Text, (TSP.DataManager.TSMemberType)MeOthIdType);
        if (MeOthIdType == (int)TSP.DataManager.TSMemberType.Member)
        {
            txtSearchFileNo.Text = WorkRequestUserControl.FileNo;
            txtDocProvCode.Text = WorkRequestUserControl.FileNo.Substring(0, 2);
            txtMjCode.Text = WorkRequestUserControl.FileNo.Substring(3, 3);
            txtDocSerialNo.Text = Convert.ToInt32(WorkRequestUserControl.FileNo.Substring(7)).ToString();
            FillCapacityInfo();
            CapacityUserControl.FillProjectIngridienCapacityInfo((TSP.DataManager.TSMemberType)MeOthIdType, OfficeEngOId, Convert.ToInt32(txtMeIdSearch.Text));
        }
        #region سال کاری و تعرفه
        if (WorkRequestUserControl.AgentId == Utility.GetCurrentAgentCode())
            ObjectCapacityAssignment.SelectParameters["IsMainAgent"].DefaultValue = "1";
        else
            ObjectCapacityAssignment.SelectParameters["IsMainAgent"].DefaultValue = "0";
        comboYear.DataBind();
        comboYear.SelectedIndex = 0;

        TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
        if (WorkRequestUserControl.AgentId == Utility.GetCurrentAgentCode())
            CapacityAssignmentManager.SelectCurrentYearAndStage(1);
        else
            CapacityAssignmentManager.SelectCurrentYearAndStage(0);
        if (CapacityAssignmentManager.Count > 0)
        {
            _CurrentCapacityAssignmentYear = CapacityAssignmentManager[0]["Year"].ToString();
        }
        if (!Utility.IsDBNullOrNullValue(ProjectDesignerManager[0]["Year"]))
            comboYear.SelectedIndex = comboYear.Items.FindByText(ProjectDesignerManager[0]["Year"].ToString()).Index;
        else
            comboYear.SelectedIndex = 0;
        #endregion


        #region Fill Designer_Plans
        TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
        DesignerPlansManager.FindByPrjDesignerIdAndPlansId(-1, PrjDesignerId);
        if (DesignerPlansManager.Count > 0)
        {
            chbIsMaster.Checked = Convert.ToBoolean(DesignerPlansManager[0]["IsMaster"]);
            //  _PlansId = Convert.ToInt32(DesignerPlansManager[0]["PlansId"]);
            if (!Utility.IsDBNullOrNullValue(DesignerPlansManager[0]["PlansTypeId"]))
            {
                cmbPlanType.Value = DesignerPlansManager[0]["PlansTypeId"].ToString();
                _PlansTypeId = Convert.ToInt32(DesignerPlansManager[0]["PlansTypeId"]);
            }
        }
        FillPlanGridView();
        #endregion
    }
    private void FillPlanGridView()
    {
        ObjdsPlans.SelectParameters["PrjDesignerId"].DefaultValue = _PrjDesignerId.ToString();
        ObjdsPlans.SelectParameters["ProjectId"].DefaultValue = _PrjId.ToString();
        ObjdsPlans.SelectParameters["PrjReId"].DefaultValue = _PrjReqId.ToString();
        ObjdsPlans.SelectParameters["PlansTypeId"].DefaultValue = _PlansTypeId.ToString();
        ObjdsPlans.SelectParameters["PlansId"].DefaultValue = _PlansId.ToString();
    }
    private void FillCapacityInfo()
    {
        CapacityUserControl.ProjectId = _PrjId;
        CapacityUserControl.PrjReqId = _PrjReqId;
        CapacityUserControl.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Designer;
    }
    #endregion
    #region GetDesignerInfo By Type  
    private string GetPlanTypeName()
    {
        string PlanTypeName = "";
        switch (_PlansTypeId)
        {
            case (int)TSP.DataManager.TSPlansType.Memari:
                PlanTypeName = "معماری";
                break;
            case (int)TSP.DataManager.TSPlansType.Sazeh:
                PlanTypeName = "سازه";
                break;
            case (int)TSP.DataManager.TSPlansType.Shahrsazi:
                PlanTypeName = "شهرسازی";
                break;
            case (int)TSP.DataManager.TSPlansType.TasisatBargh:
                PlanTypeName = "برق";
                break;
            case (int)TSP.DataManager.TSPlansType.TasisatMechanic:
                PlanTypeName = "مکانیک";
                break;
        }
        return PlanTypeName;
    }
    #endregion
    #region Set Enables
    private void SetControlsEnable(bool Enable)
    {
        cmbPlanType.Enabled = false;
        txtDocSerialNo.Enabled =
        txtMeIdSearch.Enabled =
        txtMjCode.Enabled =
        txtSearchFileNo.Enabled =
        txtDocProvCode.Enabled =
        RoundPanelSearch.Enabled =
        chbIsMaster.Enabled =
        CheckBoxSaveWithOutCondition.Enabled = cmbPriceArchive.Enabled = ChbIsExteraFloor.Enabled = Enable;

        TSP.DataManager.Permission perComboYear = TSP.DataManager.TechnicalServices.Designer_PlansManager.GetUserPermissionChooseWorkYearForObserverAndDesign(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        this.ViewState["comboYear"] = comboYear.Enabled = Enable && perComboYear.CanEdit;
        SetCapcityUserControlEnable(Enable);
        cmbDesMeType.Enabled = false;
    }

    #endregion
    #region InsertUpdate
    #region Insert
    private void Insert()
    {
        if (IsPageRefresh)
        {
            return;
        }
        #region Define Managers
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        TSP.DataManager.TechnicalServices.Project_DesignerManager Project_DesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        TSP.DataManager.TechnicalServices.Designer_PlansManager Designer_PlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();

        TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.OtherPersonManager OthpManager = new TSP.DataManager.OtherPersonManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(Project_DesignerManager);
        transact.Add(Designer_PlansManager);
        transact.Add(ProjectOfficeMembersManager);
        transact.Add(ProjectCapacityDecrementManager);
        transact.Add(PlansManager);
        transact.Add(WorkFlowStateManager);
        transact.Add(WorkFlowTaskManager);
        #endregion        
        try
        {
            #region Check Conditions
            if (string.IsNullOrWhiteSpace(txtMeIdSearch.Text))
            {
                SetLabelWarning("کد عضویت طراح را وارد نمایید");
                return;
            }
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




            Boolean CapacityRelatedToPreviousYears = false;
            #region Check Capacity
            if (Convert.ToInt32(cmbDesMeType.Value) != (int)TSP.DataManager.TSMemberType.OtherPerson)
            {
                if (Utility.IsDBNullOrNullValue(comboYear.SelectedItem))
                {
                    SetLabelWarning("سال کاری انتخاب نشده است");
                    return;
                }
                if (string.Compare(_CurrentCapacityAssignmentYear, comboYear.SelectedItem.Text) > 0)//اگر سال کاری کوچکتر از سال کاری جاری باشد.ظرفیت ها از امسال کسر نمی شود و فقط ثبت می شود
                {
                    CapacityRelatedToPreviousYears = true;
                }
                else
                {
                    if (!CheckBoxSaveWithOutCondition.Checked)
                    {
                        if (Convert.ToInt32(cmbDesMeType.Value) != (int)TSP.DataManager.TSMemberType.OtherPerson
                          && Convert.ToInt32(cmbDesMeType.Value) != (int)TSP.DataManager.TSMemberType.Member)
                            if (!CheckMajor(Convert.ToInt32(txtMeIdSearch.Text), Convert.ToInt32(GetCapacityDecrement())))
                                return;

                        if (!WorkRequestUserControl.CheckConditions(MeOfOthId, TSP.DataManager.TSProjectIngridientType.Designer, -1))//چون پرشدن سال کاری به این قسمت انتقال پیدا کرد این شرط در زمان انتخاب عضو انجام نمیشود و در زمان ثبت انجام میشود
                        {
                            SetLabelWarning(WorkRequestUserControl.ErrorMessage);
                            return;
                        }
                        if (!CheckCapacityBasedOnWorkRequest(MeOfOthId, int.Parse(GetCapacityDecrement())))
                            return;
                    }
                }
            }
            #endregion       
            #endregion
            transact.BeginSave();

            int IsDecreased = 1;

            if (!InsertAndConfirmPlan(PlansManager, WorkFlowTaskManager, WorkFlowStateManager))
            {
                transact.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            if (!InsertProjectDesigner(Project_DesignerManager, MeOfOthId))
            {
                transact.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            if (!InsertDesigner_Plans(Designer_PlansManager))
            {
                transact.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }

            int ProjectCapacityDecrementId;

            TSP.DataManager.TSProjectIngridientType DecreasType = TSP.DataManager.TSProjectIngridientType.Designer;
            if (Convert.ToInt32(cmbDesMeType.Value) == (int)TSP.DataManager.TSMemberType.Member && !CheckBoxSaveWithOutCondition.Checked)
            {
                DecreasType = TSP.DataManager.TSProjectIngridientType.Observer;
            }
            CapacityCalculations CapacityCalculations = new CapacityCalculations();
            int IsWorkFree = (Convert.ToBoolean(_IsCharity) || CapacityRelatedToPreviousYears == true) ? 1 : 0;
            int IsFree = CapacityRelatedToPreviousYears == true ? 1 : 0;
            ProjectCapacityDecrementId = CapacityCalculations.InsertProjectCapacityDecrement(ProjectCapacityDecrementManager, GetCapacityDecrement(), GetCapacityWage(), (Int16)DecreasType, (int)TSP.DataManager.TSProjectIngridientType.Designer, _PrjDesignerId, null
                , Utility.GetCurrentUser_UserId(), MeOfOthId
                , Convert.ToInt32(cmbDesMeType.Value) == (int)TSP.DataManager.TSMemberType.OtherPerson ? (int)TSP.DataManager.TSMemberType.OtherPerson : (int)TSP.DataManager.TSMemberType.Member
                , _PrjId
                , IsFree
                , CapacityRelatedToPreviousYears == true ? comboYear.SelectedItem.Text + "/12/28" : Utility.GetDateOfToday()
                , CheckBoxSaveWithOutCondition.Checked
                , IsWorkFree);
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

            transact.EndSave();

            _PageMode = "Edit";
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد.");
            WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.TSProjectConfirming, _PrjReqId, (int)TSP.DataManager.TableType.TSProject_Designer, "ثبت طراح جدید" + (CheckBoxSaveWithOutCondition.Checked == true ? "گزینه ''ثبت اطلاعات بدون در نظر گرفتن کلیه پیش شرط ها'' انتخاب شده است " : ""), Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.InsertNewRow);

        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private Boolean InsertAndConfirmPlan(TSP.DataManager.TechnicalServices.PlansManager PlansManager, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager
              , TSP.DataManager.WorkFlowStateManager WorkFlowStateManager)
    {
        if (_PlansId != -1) return true;

        DataRow PlanRow = PlansManager.NewRow();
        PlanRow.BeginEdit();
        PlanRow["PrjReId"] = _PrjReqId;
        PlanRow["ProjectId"] = _PrjId;
        PlanRow["Status"] = (int)TSP.DataManager.TSPlanRequestType.SavePlanInfoInsideProjectConfirming;
        PlanRow["PlansTypeId"] = cmbPlanType.SelectedItem.Value.ToString();
        PlanRow["No"] = "پ ف/" + GetPlanTypeName() + "/" + _PrjId;
        PlanRow["PlanVersion"] = 1;
        PlanRow["Date"] = Utility.GetDateOfToday();
        PlanRow["IsConfirmed"] = 1;
        PlanRow["IsDesAccepted"] = (int)TSP.DataManager.TSDesignerAcceptance.ConfirmedWithoutSaveController;
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

        String Description1 = "آغاز گردش کار اتوماتیک سیستم جهت تایید نقشه پروژه";
        String Description2 = "تایید و پایان بررسی تغییر اطلاعات نقشه پروژه";
        int TaskId = -1;

        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans);
        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SavePlanInfo);
        TaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);

        if (WorkFlowStateManager.InsertWorkFlowState(TableType, _PlansId, TaskId, Description1, 0,
            (int)TSP.DataManager.WorkFlowStateNmcIdType.System, Utility.GetCurrentUser_UserId(), 1, Utility.GetDateOfToday()) > 0)
        {
            WorkFlowStateManager.DataTable.AcceptChanges();
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.ConfirmingPlanAndEndProccess);
            TaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);

            WorkFlowStateManager.InsertWorkFlowState(TableType, _PlansId, TaskId, Description2, 0,
                (int)TSP.DataManager.WorkFlowStateNmcIdType.System, Utility.GetCurrentUser_UserId(), 1, Utility.GetDateOfToday());
        }


        return true;
    }

    private Boolean InsertProjectDesigner(TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager, int MeId)
    {
        if (_PlansTypeId == -1)
            return false;
        int DesignerTypeId = -1;
        switch (_PlansTypeId)
        {
            case (int)TSP.DataManager.TSPlansType.Sazeh:
                DesignerTypeId = (int)TSP.DataManager.TSDesignerType.Sazeh;
                break;
            case (int)TSP.DataManager.TSPlansType.TasisatBargh:
                DesignerTypeId = (int)TSP.DataManager.TSDesignerType.TasisatBargh;
                break;
            case (int)TSP.DataManager.TSPlansType.TasisatMechanic:
                DesignerTypeId = (int)TSP.DataManager.TSDesignerType.TasisatMechanic;
                break;
            case (int)TSP.DataManager.TSPlansType.Memari:
                DesignerTypeId = (int)TSP.DataManager.TSDesignerType.Memari;
                break;
            case (int)TSP.DataManager.TSPlansType.Shahrsazi:
                DesignerTypeId = (int)TSP.DataManager.TSDesignerType.Shahrsazi;
                break;
        }

        DataRow PrjDesRow = ProjectDesignerManager.NewRow();

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
        PrjDesRow["DesignerTypeId"] = DesignerTypeId;
        PrjDesRow["PriceArchiveId"] = cmbPriceArchive.Value;
        PrjDesRow["IsExteraFloor"] = ChbIsExteraFloor.Checked;
        PrjDesRow["Year"] = comboYear.Text;
        PrjDesRow["CreateDate"] = Utility.GetDateOfToday();
        PrjDesRow["InActive"] = 0;
        PrjDesRow["UserId"] = Utility.GetCurrentUser_UserId();
        PrjDesRow["ModifiedDate"] = DateTime.Now;

        ProjectDesignerManager.AddRow(PrjDesRow);
        ProjectDesignerManager.Save();

        ProjectDesignerManager.DataTable.AcceptChanges();
        _PrjDesignerId = Convert.ToInt32(ProjectDesignerManager[0]["PrjDesignerId"]);
        return true;
    }

    private Boolean InsertDesigner_Plans(TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager)
    {
        DataRow PlanDesRow = DesignerPlansManager.NewRow();
        PlanDesRow.BeginEdit();
        PlanDesRow["PrjDesignerId"] = _PrjDesignerId;
        PlanDesRow["PlansId"] = _PlansId;
        PlanDesRow["IsMaster"] = chbIsMaster.Checked;
        PlanDesRow["UserId"] = Utility.GetCurrentUser_UserId();
        PlanDesRow["ModifiedDate"] = DateTime.Now;
        PlanDesRow.EndEdit();

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
    #endregion

    #region Update
    private void Update()
    {
        if (IsPageRefresh)
        {
            return;
        }
        if (string.IsNullOrWhiteSpace(txtMeIdSearch.Text))
        {
            SetLabelWarning("کد عضویت طراح نامشخص می باشد");
            return;
        }
        int MeOfOthId = int.Parse(txtMeIdSearch.Text);
        #region Define Managers
        TSP.DataManager.TechnicalServices.Project_DesignerManager Project_DesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        TSP.DataManager.TechnicalServices.Designer_PlansManager Designer_PlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
        TSP.DataManager.TechnicalServices.ConsultantCompanyManager ConsultantCompanyManager = new TSP.DataManager.TechnicalServices.ConsultantCompanyManager();
        TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager(); //(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager)Session["ProjectOfficeMembersManager"];
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager CapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(Project_DesignerManager);
        transact.Add(CapacityDecrementManager);
        transact.Add(Designer_PlansManager);
        transact.Add(ConsultantCompanyManager);
        transact.Add(ProjectOfficeMembersManager);
        transact.Add(ProjectCapacityDecrementManager);
        #endregion

        #region Check Condition     
        int DiffrenceCapacity1 = 0;
        int CapacityDecrementOrigin = 0;
        CapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(_PrjId, _PrjDesignerId, -1, (int)TSP.DataManager.TSProjectIngridientType.Designer);
        if (CapacityDecrementManager.Count > 0)
        {
            CapacityDecrementOrigin = Convert.ToInt32(CapacityDecrementManager[0]["CapacityDecrement"]);
            DiffrenceCapacity1 = Convert.ToInt32(GetCapacityDecrement()) - Convert.ToInt32(CapacityDecrementManager[0]["CapacityDecrement"]);
            if (DiffrenceCapacity1 < 0 || string.Compare(CapacityDecrementManager[0]["DecreasedDate"].ToString(), "1398/03/25") < 0)
                DiffrenceCapacity1 = 0;
        }
        if (Utility.IsDBNullOrNullValue(comboYear.SelectedItem))
        {
            SetLabelWarning("سال کاری انتخاب نشده است");
            return;
        }
        Boolean CapacityRelatedToPreviousYears = false;
        if (string.Compare(_CurrentCapacityAssignmentYear, comboYear.SelectedItem.Text) > 0)//اگر سال کاری کوچکتر از سال کای جاری باشد.ظرفیت ها از امسال کسر نمی شود و فقط ثبت می شود
        {
            CapacityRelatedToPreviousYears = true;
        }
        else
        {
            if (DiffrenceCapacity1 != 0 && !CheckCapacityBasedOnWorkRequest(MeOfOthId, DiffrenceCapacity1))
                return;
        }
        if (!CheckMajor(Convert.ToInt32(txtMeIdSearch.Text), Convert.ToInt32(GetCapacityDecrement())))
            return;
        #endregion

        try
        {
            transact.BeginSave();
            if (!UpdateProjectDesigner(Project_DesignerManager, MeOfOthId))
            {
                transact.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            TSP.DataManager.TSProjectIngridientType DecreasType = TSP.DataManager.TSProjectIngridientType.Designer;
            if (Convert.ToInt32(cmbDesMeType.Value) == (int)TSP.DataManager.TSMemberType.Member)
            {
                DecreasType = TSP.DataManager.TSProjectIngridientType.Observer;
            }
            CapacityCalculations CapacityCalculations = new CapacityCalculations();
            int DiffrenceCapacity = CapacityCalculations.UpdateProjectCapacityDecrement(ProjectCapacityDecrementManager, GetCapacityDecrement(), GetCapacityWage(), null, (Int16)DecreasType, (int)TSP.DataManager.TSProjectIngridientType.Designer, _PrjDesignerId, CapacityRelatedToPreviousYears == true ? 1 : 0, 1, Utility.GetCurrentUser_UserId(), _PrjId, CheckBoxSaveWithOutCondition.Checked);
            if (!CapacityRelatedToPreviousYears)
            {
                TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
                transact.Add(ObserverWorkRequestManager);
                if (CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, ProjectCapacityDecrementManager, MeOfOthId, Utility.GetCurrentUser_UserId(), _PrjId, _CitId, Convert.ToBoolean(_IsCharity), DecreasType, null, false, false, false) != 0)
                {
                    transact.CancelSave();
                    SetLabelWarning("خطایی در بروزرسانی اطلاعات ظرفیت عضو انجام گرفته است");
                    return;
                }
            }
            int IsDecreased = 1;
            if (Convert.ToInt32(cmbDesMeType.Value) != (int)TSP.DataManager.TSMemberType.Member &&
           !UpdateProjectOfficeMembers(ProjectOfficeMembersManager, IsDecreased))
            {
                transact.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return;
            }
            transact.EndSave();

            _PageMode = "Edit";
            SetControlsEditMode();

            SetLabelWarning("ذخیره انجام شد.");
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.TSProjectConfirming, _PrjReqId, (int)TSP.DataManager.TableType.TSProject_Designer, "ویرایش اطلاعات طراح " + (CheckBoxSaveWithOutCondition.Checked == true ? "گزینه ''ثبت اطلاعات بدون در نظر گرفتن کلیه پیش شرط ها'' انتخاب شده است " : ""), Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.UpdateInfo);
        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private Boolean UpdateProjectDesigner(TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager, int MeId)
    {
        ProjectDesignerManager.FindByPrjDesignerId(_PrjDesignerId);

        if (ProjectDesignerManager.Count > 0)
        {
            ProjectDesignerManager[0].BeginEdit();
            ProjectDesignerManager[0]["PrjReId"] = _PrjReqId;
            ProjectDesignerManager[0]["ProjectId"] = _PrjId;
            ProjectDesignerManager[0]["MeOthId"] = MeId;
            ProjectDesignerManager[0]["MeOthIdType"] = CmbMembershipType.Value;
            ProjectDesignerManager[0]["MemberTypeId"] = cmbDesMeType.Value;
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


    private Boolean UpdateProjectOfficeMembers(TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, int IsDecreased)
    {

        ProjectOfficeMembersManager.FindByIngridientTypeAndPrjImpObsDsgnId((int)TSP.DataManager.TSProjectIngridientType.Designer, _PrjDesignerId);
        if (ProjectOfficeMembersManager.Count <= 0)
            return false;
        ProjectOfficeMembersManager[0].BeginEdit();
        ProjectOfficeMembersManager[0]["PrjImpObsDsgnId"] = _PrjDesignerId;
        ProjectOfficeMembersManager[0]["ProjectIngridientTypeId"] = (int)TSP.DataManager.TSProjectIngridientType.Designer;
        ProjectOfficeMembersManager[0]["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.Member;
        ProjectOfficeMembersManager[0]["MeOthPId"] = txtMeIdSearch.Text;
        ProjectOfficeMembersManager[0]["CapacityDecrement"] = GetCapacityDecrement();
        ProjectOfficeMembersManager[0]["Wage"] = GetCapacityWage();
        ProjectOfficeMembersManager[0]["IsFree"] = 0;
        ProjectOfficeMembersManager[0]["IsDecreased"] = IsDecreased;
        if (IsDecreased == 1)
            ProjectOfficeMembersManager[0]["DecreasedDate"] = Utility.GetDateOfToday();
        ProjectOfficeMembersManager[0]["OfficeMemberTypeId"] = cmbDesMeType.Value;
        ProjectOfficeMembersManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
        ProjectOfficeMembersManager[0]["ModifiedDate"] = DateTime.Now;
        ProjectOfficeMembersManager[0].EndEdit();

        ProjectOfficeMembersManager.Save();
        return true;
    }
    #endregion
    #endregion
    #region WF

    private void CheckWorkFlowPermissionForDesignerMode()
    {
        if (_PageMode == "New")
            CheckWorkFlowPermissionForSave("New", _CurrentPrjTaskCode);
        else
            CheckWorkFlowPermissionForEdit(_PageMode);
    }
    private void CheckWorkFlowPermissionForSave(string PageMode, int TaskCode)
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest);

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew(TaskCode, TableType, Utility.GetCurrentUser_UserId(), PageMode);
        BtnNew.Enabled = WFPer.BtnNew;
        btnNew2.Enabled = WFPer.BtnNew;
        btnSave.Enabled = WFPer.BtnSave;
        btnSave2.Enabled = WFPer.BtnSave;
        btnEdit.Enabled = WFPer.BtnEdit;
        btnEdit2.Enabled = WFPer.BtnEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }
    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        TSP.DataManager.WFPermission PerProject = CheckProjectWorkFlowPermissionForEdit(PageMode);
        BtnNew.Enabled = PerProject.BtnNew;
        btnNew2.Enabled = PerProject.BtnNew;
        btnEdit.Enabled = PerProject.BtnEdit;
        btnEdit2.Enabled = PerProject.BtnEdit;
        btnSave.Enabled = PerProject.BtnSave;
        btnSave2.Enabled = PerProject.BtnSave;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }
    private TSP.DataManager.WFPermission CheckProjectWorkFlowPermissionForEdit(string PageMode)
    {
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        //*******Editing Task Code
        int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;
        int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;
        int SaveStructureAndInstallationPlanOfProjectTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject;

        TSP.DataManager.WFPermission PerAtchitecturalPlan = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(ArchitecturalPlanTaskCode, WFCode, _PrjReqId, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission PerStructurePlan = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(StructurePlanTaskCode, WFCode, _PrjReqId, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission PerElectricalInsPlan = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(ElectricalInsPlanTaskCode, WFCode, _PrjReqId, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission PerMechanicInsPlan = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(MechanicInsPlanTaskCode, WFCode, _PrjReqId, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission PerStructureAndInstallationPlan = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(SaveStructureAndInstallationPlanOfProjectTaskCode, WFCode, _PrjReqId, Utility.GetCurrentUser_UserId(), PageMode);

        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = (PerAtchitecturalPlan.BtnEdit || PerStructurePlan.BtnEdit || PerElectricalInsPlan.BtnEdit || PerMechanicInsPlan.BtnEdit || PerStructureAndInstallationPlan.BtnEdit);
        WFPer.BtnSave = (PerAtchitecturalPlan.BtnSave || PerStructurePlan.BtnSave || PerElectricalInsPlan.BtnSave || PerMechanicInsPlan.BtnSave || PerStructureAndInstallationPlan.BtnSave);
        WFPer.BtnNew = (PerAtchitecturalPlan.BtnNew || PerStructurePlan.BtnNew || PerElectricalInsPlan.BtnNew || PerMechanicInsPlan.BtnNew || PerStructureAndInstallationPlan.BtnNew);

        return WFPer;
    }
    private int GetCurrentTaskCode(int WfCode, int TableId)
    {
        int CurrentTaskCode = -2;

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        if (dtWorkFlowState.Rows.Count > 0)
        {
            CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
        }

        return CurrentTaskCode;
    }

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

    #endregion
    #region بدست آوردن و یا تنظیم مقادیر متراژ کسر ظرفیت طراح/متراژ دستمزد طراح وارد شده توسط کاربر
    private void SetCapacityWage(string CapacityWage)
    {
        CapacityUserControl.CapacityWage = CapacityWage;
    }

    private string GetCapacityWage()
    { //_Foundation < 100 ? "100" :
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
    #region OtherMethods

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
    private bool CheckMajor(int MeId, int CapacityDecreament)
    {
        if (_CitId == 317)
            return true;
        string ErrMsg = "";
        TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager ProjectIngridientMajorsManager = new TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
        if (DocMemberFileMajorManager.Count == 0)
        {
            ErrMsg = "رشته عضو مورد نظر یافت نشد.";
            SetLabelWarning(ErrMsg);
            return false;
        }

        int MemMjId = Convert.ToInt32(DocMemberFileMajorManager[0]["MjId"]);
        string MjName = DocMemberFileMajorManager[0]["MjName"].ToString();
        int FMjParentId = Convert.ToInt32(DocMemberFileMajorManager[0]["FMjParentId"]);
        int MeDesGrdId = -2;
        int MeObsGrdId = -2;
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(MeId);
        if (MemberManager.Count != 0)
        {
            MeDesGrdId = !Utility.IsDBNullOrNullValue(MemberManager[0]["DesId"]) ? Convert.ToInt32(MemberManager[0]["DesId"]) : -2;
            MeObsGrdId = !Utility.IsDBNullOrNullValue(MemberManager[0]["ObsId"]) ? Convert.ToInt32(MemberManager[0]["ObsId"]) : -2;
        }

        DataTable dt = ProjectIngridientMajorsManager.SelectTSProjectIngridientMajorsById((int)TSP.DataManager.TSProjectIngridientType.Designer, _PlansTypeId, _GroupId, MemMjId, MeDesGrdId != -2 ? MeDesGrdId : MeObsGrdId, _StructureSkeletonId, CapacityDecreament);
        if (dt.Rows.Count > 0)
            return true;

        ErrMsg = "رشته تحصیلی طراح وارد شده " + MjName + " می باشد و با توجه به متراژ و گروه ساختمانی و پایه عضو مجاز برای طراحی این نوع نقشه نیست.";
        SetLabelWarning(ErrMsg);
        return false;
    }
    #endregion
}

