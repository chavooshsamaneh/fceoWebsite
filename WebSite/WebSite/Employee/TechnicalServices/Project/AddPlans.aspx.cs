using DevExpress.Web;
using System;
using System.Data;
using System.IO;

public partial class Employee_TechnicalServices_Project_AddPlans : System.Web.UI.Page
{
    #region Properties
    Boolean IsPageRefresh = false;
    string _PageMode
    {
        get
        {
            return HiddenFieldPrjDes["PageMode"].ToString();
        }
        set
        {
            HiddenFieldPrjDes["PageMode"] = value;
        }
    }

    private int _PlansId
    {

        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldPrjDes["PlansId"]);
            }
            catch (Exception ex)
            {
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PlansId"]));
            }
        }
        set
        {
            HiddenFieldPrjDes["PlansId"] = value.ToString();
        }

    }

    private int _PrjReqId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["PrjReqId"]);
        }
        set
        {
            HiddenFieldPrjDes["PrjReqId"] = value.ToString();
        }
    }

    private int _PrjId
    {

        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldPrjDes["PrjId"]);
            }
            catch (Exception ex)
            {
                return Convert.ToInt32(Utility.DecryptQS(Request.QueryString["ProjectId"]));
            }
        }
        set
        {
            HiddenFieldPrjDes["PrjId"] = value.ToString();
        }

    }
    private int _CurrentPrjTaskCode
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["CurrentPrjTaskCode"]);
        }
        set
        {
            HiddenFieldPrjDes["CurrentPrjTaskCode"] = value.ToString();
        }
    }

    private int _PlansTypeId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["PlansTypeId"]);
        }
        set
        {
            HiddenFieldPrjDes["PlansTypeId"] = value.ToString();
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        SetLabelEarningEnable();

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
            if (string.IsNullOrEmpty(Request.QueryString["PrjId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PrePgMd"]))
            {
                Response.Redirect("Project.aspx");
            }

            if (Request.QueryString["PlnPgMd"] == null || Request.QueryString["PlnId"] == null)
            {
                string QS = "ProjectId=" + Request.QueryString["PrjId"].ToString() +
                    "&PrjReId=" + Request.QueryString["PrjReId"].ToString() +
                    "&PageMode=" + Request.QueryString["PrePgMd"].ToString();
                Response.Redirect("Plans.aspx?" + QS);
            }

            #region Sessions
            Session["PlansAttachName"] = null;
            Session["PlanAttachStatus"] = null;

            Session["AttachmentsManager"] = null;
            Session["AttachmentsManager"] = CreateAttachmentsManager();
            #endregion

            SetKeys();

            this.ViewState["btnDesigners"] = btnDesigners.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["btnSave"] = btnSave.Enabled;
            this.ViewState["DesignerVisible"] = RoundPanelDesigner.Visible;

        }

        #region Set ViewState
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["btnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["btnSave"];
        if (this.ViewState["DesignerVisible"] != null)
            RoundPanelDesigner.Visible = (bool)this.ViewState["DesignerVisible"];
        if (this.ViewState["btnDesigners"] != null)
            this.btnDesigners.Enabled = this.btnDesigners2.Enabled = (bool)this.ViewState["btnDesigners"];

        #endregion
        FilterAttachmentObjectDataSourceByTaskCode(_CurrentPrjTaskCode);
    }

    #region btnClick
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        SetEditModeKeys();
        _PageMode = "Edit";
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        SetNewModeKeys();
        _PageMode = "New";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        switch (_PageMode)
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

        Response.Redirect("Plans.aspx?PrjReId=" + Utility.EncryptQS(_PrjReqId.ToString()) + "&PageMode="
            + HiddenFieldPrjDes["PrePageMode"].ToString() + "&ProjectId=" + Utility.EncryptQS(_PrjId.ToString())
            + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
    }

    protected void btnSaveAttachment_Click(object sender, EventArgs e)
    {
        AttachRowInserting();
    }

    protected void btnDesigners_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();


        string QS = "DsPId=" + Utility.EncryptQS("-1") +
            "&PgMd=" + Utility.EncryptQS("New") +
            "&ProjectId=" + Utility.EncryptQS(_PrjId.ToString()) +
            "&PrjReId=" + Utility.EncryptQS(_PrjReqId.ToString()) +
            "&PageMode=" + HiddenFieldPrjDes["PrePageMode"].ToString() +
            "&PrjDesignerId=" + Utility.EncryptQS("-1") +
            "&PlnId=" + Utility.EncryptQS(_PlansId.ToString()) +
            "&PageSender=" + Utility.EncryptQS("Designer") +
            "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        Response.Redirect("AddPlanDesigner.aspx?" + QS);
    }
    protected void cmbPlanType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbPlanType.SelectedItem == null)
            return;
        _PlansTypeId = Convert.ToInt32(cmbPlanType.SelectedItem.Value);

        FilterAttachmentObjectDataSourceByTaskCode(_CurrentPrjTaskCode);
    }

    #endregion
    protected void GridViewAttachment_PageIndexChanged(object sender, EventArgs e)
    {
        FillPlanAttachment();
    }
    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string PageMode = HiddenFieldPrjDes["PrePageMode"].ToString();

        PrjMainMenu MainMenu = new PrjMainMenu("Project", _PrjId);
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, Utility.EncryptQS(_PrjReqId.ToString()), PageMode, GrdFlt, SrchFlt));
    }
    protected void MenuPlan_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        string QS = "PrjReId=" + Utility.EncryptQS(_PrjReqId.ToString())
            + "&PlnId=" + Utility.EncryptQS(_PlansId.ToString())
            + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;


        switch (e.Item.Name)
        {
            case "PlanDes":
                QS = QS + "&ProjectId=" + Utility.EncryptQS(_PrjId.ToString()) + "&PageMode=" + HiddenFieldPrjDes["PrePageMode"].ToString()
                    + "&PlnPgMd=" + Utility.EncryptQS(_PageMode);
                Response.Redirect("PlanDesigner.aspx?" + QS);
                break;

            case "ControlerViewPoint":
                QS = QS + "&PrjId=" + Utility.EncryptQS(_PrjId.ToString()) + "&PrePgMd=" + HiddenFieldPrjDes["PrePageMode"].ToString() + "&PlnPgMd=" + Utility.EncryptQS(_PageMode);
                Response.Redirect("PlanControlers.aspx?" + QS);


                break;
        }
    }

    protected void flpFile_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void GridViewAttachment_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager = (TSP.DataManager.TechnicalServices.AttachmentsManager)Session["AttachmentsManager"];

        DataRow AttachmentRow = AttachmentsManager.DataTable.Rows.Find(e.Keys["AttachmentId"]);
        AttachmentRow.Delete();

        e.Cancel = true;

        GridViewAttachment.CancelEdit();

        GridViewAttachment.DataSource = AttachmentsManager.DataTable;
        GridViewAttachment.KeyFieldName = "AttachmentId";
        GridViewAttachment.DataBind();
    }

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int ProjectReTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans);
        int WfCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        string QS = "PrjReId=" + Utility.EncryptQS(_PrjReqId.ToString()) +
                    "&PlnId=" + Utility.EncryptQS(_PlansId.ToString())
                    + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        QS = "AddPlans.aspx?" + QS + "&PrjId=" + Utility.EncryptQS(_PrjId.ToString()) + "&PrePgMd=" + HiddenFieldPrjDes["PrePageMode"].ToString() + "&PlnPgMd=" + Utility.EncryptQS(_PageMode);
        WFUserControl.QueryStringForRedirect = QS;
        WFUserControl.PerformCallback(_PlansId, ProjectReTableType, WfCode, e);
    }
    #endregion

    #region Methods
    #region SetKey-Mode
    private void SetKeys()
    {
        _PlansId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PlnId"].ToString()));
        _PageMode = Utility.DecryptQS(Request.QueryString["PlnPgMd"]);
        HiddenFieldPrjDes["PrePageMode"] = Request.QueryString["PrePgMd"];
        _PrjReqId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrjReId"]));
        _PrjId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrjId"]));
        HiddenFieldPrjDes["GroupId"] = "";

        string PrePageMode = Utility.DecryptQS(HiddenFieldPrjDes["PrePageMode"].ToString());
        _PlansTypeId = -1;
        if (string.IsNullOrEmpty(_PageMode) || string.IsNullOrEmpty(PrePageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        #region Check Permissions
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.PlansManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        BtnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnSave2.Enabled = btnSave.Enabled = per.CanNew || per.CanEdit;

        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;
        #endregion
        FillProjectInfo(_PrjReqId);
        SetMode(_PageMode);
        CheckWorkFlowPermissionForPrjRequest();
    }

    private void SetMode(string PageMode)
    {
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
        BtnNew.Enabled = btnNew2.Enabled =
        btnSave.Enabled = btnSave2.Enabled = true;

        btnDesigners.Enabled = btnDesigners2.Enabled =
        btnEdit.Enabled = btnEdit2.Enabled = false;
        RoundPanelControler.Visible = false;

        CheckAccess();
        SetEnabled(true);

        MenuPlan.Items[1].Enabled = false;
        MenuPlan.Items[2].Enabled = false;

        RoundPanelPlans.HeaderText = "جدید";
        ClearForm();
        FillDesignerPlans();
        FillPlanAttachment();
        FillPlanControler();
    }

    private void SetEditModeKeys()
    {
        btnDesigners.Enabled = btnDesigners2.Enabled =
        BtnNew.Enabled = btnNew2.Enabled =
        btnSave.Enabled = btnSave2.Enabled = true;
        btnEdit.Enabled = btnEdit2.Enabled = false;
        RoundPanelControler.Visible = true;

        CheckAccess();
        SetEnabled(true);
        cmbPlanType.Enabled = false;

        MenuPlan.Items[1].Enabled = true;
        MenuPlan.Items[2].Enabled = true;

        RoundPanelPlans.HeaderText = "ویرایش";
        FillForm();
    }

    private void SetViewModeKeys()
    {
        btnDesigners.Enabled = btnDesigners2.Enabled =
          BtnNew.Enabled = btnNew2.Enabled =
        btnEdit.Enabled = btnEdit2.Enabled = true;
        btnSave.Enabled = btnSave2.Enabled = false;
        RoundPanelControler.Visible = true;

        CheckAccess();
        SetEnabled(false);

        MenuPlan.Items[1].Enabled = true;
        MenuPlan.Items[2].Enabled = true;

        RoundPanelPlans.HeaderText = "مشاهده";
        FillForm();
    }

    #endregion

    private void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.PlansManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perPlanDes = TSP.DataManager.TechnicalServices.Designer_PlansManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perPrjDes = TSP.DataManager.TechnicalServices.Project_DesignerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        if (BtnNew.Enabled == true)
        {
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
        }

        if (btnEdit.Enabled == true)
        {
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
        }

        if ((_PageMode == "New") && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;
        }
        if ((_PageMode == "Edit") && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
        }

        RoundPanelDesigner.Visible = perPlanDes.CanView && perPrjDes.CanView;

        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["btnSave"] = btnSave.Enabled;
        this.ViewState["DesignerVisible"] = RoundPanelDesigner.Visible;
    }

    private void SetEnabled(bool Enable)
    {
        cmbPlanType.Enabled = Enable;
        txtPlanNo.Enabled = Enable;
        CheckBoxConfirmed.Enabled = Enable;

        TblTrFileType.Visible = Enable;
        TblTrFileUplode.Visible = Enable;
        btnSaveAttachment.Visible = Enable;
    }

    private void ClearForm()
    {
        Session["PlansAttachName"] = null;
        Session["PlanAttachStatus"] = null;

        Session["AttachmentsManager"] = null;
        Session["AttachmentsManager"] = CreateAttachmentsManager();

        cmbPlanType.DataBind();
        cmbPlanType.SelectedIndex = -1;
        txtPlanDes.Text = "";
        txtPlanNo.Text = "";
        CheckBoxConfirmed.Checked = false;

        _PlansId = -2;
        lblWorkFlowState.Text = "وضعیت درخواست ثبت طراح و نقشه مربوطه: نامشخص";
    }

    #region Fill
    private void FillForm()
    {
        FillPlan(_PlansId);
        FillPlanAttachment();
        FillPlanControler();
        FillDesignerPlans();
    }

    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Convert.ToInt32(Id));
        HiddenFieldPrjDes["GroupId"] = prjInfo.GroupId;
        _CurrentPrjTaskCode = prjInfo.CurrentTaskCode;
    }

    private void FillPlan(int PlansId)
    {
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        DataTable dtPlan = PlansManager.SelectById(PlansId, -1);

        if (dtPlan.Rows.Count == 1)
        {
            txtPlanDes.Text = dtPlan.Rows[0]["Description"].ToString();
            txtPlanNo.Text = dtPlan.Rows[0]["No"].ToString();
            cmbPlanType.DataBind();
            if (!Utility.IsDBNullOrNullValue(dtPlan.Rows[0]["PlansTypeId"]))
            {
                cmbPlanType.SelectedIndex = cmbPlanType.Items.FindByValue(dtPlan.Rows[0]["PlansTypeId"].ToString()).Index;
                _PlansTypeId = Convert.ToInt32(dtPlan.Rows[0]["PlansTypeId"]);
            }

            if (Convert.ToInt32(dtPlan.Rows[0]["IsDesAccepted"]) == (int)TSP.DataManager.TSDesignerAcceptance.ConfirmedWithoutSaveController)
                CheckBoxConfirmed.Checked = true;
            else CheckBoxConfirmed.Checked = false;

            txtFollowCode.Text = dtPlan.Rows[0]["FollowCode"].ToString();


            if (!Utility.IsDBNullOrNullValue(dtPlan.Rows[0]["TaskName"]))
                lblWorkFlowState.Text = "وضعیت درخواست ثبت طراح و نقشه مربوطه: " + dtPlan.Rows[0]["TaskName"].ToString();
            else
                lblWorkFlowState.Text = "وضعیت درخواست ثبت طراح و نقشه مربوطه: " + "نامشخص";
        }
    }

    private void FillPlanAttachment()
    {
        TSP.DataManager.TechnicalServices.AttachmentsManager Manager = (TSP.DataManager.TechnicalServices.AttachmentsManager)Session["AttachmentsManager"];
        if (_PageMode != "New")
        {

            Manager.FindByTableTypeId(_PlansId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans), -1);
            GridViewAttachment.DataSource = Manager.DataTable;
            GridViewAttachment.DataBind();
        }

        FilterAttachmentObjectDataSourceByTaskCode(_CurrentPrjTaskCode);
    }

    private void FillPlanControler()
    {
        FillControllerGrid();
    }

    private void FillDesignerPlans()
    {
        ObjectDataSourceDesignerPlans.SelectParameters["PlansId"].DefaultValue = _PlansId.ToString();
    }
    #endregion

    #region  Attach Grid
    private TSP.DataManager.TechnicalServices.AttachmentsManager CreateAttachmentsManager()
    {
        TSP.DataManager.TechnicalServices.AttachmentsManager manager = new TSP.DataManager.TechnicalServices.AttachmentsManager();
        return manager;
    }

    private void AttachRowInserting()
    {
        if (Session["PlansAttachName"] == null || Session["PlanAttachStatus"] == null)
        {
            SetLabelWarning("خطایی در اضافه کردن رخ داده است");
            return;
        }
        if (cmbAttachType.SelectedIndex == -1)
        {
            SetLabelWarning("نوع فایل پیوست را انتخاب نمایید.");
            return;
        }

        ASPxHyperLink1.ClientVisible = false;

        try
        {
            TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager = (TSP.DataManager.TechnicalServices.AttachmentsManager)Session["AttachmentsManager"];

            DataRow AttachmentRow = AttachmentsManager.NewRow();

            AttachmentRow.BeginEdit();
            AttachmentRow["TableType"] = (int)TSP.DataManager.TableCodes.TSPlans;
            AttachmentRow["TableTypeId"] = -1;

            AttachmentRow["AttachTypeId"] = cmbAttachType.Value;
            AttachmentRow["Title"] = cmbAttachType.Text;

            AttachmentRow["FilePath"] = "~/Image/TechnicalServices/Plans/" + Path.GetFileName(Session["PlanAttachStatus"].ToString());
            AttachmentRow["FileName"] = Session["PlansAttachName"].ToString();
            System.IO.FileInfo f = new FileInfo(MapPath("~/Image/TechnicalServices/Plans/" + Path.GetFileName(Session["PlanAttachStatus"].ToString())));
            long size = f.Length / 1024;
            AttachmentRow["FileSize"] = size.ToString() + " KB";
            AttachmentRow["UserId"] = Utility.GetCurrentUser_UserId();
            AttachmentRow["ModifiedDate"] = DateTime.Now;
            AttachmentRow["CreateDate"] = Utility.GetDateOfToday();

            AttachmentRow.EndEdit();

            AttachmentsManager.AddRow(AttachmentRow);

            GridViewAttachment.CancelEdit();

            GridViewAttachment.DataSource = AttachmentsManager.DataTable;
            GridViewAttachment.KeyFieldName = "AttachmentId";
            GridViewAttachment.DataBind();
            ClearAttachment();
        }
        catch (Exception err)
        {
            SetLabelWarning("خطایی در اضافه کردن رخ داده است");
        }
    }

    private void ClearAttachment()
    {
        Session["PlanAttachStatus"] = "";
        Session["PlansAttachName"] = "";
        FilterAttachmentObjectDataSourceByTaskCode(_CurrentPrjTaskCode);
        cmbAttachType.DataBind();
        cmbAttachType.SelectedIndex = -1;
    }
    private void FilterAttachmentObjectDataSourceByTaskCode(int CurrentPrjTaskCode)
    {
        switch (CurrentPrjTaskCode)
        {
            case (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject:
                ObjdsAttachType.FilterExpression =
                "AttachTypeId=" + (int)TSP.DataManager.TSAttachType.Plan
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.PlansMethod
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.ArchContract
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.FormNo5;
                TasisatNotification.Visible = false;
                ArchNotification.Visible = true;
                AllowedFileExt.InnerText = "";
                break;

            case (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject:
                ObjdsAttachType.FilterExpression =
                "AttachTypeId=" + (int)TSP.DataManager.TSAttachType.Plan
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.ElectInstalContract
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.FormNo6;
                TasisatNotification.Visible = true;
                ArchNotification.Visible = false;
                break;

            case (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject:
                ObjdsAttachType.FilterExpression =
                "AttachTypeId=" + (int)TSP.DataManager.TSAttachType.Plan
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.MechanInstalContract
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.FormNo6;
                TasisatNotification.Visible = true;
                ArchNotification.Visible = false;
                break;

            case (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject:
                ObjdsAttachType.FilterExpression =
                "AttachTypeId=" + (int)TSP.DataManager.TSAttachType.Plan
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.PlanBooklet
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.StructureContract
                + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.CalculationFile;
                TasisatNotification.Visible = false;
                ArchNotification.Visible = false;
                break;
            case (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject:
                switch (_PlansTypeId)
                {
                    case (int)TSP.DataManager.TSPlansType.Sazeh:
                        ObjdsAttachType.FilterExpression =
                        "AttachTypeId=" + (int)TSP.DataManager.TSAttachType.Plan
                        + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.PlanBooklet
                        + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.StructureContract
                        + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.CalculationFile;
                        TasisatNotification.Visible = false;
                        ArchNotification.Visible = false;
                        break;
                    case (int)TSP.DataManager.TSPlansType.TasisatBargh:
                        ObjdsAttachType.FilterExpression =
                        "AttachTypeId=" + (int)TSP.DataManager.TSAttachType.Plan
                        + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.ElectInstalContract
                        + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.FormNo6;
                        TasisatNotification.Visible = true;
                        ArchNotification.Visible = false;
                        break;
                    case (int)TSP.DataManager.TSPlansType.TasisatMechanic:
                        ObjdsAttachType.FilterExpression =
                        "AttachTypeId=" + (int)TSP.DataManager.TSAttachType.Plan
                        + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.MechanInstalContract
                        + " OR AttachTypeId=" + (int)TSP.DataManager.TSAttachType.FormNo6;
                        TasisatNotification.Visible = true;
                        ArchNotification.Visible = false;
                        break;
                    default:
                        ObjdsAttachType.FilterExpression =
                        "AttachTypeId=-2";
                        TasisatNotification.Visible = false;
                        ArchNotification.Visible = false;
                        break;
                }
                break;
        }
    }
    #endregion

    #region  Controller Grid    

    private void FillControllerGrid()
    {
        TSP.DataManager.TechnicalServices.Plans_ControlerManager manager = new TSP.DataManager.TechnicalServices.Plans_ControlerManager();
        if (_PageMode != "New")
        {
            manager.FindByPlansId(_PlansId);
        }

        GridViewControler.DataSource = manager.DataTable;
        GridViewControler.DataBind();

        ObjectDataSourcePlansControlerViewPoint.SelectParameters["PlansId"].DefaultValue = (_PlansId == -1 ? "-2" : _PlansId.ToString());
        GridViewViewPoint.DataBind();
    }

    #endregion

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                Session["PlansAttachName"] = Path.GetFileName(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/TechnicalServices/Plans/") + ret) == true);
            string tempFileName = MapPath("~/Image/TechnicalServices/Plans/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["PlanAttachStatus"] = tempFileName;
        }
        return ret;
    }

    #region Insert-Update
    #region Insert
    private void Insert()
    {
        if (IsPageRefresh)
        {
            return;
        }

        if (!CheckType())
        {
            SetLabelWarning("با توجه به در جریان بودن درخواست ثبت نقشه انتخاب شده، امکان ثبت مجدد این نوع نقشه وجود ندارد.");
            return;
        }
        if (Session["AttachmentsManager"] == null)
        {
            SetLabelWarning("فایل های پیوست مربوط به نقشه را انتخاب نمایید.");
            return;
        }
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager = (TSP.DataManager.TechnicalServices.AttachmentsManager)Session["AttachmentsManager"];
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(PlansManager);
        transact.Add(AttachmentsManager);
        transact.Add(WorkFlowStateManager);
        transact.Add(WorkFlowTaskManager);

        if (AttachmentsManager.Count == 0)
        {
            SetLabelWarning("فایل های پیوست مربوط به نقشه را انتخاب نمایید.");
            return;
        }

        if (!CheckPlansAttachType(AttachmentsManager))
            return;

        try
        {
            transact.BeginSave();

            InsertPlan(PlansManager);
            InsertAttachment(AttachmentsManager);
            if (!InsertWF(WorkFlowStateManager, WorkFlowTaskManager))
            {
                transact.CancelSave();
                return;
            }
            lblWorkFlowState.Text = "وضعیت درخواست ثبت طراح و نقشه مربوطه" + WorkFlowTaskManager[WorkFlowTaskManager.Count - 1]["TaskName"].ToString();
            transact.EndSave();

            _PageMode = "Edit";
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد.");
        }
        catch (Exception err)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void InsertPlan(TSP.DataManager.TechnicalServices.PlansManager PlansManager)
    {
        DataRow PlanRow = PlansManager.NewRow();

        PlanRow.BeginEdit();
        PlanRow["PrjReId"] = _PrjReqId;
        PlanRow["ProjectId"] = _PrjId;
        PlanRow["Status"] = (int)TSP.DataManager.TSPlanRequestType.SavePlanInfoInsideProjectConfirming;
        PlanRow["PlansTypeId"] = cmbPlanType.SelectedItem.Value.ToString();
        PlanRow["No"] = txtPlanNo.Text.Trim();
        PlanRow["Description"] = txtPlanDes.Text;
        PlanRow["Date"] = Utility.GetDateOfToday();
        if (CheckBoxConfirmed.Checked)
            PlanRow["IsDesAccepted"] = (int)TSP.DataManager.TSDesignerAcceptance.ConfirmedWithoutSaveController;
        else
            PlanRow["IsDesAccepted"] = (int)TSP.DataManager.TSDesignerAcceptance.Pending;
        PlanRow["InActive"] = 0;
        PlanRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.TSPlan);
        PlanRow["UserId"] = Utility.GetCurrentUser_UserId();
        PlanRow["ModifiedDate"] = DateTime.Now;
        PlanRow.EndEdit();

        PlansManager.AddRow(PlanRow);
        PlansManager.Save();

        PlansManager.DataTable.AcceptChanges();
        _PlansId = Convert.ToInt32(PlansManager[0]["PlansId"]);
    }

    private void InsertAttachment(TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager)
    {
        for (int i = 0; i < AttachmentsManager.Count; i++)
        {
            if (Utility.IsDBNullOrNullValue(AttachmentsManager[i]["TableTypeId"]) || Convert.ToInt32(AttachmentsManager[i]["TableTypeId"]) == -1)
            {
                AttachmentsManager[i]["TableTypeId"] = _PlansId;
                AttachmentsManager.Save();
                AttachmentsManager.DataTable.AcceptChanges();
            }
        }
    }

    private bool InsertWF(TSP.DataManager.WorkFlowStateManager WorkFlowStateManager, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager)
    {
        int TaskId = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanInfo;
        int TableId = _PlansId;

        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count != 1)
        {
            SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است.");
            return false;
        }
        TaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);

        int CurrentNmcId = FindNmcId(TaskId);
        if (CurrentNmcId == -1)
        {
            //SetLabelWarning("اطلاعات شما در چارت سازمانی ثبت نشده است.");
            return false;
        }

        int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);
        if (WfStart > 0)
        {
            return true;
        }

        SetLabelWarning("خطایی در ذخیره انجام گرفته است.");
        return false;
    }

    private void InsertController()
    {
        if (IsPageRefresh)
        {
            return;
        }

        TSP.DataManager.TechnicalServices.Plans_ControlerManager PlansControlerManager = (TSP.DataManager.TechnicalServices.Plans_ControlerManager)Session["PlansControlerManager"];

        if (PlansControlerManager.Count == 0)
        {
            SetLabelWarning("بازبین نقشه را انتخاب نمایید.");
            return;
        }

        try
        {
            PlansControlerManager.Save();
            SetLabelWarning("ذخیره انجام شد.");

        }
        catch (Exception err)
        {
            SetError(err);
        }
    }
    #endregion

    #region Update
    private void Update()
    {
        if (IsPageRefresh)
        {
            return;
        }

        if (!CheckTypeForUpdate())
        {
            SetLabelWarning("با توجه به در جریان بودن درخواست ثبت نقشه انتخاب شده، امکان ثبت این نوع نقشه وجود ندارد.");
            return;
        }

        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager = (TSP.DataManager.TechnicalServices.AttachmentsManager)Session["AttachmentsManager"];

        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(PlansManager);
        transact.Add(AttachmentsManager);

        if (AttachmentsManager.Count == 0)
        {
            SetLabelWarning("فایل های پیوست مربوط به نقشه را انتخاب نمایید.");
            return;
        }

        if (!CheckPlansAttachType(AttachmentsManager))
            return;

        try
        {
            transact.BeginSave();

            UpdatePlan(PlansManager);
            InsertAttachment(AttachmentsManager);

            transact.EndSave();

            _PageMode = "Edit";
            SetEditModeKeys();

            SetLabelWarning("ذخیره انجام شد.");
        }
        catch (Exception err)
        {
            transact.CancelSave();
            SetError(err);
        }
    }

    private void UpdatePlan(TSP.DataManager.TechnicalServices.PlansManager PlansManager)
    {
        PlansManager.FindByPlansId(_PlansId);

        if (PlansManager.Count > 0)
        {

            PlansManager[0].BeginEdit();
            PlansManager[0]["PrjReId"] = _PrjReqId;
            PlansManager[0]["ProjectId"] = _PrjId;
            //PlansManager[0]["Status"]=
            PlansManager[0]["PlansTypeId"] = cmbPlanType.SelectedItem.Value.ToString();
            PlansManager[0]["No"] = txtPlanNo.Text.Trim();
            PlansManager[0]["Description"] = txtPlanDes.Text;
            if (CheckBoxConfirmed.Checked)
                PlansManager[0]["IsDesAccepted"] = (int)TSP.DataManager.TSDesignerAcceptance.ConfirmedWithoutSaveController;
            else
                PlansManager[0]["IsDesAccepted"] = (int)TSP.DataManager.TSDesignerAcceptance.Pending;
            PlansManager[0]["InActive"] = 0;
            PlansManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            PlansManager[0]["ModifiedDate"] = DateTime.Now;
            PlansManager[0].EndEdit();

            PlansManager.Save();
        }
    }
    #endregion
    #endregion

    #region Check Conditions
    private bool CheckType()
    {
        int PlansTypeId = Convert.ToInt32(cmbPlanType.Value);
        bool Exist = CheckIfNotConfirmedPlanTypeExist(_PrjId, PlansTypeId);
        if (Exist)
            return false;
        return true;
    }

    private bool CheckPlansAttachType(TSP.DataManager.TechnicalServices.AttachmentsManager AttachmentsManager)
    {
        if (cmbPlanType.SelectedItem == null)
        {
            SetLabelWarning("نوع نقشه را انتخاب نمایید");
            return false;
        }
        bool PlanBooklet = true; bool PlansMethod = true; bool ArchContract = true; bool FormNo5 = true; bool ElectInstalContract = true; bool FormNo6 = true; bool MechanInstalContract = true; bool StructureContract = true; bool CalculationFile = true;
        bool MechanicalPlan = true;
        bool ElectricalPlan = true;
        bool StructurePlan = true;
        bool ArchPlan = true;

        int GroupId = Convert.ToInt32(HiddenFieldPrjDes["GroupId"]);


        for (int i = 0; i < AttachmentsManager.Count; i++)
        {
            if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.PlanBooklet && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Sazeh)
                PlanBooklet = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.Plan && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Sazeh)
                StructurePlan = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.StructureContract && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Sazeh)
                StructureContract = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.CalculationFile && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Sazeh)
                CalculationFile = false;


            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.Plan && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Memari)
                ArchPlan = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.ArchContract && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Memari)
                ArchContract = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.FormNo5 && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Memari)
                FormNo5 = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.PlansMethod && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Memari)
                PlansMethod = false;


            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.FormNo6 && (_PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatMechanic || _PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatBargh))
                FormNo6 = false;


            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.Plan && _PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatBargh)
                ElectricalPlan = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.ElectInstalContract && _PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatBargh)
                ElectInstalContract = false;

            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.Plan && _PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatMechanic)
                MechanicalPlan = false;
            else if (Convert.ToInt32(AttachmentsManager[i]["AttachTypeId"]) == (int)TSP.DataManager.TSAttachType.MechanInstalContract && _PlansTypeId == (int)TSP.DataManager.TSPlansType.TasisatMechanic)
                MechanInstalContract = false;



        }
        switch (Convert.ToInt32(cmbPlanType.SelectedItem.Value))//PlanType
        {
            case (int)TSP.DataManager.TSPlansType.Memari:

                if (ArchPlan || FormNo5)
                {
                    SetLabelWarning("مستندات پیوست معماری باید شامل حداقل یک نسخه از هر کدام از نقشه معماری و فرم شماره 5 باشد بارگذاری قرارداد و اصل دستور نقشه شهرداری فعلا اختیاری است.");
                    return false;
                }
                break;

            case (int)TSP.DataManager.TSPlansType.TasisatBargh:
                if (ElectricalPlan || FormNo6)
                {
                    SetLabelWarning("مستندات پیوست تاسیسات الکتریکی باید شامل حداقل یک نسخه از هر کدام از نقشه تاسیسات الکتریکی و فرم شماره 6 باشد بارگذاری قرارداد فعلا اختیاری است.");
                    return false;
                }

                break;

            case (int)TSP.DataManager.TSPlansType.TasisatMechanic:
                if (MechanicalPlan || FormNo6)
                {
                    SetLabelWarning("مستندات پیوست تاسیسات مکانیکی باید شامل حداقل یک نسخه از هر کدام از نقشه تاسیسات مکانیکی و فرم شماره 6 باشد بارگذاری قرارداد فعلا اختیاری است.");
                    return false;
                }
                break;

            case (int)TSP.DataManager.TSPlansType.Sazeh:
                if (StructurePlan || PlanBooklet || CalculationFile)
                {
                    SetLabelWarning("مستندات پیوست سازه باید شامل حداقل یک نسخه از هر کدام از نقشه سازه و فایل های محاسباتی و خلاصه دفترچه محاسباتی باشد بارگذاری قرارداد فعلا اختیاری است.");
                    return false;
                }
                break;
        }


        return true;
    }

    private bool CheckTypeForUpdate()
    {
        bool Exist = CheckIfNotConfirmedPlanTypeExist();
        if (Exist)
            return false;
        return true;
    }

    private bool CheckIfNotConfirmedPlanTypeExist()
    {
        int PlansTypeId = Convert.ToInt32(cmbPlanType.Value);

        int IsConfirmed = 0;
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        PlansManager.SelectTSPlansByProjectAndRequest(_PrjId, PlansTypeId, IsConfirmed, _PrjReqId);
        if (PlansManager.Count == 0 || (PlansManager.Count == 1 && Convert.ToInt32(PlansManager[0]["PlansId"]) == _PlansId))
            return false;

        return true;
    }
    #endregion

    #region Set Eror-Warning Methods
    private void SetLabelEarningEnable()
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
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

    private void SetLabelPlanWarning(string Warning)
    {
        lblPlanWarning.Visible = true;
        lblPlanWarning.Text = Warning;
    }
    #endregion

    private int FindNmcId(int TaskId)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            SetLabelWarning("اطلاعات شما به عنوان انجام دهنده عملیات انتخاب شده در گردش کار ثبت نشده است.");
            return (-1);
        }
    }

    #region WFPermission Methods

    private void CheckWorkFlowPermissionForPrjRequest()
    {
        if (_PageMode == "New")
            CheckWorkFlowPermissionForNew();
        else
            CheckWorkFlowPermissionForEdit(_PageMode);
    }

    #region CheckWorkFlowPermissionForNew
    private void CheckWorkFlowPermissionForNew()
    {
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        DataTable dtWorkFlowState = new DataTable();
        ProjectRequestManager.FindByCode(_PrjReqId);
        int CurrentTaskCode = -1;
        if (ProjectRequestManager.Count == 1)
        {
            int PrjReTypeId = int.Parse(ProjectRequestManager[0]["PrjReTypeId"].ToString());
            switch (PrjReTypeId)
            {
                case (int)TSP.DataManager.TSProjectRequestType.InsertProject:
                    dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode((int)TSP.DataManager.WorkFlows.TSProjectConfirming, _PrjReqId);
                    if (dtWorkFlowState.Rows.Count > 0)
                    {
                        CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                        CheckWorkFlowPermissionForSave("New", CurrentTaskCode);
                        #region Set the Type of plan by current state of ProjectWF                       
                        cmbPlanType.Enabled = false;

                        switch (CurrentTaskCode)
                        {
                            case (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject:
                                _PlansTypeId = (int)TSP.DataManager.TSPlansType.Memari;
                                if (CheckIfNotConfirmedPlanTypeExist(_PrjId, _PlansTypeId))
                                {
                                    BtnNew.Enabled = false;
                                    btnNew2.Enabled = false;
                                    btnSave.Enabled = false;
                                    btnSave2.Enabled = false;
                                    SetLabelPlanWarning("توجه: با توجه به در جریان بودن درخواست ثبت نقشه معماری امکان ثبت مجدد نقشه معماری وجود ندارد.");

                                }
                                else
                                {
                                    cmbPlanType.DataBind();
                                    cmbPlanType.SelectedIndex = cmbPlanType.Items.FindByValue(_PlansTypeId.ToString()).Index;
                                }
                                break;

                            case (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject:
                                _PlansTypeId = (int)TSP.DataManager.TSPlansType.TasisatBargh;
                                if (CheckIfNotConfirmedPlanTypeExist(_PrjId, _PlansTypeId))
                                {
                                    BtnNew.Enabled = false;
                                    btnNew2.Enabled = false;
                                    btnSave.Enabled = false;
                                    btnSave2.Enabled = false;
                                    SetLabelPlanWarning("توجه: با توجه به در جریان بودن درخواست ثبت نقشه تاسسیسات برق امکان ثبت مجدد نقشه تاسسیسات برق وجود ندارد.");
                                }
                                else
                                {
                                    cmbPlanType.DataBind();
                                    cmbPlanType.SelectedIndex = cmbPlanType.Items.FindByValue(_PlansTypeId.ToString()).Index;
                                }
                                break;

                            case (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject:
                                _PlansTypeId = (int)TSP.DataManager.TSPlansType.TasisatMechanic;
                                if (CheckIfNotConfirmedPlanTypeExist(_PrjId, _PlansTypeId))
                                {
                                    BtnNew.Enabled = false;
                                    btnNew2.Enabled = false;
                                    btnSave.Enabled = false;
                                    btnSave2.Enabled = false;
                                    SetLabelPlanWarning("توجه: با توجه به در جریان بودن درخواست ثبت نقشه تاسسیسات مکانیک امکان ثبت مجدد نقشه تاسسیسات مکانیک وجود ندارد.");
                                }
                                else
                                {
                                    cmbPlanType.DataBind();
                                    cmbPlanType.SelectedIndex = cmbPlanType.Items.FindByValue(_PlansTypeId.ToString()).Index;
                                }
                                break;

                            case (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject:
                                _PlansTypeId = (int)TSP.DataManager.TSPlansType.Sazeh;

                                if (CheckIfNotConfirmedPlanTypeExist(_PrjId, _PlansTypeId))
                                {
                                    BtnNew.Enabled = false;
                                    btnNew2.Enabled = false;
                                    btnSave.Enabled = false;
                                    btnSave2.Enabled = false;
                                    SetLabelPlanWarning("توجه: با توجه به در جریان بودن درخواست ثبت نقشه سازه امکان ثبت مجدد نقشه سازه وجود ندارد.");
                                }
                                else
                                {
                                    cmbPlanType.DataBind();
                                    cmbPlanType.SelectedIndex = cmbPlanType.Items.FindByValue(_PlansTypeId.ToString()).Index;
                                }
                                break;
                            case (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject:
                                _PlansTypeId = -1;
                                ObjdsPlansType.FilterExpression = "PlansTypeId = " + ((int)TSP.DataManager.TSPlansType.Sazeh).ToString() + " OR " + "PlansTypeId = " + ((int)TSP.DataManager.TSPlansType.TasisatBargh).ToString() + " OR " + "PlansTypeId = " + ((int)TSP.DataManager.TSPlansType.TasisatMechanic).ToString();
                                cmbPlanType.DataBind();
                                cmbPlanType.Enabled = true;
                                if (CheckIfNotConfirmedPlanTypeExist(_PrjId, (int)TSP.DataManager.TSPlansType.Sazeh)
                                    && CheckIfNotConfirmedPlanTypeExist(_PrjId, (int)TSP.DataManager.TSPlansType.TasisatBargh)
                                    && CheckIfNotConfirmedPlanTypeExist(_PrjId, (int)TSP.DataManager.TSPlansType.TasisatMechanic))
                                {
                                    BtnNew.Enabled = false;
                                    btnNew2.Enabled = false;
                                    btnSave.Enabled = false;
                                    btnSave2.Enabled = false;
                                    SetLabelPlanWarning("توجه: با توجه به در جریان بودن درخواست ثبت نقشه سازه و نقشه تاسیسات امکان ثبت مجدد نقشه وجود ندارد.");
                                }
                                break;
                            default:
                                cmbPlanType.Enabled = false;
                                BtnNew.Enabled = false;
                                btnNew2.Enabled = false;
                                btnSave.Enabled = false;
                                btnSave2.Enabled = false;
                                btnEdit.Enabled = false;
                                btnEdit2.Enabled = false;
                                break;
                        }

                        FilterAttachmentObjectDataSourceByTaskCode(CurrentTaskCode);
                        #endregion
                    }
                    break;
            }
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode, int TaskCode)
    {
        int TableType = (int)TSP.DataManager.TableCodes.TSProjectRequest;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew(TaskCode, TableType, Utility.GetCurrentUser_UserId(), PageMode);
        BtnNew.Enabled = WFPer.BtnNew;
        btnNew2.Enabled = WFPer.BtnNew;
        btnSave.Enabled = WFPer.BtnSave;
        btnSave2.Enabled = WFPer.BtnSave;
        btnEdit.Enabled = WFPer.BtnEdit;
        btnEdit2.Enabled = WFPer.BtnEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }


    #endregion

    #region CheckWorkFlowPermissionForEdit
    /// <summary>
    /// Check the permission of plan by project request Type and plan request status.    
    /// </summary>
    /// <param name="PageMode"></param>
    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        TSP.DataManager.WFPermission PerProjectAndPlan = CheckProjectAndPlanWorkFlowPermissionForEdit(PageMode);

        btnEdit.Enabled = PerProjectAndPlan.BtnEdit;
        btnEdit2.Enabled = PerProjectAndPlan.BtnEdit;
        btnSave.Enabled = PerProjectAndPlan.BtnSave;
        btnSave2.Enabled = PerProjectAndPlan.BtnSave;

        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["btnSave"] = btnSave.Enabled;
    }

    /// <summary>
    /// Check current project Task Status.
    /// If it is one of the Plan's Task then check the task status of current plan.
    /// </summary>
    private TSP.DataManager.WFPermission CheckProjectAndPlanWorkFlowPermissionForEdit(string PageMode)
    {
        if (PageMode == "")
            PageMode = "New";

        //*******Editing Task Code (WF:Project Confirming)
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;
        int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;

        TSP.DataManager.WFPermission PerAtchitecturalPlan = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(ArchitecturalPlanTaskCode, WFCode, _PrjReqId, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission PerStructurePlan = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(StructurePlanTaskCode, WFCode, _PrjReqId, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission PerElectricalInsPlan = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(ElectricalInsPlanTaskCode, WFCode, _PrjReqId, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission PerMechanicInsPlan = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(MechanicInsPlanTaskCode, WFCode, _PrjReqId, Utility.GetCurrentUser_UserId(), PageMode);

        //*******Editing Task Code (WF:Plan Confirming)
        WFCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;
        int PlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SavePlanInfo;
        TSP.DataManager.WFPermission PerPlan = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(PlanTaskCode, WFCode, _PlansId, Utility.GetCurrentUser_UserId(), PageMode);

        ////////////////
        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = (PerAtchitecturalPlan.BtnEdit || PerStructurePlan.BtnEdit || PerElectricalInsPlan.BtnEdit || PerMechanicInsPlan.BtnEdit) && (PerPlan.BtnEdit);
        WFPer.BtnSave = (PerAtchitecturalPlan.BtnSave || PerStructurePlan.BtnSave || PerElectricalInsPlan.BtnSave || PerMechanicInsPlan.BtnSave) && (PerPlan.BtnSave);
        WFPer.BtnNew = (PerAtchitecturalPlan.BtnNew || PerStructurePlan.BtnNew || PerElectricalInsPlan.BtnNew || PerMechanicInsPlan.BtnNew) && (PerPlan.BtnNew);

        return WFPer;
    }


    #endregion

    #endregion

    #region Usfull Methods For WFPermissions


    private bool CheckIfNotConfirmedPlanTypeExist(int ProjectId, int PlansTypeId)
    {
        int IsConfirmed = 0;
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        PlansManager.SelectTSPlansByProjectAndRequest(ProjectId, PlansTypeId, IsConfirmed, _PrjReqId);
        if (PlansManager.Count == 0)
            return false;

        return true;
    }
    #endregion

    #endregion   

  

}
