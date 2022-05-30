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

public partial class Employee_TechnicalServices_Project_Project : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Refresh
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
            GridViewProject.JSProperties["cpIsPostBack"] = 1;
            Session["SendBackDataTable_EmpPrj"] = "";
            LoadWfHelp();
            ObjdsWorkFlowTaskProject.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.TSProjectConfirming).ToString();
            CmbTask.DataBind();
            CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
            CmbTask.SelectedIndex = 0;
            cmbDiscountPercent.DataBind();
            cmbDiscountPercent.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
            cmbDiscountPercent.SelectedIndex = 0;
            ASPxComboBoxCity.DataBind();
            ASPxComboBoxCity.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
            ASPxComboBoxCity.SelectedIndex = 0;

            ASPxComboBoxMunicipality.DataBind();
            ASPxComboBoxMunicipality.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
            ASPxComboBoxMunicipality.SelectedIndex = 0;

            GridViewProject.JSProperties["cpSelectedIndex"] = 0;
            GridViewProject.JSProperties["cpIsReturn"] = 0;

            #region Permissions
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ProjectManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = btnNew2.Enabled = per.CanNew;
            TSP.DataManager.Permission perTSProjectRequest = TSP.DataManager.TechnicalServices.ProjectManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnChangeProject.Enabled = btnChangeProject2.Enabled = perTSProjectRequest.CanNew;
            btnEdit.Enabled = btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = btnView2.Enabled = per.CanView;
            btnDelete.Enabled = btnDelete2.Enabled = per.CanDelete;
            btnPrint.Enabled = per.CanView;
            btnPrint2.Enabled = per.CanView;
            btnExportExcel.Enabled = per.CanView;
            btnExportExcel2.Enabled = per.CanView;
            //چاپ ناظر شهرداری
            TSP.DataManager.Permission perobsPrint = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermissionPrintMunicipalityObsPermit(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnObsPermit.Visible = btnObsPermit2.Visible = perobsPrint.CanView;
            //ناظر جدید
            TSP.DataManager.Permission perobs = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnObservers.Visible = btnObservers2.Visible = perobs.CanNew;

            //مجری جدید
            TSP.DataManager.Permission perImp = TSP.DataManager.TechnicalServices.Project_ImplementerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnImplementers.Visible = btnImplementers2.Visible = perImp.CanNew;
            //چاپ طراح شهرداری
            TSP.DataManager.Permission perDesPrint = TSP.DataManager.TechnicalServices.Project_DesignerManager.GetUserPermissionPrintMunicipalityDesPermit(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnObsPermitWithText.Visible = btnObsPermitWithText2.Visible = btnDesPermit.Visible = btnDesPermit2.Visible = perDesPrint.CanView;

            //پایان کار
            TSP.DataManager.Permission perEndProject = TSP.DataManager.TechnicalServices.ProjectManager.GetUserPermissionEndProject(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEndPrjCertificate.Visible = btnEndPrjCertificate2.Visible = perEndProject.CanNew;

            TSP.DataManager.Permission perProjectDeletRequestAllInfo = TSP.DataManager.TechnicalServices.ProjectManager.GetUserPermissionProjectDeletRequestAllInfo(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDeleteAll.Visible = btnDeleteAll2.Visible = btnDeleteConfirmedWFState.Visible = btnDeleteConfirmedWFState2.Visible = perProjectDeletRequestAllInfo.CanNew;


            if (!per.CanView)
            {
                GridViewProject.Visible = false;
                btnTracing.Enabled = false;
                btnTracing2.Enabled = false;
            }
            #endregion

            drdPlanType.DataBind();
            ((ASPxListBox)(drdPlanType.FindControl("ListBoxPlanType"))).Items.Insert(0, new ListEditItem("<همه>", null));

            TSP.DataManager.TechnicalServices.ProjectUserRightManager ProjectUserRightManager = new TSP.DataManager.TechnicalServices.ProjectUserRightManager();
            DataTable dtList = ProjectUserRightManager.SelectByUser(Utility.GetCurrentUser_UserId());
            if (dtList.Rows.Count == 0)
            {
                ObjectDataSourceProject.SelectParameters["MunParentIdList"].DefaultValue = "(-2)";
                ULAlarm.Visible = false;
            }
            else if (Utility.IsDBNullOrNullValue(dtList.Rows[0]["MunParentIdList"]))
            {
                ObjectDataSourceProject.SelectParameters["MunParentIdList"].DefaultValue = "(-2)";
                ULAlarm.Visible = true;
            }
            else
            {
                ULAlarm.Visible = false;
                ObjectDataSourceProject.SelectParameters["MunParentIdList"].DefaultValue = dtList.Rows[0]["MunParentIdList"].ToString();
            }

            //GridViewProject.DataBind();
            // ObjdsWorkFlowTask.SelectParameters["WorkFlowCode"].DefaultValue = ((int)TSP.DataManager.WorkFlows.TSBuildingsLicenseConfirming).ToString();

            CheckWorkFlowPermission();
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnPrint"] = btnPrint.Enabled;
            this.ViewState["BtnTracing"] = btnTracing.Enabled;
           
            this.ViewState["btnEndPrjCertificate"] = btnEndPrjCertificate.Visible;
            this.ViewState["btnDesPermit"] = btnDesPermit.Visible;
            this.ViewState["btnObservers"] = btnObservers.Visible;
            this.ViewState["btnObsPermit"] = btnObsPermit.Visible;
            this.ViewState["BtnDeletAllReqInfo"] = btnDeleteAll.Visible;

        }

        if (this.ViewState["btnEndPrjCertificate"] != null)
            this.btnEndPrjCertificate.Visible = this.btnEndPrjCertificate2.Visible = (bool)this.ViewState["btnEndPrjCertificate"];
        if (this.ViewState["btnDesPermit"] != null)
            this.btnDesPermit.Visible = this.btnDesPermit2.Visible = btnObsPermitWithText.Visible = btnObsPermitWithText2.Visible = (bool)this.ViewState["btnDesPermit"];
        if (this.ViewState["btnObsPermit"] != null)
            this.btnObsPermit.Visible = this.btnObsPermit2.Visible = (bool)this.ViewState["btnObsPermit"];

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
        if (this.ViewState["BtnTracing"] != null)
            this.btnTracing.Enabled = this.btnTracing2.Enabled = (bool)this.ViewState["BtnTracing"];
        if (this.ViewState["BtnDeletAllReqInfo"] != null)
            btnDeleteAll.Visible = btnDeleteAll2.Visible = btnDeleteConfirmedWFState.Visible = (bool)this.ViewState["BtnDeletAllReqInfo"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        SetPageFilter();
        SetGridRowIndex();
        //  Search();

        ArrayList DeletedColumnsName = new ArrayList();
        DeletedColumnsName.Add("WFState");

        Session["DeletedColumnsName"] = DeletedColumnsName;


        Session["DataTable"] = GridViewProject.Columns;
        Session["DataSource"] = ObjectDataSourceProject;
        Session["Title"] = "پروژه ها";
        Session["Header"] = GetRepHeader();

        string script = @"<SCRIPT language='javascript'> function CheckSearch() { var txtDateFrom = document.getElementById('" + txtDateFrom.ClientID + "').value;";
        script += "var txtDateTo = document.getElementById('" + txtDateTo.ClientID + "').value;";
        script += "if ( txtDateFrom=='' && txtDateTo=='' && txtCode.GetText()=='' && txtName.GetText()=='' && txtPelak.GetText() == '' && txtFileNumber.GetText() == '' && txtMapNumer.GetText() == '' && txtLicenceNo.GetText() == '' && txtOwnerName.GetText() == '' && txtSearchArchiveNo.GetText() == '' && ASPxComboBoxStructureGroups.GetSelectedIndex() == -1 &&  ASPxComboBoxCity.GetSelectedIndex() == 0 && ASPxComboBoxMunicipality.GetSelectedIndex() == 0 && txtProjectNo.GetText()=='' && txtMainRegion.GetText()==''  && txtMainSection.GetText()=='' && CmbTask.GetSelectedIndex() == 0  && cmbDiscountPercent.GetSelectedIndex() == 0) return 0; else return 1;  }</SCRIPT>";
        script += @"<SCRIPT language='javascript'>  
                function ClearSearch() {
                      txtMainRegion.SetText('');   txtProjectNo.SetText('');   txtMainSection.SetText(''); 
                    txtCode.SetText(''); txtName.SetText(''); txtPelak.SetText(''); txtFileNumber.SetText(''); 
                    txtMapNumer.SetText(''); txtLicenceNo.SetText(''); txtOwnerName.SetText(''); txtSearchArchiveNo.SetText('');
                    ASPxComboBoxStructureGroups.SetSelectedIndex(-1); ASPxComboBoxCity.SetSelectedIndex(0); ASPxComboBoxMunicipality.SetSelectedIndex(0);CmbTask.SetSelectedIndex(0);cmbDiscountPercent.SetSelectedIndex(0);
                    document.getElementById('" + txtDateFrom.ClientID + "').value = '';";
        script += " document.getElementById('" + txtDateTo.ClientID + "').value = '';}</SCRIPT>";

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "GetDate", script);

    }

    protected void ASPxComboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ASPxComboBoxCity.SelectedIndex != 0)
        {
            ObjectdatasourceMunicipality.SelectParameters["CitId"].DefaultValue = ASPxComboBoxCity.Value.ToString();
        }
        else
        {
            ObjectdatasourceMunicipality.SelectParameters["CitId"].DefaultValue = "-1";
        }
        ASPxComboBoxMunicipality.DataBind();
        ASPxComboBoxMunicipality.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
        ASPxComboBoxMunicipality.SelectedIndex = 0;
    }

    #region Btn Click
    protected void btnObservers_Click(object sender, EventArgs e)
    {
        int ProjectId = -1;
        string GridFilterString = GridViewProject.FilterExpression;
        string SearchFilterString = GenerateFilterString();

        int focucedIndex = GridViewProject.FocusedRowIndex;
        if (focucedIndex > -1)
        {
            DataRow Prjrow = GridViewProject.GetDataRow(focucedIndex);
            ProjectId = (int)Prjrow["ProjectId"];
            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            ProjectRequestManager.SelectRequestLastVersion(ProjectId, -1, 0);
            if (ProjectRequestManager.Count <= 0)
            {
                SetLabelWarning("امکان ثبت ناظر جدید وجود ندارد برای پروژه انتخاب شده درخواست در جریان وجود ندارد");
                return;
            }
            int PrjReId = Convert.ToInt32(ProjectRequestManager[0]["PrjReId"]);

            if (!CheckProjectWorkFlowPermissionForObservers(PrjReId))
            {
                SetLabelWarning("در این مرحله از گردش کار امکان ثبت ناظر وجود ندارد.");
                return;
            }

            string QS = "ProjectId=" + Utility.EncryptQS(ProjectId.ToString())
                      + "&PrjObsId=" + Utility.EncryptQS("-1")
                      + "&PageMode2=" + Utility.EncryptQS("View")
                      + "&PageMode=" + Utility.EncryptQS("New")
                      + "&PrjReId=" + Utility.EncryptQS(PrjReId.ToString())
                      + "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString);
            Response.Redirect("ObserverInsert.aspx?" + QS);
        }
        else
        {
            SetLabelWarning("ابتدا یک پروژه را انتخاب نمائید.");
            return;
        }
    }

    protected void btnDesigners_Click(object sender, EventArgs e)
    {
        int ProjectId = -1;
        string GridFilterString = GridViewProject.FilterExpression;
        string SearchFilterString = GenerateFilterString();

        int focucedIndex = GridViewProject.FocusedRowIndex;
        if (focucedIndex > -1)
        {
            DataRow Prjrow = GridViewProject.GetDataRow(focucedIndex);
            ProjectId = (int)Prjrow["ProjectId"];
            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            ProjectRequestManager.SelectRequestLastVersion(ProjectId, -1, 0);
            if (ProjectRequestManager.Count <= 0)
            {
                SetLabelWarning("امکان ثبت طراح جدید وجود ندارد برای پروژه انتخاب شده درخواست در جریان وجود ندارد");
                return;
            }
            int PrjReId = Convert.ToInt32(ProjectRequestManager[0]["PrjReId"]);
            if (!CheckProjectWorkFlowPermissionForDesigners(PrjReId))
            {
                SetLabelWarning("در این مرحله از گردش کار امکان ثبت طراح وجود ندارد.");
                return;
            }

            string QS = "DsPId=" + Utility.EncryptQS("-1") +
                "&PgMd=" + Utility.EncryptQS("New") +
                "&ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) +
                "&PrjReId=" + Utility.EncryptQS(PrjReId.ToString()) +
                "&PageMode=" + Utility.EncryptQS("View") +
                "&PrjDesignerId=" + Utility.EncryptQS("-1") +
                "&PlnId=" + Utility.EncryptQS("-1") +
                //"&PageSender=" + Utility.EncryptQS("Designer") +
                "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString);

            Response.Redirect("AddPlanDesigner.aspx?" + QS);
        }
        else
        {
            SetLabelWarning("ابتدا یک پروژه را انتخاب نمائید.");
            return;
        }
        #region Comment
        //if (ProjectId == -1)
        //{
        //    SetLabelWarning("ابتدا یک پروژه را انتخاب نمائید.");
        //    return;
        //}
        //TSP.WebControls.CustomAspxDevGridView GridViewProjectRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewProject.FindDetailRowTemplateControl(GridViewProject.FocusedRowIndex, "GridViewProjectRequest");
        //if (GridViewProjectRequest != null)
        //{
        //    focucedIndex1 = GridViewProjectRequest.FocusedRowIndex;
        //    if (focucedIndex1 > -1)
        //    {
        //        DataRow row = GridViewProjectRequest.GetDataRow(focucedIndex1);
        //        int ReId = (int)row["ReId"];

        //        if (!CheckProjectWorkFlowPermissionForDesigners(ReId))
        //        {
        //            SetLabelWarning("در این مرحله از گردش کار امکان ثبت طراح وجود ندارد.");
        //            return;
        //        }

        //        string QS = "DsPId=" + Utility.EncryptQS("-1") +
        //            "&PgMd=" + Utility.EncryptQS("New") +
        //            "&ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) +
        //            "&PrjReId=" + Utility.EncryptQS(ReId.ToString()) +
        //            "&PageMode=" + Utility.EncryptQS("View") +
        //            "&PrjDesignerId=" + Utility.EncryptQS("-1") +
        //            "&PlnId=" + Utility.EncryptQS("-1") +
        //            "&PageSender=" + Utility.EncryptQS("Designer") +
        //            "&GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString);

        //        Response.Redirect("AddPlanDesigner.aspx?" + QS);
        //    }
        //}
        #endregion
    }

    protected void btnImplementers_Click(object sender, EventArgs e)
    {
        int ProjectId = -1;
        string GridFilterString = GridViewProject.FilterExpression;
        string SearchFilterString = GenerateFilterString();
        int focucedIndex = GridViewProject.FocusedRowIndex;
        if (focucedIndex > -1)
        {
            DataRow Prjrow = GridViewProject.GetDataRow(focucedIndex);
            ProjectId = (int)Prjrow["ProjectId"];
            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            ProjectRequestManager.SelectRequestLastVersion(ProjectId, -1, 0);
            if (ProjectRequestManager.Count <= 0)
            {
                SetLabelWarning("امکان ثبت مجری جدید وجود ندارد برای پروژه انتخاب شده درخواست در جریان وجود ندارد");
                return;
            }
            int PrjReId = Convert.ToInt32(ProjectRequestManager[0]["PrjReId"]);

            if (!CheckProjectWorkFlowPermissionForImplementers(PrjReId))
            {
                SetLabelWarning("در این مرحله از گردش کار امکان ثبت مجری وجود ندارد.");
                return;
            }
            string QS = "ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) + "&PrjImpId=" + Utility.EncryptQS("-1")
                + "&PageMode=" + Utility.EncryptQS("View")
                + "&PrjReId=" + Utility.EncryptQS(PrjReId.ToString())
                + "&GrdFlt=" + Utility.EncryptQS(GridFilterString)
                + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString)
                + "&PageMode2=" + Utility.EncryptQS("New");
            Response.Redirect("ImplementerInsert.aspx?" + QS);
        }
        else
        {
            SetLabelWarning("ابتدا یک پروژه را انتخاب نمائید.");
            return;
        }
        #region Comment
        //if (ProjectId == -1)
        //{
        //    SetLabelWarning("ابتدا یک پروژه را انتخاب نمائید.");
        //    return;
        //}
        //TSP.WebControls.CustomAspxDevGridView GridViewProjectRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewProject.FindDetailRowTemplateControl(GridViewProject.FocusedRowIndex, "GridViewProjectRequest");
        //if (GridViewProjectRequest != null)
        //{
        //    focucedIndex1 = GridViewProjectRequest.FocusedRowIndex;
        //    if (focucedIndex1 > -1)
        //    {
        //        DataRow row = GridViewProjectRequest.GetDataRow(focucedIndex1);
        //        int ReId = (int)row["ReId"];

        //        if (!CheckProjectWorkFlowPermissionForImplementers(ReId))
        //        {
        //            SetLabelWarning("در این مرحله از گردش کار امکان ثبت مجری وجود ندارد.");
        //            return;
        //        }

        //        string QS = "ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) + "&PrjImpId=" + Utility.EncryptQS("-1")
        //            + "&PageMode=" + Utility.EncryptQS("View")
        //            + "&PrjReId=" + Utility.EncryptQS(ReId.ToString())
        //            + "&GrdFlt=" + Utility.EncryptQS(GridFilterString)
        //            + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString)
        //            + "&PageMode2=" + Utility.EncryptQS("New");
        //        Response.Redirect("ImplementerInsert.aspx?" + QS);
        //    }
        //}
        #endregion
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "Projects";
        GridViewExporter.WriteXlsxToResponse(true);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        try
        {
            Search();
        }
        catch (Exception exp)
        {
            Utility.SaveWebsiteError(exp);
        }
    }
    #endregion
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        try
        {
            Clear();
        }
        catch (Exception exp)
        {
            Utility.SaveWebsiteError(exp);
        }
    }
    protected void btnResetSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        int ProjectId = -1;
        string RsType = "";

        if (GridViewProject.FocusedRowIndex > -1)
        {
            DataRow row = GridViewProject.GetDataRow(GridViewProject.FocusedRowIndex);
            ProjectId = (int)row["ProjectId"];
            RsType = ((int)TSP.DataManager.ResetPasswordType.TsProjectOwner).ToString();

        }
        if (ProjectId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
        }
        else
            Response.Redirect("~/Users/ResetPassword.aspx?ID=" + Utility.EncryptQS(ProjectId.ToString()) + "&Type=" + Utility.EncryptQS(RsType));

    }
    #region Grids Methods
    protected void GridViewProject_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridViewDetailRowEventArgs e)
    {
        GridViewProject.FocusedRowIndex = e.VisibleIndex;
    }

    protected void GridViewProject_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;

        if (e.GetValue("CountNotConfirmed") != null)
        {
            if ((int)e.GetValue("CountNotConfirmed") > 0)
                e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        }

        if (!Utility.IsDBNullOrNullValue(e.GetValue("LastReqType")))
        {
            switch ((Convert.ToInt32(e.GetValue("LastReqType"))))
            {
                case (int)TSP.DataManager.TSProjectRequestType.ChangeObservationRequest:
                    e.Row.ForeColor = System.Drawing.Color.Olive;
                    break;
                case (int)TSP.DataManager.TSProjectRequestType.ChangeImplementerRequest:
                    e.Row.ForeColor = System.Drawing.Color.DeepPink;
                    break;
                case (int)TSP.DataManager.TSProjectRequestType.ChangeProject:
                    e.Row.ForeColor = System.Drawing.Color.DarkBlue;
                    break;
                case (int)TSP.DataManager.TSProjectRequestType.ChangePlansMethodRequest:
                    e.Row.ForeColor = System.Drawing.Color.Brown;
                    break;
                case (int)TSP.DataManager.TSProjectRequestType.LicenseRevivalConfirming:
                    e.Row.ForeColor = System.Drawing.Color.Green;
                    break;
                case (int)TSP.DataManager.TSProjectRequestType.InvalidationConfirming:
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    break;
                case (int)TSP.DataManager.TSProjectRequestType.EndProjectCertificateRequest:
                    e.Row.ForeColor = System.Drawing.Color.Purple;
                    break;
                case (int)TSP.DataManager.TSProjectRequestType.ValidationConfirming:
                    e.Row.ForeColor = System.Drawing.Color.DarkOrange;
                    break;
                case (int)TSP.DataManager.TSProjectRequestType.BuildingsLicenseConfirming:
                    e.Row.ForeColor = System.Drawing.Color.Purple;
                    break;
            }

        }
    }   

    protected void GridViewProject_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "ComputerCode":
                e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";
                break;

            case "RegisteredNo":
                e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";
                break;

            case "FileNo":
                e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";
                break;

            case "PlansMethodNo":
                e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";
                break;

            case "Date":
                e.Editor.Style["direction"] = "ltr";
                break;

            case "ArchiveNo":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewProject_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (IsPageRefresh)
            return;
        //  string[] parameter = e.Parameter.Split(';');
        switch (e.Parameters)
        {
            case "search":
                Search();
                break;
            case "clear":
                txtCode.Text = "";
                txtName.Text = "";
                txtPelak.Text = "";
                txtFileNumber.Text = "";
                txtMapNumer.Text = "";
                ASPxComboBoxStructureGroups.SelectedIndex = -1;
                ASPxComboBoxCity.SelectedIndex = -1;
                ASPxComboBoxMunicipality.SelectedIndex = -1;
                txtDateFrom.Text = "";
                txtDateTo.Text = "";
                txtLicenceNo.Text = "";
                txtOwnerName.Text = "";
                txtSearchArchiveNo.Text = "";
                GridViewProject.DataBind();
                break;
            default:
                GridViewProject.DataBind();
                break;
        }

    }

    protected void GridViewProjectRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        int ProjectId = (int)(sender as ASPxGridView).GetMasterRowKeyValue();
        Session["ProjectId"] = ProjectId;
    }

    protected void GridViewProjectRequest_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "MailNo":
                e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";
                break;

            case "RequestDate":
                e.Editor.Style["direction"] = "ltr";
                break;

            case "MailDate":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }
    #endregion

    #region Callbacks
    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int focucedIndex = -1;
        if (GridViewProject.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewProjectRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewProject.FindDetailRowTemplateControl(GridViewProject.FocusedRowIndex, "GridViewProjectRequest");
            if (GridViewProjectRequest != null)
            {
                focucedIndex = GridViewProjectRequest.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = GridViewProjectRequest.GetDataRow(focucedIndex);
                    int ReId = (int)row["ReId"];
                    int ProjectReTableType = (int)TSP.DataManager.TableCodes.TSProjectRequest;
                    int WfCode = (int)row["WorkFlowCode"];
                    WFUserControl.PerformCallback(ReId, ProjectReTableType, WfCode, e);
                    GridViewProject.DataBind();
                    GridViewProject.ExpandRow(focucedIndex);
                }
            }
            else
            {
                WFUserControl.SetMsgText("برای ارسال پرونده به مرحله بعد ابتدا یک درخواست را انتخاب نمائید");
                WFUserControl.PerformCallback(-2, -2, -2, e);
            }
        }
        else
        {
            WFUserControl.SetMsgText("ردیفی انتخاب نشده است.");
            WFUserControl.PerformCallback(-2, -2, -2, e);
        }
    }

    protected void CallbackProject_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (IsPageRefresh)
        {
            return;
        }
        CallbackProject.JSProperties["cpPrint"] = 0;
        CallbackProject.JSProperties["cpPrintSummary"] = 0;
        CallbackProject.JSProperties["cpPrintSummaryPath"] = "";
        CallbackProject.JSProperties["cpPrintDesPermit"] = 0;
        CallbackProject.JSProperties["cpPrintDesPermitPath"] = "";
        CallbackProject.JSProperties["cpArchiveNoSaved"] = 0;
        int ProjectId = -1;
        int focucedIndex = GridViewProject.FocusedRowIndex;
        if (focucedIndex > -1)
        {
            DataRow row = GridViewProject.GetDataRow(focucedIndex);
            if (row == null)
            {
                SetLabelWarning("ابتدا یک پروژه را انتخاب نمائید.");
                return;
            }
            ProjectId = (int)row["ProjectId"];
        }
        switch (e.Parameter)
        {
            case "New":
                NextPage("New", false);
                break;

            case "Edit":
                NextPage("Edit", false);
                break;

            case "View":
                NextPage("View", false);
                break;

            case "Delete":
                Delete();
                break;
            case "DeleteAll":
                DeleteAll();
                break;
            case "DeleteConfirmedWFState":
                DeleteConfirmedWFState();
                break;

            case "ChangeProjectRequest":
                if (ProjectId == -1)
                {
                    SetLabelWarning("برای ثبت درخواست ابتدا یک پروژه را انتخاب نمائید.");
                    return;
                }
                NextPage("ChangeBaseInfo", true);
                GridViewProject.ExpandRow(focucedIndex);
                break;

            case "EndPrj":
                if (ProjectId == -1)
                {
                    SetLabelWarning("برای ثبت درخواست ابتدا یک پروژه را انتخاب نمائید.");
                    return;
                }
                NextPage("EndPrj", true);
                GridViewProject.ExpandRow(focucedIndex);
                break;          
            case "ChangeRequestLicence":
                if (ProjectId == -1)
                {
                    SetLabelWarning("برای ثبت درخواست ابتدا یک پروژه را انتخاب نمائید.");
                    return;
                }
                NextPage("ChangeRequestLicence", true);
                GridViewProject.ExpandRow(focucedIndex);
                break;
            case "IncreaseBuildingAreaRequest":
                if (ProjectId == -1)
                {
                    SetLabelWarning("برای ثبت درخواست ابتدا یک پروژه را انتخاب نمائید.");
                    return;
                }
                NextPage("IncreaseBuildingAreaRequest", true);
                GridViewProject.ExpandRow(focucedIndex); 
                break;
            case "AdditionalStageRequest":
                if (ProjectId == -1)
                {
                    SetLabelWarning("برای ثبت درخواست ابتدا یک پروژه را انتخاب نمائید.");
                    return;
                }
                NextPage("AdditionalStageRequest", true);
                GridViewProject.ExpandRow(focucedIndex);
                break;
            case "BuildingNotStarted":
                if (ProjectId == -1)
                {
                    SetLabelWarning("برای ثبت درخواست ابتدا یک پروژه را انتخاب نمائید.");
                    return;
                }
                NextPage("BuildingNotStarted", true);
                GridViewProject.ExpandRow(focucedIndex);
                break;
            case "Print":
                #region Print
                Session["DataTable"] = GridViewProject.Columns;
                Session["DataSource"] = ObjectDataSourceProject;
                Session["Title"] = "پروژه ها";
                Session["Header"] = GetRepHeader();
                GridViewProject.DetailRows.CollapseAllRows();
                CallbackProject.JSProperties["cpPrint"] = 1;
                #endregion
                break;

            case "PrintSummary":
                #region PrintSummary
                if (ProjectId == -1)
                {
                    SetLabelWarning("لطفا ابتدا یک پروژه را انتخاب نمائید.");
                    return;
                }
                CallbackProject.JSProperties["cpPrintSummary"] = 1;
                CallbackProject.JSProperties["cpPrintSummaryPath"] = "../../../ReportForms/TechnicalServices/Summary.aspx?ProjectId="
                    + Utility.EncryptQS(ProjectId.ToString());
                #endregion
                break;
            case "SaveArchiveNo":
                if (ProjectId == -1)
                {
                    SetLabelWarning("برای ثبت کد بایگانی ابتدا یک پروژه را انتخاب نمائید.");
                    return;
                }
                SaveArchiveNo(ProjectId);
                break;
            case "ObsPermitPrint":
            case "ObsPermitPrint2":
                #region ObsPermitPrint
                int HasText = 0;
                if (e.Parameter == "ObsPermitPrint2") HasText = 1;
                else HasText = 0;
                if (ProjectId == -1)
                {
                    SetLabelWarning("لطفا ابتدا یک پروژه را انتخاب نمائید.");
                    return;
                }

                TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
                TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
                ProjectRequestManager.FindByProject(ProjectId);
                if (ProjectRequestManager.Count == 0)
                {
                    SetLabelWarning("برای پروژه انتخاب شده درخواستی در سیستم ثبت نشده است.");
                    return;
                }
                int PrjReId = Convert.ToInt32(ProjectRequestManager[0]["PrjReId"]);
                AccountingManager.SelectAccountingForProject(ProjectId, -1, (int)TSP.DataManager.TSProjectIngridientType.Observer);
                if (AccountingManager.Count <= 0)
                {
                    SetLabelWarning("امکان چاپ نامه شهرداری وجود ندارد. فیش دستمزد ناظرین وارد نشده است");
                    return;
                }

                for (int i = 0; i < AccountingManager.Count; i++)
                {
                    if (Convert.ToInt32(AccountingManager[i]["Status"]) != (int)TSP.DataManager.TSAccountingStatus.Payment)
                    {
                        SetLabelWarning("امکان چاپ نامه شهرداری وجود ندارد. فیش دستمزد ناظرین پرداخت نشده است");
                        return;
                    }
                }

                int AccountingId = Convert.ToInt32(AccountingManager[0]["AccountingId"]);
                CallbackProject.JSProperties["cpPrintDesPermit"] = 1;
                CallbackProject.JSProperties["cpPrintDesPermitPath"] = "../../../ReportForms/TechnicalServices/ObserverPermitted.aspx?ProjectId="
                    + Utility.EncryptQS(ProjectId.ToString()) + "&AccountingId=" + Utility.EncryptQS(AccountingId.ToString()) + "&PlansTypeId=" + Utility.EncryptQS("-1") + "&HasText=" + Utility.EncryptQS(HasText.ToString());

                #endregion
                break;
            case "DesPermitPrint":
                #region DesPermitPrint
                if (ProjectId == -1)
                {
                    SetLabelWarning("لطفا ابتدا یک پروژه را انتخاب نمائید.");
                    return;
                }
                string PlansTypeId = GetSelectedInDxDropDown(drdPlanType, "ListBoxPlanType");

                CallbackProject.JSProperties["cpPrintDesPermit"] = 1;
                CallbackProject.JSProperties["cpPrintDesPermitPath"] = "../../../ReportForms/TechnicalServices/DesignerPermitted.aspx?ProjectId="
                    + Utility.EncryptQS(ProjectId.ToString()) + "&PlansTypeId=" + Utility.EncryptQS(PlansTypeId);

                #endregion
                break;

            case "Tracing":
                Tracing();
                break;

            case "ExportExcel":
                btnExportExcel_Click(this, new EventArgs());
                break;

        }
    }
    #endregion
    #endregion

    #region Methods

    private void SaveArchiveNo(int ProjectId)
    {
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        try
        {
            if (string.IsNullOrWhiteSpace(txtArchiveNo.Text))
            {
                SetLabelWarning("کد بایگانی وارد نشده است.");
                return;
            }
            DataTable dtPrj = ProjectManager.SearchProjectByArchiveNo(txtArchiveNo.Text.Trim());
            if (dtPrj.Rows.Count > 0 && Convert.ToInt32(dtPrj.Rows[0]["ProjectId"]) != ProjectId)
            {
                SetLabelWarning("کد وارد شده تکراری می باشد.");
                return;
            }
            ProjectManager.FindByProjectId(ProjectId);
            if (ProjectManager.Count != 1)
            {
                SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            ProjectManager[0].BeginEdit();
            ProjectManager[0]["ArchiveNo"] = txtArchiveNo.Text;
            ProjectManager[0].EndEdit();
            ProjectManager.Save();
            CallbackProject.JSProperties["cpArchiveNoSaved"] = 1;
            GridViewProject.DataBind();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            SetLabelWarning("خطایی در ذخیره ایجاد شده است.");
        }
    }

    private void NextPage(string Mode, bool IsRequest)
    {
        int ProjectId = -1;
        int PrjReId = -1;
        string GridFilterString = GridViewProject.FilterExpression;
        string SearchFilterString = GenerateFilterString();
        if (GridViewProject.FocusedRowIndex <= -1 && Mode != "New")
        {
            SetLabelWarning("ابتدا یک پروژه را انتخاب نمایید");
            return;
        }
        if (GridViewProject.FocusedRowIndex > -1 && Mode != "New" && !IsRequest)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewProjectRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewProject.FindDetailRowTemplateControl(GridViewProject.FocusedRowIndex, "GridViewProjectRequest");
            if (GridViewProjectRequest == null)
            {
                SetLabelWarning("برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
            if (GridViewProjectRequest.FocusedRowIndex > -1)
            {
                DataRow row = GridViewProjectRequest.GetDataRow(GridViewProjectRequest.FocusedRowIndex);
                ProjectId = (int)row["ProjectId"];

                if (!IsRequest)
                    PrjReId = (int)row["PrjReId"];
                else
                {
                    TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
                    ProjectRequestManager.SelectRequestLastVersion(ProjectId, -1, 1);
                    if (ProjectRequestManager.Count > 0)
                        PrjReId = Convert.ToInt32(ProjectRequestManager[0]["PrjReId"]);
                    else PrjReId = (int)row["PrjReId"];
                }
            }
        }
        else if (IsRequest)
        {
            DataRow Prjrow = GridViewProject.GetDataRow(GridViewProject.FocusedRowIndex);
            ProjectId = (int)Prjrow["ProjectId"];
            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
            DataTable dtReq = ProjectRequestManager.SelectRequestIdLastVersion(ProjectId, -1);
            if (dtReq.Rows.Count == 0)
            {
                SetLabelWarning("خطا در بازخوانی اطلاعات ایجاد شده است");
                return;
            }
            if (Convert.ToInt32(dtReq.Rows[0]["IsConfirmed"]) == 0)
            {
                SetLabelWarning("به دلیل وجود درخواست درجریان برای پروژه انتخاب شده،امکان ثبت درخواست جدید وجود ندارد");
                return;
            }
            PrjReId = Convert.ToInt32(dtReq.Rows[0]["PrjReId"]);
        }

        if ((PrjReId == -1 || ProjectId == -1) && Mode != "New" && !IsRequest)
        {
            SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک درخواست را انتخاب نمائید");
        }
        else
        {
            if (Mode == "Edit" && !CheckWorkFlowPermissionForEdit(PrjReId))
            {
                SetLabelWarning("در این مرحله از گردش کار قادر به ویرایش اطلاعات پایه پروژه نمی باشید.");
                return;
            }

            if (IsCallback)
                ASPxWebControl.RedirectOnCallback("ProjectInsert.aspx?ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) + "&PrjReId="
                    + Utility.EncryptQS(PrjReId.ToString())
                    + "&PageMode=" + Utility.EncryptQS(Mode)
                    + "&GrdFlt=" + Utility.EncryptQS(GridFilterString)
                    + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
            else
                Response.Redirect("ProjectInsert.aspx?ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) + "&PrjReId="
                    + Utility.EncryptQS(PrjReId.ToString())
                    + "&PageMode=" + Utility.EncryptQS(Mode)
                    + "&GrdFlt=" + Utility.EncryptQS(GridFilterString)
                    + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString));
        }
    }

    private void Print()
    {
        //string FilterExp;
        //FilterExp = GridViewProject.FilterExpression;

        //Response.Redirect("~/ReportForms/Accounting/ProjectReport.aspx?FilterExp=" + Utility.EncryptQS(FilterExp));
    }

    void Search()
    {

        if (!string.IsNullOrEmpty(txtCode.Text))
            ObjectDataSourceProject.SelectParameters["ProjectId"].DefaultValue = txtCode.Text.Trim();
        else
            ObjectDataSourceProject.SelectParameters["ProjectId"].DefaultValue = "-1";
        if (ASPxComboBoxStructureGroups.SelectedIndex != -1)
            ObjectDataSourceProject.SelectParameters["GroupId"].DefaultValue = ASPxComboBoxStructureGroups.Value.ToString();
        else
            ObjectDataSourceProject.SelectParameters["GroupId"].DefaultValue = "-1";

        if (ASPxComboBoxCity.SelectedItem != null && ASPxComboBoxCity.SelectedItem.Value != null)
            ObjectDataSourceProject.SelectParameters["CitId"].DefaultValue = ASPxComboBoxCity.Value.ToString();
        else
            ObjectDataSourceProject.SelectParameters["CitId"].DefaultValue = "-1";

        if (ASPxComboBoxMunicipality.SelectedItem != null && ASPxComboBoxMunicipality.SelectedItem.Value != null)
            ObjectDataSourceProject.SelectParameters["MunId"].DefaultValue = ASPxComboBoxMunicipality.Value.ToString();
        else
            ObjectDataSourceProject.SelectParameters["MunId"].DefaultValue = "-1";
        if (!string.IsNullOrEmpty(txtName.Text))
            ObjectDataSourceProject.SelectParameters["ProjectName"].DefaultValue = txtName.Text.Trim();
        else
            ObjectDataSourceProject.SelectParameters["ProjectName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtPelak.Text))
            ObjectDataSourceProject.SelectParameters["RegisteredNo"].DefaultValue = txtPelak.Text.Trim();
        else
            ObjectDataSourceProject.SelectParameters["RegisteredNo"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtFileNumber.Text))
            ObjectDataSourceProject.SelectParameters["FileNo"].DefaultValue = txtFileNumber.Text.Trim();
        else
            ObjectDataSourceProject.SelectParameters["FileNo"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtMapNumer.Text))
            ObjectDataSourceProject.SelectParameters["PlansMethodNo"].DefaultValue = txtMapNumer.Text.Trim();
        else
            ObjectDataSourceProject.SelectParameters["PlansMethodNo"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtDateFrom.Text))
            ObjectDataSourceProject.SelectParameters["FromDate"].DefaultValue = txtDateFrom.Text.Trim();
        else
            ObjectDataSourceProject.SelectParameters["FromDate"].DefaultValue = "1";

        if (!string.IsNullOrEmpty(txtDateTo.Text))
            ObjectDataSourceProject.SelectParameters["ToDate"].DefaultValue = txtDateTo.Text.Trim();
        else
            ObjectDataSourceProject.SelectParameters["ToDate"].DefaultValue = "2";

        if (!string.IsNullOrEmpty(txtLicenceNo.Text))
            ObjectDataSourceProject.SelectParameters["LicenseNo"].DefaultValue = txtLicenceNo.Text.Trim();
        else
            ObjectDataSourceProject.SelectParameters["LicenseNo"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtOwnerName.Text))
            ObjectDataSourceProject.SelectParameters["OwnerName"].DefaultValue = txtOwnerName.Text.Trim();
        else
            ObjectDataSourceProject.SelectParameters["OwnerName"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtSearchArchiveNo.Text))
            ObjectDataSourceProject.SelectParameters["ArchiveNo"].DefaultValue = txtSearchArchiveNo.Text.Trim();
        else
            ObjectDataSourceProject.SelectParameters["ArchiveNo"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtProjectNo.Text))
            ObjectDataSourceProject.SelectParameters["ProjectNo"].DefaultValue = txtProjectNo.Text.Trim();
        else
            ObjectDataSourceProject.SelectParameters["ProjectNo"].DefaultValue = "-1";

        if (!string.IsNullOrEmpty(txtMainRegion.Text))
            ObjectDataSourceProject.SelectParameters["MainRegion"].DefaultValue = txtMainRegion.Text.Trim();
        else
            ObjectDataSourceProject.SelectParameters["MainRegion"].DefaultValue = "%";

        if (!string.IsNullOrEmpty(txtMainSection.Text))
            ObjectDataSourceProject.SelectParameters["MainSection"].DefaultValue = txtMainSection.Text.Trim();
        else
            ObjectDataSourceProject.SelectParameters["MainSection"].DefaultValue = "%";
        if (CmbTask.SelectedItem != null && CmbTask.SelectedItem.Value != null)
            ObjectDataSourceProject.SelectParameters["TaskId"].DefaultValue = CmbTask.Value.ToString();
        else
            ObjectDataSourceProject.SelectParameters["TaskId"].DefaultValue = "-1";

        if (cmbDiscountPercent.SelectedIndex != 0)
            ObjectDataSourceProject.SelectParameters["DiscountPercentId"].DefaultValue = cmbDiscountPercent.Value.ToString();
        else
            ObjectDataSourceProject.SelectParameters["DiscountPercentId"].DefaultValue = "-1";


        GridViewProject.DataBind();
    }

    void Clear()
    {
        ObjectDataSourceProject.SelectParameters["ProjectId"].DefaultValue = "-1";
        ObjectDataSourceProject.SelectParameters["GroupId"].DefaultValue = "-1";
        ObjectDataSourceProject.SelectParameters["CitId"].DefaultValue = "-1";
        ObjectDataSourceProject.SelectParameters["MunId"].DefaultValue = "-1";
        ObjectDataSourceProject.SelectParameters["ProjectName"].DefaultValue = "%";
        ObjectDataSourceProject.SelectParameters["RegisteredNo"].DefaultValue = "%";
        ObjectDataSourceProject.SelectParameters["FileNo"].DefaultValue = "%";
        ObjectDataSourceProject.SelectParameters["PlansMethodNo"].DefaultValue = "%";
        ObjectDataSourceProject.SelectParameters["FromDate"].DefaultValue = "1";
        ObjectDataSourceProject.SelectParameters["ToDate"].DefaultValue = "2";
        ObjectDataSourceProject.SelectParameters["LicenseNo"].DefaultValue = "%";
        ObjectDataSourceProject.SelectParameters["OwnerName"].DefaultValue = "%";
        ObjectDataSourceProject.SelectParameters["ArchiveNo"].DefaultValue = "%";
        ObjectDataSourceProject.SelectParameters["ProjectNo"].DefaultValue = "-1";
        ObjectDataSourceProject.SelectParameters["MainRegion"].DefaultValue = "%";
        ObjectDataSourceProject.SelectParameters["MainSection"].DefaultValue = "%";
        ObjectDataSourceProject.SelectParameters["TaskId"].DefaultValue = "-1";
        ObjectDataSourceProject.SelectParameters["DiscountPercentId"].DefaultValue = "-1";


        GridViewProject.DataBind();

        CmbTask.DataBind();
        CmbTask.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
        CmbTask.SelectedIndex = 0;

        ASPxComboBoxCity.DataBind();
        ASPxComboBoxCity.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
        ASPxComboBoxCity.SelectedIndex = 0;

        ASPxComboBoxMunicipality.DataBind();
        ASPxComboBoxMunicipality.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
        ASPxComboBoxMunicipality.SelectedIndex = 0;


    }
    #region Delete
    /*************************************************************** Delete ******************************************************************/
    private void DeleteConfirmedWFState()
    {
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        try
        {
            int PrjReId = -1;

            if (GridViewProject.FocusedRowIndex > -1)
            {
                TSP.WebControls.CustomAspxDevGridView GridViewProjectRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewProject.FindDetailRowTemplateControl(GridViewProject.FocusedRowIndex, "GridViewProjectRequest");
                if (GridViewProjectRequest != null)
                {
                    if (GridViewProjectRequest.FocusedRowIndex > -1)
                    {
                        transact.Add(ProjectRequestManager);
                        DataRow row = GridViewProjectRequest.GetDataRow(GridViewProjectRequest.FocusedRowIndex);
                        PrjReId = (int)row["PrjReId"];
                        int PrjReTypeId = (int)row["PrjReTypeId"];
                        int CurrentTaskCode = (int)row["TaskCode"];
                        if (CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.ConfirmingProjectAndEndProccess &&
                            CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.RejectProjectAndEndProcess)
                        {
                            SetLabelWarning("تنها امکان به تعلیق در آوردن درخواست تایید/عدم تایید شه پروژه ها وجود دارد");
                            return;
                        }
                        //int ConfirmedWfTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmingProjectAndEndProccess;
                        transact.BeginSave();
                        ProjectRequestManager.DeleteConfirmedStateForTSProject(PrjReId, CurrentTaskCode);
                        transact.EndSave();
                        SetLabelWarning("ذخیره انجام شد");
                        GridViewProject.DataBind();
                    }
                }
                else
                {
                    SetLabelWarning("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                    return;
                }
            }
            else
            {
                SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            }
        }
        catch (Exception ex)
        {
            transact.CancelSave();
            Utility.SaveWebsiteError(ex);

            SetLabelWarning("خطا در ذخیره انجام شد");
        }
    }

    private void DeleteAll()
    {
        int ProjectId = -1;
        int PrjReId = -1;

        if (GridViewProject.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewProjectRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewProject.FindDetailRowTemplateControl(GridViewProject.FocusedRowIndex, "GridViewProjectRequest");
            if (GridViewProjectRequest != null)
            {
                if (GridViewProjectRequest.FocusedRowIndex > -1)
                {
                    DataRow row = GridViewProjectRequest.GetDataRow(GridViewProjectRequest.FocusedRowIndex);
                    ProjectId = (int)row["ProjectId"];
                    PrjReId = (int)row["PrjReId"];
                    int PrjReTypeId = (int)row["PrjReTypeId"];
                    DeleteRequest(ProjectId, PrjReId, PrjReTypeId);
                }
            }
            else
            {
                SetLabelWarning("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
        }
        else
        {
            SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
        }
    }
    private void Delete()
    {
        int ProjectId = -1;
        int PrjReId = -1;

        if (GridViewProject.FocusedRowIndex > -1)
        {
            TSP.WebControls.CustomAspxDevGridView GridViewProjectRequest = (TSP.WebControls.CustomAspxDevGridView)GridViewProject.FindDetailRowTemplateControl(GridViewProject.FocusedRowIndex, "GridViewProjectRequest");
            if (GridViewProjectRequest != null)
            {
                if (GridViewProjectRequest.FocusedRowIndex > -1)
                {
                    DataRow row = GridViewProjectRequest.GetDataRow(GridViewProjectRequest.FocusedRowIndex);
                    ProjectId = (int)row["ProjectId"];
                    PrjReId = (int)row["PrjReId"];
                    int PrjReTypeId = (int)row["PrjReTypeId"];
                    if (CheckWFPermissionForDeleteRequest(PrjReId, PrjReTypeId))
                    {
                        DeleteRequest(ProjectId, PrjReId, PrjReTypeId);
                    }
                    else
                        SetLabelWarning("در این مرحله از گردش کار قادر به حذف درخواست نیستید.");
                }
            }
            else
            {
                SetLabelWarning("لطفاً ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
        }
        else
        {
            SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
        }
    }

    private void DeleteRequest(int ProjectId, int PrjReId, int PrjReTypeId)
    {
        TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();

        transact.Add(ProjectRequestManager);
        try
        {
            transact.BeginSave();

            if (PrjReTypeId == (int)TSP.DataManager.TSProjectRequestType.InsertProject)
            {
                int WfCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
                ProjectRequestManager.DeleteProjectInsertRequest(ProjectId, PrjReId, WfCode);
            }
            else if (PrjReTypeId !=0)
            {
                int WfCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
                ProjectRequestManager.DeleteProjectRequest(ProjectId,PrjReId, WfCode);
            }
            else
            {
                SetLabelWarning("برای حذف درخواست از طریق صفحات فرآیند مربوطه اقدام کنید.");
                transact.CancelSave();
                return;
            }

            transact.EndSave();
            GridViewProject.DataBind();
            SetLabelWarning("حذف انجام شد");
        }
        catch (Exception err)
        {
            transact.CancelSave();
            SetError(err);
        }
    }

    #endregion

    /*************************************************************************************************************/
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

    /*************************************************************************************************************/
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

    String GetSelectedInDxDropDown(ASPxDropDownEdit DropDown, String ListBoxName)
    {
        string Param = "(";
        bool flag = false;

        ASPxListBox ListBox = (ASPxListBox)DropDown.FindControl(ListBoxName);
        if (ListBox == null)
            return "";

        for (int i = 0; i < ListBox.SelectedItems.Count; i++)
        {
            if (ListBox.SelectedItems[i].Value != null)
            {
                if (Param != "(")
                    Param += "," + ListBox.SelectedItems[i].Value.ToString();
                else
                    Param += ListBox.SelectedItems[i].Value.ToString();

                flag = true;
            }
        }

        if (flag)
        {
            Param += ")";
            return Param;
        }
        return "(0)";
    }

    void LoadWfHelp()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        DataTable dt = WorkFlowTaskManager.SelectWorkFlowTaskActiveTasks(((int)TSP.DataManager.WorkFlows.TSProjectConfirming));
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
            RepeaterWfHelp1.DataSource = dt1;
            RepeaterWfHelp1.DataBind();
        }
        if (dt2.Rows.Count != 0)
        {
            RepeaterWfHelp2.DataSource = dt2;
            RepeaterWfHelp2.DataBind();
        }
        if (dt3.Rows.Count != 0)
        {
            RepeaterWfHelp3.DataSource = dt3;
            RepeaterWfHelp3.DataBind();
        }


        DataTable dtWFPlan = WorkFlowTaskManager.SelectWorkFlowTaskActiveTasks(((int)TSP.DataManager.WorkFlows.TSPlansConfirming));
        DataTable dtWFPlan1 = dt.Clone();
        DataTable dtWFPlan2 = dt.Clone();
        DataTable dtWFPlan3 = dt.Clone();
        int countWF = 0;
        foreach (DataRow dr in dtWFPlan.Rows)
        {
            if (countWF % 3 == 0) { dtWFPlan1.Rows.Add(dr.ItemArray); }
            if (countWF % 3 == 1) { dtWFPlan2.Rows.Add(dr.ItemArray); }
            if (countWF % 3 == 2) { dtWFPlan3.Rows.Add(dr.ItemArray); }
            countWF++;
        }
        if (dtWFPlan1.Rows.Count != 0)
        {
            RepeaterWfPLnaHelp1.DataSource = dtWFPlan1;
            RepeaterWfPLnaHelp1.DataBind();
        }
        if (dtWFPlan2.Rows.Count != 0)
        {
            RepeaterWfPLnaHelp2.DataSource = dtWFPlan2;
            RepeaterWfPLnaHelp2.DataBind();
        }
        if (dtWFPlan3.Rows.Count != 0)
        {
            RepeaterWfPLnaHelp3.DataSource = dtWFPlan3;
            RepeaterWfPLnaHelp3.DataBind();
        }
    }
    //private void SetlblRequestMsg(bool SuccessRequest, bool Request, string Msg, System.Drawing.Color Color)
    //{
    //    PanelSuccessRequest.ClientVisible = SuccessRequest;
    //    PanelRequest.ClientVisible = Request;
    //    lblRequestMsg.ForeColor = Color;
    //    lblRequestMsg.Text = Msg;
    //}

    //private void SetCallBackErr(Exception err)
    //{
    //    if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
    //    {
    //        System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

    //        if (se.Number == 2601)
    //        {
    //            SetlblRequestMsg(true, false, "اطلاعات تکراری می باشد", System.Drawing.Color.Red);
    //        }
    //        else if (se.Number == 2627)
    //        {
    //            SetlblRequestMsg(true, false, "کد تکراری می باشد", System.Drawing.Color.Red);
    //        }
    //        else if (se.Number == 547)
    //        {
    //            SetlblRequestMsg(true, false, "اطلاعات وابسته معتبر نمی باشد.", System.Drawing.Color.Red);
    //        }
    //        else
    //        {
    //            SetlblRequestMsg(true, false, "خطایی در ذخیره انجام گرفته است", System.Drawing.Color.Red);
    //        }
    //    }
    //    else
    //    {
    //        SetlblRequestMsg(true, false, "خطایی در ذخیره انجام گرفته است", System.Drawing.Color.Red);
    //    }
    //}

    #region SetGridIndex

    private void SetPageFilter()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
            {
                string GrdFlt = Utility.DecryptQS(Request.QueryString["GrdFlt"].ToString());
                string SrchFlt = Utility.DecryptQS(Request.QueryString["SrchFlt"].ToString());

                if (!string.IsNullOrEmpty(SrchFlt))
                    FilterObjdsByValue(SrchFlt);
                if (!string.IsNullOrEmpty(GrdFlt))
                    GridViewProject.FilterExpression = GrdFlt;
            }
        }
    }

    private int SetGridRowIndex()
    {
        int Index = -1;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["PostId"]))
            {
                string PostId = Utility.DecryptQS(Request.QueryString["PostId"].ToString());
                if (!string.IsNullOrEmpty(PostId))
                {
                    int PostKeyValue = int.Parse(PostId);

                    GridViewProject.DataBind();
                    Index = GridViewProject.FindVisibleIndexByKeyValue(PostKeyValue);
                    int PageIndex = -1;
                    if (Index >= 0)
                        PageIndex = Index / GridViewProject.SettingsPager.PageSize;
                    if (PageIndex >= 0)
                        GridViewProject.PageIndex = PageIndex;
                    if (Index >= 0)
                    {
                        GridViewProject.JSProperties["cpSelectedIndex"] = Index;
                        GridViewProject.DetailRows.ExpandRow(Index);
                        GridViewProject.FocusedRowIndex = Index;
                        GridViewProject.JSProperties["cpIsReturn"] = 1;
                    }
                }
            }
        }
        return Index;
    }

    private string GenerateFilterString()
    {
        string FilterString = "";

        for (int i = 0; i < ObjectDataSourceProject.SelectParameters.Count; i++)
        {
            if (ObjectDataSourceProject.SelectParameters[i].DefaultValue != "%")
            {
                FilterString += ObjectDataSourceProject.SelectParameters[i].Name + "&";
                FilterString += ObjectDataSourceProject.SelectParameters[i].DefaultValue + "&";
            }
        }
        FilterString = FilterString.Remove(FilterString.Length - 1);
        return FilterString;
    }

    private void FilterObjdsByValue(string FilterString)
    {
        string[] SearchFilter = FilterString.Split('&');
        for (int i = 0; i < SearchFilter.Length; i = i + 2)
        {
            string ParameterName = SearchFilter[i].ToString();
            string Value = SearchFilter[i + 1].ToString();
            ObjectDataSourceProject.SelectParameters[ParameterName].DefaultValue = Value;
            if (Value != "%")
            {
                switch (ParameterName)
                {
                    case "ProjectId":
                        if (Value != "-1")
                            txtCode.Text = Value;
                        break;
                    case "ProjectName":
                        txtName.Text = Value;
                        break;
                    case "RegisteredNo":
                        txtPelak.Text = Value;
                        break;
                    case "FileNo":
                        txtFileNumber.Text = Value;
                        break;
                    case "PlansMethodNo":
                        txtMapNumer.Text = Value;
                        break;
                    case "GroupId":
                        ASPxComboBoxStructureGroups.DataBind();
                        if (Value == "-1")
                        {
                            ASPxComboBoxStructureGroups.DataBind();
                            ASPxComboBoxStructureGroups.SelectedIndex = -1;
                        }
                        else
                        {
                            ASPxComboBoxStructureGroups.SelectedIndex = ASPxComboBoxStructureGroups.Items.FindByValue(Convert.ToInt32(Value)).Index;
                        }
                        break;
                    case "CitId":
                        ASPxComboBoxCity.DataBind();
                        if (Value == "-1")
                        {
                            ASPxComboBoxCity.DataBind();

                            ASPxComboBoxCity.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
                            ASPxComboBoxCity.SelectedIndex = 0;
                        }
                        else
                        {
                            ASPxComboBoxCity.DataBind();
                            ASPxComboBoxCity.SelectedIndex = ASPxComboBoxCity.Items.FindByValue(Value).Index;
                        }
                        break;
                    case "MunId":
                        ASPxComboBoxMunicipality.DataBind();
                        if (Value == "-1")
                        {
                            ASPxComboBoxMunicipality.DataBind();

                            ASPxComboBoxMunicipality.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
                            ASPxComboBoxMunicipality.SelectedIndex = 0;

                        }
                        else
                        {
                            ASPxComboBoxMunicipality.SelectedIndex = ASPxComboBoxMunicipality.Items.FindByValue(Convert.ToInt32(Value)).Index;
                        }
                        break;
                    case "FromDate":
                        if (Value != "1")
                            txtDateFrom.Text = Value;
                        break;
                    case "ToDate":
                        if (Value != "2")
                            txtDateTo.Text = Value;
                        break;
                    case "ProjectNo":
                        if (Value != "-1")
                            txtProjectNo.Text = Value;
                        break;

                }
            }
        }
    }



    #endregion

    /***************************************************************** WorkFlow *****************************************************************/
    #region WorkFlow

    private void Tracing()
    {
        int focucedIndex = -1;

        if (GridViewProject.FocusedRowIndex > -1)
        {
            int PostId = int.Parse(GridViewProject.GetDataRow(GridViewProject.FocusedRowIndex)["ProjectId"].ToString());
            string GridFilterString = GridViewProject.FilterExpression;
            string SearchFilterString = GenerateFilterString();

            TSP.WebControls.CustomAspxDevGridView grid = (TSP.WebControls.CustomAspxDevGridView)GridViewProject.FindDetailRowTemplateControl(GridViewProject.FocusedRowIndex, "GridViewProjectRequest");
            if (grid != null)
            {
                focucedIndex = grid.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = grid.GetDataRow(focucedIndex);
                    int PrjReTypeId = (int)row["PrjReTypeId"];
                    if (PrjReTypeId != 0)
                    {
                        int TableId = (int)row["PrjReId"];
                        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest);
                        int WorkFlowCode = (int)row["WorkFlowCode"];

                        String Url = Request.Url.AbsoluteUri.Split('?')[0] + "?GrdFlt=" + Utility.EncryptQS(GridFilterString) + "&SrchFlt=" + Utility.EncryptQS(SearchFilterString) +
                        "&PostId=" + Utility.EncryptQS(PostId.ToString());

                        if (IsCallback)
                            ASPxWebControl.RedirectOnCallback("../../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));
                        else
                            Response.Redirect("../../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()) + "&WorkFlowCode=" + Utility.EncryptQS(WorkFlowCode.ToString()) + "&UrlReferrer=" + Utility.EncryptQS(Url));

                        //  Response.Redirect("../WorkFlow/WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
                    }
                    else
                    {
                        SetLabelWarning("این مورد جز درخواست های میانی این پروژه است برای پیگیری به فرم های مربوطه ی درونی مراجعه کنید");
                        return;
                    }
                }
            }
            else
            {
                SetLabelWarning("ابتدا یک درخواست را انتخاب نمائید");
                return;
            }
        }
        else
        {
            SetLabelWarning("ردیفی انتخاب نشده است.");
        }
    }

    #region WorkFlowPermission

    /****************************************************************** BtnNew *************************************************************/
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForSave();
    }

    private void CheckWorkFlowPermissionForSave()
    {
        //*****TableType
        int TableType = (int)TSP.DataManager.TableCodes.TSProjectRequest;

        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNewForManagementPage(TaskCode, TableType, Utility.GetCurrentUser_UserId());

        btnNew.Enabled = WFPer.BtnNew;
        btnNew2.Enabled = WFPer.BtnNew;
    }

    /****************************************************************** BtnEdit *************************************************************/
    private bool CheckWorkFlowPermissionForEdit(int PrjReId)
    {
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());


        return WFPer.BtnEdit;
    }

    /****************************************************************** btnDelete **************************************************************/
    private bool CheckWFPermissionForDeleteRequest(int PrjReId, int RequestType)
    {
        ArrayList TaskCode = GetProjectWfCode(RequestType);

        int WFCode = Convert.ToInt32(TaskCode[0]);

        //*******Editing Task Code
        int PrjTaskCode = Convert.ToInt32(TaskCode[1]);

        //TSP.DataManager.WorkFlowPermission WFPermission = new TSP.DataManager.WorkFlowPermission();

        return TSP.DataManager.WorkFlowPermission.CheckWFPermissionForDeleteRequest(PrjReId, WFCode, PrjTaskCode, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId);
        //return WFPermission.CheckWFPermissionForDeleteRequest(PrjReId, WFCode, PrjTaskCode, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId);
    }

    private ArrayList GetProjectWfCode(int RequestType)
    {
        int WfCode = -2;
        int SaveTaskCode = -2;

        switch (RequestType)
        {
            case (int)TSP.DataManager.TSProjectRequestType.InsertProject:
                WfCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
                SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;
                break;

            case (int)TSP.DataManager.TSProjectRequestType.ChangeProject:
                WfCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
                SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;
                break;                

            case (int)TSP.DataManager.TSProjectRequestType.ChangeImplementerAgentRequest:
                WfCode = (int)TSP.DataManager.WorkFlows.TSChangeImplementerAgentConfirming;
                //SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;
                break;

            case (int)TSP.DataManager.TSProjectRequestType.ChangePlansMethodRequest:
                WfCode = (int)TSP.DataManager.WorkFlows.TSPlanMethodsChangesConfirming;
                //SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;
                break;

            case (int)TSP.DataManager.TSProjectRequestType.ChangeImplementerRequest:
                WfCode = (int)TSP.DataManager.WorkFlows.TSChangeImplementerConfirming;
                //SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;
                break;

            case (int)TSP.DataManager.TSProjectRequestType.EndProjectCertificateRequest:
                WfCode = (int)TSP.DataManager.WorkFlows.TSEndStructuralProjectLicenceConfirming;
                //SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo;
                break;
        }

        ArrayList TaskCode = new ArrayList();
        TaskCode.Add(WfCode);
        TaskCode.Add(SaveTaskCode);
        return TaskCode;
    }

    /********************************************************************************************************************************************/

    private int GetRequestType(string RequestType)
    {
        switch (RequestType)
        {
            case "ChangeImplementerAgentRequest":
                return (int)TSP.DataManager.TSProjectRequestType.ChangeImplementerAgentRequest;

            case "ChangeImplementerRequest":
                return (int)TSP.DataManager.TSProjectRequestType.ChangeImplementerRequest;

            case "ChangeObservationRequest":
                return (int)TSP.DataManager.TSProjectRequestType.ChangeObservationRequest;

            case "ChangePlansMethodRequest":
                return (int)TSP.DataManager.TSProjectRequestType.ChangePlansMethodRequest;

            case "EndProjectCertificateRequest":
                return (int)TSP.DataManager.TSProjectRequestType.EndProjectCertificateRequest;

            case "ChangeProjectRequest":
                return (int)TSP.DataManager.TSProjectRequestType.ChangeProject;

            default:
                return -2;
        }
    }

    private bool CheckProjectWorkFlowPermissionForDesigners(int PrjReId)
    {
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        //*******Editing Task Code
        int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;
        int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;

        TSP.DataManager.WFPermission PerAtchitecturalPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ArchitecturalPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerStructurePlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(StructurePlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerElectricalInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ElectricalInsPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission PerMechanicInsPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(MechanicInsPlanTaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());

        return (PerAtchitecturalPlan.BtnNew || PerStructurePlan.BtnNew || PerElectricalInsPlan.BtnNew || PerMechanicInsPlan.BtnNew);
    }

    private bool CheckProjectWorkFlowPermissionForImplementers(int PrjReId)
    {
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementerOfProject;

        int ChangeWFCode = (int)TSP.DataManager.WorkFlows.TSChangeImplementerConfirming;
        int ChangeTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveChangeProjectImplementerRequestInfo;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(TaskCode, WFCode, PrjReId, Utility.GetCurrentUser_UserId());
        TSP.DataManager.WFPermission ChangeWFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(ChangeTaskCode, ChangeWFCode, PrjReId, Utility.GetCurrentUser_UserId());

        return WFPer.BtnNew || ChangeWFPer.BtnNew;
    }

    private bool CheckProjectWorkFlowPermissionForObservers(int PrjReId)
    {
        //****TableType
        int ProjectWFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject;

        TSP.DataManager.WFPermission SaveWFPer = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(SaveTaskCode, ProjectWFCode, PrjReId, Utility.GetCurrentUser_UserId());

        return SaveWFPer.BtnNew ;
    }

    #endregion

    #endregion

    #endregion
}