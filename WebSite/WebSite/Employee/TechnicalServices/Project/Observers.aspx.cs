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

public partial class Employee_TechnicalServices_Project_Observers : System.Web.UI.Page
{
    private int _GroupId
    {
        get
        {
            return Convert.ToInt32(Utility.DecryptQS(HiddenFieldObserver["GroupId"].ToString()));
        }
        set
        {
            HiddenFieldObserver["GroupId"] = Utility.EncryptQS(value.ToString());
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

    public int _PrjReTypeId
    {

        get
        {
            return Convert.ToInt32(HiddenFieldObserver["PrjReTypeId"]);
        }
        set
        {
            HiddenFieldObserver["PrjReTypeId"] = value.ToString();
        }
    }

    public int _FundationDifference
    {
        get
        {
            return Convert.ToInt32(HiddenFieldObserver["FundationDifference"]);
        }
        set
        {
            HiddenFieldObserver["FundationDifference"] = value.ToString();
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
    private int _CurrentPrjReId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldObserver["CurrentPrjReId"]);
        }
        set
        {
            HiddenFieldObserver["CurrentPrjReId"] = value.ToString();
        }
    }
    public Boolean _IsPopulationUnder25000
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldObserver["IsPopulationUnder25000"]);
        }
        set
        {
            HiddenFieldObserver["IsPopulationUnder25000"] = value.ToString();
        }
    }


    private Boolean _CanEditProjectInfoInThisRequest
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldObserver["CanEditProjectInfoInThisRequest"]);
        }
        set
        {
            HiddenFieldObserver["CanEditProjectInfoInThisRequest"] = value.ToString();
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
            TSP.DataManager.Permission perobsSelect = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermissionObserverSelect(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSelectObserversWithLimit2.Visible = btnSelectObserversWithLimit1.Visible = btnSelectObservers.Visible = btnSelectObservers2.Visible = perobsSelect.CanNew;
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Visible = btnNew2.Visible = per.CanNew;
            btnactive.Visible = btnactive2.Visible = btnInActive.Visible = btnInActive2.Visible = btnEdit.Visible = btnEdit2.Visible = per.CanEdit;
            btnView.Enabled = btnView2.Enabled = btnAccObs.Enabled = btnAccObs2.Enabled = per.CanView;
            GridViewObserver.Visible = per.CanView;

            TSP.DataManager.Permission perRejectObsSselect = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermissionRejectObserverSelectByNezam(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnRejectObsWork.Visible = btnRejectObsWork2.Visible = perRejectObsSselect.CanView || perRejectObsSselect.CanEdit || perRejectObsSselect.CanNew;

            TSP.DataManager.Permission perChooseCoordinatorObserver = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermissionChooseCoordinatorObserver(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnCancelObsCoordinator.Visible = btnCancelObsCoordinator1.Visible = btnObsCoordinator.Visible = btnObsCoordinator2.Visible = perChooseCoordinatorObserver.CanView || perChooseCoordinatorObserver.CanEdit || perChooseCoordinatorObserver.CanNew;

            TSP.DataManager.Permission perTSNewObserverKardan = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermissionTSNewObserverKardan(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNewKardan.Visible = BtnNewKardan2.Visible = perTSNewObserverKardan.CanNew;
            TSP.DataManager.Permission perTSDeleteObSelect = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermissionTSDeleteObSelect(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnInDeleteObsSelect.Visible = btnInDeleteObsSelect2.Visible = perTSDeleteObSelect.CanNew || perTSDeleteObSelect.CanEdit || perTSDeleteObSelect.CanDelete || perTSDeleteObSelect.CanView;

            Session["SendBackDataTable_EmpPrj"] = "";

            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]))
            {
                Response.Redirect("Project.aspx");
            }

            SetKeys();

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["btnAccObs"] = btnAccObs.Enabled;
            this.ViewState["btnRejectObsWork"] = btnRejectObsWork.Visible;
            this.ViewState["btnObsCoordinator"] = btnObsCoordinator.Visible;
            this.ViewState["BtnNewKardan"] = BtnNewKardan.Visible;
            this.ViewState["btnInDeleteObsSelect"] = btnInDeleteObsSelect.Visible;

        }

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            btnactive.Enabled = btnactive2.Enabled = this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnAccObs"] != null)
            this.btnAccObs.Enabled = this.btnAccObs2.Enabled = (bool)this.ViewState["btnAccObs"];
        if (this.ViewState["btnRejectObsWork"] != null)
            this.btnRejectObsWork.Visible = this.btnRejectObsWork2.Visible = (bool)this.ViewState["btnRejectObsWork"];
        if (this.ViewState["btnObsCoordinator"] != null)
            btnCancelObsCoordinator.Visible = btnCancelObsCoordinator1.Visible = this.btnObsCoordinator.Visible = this.btnObsCoordinator2.Visible = (bool)this.ViewState["btnObsCoordinator"];
        if (this.ViewState["BtnNewKardan"] != null)
            this.BtnNewKardan.Visible = this.BtnNewKardan2.Visible = (bool)this.ViewState["BtnNewKardan"];
        if (this.ViewState["btnInDeleteObsSelect"] != null)
            this.btnInDeleteObsSelect.Visible = this.btnInDeleteObsSelect2.Visible = (bool)this.ViewState["btnInDeleteObsSelect"];



    }

    /*************************************************************************************************************/
    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        string PageMode = PgMode.Value;
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        PrjMainMenu MainMenu = new PrjMainMenu("Observers", Convert.ToInt32(ProjectId));
        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, Utility.EncryptQS(_CurrentPrjReId.ToString()), PageMode, GrdFlt, SrchFlt));
    }

    /*************************************************************************************************************/
    protected void GridViewObserver_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;

        if (e.GetValue("PrjReId") == null)
            return;
        if (_CurrentPrjReId == Convert.ToInt32(e.GetValue("PrjReId")))
        {
            e.Row.BackColor = System.Drawing.Color.LightGray;
        }

    }

    protected void GridViewObserver_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "No":
                e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";
                break;

            case "CreateDate":
                e.Editor.Style["direction"] = "ltr";
                break;

            case "InActiveDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    #region btn Click
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }
    protected void BtnNewKardan_Click(object sender, EventArgs e)
    {
        NextPage("NewKardan");

    }
    protected void btnSelectObservers_Click(object sender, EventArgs e)
    {

        if (!CheckAccountingConditions())
        {
            SetMessage("قبل از ارجاع ناظر در این درخواست باید حق الزحمه پرداخت شده باشد");
            return;
        }

        TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager ProjectObserverSelectedManager = new TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager();
        ProjectObserverSelectedManager.SearchObserverSelected(-1, Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value)), _CurrentPrjReId);
        if (ProjectObserverSelectedManager.Count > 0)
        {
            SetMessage("پیشتر به این پروژه ناظر ارجاع داده شده است");
            return;
        }
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "PrjId=" + HDProjectId.Value
               + "&PrjReId=" + Utility.EncryptQS(_CurrentPrjReId.ToString())
               + "&PageMode=" + PgMode.Value
               + "&PgMd=" + Utility.EncryptQS("New")
               + "&GrdFlt=" + GrdFlt
               + "&SrchFlt=" + SrchFlt
               + "&SelObT=" + Utility.EncryptQS("All");
        Response.Redirect("ObserverSelected.aspx?" + QS);
    }
    protected void btnSelectObserversWithLimit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!CheckAccountingConditions())
            {
                SetMessage("قبل از ارجاع ناظر در این درخواست باید حق الزحمه پرداخت شده باشد");
                return;
            }
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();
            string QS = "PrjId=" + HDProjectId.Value
                   + "&PrjReId=" + Utility.EncryptQS(_CurrentPrjReId.ToString())
                   + "&PageMode=" + PgMode.Value
                   + "&PgMd=" + Utility.EncryptQS("New")
                   + "&GrdFlt=" + GrdFlt
                   + "&SrchFlt=" + SrchFlt
                   + "&SelObT=" + Utility.EncryptQS("Limit");
            Response.Redirect("ObserverSelected.aspx?" + QS);
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int PrjObsId = -1;
        int PrjReId = -1;
        int ProjectId = -1;
        Boolean IsPriceArchiveIdNull = false;
        if (GridViewObserver.FocusedRowIndex > -1)
        {
            DataRow row = GridViewObserver.GetDataRow(GridViewObserver.FocusedRowIndex);

            PrjObsId = (int)row["ProjectObserversId"];
            PrjReId = (int)row["PrjReId"];
            ProjectId = (int)row["ProjectId"];

            if (_CurrentPrjReId != PrjReId)
            {
                SetMessage("امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.در صورت تغییر اطلاعات این ناظر ابتدا آن را غیرفعال نموده و سپس ناظر با اطلاعات جدید را ثبت نمایید");
                return;
            }
            TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
            AccountingDetailManager.SelectAccDetailByTableId(PrjObsId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers));
            for (int i = 0; i < AccountingDetailManager.Count; i++)
            {
                if (Convert.ToInt32(AccountingDetailManager[0]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.Payment)
                {
                    if (!Utility.IsDBNullOrNullValue(row["PriceArchiveId"]))
                    {
                        SetMessage("این ناظر دارای فیش پرداخت شده می باشد.در صورت تغییر اطلاعات این ناظر از قبیل متراژ کارکرد وی ابتدا آن را غیرفعال نموده و سپس ناظر با متراژ جدید را ثبت نمایید و در صورت نیاز فیش اختلاف هزینه مربوطه را در سیستم ثبت نمایید ");
                        return;
                    }
                    else
                        IsPriceArchiveIdNull = true;
                }
            }
        }


        NextPage("Edit", IsPriceArchiveIdNull);
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect("ProjectInsert.aspx?ProjectId=" + HDProjectId.Value
            + "&PageMode=" + PgMode.Value
            + "&PrjReId=" + Utility.EncryptQS(_CurrentPrjReId.ToString())
            + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
    }

    protected void btnAccObs_Click(object sender, EventArgs e)
    {
        TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "&ProjectId=" + HDProjectId.Value +
                "&PrjReId=" + Utility.EncryptQS(_CurrentPrjReId.ToString()) +
                "&IngT=" + Utility.EncryptQS(((int)TSP.DataManager.TSProjectIngridientType.Observer).ToString()) +
                "&PageMode=" + PgMode.Value +
                "&UrlReferrer=" + Utility.EncryptQS("Observers.aspx") +
                "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        Response.Redirect("ProjectAccounting.aspx?" + QS);
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("Project.aspx?PostId=" + HDProjectId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("Project.aspx");
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        InActivObserverWork(false, false);
    }

    protected void btnInDeleteObsSelect_Click(object sender, EventArgs e)
    {
        InActivObserverWork(false, true);

    }
    protected void btnactive_Click(object sender, EventArgs e)
    {

        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager ProjectObserverSelectedManager = new TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TransactionManager.Add(ProjectObserverSelectedManager);
        TransactionManager.Add(ObserverWorkRequestManager);
        TransactionManager.Add(ProjectCapacityDecrementManager);
        TransactionManager.Add(RequestInActivesManager);
        try
        {
            string CurrentCapacityAssignmentYear = "";
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            if (_AgentId == Utility.GetCurrentAgentCode())
                CapacityAssignmentManager.SelectCurrentYearAndStage(1);
            else
                CapacityAssignmentManager.SelectCurrentYearAndStage(0);
            if (CapacityAssignmentManager.Count > 0)
            {
                CurrentCapacityAssignmentYear = CapacityAssignmentManager[0]["Year"].ToString();
            }
            int ProjectObserverSelectedId = -2;
            if (GridViewObserver.FocusedRowIndex < 0)
            {
                SetMessage("ابتدا یک رکورد را انتخاب نمایید.");
                return;
            }
            DataRow row = GridViewObserver.GetDataRow(GridViewObserver.FocusedRowIndex);
            if (Convert.ToInt32(row["InActives"]) == 0)
            {
                SetMessage("ناظر انتخاب شده فعال می باشد.");
                return;
            }
            ProjectObserverSelectedId = Utility.IsDBNullOrNullValue(row["ProjectObserverSelectedId"]) ? -2 : (int)row["ProjectObserverSelectedId"];
            string DecreamentYear = Utility.IsDBNullOrNullValue(row["Year"]) ? "" : row["Year"].ToString();
            int PrjObsId = (int)row["ProjectObserversId"];
            int PrjReId = (int)row["PrjReId"];
            int ProjectId = (int)row["ProjectId"];
            int CapacityDecrement = Convert.ToInt32(row["CapacityDecrement"]);
            int MeId = Convert.ToInt32(row["ObsMeId"]);
            int MemberTypeId = Convert.ToInt32(row["MemberTypeId"]);

            TransactionManager.BeginSave();
            int ResultDel = TSP.DataManager.RequestInActivesManager.DeleteRequestInActive(_CurrentPrjReId, PrjObsId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers), RequestInActivesManager);
            if (ResultDel == 1)
            {
                SetMessage("رکورد فعال می باشد و یا در درخواست های قبل غیر فعال شده است");
                TransactionManager.CancelSave();
                return;
            }
            if (ResultDel == 2)
            {
                SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                SetMessage("رکورد فعال می باشد و یا در درخواست های قبل غیر فعال شده است");
                TransactionManager.CancelSave();
                return;
            }
            if (ResultDel != 0)
            {
                SetMessage("خطا در ذخیره ایجاد شده است.");
                TransactionManager.CancelSave();
                return;
            }
            ProjectObserverSelectedManager.FindByCode(ProjectObserverSelectedId);
            if (ProjectObserverSelectedManager.Count != 0)
            {
                ProjectObserverSelectedManager[0].BeginEdit();
                ProjectObserverSelectedManager[0]["IsObserverConfirmed"] = (int)TSP.DataManager.TSProjectObserverSelectedConfirmType.SaveInfo;
                ProjectObserverSelectedManager[0]["RejectDate"] = "";
                ProjectObserverSelectedManager[0].EndEdit();
                if (ProjectObserverSelectedManager.Save() <= 0)
                {
                    SetMessage("خطا در ذخیره ایجاد شده است.");
                    TransactionManager.CancelSave();
                    return;
                }
            }

            ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(ProjectId, PrjObsId, (int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer);
            if (ProjectCapacityDecrementManager.Count != 1)
            {
                SetMessage("خطا در بازیابی اطلاعات ایجاد شده است.");
                TransactionManager.CancelSave();
                return;
            }
            ProjectCapacityDecrementManager[0].BeginEdit();
            ProjectCapacityDecrementManager[0]["IsFine"] = 0;
            ProjectCapacityDecrementManager[0]["FineExpireDate"] = "";
            if (string.Compare(CurrentCapacityAssignmentYear, DecreamentYear) <= 0)//اگر سال کاری کوچکتر از سال کاری جاری باشد.ظرفیت ها از امسال کسر نمی شود و فقط ثبت می شود
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
                int Result = CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, ProjectCapacityDecrementManager, MeId, Utility.GetCurrentUser_UserId(), ProjectId, _CitId, Convert.ToBoolean(_IsCharity), TSP.DataManager.TSProjectIngridientType.Observer, null, false, false);
                if (Result < 0)
                {
                    SetMessage("خطا در ذخیره ایجاد شده است.");
                    TransactionManager.CancelSave();
                    return;
                }

            }
            TransactionManager.EndSave();
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            GridViewObserver.DataBind();
        }
        catch (Exception ex)
        {
            TransactionManager.CancelSave();
            SetMessage("خطا در ذخیره اطلاعات ایجاد شده است");
            Utility.SaveWebsiteError(ex);
        }
    }

    protected void btnRejectObsWork_Click(object sender, EventArgs e)
    {
        InActivObserverWork(true, false);
    }
    #endregion

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int ProjectReTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest);
        //  int WfCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        int WfCode = -1;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dt = WorkFlowStateManager.SelectLastState(ProjectReTableType, _CurrentPrjReId);
        if (dt.Rows.Count > 0)
            WfCode = Convert.ToInt32(dt.Rows[0]["WorkFlowCode"]);

        if (WfCode == -1)
        {
            WFUserControl.SetMsgText("گردش کار درخواست جاری نامشخص است");
            return;
        }

        string Qs = "~/Employee/TechnicalServices/Project/Observers.aspx?" + "ProjectId=" + HDProjectId.Value
           + "&PrjReId=" + Utility.EncryptQS(_CurrentPrjReId.ToString())
           + "&PageMode=" + PgMode.Value
           + "&GrdFlt=" + Request.QueryString["GrdFlt"].ToString()
           + "&SrchFlt=" + Request.QueryString["SrchFlt"].ToString();

        WFUserControl.QueryStringForRedirect = Qs;
        WFUserControl.PerformCallback(_CurrentPrjReId, ProjectReTableType, WfCode, e);
    }
    protected void btnObsCoordinator_Click(object sender, EventArgs e)
    {
        SetCoordinator(true);
    }

    protected void btnCancelObsCoordinator_Click(object sender, EventArgs e)
    {
        SetCoordinator(false);
    }
    #endregion

    #region Method
    /*************************************************************************************************************/
    private void SetKeys()
    {
        try
        {
            HDProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"].ToString());
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();
            _CurrentPrjReId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString()));
            string ProjectId = Utility.DecryptQS(HDProjectId.Value);
            string PageMode = Utility.DecryptQS(PgMode.Value);


            if (string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            ObjectDataSourceObserver.SelectParameters[0].DefaultValue = ProjectId;
            ObjectDataSourceObserver.SelectParameters[1].DefaultValue = _CurrentPrjReId.ToString();

            FillProjectInfo(_CurrentPrjReId);
            SetProjectMainMenuEnabled();

            CheckWorkFlowPermission();
            if (!_CanEditProjectInfoInThisRequest)
                btnAccObs.Enabled = btnAccObs2.Enabled = btnactive.Enabled = btnactive2.Enabled =
                    btnCancelObsCoordinator.Enabled = btnCancelObsCoordinator1.Enabled = btnEdit.Enabled = btnEdit2.Enabled =
                    btnInActive.Enabled = btnInActive2.Enabled = btnInDeleteObsSelect.Enabled = btnInDeleteObsSelect2.Enabled =
                    BtnNew.Enabled = btnNew2.Enabled = BtnNewKardan.Enabled = BtnNewKardan2.Enabled = BtnNew.Enabled = btnNew2.Enabled =
                    btnObsCoordinator.Enabled = btnObsCoordinator2.Enabled = btnRejectObsWork.Enabled = btnRejectObsWork2.Enabled =
                    btnSelectObservers.Enabled = btnSelectObservers2.Enabled = btnSelectObserversWithLimit1.Enabled = btnSelectObserversWithLimit2.Enabled =btnView.Enabled=btnView2.Enabled= false;
        }
        catch (Exception Err)
        {
            Utility.SaveWebsiteError(Err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }

    private void NextPage(string Mode, Boolean EditJustPriceArchive = false)
    {
        int PrjObsId = -1;
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        if (GridViewObserver.FocusedRowIndex > -1)
        {
            DataRow row = GridViewObserver.GetDataRow(GridViewObserver.FocusedRowIndex);
            PrjObsId = (int)row["ProjectObserversId"];

        }
        if (PrjObsId == -1 && Mode != "New" && Mode != "NewKardan")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            string QS = "ProjectId=" + HDProjectId.Value
                + "&PrjObsId=" + Utility.EncryptQS(PrjObsId.ToString())
                + "&PageMode2=" + PgMode.Value
                + "&PageMode=" + Utility.EncryptQS(Mode)
                + "&PrjReId=" + Utility.EncryptQS(_CurrentPrjReId.ToString())
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
            if (EditJustPriceArchive)
                QS += "&PrAId=" + Utility.EncryptQS("JustPrArcId");
            Response.Redirect("ObserverInsert.aspx?" + QS);
        }
    }

    #region Inactive-Delete
    private void InActivObserverWork(Boolean WorkReject, Boolean DeleteObsSelect)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager ProjectObserverSelectedManager = new TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TransactionManager.Add(ProjectObserverSelectedManager);
        TransactionManager.Add(ObserverWorkRequestManager);
        TransactionManager.Add(ProjectCapacityDecrementManager);
        TransactionManager.Add(RequestInActivesManager);

        int PrjObsId = -1;
        int PrjReId = -1;
        int ProjectId = -1;
        if (GridViewObserver.FocusedRowIndex < 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        DataRow row = GridViewObserver.GetDataRow(GridViewObserver.FocusedRowIndex);
        if (Convert.ToInt32(row["InActives"]) == 1)
        {
            SetMessage("ناظر انتخاب شده غیر فعال می باشد.");
            return;
        }
        string DesCription = WorkReject ? " رد کار ارجاع توسط سازمان" : "غیرفعال شدن ناظر ";
        PrjObsId = (int)row["ProjectObserversId"];
        PrjReId = (int)row["PrjReId"];
        ProjectId = (int)row["ProjectId"];
        int CapacityDecrement =Utility.IsDBNullOrNullValue(row["CapacityDecrement"])?0:(int)row["CapacityDecrement"];
        int IsObserverConfirmed = (int)row["IsObserverConfirmed"];
        int MeId = Convert.ToInt32(row["ObsMeId"]);
        int MemberTypeId = Convert.ToInt32(row["MemberTypeId"]);
        DesCription += " با کد عضویت" + MeId.ToString() + " متراژ کسر ظرفیت" + CapacityDecrement.ToString() + " متراژ دستمزد" + row["Wage"].ToString() + "سال کاری " + (Utility.IsDBNullOrNullValue(row["Year"]) ? "نامشخص" : row["Year"].ToString());
        if (!WorkReject)
        {
            if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Member && IsObserverConfirmed != (int)TSP.DataManager.TSProjectObserverSelectedConfirmType.SaveInfo)
            {
                SetMessage("به دلیل تغییر وضعیت ارجاع توسط ناظر مربوطه امکان غیرفعال کردن اطلاعات وجود ندارد");
                return;
            }
        }
        TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager AccountingDocumentDetailManager = new TSP.DataManager.TechnicalServices.AccountingDocumentDetailManager();
        AccountingDocumentDetailManager.FindByObsever(PrjObsId);
        if (AccountingDocumentDetailManager.Count > 0)
        {
            SetMessage("امکان غیرفعال و رد کار برای ناظر انتخاب شده وجود ندارد.نام این ناظر در لیست شماره " + AccountingDocumentDetailManager[0]["ListNo"].ToString() + " حق الزحمه ناظرین،جهت پرداخت حق الزحمه وی به واحد مالی ارائه شده است.");
            return;
        }

        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
        AccountingDetailManager.SelectTSAccountingDetailForTsProjectObserver(PrjObsId, ProjectId);
        if (AccountingDetailManager.Count > 0)
        {
            SetMessage("امکان حذف/غیرفعال/رد کار برای ناظر انتخاب شده وجود ندارد.نام ناظر انتخاب شده در فیش ثبت شده جهت حق الزحمه ناظران می باشد");
            return;
        }
        if (!WorkReject && PrjReId == _CurrentPrjReId)
        {
            if (!DeleteObsSelect)
            {
                int ProjectObserverSelectedId = -2;
                ProjectObserverSelectedId = Utility.IsDBNullOrNullValue(row["ProjectObserverSelectedId"]) ? -2 : (int)row["ProjectObserverSelectedId"];
                ProjectObserverSelectedManager.FindByCode(ProjectObserverSelectedId);
                if (ProjectObserverSelectedManager.Count > 0)
                {
                    SetMessage("تنها امکان حذف کارهای خارج از ارجاع  وجود دارد.");
                    return;
                }
            }
            Delete(PrjObsId, PrjReId, ProjectId);
        }
        else
        {
            #region InActive OR Reject
            try
            {


                TransactionManager.BeginSave();
                if (WorkReject)
                {
                    int ProjectObserverSelectedId = -2;
                    ProjectObserverSelectedId = Utility.IsDBNullOrNullValue(row["ProjectObserverSelectedId"]) ? -2 : (int)row["ProjectObserverSelectedId"];
                    ProjectObserverSelectedManager.FindByCode(ProjectObserverSelectedId);
                    if (ProjectObserverSelectedManager.Count == 0)
                    {
                        SetMessage("تنها امکان رد کار کارهای ارجاع داده شده وجود دارد.");
                        TransactionManager.CancelSave();
                        return;
                    }
                    ProjectObserverSelectedManager[0].BeginEdit();
                    ProjectObserverSelectedManager[0]["IsObserverConfirmed"] = (int)TSP.DataManager.TSProjectObserverSelectedConfirmType.RejectWorkByNezam;
                    ProjectObserverSelectedManager[0]["RejectDate"] = Utility.GetDateOfToday();
                    ProjectObserverSelectedManager[0].EndEdit();
                    if (ProjectObserverSelectedManager.Save() <= 0)
                    {
                        SetMessage("خطا در ذخیره ایجاد شده است.");
                        TransactionManager.CancelSave();
                        return;
                    }
                }
                InsertInActive(PrjObsId, _CurrentPrjReId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest), RequestInActivesManager);

                ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(ProjectId, PrjObsId, (int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer);
                if (ProjectCapacityDecrementManager.Count != 1)
                {
                    SetMessage("خطا در بازیابی اطلاعات ایجاد شده است.");
                    TransactionManager.CancelSave();
                    return;
                }
                ProjectCapacityDecrementManager[0].BeginEdit();
                int _CountRejectByObs = 0;
                if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Member)
                {
                    ObserverWorkRequestManager.FindByMeId(MeId);
                    if (ObserverWorkRequestManager.Count != 0)
                    {
                        _CountRejectByObs = Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["CountRejectByObs"]) ? 0 : Convert.ToInt32(ObserverWorkRequestManager[0]["CountRejectByObs"]);

                    }
                }
                if (_CountRejectByObs >= 1)
                {
                    Utility.Date objDateFree = new Utility.Date(Utility.GetDateOfToday());
                    string FineExpireDate = objDateFree.AddMonths(6);
                    ProjectCapacityDecrementManager[0]["IsFine"] = 1;
                    ProjectCapacityDecrementManager[0]["FineExpireDate"] = FineExpireDate;
                }
                ProjectCapacityDecrementManager[0]["IsFree"] = 1;
                ProjectCapacityDecrementManager[0]["FreeDate"] = Utility.GetDateOfToday();
                ProjectCapacityDecrementManager[0]["IsWorkFree"] = 1;
                ProjectCapacityDecrementManager[0]["WorkFreeDate"] = Utility.GetDateOfToday();
                ProjectCapacityDecrementManager[0].EndEdit();
                ProjectCapacityDecrementManager.Save();
                ProjectCapacityDecrementManager.DataTable.AcceptChanges();
                if (MemberTypeId == (int)TSP.DataManager.TSMemberType.Member)
                {
                    CapacityCalculations CapacityCalculations = new CapacityCalculations();
                    if (ObserverWorkRequestManager.Count != 0)
                    {
                        int Result = CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, ProjectCapacityDecrementManager, MeId, Utility.GetCurrentUser_UserId(), ProjectId, _CitId, Convert.ToBoolean(_IsCharity), TSP.DataManager.TSProjectIngridientType.Observer, null, false, WorkReject);
                        if (Result < 0)
                        {
                            SetMessage("خطا در ذخیره ایجاد شده است.");
                            TransactionManager.CancelSave();
                            return;
                        }
                    }
                }
                TransactionManager.EndSave();
                InsertWorkFlowInActiveLog((int)TSP.DataManager.WorkFlows.TSProjectConfirming, _CurrentPrjReId, DesCription);
                SetMessage("ذخیره انجام شد.");
            }
            catch (Exception ex)
            {
                SetMessage("خطا در ذخیره ایجاد شده است.");
                TransactionManager.CancelSave();
                Utility.SaveWebsiteError(ex);
            }
            GridViewObserver.DataBind();
            #endregion
        }


    }
    private void InActive(int PrjObsId)//, int PrjReId, int ProjectId)
    {
        TSP.DataManager.TechnicalServices.Project_ObserversManager ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
        trans.Add(ObserversManager);
        trans.Add(Manager);
        ObserversManager.FindByProjectObserversId(PrjObsId);
        try
        {

            if (ObserversManager.Count > 0)
            {
                InsertInActive(PrjObsId, _CurrentPrjReId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest), Manager);

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد.";

                GridViewObserver.DataBind();
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
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    protected void InsertInActive(int TableId, int ReqId, int TableType, int ReTableType, TSP.DataManager.RequestInActivesManager Manager)
    {
        DataRow dr = Manager.NewRow();
        dr["TableId"] = TableId;
        dr["TableType"] = TableType;
        dr["ReqId"] = ReqId;
        dr["ReqType"] = ReTableType;
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        if (Manager.Save() > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد.";
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است.";
        }
    }

    private void Delete(int PrjObsId, int PrjReId, int ProjectId)
    {
        TSP.DataManager.TechnicalServices.Project_ObserversManager ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager = new TSP.DataManager.TechnicalServices.AccountingDetailManager();
        TSP.DataManager.TechnicalServices.AccountingDetailManager AccountingDetailManager2 = new TSP.DataManager.TechnicalServices.AccountingDetailManager();

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        trans.Add(ObserversManager);
        trans.Add(AccountingManager);
        trans.Add(ProjectCapacityDecrementManager);
        trans.Add(AccountingDetailManager);
        trans.Add(AccountingDetailManager2);

        try
        {
            string DesCription = "حذف ناظر ";
            string CurrentCapacityAssignmentYear = "";
            TSP.DataManager.TechnicalServices.CapacityAssignmentManager CapacityAssignmentManager = new TSP.DataManager.TechnicalServices.CapacityAssignmentManager();
            if (_AgentId == Utility.GetCurrentAgentCode())
                CapacityAssignmentManager.SelectCurrentYearAndStage(1);
            else
                CapacityAssignmentManager.SelectCurrentYearAndStage(0);
            if (CapacityAssignmentManager.Count > 0)
            {
                CurrentCapacityAssignmentYear = CapacityAssignmentManager[0]["Year"].ToString();
            }
            trans.BeginSave();
            ObserversManager.FindByProjectObserversId(PrjObsId);
            if (ObserversManager.Count != 1)
            {
                SetMessage("اطلاعات توسط کاربر دیگری تغییر یافته است");
                trans.CancelSave();
                return;
            }

            DataTable dtAccDetail = AccountingDetailManager.SelectTSAccountingDetailForTsProjectObserver(PrjObsId, ProjectId);
            int AccDetailCount = dtAccDetail.Rows.Count;
            for (int i = 0; i < AccDetailCount; i++)
            {
                int AccId = Convert.ToInt32(dtAccDetail.Rows[i]["AccountingId"]);
                int AccDetailId = Convert.ToInt32(dtAccDetail.Rows[i]["AccDetailId"]);
                AccountingDetailManager.FindById(AccDetailId);
                if (AccountingDetailManager.Count == 1)
                {
                    AccountingDetailManager[0].Delete();
                    AccountingDetailManager.Save();
                    AccountingDetailManager.DataTable.AcceptChanges();
                }

                AccountingDetailManager2.FindByAccountingId(AccId);
                if (AccountingDetailManager2.Count == 0)
                {
                    AccountingManager.FindByAccountingId(AccId);
                    if (AccountingManager.Count != 1)
                    {
                        SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                        trans.CancelSave();
                        return;
                    }
                    DesCription += "دارای فیش به مبلغ " + (Utility.IsDBNullOrNullValue(AccountingManager[0]["Amount"]) ? " نامشخص" : AccountingDetailManager[0]["Amount"].ToString()) + " با وضعیت" +
                        (Convert.ToInt32(AccountingManager[0]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.Payment ? "پرداخت شده " : " عدم پرداخت ");
                    if (Convert.ToInt32(AccountingManager[0]["Status"]) != (int)TSP.DataManager.TSAccountingStatus.Payment)
                    {
                        AccountingManager[0].Delete();
                        AccountingManager.Save();
                        AccountingManager.DataTable.AcceptChanges();
                    }
                }
            }

            ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(ProjectId, PrjObsId, (int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer);
            string DecreamentDate = (ProjectCapacityDecrementManager.Count==0 || Utility.IsDBNullOrNullValue(ProjectCapacityDecrementManager[0]["Year"]))?"": ProjectCapacityDecrementManager[0]["Year"].ToString();
            int CapacityDecrement =Utility.IsDBNullOrNullValue(ObserversManager[0]["CapacityDecrement"])?0: Convert.ToInt32(ObserversManager[0]["CapacityDecrement"]);
            int MeOfficeOthPEngOId = Convert.ToInt32(ObserversManager[0]["MeOfficeOthPEngOId"]);
            int lendec = ProjectCapacityDecrementManager.Count;
            for (int i = 0; i < lendec; i++)
                ProjectCapacityDecrementManager[0].Delete();
            ProjectCapacityDecrementManager.Save();
            ProjectCapacityDecrementManager.DataTable.AcceptChanges();


            DesCription += "با کد عضویت " + Convert.ToInt32(ObserversManager[0]["MeOfficeOthPEngOId"]).ToString() + "متراژ  کارکرد" + CapacityDecrement + " متراژ دستمزد" + ( Utility.IsDBNullOrNullValue(ObserversManager[0]["Wage"]) ? "0" : Convert.ToInt32(ObserversManager[0]["Wage"]).ToString() )+ " سال کاری" + DecreamentDate;
            if (Utility.WorkBasedOnWorkRequest() && Convert.ToInt32(ObserversManager[0]["MemberTypeId"]) == (int)TSP.DataManager.TSMemberType.Member)
            {

                if (string.Compare(CurrentCapacityAssignmentYear, DecreamentDate) <= 0)//اگر سال کاری کوچکتر از سال کاری جاری باشد.ظرفیت ها از امسال کسر نمی شود و فقط ثبت می شود
                {
                    TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
                    trans.Add(ObserverWorkRequestManager);
                    CapacityCalculations CapacityCalculations = new CapacityCalculations();
                    CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, ProjectCapacityDecrementManager, MeOfficeOthPEngOId, Utility.GetCurrentUser_UserId(), ProjectId, _CitId, Convert.ToBoolean(_IsCharity), TSP.DataManager.TSProjectIngridientType.Observer, null, false, false);
                }
                TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager ProjectObserverSelectedManager = new TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager();
                trans.Add(ProjectObserverSelectedManager);
                ProjectObserverSelectedManager.SearchObserverSelected(Convert.ToInt32(ObserversManager[0]["MeOfficeOthPEngOId"]), ProjectId, PrjReId);
                if (ProjectObserverSelectedManager.Count > 0)
                {

                    //trans.CancelSave();
                    //SetMessage("ثبت این ناظر از طریق ارجاع کار بوده است.لطفا از گزینه رد کار استفاده نمایید");
                    //return;
                    if (ProjectObserverSelectedManager.Count == 1)
                    {
                        ProjectObserverSelectedManager[0].Delete();
                        ProjectObserverSelectedManager.Save();
                    }
                }
            }

            ObserversManager[0].Delete();
            ObserversManager.Save();
            trans.EndSave();
            InsertWorkFlowInActiveLog((int)TSP.DataManager.WorkFlows.TSProjectConfirming, PrjReId, DesCription);
            GridViewObserver.DataBind();
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInDelete));
        }

    }
    #endregion

    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
        _IsCharity = prjInfo.IsCharity;
        _CitId = prjInfo.CitId;
        _Foundation = prjInfo.Foundation;
        _GroupId = prjInfo.GroupId;
        _PrjReTypeId = prjInfo.PrjReTypeId;
        _FundationDifference = prjInfo.FundationDifference;
        _AgentId = prjInfo.AgentId;
        _IsPopulationUnder25000 = prjInfo.IsPopulationUnder25000;
        _CanEditProjectInfoInThisRequest = prjInfo.CanEditProjectInfoInThisRequest;
    }

    private void SetProjectMainMenuEnabled()
    {
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        if (ProjectId == "-1")
            ProjectId = "-2";

        PrjMainMenu PrjMainMenu = new PrjMainMenu("Observers", Convert.ToInt32(ProjectId));
        MainMenu.Items.FindByName("Observers").Selected = true; //Observers
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
        MainMenu.Items.FindByName("Designer").Enabled = PrjMainMenu.GetEnabled("Designer");
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private void SetObserverSharePayement(Boolean IsPayed, int ProjectObserverId)
    {
        TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        ProjectObserversManager.FindByProjectObserversId(ProjectObserverId);
        if (ProjectObserversManager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        ProjectObserversManager[0].BeginEdit();
        ProjectObserversManager[0]["Payed"] = IsPayed;
        if (IsPayed)
            ProjectObserversManager[0]["PayeDate"] = Utility.GetDateOfToday();
        else
            ProjectObserversManager[0]["PayeDate"] = DBNull.Value;
        ProjectObserversManager[0].EndEdit();
        ProjectObserversManager.Save();
        SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
        GridViewObserver.DataBind();
    }
    /*************************************************** WorkFlow ************************************************/
    #region WorkFlow Methods

    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableType
        int ProjectWFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject;

        int ChangeWFCode = (int)TSP.DataManager.WorkFlows.TSObserverChangesConfirming;
        int ChangeTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveChangeProjectObserverRequestInfo;

        TSP.DataManager.WFPermission SaveWFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(SaveTaskCode, ProjectWFCode, _CurrentPrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission ChangeWFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ChangeTaskCode, ChangeWFCode, _CurrentPrjReId, Utility.GetCurrentUser_UserId());

        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = SaveWFPer.BtnEdit || ChangeWFPer.BtnEdit;
        this.ViewState["BtnInActive"] = btnactive.Enabled = btnactive2.Enabled = btnInActive.Enabled = btnInActive2.Enabled = SaveWFPer.BtnInactive || ChangeWFPer.BtnInactive;
        this.ViewState["BtnNew"] = btnSelectObserversWithLimit2.Visible = btnSelectObserversWithLimit1.Visible = btnSelectObservers.Visible = btnSelectObservers2.Visible = BtnNew.Enabled = btnNew2.Enabled = SaveWFPer.BtnNew || ChangeWFPer.BtnNew;
        if (BtnNewKardan.Visible)
            BtnNewKardan.Enabled = BtnNewKardan2.Enabled = SaveWFPer.BtnNew || ChangeWFPer.BtnNew;
        if (btnObsCoordinator.Visible)
            btnObsCoordinator.Enabled = btnObsCoordinator2.Enabled = SaveWFPer.BtnNew || ChangeWFPer.BtnNew;
        if (btnRejectObsWork.Visible)
            btnRejectObsWork.Enabled = btnRejectObsWork2.Enabled = SaveWFPer.BtnNew || ChangeWFPer.BtnNew;

    }

    private bool CheckAccountingConditions()
    {
        if (Convert.ToBoolean(_IsCharity)) return true;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers);
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        string AccTypeList = ((int)TSP.DataManager.TSAccountingAccType.ObserversFiche).ToString() + "," + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure).ToString() + "," + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation).ToString();
        int ProjectId = -1;
        int TableTypeId = -1;
        if (_PrjReTypeId==(int)TSP.DataManager.TSProjectRequestType.AdditionalStageRequest || _PrjReTypeId == (int)TSP.DataManager.TSProjectRequestType.IncreaseBuildingAreaRequest)//اگر اضافه اشکوب یا افزایش متراژ بود فقط فیش پرداخت شده درخواست جاری مورد قبول است
        {
            ProjectId = -1;
            TableTypeId = _CurrentPrjReId;
        }
        else
        {
            ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
            TableTypeId = -1;
        }
        DataTable dt = AccountingManager.SelectExistAccountingByAccTypeList(TableTypeId, TableType, ProjectId, AccTypeList);
        if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["Status"]) == (int)TSP.DataManager.TSAccountingStatus.Payment)
        {
            return true;
        }
        return false;
    }

    private void InsertWorkFlowInActiveLog(int WorkFlowCode, int TableId, string DesCription)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowSaveLogForInActiveInfo(WorkFlowCode, TableId, DesCription, Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
            {
                SetMessage("خطایی در اطلاعات انجام گرفته است");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    SetMessage("اطلاعات تکراری می باشد");
                }
                else
                {
                    SetMessage("خطایی در ذخیره انجام گرفته است");
                }
            }
            else
            {
                SetMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
    }

    private void SetCoordinator(Boolean IsCoordinator)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        try
        {
            if (GridViewObserver.FocusedRowIndex < 0)
            {
                SetMessage("ابتدا یک رکورد را انتخاب نمایید.");
                return;
            }
            DataRow row = GridViewObserver.GetDataRow(GridViewObserver.FocusedRowIndex);
            if (IsCoordinator)
            {
                if (Convert.ToInt32(row["InActives"]) == 1)
                {
                    SetMessage("امکان انتخاب ناظر غیرفعال به عنوان ناظر هماهنگ کننده وجود ندارد");
                    return;
                }
                if (Convert.ToInt32(row["ObserversTypeId"]) != (int)TSP.DataManager.TSObserversType.Sazeh
                    && Convert.ToInt32(row["ObserversTypeId"]) != (int)TSP.DataManager.TSObserversType.Memari)
                {
                    SetMessage("ناظر هماهنگ کننده بایستی از بین ناظران سازه یا معماری انتخاب شود");
                    return;
                }
            }
            int PrjObsId = (int)row["ProjectObserversId"];
            int PrjReId = (int)row["PrjReId"];
            int ProjectId = (int)row["ProjectId"];
            int CapacityDecrement = (int)row["CapacityDecrement"];
            int Wage = (int)row["Wage"];

            TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
            TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObsManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
            TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager CapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
            trans.Add(ProjectObsManager);
            trans.Add(Project_ObserversManager);
            trans.Add(CapacityDecrementManager);

            trans.BeginSave();

            Project_ObserversManager.SetObserverCoordinator(ProjectId, PrjObsId, IsCoordinator);
            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            ProjectRequestManager.UpdateObserverPriceByObserverId(PrjObsId, trans, ProjectObsManager);
            trans.EndSave();
            InsertWorkFlowInActiveLog((int)TSP.DataManager.WorkFlows.TSProjectConfirming, _CurrentPrjReId, "انتخاب ناظر هماهنگ کننده");
            SetMessage("ذخیره انجام شد");
            GridViewObserver.DataBind();

        }
        catch (Exception ex)
        {

            trans.CancelSave();
            Utility.SaveWebsiteError(ex);
        }
    }
    #endregion
    #endregion

}

