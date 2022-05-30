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

public partial class Employee_TechnicalServices_Project_BlockInsert : System.Web.UI.Page
{
    string PageMode;
    string ProjectId;
    string PrjReId;
    string BlockId;

    bool IsPageRefresh = false;
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        this.DivReport.Visible = false;

        #region PageRefresh
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
            #region Check Permissions
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.BlockManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;

            TSP.DataManager.Permission perFoundation = TSP.DataManager.TechnicalServices.FoundationManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            RoundPanelFoundation.Visible = perFoundation.CanView;
            btnAdd.Enabled = perFoundation.CanNew;

            TSP.DataManager.Permission perEntrance = TSP.DataManager.TechnicalServices.EntranceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            RoundPanelEntrance.Visible = perEntrance.CanView;
            btnAddEntrance.Enabled = perEntrance.CanNew;
            CustomAspxDevGridViewEntrance.Columns["clnDelete"].Visible = perEntrance.CanEdit || perEntrance.CanDelete;

            TSP.DataManager.Permission perWalls = TSP.DataManager.TechnicalServices.WallsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            RoundPanelWall.Visible = perWalls.CanView;
            btnAddWall.Enabled = perWalls.CanNew;
            CustomAspxDevGridViewWall.Columns["clnDelete"].Visible = perWalls.CanEdit || perWalls.CanDelete;
            #endregion

