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

public partial class Employee_TechnicalServices_Project_Owner : System.Web.UI.Page
{
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
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {

            Session["SendBackDataTable_EmpPrjOwner"] = "";
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.OwnerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnInActive.Enabled = per.CanEdit;
            btnInActive2.Enabled = per.CanEdit;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnAccOwner.Enabled = btnAccOwner2.Enabled = per.CanView;

            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Project.aspx");
            }

            if (!per.CanView)
                GridViewOwner.Visible = false;

            SetKey();
            SetProjectMainMenuEnabled();

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["btnAccOwner"] = btnAccOwner.Enabled;
        }

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnAccOwner"] != null)
            this.btnAccOwner.Enabled = this.btnAccOwner2.Enabled = (bool)this.ViewState["btnAccOwner"];
    }

    #region btn Click
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        InActive();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Utility.DecryptQS(HDProjectId.Value) == "-1")
            Response.Redirect("Project.aspx");

        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect("ProjectInsert.aspx?ProjectId=" + HDProjectId.Value + "&PageMode=" + PgMode.Value + "&PrjReId=" + RequestId.Value
             + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
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

    protected void btnAccOwner_Click(object sender, EventArgs e)
    {
        TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        OwnerManager.FindOwnerAgent(ProjectId);
        if (OwnerManager.Count != 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = ("نماینده مالکین نامشخص است.جهت ثبت فیش پنج در هزار بایستی نماینده مالکین مشخص باشد.");
            return;
        }
        string OwnerId = OwnerManager[0]["OwnerId"].ToString();

        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "&ProjectId=" + HDProjectId.Value +
                "&PrjReId=" + RequestId.Value +
                "&IngT=" + Utility.EncryptQS(((int)TSP.DataManager.TSProjectIngridientType.Owner).ToString()) +
                "&PageMode=" + PgMode.Value +
                "&tbtId=" + Utility.EncryptQS(OwnerId) +
                "&UrlReferrer=" + Utility.EncryptQS("Owner.aspx") +
                "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        Response.Redirect("ProjectAccounting.aspx?" + QS);
    }
    #endregion

    #region Menu Click
    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        string PrjReId = RequestId.Value;
        string PageMode = PgMode.Value;

        PrjMainMenu MainMenu = new PrjMainMenu("Owner", Convert.ToInt32(ProjectId));
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, PrjReId, PageMode, GrdFlt, SrchFlt));
    }

    /*****************************************************************************************************************/
    protected void ASPxMenuOwner_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {

            case "Accounting":
                //Response.Redirect("OwnerAccounting.aspx?ProjectId=" + HDProjectId.Value + "&PageMode=" + PgMode.Value + "&PrjReId=" + RequestId.Value);
                string GrdFlt = Request.QueryString["GrdFlt"].ToString();
                string SrchFlt = Request.QueryString["SrchFlt"].ToString();
                Response.Redirect("OwnerAccounting.aspx?ProjectId=" + HDProjectId.Value
                  + "&PageMode=" + PgMode.Value
                  + "&PageMode2=" + Utility.EncryptQS("View")
                  + "&PrjReId=" + RequestId.Value
                  + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
                //Response.Redirect("OwnerAccountingInsert.aspx?ProjectId=" + HDProjectId.Value
                //    + "&PageMode=" + PgMode.Value
                //    + "&PageMode2=" + Utility.EncryptQS("View")
                //    + "&PrjReId=" + RequestId.Value
                //    + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
                break;

        }
    }
    #endregion

    #region Grid

    protected void GridViewOwner_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (RequestId.Value != null)
        {
            string PrjReId = Utility.DecryptQS(RequestId.Value);
            if (e.GetValue("PrjReId") == null)
                return;
            string CurretnPrjReId = e.GetValue("PrjReId").ToString();
            if (PrjReId == CurretnPrjReId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void GridViewOwner_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "CreateDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewOwner_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "CreateDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }
    #endregion

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int ProjectReId = Convert.ToInt32(Utility.DecryptQS(RequestId.Value));
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

        string Qs = "~/Employee/TechnicalServices/Project/Owner.aspx?ProjectId=" + HDProjectId.Value
            + "&PrjReId=" + RequestId.Value
            + "&PageMode=" + PgMode.Value
              + "&GrdFlt=" + Request.QueryString["GrdFlt"].ToString()
              + "&SrchFlt=" + Request.QueryString["SrchFlt"].ToString();
            
        WFUserControl.QueryStringForRedirect = Qs;
        WFUserControl.PerformCallback(ProjectReId, ProjectReTableType, WfCode, e);
    }
    #endregion

    #region Methods

    /*****************************************************************************************************************/
    private void SetKey()
    {
        try
        {
            HDProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"].ToString());
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();
            RequestId.Value = Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString();

            string ProjectId = Utility.DecryptQS(HDProjectId.Value);
            string PrjReId = Utility.DecryptQS(RequestId.Value);
            string PageMode = Utility.DecryptQS(PgMode.Value);

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(PrjReId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            if (ProjectId == "-1")
            {
                ASPxMenuOwner.Visible = false;
                MainMenu.Visible = false;
                prjInfo.Visible = false;
            }

            ObjectDataSourceOwner.SelectParameters[0].DefaultValue = ProjectId;
            ObjectDataSourceOwner.SelectParameters[1].DefaultValue = PrjReId;

            FillProjectInfo(int.Parse(PrjReId));
            CheckWorkFlowPermission();
            CheckMenueViewPermission();
            if (!_CanEditProjectInfoInThisRequest)
                btnAccOwner.Enabled = btnAccOwner2.Enabled = btnEdit.Enabled = btnEdit2.Enabled
                    = btnInActive.Enabled = btnInActive2.Enabled = BtnNew.Enabled = btnNew2.Enabled= false;
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    /*****************************************************************************************************************/
    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
        _CanEditProjectInfoInThisRequest = prjInfo.CanEditProjectInfoInThisRequest;
    }

    private void NextPage(string Mode)
    {
        int OwnerId = -1;

        if (Mode != "New")
        {
            if (GridViewOwner.FocusedRowIndex > -1)
            {
                DataRow row = GridViewOwner.GetDataRow(GridViewOwner.FocusedRowIndex);
                OwnerId = (int)row["OwnerId"];
            }
        }

        if (OwnerId == -1 && Mode != "New")
        {
            SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();
            string QS = "ProjectId=" + HDProjectId.Value + "&OwnerId=" + Utility.EncryptQS(OwnerId.ToString())
                + "&PageMode=" + PgMode.Value
                + "&PageMode2=" + Utility.EncryptQS(Mode)
                + "&PrjReId=" + RequestId.Value
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

            Response.Redirect("OwnerInsert.aspx?" + QS);
        }
    }

    private void InActive()
    {
        try
        {
            int OwnerId = -1;
            int PrjReId = -1;

            if (GridViewOwner.FocusedRowIndex > -1)
            {
                DataRow row = GridViewOwner.GetDataRow(GridViewOwner.FocusedRowIndex);
                OwnerId = (int)row["OwnerId"];
                PrjReId = (int)row["PrjReId"];
            }
            if (OwnerId == -1)
            {
                SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            }
            else
            {
                TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();

                OwnerManager.FindByOwnerId(OwnerId);
                if (OwnerManager.Count == 1)
                {
                    if (Convert.ToBoolean(OwnerManager[0]["IsAgent"]))
                    {
                        SetLabelWarning("نماینده مالکان را نمی توانید حذف یا غیر فعال نمایید");
                        return;
                    }
                    int CurrentPrjReId = int.Parse(Utility.DecryptQS(RequestId.Value));

                    if (PrjReId == CurrentPrjReId)
                    {
                        OwnerManager[0].Delete();
                    }
                    else
                    {
                        if (Convert.ToBoolean(OwnerManager[0]["InActive"]))
                        {
                            SetLabelWarning("رکورد مورد نظر غیر فعال می باشد");
                            return;
                        }
                        else
                        {
                            OwnerManager[0].BeginEdit();
                            OwnerManager[0]["InActive"] = 1;
                            OwnerManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            OwnerManager[0]["InactiveDate"] = Utility.GetDateOfToday();
                            OwnerManager[0].EndEdit();
                        }
                    }
                    int cn = OwnerManager.Save();
                    if (cn == 1)
                    {
                        GridViewOwner.DataBind();
                        SetLabelWarning("ذخیره انجام شد");
                    }
                    else
                    {
                        SetLabelWarning("خطایی در ذخیره انجام گرفته است");
                    }
                }
                else
                {
                    SetLabelWarning("اطلاعات توسط کاربر دیگری تغییر یافته است");
                }
            }
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    #region Warning Methos
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

    private void SetProjectMainMenuEnabled()
    {
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        if (ProjectId == "-1")
            ProjectId = "-2";

        PrjMainMenu PrjMainMenu = new PrjMainMenu("Owner", Convert.ToInt32(ProjectId));
        MainMenu.Items.FindByName("Owner").Selected = true; //Owner
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

    #region WorkFlow Methods
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableId
        string PrjReId = Utility.DecryptQS(RequestId.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.TSProjectRequest;
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, Convert.ToInt32(PrjReId), Utility.GetCurrentUser_UserId());
        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit;
        this.ViewState["BtnInActive"] = btnInActive.Enabled = btnInActive2.Enabled = WFPer.BtnInactive;
        this.ViewState["BtnNew"] = BtnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew;
    }
    #endregion
    #endregion
}
