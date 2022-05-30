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
using DevExpress.Web;

public partial class Employee_TechnicalServices_Capacity_ConditionalCapacityInsert : System.Web.UI.Page
{
    string PageMode;
    string ConditionalCapacityId;
    bool IsPageRefresh = false;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("style", "display:block");
        this.DivReport.Attributes.Add("style", "display:none");
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
            RoundPanelGroupSaving.Visible = false;
            HiddenFieldCapacity["ReportURL"] = "../Report/MemberOperationReport.aspx";
            MemberCapacityUCDesign.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Designer;            
            tblMemberInfo.Visible = false;
            tblOfficeInfo.Visible = false;
            tblEngOfficeInfo.Visible = false;
            TblGroupMemberInfo.Visible = false;

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ConditionalCapacityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnNewCondition.Enabled = btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["ConditionalCapacityId"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString())) != "New"))
            {
                Response.Redirect("ConditionalCapacity.aspx");
                return;
            }

            ObjectDataSourceProjectIngridientType.FilterParameters[0].DefaultValue = "4";

            cmbGradeObs.DataBind();
            cmbGradeObs.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", -1));
            cmbGradeDesign.DataBind();
            cmbGradeDesign.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", -1));
            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }

        KeepPageState();
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = this.btnNewCondition.Enabled = (bool)this.ViewState["BtnNew"];       
    }

    #region btnClick
    protected void btnNew_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("New");
        SetNewModeKeys();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("Edit");
        SetEditModeKeys();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        switch (PageMode)
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
        //string Qs = "ProjectId=" + PkProjectId.Value + "&PrjReId=" + PkPrjReId.Value;
        Response.Redirect("ConditionalCapacity.aspx");
    }

    protected void btnNewCondition_Click(object sender, EventArgs e)
    {
        Response.Redirect("ConditionalCapacityInsert.aspx?ConditionalCapacityId=" + Utility.EncryptQS("-1") + "&PageMode=" + Utility.EncryptQS("New"));
    }
    #endregion

    #region Callbacks
    protected void CallbackCapacity_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] parameter = e.Parameter.Split(';');
        switch (parameter[0])
        {
            case "SearchMe":
                int meid = Convert.ToInt32(parameter[1]);
                GetInfo(meid);
                FillDesignCapacity(meid);
               // FillImplementCapacity(meid);
                break;
            case "SearchProj":
                int prid = Convert.ToInt32(parameter[1]);
                GetProjectName(prid);
                break;
            case "ChangeMe":
                #region ChangeMe
                ASPxTextBoxMeOfficeEngOId.Text = string.Empty;
                int MemberType = Convert.ToInt32(parameter[1]);
                switch (MemberType)
                {
                    case (int)TSP.DataManager.TSMemberType.Member:
                        tblMemberInfo.Visible = true;
                        tblOfficeInfo.Visible = false;
                        TblGroupMemberInfo.Visible = false;
                        tblEngOfficeInfo.Visible = false;

                        lblId.Visible = true;
                        ASPxTextBoxMeOfficeEngOId.Visible = true;
                        lblProjectId.Visible = true;
                        ASPxTextBoxProjectId.Visible = true;
                        lblProjectName.Visible = true;
                        txtProjectName.Visible = true;

                        ClearMember();

                        RoundPanelCapacity.Visible = true;
                        break;
                    case (int)TSP.DataManager.TSMemberType.Office:
                        tblMemberInfo.Visible = false;
                        tblOfficeInfo.Visible = true;
                        TblGroupMemberInfo.Visible = false;
                        tblEngOfficeInfo.Visible = false;

                        lblId.Visible = true;
                        ASPxTextBoxMeOfficeEngOId.Visible = true;
                        lblProjectId.Visible = true;
                        ASPxTextBoxProjectId.Visible = true;
                        lblProjectName.Visible = true;
                        txtProjectName.Visible = true;

                        ClearOffice();

                        RoundPanelCapacity.Visible = false;
                        break;
                    case (int)TSP.DataManager.TSMemberType.EngOffice:
                        tblMemberInfo.Visible = false;
                        tblOfficeInfo.Visible = false;
                        TblGroupMemberInfo.Visible = false;
                        tblEngOfficeInfo.Visible = true;

                        lblId.Visible = true;
                        ASPxTextBoxMeOfficeEngOId.Visible = true;
                        lblProjectId.Visible = true;
                        ASPxTextBoxProjectId.Visible = true;
                        lblProjectName.Visible = true;
                        txtProjectName.Visible = true;

                        ClearEngOffice();

                        RoundPanelCapacity.Visible = false;
                        break;
                    case 4:
                        TblGroupMemberInfo.Visible = true;
                        tblMemberInfo.Visible = false;
                        tblOfficeInfo.Visible = false;
                        tblEngOfficeInfo.Visible = false;

                        lblId.Visible = false;
                        ASPxTextBoxMeOfficeEngOId.Visible = false;
                        lblProjectId.Visible = false;
                        ASPxTextBoxProjectId.Visible = false;
                        lblProjectName.Visible = false;
                        txtProjectName.Visible = false;

                        ClearGroup();

                        RoundPanelCapacity.Visible = false;
                        break;
                }
                #endregion
                break;
            case "MeGroupMajorChange":
                //TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
                //MajorManager.FindByCode(SelectMjId);
                //if (MajorManager.Count != 1)
                //    return;
                // if (Convert.ToInt32(MajorManager[0]["ParrentId"]) == (int)TSP.DataManager.MainMajors.Mapping)
                int SelectMjId = Convert.ToInt32(cmbMajor.Value);
                switch (SelectMjId)
                {
                    case (int)TSP.DataManager.MainMajors.Mapping:
                        lblGradeObs.Text = "پایه نقشه برداری";
                        lblGradeDes.Visible = false;
                        cmbGradeDesign.Visible = false;
                        break;
                    case (int)TSP.DataManager.MainMajors.Traffic:
                        lblGradeObs.Text = "پایه ترافیک";
                        lblGradeDes.Visible = false;
                        cmbGradeDesign.Visible = false;
                        break;
                    case (int)TSP.DataManager.MainMajors.Urbanism:
                        lblGradeObs.Text = "پایه شهرسازی";
                        lblGradeDes.Visible = false;
                        cmbGradeDesign.Visible = false;
                        break;
                    default:
                        lblGradeObs.Text = "پایه نظارت";
                        lblGradeDes.Visible = true;
                        cmbGradeDesign.Visible = true;
                        break;
                }
                break;
        }
    }

    #endregion

    protected void ASPxComboBoxReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ASPxComboBoxReason.Value.ToString() == ((int)TSP.DataManager.TSReason.LackRecovery).ToString())
        {
            //ToDate.Visible = false;
            //Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script>document.getElementById('<%=ToDate.ClientID%>').style.display='none';</script>");
            //LabelToDate.Visible = false;
            ToDate.Enabled = false;
            RequiredFieldValidatorTodate.Enabled = false;
        }
        else
        {
            //ToDate.Visible = true;
            //Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script>document.getElementById('<%=ToDate.ClientID%>').style.display='block';</script>");
            //LabelToDate.Visible = true;
            ToDate.Enabled = true;
            RequiredFieldValidatorTodate.Enabled = true;
        }
    }
    #endregion

    #region Methods
    #region Set Key-Modes
    private void SetKeys()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            PkConditionalCapacityId.Value = Server.HtmlDecode(Request.QueryString["ConditionalCapacityId"]).ToString();

            PageMode = Utility.DecryptQS(PgMode.Value);
            ConditionalCapacityId = Utility.DecryptQS(PkConditionalCapacityId.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            SetMode();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

    }

    private void SetMode()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

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
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        CheckAccess();
        RoundPanelCapacity.Visible = false;
        //ASPxTextBoxLetterNo.Enabled = true;
        //LetterDate.Enabled = true;
        SetEnable(true);

        //ASPxTextBoxLetterNo.Text = "";
        //LetterDate.Text = "";
        // txtMailNo.Text = "";
        // txtMailDate.Text = "";
        //txtLetterTitle.Text = "";
        ASPxComboBoxMemberType.SelectedIndex = -1;
        ASPxTextBoxMeOfficeEngOId.Text = "";
        ASPxComboBoxReason.SelectedIndex = -1;
        ASPxComboBoxProjectIngridientType.SelectedIndex = -1;
        ASPxTextBoxCapacity.Text = "";
        FromDate.Text = "";
        ToDate.Text = "";
        ASPxTextBoxProjectId.Text = "";
        ASPxMemoDescription.Text = "";
        txtDesign.Text = "";
        txtEngOffManager.Text = "";
        txtEngOffName.Text = "";
        txtEngOffType.Text = "";
        txtFileDate.Text = "";
        txtFileNo.Text = "";
        txtImp.Text = "";
        txtMapping.Text = "";
        txtObs.Text = "";
        txtProjectName.Text = "";
        txtMemberName.Text = "";
        txtMeNo.Text = "";
        txtUrbanism.Text = "";
        txtTraffic.Text = "";
        ClearOffice();
        ClearEngOffice();
        ClearGroup();
        ClearMember();
        ASPxCheckBoxIsConfirmed.Checked = false;
        //MemberCapacityUCDesign.ClearControlsIngridienCapacityInfo();
        //MemberCapacityUCImplement.ClearControlsIngridienCapacityInfo();
        LabelReasonErr.Visible = false;
        tblMemberInfo.Visible = false;
        tblOfficeInfo.Visible = false;
        tblEngOfficeInfo.Visible = false;
        TblGroupMemberInfo.Visible = false;
        ObjectDataSourceReason.FilterParameters[0].DefaultValue = ((int)TSP.DataManager.TSReason.Repay).ToString();

        RoundPanelMain.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;

        CheckAccess();

        //ASPxTextBoxLetterNo.Enabled = true;
        //LetterDate.Enabled = true;
        SetEnable(true);
        //txtMailNo.ReadOnly = false;
        ASPxComboBoxMemberType.ClientEnabled = false;
        ASPxTextBoxMeOfficeEngOId.Enabled = false;
        bool InActive = SetValues();

        LabelReasonErr.Visible = false;
        ObjectDataSourceReason.FilterParameters[0].DefaultValue = ((int)TSP.DataManager.TSReason.Repay).ToString();

        RoundPanelMain.HeaderText = "ویرایش";

        //if (ASPxCheckBoxIsConfirmed.Checked)
        //{
        //    SetLabelWarning("کاهش/افزایش ظرفیت تایید شده است و قابل ویرایش نمی باشد");

        //    PgMode.Value = Utility.EncryptQS("View");
        //    SetViewModeKeys();
        //}

        if (InActive)
        {
            SetLabelWarning("کاهش/افزایش ظرفیت غیرفعال شده است و قابل ویرایش نمی باشد");

            PgMode.Value = Utility.EncryptQS("View");
            SetViewModeKeys();
        }
    }

    private void SetViewModeKeys()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;

        CheckAccess();

        SetEnable(false);

        SetValues();

        LabelReasonErr.Visible = false;
        ObjectDataSourceReason.FilterParameters[0].DefaultValue = "0";

        RoundPanelMain.HeaderText = "مشاهده";
    }
    #endregion

    private void SetEnable(Boolean Enable)
    {
        //txtMailNo.ReadOnly = !Enable;
        ASPxComboBoxMemberType.ClientEnabled = Enable;
        ASPxTextBoxMeOfficeEngOId.Enabled = Enable;
        ASPxComboBoxReason.Enabled = Enable;
        ASPxComboBoxProjectIngridientType.Enabled = Enable;
        ASPxTextBoxCapacity.Enabled = Enable;
        FromDate.Enabled = Enable;
        ToDate.Enabled = Enable;
        ASPxTextBoxProjectId.Enabled = Enable;
        ASPxMemoDescription.Enabled = Enable;
        ASPxCheckBoxIsConfirmed.Enabled = Enable;
        CallbackCapacity.Enabled = Enable;
    }

    private bool SetValues()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        ConditionalCapacityId = Utility.DecryptQS(PkConditionalCapacityId.Value);

        if ((string.IsNullOrEmpty(ConditionalCapacityId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return false;
        }

        TSP.DataManager.TechnicalServices.ConditionalCapacityManager Manager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();
        Manager.FindByConditionalCapacityId(Convert.ToInt32(ConditionalCapacityId));
        if (Manager.Count == 1)
        {
            //ASPxTextBoxLetterNo.Text = Manager[0]["LetterNo"].ToString();
            //LetterDate.Text = Manager[0]["LetterDate"].ToString();
            // txtMailNo.Text = Manager[0]["LetterNo"].ToString();
            //TSP.DataManager.Automation.LettersManager LetterManager = new TSP.DataManager.Automation.LettersManager();
            //LetterManager.FindByLetterNumber(txtMailNo.Text.Trim());
            //if (LetterManager.Count == 1)
            //{
            //    txtMailDate.Text = LetterManager[0]["LetterDate"].ToString();
            //    txtLetterTitle.Text = LetterManager[0]["Title"].ToString();
            //}

            // txtMailDate.Text = Manager[0]["LetterDate"].ToString();
            ASPxComboBoxMemberType.DataBind();
            ASPxComboBoxMemberType.Value = Manager[0]["MemberTypeId"].ToString();
            ASPxTextBoxMeOfficeEngOId.Text = Manager[0]["MeOfficeEngOId"].ToString();
            GetInfo(Convert.ToInt32(ASPxTextBoxMeOfficeEngOId.Text));
            ASPxComboBoxReason.DataBind();
            ASPxComboBoxReason.Value = Manager[0]["ReasonId"].ToString();
            ASPxComboBoxProjectIngridientType.DataBind();
            ASPxComboBoxProjectIngridientType.Value = Manager[0]["ProjectIngridientTypeId"].ToString();
            ASPxTextBoxCapacity.Text = Manager[0]["Capacity"].ToString();
            FromDate.Text = Manager[0]["FromDate"].ToString();
            ToDate.Text = Manager[0]["ToDate"].ToString();
            if (!Utility.IsDBNullOrNullValue(Manager[0]["ProjectId"]))
            {
                ASPxTextBoxProjectId.Text = Manager[0]["ProjectId"].ToString();
                GetProjectName(Convert.ToInt32(ASPxTextBoxProjectId.Text));
            }
            ASPxMemoDescription.Text = Manager[0]["Description"].ToString();
            ASPxCheckBoxIsConfirmed.Checked = Convert.ToBoolean(Manager[0]["IsConfirmed"]);
            if (Convert.ToInt32(Manager[0]["MemberTypeId"]) == (int)TSP.DataManager.TSMemberType.Member)
            {
                int MeId = Convert.ToInt32(Manager[0]["MeOfficeEngOId"]);
                FillDesignCapacity(MeId);
            }
            return Convert.ToBoolean(Manager[0]["InActive"]);
        }
        else
        {
            SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است");

            return false;
        }
    }

    private void GetProjectName(int ProjectId)
    {
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        ProjectManager.FindByProjectId(ProjectId);
        if (ProjectManager.Count == 1)
            txtProjectName.Text = ProjectManager[0]["ProjectName"].ToString();
        else
        {
            ShowCallBackMessage("کد پروژه نامعتبر می باشد");
            txtProjectName.Text = string.Empty;
        }
    }

    private void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ConditionalCapacityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (btnNew.Enabled == true)
        {
            btnNewCondition.Enabled = btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
        }

        if (btnEdit.Enabled == true)
        {
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
        }

        if (Utility.DecryptQS(PgMode.Value) == "New" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;
        }
        if (Utility.DecryptQS(PgMode.Value) == "Edit" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    /*******************************************************************************************************************************************/
    private bool CheckReason()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return false;
        }

        switch (PageMode)
        {
            case "New":
            case "Edit":
                if (ASPxComboBoxReason.Value.ToString() == ((int)TSP.DataManager.TSReason.Repay).ToString())
                {
                    LabelReasonErr.Visible = true;
                    return false;
                }
                break;
        }

        LabelReasonErr.Visible = false;
        return true;
    }

    private void GetInfo(int MeId)
    {
        int MemberTypeId = Convert.ToInt32(ASPxComboBoxMemberType.Value);
        switch (MemberTypeId)
        {
            case (int)TSP.DataManager.TSMemberType.Member:
                TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                MemberManager.FindByCode(MeId);
                if (MemberManager.Count > 0)
                {
                    tblMemberInfo.Visible = true;
                    tblOfficeInfo.Visible = false;
                    tblEngOfficeInfo.Visible = false;
                    TblGroupMemberInfo.Visible = false;

                    ASPxTextBoxMeOfficeEngOId.Text = MemberManager[0]["MeId"].ToString();
                    txtMemberName.Text = MemberManager[0]["MeName"].ToString();
                    txtMeNo.Text = MemberManager[0]["MeNo"].ToString();
                    txtFileNo.Text = MemberManager[0]["FileNo"].ToString();
                    txtFileDate.Text = MemberManager[0]["FileDate"].ToString();
                    txtObs.Text = MemberManager[0]["ObsGrdName"].ToString();
                    txtImp.Text = MemberManager[0]["ImpGrdName"].ToString();
                    txtDesign.Text = MemberManager[0]["DesGrdName"].ToString();
                    txtUrbanism.Text = MemberManager[0]["UrbanismGrdName"].ToString();
                    txtTraffic.Text = MemberManager[0]["TrafficGrdName"].ToString();
                    txtMapping.Text = MemberManager[0]["MappingGrdName"].ToString();
                }
                else ShowCallBackMessage("کد عضویت نا معتبر است");
                break;

            case (int)TSP.DataManager.TSMemberType.Office:
                TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
                OfficeManager.FindByCode(MeId);
                if (OfficeManager.Count > 0)
                {
                    tblMemberInfo.Visible = false;
                    tblOfficeInfo.Visible = true;
                    tblEngOfficeInfo.Visible = false;
                    TblGroupMemberInfo.Visible = false;

                    ASPxTextBoxMeOfficeEngOId.Text = OfficeManager[0]["OfId"].ToString();
                    txtOfficeName.Text = OfficeManager[0]["OfName"].ToString();
                    txtOfficeSubject.Text = OfficeManager[0]["Subject"].ToString();
                    txtOfficeManager.Text = OfficeManager[0]["MName"].ToString();
                    txtMembersCount.Text = OfficeManager[0]["MemberCount"].ToString();
                }
                else ShowCallBackMessage("کد شرکت نا معتبر است");
                break;

            case (int)TSP.DataManager.TSMemberType.EngOffice:
                TSP.DataManager.EngOfficeManager EngOfficeManager = new TSP.DataManager.EngOfficeManager();
                EngOfficeManager.FindByCode(MeId);
                if (EngOfficeManager.Count > 0)
                {
                    tblMemberInfo.Visible = false;
                    tblOfficeInfo.Visible = false;
                    tblEngOfficeInfo.Visible = true;
                    TblGroupMemberInfo.Visible = false;

                    ASPxTextBoxMeOfficeEngOId.Text = EngOfficeManager[0]["EngOfId"].ToString();
                    txtEngOffName.Text = EngOfficeManager[0]["EngOffName"].ToString();
                    txtEngOffType.Text = EngOfficeManager[0]["OfTName"].ToString();
                    txtEngOffManager.Text = EngOfficeManager[0]["MeName"].ToString();
                }
                else ShowCallBackMessage("کد دفتر نا معتبر است");
                break;
        }
    }

    private bool CheckGrade()
    {
        int MemberTypeId = Convert.ToInt32(ASPxComboBoxMemberType.Value);
        int Grade = 0;
        switch (MemberTypeId)
        {
            case (int)TSP.DataManager.TSMemberType.Member:
                return CheckMeGradeType(Convert.ToInt32(ASPxTextBoxMeOfficeEngOId.Text), Convert.ToInt32(ASPxComboBoxProjectIngridientType.Value));

            case (int)TSP.DataManager.TSMemberType.Office:
                return CheckOfGradeType(Convert.ToInt32(ASPxTextBoxMeOfficeEngOId.Text), Convert.ToInt32(ASPxComboBoxProjectIngridientType.Value));

            case (int)TSP.DataManager.TSMemberType.EngOffice:
                return CheckEngOGradeType(Convert.ToInt32(ASPxTextBoxMeOfficeEngOId.Text), Convert.ToInt32(ASPxComboBoxProjectIngridientType.Value));
        }
        return false;
    }

    private bool CheckMeGradeType(int MeId, int ProjectIngridientTypeId)
    {
        int Grade = 0;
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        int ResponsibilityType = 0;

        switch (ProjectIngridientTypeId)
        {
            case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                ResponsibilityType = (int)TSP.DataManager.DocumentResponsibilityType.Design;
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                ResponsibilityType = (int)TSP.DataManager.DocumentResponsibilityType.Implement;
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                ResponsibilityType = (int)TSP.DataManager.DocumentResponsibilityType.Observation;
                break;
        }

        ArrayList GradeArr = DocMemberFileDetailManager.FindActiveResByResponsibility(MeId, ResponsibilityType);
        if (GradeArr.Count != 0)
            Grade = Convert.ToInt32(GradeArr[0]);
        else
            Grade = 0;

        if (Grade == 0)
            return false;
        else
            return true;
    }

    private bool CheckOfGradeType(int OfId, int ProjectIngridientTypeId)
    {
        TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
        OfficeManager.FindByCode(OfId);
        if (OfficeManager.Count > 0)
        {
            int MFType = -1;
            if (!Utility.IsDBNullOrNullValue(OfficeManager[0]["MFType"]))
                MFType = Convert.ToInt32(OfficeManager[0]["MFType"]);

            switch (MFType)
            {
                case (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesign:
                    if (ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Designer || ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Observer)
                        return true;
                    else
                        return false;

                case (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement:
                    if (ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Implementer)
                        return true;
                    else
                        return false;

                case (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesignAndImplement:
                    return true;
            }
        }

        return false;
    }

    private bool CheckEngOGradeType(int EngOfId, int ProjectIngridientTypeId)
    {
        TSP.DataManager.EngOfficeManager EngOfficeManager = new TSP.DataManager.EngOfficeManager();
        EngOfficeManager.FindByCode(EngOfId);
        if (EngOfficeManager.Count > 0)
        {
            int MFType = -1;
            if (!Utility.IsDBNullOrNullValue(EngOfficeManager[0]["EOfTId"]))
                MFType = Convert.ToInt32(EngOfficeManager[0]["EOfTId"]);

            switch (MFType)
            {
                case (int)TSP.DataManager.EngOfficeType.Design:
                    if (ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Designer)
                        return true;
                    else
                        return false;

                case (int)TSP.DataManager.EngOfficeType.Implimentation:
                    if (ProjectIngridientTypeId == (int)TSP.DataManager.TSProjectIngridientType.Implementer)
                        return true;
                    else
                        return false;
            }
        }

        return false;
    }

    #region Insert-Update
    /************************************************************ Insert *******************************************************************/
    private void Insert()
    {
        if (IsPageRefresh) return;

        if (!CheckReason())
            return;

        if (Convert.ToInt32(ASPxComboBoxMemberType.Value) != 4)
        {
            if (string.IsNullOrEmpty(ASPxTextBoxMeOfficeEngOId.Text))
            {
                SetLabelWarning("کد عضویت را وارد نمایید");
                return;
            }
            if (!CheckGrade())
            {
                SetLabelWarning("عضو مورد نظر صلاحیت " + ASPxComboBoxProjectIngridientType.Text + " ندارد");
                return;
            }
        }

        TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();
        //  TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();

        //TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();        
        //TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        //TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        //TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        //TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        // TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        //  transact.Add(ConditionalCapacityManager);
        // transact.Add(LetterRelatedTablesManager);

        //transact.Add(ProjectRequestManager);
        //transact.Add(WorkFlowStateManager);
        //transact.Add(NezamChartManager);
        //transact.Add(LoginManager);
        //transact.Add(WorkFlowTaskManager);

        try
        {
            //  transact.BeginSave();

            //int LetterId = GetLetterId();
            //if (LetterId == -1)
            //{
            //    SetLabelWarning("شماره نامه مورد نظر موجود نمی باشد.");
            //    return;
            //}


            if (Convert.ToInt32(ASPxComboBoxMemberType.Value) != 4)
            {
                InsertConditionalCapacity(ConditionalCapacityManager);
                //  InsertLetterRelatedTables(LetterRelatedTablesManager, LetterId);
            }
            else
            {
                if (!InsertGroupConditionalCapacity(ConditionalCapacityManager))
                    return;
            }


            //InsertProjectRequest(ProjectRequestManager);
            //InsertWorkFlowState(WorkFlowStateManager, NezamChartManager, LoginManager, WorkFlowTaskManager);

            //transact.EndSave();
            if (Convert.ToInt32(ASPxComboBoxMemberType.Value) != 4)
            {
                PgMode.Value = Utility.EncryptQS("Edit");
                SetEditModeKeys();
                SetLabelWarning("ذخیره انجام شد");
            }
            else
            {
                SetControlAfterGroupSaving();
            }

        }
        catch (Exception err)
        {
            // transact.CancelSave();
            SetError(err, 'I');
        }
    }

    private void InsertConditionalCapacity(TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager)
    {
        DataRow rowConditionalCapacity = ConditionalCapacityManager.NewRow();

        //rowConditionalCapacity["LetterNo"] = txtMailNo.Text; //ASPxTextBoxLetterNo.Text;
        // rowConditionalCapacity["LetterDate"] = txtMailDate.Text; //LetterDate.Text;
        rowConditionalCapacity["ReasonId"] = ASPxComboBoxReason.Value;
        rowConditionalCapacity["Capacity"] = ASPxTextBoxCapacity.Text;
        rowConditionalCapacity["FromDate"] = FromDate.Text;
        if (ToDate.Text != "")
            rowConditionalCapacity["ToDate"] = ToDate.Text;
        else
            rowConditionalCapacity["ToDate"] = "2";
        rowConditionalCapacity["Description"] = ASPxMemoDescription.Text;
        rowConditionalCapacity["MemberTypeId"] = ASPxComboBoxMemberType.Value;
        rowConditionalCapacity["MeOfficeEngOId"] = ASPxTextBoxMeOfficeEngOId.Text;
        rowConditionalCapacity["ProjectIngridientTypeId"] = ASPxComboBoxProjectIngridientType.Value;
        if (ASPxTextBoxProjectId.Text != "")
            rowConditionalCapacity["ProjectId"] = ASPxTextBoxProjectId.Text;
        rowConditionalCapacity["InActive"] = 0;

        // rowConditionalCapacity["IsConfirmed"] = 0;//-----به دلیل نامشخص بودن محل تایید، فعلا هنگام وارد کردن مقدار یک می گیرد
        rowConditionalCapacity["IsConfirmed"] = 1;
        rowConditionalCapacity["UserId"] = Utility.GetCurrentUser_UserId();
        rowConditionalCapacity["ModifiedDate"] = DateTime.Now;

        ConditionalCapacityManager.AddRow(rowConditionalCapacity);
        ConditionalCapacityManager.Save();

        ConditionalCapacityManager.DataTable.AcceptChanges();
        ConditionalCapacityId = ConditionalCapacityManager[0]["ConditionalCapacityId"].ToString();
        PkConditionalCapacityId.Value = Utility.EncryptQS(ConditionalCapacityId.ToString());

    }

    private bool InsertGroupConditionalCapacity(TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager)
    {
        string Description = "";
        string ToDatee = "";
        //   string GrdId = cmbGrade.Value.ToString();
        //  string ResId = "";// cmbResponsibility.Value.ToString();
        if (cmbGradeObs.Value != null && Convert.ToInt32(cmbGradeObs.Value) == -1 && cmbGradeDesign.Value != null && Convert.ToInt32(cmbGradeDesign.Value) == -1)
        {
            SetLabelWarning("پایه رشته مورد نظر جهت افزایش/کاهش ظرفیت انتخاب نشده است");
            return false;
        }
        int GrdObsId, GrdImpId, GrdDesId, GrdUrbanismId, GrdTrafficId, GrdMappingId = -1;
        int SelectMjId = Convert.ToInt32(cmbMajor.Value);
        string ObsDes = "";
        string DesignDes = "";
        switch (SelectMjId)
        {
            case (int)TSP.DataManager.MainMajors.Mapping:
                lblGradeObs.Text = "پایه نقشه برداری";
                lblGradeDes.Visible = false;
                cmbGradeDesign.Visible = false;
                GrdObsId = -1; GrdImpId = -1; GrdDesId = -1; GrdUrbanismId = -1; GrdTrafficId = -1; GrdMappingId = Convert.ToInt32(cmbGradeObs.Value);
                ObsDes = cmbGradeObs.SelectedItem.Text + " صلاحیت نقشه برداری ";
                DesignDes = "";
                break;
            case (int)TSP.DataManager.MainMajors.Traffic:
                lblGradeObs.Text = "پایه ترافیک";
                lblGradeDes.Visible = false;
                cmbGradeDesign.Visible = false;
                GrdObsId = -1; GrdImpId = -1; GrdDesId = -1; GrdUrbanismId = -1; GrdTrafficId = Convert.ToInt32(cmbGradeObs.Value); GrdMappingId = -1;
                ObsDes = cmbGradeObs.SelectedItem.Text + " صلاحیت ترافیک ";
                DesignDes = "";
                break;
            case (int)TSP.DataManager.MainMajors.Urbanism:
                lblGradeObs.Text = "پایه شهرسازی";
                lblGradeDes.Visible = false;
                cmbGradeDesign.Visible = false;
                GrdObsId = -1; GrdImpId = -1; GrdDesId = -1; GrdUrbanismId = Convert.ToInt32(cmbGradeObs.Value); GrdTrafficId = -1; GrdMappingId = -1;
                ObsDes = cmbGradeObs.SelectedItem.Text + " صلاحیت شهرسازی ";
                DesignDes = "";
                break;
            default:
                lblGradeObs.Text = "پایه نظارت";
                lblGradeDes.Visible = true;
                cmbGradeDesign.Visible = true;
                GrdObsId = Convert.ToInt32(cmbGradeObs.Value); GrdImpId = -1; GrdDesId = Convert.ToInt32(cmbGradeDesign.Value); GrdUrbanismId = -1; GrdTrafficId = -1; GrdMappingId = -1;
                if (cmbGradeObs.Value != null && Convert.ToInt32(cmbGradeObs.Value) != -1)
                    ObsDes = cmbGradeObs.SelectedItem.Text + " صلاحیت نظارت ";
                if (cmbGradeDesign.Value != null && Convert.ToInt32(cmbGradeDesign.Value) != -1)
                    DesignDes = " و " + cmbGradeDesign.SelectedItem.Text + " صلاحیت طراحی ";
                break;
        }

        string MjId = cmbMajor.Value.ToString();
        string ReasonId = ASPxComboBoxReason.Value.ToString();
        string FromDatee = FromDate.Text.Trim();
        if (ToDate.Text != "")
            ToDatee = ToDate.Text.Trim();
        else
            ToDatee = "2";

        DataTable dt = ConditionalCapacityManager.SelectGroupConditionalCapacity(GrdImpId, GrdObsId, GrdDesId, GrdUrbanismId, GrdTrafficId, GrdMappingId, MjId, ReasonId, FromDatee, ToDatee);
        if (dt.Rows.Count > 0)
        {
            SetLabelWarning("پیش از این جهت اعضایی با این مشخصات افزایش/کاهش ظرفیت وارد شده است ");
            return false;
        }

        Description = ASPxComboBoxReason.SelectedItem.Text + " جهت اعضایی با مشخصات " + ObsDes +
        DesignDes + " رشته " + cmbMajor.SelectedItem.Text;

        int MemberTypeId = (int)TSP.DataManager.TSMemberType.Member;

        ConditionalCapacityManager.InsertGroupConditionalCapacity(ReasonId, ASPxTextBoxCapacity.Text.Trim(),
                    FromDate.Text, ToDatee, Description, MemberTypeId, Utility.GetCurrentUser_UserId(), MjId
                    , ASPxComboBoxProjectIngridientType.Value.ToString(), GrdObsId, GrdImpId, GrdDesId, GrdUrbanismId, GrdTrafficId, GrdMappingId);
        DataTable dt1 = ConditionalCapacityManager.SelectGroupConditionalCapacity(GrdImpId, GrdObsId, GrdDesId, GrdUrbanismId, GrdTrafficId, GrdMappingId, MjId, ReasonId, FromDatee, ToDatee);

        if (dt1.Rows.Count == 0)
            lblMeGroupSaveMessage.Text = Description + " تعداد نفرات یافت شده با این شرایط :" + dt1.Rows.Count.ToString();
        else
            lblMeGroupSaveMessage.Text = Description + " با موفقیت انجام شد.تعداد نفرات :" + dt1.Rows.Count.ToString();
        return true;

    }

    //private void InsertLetterRelatedTables(TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager, int LetterId)
    //{
    //    ConditionalCapacityId = Utility.DecryptQS(PkConditionalCapacityId.Value);

    //    DataRow dr = LetterRelatedTablesManager.NewRow();

    //    dr.BeginEdit();
    //    dr["LetterId"] = LetterId;
    //    dr["TableId"] = ConditionalCapacityId;
    //    dr["TableType"] = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSConditionalCapacity);
    //    dr["UserId"] = Utility.GetCurrentUser_UserId();
    //    dr["ModifiedDate"] = DateTime.Now;
    //    dr.EndEdit();

    //    LetterRelatedTablesManager.AddRow(dr);
    //    LetterRelatedTablesManager.Save();
    //}

    /************************************************************* Update *********************************************************/
    private void Update()
    {
        if (IsPageRefresh)
        {
            return;
        }

        if (!CheckReason())
            return;

        TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager = new TSP.DataManager.TechnicalServices.ConditionalCapacityManager();
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();

        //TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(ConditionalCapacityManager);
        transact.Add(LetterRelatedTablesManager);
        //transact.Add(ProjectRequestManager);       

        try
        {
            transact.BeginSave();

            UpdateConditionalCapacity(ConditionalCapacityManager);

            transact.EndSave();

            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد");

        }
        catch (Exception err)
        {
            transact.CancelSave();
            SetError(err, 'U');
        }
    }

    private void UpdateConditionalCapacity(TSP.DataManager.TechnicalServices.ConditionalCapacityManager ConditionalCapacityManager)
    {
        ConditionalCapacityId = Utility.DecryptQS(PkConditionalCapacityId.Value);

        if (string.IsNullOrEmpty(ConditionalCapacityId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        ConditionalCapacityManager.FindByConditionalCapacityId(Convert.ToInt32(ConditionalCapacityId));

        if (ConditionalCapacityManager.Count >= 1)
        {
            ConditionalCapacityManager[0].BeginEdit();
            //ConditionalCapacityManager[0]["LetterNo"] = txtMailNo.Text; //ASPxTextBoxLetterNo.Text;
            // ConditionalCapacityManager[0]["LetterDate"] = txtMailDate.Text; //LetterDate.Text;
            ConditionalCapacityManager[0]["ReasonId"] = ASPxComboBoxReason.Value;
            ConditionalCapacityManager[0]["Capacity"] = ASPxTextBoxCapacity.Text;
            ConditionalCapacityManager[0]["FromDate"] = FromDate.Text;
            if (ToDate.Text != "")
                ConditionalCapacityManager[0]["ToDate"] = ToDate.Text;
            else
                ConditionalCapacityManager[0]["ToDate"] = "2";
            ConditionalCapacityManager[0]["Description"] = ASPxMemoDescription.Text;
            ConditionalCapacityManager[0]["MemberTypeId"] = ASPxComboBoxMemberType.Value;
            ConditionalCapacityManager[0]["MeOfficeEngOId"] = ASPxTextBoxMeOfficeEngOId.Text;
            ConditionalCapacityManager[0]["ProjectIngridientTypeId"] = ASPxComboBoxProjectIngridientType.Value;
            if (ASPxTextBoxProjectId.Text != "")
                ConditionalCapacityManager[0]["ProjectId"] = ASPxTextBoxProjectId.Text;
            ConditionalCapacityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ConditionalCapacityManager[0]["ModifiedDate"] = DateTime.Now;
            ConditionalCapacityManager[0].EndEdit();

            ConditionalCapacityManager.Save();

            ConditionalCapacityManager.DataTable.AcceptChanges();
            ConditionalCapacityId = ConditionalCapacityManager[0]["ConditionalCapacityId"].ToString();
            PkConditionalCapacityId.Value = Utility.EncryptQS(ConditionalCapacityId.ToString());
        }
        else
        {
            SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است");
        }
    }

    #endregion
    /*****************************************************************************************************************************/

    private void KeepPageState()
    {
        int MemberType = Convert.ToInt32(ASPxComboBoxMemberType.Value);
        switch (MemberType)
        {
            case (int)TSP.DataManager.TSMemberType.Member:
                tblMemberInfo.Visible = true;
                tblOfficeInfo.Visible = false;
                tblEngOfficeInfo.Visible = false;
                TblGroupMemberInfo.Visible = false;

                lblId.Visible = true;
                ASPxTextBoxMeOfficeEngOId.Visible = true;
                lblProjectId.Visible = true;
                ASPxTextBoxProjectId.Visible = true;
                lblProjectName.Visible = true;
                txtProjectName.Visible = true;

                RoundPanelCapacity.Visible = true;
                break;
            case (int)TSP.DataManager.TSMemberType.Office:
                tblMemberInfo.Visible = false;
                tblOfficeInfo.Visible = true;
                tblEngOfficeInfo.Visible = false;
                TblGroupMemberInfo.Visible = false;

                lblId.Visible = true;
                ASPxTextBoxMeOfficeEngOId.Visible = true;
                lblProjectId.Visible = true;
                ASPxTextBoxProjectId.Visible = true;
                lblProjectName.Visible = true;
                txtProjectName.Visible = true;

                RoundPanelCapacity.Visible = false;
                break;
            case (int)TSP.DataManager.TSMemberType.EngOffice:
                tblMemberInfo.Visible = false;
                tblOfficeInfo.Visible = false;
                tblEngOfficeInfo.Visible = true;
                TblGroupMemberInfo.Visible = false;

                lblId.Visible = true;
                ASPxTextBoxMeOfficeEngOId.Visible = true;
                lblProjectId.Visible = true;
                ASPxTextBoxProjectId.Visible = true;
                lblProjectName.Visible = true;
                txtProjectName.Visible = true;

                RoundPanelCapacity.Visible = false;
                break;
            case 4:
                tblMemberInfo.Visible = false;
                tblOfficeInfo.Visible = false;
                tblEngOfficeInfo.Visible = false;
                TblGroupMemberInfo.Visible = true;

                lblId.Visible = false;
                ASPxTextBoxMeOfficeEngOId.Visible = false;
                lblProjectId.Visible = false;
                ASPxTextBoxProjectId.Visible = false;
                lblProjectName.Visible = false;
                txtProjectName.Visible = false;

                RoundPanelCapacity.Visible = false;
                break;
        }
    }

    #region Clear FormControl
    private void ClearMember()
    {
        txtMemberName.Text = string.Empty;
        txtMeNo.Text = string.Empty;
        txtFileNo.Text = string.Empty;
        txtFileDate.Text = string.Empty;
        txtObs.Text = string.Empty;
        txtImp.Text = string.Empty;
        txtDesign.Text = string.Empty;
        txtUrbanism.Text = string.Empty;
        txtTraffic.Text = string.Empty;
        txtMapping.Text = string.Empty;
    }

    private void ClearOffice()
    {
        txtOfficeName.Text = string.Empty;
        txtOfficeSubject.Text = string.Empty;
        txtOfficeManager.Text = string.Empty;
        txtMembersCount.Text = string.Empty;
    }

    private void ClearEngOffice()
    {
        txtEngOffName.Text = string.Empty;
        txtEngOffType.Text = string.Empty;
        txtEngOffManager.Text = string.Empty;
    }

    private void ClearGroup()
    {
        cmbGradeDesign.SelectedIndex = 0;
        cmbGradeObs.SelectedIndex = 0;
        cmbMajor.SelectedIndex = -1;
        lblGradeObs.Text = "پایه نظارت";
        lblGradeDes.Visible = true;
        cmbGradeDesign.Visible = true;
    }
    #endregion

    /*****************************************************************************************************************************/
    private void SetError(Exception err, char Flag)
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
                if (Flag == 'D')
                {
                    SetLabelWarning("به علت وجود اطلاعات وابسته امکان حذف نمی باشد");
                }
                else
                {
                    SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
                }
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

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Attributes.Add("style", "display:visible");
        this.LabelWarning.Text = Warning;
    }

    void ShowCallBackMessage(string Msg)
    {
        CallbackCapacity.JSProperties["cpMsg"] = Msg;
        CallbackCapacity.JSProperties["cpError"] = 1;
    }

    /*****************************************************************************************************************************/
    private void FillDesignCapacity(int MeId)
    {
        MemberCapacityUCDesign.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Designer;
        MemberCapacityUCDesign.FillMemberCapacityInfo(MeId, 1);
    }

    //private void FillImplementCapacity(int MeId)
    //{
    //    MemberCapacityUCImplement.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Implementer;
    //    MemberCapacityUCImplement.FillMemberCapacityInfo(MeId, 0);
    //}

    private void SetControlAfterGroupSaving()
    {
        RoundPanelFooter.Visible = false;
        RoundPanelHeader.Visible = false;
        RoundPanelMain.Visible = false;
        RoundPanelGroupSaving.Visible = true;
    }
    #endregion
}