            if ((string.IsNullOrEmpty(Request.QueryString["MPgMode"])) || (string.IsNullOrEmpty(Request.QueryString["ProjectId"])) || (string.IsNullOrEmpty(Request.QueryString["PrjReId"])))
            {
                Response.Redirect("Project.aspx");
                return;
            }

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["BlockId"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString())) != "New"))
            {
                Response.Redirect("Block.aspx?ProjectId=" + Server.HtmlDecode(Request.QueryString["ProjectId"]) + "&PrjReId=" + Server.HtmlDecode(Request.QueryString["PrjReId"]) + "&PageMode=" + Server.HtmlDecode(Request.QueryString["MPgMode"]));
                return;
            }

            Session["FoundationManager"] = null;
            Session["FoundationManager"] = CreateFoundationManager();

            Session["WallsManager"] = null;
            Session["WallsManager"] = CreateWallsManager();

            Session["EntranceManager"] = null;
            Session["EntranceManager"] = CreateEntranceManager();

            SetKeys();
            FillGrid();
            FillWallsGrid();
            FillEntranceGrid();


            if (!Utility.TSProject_IsBasedOnStep())
                SetLableWarningInfo("توجه : تعداد طبقات در محاسبه هزینه ها و فیش ها تاثیر دارد", true);
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            if (SetControlEnableForProjectIngrediants(int.Parse(PrjReId)))
            {
                this.ViewState["BtnEdit"] =
                this.ViewState["BtnDelete"] =
                this.ViewState["BtnNew"] = false;
            }

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    #region btn Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("New");
        Session["FoundationManager"] = null;
        Session["FoundationManager"] = CreateFoundationManager();

        Session["WallsManager"] = null;
        Session["WallsManager"] = CreateWallsManager();

        Session["EntranceManager"] = null;
        Session["EntranceManager"] = CreateEntranceManager();
        FillGrid();
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
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string Qs = "ProjectId=" + PkProjectId.Value + "&PrjReId=" + PkPrjReId.Value + "&PageMode=" + MPgMode.Value
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        Response.Redirect("Block.aspx?" + Qs);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        RowInserting();
        ASPxTextBoxStageTitle.Focus();
    }

    protected void btnAddWall_Click(object sender, EventArgs e)
    {
        WallsRowInserting();
        ASPxComboBoxWallMainDirections.Focus();
    }

    protected void btnAddEntrance_Click(object sender, EventArgs e)
    {
        EntranceRowInserting();
        ASPxComboBoxEntranceType.Focus();
    }
    #endregion

    #region Grid
    protected void CustomAspxDevGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (Session["FoundationManager"] == null)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return;
        }
        TSP.DataManager.TechnicalServices.FoundationManager FoundationManager = (TSP.DataManager.TechnicalServices.FoundationManager)Session["FoundationManager"];
        DataRow rowFoundation = FoundationManager.DataTable.Rows.Find(e.Keys["FoundationId"]);
        rowFoundation.Delete();

        e.Cancel = true;

        CustomAspxDevGridView1.CancelEdit();

        CustomAspxDevGridView1.DataSource = FoundationManager.DataTable;
        CustomAspxDevGridView1.KeyFieldName = "FoundationId";
        CustomAspxDevGridView1.DataBind();
    }

    protected void CustomAspxDevGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (Session["FoundationManager"] == null)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return;
        }
        TSP.DataManager.TechnicalServices.FoundationManager FoundationManager = (TSP.DataManager.TechnicalServices.FoundationManager)Session["FoundationManager"];
        DataRow rowFoundation = FoundationManager.DataTable.Rows.Find(e.Keys["FoundationId"]);

        rowFoundation.BeginEdit();
        rowFoundation["StageTitle"] = e.NewValues["StageTitle"];
        rowFoundation["EshghalSurface"] = e.NewValues["EshghalSurface"];
        rowFoundation["Area"] = e.NewValues["Area"];
        rowFoundation["Height"] = e.NewValues["Height"];
        rowFoundation["Flat"] = e.NewValues["Flat"];
        rowFoundation["UsageId"] = e.NewValues["UsageId"];
        rowFoundation["SecondaryUsageId"] = e.NewValues["SecondaryUsageId"];
        rowFoundation["CloseYard"] = e.NewValues["CloseYard"];
        rowFoundation["ClosePathway"] = e.NewValues["ClosePathway"];
        rowFoundation["OpenYard"] = e.NewValues["OpenYard"];
        rowFoundation["OpenPathway"] = e.NewValues["OpenPathway"];
        rowFoundation["UserId"] = Utility.GetCurrentUser_UserId();
        rowFoundation["ModifiedDate"] = DateTime.Now;
        rowFoundation.EndEdit();

        e.Cancel = true;

        CustomAspxDevGridView1.CancelEdit();

        CustomAspxDevGridView1.DataSource = FoundationManager.DataTable;
        CustomAspxDevGridView1.KeyFieldName = "FoundationId";
        CustomAspxDevGridView1.DataBind();
    }

    protected void CustomAspxDevGridViewWall_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (Session["WallsManager"] == null)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return;
        }
        TSP.DataManager.TechnicalServices.WallsManager WallsManager = (TSP.DataManager.TechnicalServices.WallsManager)Session["WallsManager"];
        DataRow rowWalls = WallsManager.DataTable.Rows.Find(e.Keys["WallsId"]);
        rowWalls.Delete();

        e.Cancel = true;

        CustomAspxDevGridViewWall.CancelEdit();

        CustomAspxDevGridViewWall.DataSource = WallsManager.DataTable;
        CustomAspxDevGridViewWall.KeyFieldName = "WallsId";
        CustomAspxDevGridViewWall.DataBind();
    }

    protected void CustomAspxDevGridViewWall_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (Session["WallsManager"] == null)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return;
        }
        TSP.DataManager.TechnicalServices.WallsManager WallsManager = (TSP.DataManager.TechnicalServices.WallsManager)Session["WallsManager"];
        DataRow rowWalls = WallsManager.DataTable.Rows.Find(e.Keys["WallsId"]);

        rowWalls.BeginEdit();
        rowWalls["MainDirectionsId"] = e.NewValues["MainDirectionsId"];
        rowWalls["Length"] = e.NewValues["Length"];
        rowWalls["Height"] = e.NewValues["Height"];
        rowWalls["UserId"] = Utility.GetCurrentUser_UserId();
        rowWalls["ModifiedDate"] = DateTime.Now;
        rowWalls.EndEdit();

        e.Cancel = true;

        CustomAspxDevGridViewWall.CancelEdit();

        CustomAspxDevGridViewWall.DataSource = WallsManager.DataTable;
        CustomAspxDevGridViewWall.KeyFieldName = "WallsId";
        CustomAspxDevGridViewWall.DataBind();
    }

    protected void CustomAspxDevGridViewEntrance_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (Session["EntranceManager"] == null)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return;
        }
        TSP.DataManager.TechnicalServices.EntranceManager EntranceManager = (TSP.DataManager.TechnicalServices.EntranceManager)Session["EntranceManager"];
        DataRow rowEntrance = EntranceManager.DataTable.Rows.Find(e.Keys["EntranceId"]);
        rowEntrance.Delete();

        e.Cancel = true;

        CustomAspxDevGridViewEntrance.CancelEdit();

        CustomAspxDevGridViewEntrance.DataSource = EntranceManager.DataTable;
        CustomAspxDevGridViewEntrance.KeyFieldName = "EntranceId";
        CustomAspxDevGridViewEntrance.DataBind();
    }

    protected void CustomAspxDevGridViewEntrance_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (Session["EntranceManager"] == null)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return;
        }
        TSP.DataManager.TechnicalServices.EntranceManager EntranceManager = (TSP.DataManager.TechnicalServices.EntranceManager)Session["EntranceManager"];
        DataRow rowEntrance = EntranceManager.DataTable.Rows.Find(e.Keys["EntranceId"]);

        rowEntrance.BeginEdit();
        rowEntrance["MainDirectionsId"] = e.NewValues["MainDirectionsId"];
        rowEntrance["EntranceTypeId"] = e.NewValues["EntranceTypeId"];
        rowEntrance["Num"] = e.NewValues["Num"];
        rowEntrance["UserId"] = Utility.GetCurrentUser_UserId();
        rowEntrance["ModifiedDate"] = DateTime.Now;
        rowEntrance.EndEdit();

        e.Cancel = true;

        CustomAspxDevGridViewEntrance.CancelEdit();

        CustomAspxDevGridViewEntrance.DataSource = EntranceManager.DataTable;
        CustomAspxDevGridViewEntrance.KeyFieldName = "EntranceId";
        CustomAspxDevGridViewEntrance.DataBind();
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
        string QS = "~/Employee/TechnicalServices/Project/BlockInsert.aspx?" + "BlockId=" + PkBlockId.Value
            + "&PageMode=" + PgMode.Value
            + "&ProjectId=" + Server.HtmlDecode(Request.QueryString["ProjectId"])
            + "&PrjReId=" + Server.HtmlDecode(Request.QueryString["PrjReId"])
            + "&MPgMode=" + Server.HtmlDecode(Request.QueryString["MPgMode"])
            + "&GrdFlt=" + Server.HtmlDecode(Request.QueryString["GrdFlt"])
            + "&SrchFlt=" + Server.HtmlDecode(Request.QueryString["SrchFlt"]);
        WFUserControl.QueryStringForRedirect = QS;
        WFUserControl.PerformCallback(ProjectReId, ProjectReTableType, WfCode, e);
    }
    #endregion

    #region Methods
    /**********************************************************************************************************************************/
    #region SetKey-Method
    private void SetKeys()
    {
        try
        {
            MPgMode.Value = Server.HtmlDecode(Request.QueryString["MPgMode"].ToString());
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            PkProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"]).ToString();
            PkBlockId.Value = Server.HtmlDecode(Request.QueryString["BlockId"]).ToString();
            PkPrjReId.Value = Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString();

            PageMode = Utility.DecryptQS(PgMode.Value);
            PrjReId = Utility.DecryptQS(PkPrjReId.Value);
            ProjectId = Utility.DecryptQS(PkProjectId.Value);
            BlockId = Utility.DecryptQS(PkBlockId.Value);
            string MPageMode = Utility.DecryptQS(MPgMode.Value);

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(PrjReId) || string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(BlockId) || string.IsNullOrEmpty(MPageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            SetMode();
            CheckWorkFlowPermission();
            if (!Utility.TSProject_IsBasedOnStep())
                CheckCurrentStepsByGroup(int.Parse(PrjReId));
            FillProjectInfo(int.Parse(PrjReId));
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
        //btnDelete.Enabled = false;
        //btnDelete2.Enabled = false;
        CheckAccess();

        lblOtherStructureSystem.ClientEnabled = true;
        lblOtherStructureSkeleton.ClientEnabled = true;
        lblOtherRoofType.ClientEnabled = true;

        ASPxTextBoxFoundation.Enabled = true;
        ASPxTextBoxStageNum.Enabled = true;
        ASPxComboBoxStructureSystem.Enabled = true;
        ASPxTextBoxOtherStructureSystem.ClientEnabled = true;
        ASPxComboBoxStructureSkeleton.Enabled = true;
        ASPxTextBoxOtherStructureSkeleton.ClientEnabled = true;
        ASPxComboBoxRoofType.Enabled = true;
        ASPxTextBoxOtherRoofType.ClientEnabled = true;
        ASPxComboBoxWallMainDirections.Enabled = true;
        ASPxTextBoxWallLength.Enabled = true;
        ASPxTextBoxWallHeight.Enabled = true;
        CustomAspxDevGridViewWall.Columns["clnDelete"].Visible = true;
        ASPxComboBoxEntranceType.Enabled = true;
        ASPxComboBoxMainDirections.Enabled = true;
        ASPxTextBoxNum.Enabled = true;
        CustomAspxDevGridViewEntrance.Columns["clnDelete"].Visible = true;
        ASPxTextBoxStageTitle.Enabled = true;
        ASPxTextBoxEshghalSurface.Enabled = true;
        ASPxTextBoxFoundationArea.Enabled = true;
        ASPxTextBoxHeight.Enabled = true;
        ASPxTextBoxFlat.Enabled = true;
        ASPxComboBoxFoundationUsage.Enabled = true;
        ASPxComboBoxSecondaryUsage.Enabled = true;
        ASPxTextBoxOpenYard.Enabled = true;
        ASPxTextBoxOpenPathway.Enabled = true;
        ASPxTextBoxCloseYard.Enabled = true;
        ASPxTextBoxClosePathway.Enabled = true;
        CustomAspxDevGridView1.Columns["clnEdite"].Visible = true;

        btnAdd.Enabled = true;
        btnCancel.Enabled = true;
        btnAddWall.Enabled = true;
        btnCancelWall.Enabled = true;
        btnAddEntrance.Enabled = true;
        btnCancelEntrance.Enabled = true;

        ASPxTextBoxFoundation.Text = "";
        ASPxTextBoxStageNum.Text = "";
        ASPxComboBoxStructureSystem.DataBind();
        ASPxComboBoxStructureSystem.SelectedIndex = ASPxComboBoxStructureSystem.Items.FindByValue((int)TSP.DataManager.TSStructureSystem.Others).Index;
        ASPxTextBoxOtherStructureSystem.Text = "";
        ASPxComboBoxStructureSkeleton.DataBind();
        //ASPxComboBoxStructureSkeleton.SelectedIndex = -1;
        ASPxComboBoxStructureSkeleton.SelectedIndex = ASPxComboBoxStructureSkeleton.Items.FindByValue((int)TSP.DataManager.TSStructureSkeleton.Other).Index;
        ASPxTextBoxOtherStructureSkeleton.Text = "";
        ASPxComboBoxRoofType.DataBind();
        ASPxComboBoxRoofType.SelectedIndex = ASPxComboBoxRoofType.Items.FindByValue((int)TSP.DataManager.TSRoofType.Others).Index;
        ASPxTextBoxOtherRoofType.Text = "";
        ASPxComboBoxWallMainDirections.DataBind();
        ASPxComboBoxWallMainDirections.SelectedIndex = -1;
        ASPxTextBoxWallLength.Text = "";
        ASPxTextBoxWallHeight.Text = "";
        ASPxComboBoxEntranceType.DataBind();
        ASPxComboBoxEntranceType.SelectedIndex = -1;
        ASPxComboBoxMainDirections.DataBind();
        ASPxComboBoxMainDirections.SelectedIndex = -1;
        ASPxTextBoxNum.Text = "";

        ASPxTextBoxStageTitle.Text = "";
        ASPxTextBoxEshghalSurface.Text = "";
        ASPxTextBoxFoundationArea.Text = "";
        ASPxTextBoxHeight.Text = "";
        ASPxTextBoxFlat.Text = "";
        ASPxComboBoxFoundationUsage.DataBind();
        ASPxComboBoxFoundationUsage.SelectedIndex = -1;
        ASPxComboBoxSecondaryUsage.DataBind();
        ASPxComboBoxSecondaryUsage.SelectedIndex = -1;
        ASPxTextBoxOpenYard.Text = "";
        ASPxTextBoxOpenPathway.Text = "";
        ASPxTextBoxCloseYard.Text = "";
        ASPxTextBoxClosePathway.Text = "";

        RoundPanelContent.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        //btnDelete.Enabled = true;
        //btnDelete2.Enabled = true;
        CheckAccess();

        ASPxTextBoxFoundation.Enabled = true;
        ASPxTextBoxStageNum.Enabled = true;
        ASPxComboBoxStructureSystem.Enabled = true;

        lblOtherStructureSystem.ClientEnabled = false;
        lblOtherStructureSkeleton.ClientEnabled = false;
        lblOtherRoofType.ClientEnabled = false;


        ASPxTextBoxOtherStructureSystem.ClientEnabled = false;
        ASPxComboBoxStructureSkeleton.Enabled = true;
        ASPxTextBoxOtherStructureSkeleton.ClientEnabled = false;
        ASPxComboBoxRoofType.Enabled = true;
        ASPxTextBoxOtherRoofType.ClientEnabled = false;
        ASPxComboBoxWallMainDirections.Enabled = true;
        ASPxTextBoxWallLength.Enabled = true;
        ASPxTextBoxWallHeight.Enabled = true;
        CustomAspxDevGridViewWall.Columns["clnDelete"].Visible = true;
        ASPxComboBoxEntranceType.Enabled = true;
        ASPxComboBoxMainDirections.Enabled = true;
        ASPxTextBoxNum.Enabled = true;
        CustomAspxDevGridViewEntrance.Columns["clnDelete"].Visible = true;
        ASPxTextBoxStageTitle.Enabled = true;
        ASPxTextBoxEshghalSurface.Enabled = true;
        ASPxTextBoxFoundationArea.Enabled = true;
        ASPxTextBoxHeight.Enabled = true;
        ASPxTextBoxFlat.Enabled = true;
        ASPxComboBoxFoundationUsage.Enabled = true;
        ASPxComboBoxSecondaryUsage.Enabled = true;
        ASPxTextBoxOpenYard.Enabled = true;
        ASPxTextBoxOpenPathway.Enabled = true;
        ASPxTextBoxCloseYard.Enabled = true;
        ASPxTextBoxClosePathway.Enabled = true;
        CustomAspxDevGridView1.Columns["clnEdite"].Visible = true;


        btnAdd.Enabled = true;
        btnCancel.Enabled = true;
        btnAddWall.Enabled = true;
        btnCancelWall.Enabled = true;
        btnAddEntrance.Enabled = true;
        btnCancelEntrance.Enabled = true;

        SetValues();

        if (ASPxTextBoxOtherStructureSystem.Text != "")
        {
            ASPxTextBoxOtherStructureSystem.ClientEnabled = true;
            lblOtherStructureSystem.ClientEnabled = true;
        }

        if (ASPxTextBoxOtherStructureSkeleton.Text != "")
        {
            ASPxTextBoxOtherStructureSkeleton.ClientEnabled = true;
            lblOtherStructureSkeleton.ClientEnabled = true;
        }

        if (ASPxTextBoxOtherRoofType.Text != "")
        {
            ASPxTextBoxOtherRoofType.ClientEnabled = true;
            lblOtherRoofType.ClientEnabled = true;
        }

        RoundPanelContent.HeaderText = "ویرایش";
    }

    private void SetViewModeKeys()
    {
        btnNew.Enabled = true;
        btnNew2.Enabled = true;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        //btnDelete.Enabled = false;
        //btnDelete2.Enabled = false;
        CheckAccess();

        ASPxTextBoxFoundation.Enabled = false;
        ASPxTextBoxStageNum.Enabled = false;
        ASPxComboBoxStructureSystem.Enabled = false;
        ASPxTextBoxOtherStructureSystem.ClientEnabled = false;

        lblOtherStructureSystem.ClientEnabled = false;
        lblOtherStructureSkeleton.ClientEnabled = false;
        lblOtherRoofType.ClientEnabled = false;

        ASPxComboBoxStructureSkeleton.Enabled = false;
        ASPxTextBoxOtherStructureSkeleton.ClientEnabled = false;
        ASPxComboBoxRoofType.Enabled = false;
        ASPxTextBoxOtherRoofType.ClientEnabled = false;
        ASPxComboBoxWallMainDirections.Enabled = false;
        ASPxTextBoxWallLength.Enabled = false;
        ASPxTextBoxWallHeight.Enabled = false;
        CustomAspxDevGridViewWall.Columns["clnDelete"].Visible = false;
        ASPxComboBoxEntranceType.Enabled = false;
        ASPxComboBoxMainDirections.Enabled = false;
        ASPxTextBoxNum.Enabled = false;
        CustomAspxDevGridViewEntrance.Columns["clnDelete"].Visible = false;
        ASPxTextBoxStageTitle.Enabled = false;
        ASPxTextBoxEshghalSurface.Enabled = false;
        ASPxTextBoxFoundationArea.Enabled = false;
        ASPxTextBoxHeight.Enabled = false;
        ASPxTextBoxFlat.Enabled = false;
        ASPxComboBoxFoundationUsage.Enabled = false;
        ASPxComboBoxSecondaryUsage.Enabled = false;
        ASPxTextBoxOpenYard.Enabled = false;
        ASPxTextBoxOpenPathway.Enabled = false;
        ASPxTextBoxCloseYard.Enabled = false;
        ASPxTextBoxClosePathway.Enabled = false;
        CustomAspxDevGridView1.Columns["clnEdite"].Visible = false;

        btnAdd.Enabled = false;
        btnCancel.Enabled = false;
        btnAddWall.Enabled = false;
        btnCancelWall.Enabled = false;
        btnAddEntrance.Enabled = false;
        btnCancelEntrance.Enabled = false;

        SetValues();

        RoundPanelContent.HeaderText = "مشاهده";
    }
    #endregion

    private void SetValues()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        BlockId = Utility.DecryptQS(PkBlockId.Value);

        if ((string.IsNullOrEmpty(BlockId)) || (string.IsNullOrEmpty(PageMode)))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TechnicalServices.BlockManager Manager = new TSP.DataManager.TechnicalServices.BlockManager();
        Manager.FindByBlockId(Convert.ToInt32(BlockId));
        if (Manager.Count == 1)
        {
            ASPxTextBoxFoundation.Text = Manager[0]["Foundation"].ToString();
            ASPxTextBoxStageNum.Text = Manager[0]["StageNum"].ToString();
            ASPxComboBoxStructureSystem.DataBind();
            ASPxComboBoxStructureSystem.SelectedIndex = ASPxComboBoxStructureSystem.Items.IndexOfValue(Manager[0]["StructureSystemId"]);
            ASPxTextBoxOtherStructureSystem.Text = Manager[0]["StructureSystem"].ToString();
            ASPxComboBoxStructureSkeleton.DataBind();
            ASPxComboBoxStructureSkeleton.SelectedIndex = ASPxComboBoxStructureSkeleton.Items.IndexOfValue(Manager[0]["StructureSkeletonId"]);
            ASPxTextBoxOtherStructureSkeleton.Text = Manager[0]["StructureSkeleton"].ToString();
            ASPxComboBoxRoofType.DataBind();
            ASPxComboBoxRoofType.SelectedIndex = ASPxComboBoxRoofType.Items.IndexOfValue(Manager[0]["RoofTypeId"]);
            ASPxTextBoxOtherRoofType.Text = Manager[0]["RoofType"].ToString();

            if (ASPxComboBoxStructureSystem.Text == ASPxTextBoxOtherStructureSystem.Text)
                ASPxTextBoxOtherStructureSystem.Text = "";

            if (ASPxComboBoxStructureSkeleton.Text == ASPxTextBoxOtherStructureSkeleton.Text)
                ASPxTextBoxOtherStructureSkeleton.Text = "";

            if (ASPxComboBoxRoofType.Text == ASPxTextBoxOtherRoofType.Text)
                ASPxTextBoxOtherRoofType.Text = "";

            FillGrid();
            FillEntranceGrid();
            FillWallsGrid();
        }
        else
        {
            SetLabelWarning("چنین رکوردی وجود ندارد");
        }
    }

    private void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.BlockManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        if (btnNew.Enabled == true)
        {
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
        }

        if (btnEdit.Enabled == true)
        {
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
        }

        //if (btnDelete.Enabled == true)
        //{
        //    btnDelete.Enabled = per.CanDelete;
        //    btnDelete2.Enabled = per.CanDelete;
        //}
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
        //this.ViewState["BtnDelete"] = btnDelete.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }

    /**********************************************************************************************************************************/
    private TSP.DataManager.TechnicalServices.FoundationManager CreateFoundationManager()
    {
        TSP.DataManager.TechnicalServices.FoundationManager manager = new TSP.DataManager.TechnicalServices.FoundationManager();
        return manager;
    }

    private void FillGrid(int BlockId)
    {
        TSP.DataManager.TechnicalServices.FoundationManager manager = (TSP.DataManager.TechnicalServices.FoundationManager)Session["FoundationManager"];
        manager.FindByBlockId(BlockId);
        CustomAspxDevGridView1.DataSource = manager.DataTable;
        CustomAspxDevGridView1.DataBind();
    }

    private void FillGrid()
    {
        TSP.DataManager.TechnicalServices.FoundationManager manager = (TSP.DataManager.TechnicalServices.FoundationManager)Session["FoundationManager"];
        if (Utility.DecryptQS(PgMode.Value) != "New")
        {
            BlockId = Utility.DecryptQS(PkBlockId.Value);
            manager.FindByBlockId(Convert.ToInt32(BlockId));
        }

        CustomAspxDevGridView1.DataSource = manager.DataTable;
        CustomAspxDevGridView1.DataBind();
    }

    private void RowInserting()
    {
        TSP.DataManager.TechnicalServices.FoundationManager FoundationManager = (TSP.DataManager.TechnicalServices.FoundationManager)Session["FoundationManager"];

        DataRow rowFoundation = FoundationManager.NewRow();

        rowFoundation.BeginEdit();

        rowFoundation["BlockId"] = -1;
        rowFoundation["StageTitle"] = ASPxTextBoxStageTitle.Text;
        rowFoundation["EshghalSurface"] = ASPxTextBoxEshghalSurface.Text;
        rowFoundation["Area"] = ASPxTextBoxFoundationArea.Text;
        rowFoundation["Height"] = ASPxTextBoxHeight.Text;
        rowFoundation["Flat"] = ASPxTextBoxFlat.Text;
        rowFoundation["UsageId"] = ASPxComboBoxFoundationUsage.Value;
        rowFoundation["SecondaryUsageId"] = ASPxComboBoxSecondaryUsage.Value;
        rowFoundation["CloseYard"] = ASPxTextBoxCloseYard.Text;
        rowFoundation["ClosePathway"] = ASPxTextBoxClosePathway.Text;
        rowFoundation["OpenYard"] = ASPxTextBoxOpenYard.Text;
        rowFoundation["OpenPathway"] = ASPxTextBoxOpenPathway.Text;
        rowFoundation["UserId"] = Utility.GetCurrentUser_UserId();
        rowFoundation["ModifiedDate"] = DateTime.Now;

        rowFoundation.EndEdit();

        FoundationManager.AddRow(rowFoundation);

        CustomAspxDevGridView1.CancelEdit();

        CustomAspxDevGridView1.DataSource = FoundationManager.DataTable;
        CustomAspxDevGridView1.KeyFieldName = "FoundationId";
        CustomAspxDevGridView1.DataBind();
        Clear();
    }

    private void Clear()
    {
        ASPxTextBoxStageTitle.Text = "";
        ASPxTextBoxEshghalSurface.Text = "";
        ASPxTextBoxFoundationArea.Text = "";
        ASPxTextBoxHeight.Text = "";
        ASPxTextBoxFlat.Text = "";
        ASPxComboBoxFoundationUsage.SelectedIndex = -1;
        ASPxComboBoxSecondaryUsage.SelectedIndex = -1;
        ASPxTextBoxOpenYard.Text = "";
        ASPxTextBoxOpenPathway.Text = "";
        ASPxTextBoxCloseYard.Text = "";
        ASPxTextBoxClosePathway.Text = "";
    }

    /**********************************************************************************************************************************/
    private TSP.DataManager.TechnicalServices.WallsManager CreateWallsManager()
    {
        TSP.DataManager.TechnicalServices.WallsManager manager = new TSP.DataManager.TechnicalServices.WallsManager();
        return manager;
    }

    private void FillWallsGrid(int BlockId)
    {
        TSP.DataManager.TechnicalServices.WallsManager manager = (TSP.DataManager.TechnicalServices.WallsManager)Session["WallsManager"];
        manager.FindByBlockId(BlockId);
        CustomAspxDevGridViewWall.DataSource = manager.DataTable;
        CustomAspxDevGridViewWall.DataBind();
    }

    private void FillWallsGrid()
    {
        TSP.DataManager.TechnicalServices.WallsManager manager = (TSP.DataManager.TechnicalServices.WallsManager)Session["WallsManager"];
        if (Utility.DecryptQS(PgMode.Value) != "New")
        {
            BlockId = Utility.DecryptQS(PkBlockId.Value);
            manager.FindByBlockId(Convert.ToInt32(BlockId));
        }

        CustomAspxDevGridViewWall.DataSource = manager.DataTable;
        CustomAspxDevGridViewWall.DataBind();
    }

    private void WallsRowInserting()
    {
        TSP.DataManager.TechnicalServices.WallsManager WallsManager = (TSP.DataManager.TechnicalServices.WallsManager)Session["WallsManager"];

        DataRow rowWalls = WallsManager.NewRow();

        rowWalls.BeginEdit();

        rowWalls["BlockId"] = -1;
        rowWalls["MainDirectionsId"] = ASPxComboBoxWallMainDirections.Value;
        rowWalls["Length"] = ASPxTextBoxWallLength.Text;
        rowWalls["Height"] = ASPxTextBoxWallHeight.Text;
        rowWalls["UserId"] = Utility.GetCurrentUser_UserId();
        rowWalls["ModifiedDate"] = DateTime.Now;

        rowWalls.EndEdit();

        WallsManager.AddRow(rowWalls);

        CustomAspxDevGridViewWall.CancelEdit();

        CustomAspxDevGridViewWall.DataSource = WallsManager.DataTable;
        CustomAspxDevGridViewWall.KeyFieldName = "WallsId";
        CustomAspxDevGridViewWall.DataBind();
        ClearWalls();
    }

    private void ClearWalls()
    {
        ASPxComboBoxWallMainDirections.SelectedIndex = -1;
        ASPxTextBoxWallLength.Text = "";
        ASPxTextBoxWallHeight.Text = "";
    }

    /**********************************************************************************************************************************/
    private TSP.DataManager.TechnicalServices.EntranceManager CreateEntranceManager()
    {
        TSP.DataManager.TechnicalServices.EntranceManager manager = new TSP.DataManager.TechnicalServices.EntranceManager();
        return manager;
    }

    private void FillEntranceGrid(int BlockId)
    {
        TSP.DataManager.TechnicalServices.EntranceManager manager = (TSP.DataManager.TechnicalServices.EntranceManager)Session["EntranceManager"];
        manager.FindByBlockId(BlockId);
        CustomAspxDevGridViewEntrance.DataSource = manager.DataTable;
        CustomAspxDevGridViewEntrance.DataBind();
    }

    private void FillEntranceGrid()
    {
        TSP.DataManager.TechnicalServices.EntranceManager manager = (TSP.DataManager.TechnicalServices.EntranceManager)Session["EntranceManager"];
        if (Utility.DecryptQS(PgMode.Value) != "New")
        {
            BlockId = Utility.DecryptQS(PkBlockId.Value);
            manager.FindByBlockId(Convert.ToInt32(BlockId));
        }

        CustomAspxDevGridViewEntrance.DataSource = manager.DataTable;
        CustomAspxDevGridViewEntrance.DataBind();
    }

    private void EntranceRowInserting()
    {
        TSP.DataManager.TechnicalServices.EntranceManager EntranceManager = (TSP.DataManager.TechnicalServices.EntranceManager)Session["EntranceManager"];

        DataRow rowEntrance = EntranceManager.NewRow();

        rowEntrance.BeginEdit();

        rowEntrance["BlockId"] = -1;
        rowEntrance["MainDirectionsId"] = ASPxComboBoxMainDirections.Value;
        rowEntrance["EntranceTypeId"] = ASPxComboBoxEntranceType.Value;
        rowEntrance["Num"] = ASPxTextBoxNum.Text;
        rowEntrance["UserId"] = Utility.GetCurrentUser_UserId();
        rowEntrance["ModifiedDate"] = DateTime.Now;

        rowEntrance.EndEdit();

        EntranceManager.AddRow(rowEntrance);

        CustomAspxDevGridViewEntrance.CancelEdit();

        CustomAspxDevGridViewEntrance.DataSource = EntranceManager.DataTable;
        CustomAspxDevGridViewEntrance.KeyFieldName = "EntranceId";
        CustomAspxDevGridViewEntrance.DataBind();
        ClearEntrance();
    }

    private void ClearEntrance()
    {
        ASPxComboBoxMainDirections.SelectedIndex = -1;
        ASPxComboBoxEntranceType.SelectedIndex = -1;
        ASPxTextBoxNum.Text = "";
    }

    /**********************************************************************************************************************************/
    private int GetPlansMethodId(TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager)
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);

        if (ProjectId == "-1")
            return -1;

        PlansMethodManager.FindByProjectId(Convert.ToInt32(ProjectId));
        if (PlansMethodManager.Count > 0)
            return Convert.ToInt32(PlansMethodManager[0]["PlansMethodId"]);
        else
            return -1;
    }

    private bool CheckStageNum(int Count)
    {
        if (Count != Convert.ToInt32(ASPxTextBoxStageNum.Text))
            return false;
        return true;
    }

    private bool CheckFoundation(TSP.DataManager.TechnicalServices.FoundationManager FoundationManager)
    {
        double Foundation = 0;
        for (int i = 0; i < FoundationManager.Count; i++)
            Foundation += Convert.ToDouble(FoundationManager[i]["Area"]);
        if (Foundation != Convert.ToDouble(ASPxTextBoxFoundation.Text))
            return false;
        return true;
    }

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
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    #region Insert-Update
    /************************************************************ Insert ************************************************************/
    private void Insert()
    {
        if (IsPageRefresh)
        {
            return;
        }

        if (Session["FoundationManager"] == null || Session["WallsManager"] == null || Session["EntranceManager"] == null)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return;
        }
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
        TSP.DataManager.TechnicalServices.BalconyManager BalconyManager = new TSP.DataManager.TechnicalServices.BalconyManager();
        TSP.DataManager.TechnicalServices.FoundationManager FoundationManager = (TSP.DataManager.TechnicalServices.FoundationManager)Session["FoundationManager"];
        TSP.DataManager.TechnicalServices.WallsManager WallsManager = (TSP.DataManager.TechnicalServices.WallsManager)Session["WallsManager"];
        TSP.DataManager.TechnicalServices.EntranceManager EntranceManager = (TSP.DataManager.TechnicalServices.EntranceManager)Session["EntranceManager"];
        TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager = new TSP.DataManager.TechnicalServices.PlansMethodManager();
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();

        TSP.DataManager.TechnicalServices.StructureGroupsManager StructureGroupsManager = new TSP.DataManager.TechnicalServices.StructureGroupsManager();
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(BlockManager);
        transact.Add(BalconyManager);
        transact.Add(FoundationManager);
        transact.Add(WallsManager);
        transact.Add(EntranceManager);
        transact.Add(PlansMethodManager);
        transact.Add(ProjectRequestManager);
        transact.Add(StructureGroupsManager);
        transact.Add(ProjectManager);

        //if (!CheckStageNum(FoundationManager.Count))
        //{
        //    SetLabelWarning("مقدار تعداد طبقات معتبر نیست");
        //    return;
        //}

        if (FoundationManager.Count > 0)
            if (!CheckFoundation(FoundationManager))
            {
                SetLabelWarning("مجموع مساحت زیربنای طبقات بایستی با زیربنای کل بلوک برابر باشد");
                return;
            }

        if (!CheckFoundationWithProject(Convert.ToDouble(ASPxTextBoxFoundation.Text.Trim())))
        {
            SetLabelWarning("مجموع مساحت زیربنای بلوک ها از زیربنای پروژه بیشتر است");
            return;
        }

        int StageNum = Convert.ToInt32(ASPxTextBoxStageNum.Text.Trim());
        if (StageNum <= 0)
        {
            SetLabelWarning("تعداد طبقات از روی شالوده نامشخص است");
            return;
        }

        if (!Utility.TSProject_IsBasedOnStep())
            if (!CheckStepByGroup(StageNum)) return;

        try
        {
            transact.BeginSave();

            if (!InsertBlock(BlockManager, PlansMethodManager))
            {
                transact.CancelSave();
                return;
            }

            BlockId = Utility.DecryptQS(PkBlockId.Value);
            for (int i = 0; i < FoundationManager.Count; i++)
            {
                FoundationManager[i]["BlockId"] = BlockId;
            }
            FoundationManager.Save();

            for (int i = 0; i < WallsManager.Count; i++)
            {
                WallsManager[i]["BlockId"] = BlockId;
            }
            WallsManager.Save();

            for (int i = 0; i < EntranceManager.Count; i++)
            {
                EntranceManager[i]["BlockId"] = BlockId;
            }
            EntranceManager.Save();

            if (!InsertBalcony(BalconyManager, FoundationManager))
            {
                transact.CancelSave();
                return;
            }

            UpdateDefualtPlanMethods(BlockManager, PlansMethodManager);
            UpdateGroupId(ProjectRequestManager, BlockManager, StructureGroupsManager, ProjectManager);
            transact.EndSave();

            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد");

            PrjReId = Utility.DecryptQS(PkPrjReId.Value);
            FillProjectInfo(int.Parse(PrjReId));

            ASPxTextBoxOtherStructureSystem.ErrorText = "";
            ASPxTextBoxOtherStructureSystem.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.Text;

            ASPxTextBoxOtherStructureSkeleton.ErrorText = "";
            ASPxTextBoxOtherStructureSkeleton.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.Text;

            ASPxTextBoxOtherRoofType.ErrorText = "";
            ASPxTextBoxOtherRoofType.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.Text;
        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err, 'I');
        }
    }

    private Boolean InsertBlock(TSP.DataManager.TechnicalServices.BlockManager BlockManager, TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager)
    {
        DataRow rowBlock = BlockManager.NewRow();

        rowBlock.BeginEdit();
        int PlansMethodsId = GetPlansMethodId(PlansMethodManager);
        if (PlansMethodsId == -1)
        {
            PlansMethodsId = InsertDefualtPlanMethods(PlansMethodManager);
            if (PlansMethodsId == -1)
            {
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
                return false;
            }
        }
        rowBlock["PlansMethodId"] = PlansMethodsId;
        rowBlock["Foundation"] = ASPxTextBoxFoundation.Text;
        rowBlock["StageNum"] = ASPxTextBoxStageNum.Text;
        rowBlock["StructureSystemId"] = ASPxComboBoxStructureSystem.Value;

        if (ASPxTextBoxOtherStructureSystem.Text == "")
            rowBlock["StructureSystem"] = ASPxComboBoxStructureSystem.Text;
        else
            rowBlock["StructureSystem"] = ASPxTextBoxOtherStructureSystem.Text;

        rowBlock["StructureSkeletonId"] = ASPxComboBoxStructureSkeleton.Value;

        if (ASPxTextBoxOtherStructureSkeleton.Text == "")
            rowBlock["StructureSkeleton"] = ASPxComboBoxStructureSkeleton.Text;
        else
            rowBlock["StructureSkeleton"] = ASPxTextBoxOtherStructureSkeleton.Text;

        rowBlock["RoofTypeId"] = ASPxComboBoxRoofType.Value;

        if (ASPxTextBoxOtherRoofType.Text == "")
            rowBlock["RoofType"] = ASPxComboBoxRoofType.Text;
        else
            rowBlock["RoofType"] = ASPxTextBoxOtherRoofType.Text;

        rowBlock["UserId"] = Utility.GetCurrentUser_UserId();
        rowBlock["ModifiedDate"] = DateTime.Now;
        rowBlock.EndEdit();

        BlockManager.AddRow(rowBlock);
        BlockManager.Save();

        BlockManager.DataTable.AcceptChanges();
        BlockId = BlockManager[0]["BlockId"].ToString();
        PkBlockId.Value = Utility.EncryptQS(BlockId.ToString());
        return true;
    }

    private Boolean InsertBalcony(TSP.DataManager.TechnicalServices.BalconyManager BalconyManager, TSP.DataManager.TechnicalServices.FoundationManager FoundationManager)
    {
        for (int i = 0; i < FoundationManager.Count; i++)
        {
            DataRow rowOpenBalcony = BalconyManager.NewRow();
            if (!Utility.IsDBNullOrNullValue(FoundationManager[i]["FoundationId"]))
                rowOpenBalcony["FoundationId"] = FoundationManager[i]["FoundationId"];
            if (!Utility.IsDBNullOrNullValue(FoundationManager[i]["OpenYard"]))
                rowOpenBalcony["Yard"] = FoundationManager[i]["OpenYard"];
            if (!Utility.IsDBNullOrNullValue(FoundationManager[i]["OpenPathway"]))
                rowOpenBalcony["Pathway"] = FoundationManager[i]["OpenPathway"];
            rowOpenBalcony["BalconyTypeId"] = (int)TSP.DataManager.TSBalconyType.Open;
            rowOpenBalcony["UserId"] = Utility.GetCurrentUser_UserId();
            rowOpenBalcony["ModifiedDate"] = DateTime.Now;
            BalconyManager.AddRow(rowOpenBalcony);

            DataRow rowCloseBalcony = BalconyManager.NewRow();
            if (!Utility.IsDBNullOrNullValue(FoundationManager[i]["FoundationId"]))
                rowCloseBalcony["FoundationId"] = FoundationManager[i]["FoundationId"];
            if (!Utility.IsDBNullOrNullValue(FoundationManager[i]["CloseYard"]))
                rowCloseBalcony["Yard"] = FoundationManager[i]["CloseYard"];
            if (!Utility.IsDBNullOrNullValue(FoundationManager[i]["ClosePathway"]))
                rowCloseBalcony["Pathway"] = FoundationManager[i]["ClosePathway"];
            rowCloseBalcony["BalconyTypeId"] = (int)TSP.DataManager.TSBalconyType.Close;
            rowCloseBalcony["UserId"] = Utility.GetCurrentUser_UserId();
            rowCloseBalcony["ModifiedDate"] = DateTime.Now;
            BalconyManager.AddRow(rowCloseBalcony);
        }
        BalconyManager.Save();
        BalconyManager.DataTable.AcceptChanges();
        return true;
    }

    private int InsertDefualtPlanMethods(TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager)
    {
        int PlansMethodId = -1;
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        PrjReId = Utility.DecryptQS(PkPrjReId.Value);

        DataRow rowPlansMethod = PlansMethodManager.NewRow();

        rowPlansMethod.BeginEdit();

        rowPlansMethod["ProjectId"] = ProjectId;
        rowPlansMethod["PlansMethodNo"] = "پیش فرض/" + ProjectId;
        rowPlansMethod["RegisteredDate"] = Utility.GetDateOfToday();
        rowPlansMethod["StructureBuiltPlaceId"] = 1;
        rowPlansMethod["EshghalSurface"] = ASPxTextBoxEshghalSurface.Text;
        rowPlansMethod["Tarakom"] = 0;
        rowPlansMethod["AllowableHeight"] = 0;
        rowPlansMethod["CommercialLimitation"] = 0;
        rowPlansMethod["BlockNum"] = 0;
        rowPlansMethod["InActive"] = 0;
        rowPlansMethod["PrjReId"] = PrjReId;
        rowPlansMethod["LocationCriterion"] = 0;
        rowPlansMethod["Mantelet"] = 0;
        rowPlansMethod["UserId"] = Utility.GetCurrentUser_UserId();
        rowPlansMethod["ModifiedDate"] = DateTime.Now;

        rowPlansMethod.EndEdit();

        PlansMethodManager.AddRow(rowPlansMethod);
        PlansMethodManager.Save();

        PlansMethodManager.DataTable.AcceptChanges();
        PlansMethodId = Convert.ToInt32(PlansMethodManager[0]["PlansMethodId"]);
        return PlansMethodId;
    }

    private void UpdateDefualtPlanMethods(TSP.DataManager.TechnicalServices.BlockManager BlockManager, TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager)
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        PrjReId = Utility.DecryptQS(PkPrjReId.Value);
        if (ProjectId == "-1")
            return;
        if (PrjReId == "-1")
            return;

      int BlockNum= BlockManager.SelectTSBlockCountByProjectAndPrjReId(int.Parse(ProjectId), int.Parse(PrjReId));
        PlansMethodManager.FindByProjectAndPrjReId(int.Parse(ProjectId), int.Parse(PrjReId));
        if (PlansMethodManager.Count == 1)
        {
            PlansMethodManager[0].BeginEdit();
            PlansMethodManager[0]["BlockNum"] = BlockNum;
            PlansMethodManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            PlansMethodManager[0]["ModifiedDate"] = DateTime.Now;
            PlansMethodManager[0].EndEdit();
            PlansMethodManager.Save();
        }
    }

    private void UpdateGroupId(TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager, TSP.DataManager.TechnicalServices.BlockManager BlockManager, TSP.DataManager.TechnicalServices.StructureGroupsManager StructureGroupsManager, TSP.DataManager.TechnicalServices.ProjectManager ProjectManager)
    {
        PrjReId = Utility.DecryptQS(PkPrjReId.Value);
        int MaxStageNum = BlockManager.GetMaxStageNumByRequest(Convert.ToInt32(PrjReId));
        StructureGroupsManager.FindArchiveStructureItemsForGroup(MaxStageNum, -1);
        if (StructureGroupsManager.Count != 1)
            return;
        int GroupId = Convert.ToInt32(StructureGroupsManager[0]["GroupId"]);
        ProjectRequestManager.FindByCode(Convert.ToInt32(PrjReId));
        if (ProjectRequestManager.Count != 1)
            return;

        ProjectRequestManager[0].BeginEdit();
        ProjectRequestManager[0]["GroupId"] = GroupId;
        ProjectRequestManager[0].EndEdit();
        ProjectRequestManager.Save();

        if (Convert.ToInt32(ProjectRequestManager[0]["PrjReTypeId"]) == (int)TSP.DataManager.TSProjectRequestType.InsertProject)
        {
            ProjectManager.FindByProjectId(Convert.ToInt32(ProjectRequestManager[0]["ProjectId"]));
            if (ProjectManager.Count == 1)
            {
                ProjectManager[0].BeginEdit();
                ProjectManager[0]["GroupId"] = GroupId;
                ProjectManager[0].EndEdit();
                ProjectManager.Save();
            }
        }
    }
    /************************************************************* Update **********************************************************/
    private void Update()
    {
        if (IsPageRefresh)
        {
            return;
        }
        if (Session["FoundationManager"] == null || Session["WallsManager"] == null || Session["EntranceManager"] == null)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired));
            return;
        }
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
        TSP.DataManager.TechnicalServices.BalconyManager BalconyManager = new TSP.DataManager.TechnicalServices.BalconyManager();
        TSP.DataManager.TechnicalServices.FoundationManager FoundationManager = (TSP.DataManager.TechnicalServices.FoundationManager)Session["FoundationManager"];
        TSP.DataManager.TechnicalServices.WallsManager WallsManager = (TSP.DataManager.TechnicalServices.WallsManager)Session["WallsManager"];
        TSP.DataManager.TechnicalServices.EntranceManager EntranceManager = (TSP.DataManager.TechnicalServices.EntranceManager)Session["EntranceManager"];
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        TSP.DataManager.TechnicalServices.StructureGroupsManager StructureGroupsManager = new TSP.DataManager.TechnicalServices.StructureGroupsManager();
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(BlockManager);
        transact.Add(BalconyManager);
        transact.Add(FoundationManager);
        transact.Add(WallsManager);
        transact.Add(EntranceManager);
        transact.Add(ProjectRequestManager);
        transact.Add(StructureGroupsManager);
        transact.Add(ProjectManager);

        //if (!CheckStageNum(FoundationManager.Count))
        //{
        //    SetLabelWarning("مقدار تعداد طبقات معتبر نیست");
        //    return;
        //}

        if (!CheckFoundationWithProject(Convert.ToDouble(ASPxTextBoxFoundation.Text.Trim())))
        {
            SetLabelWarning("مجموع مساحت زیربنای بلوک ها از زیربنای پروژه بیشتر است");
            return;
        }

        if (FoundationManager.Count > 0)
            if (!CheckFoundation(FoundationManager))
            {
                SetLabelWarning("مجموع مساحت زیربنای طبقات بایستی با زیربنای کل بلوک برابر باشد");
                return;
            }

        if (!Utility.TSProject_IsBasedOnStep())
            if (!CheckStepByGroup(Convert.ToInt32(ASPxTextBoxStageNum.Text.Trim()))) return;

        try
        {
            transact.BeginSave();

            UpdateBlock(BlockManager);

            BlockId = Utility.DecryptQS(PkBlockId.Value);
            for (int i = 0; i < FoundationManager.Count; i++)
            {
                FoundationManager[i]["BlockId"] = BlockId;
            }
            FoundationManager.Save();

            for (int i = 0; i < WallsManager.Count; i++)
            {
                WallsManager[i]["BlockId"] = BlockId;
            }
            WallsManager.Save();

            for (int i = 0; i < EntranceManager.Count; i++)
            {
                EntranceManager[i]["BlockId"] = BlockId;
            }
            EntranceManager.Save();

            UpdateBalcony(BalconyManager, FoundationManager);

            UpdateGroupId(ProjectRequestManager, BlockManager, StructureGroupsManager, ProjectManager);
            transact.EndSave();

            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد");

            PrjReId = Utility.DecryptQS(PkPrjReId.Value);
            FillProjectInfo(int.Parse(PrjReId));
            ASPxTextBoxOtherStructureSystem.ErrorText = "";
            ASPxTextBoxOtherStructureSystem.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.Text;

            ASPxTextBoxOtherStructureSkeleton.ErrorText = "";
            ASPxTextBoxOtherStructureSkeleton.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.Text;

            ASPxTextBoxOtherRoofType.ErrorText = "";
            ASPxTextBoxOtherRoofType.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.Text;

        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err, 'U');
        }
    }

    private void UpdateBlock(TSP.DataManager.TechnicalServices.BlockManager BlockManager)
    {
        BlockId = Utility.DecryptQS(PkBlockId.Value);

        if (string.IsNullOrEmpty(BlockId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        BlockManager.FindByBlockId(Convert.ToInt32(BlockId));

        if (BlockManager.Count >= 1)
        {
            BlockManager[0].BeginEdit();
            BlockManager[0]["Foundation"] = ASPxTextBoxFoundation.Text;
            BlockManager[0]["StageNum"] = ASPxTextBoxStageNum.Text;
            BlockManager[0]["StructureSystemId"] = ASPxComboBoxStructureSystem.Value;

            if (ASPxTextBoxOtherStructureSystem.Text == "")
                BlockManager[0]["StructureSystem"] = ASPxComboBoxStructureSystem.Text;
            else
                BlockManager[0]["StructureSystem"] = ASPxTextBoxOtherStructureSystem.Text;

            BlockManager[0]["StructureSkeletonId"] = ASPxComboBoxStructureSkeleton.Value;

            if (ASPxTextBoxOtherStructureSkeleton.Text == "")
                BlockManager[0]["StructureSkeleton"] = ASPxComboBoxStructureSkeleton.Text;
            else
                BlockManager[0]["StructureSkeleton"] = ASPxTextBoxOtherStructureSkeleton.Text;

            BlockManager[0]["RoofTypeId"] = ASPxComboBoxRoofType.Value;

            if (ASPxTextBoxOtherRoofType.Text == "")
                BlockManager[0]["RoofType"] = ASPxComboBoxRoofType.Text;
            else
                BlockManager[0]["RoofType"] = ASPxTextBoxOtherRoofType.Text;

            BlockManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            BlockManager[0]["ModifiedDate"] = DateTime.Now;
            BlockManager[0].EndEdit();

            BlockManager.Save();

            BlockManager.DataTable.AcceptChanges();
            BlockId = BlockManager[0]["BlockId"].ToString();
            PkBlockId.Value = Utility.EncryptQS(BlockId.ToString());
        }
    }

    private void UpdateBalcony(TSP.DataManager.TechnicalServices.BalconyManager BalconyManager, TSP.DataManager.TechnicalServices.FoundationManager FoundationManager)
    {
        DataRow[] DeletedFoundationRow = FoundationManager.DataTable.Select("", "", DataViewRowState.Deleted);
        DataRow[] UpdatedFoundationRow = FoundationManager.DataTable.Select("", "", DataViewRowState.ModifiedOriginal);
        DataRow[] InsertedFoundationRow = FoundationManager.DataTable.Select("", "", DataViewRowState.Added);

        //if (DeletedFoundationRow != null)
        //    for (int i = 0; i < DeletedFoundationRow.Length; i++)
        //    {
        //        BalconyManager.FindByFoundationId(Convert.ToInt32(DeletedFoundationRow[i]["FoundationId"]));
        //        while (BalconyManager.Count > 0)
        //            BalconyManager[0].Delete();
        //        BalconyManager.Save();
        //    }

        if (UpdatedFoundationRow != null)
            for (int i = 0; i < UpdatedFoundationRow.Length; i++)
            {
                UpdateBalconyInUpdate(BalconyManager, UpdatedFoundationRow[i]);
            }

        if (InsertedFoundationRow != null)
            for (int i = 0; i < InsertedFoundationRow.Length; i++)
            {
                InsertBalconyInUpdate(BalconyManager, InsertedFoundationRow[i]);
            }
    }

    private void UpdateBalconyInUpdate(TSP.DataManager.TechnicalServices.BalconyManager BalconyManager, DataRow FoundationManager)
    {
        BalconyManager.FindByFoundationId(Convert.ToInt32(FoundationManager["FoundationId"]));

        if (BalconyManager.Count == 0)
        {
            InsertBalconyInUpdate(BalconyManager, FoundationManager);
        }
        else
        {

            for (int i = 0; i < BalconyManager.Count; i++)
            {
                BalconyManager[i].BeginEdit();

                switch (Convert.ToInt32(BalconyManager[i]["BalconyTypeId"]))
                {
                    case (int)TSP.DataManager.TSBalconyType.Open:
                        BalconyManager[i]["Yard"] = FoundationManager["OpenYard"];
                        BalconyManager[i]["Pathway"] = FoundationManager["OpenPathway"];
                        break;

                    case (int)TSP.DataManager.TSBalconyType.Close:
                        BalconyManager[i]["Yard"] = FoundationManager["CloseYard"];
                        BalconyManager[i]["Pathway"] = FoundationManager["ClosePathway"];
                        break;

                }

                BalconyManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                BalconyManager[i]["ModifiedDate"] = DateTime.Now;
                BalconyManager[i].EndEdit();
            }
            BalconyManager.Save();
        }
    }

    private void InsertBalconyInUpdate(TSP.DataManager.TechnicalServices.BalconyManager BalconyManager, DataRow FoundationManager)
    {
        DataRow rowOpenBalcony = BalconyManager.NewRow();

        rowOpenBalcony.BeginEdit();
        rowOpenBalcony["FoundationId"] = FoundationManager["FoundationId"];
        rowOpenBalcony["Yard"] = FoundationManager["OpenYard"];
        rowOpenBalcony["Pathway"] = FoundationManager["OpenPathway"];
        rowOpenBalcony["BalconyTypeId"] = (int)TSP.DataManager.TSBalconyType.Open;
        rowOpenBalcony["UserId"] = Utility.GetCurrentUser_UserId();
        rowOpenBalcony["ModifiedDate"] = DateTime.Now;
        rowOpenBalcony.EndEdit();

        BalconyManager.AddRow(rowOpenBalcony);

        DataRow rowCloseBalcony = BalconyManager.NewRow();

        rowCloseBalcony.BeginEdit();
        rowCloseBalcony["FoundationId"] = FoundationManager["FoundationId"];
        rowCloseBalcony["Yard"] = FoundationManager["CloseYard"];
        rowCloseBalcony["Pathway"] = FoundationManager["ClosePathway"];
        rowCloseBalcony["BalconyTypeId"] = (int)TSP.DataManager.TSBalconyType.Close;
        rowCloseBalcony["UserId"] = Utility.GetCurrentUser_UserId();
        rowCloseBalcony["ModifiedDate"] = DateTime.Now;
        rowCloseBalcony.EndEdit();

        BalconyManager.AddRow(rowCloseBalcony);


        BalconyManager.Save();
        BalconyManager.DataTable.AcceptChanges();
    }
    #endregion
    /**********************************************************************************************************************************/
    private void DeleteBlock()
    {
        BlockId = Utility.DecryptQS(PkBlockId.Value);

        if (string.IsNullOrEmpty(BlockId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
        BlockManager.FindByBlockId(Convert.ToInt32(BlockId));

        if (BlockManager.Count == 1)
        {
            try
            {
                BlockManager[0].Delete();
                int cn = BlockManager.Save();
                if (cn == 1)
                {
                    BlockManager.DataTable.AcceptChanges();
                    PkBlockId.Value = Utility.EncryptQS("-1");
                    PgMode.Value = Utility.EncryptQS("New");
                    SetNewModeKeys();

                    SetLabelWarning("حذف انجام شد");
                }
                else
                {
                    SetLabelWarning("خطایی در حذف انجام گرفته است");
                }
            }
            catch (Exception err)
            {
                SetError(err, 'D');
            }
        }
    }

    /**********************************************************************************************************************************/
    protected void ASPxComboBoxStructureSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ASPxComboBoxStructureSystem.Text == "سایر موارد")
            ASPxTextBoxOtherStructureSystem.ClientEnabled = true;
        else
        {
            ASPxTextBoxOtherStructureSystem.ClientEnabled = false;
            ASPxTextBoxOtherStructureSystem.Text = "";
        }
    }

    protected void ASPxComboBoxStructureSkeleton_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ASPxComboBoxStructureSkeleton.Text == "سایر موارد")
            ASPxTextBoxOtherStructureSkeleton.ClientEnabled = true;
        else
        {
            ASPxTextBoxOtherStructureSkeleton.ClientEnabled = false;
            ASPxTextBoxOtherStructureSkeleton.Text = "";
        }
    }

    protected void ASPxComboBoxRoofType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ASPxComboBoxRoofType.Text == "سایر موارد")
            ASPxTextBoxOtherRoofType.ClientEnabled = true;
        else
        {
            ASPxTextBoxOtherRoofType.ClientEnabled = false;
            ASPxTextBoxOtherRoofType.Text = "";
        }
    }

    #region WF
    private void CheckWorkFlowPermission()
    {
        PageMode = Utility.DecryptQS(PgMode.Value);
        CheckWorkFlowPermissionForEdit(PageMode);
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        //*****TableId
        string PrjReId = Utility.DecryptQS(PkPrjReId.Value);
        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId(), PageMode);
        btnEdit.Enabled = WFPer.BtnEdit;
        btnEdit2.Enabled = WFPer.BtnEdit;
        btnSave.Enabled = WFPer.BtnSave;
        btnSave2.Enabled = WFPer.BtnSave;
        btnNew.Enabled = WFPer.BtnNew;
        btnNew2.Enabled = WFPer.BtnNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;
    }
    #endregion

    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
    }

    //private int GetCurrentGroupId()
    //{
    //    int ProjectId = Convert.ToInt32(Utility.DecryptQS(PkProjectId.Value));
    //    TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
    //    ProjectManager.FindByProjectId(ProjectId);
    //    if (ProjectManager.Count == 1)
    //        return Convert.ToInt32(ProjectManager[0]["GroupId"]);
    //    else return -1;
    //}

    private bool CheckStepByGroup(int Step)
    {
        TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager PriceArchiveStructureItemsManager = new TSP.DataManager.TechnicalServices.PriceArchiveStructureItemsManager();
        try
        {
            int PrjReId = Convert.ToInt32(Utility.DecryptQS(PkPrjReId.Value));
            ArrayList ArrStepRange = new ArrayList();
            ArrStepRange = PriceArchiveStructureItemsManager.GetCurrentStepFromAndStepTo(PrjReId);

            if (Convert.ToInt32(ArrStepRange[0]) == -1 || Convert.ToInt32(ArrStepRange[1]) == -1)
            {
                SetLabelWarning("زیربنا یا گروه ساختمانی پروژه نامشخص است");
                return false;
            }

            //------------error-------------------------
            if (Convert.ToInt32(ArrStepRange[1]) != -2)
                if (Step > Convert.ToInt32(ArrStepRange[1]))
                {
                    SetLabelWarning("هشدار : با توجه به زیربنا و گروه ساختمانی پروژه تعداد طبقات باید در محدوده "
                                            + ArrStepRange[0].ToString() + " تا " + ArrStepRange[1].ToString() + " طبقه باشد");
                    return false;
                }

            //----------------just warning--------------
            if (Step < Convert.ToInt32(ArrStepRange[0]))
            {
                string Msg = "";
                if (Convert.ToInt32(ArrStepRange[1]) != -2)
                    Msg = "هشدار : با توجه به زیربنا و گروه ساختمانی پروژه تعداد طبقات باید در محدوده "
                                            + ArrStepRange[0].ToString() + " تا " + ArrStepRange[1].ToString() + " طبقه باشد";
                else
                    Msg = "هشدار : با توجه به زیربنا و گروه ساختمانی پروژه تعداد طبقات باید  "
                                            + ArrStepRange[0].ToString() + " به بالا باشد ";
                SetLableWarningInfo(Msg, true);
            }
            else
            {
                SetLableWarningInfo("", false);
            }
            return true;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetLabelWarning("اطلاعات تعرفه خدمات مهندسی یا گروه ساختمانی نامشخص است");
            return false;
        }
    }

    private void SetLableWarningInfo(string Message, Boolean ImgVisible)
    {
        lblWarning.Visible = true;
        ImgWarningMsg.ClientVisible = ImgVisible;
        Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script>Blink('bkImgWarningMsg');</script>");
        lblWarning.Text = Message;
    }

    private bool CheckFoundationWithProject(double Foundation)
    {
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
        double ProjectFoundation = -1;
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(PkProjectId.Value));
        int BlockId = Convert.ToInt32(Utility.DecryptQS(PkBlockId.Value));
        int PrjReId = Convert.ToInt32(Utility.DecryptQS(PkPrjReId.Value));

       DataTable dtBlock= BlockManager.SelectTSBlockByProject(ProjectId);
        for (int i = 0; i < dtBlock.Rows.Count; i++)
            if (BlockId != Convert.ToInt32(dtBlock.Rows[i]["BlockId"]))
                Foundation = Convert.ToDouble(dtBlock.Rows[i]["Foundation"]) + Foundation;

        ProjectRequestManager.FindByCode(PrjReId);
        if (ProjectRequestManager.Count > 0)
            ProjectFoundation = Convert.ToDouble(ProjectRequestManager[0]["Foundation"]);

        if (Foundation > ProjectFoundation) return false;
        else return true;
    }

    //-------just for warning-------------------
    public void CheckCurrentStepsByGroup(int PrjReId)
    {
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
        string msg = BlockManager.CheckCurrentMaxStepByGroup(PrjReId);
        if (msg != null)
        {
            lblWarning.Text = msg;
            ImgWarningMsg.ClientVisible = true;
            Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script>Blink('bkImgWarningMsg');</script>");
        }
        else
        {
            ImgWarningMsg.ClientVisible = false;
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