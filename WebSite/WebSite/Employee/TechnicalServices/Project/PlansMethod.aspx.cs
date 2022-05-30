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

public partial class Employee_TechnicalServices_Project_PlansMethod : System.Web.UI.Page
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
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.PlansMethodManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnPrint.Enabled = per.CanView;
            btnPrint2.Enabled = per.CanView;
            btnInActive.Enabled = per.CanDelete;
            btnInActive2.Enabled = per.CanDelete;

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["PrjReId"])) || (string.IsNullOrEmpty(Request.QueryString["ProjectId"])))
            {
                Response.Redirect("Project.aspx");
                return;
            }

            if (!per.CanView)
                CustomAspxDevGridView1.Visible = false;

            SetKey();
            FillProjectInfo(int.Parse(PrjReId));
            SetProjectMenuEnabled();
            SetProjectMainMenuEnabled();

            CheckWorkFlowPermission();
            if (!_CanEditProjectInfoInThisRequest)
                btnEdit.Enabled = btnEdit2.Enabled = btnInActive.Enabled = btnInActive2.Enabled = btnNew.Enabled = btnNew2.Enabled = false;                    
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnPrint"] = btnPrint.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;

        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnPrint"] != null)
            this.btnPrint.Enabled = this.btnPrint2.Enabled = (bool)this.ViewState["BtnPrint"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive.Enabled = (bool)this.ViewState["BtnInActive"];


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        Session["DataTable"] = CustomAspxDevGridView1.Columns;
        Session["DataSource"] = ObjectDataSourcePlansMethod;
        Session["Title"] = "دستور نقشه";
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
    

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        InActive();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Utility.DecryptQS(PkProjectId.Value) == "-1")
            Response.Redirect("Project.aspx");

        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string Qs = "ProjectId=" + PkProjectId.Value + "&PrjReId=" + PkPrjReId.Value + "&PageMode=" + PgMode.Value +
            "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
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

    /******************************************************* ProjectMenu **************************************************************/
    protected void ProjectMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        PrjReId = PkPrjReId.Value;
        PageMode = PgMode.Value;

        PrjMenu PrjMenu = new PrjMenu("PlansMethod", Convert.ToInt32(ProjectId));
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect(PrjMenu.GetRedirectLink(e.Item.Name, PrjReId, PageMode, GrdFlt, SrchFlt));
    }
    #endregion

    #region Grid
    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
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

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "RegisteredDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "PlansMethodNo":
                e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";
                break;

            case "RegisteredDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }
    #endregion
    #endregion

    #region Methods

    private void SetKey()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
            PkProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"]).ToString();
            PkPrjReId.Value = Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString();

            ProjectId = Utility.DecryptQS(PkProjectId.Value);
            PrjReId = Utility.DecryptQS(PkPrjReId.Value);
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

            ObjectDataSourcePlansMethod.SelectParameters[0].DefaultValue = ProjectId;
            ObjectDataSourcePlansMethod.SelectParameters[1].DefaultValue = PrjReId;
            CheckMenueViewPermission();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }
    /***********************************************************************************************************************************/
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

    private void NextPage(string Mode)
    {
        int PlansMethodId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1 && Mode != "New")
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            PlansMethodId = (int)row["PlansMethodId"];
        }

        if (PlansMethodId == -1 && Mode != "New")
        {
            SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();
            string QS = "PlansMethodId=" + Utility.EncryptQS(PlansMethodId.ToString())
                + "&PageMode=" + Utility.EncryptQS(Mode)
                + "&ProjectId=" + Server.HtmlDecode(Request.QueryString["ProjectId"])
                + "&PrjReId=" + Server.HtmlDecode(Request.QueryString["PrjReId"])
                + "&MPgMode=" + PgMode.Value
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
            Response.Redirect("PlansMethodInsert.aspx?" + QS);
        }
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

    #region InActive-Delete
    private void InActive()
    {
        PrjReId = Utility.DecryptQS(PkPrjReId.Value);

        if (string.IsNullOrEmpty(PrjReId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        int PlansMethodId = -1;
        int ProjectReqId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            PlansMethodId = (int)row["PlansMethodId"];
            ProjectReqId = (int)row["PrjReId"];
        }

        if (PlansMethodId == -1)
        {
            SetLabelWarning("لطفاًابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager = new TSP.DataManager.TechnicalServices.PlansMethodManager();
            PlansMethodManager.FindByPlansMethodId(Convert.ToInt32(PlansMethodId));
            if (PlansMethodManager.Count == 1)
            {
                try
                {
                    if (Convert.ToInt32(PrjReId) == ProjectReqId)
                        DeletePlansMethod(PlansMethodManager);
                    else
                        InActivePlansMethod(PlansMethodManager);

                    CustomAspxDevGridView1.DataBind();
                    SetLabelWarning("ذخیره انجام شد");
                }
                catch (Exception err)
                {
                    SetError(err);
                }
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
    }

    private void DeletePlansMethod(TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager)
    {
        PlansMethodManager[0].Delete();
        PlansMethodManager.Save();
    }

    private void InActivePlansMethod(TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager)
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        PrjReId = Utility.DecryptQS(PkPrjReId.Value);

        if (string.IsNullOrEmpty(PrjReId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        PlansMethodManager[0].BeginEdit();
        PlansMethodManager[0]["InActive"] = 1;
        PlansMethodManager[0]["InActiveDate"] = Utility.GetDateOfToday();
        PlansMethodManager[0]["PrjReId"] = PrjReId;
        PlansMethodManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
        PlansMethodManager[0]["ModifiedDate"] = DateTime.Now;
        PlansMethodManager[0].EndEdit();

        PlansMethodManager.Save();

    }
    #endregion

    #region SetMenu
    private void SetProjectMenuEnabled()
    {
        ProjectId = Utility.DecryptQS(PkProjectId.Value);
        if (ProjectId == "-1")
            ProjectId = "-2";

        PrjMenu PrjMenu = new PrjMenu("PlansMethod", Convert.ToInt32(ProjectId));
        ProjectMenu.Items.FindByName("PlansMethod").Selected = true; //BaseInfo
        ProjectMenu.Items.FindByName("Insurance").Enabled = PrjMenu.GetEnabled("Insurance");
        ProjectMenu.Items.FindByName("Block").Enabled = PrjMenu.GetEnabled("Block");
        ProjectMenu.Items.FindByName("PlansMethod").Enabled = PrjMenu.GetEnabled("PlansMethod");
        ProjectMenu.Items.FindByName("RegisteredNo").Enabled = PrjMenu.GetEnabled("RegisteredNo");
        ProjectMenu.Items.FindByName("BaseInfo").Enabled = PrjMenu.GetEnabled("BaseInfo");
    }

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
        MainMenu.Items.FindByName("Implementer").Enabled = PrjMainMenu.GetEnabled("Implementer");
        MainMenu.Items.FindByName("Observers").Enabled = PrjMainMenu.GetEnabled("Observers");
        MainMenu.Items.FindByName("Plans").Enabled = PrjMainMenu.GetEnabled("Plans");
        MainMenu.Items.FindByName("Owner").Enabled = PrjMainMenu.GetEnabled("Owner");
        MainMenu.Items.FindByName("Project").Enabled = PrjMainMenu.GetEnabled("Project");
        MainMenu.Items.FindByName("Accounting").Enabled = PrjMainMenu.GetEnabled("Accounting");
        MainMenu.Items.FindByName("Designer").Enabled = PrjMainMenu.GetEnabled("Designer");
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
        MainMenu.Items.FindByName("Implementer").Visible = PrjMainMenuPer.CanViewImplementer;
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
        this.ViewState["BtnNew"] = btnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew;
        this.ViewState["BtnInActive"] = btnInActive.Enabled = btnInActive2.Enabled = WFPer.BtnInactive;
    }
    #endregion

    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
        _CanEditProjectInfoInThisRequest = prjInfo.CanEditProjectInfoInThisRequest;
    }
    #endregion
}