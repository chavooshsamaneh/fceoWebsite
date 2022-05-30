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

public partial class Employee_TechnicalServices_Project_PlanDesigner : System.Web.UI.Page
{
    #region Properties
    private string PlansId
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldDesPlans["PlnId"].ToString());
        }
        set
        {
            HiddenFieldDesPlans["PlnId"] = Utility.EncryptQS(value.ToString());
        }
    }

    private string PrjReId
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldDesPlans["PrjReId"].ToString());
        }
        set
        {
            HiddenFieldDesPlans["PrjReId"] = Utility.EncryptQS(value.ToString());
        }
    }

    private string ProjectId
    {
        get
        {
            return Utility.DecryptQS(HiddenFieldDesPlans["PrjId"].ToString());
        }
        set
        {
            HiddenFieldDesPlans["PrjId"] = Utility.EncryptQS(value.ToString());
        }
    }
    private int _CitId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldDesPlans["CitId"]);
        }
        set
        {
            HiddenFieldDesPlans["CitId"] = value.ToString();
        }
    }

    private int _IsCharity
    {
        get
        {
            return Convert.ToInt32(HiddenFieldDesPlans["IsCharity"]);
        }
        set
        {
            HiddenFieldDesPlans["IsCharity"] = value.ToString();
        }
    }
    private int _AgentId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldDesPlans["AgentId"]);
        }
        set
        {
            HiddenFieldDesPlans["AgentId"] = value.ToString();
        }
    }
    private Boolean _CanEditProjectInfoInThisRequest
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldDesPlans["CanEditProjectInfoInThisRequest"]);
        }
        set
        {
            HiddenFieldDesPlans["CanEditProjectInfoInThisRequest"] = value.ToString();
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();
            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Project.aspx?" + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
            }

            if (Request.QueryString["PlnPgMd"] == null || Request.QueryString["PlnId"] == null)
            {
                string QS = "ProjectId=" + Request.QueryString["ProjectId"].ToString() +
                    "&PrjReId=" + Request.QueryString["PrjReId"].ToString() +
                    "&PageMode=" + Request.QueryString["PageMode"].ToString() +
                    "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
                Response.Redirect("Plans.aspx?" + QS);
            }

            TSP.DataManager.Permission perDes = TSP.DataManager.TechnicalServices.Project_DesignerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            BtnNew.Enabled = perDes.CanNew;
            BtnNew2.Enabled = perDes.CanNew;
            btnEdit.Enabled = perDes.CanEdit;
            btnEdit2.Enabled = perDes.CanEdit;
            btnView.Enabled = perDes.CanView;
            btnView2.Enabled = perDes.CanView;
            btnInActive.Enabled = perDes.CanDelete;
            btnInActive2.Enabled = perDes.CanDelete;
            GridViewPlanDesigner.Visible = perDes.CanView;
            btnDesAcc.Enabled = btnDesAcc2.Enabled = perDes.CanView;

            SetKey();
            CheckWorkFlowPermission();
            if (!_CanEditProjectInfoInThisRequest)
                BtnNew.Enabled = BtnNew2.Enabled = btnEdit.Enabled = btnEdit2.Enabled = btnInActive.Enabled = btnInActive2.Enabled = false;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnDelete"] = btnInActive.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["btnDesAcc"] = btnDesAcc.Enabled;
        }

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["btnDesAcc"] != null)
            this.btnDesAcc.Enabled = this.btnDesAcc2.Enabled = (bool)this.ViewState["btnDesAcc"];
    }

    #region btn Click
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        Delete_Inactive();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "ProjectId=" + HiddenFieldDesPlans["PrjId"].ToString() +
                    "&PrjReId=" + HiddenFieldDesPlans["PrjReId"].ToString() +
                    "&PageMode=" + HiddenFieldDesPlans["PrjPgMd"].ToString() +
                    "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        Response.Redirect("Plans.aspx?" + QS);
    }

    protected void btnDesAcc_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "&ProjectId=" + HiddenFieldDesPlans["PrjId"].ToString() +
                "&PrjReId=" + HiddenFieldDesPlans["PrjReId"].ToString() +
                "&IngT=" + Utility.EncryptQS(((int)TSP.DataManager.TSProjectIngridientType.Designer).ToString()) +
                "&PageMode=" + HiddenFieldDesPlans["PrjPgMd"].ToString() +
                "&PlnPgMd=" + Request.QueryString["PlnPgMd"] +
                "&UrlReferrer=" + Utility.EncryptQS("PlanDesigner.aspx") +
                "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        Response.Redirect("ProjectAccounting.aspx?" + QS);

    }
    #endregion

    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        ProjectId = Utility.DecryptQS(HiddenFieldDesPlans["PrjId"].ToString());
        PrjReId = HiddenFieldDesPlans["PrjReId"].ToString();
        string PageMode = HiddenFieldDesPlans["PrjPgMd"].ToString();

        PrjMainMenu MainMenu = new PrjMainMenu("Project", Convert.ToInt32(ProjectId));
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, PrjReId, PageMode, GrdFlt, SrchFlt));
    }
    protected void MenuPlan_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        string QS = "PrjId=" + HiddenFieldDesPlans["PrjId"].ToString() +
                    "&PrjReId=" + HiddenFieldDesPlans["PrjReId"].ToString() +
                    "&PlnId=" + HiddenFieldDesPlans["PlnId"].ToString()
                    + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        switch (e.Item.Name)
        {
            case "Plan":
                QS = QS + "&PrePgMd=" + HiddenFieldDesPlans["PrjPgMd"].ToString() + "&PlnPgMd=" + Request.QueryString["PlnPgMd"];
                Response.Redirect("AddPlans.aspx?" + QS);
                break;

            case "ControlerViewPoint":
                QS = QS + "&PrePgMd=" + HiddenFieldDesPlans["PrjPgMd"].ToString() + "&PlnPgMd=" + Request.QueryString["PlnPgMd"];
                Response.Redirect("PlanControlers.aspx?" + QS);
                break;
        }
    }

    protected void GridViewPlanDesigner_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "Date":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewPlanDesigner_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Date")
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion

    #region Methods
    /**************************************************************************************************************************************************/
    private void SetKey()
    {
        try
        {
            PrjReId = Utility.DecryptQS(Request.QueryString["PrjReId"].ToString());
            PlansId = Utility.DecryptQS(Request.QueryString["PlnId"].ToString());
            ProjectId = Utility.DecryptQS(Request.QueryString["ProjectId"].ToString());
            HiddenFieldDesPlans["PrjPgMd"] = Request.QueryString["PageMode"].ToString();
            HiddenFieldDesPlans["PlansTypeId"] = "";

            if (string.IsNullOrEmpty(PlansId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            ObjdsPlanDesigner.SelectParameters["PlansId"].DefaultValue = PlansId;

            FillProjectInfo(int.Parse(PrjReId));
            FillPlanInfo();

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    protected void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
        _IsCharity = prjInfo.IsCharity;
        _CitId = prjInfo.CitId;
        _AgentId = prjInfo.AgentId;
        _CanEditProjectInfoInThisRequest = prjInfo.CanEditProjectInfoInThisRequest;
    }

    private void FillPlanInfo()
    {
        if (string.IsNullOrEmpty(PlansId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();

        DataTable dtPlan = PlansManager.SelectById(Convert.ToInt32(PlansId), -1);

        if (dtPlan.Rows.Count == 1)
        {

            if (!Utility.IsDBNullOrNullValue(dtPlan.Rows[0]["No"]))
                txtPlanNo.Text = dtPlan.Rows[0]["No"].ToString();
            if (!Utility.IsDBNullOrNullValue(dtPlan.Rows[0]["PlansType"]))
                txtPlanType.Text = dtPlan.Rows[0]["PlansType"].ToString();
            if (!Utility.IsDBNullOrNullValue(dtPlan.Rows[0]["PlanVersion"]))
                txtPlanVer.Text = dtPlan.Rows[0]["PlanVersion"].ToString();

            HiddenFieldDesPlans["PlansTypeId"] = dtPlan.Rows[0]["PlansTypeId"];

            txtFollowCode.Text = dtPlan.Rows[0]["FollowCode"].ToString();


            if (!Utility.IsDBNullOrNullValue(dtPlan.Rows[0]["TaskName"]))
                lblWorkFlowState.Text = "وضعیت درخواست ثبت طراح و نقشه مربوطه: " + dtPlan.Rows[0]["TaskName"].ToString();
            else
                lblWorkFlowState.Text = "وضعیت درخواست ثبت طراح و نقشه مربوطه: " + "نامشخص";
        }
    }

    private void NextPage(string Mode)
    {
        int DesignerPlansId = -1;
        int PrjDesignerId = -1;
        int focucedIndex = GridViewPlanDesigner.FocusedRowIndex;

        if (focucedIndex > -1 && Mode != "New")
        {
            DataRow row = GridViewPlanDesigner.GetDataRow(focucedIndex);
            DesignerPlansId = (int)row["DesignerPlansId"];
            PrjDesignerId = (int)row["PrjDesignerId"];
        }
        if (DesignerPlansId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک ردیف را انتخاب نمائید";
            return;
        }
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        string QS = "DsPId=" + Utility.EncryptQS(DesignerPlansId.ToString()) +
            "&PgMd=" + Utility.EncryptQS(Mode) +
            "&ProjectId=" + HiddenFieldDesPlans["PrjId"].ToString() +
            "&PrjReId=" + HiddenFieldDesPlans["PrjReId"].ToString() +
            "&PlnPgMd=" + Request.QueryString["PlnPgMd"] +
            "&PlnId=" + HiddenFieldDesPlans["PlnId"].ToString() +
            "&PageMode=" + HiddenFieldDesPlans["PrjPgMd"].ToString() +
            "&PrjDesignerId=" + Utility.EncryptQS(PrjDesignerId.ToString()) +
            "&PageSender=" + Utility.EncryptQS("PlanDesigner") +
            "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        Response.Redirect("AddPlanDesigner.aspx?" + QS);
    }

    private void Delete_Inactive()
    {
        int PrjDesignerId = -1;
        int PrjDesignerReId = -1;
        int DesignerPlansId = -1;
        int PlansId = -1;
        int DesignerInActive = -1;
        int MemberTypeId = -1;
        if (GridViewPlanDesigner.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPlanDesigner.GetDataRow(GridViewPlanDesigner.FocusedRowIndex);
            PrjDesignerId = (int)row["PrjDesignerId"];
            PrjDesignerReId = (int)row["PrjDesignerReId"];
            DesignerPlansId = (int)row["DesignerPlansId"];
            PlansId = (int)row["PlansId"];
            DesignerInActive = (int)row["DesignerInActive"];
            MemberTypeId = (int)row["MemberTypeId"];
        }

        if (PrjDesignerId == -1 || PrjDesignerReId == -1 || DesignerPlansId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاًابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        else
        {
            if (DesignerInActive == 1)
            {
                ShowMessage("پیش از این طراح انتخاب شده غیر فعال شده است.");
                return;
            }
            int ProjectIngridientTypeId = MemberTypeId == (int)TSP.DataManager.TSMemberType.Member ? (int)TSP.DataManager.TSProjectIngridientType.Observer : (int)TSP.DataManager.TSProjectIngridientType.Designer;
            string CurrentCapacityAssignmentYear = ""; Boolean IsCapacityDecreased = false;
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            if (_AgentId == Utility.GetCurrentAgentCode())
                CapacityAssignmentManager.SelectCurrentYearAndStage(1);
            else
                CapacityAssignmentManager.SelectCurrentYearAndStage(0);
            if (CapacityAssignmentManager.Count > 0)
            {
                CurrentCapacityAssignmentYear = CapacityAssignmentManager[0]["Year"].ToString();
            }
            TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
            ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(Convert.ToInt32(ProjectId), PrjDesignerId, ProjectIngridientTypeId, (int)TSP.DataManager.TSProjectIngridientType.Designer);
            string DecreamentDate = ProjectCapacityDecrementManager[0]["Year"].ToString();
            if (string.Compare(CurrentCapacityAssignmentYear, DecreamentDate) <= 0)//اگر سال کاری کوچکتر از سال کاری جاری باشد.ظرفیت ها از امسال کسر نمی شود و فقط ثبت می شود
            {
                if (Utility.WorkBasedOnWorkRequest())
                {
                    IsCapacityDecreased = true;
                }
            }
            if (PrjDesignerReId == Convert.ToInt32(PrjReId))
                Delete(PrjDesignerId, DesignerPlansId, PlansId, ProjectIngridientTypeId, CurrentCapacityAssignmentYear, IsCapacityDecreased);
            else
                InActive(PrjDesignerId, IsCapacityDecreased, ProjectIngridientTypeId);
        }
    }
    private void Delete(int PrjDesignerId, int DesignerPlansId, int PlansId, int ProjectIngridientTypeId, string CurrentCapacityAssignmentYear, Boolean IsCapacityDecreased)
    {
        TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(ProjectDesignerManager);
        transact.Add(DesignerPlansManager);
        transact.Add(ProjectCapacityDecrementManager);
        transact.Add(ProjectOfficeMembersManager);

        try
        {
            transact.BeginSave();
            DeleteDesignerPlans(DesignerPlansId, DesignerPlansManager);
            DeleteProjectCapacityDecrement(PrjDesignerId, ProjectCapacityDecrementManager, transact, CurrentCapacityAssignmentYear, ProjectIngridientTypeId, IsCapacityDecreased);
            DeleteProjectOfficeMembers(PrjDesignerId, ProjectOfficeMembersManager);
            DeleteProjectDesigner(PrjDesignerId, ProjectDesignerManager);
            transact.EndSave();
            ShowMessage("ذخیره انجام شد.");
            GridViewPlanDesigner.DataBind();
        }
        catch (Exception err)
        {
            transact.CancelSave();
            SetError(err);
        }
    }

    private void DeleteDesignerPlans(int DesignerPlansId, TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager)
    {
        DesignerPlansManager.FindByDesignerPlansId(DesignerPlansId);
        if (DesignerPlansManager.Count > 0)
        {
            DesignerPlansManager[0].Delete();
            DesignerPlansManager.Save();
        }
    }
    private void DeleteProjectCapacityDecrement(int PrjDesignerId, TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TransactionManager trans, string CurrentCapacityAssignmentYear, int ProjectIngridientTypeId, Boolean IsCapacityDecreased)
    {
        ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(Convert.ToInt32(ProjectId), PrjDesignerId, ProjectIngridientTypeId, (int)TSP.DataManager.TSProjectIngridientType.Designer);
        string DecreamentDate = ProjectCapacityDecrementManager[0]["Year"].ToString();
        int CapacityDecrement = Convert.ToInt32(ProjectCapacityDecrementManager[0]["CapacityDecrement"]);
        int MeOfficeOthPEngOId = Convert.ToInt32(ProjectCapacityDecrementManager[0]["MeOfficeOthPEngOId"]);
        if (ProjectCapacityDecrementManager.Count > 0)
        {
            ProjectCapacityDecrementManager[0].Delete();
            ProjectCapacityDecrementManager.Save();
            ProjectCapacityDecrementManager.DataTable.AcceptChanges();
        }
        if (IsCapacityDecreased)
        {
            TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
            trans.Add(ObserverWorkRequestManager);
            CapacityCalculations CapacityCalculations = new CapacityCalculations();
            CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, ProjectCapacityDecrementManager, MeOfficeOthPEngOId, Utility.GetCurrentUser_UserId(), Convert.ToInt32(ProjectId), _CitId, Convert.ToBoolean(_IsCharity), (TSP.DataManager.TSProjectIngridientType)ProjectIngridientTypeId, null, false, false);
        }
    }
    private void DeleteProjectOfficeMembers(int PrjDesignerId, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager)
    {
        ProjectOfficeMembersManager.FindByIngridientTypeAndPrjImpObsDsgnId((int)TSP.DataManager.TSProjectIngridientType.Designer, PrjDesignerId);
        for (int i = 0; i < ProjectOfficeMembersManager.Count; i++)
        {
            ProjectOfficeMembersManager[i].Delete();
        }
        ProjectOfficeMembersManager.Save();
    }
    private void DeleteProjectDesigner(int PrjDesignerId, TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager)
    {
        ProjectDesignerManager.spSelectTSProjectDesignerForDelete(PrjDesignerId);
        if (ProjectDesignerManager.Count > 0)
        {
            ProjectDesignerManager[0].Delete();
            ProjectDesignerManager.Save();
        }
    }
    private void InActive(int PrjDesignerId, Boolean IsCapacityDecreased, int ProjectIngridientTypeId)
    {
        TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        ProjectDesignerManager.FindByPrjDesignerId(PrjDesignerId);

        try
        {
            if (ProjectDesignerManager.Count > 0)
            {
                InsertInActive(PrjDesignerId, Convert.ToInt32(PrjReId), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Designer), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest), IsCapacityDecreased, ProjectIngridientTypeId);

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است.";
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="TableId">PrjDesignerId</param>
    /// <param name="ReqId">PrjReId</param>
    /// <param name="TableType"></param>
    /// <param name="ReTableType"></param>
    protected void InsertInActive(int TableId, int ReqId, int TableType, int ReTableType, Boolean IsCapacityDecreased, int ProjectIngridientTypeId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();

        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        trans.Add(Manager);
        trans.Add(ProjectCapacityDecrementManager);
        try
        {
            trans.BeginSave();

            DataRow dr = Manager.NewRow();

            dr.BeginEdit();
            dr["TableId"] = TableId;
            dr["TableType"] = TableType;
            dr["ReqId"] = ReqId;
            dr["ReqType"] = ReTableType;
            dr["InActive"] = 1;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            dr.EndEdit();

            Manager.AddRow(dr);
            Manager.Save();
            InActiveProjectCapacityDecrement(TableId, ProjectCapacityDecrementManager, trans, ProjectIngridientTypeId, IsCapacityDecreased);
            trans.EndSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد.";

            GridViewPlanDesigner.DataBind();
        }
        catch (Exception ex)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطا در ذخیره اطلاعات ایجاد شده است.";
            trans.CancelSave();
            Utility.SaveWebsiteError(ex);
        }
    }
    private void InActiveProjectCapacityDecrement(int PrjDesignerId, TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TransactionManager trans, int ProjectIngridientTypeId, Boolean IsCapacityDecreased)
    {
        ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(Convert.ToInt32(ProjectId), PrjDesignerId, ProjectIngridientTypeId, (int)TSP.DataManager.TSProjectIngridientType.Designer);
        string DecreamentDate = ProjectCapacityDecrementManager[0]["Year"].ToString();
        int CapacityDecrement = Convert.ToInt32(ProjectCapacityDecrementManager[0]["CapacityDecrement"]);
        int MeOfficeOthPEngOId = Convert.ToInt32(ProjectCapacityDecrementManager[0]["MeOfficeOthPEngOId"]);
        if (ProjectCapacityDecrementManager.Count > 0 && Convert.ToInt32(ProjectCapacityDecrementManager[0]["IsFree"]) == 0)
        {
            ProjectCapacityDecrementManager[0].BeginEdit();
            ProjectCapacityDecrementManager[0]["IsFree"] = 1;
            ProjectCapacityDecrementManager[0]["FreeDate"] = Utility.GetDateOfToday();
            ProjectCapacityDecrementManager[0].EndEdit();
            ProjectCapacityDecrementManager.Save();
            ProjectCapacityDecrementManager.DataTable.AcceptChanges();
        }
        if (IsCapacityDecreased)
        {
            TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
            trans.Add(ObserverWorkRequestManager);
            CapacityCalculations CapacityCalculations = new CapacityCalculations();
            CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, ProjectCapacityDecrementManager, MeOfficeOthPEngOId, Utility.GetCurrentUser_UserId(), Convert.ToInt32(ProjectId), _CitId, Convert.ToBoolean(_IsCharity), (TSP.DataManager.TSProjectIngridientType)ProjectIngridientTypeId, null, false, false);
        }
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else if (se.Number == 2627)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    #region WorkFlow Methods
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForEdit();
    }

    private void CheckWorkFlowPermissionForEdit()
    {
        TSP.DataManager.WFPermission PerProject = CheckProjectWorkFlowPermissionForEdit();
        TSP.DataManager.WFPermission PerPlanType = CheckPlanWorkFlowPermissionByPlanType();
        //**************در مرحله ثبت نقشه های گردش کار پروژه باشد و یا در مرحله ثبت اطلاعات گردش کار تغییرات نقشه باشد و همچنین نوع نقشه مورد نظر باشد**************
        BtnNew.Enabled = PerProject.BtnNew && PerPlanType.BtnNew;
        BtnNew2.Enabled = PerProject.BtnNew && PerPlanType.BtnNew;
        btnEdit.Enabled = PerProject.BtnEdit && PerPlanType.BtnEdit;
        btnEdit2.Enabled = PerProject.BtnEdit && PerPlanType.BtnEdit;
        btnInActive.Enabled = PerProject.BtnInactive && PerPlanType.BtnInactive;
        btnInActive2.Enabled = PerProject.BtnInactive && PerPlanType.BtnInactive;

        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnDelete"] = btnInActive.Enabled;
    }

    private TSP.DataManager.WFPermission CheckProjectWorkFlowPermissionForEdit()
    {
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        //*******Editing Task Code
        int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;
        int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;

        TSP.DataManager.WFPermission PerAtchitecturalPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ArchitecturalPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructurePlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructurePlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerElectricalInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ElectricalInsPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerMechanicInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(MechanicInsPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());

        WFCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;

        ////*******Editing Task Code
        //int PlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanInfo;

        //TSP.DataManager.WFPermission PerPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(PlanTaskCode, WFCode, int.Parse(PlansId), Utility.GetCurrentUser_UserId());

        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = (PerAtchitecturalPlan.BtnEdit || PerStructurePlan.BtnEdit || PerElectricalInsPlan.BtnEdit || PerMechanicInsPlan.BtnEdit);
        WFPer.BtnSave = (PerAtchitecturalPlan.BtnSave || PerStructurePlan.BtnSave || PerElectricalInsPlan.BtnSave || PerMechanicInsPlan.BtnSave);
        WFPer.BtnNew = (PerAtchitecturalPlan.BtnNew || PerStructurePlan.BtnNew || PerElectricalInsPlan.BtnNew || PerMechanicInsPlan.BtnNew);
        WFPer.BtnInactive = (PerAtchitecturalPlan.BtnInactive || PerStructurePlan.BtnInactive || PerElectricalInsPlan.BtnInactive || PerMechanicInsPlan.BtnInactive);

        return WFPer;
    }

    private TSP.DataManager.WFPermission CheckPlanWorkFlowPermissionByPlanType()
    {
        //*****TableId
        //PrjReId = Utility.DecryptQS(HiddenFieldDesPlans["PrjReId"].ToString());

        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int PlanTaskCode = GetCurrentTaskCode(WFCode, Convert.ToInt32(PrjReId));
        int PlansTypeId = Convert.ToInt32(HiddenFieldDesPlans["PlansTypeId"]);

        bool Permit = false;

        switch (PlansTypeId)
        {
            case (int)TSP.DataManager.TSPlansType.Memari:
                switch (PlanTaskCode)
                {
                    case (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject:
                        Permit = true;
                        break;

                    default:
                        Permit = false;
                        break;
                }
                break;

            case (int)TSP.DataManager.TSPlansType.Sazeh:
            case (int)TSP.DataManager.TSPlansType.TasisatBargh:
            case (int)TSP.DataManager.TSPlansType.TasisatMechanic:
            case (int)TSP.DataManager.TSPlansType.Shahrsazi:
                switch (PlanTaskCode)
                {
                    case (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject:
                    case (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject:
                    case (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject:
                        Permit = true;
                        break;

                    default:
                        Permit = false;
                        break;
                }
                break;

            default:
                Permit = false;
                break;
        }

        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = Permit;
        WFPer.BtnSave = Permit;
        WFPer.BtnNew = Permit;
        WFPer.BtnInactive = Permit;

        return WFPer;
    }

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

    #endregion

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}
