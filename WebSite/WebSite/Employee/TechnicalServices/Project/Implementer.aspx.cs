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

public partial class Employee_TechnicalServices_Project_Implementer : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["ImpId"] = null;

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.Project_ImplementerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnInActive.Enabled = per.CanEdit;
            btnInActive2.Enabled = per.CanEdit;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnAccImp.Enabled = btnAccImp2.Enabled = per.CanView;
            CustomAspxDevGridView1.Visible = per.CanView;
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
            this.ViewState["btnAccImp"] = btnAccImp.Enabled;
        }

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnAccImp"] != null)
            this.btnAccImp.Enabled = this.btnAccImp2.Enabled = (bool)this.ViewState["btnAccImp"];
    }

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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect("ProjectInsert.aspx?ProjectId=" + HDProjectId.Value + "&PageMode=" + PgMode.Value
            + "&PrjReId=" + RequestId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
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
        TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImpManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        TSP.DataManager.TechnicalServices.ImplementerAgentManager ImplementerAgentManager = new TSP.DataManager.TechnicalServices.ImplementerAgentManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        trans.Add(ProjectImpManager);
        trans.Add(AccountingManager);
        trans.Add(ProjectCapacityDecrementManager);
        try
        {
            int PrjImpId = -1;
            int PrjReId = -1;
            int ProjectId = -1;

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {
                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                PrjImpId = (int)row["PrjImpId"];
                PrjReId = (int)row["PrjReId"];
                ProjectId = (int)row["ProjectId"];
            }
            if (PrjImpId == -1)
            {
                SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            }
            else
            {
                trans.BeginSave();
                ProjectImpManager.FindByPrjImpId(PrjImpId);
                if (ProjectImpManager.Count == 1)
                {
                    int CurrentPrjReId = int.Parse(Utility.DecryptQS(RequestId.Value));

                    if (PrjReId == CurrentPrjReId)
                    {
                        ImplementerAgentManager.FindByProjectIdPrjImpIdPrjReId(ProjectId, PrjImpId, PrjReId);
                        int len = ImplementerAgentManager.Count;
                        for (int i = 0; i < len; i++)
                            ImplementerAgentManager[0].Delete();
                        ImplementerAgentManager.Save();

                        AccountingManager.FindByTableTypeId(PrjImpId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Implementer));
                        int acclen = AccountingManager.Count;
                        for (int i = 0; i < acclen; i++)
                            AccountingManager[0].Delete();
                        AccountingManager.Save();

                        ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(ProjectId, PrjImpId, (int)TSP.DataManager.TSProjectIngridientType.Implementer, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                        int lendec = ProjectCapacityDecrementManager.Count;
                        for (int i = 0; i < lendec; i++)
                            ProjectCapacityDecrementManager[0].Delete();
                        ProjectCapacityDecrementManager.Save();

                        ProjectImpManager[0].Delete();
                    }
                    else
                    {
                        if (Convert.ToBoolean(ProjectImpManager[0]["InActive"]))
                        {
                            SetLabelWarning("رکورد مورد نظر غیر فعال می باشد");
                            trans.CancelSave();
                            return;
                        }
                        else
                        {
                            ProjectImpManager[0].BeginEdit();
                            ProjectImpManager[0]["InActive"] = 1;
                            ProjectImpManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            ProjectImpManager[0]["InactiveDate"] = Utility.GetDateOfToday();
                            ProjectImpManager[0].EndEdit();
                        }
                    }
                    int cn = ProjectImpManager.Save();
                    if (cn == 1)
                    {
                        trans.EndSave();
                        CustomAspxDevGridView1.DataBind();
                        SetLabelWarning("ذخیره انجام شد");
                    }
                    else
                    {
                        SetLabelWarning("خطایی در ذخیره انجام گرفته است");
                        trans.CancelSave();
                    }

                }
                else
                {
                    SetLabelWarning("اطلاعات توسط کاربر دیگری تغییر یافته است");
                    trans.CancelSave();
                }
            }
        }
        catch (Exception Err)
        {
            trans.CancelSave();
            SetError(Err);
            Utility.SaveWebsiteError(Err);
        }
    }

    protected void btnAgent_Click(object sender, EventArgs e)
    {
        NextPage("Imp");
    }

    protected void btnAccImp_Click(object sender, EventArgs e)
    {
        TSP.DataManager.TechnicalServices.Project_ImplementerManager Project_ImplementerManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        Project_ImplementerManager.FindImpMother(ProjectId);
        if (Project_ImplementerManager.Count != 1)
        {
            SetLabelWarning("نماینده مجریان نامشخص است.جهت ثبت فیش مجریان بایستی نماینده مجریان مشخص باشد.");
            return;
        }
        string PrjImpId = Project_ImplementerManager[0]["PrjImpId"].ToString();

        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "&ProjectId=" + HDProjectId.Value +
                "&PrjReId=" + RequestId.Value +
                "&IngT=" + Utility.EncryptQS(((int)TSP.DataManager.TSProjectIngridientType.Implementer).ToString()) +
                "&PageMode=" + PgMode.Value +
                "&tbtId=" + Utility.EncryptQS(PrjImpId.ToString()) +
                "&UrlReferrer=" + Utility.EncryptQS("Implementer.aspx") +
                "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        Response.Redirect("ProjectAccounting.aspx?" + QS);
    }
    /********************************************************************************************************************************************/
    private void SetKeys()
    {
        try
        {
            HDProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"].ToString());
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"]).ToString();
            RequestId.Value = Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString();

            string ProjectId = Utility.DecryptQS(HDProjectId.Value);
            string PrjReId = Utility.DecryptQS(RequestId.Value);
            string PageMode = Utility.DecryptQS(PgMode.Value);

            if (string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(PrjReId) || string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            ObjectDataSource1.SelectParameters[0].DefaultValue = ProjectId;
            ObjectDataSource1.SelectParameters[1].DefaultValue = PrjReId;

            FillProjectInfo(int.Parse(PrjReId));
            SetProjectMainMenuEnabled();

            CheckWorkFlowPermission();
            if (!_CanEditProjectInfoInThisRequest)
                btnAccImp.Enabled = btnAccImp2.Enabled = btnAgent.Enabled = btnAgent1.Enabled = btnEdit.Enabled = btnEdit2.Enabled = btnInActive.Enabled =
                    btnInActive2.Enabled = BtnNew.Enabled = BtnNew.Enabled = btnNew2.Enabled = false;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    private void NextPage(string Mode)
    {
        int PrjImpId = -1;
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        if (CustomAspxDevGridView1.FocusedRowIndex > -1 && Mode != "New")
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            PrjImpId = (int)row["PrjImpId"];
        }
        if (PrjImpId == -1 && Mode != "New")
        {
            SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            string QS = "ProjectId=" + HDProjectId.Value + "&PrjImpId=" + Utility.EncryptQS(PrjImpId.ToString())
                + "&PageMode=" + PgMode.Value
                + "&PrjReId=" + RequestId.Value
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
            if (Mode == "Imp")
            {
                QS = QS + "&Mode=" + Utility.EncryptQS("Imp");
                Response.Redirect("ImplementerAgent.aspx?" + QS);
            }
            else
            {
                QS = QS + "&PageMode2=" + Utility.EncryptQS(Mode);
                Response.Redirect("ImplementerInsert.aspx?" + QS);
            }
        }
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
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

    /********************************************************************************************************************************************/
    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
        _CanEditProjectInfoInThisRequest = prjInfo.CanEditProjectInfoInThisRequest;
    }

    /********************************************************************************************************************************************/
    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        string PrjReId = RequestId.Value;
        string PageMode = PgMode.Value;

        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        PrjMainMenu MainMenu = new PrjMainMenu("Implementer", Convert.ToInt32(ProjectId));
        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, PrjReId, PageMode, GrdFlt, SrchFlt));
    }

    private void SetProjectMainMenuEnabled()
    {
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        if (ProjectId == "-1")
            ProjectId = "-2";

        PrjMainMenu PrjMainMenu = new PrjMainMenu("Implementer", Convert.ToInt32(ProjectId));
        MainMenu.Items.FindByName("Implementer").Selected = true; //Implementer
        //MainMenu.Items.FindByName("StatusAnnouncement").Enabled = PrjMainMenu.GetEnabled("StatusAnnouncement");
        //MainMenu.Items.FindByName("BuildingsLicense").Enabled = PrjMainMenu.GetEnabled("BuildingsLicense");
        //MainMenu.Items.FindByName("Timing").Enabled = PrjMainMenu.GetEnabled("Timing");
        //MainMenu.Items.FindByName("Contract").Enabled = PrjMainMenu.GetEnabled("Contract");
        //MainMenu.Items.FindByName("Implementer").Enabled = PrjMainMenu.GetEnabled("Implementer");
        MainMenu.Items.FindByName("Observers").Enabled = PrjMainMenu.GetEnabled("Observers");
        MainMenu.Items.FindByName("Plans").Enabled = PrjMainMenu.GetEnabled("Plans");
        MainMenu.Items.FindByName("Owner").Enabled = PrjMainMenu.GetEnabled("Owner");
        MainMenu.Items.FindByName("Project").Enabled = PrjMainMenu.GetEnabled("Project");
        MainMenu.Items.FindByName("Accounting").Enabled = PrjMainMenu.GetEnabled("Accounting");
        MainMenu.Items.FindByName("Designer").Enabled = PrjMainMenu.GetEnabled("Designer");

    }

    /********************************************************************************************************************************************/
    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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

    protected void CustomAspxDevGridView1_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridViewDetailRowEventArgs e)
    {
        CustomAspxDevGridView1.FocusedRowIndex = e.VisibleIndex;

    }

    protected void CustomAspxDevGridView1_FocusedRowChanged(object sender, EventArgs e)
    {
        if (CustomAspxDevGridView1.FocusedRowIndex != -1 && CustomAspxDevGridView1.SettingsDetail.ShowDetailRow)
            CustomAspxDevGridView1.DetailRows.ExpandRow(CustomAspxDevGridView1.FocusedRowIndex);
    }

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "CreateDate":
                e.Cell.Style["direction"] = "ltr";
                break;

            case "Date":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "No":
                e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";
                break;

            case "CreateDate":
                e.Editor.Style["direction"] = "ltr";
                break;

            case "Date":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void CustomAspxDevGridView2_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["ImpId"] = (sender as ASPxGridView).GetMasterRowKeyValue();

    }

    protected void CustomAspxDevGridView2_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "CreateDate":
                e.Cell.Style["direction"] = "ltr";
                break;

            case "InActiveDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void CustomAspxDevGridView2_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
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

    /************************************************************* WorkFlow *********************************************************************/
    #region WorkFlow Methods
    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        string PrjReId = Utility.DecryptQS(RequestId.Value);
        int ProjectReTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest);
        //  int WfCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        int WfCode = -1;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dt = WorkFlowStateManager.SelectLastState(ProjectReTableType, int.Parse(PrjReId));
        if (dt.Rows.Count > 0)
            WfCode = Convert.ToInt32(dt.Rows[0]["WorkFlowCode"]);

        if (WfCode == -1)
        {
            WFUserControl.SetMsgText("گردش کار درخواست جاری نامشخص است");
            return;
        }

        WFUserControl.PerformCallback(int.Parse(PrjReId), ProjectReTableType, WfCode, e);
    }

    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //****TableId
        string PrjReId = Utility.DecryptQS(RequestId.Value);
        //****TableType

        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementerOfProject;

        int ChangeWFCode = (int)TSP.DataManager.WorkFlows.TSChangeImplementerConfirming;
        int ChangeTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveChangeProjectImplementerRequestInfo;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, Convert.ToInt32(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission ChangeWFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ChangeTaskCode, ChangeWFCode, Convert.ToInt32(PrjReId), Utility.GetCurrentUser_UserId());

        this.ViewState["BtnEdit"] = btnEdit.Enabled = btnEdit2.Enabled = WFPer.BtnEdit || ChangeWFPer.BtnEdit;
        this.ViewState["BtnInActive"] = btnInActive.Enabled = btnInActive2.Enabled = WFPer.BtnInactive || ChangeWFPer.BtnInactive;
        this.ViewState["BtnNew"] = BtnNew.Enabled = btnNew2.Enabled = WFPer.BtnNew || ChangeWFPer.BtnNew;
    }

    #endregion
}
