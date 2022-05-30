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

public partial class Employee_TechnicalServices_Project_Contract : System.Web.UI.Page
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

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ContractManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnInActive.Enabled = per.CanEdit;
            btnInActive2.Enabled = per.CanEdit;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            CustomAspxDevGridView1.Visible = per.CanView;


            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]))
            {
                Response.Redirect("Project.aspx");
            }

            SetKey();
            SetProjectMainMenuEnabled();

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            if (!_CanEditProjectInfoInThisRequest)
                BtnNew.Enabled = btnNew2.Enabled = btnInActive.Enabled = btnInActive2.Enabled = btnEdit.Enabled = btnEdit2.Enabled = false;
        }

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
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
        Response.Redirect("ProjectInsert.aspx?ProjectId=" + HDProjectId.Value
            + "&PageMode=" + PgMode.Value + "&PrjReId=" + RequestId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();
            Response.Redirect("Project.aspx?PostId=" + HDProjectId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("Project.aspx");
        }
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        Delete_Inactive();
    }

    /*********************************************************************************************************************************************/
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


            if (string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(PrjReId) || string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            ObjectDataSourceContractManager.SelectParameters["ProjectId"].DefaultValue = ProjectId;
            ObjectDataSourceContractManager.SelectParameters["PrjReId"].DefaultValue = PrjReId;

            FillProjectInfo(int.Parse(PrjReId));
            CheckWorkFlowPermission();
        }
        catch (Exception Err)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            Utility.SaveWebsiteError(Err);
        }
    }

    private void NextPage(string Mode)
    {
        int ContractId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            ContractId = (int)row["ContractId"];
        }
        if (ContractId == -1 && Mode != "New")
        {
            SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            if (Mode == "Edit" && !CheckWorkFlowPermissionForBtnEdit(ContractId))
                return;

            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();
            string QS = "ProjectId=" + HDProjectId.Value
                + "&ContractId=" + Utility.EncryptQS(ContractId.ToString())
                + "&PageMode=" + PgMode.Value
                + "&PageMode2=" + Utility.EncryptQS(Mode)
                + "&PrjReId=" + RequestId.Value
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
            Response.Redirect("ContractInsert.aspx?" + QS);
        }
    }

    private void Delete_Inactive()
    {
        int ContractId = -1;
        int PrjReId = -1;
        int InActiveStatus = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            ContractId = (int)row["ContractId"];
            PrjReId = (int)row["PrjReId"];
            InActiveStatus = (int)row["InActiveStatus"];

        }
        if (Convert.ToBoolean(InActiveStatus))
        {
            SetLabelWarning("رکورد مورد نظر غیر فعال می باشد");
            return;
        }
        if (ContractId == -1)
        {
            SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            Inactive(ContractId, PrjReId);
        }

    }

    private void Inactive(int ContractId, int PrjReId)
    {
        TSP.DataManager.TechnicalServices.ContractManager ContractManager = new TSP.DataManager.TechnicalServices.ContractManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        trans.Add(ContractManager);

        try
        {
            trans.BeginSave();
            ContractManager.FindByContractId(ContractId);
            if (ContractManager.Count == 1)
            {
                int CurrentPrjReId = int.Parse(Utility.DecryptQS(RequestId.Value));

                if (PrjReId == CurrentPrjReId)
                {
                    ContractManager[0].Delete();
                }
                else
                {
                    if (Convert.ToBoolean(ContractManager[0]["InActive"]))
                    {
                        SetLabelWarning("رکورد مورد نظر غیر فعال می باشد");
                        trans.CancelSave();
                        return;
                    }
                    else
                    {
                        ContractManager[0].BeginEdit();
                        ContractManager[0]["InActive"] = 1;
                        ContractManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        ContractManager[0]["InActiveDate"] = Utility.GetDateOfToday();
                        ContractManager[0].EndEdit();
                    }
                    TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();

                    InsertInActive(ContractId, CurrentPrjReId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSContract), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest), Manager);
                }
                ContractManager.Save();
                trans.EndSave();
                CustomAspxDevGridView1.DataBind();
                SetLabelWarning("ذخیره انجام شد");
            }
            else
            {
                SetLabelWarning("اطلاعات توسط کاربر دیگری تغییر یافته است");
                trans.CancelSave();
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            SetLabelWarning("خطایی در مشاهده اطلاعات رخ داده است");
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

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    /*********************************************************************************************************************************************/
    protected void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
        _CanEditProjectInfoInThisRequest = prjInfo.CanEditProjectInfoInThisRequest;
    }

    /*********************************************************************************************************************************************/
    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        string PrjReId = RequestId.Value;
        string PageMode = PgMode.Value;

        PrjMainMenu MainMenu = new PrjMainMenu("Contract", Convert.ToInt32(ProjectId));
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, PrjReId, PageMode, GrdFlt, SrchFlt));
    }

    private void SetProjectMainMenuEnabled()
    {
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        if (ProjectId == "-1")
            ProjectId = "-2";

        PrjMainMenu PrjMainMenu = new PrjMainMenu("Contract", Convert.ToInt32(ProjectId));
        MainMenu.Items.FindByName("Contract").Selected = true; //Contract
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

    /*********************************************************************************************************************************************/
    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
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

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "ContractDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }

        switch (e.DataColumn.FieldName)
        {
            case "CreateDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }

        switch (e.DataColumn.FieldName)
        {
            case "InActiveDate":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "ContractDate":
                e.Editor.Style["direction"] = "ltr";
                break;

            case "CreateDate":
                e.Editor.Style["direction"] = "ltr";
                break;

            case "InActiveDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    /***************************************************WorkFlow**********************************************************/
    #region WorkFlow Methods

    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        TSP.DataManager.WFPermission PerImp = CheckWorkFlowPermissionForImp();
        TSP.DataManager.WFPermission PerObs = CheckWorkFlowPermissionForObs();
        TSP.DataManager.WFPermission PerDes = CheckWorkFlowPermissionForDesign();

        BtnNew.Enabled = (PerImp.BtnNew || PerObs.BtnNew || PerDes.BtnNew);
        btnNew2.Enabled = (PerImp.BtnNew || PerObs.BtnNew || PerDes.BtnNew);
        btnEdit.Enabled = (PerImp.BtnEdit || PerObs.BtnEdit || PerDes.BtnEdit);
        btnEdit2.Enabled = (PerImp.BtnEdit || PerObs.BtnEdit || PerDes.BtnEdit);
        btnInActive.Enabled = (PerImp.BtnInactive || PerObs.BtnInactive || PerDes.BtnInactive);
        btnInActive2.Enabled = (PerImp.BtnInactive || PerObs.BtnInactive || PerDes.BtnInactive);

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnInActive"] = btnInActive.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    private TSP.DataManager.WFPermission CheckWorkFlowPermissionForImp()
    {
        //****TableId
        string PrjReId = Utility.DecryptQS(RequestId.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.TSProjectRequest;

        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int ImpTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementerOfProject;

        TSP.DataManager.WFPermission WFImpPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ImpTaskCode, WFCode, Convert.ToInt32(PrjReId), Utility.GetCurrentUser_UserId());

        return WFImpPer;
    }

    private TSP.DataManager.WFPermission CheckWorkFlowPermissionForObs()
    {
        //****TableId
        string PrjReId = Utility.DecryptQS(RequestId.Value);
        //****TableType
        int TableType = (int)TSP.DataManager.TableCodes.TSProjectRequest;

        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int ObsTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject;

        TSP.DataManager.WFPermission WFObsPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ObsTaskCode, WFCode, Convert.ToInt32(PrjReId), Utility.GetCurrentUser_UserId());

        return WFObsPer;
    }

    private TSP.DataManager.WFPermission CheckWorkFlowPermissionForDesign()
    {
        //****TableId
        string PrjReId = Utility.DecryptQS(RequestId.Value);
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        //*******Editing Task Code
        int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;
        int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;

        TSP.DataManager.WFPermission PerAtchitecturalPlanTask = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ArchitecturalPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructurePlanTask = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructurePlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerElectricalInsPlanTask = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ElectricalInsPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerMechanicInsPlanTask = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(MechanicInsPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());

        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = PerAtchitecturalPlanTask.BtnEdit || PerStructurePlanTask.BtnEdit || PerElectricalInsPlanTask.BtnEdit || PerMechanicInsPlanTask.BtnEdit;
        WFPer.BtnSave = PerAtchitecturalPlanTask.BtnSave || PerStructurePlanTask.BtnSave || PerElectricalInsPlanTask.BtnSave || PerMechanicInsPlanTask.BtnSave;
        WFPer.BtnNew = PerAtchitecturalPlanTask.BtnNew || PerStructurePlanTask.BtnNew || PerElectricalInsPlanTask.BtnNew || PerMechanicInsPlanTask.BtnNew;
        WFPer.BtnInactive = PerAtchitecturalPlanTask.BtnInactive || PerStructurePlanTask.BtnInactive || PerElectricalInsPlanTask.BtnInactive || PerMechanicInsPlanTask.BtnInactive;

        return WFPer;
    }
    private bool CheckWorkFlowPermissionForBtnEdit(int ContractId)
    {
        TSP.DataManager.TechnicalServices.ContractManager ContractManager = new TSP.DataManager.TechnicalServices.ContractManager();
        ContractManager.FindByContractId(ContractId);
        if (ContractManager.Count > 0)
        {
            int ProjectIngridientTypeId = Convert.ToInt32(ContractManager[0]["ProjectIngridientTypeId"]);

            TSP.DataManager.WFPermission PerImp = CheckWorkFlowPermissionForImp();
            TSP.DataManager.WFPermission PerObs = CheckWorkFlowPermissionForObs();
            TSP.DataManager.WFPermission PerDes = CheckWorkFlowPermissionForDesign();
            bool Imp = PerImp.BtnNew || PerImp.BtnSave || PerImp.BtnEdit;
            bool Obs = PerObs.BtnNew || PerObs.BtnSave || PerObs.BtnEdit;
            bool Des = PerDes.BtnNew || PerDes.BtnSave || PerDes.BtnEdit;

            switch (ProjectIngridientTypeId)
            {
                case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                    if (!Imp)
                    {
                        SetLabelWarning("در این مرحله از گردش کار نمی توانید این نوع طراح قرارداد را ویرایش کنید.");
                        return false;
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                    if (!Obs)
                    {
                        SetLabelWarning("در این مرحله از گردش کار نمی توانید این نوع طراح قرارداد را ویرایش کنید.");
                        return false;
                    }
                    break;

                case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                    if (!Des)
                    {
                        SetLabelWarning("در این مرحله از گردش کار نمی توانید قرارداد  طراح را ویرایش کنید.");
                        return false;
                    }
                    break;
            }

        }
        else
        {
            SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است.");
            return false;
        }

        return true;
    }

    #endregion
}
