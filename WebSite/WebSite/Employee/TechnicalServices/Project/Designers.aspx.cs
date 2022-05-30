using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Employee_TechnicalServices_Project_Designers : System.Web.UI.Page
{
    #region Properties
    private string _PageMode
    {
        get
        {
            return HiddenFieldDesPlans["PageMode"].ToString();
        }
        set
        {
            HiddenFieldDesPlans["PageMode"] = value;
        }
    }

    private int _PrjReId
    {
        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldDesPlans["PrjReId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrjReId"].ToString()));
            }
        }
        set
        {
            HiddenFieldDesPlans["PrjReId"] = value;
        }
    }

    private int _ProjectId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldDesPlans["PrjId"]);
        }
        set
        {
            HiddenFieldDesPlans["PrjId"] = value;
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
    #region Btns
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]))
            {
                Response.Redirect("Project.aspx");
            }

            Session["SendBackDataTable_EmpPrjDes"] = "";

            TSP.DataManager.Permission perDes = TSP.DataManager.TechnicalServices.Project_DesignerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = perDes.CanNew;
            BtnNew2.Enabled = perDes.CanNew;
            btnactive.Enabled = btnactive2.Enabled = btnEdit.Enabled = btnEdit2.Enabled = perDes.CanEdit;
            btnView.Enabled = perDes.CanView;
            btnView2.Enabled = perDes.CanView;
            btnInActive.Enabled = perDes.CanDelete;
            btnInActive2.Enabled = perDes.CanDelete;
            GridViewDesigner.Visible = perDes.CanView;
            btnDesAcc.Enabled = btnDesAcc2.Enabled = perDes.CanView;

            SetKeys();
            CheckWorkFlowPermission();
            if (!_CanEditProjectInfoInThisRequest)
                BtnNew.Enabled = BtnNew2.Enabled = btnactive.Enabled = btnactive2.Enabled = btnEdit.Enabled = btnEdit2.Enabled 
                    =btnInActive.Enabled = btnInActive2.Enabled = btnDesAcc.Enabled = btnDesAcc2.Enabled = btnView.Enabled = btnView2.Enabled = false;
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
            btnactive.Enabled = btnactive2.Enabled = this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["btnDesAcc"] != null)
            this.btnDesAcc.Enabled = this.btnDesAcc2.Enabled = (bool)this.ViewState["btnDesAcc"];
    }

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
        int LastPrjReId = -1;
        if (GridViewDesigner.FocusedRowIndex > -1)
        {
            DataRow row = GridViewDesigner.GetDataRow(GridViewDesigner.FocusedRowIndex);
            LastPrjReId = (int)row["PrjDesignerReId"];
            if (_PrjReId != LastPrjReId)
            {
                ShowMessage("امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.");
                return;
            }

            NextPage("Edit");
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        int PrjDesignerId = -1;
        int PrjDesignerReId = -1;
        int PlansId = -1;
        int DesignerInActive = -1;
        int MemberTypeId = -1;
        int AgentId = -2;
        int MeId = -2;
        if (GridViewDesigner.FocusedRowIndex > -1)
        {
            DataRow row = GridViewDesigner.GetDataRow(GridViewDesigner.FocusedRowIndex);
            PrjDesignerId = (int)row["PrjDesignerId"];
            PrjDesignerReId = (int)row["PrjDesignerReId"];
            DesignerInActive = (int)row["DesignerInActive"];
            MemberTypeId = (int)row["MemberTypeId"];
            AgentId = (int)row["DesignerAgentId"];
            MeId = (int)row["DesignerMeId"];
        }

        if (PrjDesignerId == -1 || PrjDesignerReId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاًابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        if (DesignerInActive == 1)
        {
            ShowMessage("پیش از این طراح انتخاب شده غیر فعال شده است.");
            return;
        }
        int ProjectIngridientTypeId = MemberTypeId == (int)TSP.DataManager.TSMemberType.Member ? (int)TSP.DataManager.TSProjectIngridientType.Observer : (int)TSP.DataManager.TSProjectIngridientType.Designer;
        string CurrentCapacityAssignmentYear = ""; Boolean IsCapacityDecreased = false;
        TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
        if (AgentId == Utility.GetCurrentAgentCode())
            CapacityAssignmentManager.SelectCurrentYearAndStage(1);
        else
            CapacityAssignmentManager.SelectCurrentYearAndStage(0);
        if (CapacityAssignmentManager.Count > 0)
        {
            CurrentCapacityAssignmentYear = CapacityAssignmentManager[0]["Year"].ToString();
        }
        if (CurrentCapacityAssignmentYear == "")
        {
            ShowMessage("خطا در بازیابی اطلاعات سال کاری ایجاد شده است.");
            return;
        }
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(_ProjectId, PrjDesignerId, ProjectIngridientTypeId, (int)TSP.DataManager.TSProjectIngridientType.Designer);
        if (ProjectCapacityDecrementManager.Count > 0)
        {
            string DecreamentDate = ProjectCapacityDecrementManager[0]["Year"].ToString();
            if (string.Compare(CurrentCapacityAssignmentYear, DecreamentDate) <= 0)//اگر سال کاری کوچکتر از سال کاری جاری باشد.ظرفیت ها از امسال کسر نمی شود و فقط ثبت می شود
            {
                if (Utility.WorkBasedOnWorkRequest())
                {
                    IsCapacityDecreased = true;
                }
            }
        }
        if (PrjDesignerReId == _PrjReId)
            Delete(PrjDesignerId, PlansId, ProjectIngridientTypeId, CurrentCapacityAssignmentYear, IsCapacityDecreased);
        else
            InActive(PrjDesignerId, IsCapacityDecreased, ProjectIngridientTypeId);
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.TSProjectConfirming, _PrjReId, (int)TSP.DataManager.TableType.TSProject_Designer, "غیر فعال کردن طراح با کد عضویت " + MeId.ToString(), Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.InActiveInfo);
    }

    protected void btnactive_Click(object sender, EventArgs e)
    {

        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TransactionManager.Add(ObserverWorkRequestManager);
        TransactionManager.Add(ProjectCapacityDecrementManager);
        TransactionManager.Add(RequestInActivesManager);
        try
        {
            int PrjDesignerId = -1;
            int PrjDesignerReId = -1;
            int DesignerInActive = -1;
            int MemberTypeId = -1;
            int AgentId = -2;
            if (GridViewDesigner.FocusedRowIndex > -1)
            {
                DataRow row = GridViewDesigner.GetDataRow(GridViewDesigner.FocusedRowIndex);
                PrjDesignerId = (int)row["PrjDesignerId"];
                PrjDesignerReId = (int)row["PrjDesignerReId"];
                DesignerInActive = (int)row["DesignerInActive"];
                MemberTypeId = (int)row["MemberTypeId"];
                AgentId = (int)row["DesignerAgentId"];
            }

            if (PrjDesignerId == -1 || PrjDesignerReId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاًابتدا یک رکورد را انتخاب نمائید";
                return;
            }
            if (DesignerInActive == 0)
            {
                ShowMessage("طراح انتخاب شده  فعال می باشد.");
                return;
            }
            int ProjectIngridientTypeId = (int)TSP.DataManager.TSProjectIngridientType.Designer;
            string CurrentCapacityAssignmentYear = ""; Boolean IsCapacityDecreased = false;
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            if (AgentId == Utility.GetCurrentAgentCode())
                CapacityAssignmentManager.SelectCurrentYearAndStage(1);
            else
                CapacityAssignmentManager.SelectCurrentYearAndStage(0);
            if (CapacityAssignmentManager.Count > 0)
            {
                CurrentCapacityAssignmentYear = CapacityAssignmentManager[0]["Year"].ToString();
            }
            if (CurrentCapacityAssignmentYear == "")
            {
                ShowMessage("خطا در بازیابی اطلاعات سال کاری ایجاد شده است.");
                return;
            }
            ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(_ProjectId, PrjDesignerId, ProjectIngridientTypeId, (int)TSP.DataManager.TSProjectIngridientType.Designer);
            if (ProjectCapacityDecrementManager.Count > 0)
            {
                string DecreamentDate = ProjectCapacityDecrementManager[0]["Year"].ToString();
                if (string.Compare(CurrentCapacityAssignmentYear, DecreamentDate) <= 0)//اگر سال کاری کوچکتر از سال کاری جاری باشد.ظرفیت ها از امسال کسر نمی شود و فقط ثبت می شود
                {
                    IsCapacityDecreased = true;
                }
            }
            TransactionManager.BeginSave();
            int ResultDel = TSP.DataManager.RequestInActivesManager.DeleteRequestInActive(_PrjReId, PrjDesignerId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Designer), RequestInActivesManager);
            if (ResultDel == 1)
            {
                ShowMessage("رکورد فعال می باشد و یا در درخواست های قبل غیر فعال شده است");
                TransactionManager.CancelSave();
                return;
            }
            if (ResultDel == 2)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                TransactionManager.CancelSave();
                return;
            }
            if (ResultDel != 0)
            {
                ShowMessage("خطا در ذخیره ایجاد شده است.");
                TransactionManager.CancelSave();
                return;
            }


            ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(_ProjectId, PrjDesignerId, (int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSProjectIngridientType.Designer);
            if (ProjectCapacityDecrementManager.Count != 1)
            {
                ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است.");
                TransactionManager.CancelSave();
                return;
            }
            ProjectCapacityDecrementManager[0].BeginEdit();
            ProjectCapacityDecrementManager[0]["IsFine"] = 0;
            ProjectCapacityDecrementManager[0]["FineExpireDate"] = "";
            int MeId = Convert.ToInt32(ProjectCapacityDecrementManager[0]["MeOfficeOthPEngOId"]);
            if (IsCapacityDecreased)//اگر سال کاری کوچکتر از سال کاری جاری باشد.ظرفیت ها از امسال کسر نمی شود و فقط ثبت می شود
            {
                ProjectCapacityDecrementManager[0]["IsFree"] = 0;
                ProjectCapacityDecrementManager[0]["FreeDate"] = DBNull.Value;
                ProjectCapacityDecrementManager[0]["IsWorkFree"] = 0;
                ProjectCapacityDecrementManager[0]["WorkFreeDate"] = DBNull.Value;
            }
            ProjectCapacityDecrementManager[0].EndEdit();
            ProjectCapacityDecrementManager.Save();
            ProjectCapacityDecrementManager.DataTable.AcceptChanges();
            if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Member)
            {
                CapacityCalculations CapacityCalculations = new CapacityCalculations();
                int Result = CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, ProjectCapacityDecrementManager, MeId, Utility.GetCurrentUser_UserId(), _ProjectId, _CitId, Convert.ToBoolean(_IsCharity), TSP.DataManager.TSProjectIngridientType.Observer, null, false, false);
                if (Result < 0)
                {
                    ShowMessage("خطا در ذخیره ایجاد شده است.");
                    TransactionManager.CancelSave();
                    return;
                }

            }
            TransactionManager.EndSave();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            WorkFlowStateManager.InsertWorkFlowStateLog((int)TSP.DataManager.WorkFlows.TSProjectConfirming, _PrjReId, (int)TSP.DataManager.TableType.TSProject_Designer, "فعال کردن طراح با کد عضویت " + MeId.ToString(), Utility.GetCurrentUser_UserId(), TSP.DataManager.WorkFlowStateType.UpdateInfo);
            GridViewDesigner.DataBind();
        }
        catch (Exception ex)
        {
            TransactionManager.CancelSave();
            ShowMessage("خطا در ذخیره اطلاعات ایجاد شده است");
            Utility.SaveWebsiteError(ex);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "ProjectId=" + Utility.EncryptQS(_ProjectId.ToString()) +
                    "&PrjReId=" + Utility.EncryptQS(_PrjReId.ToString()) +
                    "&PageMode=" + Utility.EncryptQS(_PageMode.ToString()) +
                    "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        Response.Redirect("ProjectInsert.aspx?" + QS);
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();
            Response.Redirect("Project.aspx?PostId=" + HiddenFieldDesPlans["PrjId"] + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("Project.aspx");
        }
    }

    protected void btnDesAcc_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "&ProjectId=" + Utility.EncryptQS(_ProjectId.ToString()) +
                "&PrjReId=" + Utility.EncryptQS(_PrjReId.ToString()) +
                "&IngT=" + Utility.EncryptQS(((int)TSP.DataManager.TSProjectIngridientType.Designer).ToString()) +
                "&PageMode=" + Utility.EncryptQS(_PageMode) +
                //"&tbtId=" + Utility.EncryptQS(PrjDesignerId.ToString()) +
                //"&PlnId=" + Utility.EncryptQS(PlansId.ToString()) +
                //"&PlnTypeId=" + Utility.EncryptQS(PlanTypeId.ToString()) +
                "&UrlReferrer=" + Utility.EncryptQS("Designers.aspx") +
                "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        Response.Redirect("ProjectAccountingDesigner.aspx?" + QS);

    }
    #endregion

    #region Grids

    protected void GridViewDesigner_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (e.GetValue("PrjDesignerReId") == null)
            return;
        int CurretnPrjReId = Convert.ToInt32(e.GetValue("PrjDesignerReId"));
        if (_PrjReId == CurretnPrjReId)
        {
            e.Row.BackColor = System.Drawing.Color.LightGray;
        }
    }

    #endregion

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int ProjectReTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest);
        //  int WfCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        int WfCode = -1;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dt = WorkFlowStateManager.SelectLastState(ProjectReTableType, _PrjReId);
        if (dt.Rows.Count > 0)
            WfCode = Convert.ToInt32(dt.Rows[0]["WorkFlowCode"]);

        if (WfCode == -1)
        {
            WFUserControl.SetMsgText("گردش کار درخواست جاری نامشخص است");
            return;
        }
        string QS = "~/Employee/TechnicalServices/Project/Designers.aspx?ProjectId=" + Request.QueryString["ProjectId"] +
               "&PrjReId=" + Request.QueryString["PrjReId"] +
               "&PageMode=" + Request.QueryString["PageMode"] +
               //"&PlnId=" + HiddenFieldPrjDes["PlansId"].ToString() +
               "&GrdFlt=" + Request.QueryString["GrdFlt"].ToString() + "&SrchFlt=" + Request.QueryString["SrchFlt"].ToString();
        WFUserControl.QueryStringForRedirect = QS;
        WFUserControl.PerformCallback(_PrjReId, ProjectReTableType, WfCode, e);
    }
    #endregion

    #region Methods
    private void SetKeys()
    {
        try
        {
            _PrjReId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrjReId"].ToString()));
            _ProjectId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["ProjectId"].ToString()));
            _PageMode = Utility.DecryptQS(Request.QueryString["PageMode"].ToString());

            if (Utility.IsDBNullOrNullValue(_ProjectId) || Utility.IsDBNullOrNullValue(_PrjReId) || Utility.IsDBNullOrNullValue(_PageMode))
            {
                return;
            }

            ObjdsDesigner.SelectParameters["ProjectId"].DefaultValue = _ProjectId.ToString();
            ObjdsDesigner.SelectParameters["PrjReId"].DefaultValue = _PrjReId.ToString();
            GridViewDesigner.DataBind();

            FillProjectInfo(_PrjReId);
            SetProjectMainMenuEnabled();
            CheckWorkFlowPermission();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            return;
        }
    }

    private void NextPage(string Mode)
    {
        try
        {
            int DesignerPlansId = -1;
            int PrjDesignerId = -1;
            int PlansId = -1;
            if (Mode != "New")
            {
                if (GridViewDesigner.FocusedRowIndex > -1)
                {

                    DataRow row = GridViewDesigner.GetDataRow(GridViewDesigner.FocusedRowIndex);
                    PrjDesignerId = (int)row["PrjDesignerId"];
                    TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
                    DataTable dtDesPlan = DesignerPlansManager.SelectTSDesignerPlansByProjectDesigner(PrjDesignerId, _ProjectId, _PrjReId, -1, -1);
                    if (dtDesPlan.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtDesPlan.Rows[0]["PlanConfirm"]) == 0)//***نقشه در جریان داشته باشد
                        {//Edit
                            PlansId = Convert.ToInt32(dtDesPlan.Rows[0]["PlansId"]);
                            DesignerPlansId = Convert.ToInt32(dtDesPlan.Rows[0]["DesignerPlansId"]);
                        }
                        if (Convert.ToInt32(dtDesPlan.Rows[0]["PlanConfirm"]) != 0)
                        { //NewPlan
                            PlansId = -1;
                            DesignerPlansId = -1;
                        }
                    }
                }

                if (PrjDesignerId == -1)
                {
                    ShowMessage("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
                    return;
                }
            }
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();

            string QS = "DsPId=" + Utility.EncryptQS(DesignerPlansId.ToString()) +
                "&PgMd=" + Utility.EncryptQS(Mode) +
                "&ProjectId=" + Utility.EncryptQS(_ProjectId.ToString()) +
                "&PrjReId=" + Utility.EncryptQS(_PrjReId.ToString()) +
                "&PageMode=" + Utility.EncryptQS(_PageMode) +
                "&PrjDesignerId=" + Utility.EncryptQS(PrjDesignerId.ToString()) +
                "&PlnId=" + Utility.EncryptQS("-1") +
                "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

            Response.Redirect("AddPlanDesigner.aspx?" + QS);
        }
        catch (Exception ex)
        {
            ShowMessage("خطا در بازخوانی اطلاعات ایجاد شده است");
            return;
        }
    }

    protected void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
        _IsCharity = prjInfo.IsCharity;
        _CitId = prjInfo.CitId;
        _CanEditProjectInfoInThisRequest = prjInfo.CanEditProjectInfoInThisRequest;
    }

    private void Delete(int PrjDesignerId, int PlansId, int ProjectIngridientTypeId, string CurrentCapacityAssignmentYear, Boolean IsCapacityDecreased)
    {
        TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();

        TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();

        transact.Add(AccountingDetailManager);
        transact.Add(AccountingManager);
        transact.Add(ProjectDesignerManager);
        transact.Add(DesignerPlansManager);
        transact.Add(ProjectCapacityDecrementManager);
        transact.Add(ProjectOfficeMembersManager);

        transact.Add(AttachmentsManager);
        transact.Add(WorkFlowStateManager);
        transact.Add(PlansManager);

        try
        {

            AccountingManager.FindByTableTypeId(PrjDesignerId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Designer));
            if (AccountingManager.Count > 0)
            {
                if (Convert.ToInt32(AccountingManager[0]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.Payment)
                {
                    ShowMessage("برای طراح انتخاب شده فیش طراحی پرداخت شده وجود دارد.امکان حذف وجود ندارد.");
                    return;
                }
            }
            transact.BeginSave();
            DeleteDesignerPlans(PrjDesignerId, DesignerPlansManager);
            DeleteProjectCapacityDecrement(PrjDesignerId, ProjectCapacityDecrementManager, transact, CurrentCapacityAssignmentYear, ProjectIngridientTypeId, IsCapacityDecreased);
            DeleteProjectOfficeMembers(PrjDesignerId, ProjectOfficeMembersManager);
            DeleteProjectDesigner(PrjDesignerId, ProjectDesignerManager);
            int AccCount = AccountingManager.Count;
            for (int i = 0; i < AccCount; i++)
            {
                int AccId = Convert.ToInt32(AccountingManager[0]["AccountingId"]);
                AccountingDetailManager.FindByAccountingId(AccId);
                int AccDetailCount = AccountingDetailManager.Count;
                for (int j = 0; j < AccDetailCount; j++)
                {
                    AccountingDetailManager[0].Delete();
                    AccountingDetailManager.Save();
                    AccountingDetailManager.DataTable.AcceptChanges();
                }
                AccountingManager[0].Delete();
                AccountingManager.Save();
                AccountingManager.DataTable.AcceptChanges();
            }
            transact.EndSave();

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد.";
            GridViewDesigner.DataBind();
        }
        catch (Exception err)
        {
            transact.CancelSave();
            SetError(err);
        }
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

    private void DeleteDesignerPlans(int PrjDesignerId, TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager)
    {
        DesignerPlansManager.FindByDesignerId(PrjDesignerId);
        for (int i = 0; i < DesignerPlansManager.Count; i++)
        {
            DesignerPlansManager[i].Delete();
        }
        DesignerPlansManager.Save();
    }

    private void DeleteProjectCapacityDecrement(int PrjDesignerId, TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, TSP.DataManager.TransactionManager trans, string CurrentCapacityAssignmentYear, int ProjectIngridientTypeId, Boolean IsCapacityDecreased)
    {
        ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(_ProjectId, PrjDesignerId, ProjectIngridientTypeId, (int)TSP.DataManager.TSProjectIngridientType.Designer);

        if (ProjectCapacityDecrementManager.Count > 0)
        {
            string DecreamentDate = ProjectCapacityDecrementManager[0]["Year"].ToString();
            int CapacityDecrement = Convert.ToInt32(ProjectCapacityDecrementManager[0]["CapacityDecrement"]);
            int MeOfficeOthPEngOId = Convert.ToInt32(ProjectCapacityDecrementManager[0]["MeOfficeOthPEngOId"]);
            ProjectCapacityDecrementManager[0].Delete();
            ProjectCapacityDecrementManager.Save();
            ProjectCapacityDecrementManager.DataTable.AcceptChanges();
            if (IsCapacityDecreased)
            {
                TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
                trans.Add(ObserverWorkRequestManager);
                CapacityCalculations CapacityCalculations = new CapacityCalculations();
                CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, ProjectCapacityDecrementManager, MeOfficeOthPEngOId, Utility.GetCurrentUser_UserId(), _ProjectId, _CitId, Convert.ToBoolean(_IsCharity), (TSP.DataManager.TSProjectIngridientType)ProjectIngridientTypeId, null, false, false);
            }
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

    private void InActive(int PrjDesignerId, Boolean IsCapacityDecreased, int ProjectIngridientTypeId)
    {
        TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        ProjectDesignerManager.FindByPrjDesignerId(PrjDesignerId);

        try
        {
            if (ProjectDesignerManager.Count > 0)
            {
                InsertInActive(PrjDesignerId, _PrjReId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Designer), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest), IsCapacityDecreased, ProjectIngridientTypeId);

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

            GridViewDesigner.DataBind();
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
        ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(_ProjectId, PrjDesignerId, ProjectIngridientTypeId, (int)TSP.DataManager.TSProjectIngridientType.Designer);
        if (ProjectCapacityDecrementManager.Count > 0)
        {
            string DecreamentDate = ProjectCapacityDecrementManager[0]["Year"].ToString();
            int CapacityDecrement = Convert.ToInt32(ProjectCapacityDecrementManager[0]["CapacityDecrement"]);
            int MeOfficeOthPEngOId = Convert.ToInt32(ProjectCapacityDecrementManager[0]["MeOfficeOthPEngOId"]);
            if (Convert.ToInt32(ProjectCapacityDecrementManager[0]["IsFree"]) == 0)
            {
                ProjectCapacityDecrementManager[0].BeginEdit();
                ProjectCapacityDecrementManager[0]["IsFree"] = 1;
                ProjectCapacityDecrementManager[0]["FreeDate"] = Utility.GetDateOfToday();
                ProjectCapacityDecrementManager[0]["IsWorkFree"] = 1;
                ProjectCapacityDecrementManager[0]["WorkFreeDate"] = Utility.GetDateOfToday();
                ProjectCapacityDecrementManager[0].EndEdit();
                ProjectCapacityDecrementManager.Save();
                ProjectCapacityDecrementManager.DataTable.AcceptChanges();
            }
            if (IsCapacityDecreased)
            {
                TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
                trans.Add(ObserverWorkRequestManager);
                CapacityCalculations CapacityCalculations = new CapacityCalculations();
                CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, ProjectCapacityDecrementManager, MeOfficeOthPEngOId, Utility.GetCurrentUser_UserId(), _ProjectId, _CitId, Convert.ToBoolean(_IsCharity), (TSP.DataManager.TSProjectIngridientType)ProjectIngridientTypeId, null, false, false);
            }
        }
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
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
    #endregion

    #region Menu
    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        PrjMainMenu MainMenu = new PrjMainMenu("Designer", _ProjectId);
        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, Utility.EncryptQS(_PrjReId.ToString()), Utility.EncryptQS(_PageMode), GrdFlt, SrchFlt));
    }

    private void SetProjectMainMenuEnabled()
    {
        if (_ProjectId == -1)
            _ProjectId = -2;

        PrjMainMenu PrjMainMenu = new PrjMainMenu("Designer", _ProjectId);
        MainMenu.Items.FindByName("Designer").Selected = true; //Designer
        //MainMenu.Items.FindByName("StatusAnnouncement").Enabled = PrjMainMenu.GetEnabled("StatusAnnouncement");
        //MainMenu.Items.FindByName("BuildingsLicense").Enabled = PrjMainMenu.GetEnabled("BuildingsLicense");
        //MainMenu.Items.FindByName("Timing").Enabled = PrjMainMenu.GetEnabled("Timing");
        MainMenu.Items.FindByName("Contract").Enabled = PrjMainMenu.GetEnabled("Contract");
        MainMenu.Items.FindByName("Implementer").Enabled = PrjMainMenu.GetEnabled("Implementer");
        MainMenu.Items.FindByName("Observers").Enabled = PrjMainMenu.GetEnabled("Observers");
        MainMenu.Items.FindByName("Plans").Enabled = PrjMainMenu.GetEnabled("Plans");
        MainMenu.Items.FindByName("Owner").Enabled = PrjMainMenu.GetEnabled("Owner");
        MainMenu.Items.FindByName("Project").Enabled = PrjMainMenu.GetEnabled("Project");
        MainMenu.Items.FindByName("Accounting").Enabled = PrjMainMenu.GetEnabled("Accounting");

    }
    #endregion

    #region WorkFlow Methods
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForEdit();
    }

    private void CheckWorkFlowPermissionForEdit()
    {
        TSP.DataManager.WFPermission PerProject = CheckProjectWorkFlowPermissionForEdit();
        // TSP.DataManager.WFPermission PerChangePlans = CheckPlanWorkFlowPermissionForEdit();
        //  TSP.DataManager.WFPermission PerPlanType = CheckPlanWorkFlowPermissionByPlanType();
        //**************در مرحله ثبت نقشه های گردش کار پروژه باشد و یا در مرحله ثبت اطلاعات گردش کار تغییرات نقشه باشد و همچنین نوع نقشه مورد نظر باشد**************
        BtnNew.Enabled = PerProject.BtnNew;//|| PerChangePlans.BtnNew) && PerPlanType.BtnNew;
        BtnNew2.Enabled = PerProject.BtnNew;//|| PerChangePlans.BtnNew) && PerPlanType.BtnNew;
        btnactive.Enabled = btnactive2.Enabled = btnEdit.Enabled = PerProject.BtnEdit;//|| PerChangePlans.BtnEdit) && PerPlanType.BtnEdit;
        btnEdit2.Enabled = PerProject.BtnEdit;//|| PerChangePlans.BtnEdit) && PerPlanType.BtnEdit;
        btnInActive.Enabled = PerProject.BtnInactive;// || PerChangePlans.BtnInactive) && PerPlanType.BtnInactive;
        btnInActive2.Enabled = PerProject.BtnInactive;//|| PerChangePlans.BtnInactive) && PerPlanType.BtnInactive;

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
        int SaveStructureAndInstallationPlanOfProjectTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject;

        TSP.DataManager.WFPermission PerAtchitecturalPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ArchitecturalPlanTaskCode, WFCode, _PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructurePlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructurePlanTaskCode, WFCode, _PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerElectricalInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ElectricalInsPlanTaskCode, WFCode, _PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerMechanicInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(MechanicInsPlanTaskCode, WFCode, _PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructureAndInstallationInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(SaveStructureAndInstallationPlanOfProjectTaskCode, WFCode, _PrjReId, Utility.GetCurrentUser_UserId());

        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = (PerAtchitecturalPlan.BtnEdit || PerStructurePlan.BtnEdit || PerElectricalInsPlan.BtnEdit || PerMechanicInsPlan.BtnEdit || PerStructureAndInstallationInsPlan.BtnEdit);
        WFPer.BtnSave = (PerAtchitecturalPlan.BtnSave || PerStructurePlan.BtnSave || PerElectricalInsPlan.BtnSave || PerMechanicInsPlan.BtnSave || PerStructureAndInstallationInsPlan.BtnSave);
        WFPer.BtnNew = (PerAtchitecturalPlan.BtnNew || PerStructurePlan.BtnNew || PerElectricalInsPlan.BtnNew || PerMechanicInsPlan.BtnNew || PerStructureAndInstallationInsPlan.BtnNew);
        WFPer.BtnInactive = (PerAtchitecturalPlan.BtnInactive || PerStructurePlan.BtnInactive || PerElectricalInsPlan.BtnInactive || PerMechanicInsPlan.BtnInactive || PerStructureAndInstallationInsPlan.BtnInactive);

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
}