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
using System.Drawing;

public partial class Employee_TechnicalServices_Project_Plans : System.Web.UI.Page
{
    string ProjectId;
    string PrjReId;
    string PrjPgMode;
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
        if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
        {
            Response.Redirect("Project.aspx");
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.TSPlansConfirming).ToString();

            TSP.DataManager.Permission perPlan = TSP.DataManager.TechnicalServices.PlansManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = perPlan.CanNew;
            BtnNew2.Enabled = perPlan.CanNew;
            btnEdit.Enabled = perPlan.CanEdit;
            btnEdit2.Enabled = perPlan.CanEdit;
            btnView.Enabled = perPlan.CanView;
            btnView2.Enabled = perPlan.CanView;
            GridViewPlans.Visible = perPlan.CanView;
            btnDesAcc2.Enabled = btnDesAcc.Enabled = perPlan.CanView;

            Session["SendBackDataTable_EmpPln"] = "";

            SetKeys();
            LoadWfHelp();
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["btnView"] = btnView.Enabled;
        }

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        if (this.ViewState["btnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["btnView"];
    }

    #region btn Click
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        EditNextPage();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldPlan["PageMode"].ToString());
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Utility.IsDBNullOrNullValue(Request.QueryString["SrchFlt"]) ? Utility.EncryptQS("") : Request.QueryString["SrchFlt"].ToString();
        if (PageMode == "ShowAll")
        {
            Response.Redirect("~/TechnicalServices/TsHome.aspx");
        }
        else
        {
            if (Utility.DecryptQS(HiddenFieldPlan["ProjectId"].ToString()) == "-1")
                Response.Redirect("Project.aspx");

            string QS = "ProjectId=" + HiddenFieldPlan["ProjectId"].ToString()
                + "&PrjReId=" + HiddenFieldPlan["PrjReId"].ToString()
                + "&PageMode=" + HiddenFieldPlan["PageMode"].ToString()
                + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
            Response.Redirect("ProjectInsert.aspx?" + QS);
        }
    }

    protected void btnBackToManagment_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("Project.aspx?PostId=" + HiddenFieldPlan["ProjectId"] + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("Project.aspx");
        }
    }

    protected void btnControler_Click(object sender, EventArgs e)
    {
        ControllerNextPage();
    }

    protected void btnDeleteReq_Click(object sender, EventArgs e)
    {
        DeleteReq();
    }

    protected void btnTracing_Click(object sender, EventArgs e)
    {
        Tracing();
    }

    protected void btnDesigner_Click(object sender, EventArgs e)
    {
        int PlansId = -1;
        if (GridViewPlans.FocusedRowIndex <= -1)
        {
            WFUserControl.SetMsgText("ردیفی انتخاب نشده است.");
            return;
        }
        DataRow row = GridViewPlans.GetDataRow(GridViewPlans.FocusedRowIndex);
        PlansId = (int)row["PlansId"];

        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Utility.IsDBNullOrNullValue(Request.QueryString["SrchFlt"]) ? Utility.EncryptQS("") : Request.QueryString["SrchFlt"].ToString();

        if (PlansId == -1)
        {
            SetLabelWarning("جهت مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        string QS = "ProjectId=" + HiddenFieldPlan["ProjectId"].ToString()
           + "&PrjReId=" + HiddenFieldPlan["PrjReId"].ToString()
           + "&PlnId=" + Utility.EncryptQS(PlansId.ToString())
           + "&PageMode=" + HiddenFieldPlan["PageMode"].ToString()
           + "&PlnPgMd=" + Utility.EncryptQS("View")
           + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        Response.Redirect("PlanDesigner.aspx?" + QS);
    }

    protected void btnDesAcc_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Utility.IsDBNullOrNullValue(Request.QueryString["SrchFlt"]) ? Utility.EncryptQS("") : Request.QueryString["SrchFlt"].ToString();
        string QS = "&ProjectId=" + HiddenFieldPlan["ProjectId"].ToString() +
                "&PrjReId=" + HiddenFieldPlan["PrjReId"].ToString() +
                "&IngT=" + Utility.EncryptQS(((int)TSP.DataManager.TSProjectIngridientType.Designer).ToString()) +
                "&PageMode=" + HiddenFieldPlan["PageMode"].ToString() +
                "&UrlReferrer=" + Utility.EncryptQS("Plans.aspx") +
                "&PlnPgMd=" + Utility.EncryptQS("View") +
                "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        Response.Redirect("ProjectAccountingDesigner.aspx?" + QS);

    }
    #endregion

    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        ProjectId = Utility.DecryptQS(HiddenFieldPlan["ProjectId"].ToString());
        PrjReId = HiddenFieldPlan["PrjReId"].ToString();
        PrjPgMode = HiddenFieldPlan["PageMode"].ToString();

        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Utility.IsDBNullOrNullValue(Request.QueryString["SrchFlt"]) ? Utility.EncryptQS("") : Request.QueryString["SrchFlt"].ToString();

        PrjMainMenu MainMenu = new PrjMainMenu("Plans", Convert.ToInt32(ProjectId));
        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, PrjReId, PrjPgMode, GrdFlt, SrchFlt));
    }

    #region Grid
    protected void GridViewPlanSubRe_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["PlansId"] = (int)(sender as ASPxGridView).GetMasterRowKeyValue();
    }


    protected void GridViewPlans_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (HiddenFieldPlan["PrjReId"] != null)
        {
            string PrjReId = Utility.DecryptQS(HiddenFieldPlan["PrjReId"].ToString());
            if (e.GetValue("PrjReId") == null)
                return;
            string CurretnPrjReId = e.GetValue("PrjReId").ToString();
            if (PrjReId == CurretnPrjReId)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }



    protected void GridViewPlans_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewPlans.DataBind();
    }


    protected void GridViewPlanSubRe_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "Date":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewPlanSubRe_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Date")
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int focucedIndex = -1;
        if (GridViewPlans.FocusedRowIndex <= -1)
        {
            WFUserControl.SetMsgText("ردیفی انتخاب نشده است.");
            return;
        }
        focucedIndex = GridViewPlans.FocusedRowIndex;
        if (focucedIndex > -1)
        {
            DataRow row = GridViewPlans.GetDataRow(focucedIndex);
            int PrjReId = (int)row["PrjReId"];
            int PlansId = (int)row["PlansId"];
            int ProjectReTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans);
            int WfCode = (int)row["WorkFlowCode"];
            WFUserControl.PerformCallback(PlansId, ProjectReTableType, WfCode, e);
            //if (e.Parameter == "Send")
            //{

            //    //SendDocToNextStep(PlansId, WfCode);
            //    WFUserControl.SendDocToNextStep(PlansId, WfCode, ProjectReTableType);
            //    GridViewPlans.DataBind();
            //}
            //else
            //{
            //    // SelectSendBackTask(ProjectReTableType, WfCode, PlansId);
            //    WFUserControl.SelectSendBackTask(ProjectReTableType, WfCode, PlansId);
            //}
            GridViewPlans.ExpandRow(focucedIndex);
        }

    }
    #endregion

    #region Methods
    /***************************************************************** NextPage**************************************************************/
    private void NextPage(string Mode)
    {
        int PlansId = -1;
        int focucedIndex = -1;

        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Utility.IsDBNullOrNullValue(Request.QueryString["SrchFlt"]) ? Utility.EncryptQS("") : Request.QueryString["SrchFlt"].ToString();
        if (Mode != "New")
        {
            if (GridViewPlans.FocusedRowIndex > -1)
            {
                focucedIndex = GridViewPlans.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = GridViewPlans.GetDataRow(focucedIndex);
                    PlansId = (int)row["PlansId"];
                }
            }
        }

        if (PlansId == -1 && Mode != "New")
            SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        else
        {
            if (Mode == "New" && !CheckPermissionForNew())
            {
                SetLabelWarning("با توجه به در جریان بودن درخواست ثبت نقشه معماری امکان ثبت مجدد نقشه وجود ندارد.");
                GridViewPlans.DataBind();
            }
            else
            {
                string QS = "PlnId=" + Utility.EncryptQS(PlansId.ToString())
                    + "&PrjId=" + HiddenFieldPlan["ProjectId"].ToString()
                    + "&PrjReId=" + HiddenFieldPlan["PrjReId"].ToString()
                    + "&PlnPgMd=" + Utility.EncryptQS(Mode)
                    + "&PrePgMd=" + HiddenFieldPlan["PageMode"].ToString()
                    + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
                Response.Redirect("AddPlans.aspx?" + QS);
            }
        }
    }

    private void EditNextPage()
    {
        if (!CheckWorkFlowPermissionForEdit())
        {
            SetLabelWarning("در این مرحله از گردش کار قادر به ویرایش اطلاعات نیستید.");
            return;
        }

        NextPage("Edit");
    }

    private void ControllerNextPage()
    {
        if (GridViewPlans.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPlans.GetDataRow(GridViewPlans.FocusedRowIndex);
            int PlansId = (int)row["PlansId"];
            int PrjReId = (int)row["PrjReId"];
            //if (CheckWorkFlowPermissionForControler(PrjReId, PlansId))
            //    NextPage("Controler");
            //else
            //    SetLabelWarning("شما قادر به انتخاب بازبین نقشه نمی باشید.");
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Utility.IsDBNullOrNullValue(Request.QueryString["SrchFlt"]) ? Utility.EncryptQS("") : Request.QueryString["SrchFlt"].ToString();

            string QS = "PrjReId=" + Utility.EncryptQS(PrjReId.ToString())
            + "&PlnId=" + Utility.EncryptQS(PlansId.ToString())
            + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
            QS = QS + "&PrjId=" + HiddenFieldPlan["ProjectId"].ToString() + "&PrePgMd=" + HiddenFieldPlan["PageMode"].ToString() + "&PlnPgMd=" + Utility.EncryptQS("View");
            Response.Redirect("PlanControlers.aspx?" + QS);
        }
        else
            SetLabelWarning("لطفاً برای انتخاب بازبین نقشه ابتدا یک رکورد را انتخاب نمائید");


    }

    /****************************************************************************************************************************************/
    private void SetKeys()
    {
        try
        {
            HiddenFieldPlan["PageMode"] = Request.QueryString["PageMode"];
            HiddenFieldPlan["PrjReId"] = Request.QueryString["PrjReId"];
            HiddenFieldPlan["ProjectId"] = Request.QueryString["ProjectId"];

            ProjectId = Utility.DecryptQS(HiddenFieldPlan["ProjectId"].ToString());
            PrjReId = Utility.DecryptQS(HiddenFieldPlan["PrjReId"].ToString());
            PrjPgMode = Utility.DecryptQS(HiddenFieldPlan["PageMode"].ToString());

            if (string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(PrjReId) || string.IsNullOrEmpty(PrjPgMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            if (PrjPgMode == "ShowAll" || ProjectId == "-1")
            {
                prjInfo.Visible = false;
                MainMenu.Visible = false;
                GridViewPlans.Columns["LicenseNo"].Visible = true;
                GridViewPlans.Columns["RegisteredNo"].Visible = true;
                GridViewPlans.Columns["FileNo"].Visible = true;

                BtnNew.Visible = false;
                BtnNew2.Visible = false;
                btnEdit.Visible = false;
                btnEdit2.Visible = false;
                btnDeleteReq.Visible = false;
                btnDeleteReq2.Visible = false;
                btnControler.Visible = false;
                btnControler2.Visible = false;
                btnSendNextStep.Visible = false;
                btnSendToNextStep2.Visible = false;
            }
            else
            {
                prjInfo.Visible = true;
                MainMenu.Visible = true;
                GridViewPlans.Columns["LicenseNo"].Visible = false;
                GridViewPlans.Columns["RegisteredNo"].Visible = false;
                GridViewPlans.Columns["FileNo"].Visible = false;

                FillProjectInfo(int.Parse(PrjReId));
                CheckWorkFlowPermission();
                SetProjectMainMenuEnabled();
            }

            ObjdsPlans.SelectParameters["ProjectId"].DefaultValue = ProjectId;
            if (!_CanEditProjectInfoInThisRequest)
                BtnNew.Enabled = BtnNew2.Enabled = btnEdit.Enabled = btnEdit2.Enabled =
             btnDeleteReq.Enabled = btnDeleteReq2.Enabled = btnControler.Enabled = btnControler2.Enabled = false;

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
        _CanEditProjectInfoInThisRequest = prjInfo.CanEditProjectInfoInThisRequest;
    }

    #region Set Warning-Error Messages
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
                SetLabelWarning("کد تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetLabelWarning("به علت وجود اطلاعات وابسته امکان حذف نمی باشد.");
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

    #endregion

    #region DeleteRequest
    private void DeleteReq()
    {
        if (GridViewPlans.FocusedRowIndex < 0)
        {
            SetLabelWarning("لطفاً برای حذف درخواست ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        DataRow row = GridViewPlans.GetDataRow(GridViewPlans.FocusedRowIndex);
        int PlansId = (int)row["PlansId"];
        int RequestType = int.Parse(row["Status"].ToString());
        if (Convert.ToInt32(row["IsConfirmed"]) != 0)
        {
            SetLabelWarning("تنها قادر به حذف نقشه های  با وضعیت 'درجریان' می باشید. ");
            return;
        }
        if (CheckWFPermissionForDeleteRequest(PlansId, RequestType) || Convert.ToInt32(row["IsDesAccepted"]) == 3)
        {
            DeleteRequest(PlansId, RequestType);
        }
        else
            SetLabelWarning("شما  قادر به لغو درخواست نمی باشید. ");
    }

    private void DeleteRequest(int PlansId, int RequestType)
    {
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
        TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();

        TSP.DataManager.TransactionManager Transact = new TSP.DataManager.TransactionManager();

        Transact.Add(PlansManager);
        Transact.Add(DesignerPlansManager);
        Transact.Add(ProjectDesignerManager);
        Transact.Add(ProjectOfficeMembersManager);
        Transact.Add(AttachmentsManager);
        Transact.Add(WorkFlowStateManager);
        Transact.Add(ProjectCapacityDecrementManager);

        try
        {
            Transact.BeginSave();

            DeleteDesignerPlans(DesignerPlansManager, ProjectDesignerManager, ProjectOfficeMembersManager, ProjectCapacityDecrementManager, PlansId);
            DeleteAttachments(AttachmentsManager, PlansId);
            if (!DeletePlans(PlansManager, PlansId))
            {
                Transact.CancelSave();
                SetLabelWarning("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            DeleteWorkFlowState(WorkFlowStateManager, PlansId, RequestType);

            Transact.EndSave();
            GridViewPlans.DataBind();
            SetLabelWarning("ذخیره انجام شد.");

        }
        catch (Exception err)
        {
            Transact.CancelSave();
            SetError(err);
            Utility.SaveWebsiteError(err);
        }
    }

    private bool DeletePlans(TSP.DataManager.TechnicalServices.PlansManager PlansManager, int PlansId)
    {
        PlansManager.FindByPlansId(PlansId);
        if (PlansManager.Count == 1)
        {
            PlansManager[0].Delete();
            PlansManager.Save();
            return true;
        }
        return false;
    }

    private void DeleteDesignerPlans(TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager, TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager, TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager, TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager, int PlansId)
    {
        DesignerPlansManager.FindByPlansId(PlansId);

        int Count = DesignerPlansManager.Count;
        for (int i = 0; i < Count; i++)
        {
            int PrjDesignerId = Convert.ToInt32(DesignerPlansManager[0]["PrjDesignerId"]);
            DesignerPlansManager[0].Delete();
        }
        DesignerPlansManager.Save();
    }

    private void DeleteAttachments(TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager, int PlansId)
    {
        AttachmentsManager.FindByTableTypeId(PlansId, (int)TSP.DataManager.TableCodes.TSPlans, -1);
        int Count = AttachmentsManager.Count;
        for (int i = 0; i < Count; i++)
        {
            AttachmentsManager[0].Delete();
        }
        AttachmentsManager.Save();
    }

    private void DeleteWorkFlowState(TSP.DataManager.WorkFlowStateManager WorkFlowStateManager, int PlansId, int RequestType)
    {
        int WFCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;
        WorkFlowStateManager.SelectByWorkFlowCodeForDelete(WFCode, PlansId);

        int Count = WorkFlowStateManager.Count;
        for (int i = 0; i < Count; i++)
        {
            WorkFlowStateManager[0].Delete();
        }
        WorkFlowStateManager.Save();
    }

    #endregion

    private void SetProjectMainMenuEnabled()
    {
        ProjectId = Utility.DecryptQS(HiddenFieldPlan["ProjectId"].ToString());
        if (ProjectId == "-1")
            ProjectId = "-2";

        PrjMainMenu PrjMainMenu = new PrjMainMenu("Plans", Convert.ToInt32(ProjectId));
        MainMenu.Items.FindByName("Plans").Selected = true; //Plans
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

    #region WorkFlow

    private void Tracing()
    {
        int focucedIndex = GridViewPlans.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            int PostId = int.Parse(GridViewPlans.GetDataRow(GridViewPlans.FocusedRowIndex)["PlansId"].ToString());
            string GridFilterString = GridViewPlans.FilterExpression;

            DataRow row = GridViewPlans.GetDataRow(focucedIndex);
            int TableId = Convert.ToInt32(row["PlansId"]);
            int TableType = (int)TSP.DataManager.TableCodes.TSPlans;
            int WorkFlowCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;


            String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) +
                        "&PostId=" + Utility.EncryptQS(PostId.ToString());

            string QS = "&ProjectId=" + HiddenFieldPlan["ProjectId"].ToString()
                + "&PrjReId=" + HiddenFieldPlan["PrjReId"].ToString()
                + "&PageMode=" + HiddenFieldPlan["PageMode"].ToString();

            Url = Url + QS;

            if (IsCallback)
                ASPxWebControl.RedirectOnCallback("../../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
            else
                Response.Redirect("../../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));

            //  Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
        }
        else
        {
            SetLabelWarning("ردیفی انتخاب نشده است.");

        }
    }

    #region WorkFlowPermission    
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //*****TableId
        PrjReId = Utility.DecryptQS(HiddenFieldPlan["PrjReId"].ToString());

        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        //*******Editing Task Code
        int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;
        int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;
        int StructureAndInstallationCode = (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject;

        TSP.DataManager.WFPermission PerAtchitecturalPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ArchitecturalPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructurePlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructurePlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerElectricalInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ElectricalInsPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerMechanicInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(MechanicInsPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructureAndInstallationPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructureAndInstallationCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());

        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        //btnEdit.Enabled = PerAtchitecturalPlan.BtnEdit || PerStructurePlan.BtnEdit || PerElectricalInsPlan.BtnEdit || PerMechanicInsPlan.BtnEdit;
        //btnEdit2.Enabled = PerAtchitecturalPlan.BtnEdit || PerStructurePlan.BtnEdit || PerElectricalInsPlan.BtnEdit || PerMechanicInsPlan.BtnEdit;
        BtnNew2.Enabled = BtnNew.Enabled = PerAtchitecturalPlan.BtnNew || PerStructurePlan.BtnNew || PerElectricalInsPlan.BtnNew || PerMechanicInsPlan.BtnNew || PerStructureAndInstallationPlan.BtnNew;

    }
    private bool CheckWorkFlowPermissionForEdit()
    {
        TSP.DataManager.WFPermission PerProject = CheckProjectWorkFlowPermissionForEdit();
        TSP.DataManager.WFPermission PerPlan = CheckPlanWorkFlowPermissionForEdit();

        return (PerProject.BtnEdit || PerPlan.BtnEdit);
    }

    private int GetPlanId()
    {
        if (GridViewPlans.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPlans.GetDataRow(GridViewPlans.FocusedRowIndex);
            int PlansId = (int)row["PlansId"];
            return PlansId;
        }
        else
            SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
        return -2;
    }

    private TSP.DataManager.WFPermission CheckProjectWorkFlowPermissionForEdit()
    {
        //*****TableId
        PrjReId = Utility.DecryptQS(HiddenFieldPlan["PrjReId"].ToString());

        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        //*******Editing Task Code
        int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;
        int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;
        int StructureAndInstallationCode = (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject;

        TSP.DataManager.WFPermission PerAtchitecturalPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ArchitecturalPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructurePlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructurePlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerElectricalInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ElectricalInsPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerMechanicInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(MechanicInsPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructureAndInstallationPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructureAndInstallationCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId());

        //*****TableId
        int PlansId = GetPlanId();

        WFCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;

        //*******Editing Task Code
        int PlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanInfo;

        TSP.DataManager.WFPermission PerPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(PlanTaskCode, WFCode, PlansId, Utility.GetCurrentUser_UserId());

        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = (PerAtchitecturalPlan.BtnEdit || PerStructurePlan.BtnEdit || PerElectricalInsPlan.BtnEdit || PerMechanicInsPlan.BtnEdit || PerStructureAndInstallationPlan.BtnEdit) && (PerPlan.BtnEdit);
        WFPer.BtnSave = (PerAtchitecturalPlan.BtnSave || PerStructurePlan.BtnSave || PerElectricalInsPlan.BtnSave || PerMechanicInsPlan.BtnSave || PerStructureAndInstallationPlan.BtnSave) && (PerPlan.BtnSave);
        WFPer.BtnNew = (PerAtchitecturalPlan.BtnNew || PerStructurePlan.BtnNew || PerElectricalInsPlan.BtnNew || PerMechanicInsPlan.BtnNew || PerStructureAndInstallationPlan.BtnNew) && (PerPlan.BtnNew);

        return WFPer;
    }

    private TSP.DataManager.WFPermission CheckPlanWorkFlowPermissionForEdit()
    {
        //*****TableId
        int PlansId = GetPlanId();

        ArrayList TaskCode = GetPlansTaskCode(PlansId);

        int WFCode = Convert.ToInt32(TaskCode[0]);

        //*******Editing Task Code
        int PlanTaskCode = Convert.ToInt32(TaskCode[1]);

        TSP.DataManager.WFPermission PerPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(PlanTaskCode, WFCode, Convert.ToInt32(PlansId), Utility.GetCurrentUser_UserId());

        return PerPlan;
    }

    /// <summary>
    /// ArrayList[0]=PlanWfCode, ArrayList[1]=PlanTaskCode
    /// </summary>
    private ArrayList GetPlansTaskCode(int PlansId)
    {
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();

        int PlanStatus = -1;

        int WfCodePlan = -2;
        int SavePlanInfoTaskCode = -2;

        PlansManager.FindByPlansId(PlansId);
        if (PlansManager.Count == 1)
        {
            PlanStatus = Convert.ToInt32(PlansManager[0]["Status"].ToString());
        }

        switch (PlanStatus)
        {
            case (int)TSP.DataManager.TSPlanRequestType.ChangePlanAndDesignerBasically:
                WfCodePlan = (int)TSP.DataManager.WorkFlows.TSChangePlansAndDesigner;
                SavePlanInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanAndDesignerInfo;
                break;
            case (int)TSP.DataManager.TSPlanRequestType.PlanRevisingRequest:
                WfCodePlan = (int)TSP.DataManager.WorkFlows.TSPlanRevisionConfirming;
                SavePlanInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanNewRevisionInfo;
                break;
            //case (int)TSP.DataManager.TSPlanRequestType.SavePlanInfoInsideProjectConfirming:
            //    WfCodePlan = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;
            //    SavePlanInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanInfo;
            //    break;
            default:
                SavePlanInfoTaskCode = -2;
                WfCodePlan = -2;
                break;
        }

        ArrayList TaskCode = new ArrayList();
        TaskCode.Add(WfCodePlan);
        TaskCode.Add(SavePlanInfoTaskCode);
        return TaskCode;
    }

    private bool CheckWFPermissionForDeleteRequest(int PlansId, int RequestType)
    {
        int WFCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming; ;
        int PlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanInfo;
        return TSP.DataManager.WorkFlowPermission.CheckWFPermissionForDeleteRequest(PlansId, WFCode, PlanTaskCode, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId);
    }

    private TSP.DataManager.WFPermission CheckProjectWorkFlowPermissionForControler(int PrjReId, int PlansId)
    {
        //*****TableId  PlansId
        int WFCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;
        //*******Editing Task Code
        int PlanTaskCode = (int)TSP.DataManager.WorkFlowTask.AssignControlerToPlan;

        TSP.DataManager.WFPermission PerPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(PlanTaskCode, WFCode, PlansId, Utility.GetCurrentUser_UserId());

        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = PerPlan.BtnEdit;
        WFPer.BtnSave = PerPlan.BtnSave;
        WFPer.BtnNew = PerPlan.BtnNew;

        return WFPer;
    }

    private bool CheckPermissionForNew()
    {
        bool Per = false;
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        int PlansTypeId = (int)TSP.DataManager.TSPlansType.Memari;
        int ProjectId = int.Parse(Utility.DecryptQS(HiddenFieldPlan["ProjectId"].ToString()));
        PlansManager.SelectByPlanType(ProjectId, PlansTypeId, 0, -1);
        if (PlansManager.Count > 0) return false;

        PlansManager.SelectByPlanType(ProjectId, PlansTypeId, 1, -1);
        if (PlansManager.Count <= 0) return true;
        for (int i = 0; i < PlansManager.Count; i++)
        {
            int PlansId = Convert.ToInt32(PlansManager[i]["PlansId"]);
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            DataTable dtWfState = WorkFlowStateManager.SelectLastState(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans), PlansId);
            if (dtWfState.Rows.Count == 1)
            {
                if (Convert.ToInt32(dtWfState.Rows[0]["Type"]) != (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask &&
                   Convert.ToInt32(dtWfState.Rows[0]["Type"]) != (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                {
                    Per = false;
                    break;
                }
                else Per = true;
            }
        }
        return Per;
    }
    #endregion
    #endregion
    void LoadWfHelp()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        DataTable dt = WorkFlowTaskManager.SelectWorkFlowTaskActiveTasks(((int)TSP.DataManager.WorkFlows.TSPlansConfirming));
        DataTable dt1 = dt.Clone();
        DataTable dt2 = dt.Clone();
        DataTable dt3 = dt.Clone();
        int count = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (count % 3 == 0) { dt1.Rows.Add(dr.ItemArray); }
            if (count % 3 == 1) { dt2.Rows.Add(dr.ItemArray); }
            if (count % 3 == 2) { dt3.Rows.Add(dr.ItemArray); }
            count++;
        }
        if (dt1.Rows.Count != 0)
        {
            RepeaterWfHep1.DataSource = dt1;
            RepeaterWfHep1.DataBind();
        }
        if (dt2.Rows.Count != 0)
        {
            RepeaterWfHep2.DataSource = dt2;
            RepeaterWfHep2.DataBind();
        }
        if (dt3.Rows.Count != 0)
        {
            RepeaterWfHep3.DataSource = dt3;
            RepeaterWfHep3.DataBind();
        }
    }
    #endregion
}
