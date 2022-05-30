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

public partial class Employee_TechnicalServices_Project_Block : System.Web.UI.Page
{
    string ProjectId;
    string PrjReId;
    string PageMode;
    private Boolean _CanEditProjectInfoInThisRequest
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldPage["CanEditProjectInfoInThisRequest"]);
        }
        set
        {
            HiddenFieldPage["CanEditProjectInfoInThisRequest"] = value.ToString();
        }
    }
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["SendBackDataTable_EmpPrjBlock"] = "";
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.BlockManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnPrint.Enabled = per.CanView;
            btnPrint2.Enabled = per.CanView;

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["PrjReId"])) || (string.IsNullOrEmpty(Request.QueryString["ProjectId"])))
            {
                Response.Redirect("Project.aspx");
                return;
            }

            if (!per.CanView)
                GridViewBlock.Visible = false;

            SetKey();
            SetProjectMenuEnabled();
            SetProjectMainMenuEnabled();

            CheckWorkFlowPermission();

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnPrint"] = btnPrint.Enabled;
            if (SetControlEnableForProjectIngrediants(int.Parse(PrjReId)))
            {
                this.ViewState["BtnEdit"] =
                this.ViewState["BtnDelete"] =
                this.ViewState["BtnNew"] = false;
            }

        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnPrint"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["BtnPrint"];


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        Session["DataTable"] = GridViewBlock.Columns;
        Session["DataSource"] = ObjectDataSourceBlock;
        Session["Title"] = "بلوک ها";
        Session["Header"] = GetRepHeader();
    }

    #region btn Click
    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Print();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int BlockId = -1;
        if (GridViewBlock.FocusedRowIndex > -1)
        {
            DataRow row = GridViewBlock.GetDataRow(GridViewBlock.FocusedRowIndex);
            BlockId = (int)row["BlockId"];
        }

        if (BlockId == -1)
        {
            SetLabelWarning("لطفاًابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            DeleteBlock(BlockId);
            UpdateDefualtPlanMethods();
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Utility.DecryptQS(PkProjectId.Value) == "-1")
            Response.Redirect("Project.aspx");

        string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
        string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
        string Qs = "ProjectId=" + PkProjectId.Value + "&PrjReId=" + PkPrjReId.Value + "&PageMode=" + PgMode.Value
          + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        Response.Redirect("ProjectInsert.aspx?" + Qs);
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("Project.aspx?PostId=" + PkProjectId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("Project.aspx");
        }
    }
    #endregion

    #region Menu Click
    /***************************************************** ProjectMainMenu *************************************************************/
    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        PrjReId = PkPrjReId.Value;
        PageMode = PgMode.Value;

        PrjMainMenu MainMenu = new PrjMainMenu("Project", Convert.ToInt32(ProjectId));
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, PrjReId, PageMode, GrdFlt, SrchFlt));
    }

    /******************************************************* ProjectMenu ****************************************************************/
    protected void ProjectMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        PrjReId = PkPrjReId.Value;
        PageMode = PgMode.Value;

        PrjMenu PrjMenu = new PrjMenu("Block", Convert.ToInt32(ProjectId));
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        Response.Redirect(PrjMenu.GetRedirectLink(e.Item.Name, PrjReId, PageMode, GrdFlt, SrchFlt));
    }

    #endregion

    #region Grid

    protected void GridViewBlock_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (PkPrjReId.Value != null)
        {
            string PrjReId = Utility.DecryptQS(PkPrjReId.Value);
            if (e.GetValue("PrjReId") == null)
                return;
            string CurretnPrjReId = e.GetValue("PrjReId").ToString();
            if (PrjReId == CurretnPrjReId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }
    #endregion


    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int ProjectReId = Convert.ToInt32(Utility.DecryptQS(PkPrjReId.Value));
        int ProjectReTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest);
        int WfCode = -1;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dt = WorkFlowStateManager.SelectLastState(ProjectReTableType, ProjectReId);
        if (dt.Rows.Count > 0)
            WfCode = Convert.ToInt32(dt.Rows[0]["WorkFlowCode"]);

        if (WfCode == -1)
        {
            WFUserControl.SetMsgText("گردش کار درخواست جاری نامشخص است");
            return;
        }

        WFUserControl.PerformCallback(ProjectReId, ProjectReTableType, WfCode, e);
    }
    #endregion

    #region Methods
    /***********************************************************************************************************************************/
    private void SetKey()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            PkProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"]).ToString();
            PkPrjReId.Value = Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString();

            PrjReId = Utility.DecryptQS(PkPrjReId.Value);
            ProjectId = Utility.DecryptQS(PkProjectId.Value);
            PageMode = Utility.DecryptQS(PgMode.Value);

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(PrjReId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            if (ProjectId == "-1")
            {
                ProjectMenu.Visible = false;
                MainMenu.Visible = false;
            }


            ObjectDataSourceBlock.SelectParameters[0].DefaultValue = ProjectId;
            ObjectDataSourceBlock.SelectParameters[1].DefaultValue = PrjReId;

            //ObjectDataSourceBlock.SelectParameters[0].DefaultValue = GetPlansMethodId().ToString();
            CheckMenueViewPermission();
            FillProjectInfo(int.Parse(PrjReId));
            if (!Utility.TSProject_IsBasedOnStep())
                CheckCurrentStepsByGroup(int.Parse(PrjReId));
            if (!_CanEditProjectInfoInThisRequest)
                btnDelete.Enabled = btnDelete2.Enabled = btnEdit.Enabled = btnEdit2.Enabled = btnNew.Enabled = btnNew2.Enabled = false;

        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

    }

    /***********************************************************************************************************************************/
    private void NextPage(string Mode)
    {
        int BlockId = -1;
        if (GridViewBlock.FocusedRowIndex > -1 && Mode != "New")
        {
            DataRow row = GridViewBlock.GetDataRow(GridViewBlock.FocusedRowIndex);
            BlockId = (int)row["BlockId"];
        }

        if (BlockId == -1 && Mode != "New")
        {
            SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();
            string QS = "BlockId=" + Utility.EncryptQS(BlockId.ToString())
                + "&PageMode=" + Utility.EncryptQS(Mode)
                + "&ProjectId=" + Server.HtmlDecode(Request.QueryString["ProjectId"])
                + "&PrjReId=" + Server.HtmlDecode(Request.QueryString["PrjReId"])
                + "&MPgMode=" + PgMode.Value
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
            Response.Redirect("BlockInsert.aspx?" + QS);
        }
    }

    private void Print()
    {
        //string FilterExp;
        //FilterExp = GridViewBlock.FilterExpression;

        //Response.Redirect("~/ReportForms/Accounting/BlockReport.aspx?FilterExp=" + Utility.EncryptQS(FilterExp));
    }

    #region Delete
    private void DeleteBlock(int BlockId)
    {
        //TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
        //TSP.DataManager.TechnicalServices.BalconyManager BalconyManager = new TSP.DataManager.TechnicalServices.BalconyManager();
        //TSP.DataManager.TechnicalServices.FoundationManager FoundationManager = new TSP.DataManager.TechnicalServices.FoundationManager();
        //TSP.DataManager.TechnicalServices.WallsManager WallsManager = new TSP.DataManager.TechnicalServices.WallsManager();
        //TSP.DataManager.TechnicalServices.EntranceManager EntranceManager = new TSP.DataManager.TechnicalServices.EntranceManager();
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();
        //transact.Add(BlockManager);
        //transact.Add(BalconyManager);
        //transact.Add(FoundationManager);
        //transact.Add(WallsManager);
        //transact.Add(EntranceManager);

        //BlockManager.FindByBlockId(BlockId);
        //if (BlockManager.Count != 1)
        //{
        //    SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
        //    return;
        //}
        try
        {
            if (TSP.DataManager.TechnicalServices.BlockManager.DeleteBlock(BlockId, transact, true))
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            else
            {
                transact.CancelSave();
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }


            //transact.BeginSave();
            //EntranceManager.FindByBlockId(BlockId);
            //if (EntranceManager.Count > 0)
            //{
            //    int len = EntranceManager.Count;
            //    for (int i = 0; i < len; i++)
            //        EntranceManager[0].Delete();
            //    EntranceManager.Save();
            //    EntranceManager.DataTable.AcceptChanges();
            //}

            //FoundationManager.FindByBlockId(BlockId);
            //if (FoundationManager.Count > 0)
            //{
            //    int len = FoundationManager.Count;
            //    for (int i = 0; i < len; i++)
            //    {
            //        BalconyManager.FindByFoundationId(Convert.ToInt32(FoundationManager[0]["FoundationId"]));
            //        if (BalconyManager.Count > 0)
            //        {
            //            int lenbalcony = BalconyManager.Count;
            //            for (int j = 0; j < lenbalcony; j++)
            //                BalconyManager[0].Delete();
            //            BalconyManager.Save();
            //            BalconyManager.DataTable.AcceptChanges();
            //        }

            //        FoundationManager[0].Delete();
            //        FoundationManager.Save();
            //        FoundationManager.DataTable.AcceptChanges();
            //    }
            //}

            //WallsManager.FindByBlockId(BlockId);
            //if (WallsManager.Count > 0)
            //{
            //    int len = WallsManager.Count;
            //    for (int i = 0; i < len; i++)
            //        WallsManager[0].Delete();
            //    WallsManager.Save();
            //    WallsManager.DataTable.AcceptChanges();
            //}

            //BlockManager[0].Delete();
            //BlockManager.Save();
            //BlockManager.DataTable.AcceptChanges();

            //GridViewBlock.DataBind();
            //SetLabelWarning("حذف انجام شد");
            //transact.EndSave();

        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }
    #endregion

    private string GetRepHeader()
    {
        string AgentName = GetAgentName();
        return "نمایندگی : " + AgentName;
    }

    private string GetAgentName()
    {
        int AgentCode = Utility.GetCurrentUser_AgentId();
        TSP.DataManager.AccountingAgentManager Manager = new TSP.DataManager.AccountingAgentManager();
        Manager.FindByCode(AgentCode);
        if (Manager.Count > 0)
            return Manager[0]["Name"].ToString();
        else
            return "";
    }

    private int GetPlansMethodId()
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);

        if (ProjectId == "-1")
            return -1;

        TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager = new TSP.DataManager.TechnicalServices.PlansMethodManager();
        PlansMethodManager.FindByProjectId(Convert.ToInt32(ProjectId));
        if (PlansMethodManager.Count > 0)
            return Convert.ToInt32(PlansMethodManager[0]["PlansMethodId"]);
        else
            return -1;
    }

    #region Warning Methods
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
                SetLabelWarning("کد تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetLabelWarning("به علت وجود اطلاعات وابسته امکان حذف نمی باشد");
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
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }
    #endregion

    #region SetMenu
    private void SetProjectMainMenuEnabled()
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        if (ProjectId == "-1")
            ProjectId = "-2";

        PrjMainMenu PrjMainMenu = new PrjMainMenu("Project", Convert.ToInt32(ProjectId));
        MainMenu.Items.FindByName("Project").Selected = true; //Project
        //MainMenu.Items.FindByName("StatusAnnouncement").Enabled = PrjMainMenu.GetEnabled("StatusAnnouncement");
        //MainMenu.Items.FindByName("BuildingsLicense").Enabled = PrjMainMenu.GetEnabled("BuildingsLicense");
        //MainMenu.Items.FindByName("Timing").Enabled = PrjMainMenu.GetEnabled("Timing");
        MainMenu.Items.FindByName("Contract").Enabled = PrjMainMenu.GetEnabled("Contract");
        //MainMenu.Items.FindByName("Implementer").Enabled = PrjMainMenu.GetEnabled("Implementer");
        MainMenu.Items.FindByName("Observers").Enabled = PrjMainMenu.GetEnabled("Observers");
        MainMenu.Items.FindByName("Plans").Enabled = PrjMainMenu.GetEnabled("Plans");
        MainMenu.Items.FindByName("Owner").Enabled = PrjMainMenu.GetEnabled("Owner");
        MainMenu.Items.FindByName("Project").Enabled = PrjMainMenu.GetEnabled("Project");
        MainMenu.Items.FindByName("Accounting").Enabled = PrjMainMenu.GetEnabled("Accounting");
        MainMenu.Items.FindByName("Designer").Enabled = PrjMainMenu.GetEnabled("Designer");
    }

    private void SetProjectMenuEnabled()
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        if (ProjectId == "-1")
            ProjectId = "-2";

        PrjMenu PrjMenu = new PrjMenu("Block", Convert.ToInt32(ProjectId));
        ProjectMenu.Items.FindByName("Block").Selected = true; //Insurance
        ProjectMenu.Items.FindByName("Insurance").Enabled = PrjMenu.GetEnabled("Insurance");
        ProjectMenu.Items.FindByName("Block").Enabled = PrjMenu.GetEnabled("Block");
        ProjectMenu.Items.FindByName("PlansMethod").Enabled = PrjMenu.GetEnabled("PlansMethod");
        ProjectMenu.Items.FindByName("RegisteredNo").Enabled = PrjMenu.GetEnabled("RegisteredNo");
        ProjectMenu.Items.FindByName("BaseInfo").Enabled = PrjMenu.GetEnabled("BaseInfo");
    }

    private void CheckMenueViewPermission()
    {
        PrjMenu.ProjectMenusViewPermission PrjMenuPer = PrjMenu.CheckProjectMenusViewPermission();
        ProjectMenu.Items.FindByName("Insurance").Visible = PrjMenuPer.CanViewInsurance;
        ProjectMenu.Items.FindByName("Block").Visible = PrjMenuPer.CanViewBlock;
        ProjectMenu.Items.FindByName("PlansMethod").Visible = PrjMenuPer.CanViewPlansMethod;
        ProjectMenu.Items.FindByName("RegisteredNo").Visible = PrjMenuPer.CanViewRegisteredNo;
        ProjectMenu.Items.FindByName("BaseInfo").Visible = PrjMenuPer.CanViewBaseInfo;

        PrjMainMenu.ProjectMainMenusViewPermission PrjMainMenuPer = PrjMainMenu.CheckProjectMenusViewPermission();
        //MainMenu.Items.FindByName("StatusAnnouncement").Visible = PrjMainMenuPer.CanViewStatusAnnouncement;
        //MainMenu.Items.FindByName("BuildingsLicense").Visible = PrjMainMenuPer.CanViewBuildingsLicense;
        //MainMenu.Items.FindByName("Timing").Visible = PrjMainMenuPer.CanViewTiming;
        MainMenu.Items.FindByName("Contract").Visible = PrjMainMenuPer.CanViewContract;
        //MainMenu.Items.FindByName("Implementer").Visible = PrjMainMenuPer.CanViewImplementer;
        MainMenu.Items.FindByName("Observers").Visible = PrjMainMenuPer.CanViewObservers;
        MainMenu.Items.FindByName("Plans").Visible = PrjMainMenuPer.CanViewPlans;
        MainMenu.Items.FindByName("Owner").Visible = PrjMainMenuPer.CanViewOwner;
        MainMenu.Items.FindByName("Project").Visible = PrjMainMenuPer.CanViewProject;
        MainMenu.Items.FindByName("Accounting").Visible = PrjMainMenuPer.CanViewTSAccounting;
        MainMenu.Items.FindByName("Designer").Visible = PrjMainMenuPer.CanViewDesigner;
    }
    #endregion

    #region WorkFlow Methods
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableId
        PrjReId = Utility.DecryptQS(PkPrjReId.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.TSProjectRequest;
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, Convert.ToInt32(PrjReId), Utility.GetCurrentUser_UserId());
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit;
        this.ViewState["BtnDelete"] = btnDelete.Enabled = btnDelete2.Enabled = WFPer.BtnInactive;
        this.ViewState["BtnNew"] = btnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew;
    }
    #endregion

    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
        _CanEditProjectInfoInThisRequest = prjInfo.CanEditProjectInfoInThisRequest;
    }

    private void UpdateDefualtPlanMethods()
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        PrjReId = Utility.DecryptQS(PkPrjReId.Value);
        if (ProjectId == "-1")
            return;
        if (PrjReId == "-1")
            return;

        // TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
        TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager = new TSP.DataManager.TechnicalServices.PlansMethodManager();

        //   BlockManager.FindByProjectAndPrjReId(int.Parse(ProjectId), int.Parse(PrjReId));

        PlansMethodManager.FindByProjectAndPrjReId(int.Parse(ProjectId), int.Parse(PrjReId));
        if (PlansMethodManager.Count == 1)
        {
            PlansMethodManager[0].BeginEdit();
            PlansMethodManager[0]["BlockNum"] = Convert.ToInt32(PlansMethodManager[0]["BlockNum"]) - 1;
            PlansMethodManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            PlansMethodManager[0]["ModifiedDate"] = DateTime.Now;
            PlansMethodManager[0].EndEdit();
            PlansMethodManager.Save();
        }
    }

    public void CheckCurrentStepsByGroup(int PrjReId)
    {
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
        string msg = BlockManager.CheckCurrentMaxStepByGroup(PrjReId);
        if (msg != null)
        {
            lblWarning.Text = msg;
        }
        else
        {
            lblWarning.Text = "";
        }
    }

    private bool SetControlEnableForProjectIngrediants(int PrjReId)
    {
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        DataTable dt = ProjectRequestManager.SelectTSProjectCountIngrediant(PrjReId);
        if (Convert.ToInt32(dt.Rows[0]["CountIngrediant"]) != 0)
        {
            liCountIngrediant.Visible = true;
            return true;
        }
        else
        {
            liCountIngrediant.Visible = false;
            return false;
        }
    }
    #endregion
}