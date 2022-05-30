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
using System.IO;

public partial class Employee_TechnicalServices_Project_ContractInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    DataTable dtJudgmentGroup;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

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

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Project.aspx");
            }

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ContractManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            if (string.IsNullOrEmpty(Request.QueryString["PageMode2"]) || string.IsNullOrEmpty(Request.QueryString["ContractId"]) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode2"].ToString())) != "New"))
            {
                string QS = "ProjectId=" + Request.QueryString["ProjectId"].ToString() +
                    "&PrjReId=" + Request.QueryString["PrjReId"].ToString() +
                    "&PageMode=" + Request.QueryString["PageMode"].ToString();

                Response.Redirect("Contract.aspx?" + QS);
            }

            if (Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName] != null)
            {
                String QueryValue = Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName];
                if (TSP.DataManager.Automation.AttachPageToLetter.CheckPageParameterValue(QueryValue) == false)
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }

            Session["AttachContractName"] = null;
            Session["AttachContract"] = null;

            Session["JudgmentGroupManager"] = null;
            Session["JudgmentGroupManager"] = MakeTblJudgmentGroup(); //CreateJudgmentGroupManager();

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("Edit");
        SetEditModeKeys();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        if (CmbType.Value == null)
        {
            SetLabelWarning("نوع قرارداد را انتخاب نمایید");
            return;
        }

        if (CmbType.Value.ToString() == ((int)TSP.DataManager.TSProjectIngridientType.Implementer).ToString())
        {
            if (ASPxRoundPanelJudgmentGroup.ClientVisible == true && CustomAspxDevGridView1.VisibleRowCount != 3)
            {
                SetLabelWarning("برای هیئت حل اختلاف وارد کردن 3 نفر الزامی می باشد");
                return;
            }
        }

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

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("New");
        SetNewModeKeys();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["Judgment"] = null;
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect("Contract.aspx?ProjectId=" + HDProjectId.Value
            + "&PageMode=" + Request.QueryString["PageMode"] + "&PrjReId=" + RequestId.Value
            + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);

    }

    protected void txtParentId_TextChanged(object sender, EventArgs e)
    {
        CheckParentId();
    }

    protected void CmbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetNameAndType();
    }

    protected void btnAddJudg_Click(object sender, EventArgs e)
    {
        RowInserting();
    }

    protected void CustomAspxDevGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        if (Session["JudgmentGroupManager"] != null)
        {
            DataTable dt = (DataTable)Session["JudgmentGroupManager"];
            dt.Rows.Find(e.Keys["Id"]).Delete();
            FillGrid();
        }
        e.Cancel = true;

        //TSP.DataManager.TechnicalServices.JudgmentGroupManager JudgmentGroupManager = (TSP.DataManager.TechnicalServices.JudgmentGroupManager)Session["JudgmentGroupManager"];
        //DataRow rowJudgmentGroup = JudgmentGroupManager.DataTable.Rows.Find(e.Keys["JudgmentGroupId"]);
        //rowJudgmentGroup.Delete();

        //e.Cancel = true;

        //rowJudgmentGroup.EndEdit();

        //CustomAspxDevGridView1.DataSource = JudgmentGroupManager.DataTable;
        //CustomAspxDevGridView1.KeyFieldName = "JudgmentGroupId";
        //CustomAspxDevGridView1.DataBind();
    }

    protected void flpContract_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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
    #endregion

    #region Set&Get
    private void SetKeys()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode2"].ToString());
            HDProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"]).ToString();
            HDContractId.Value = Server.HtmlDecode(Request.QueryString["ContractId"]).ToString();
            RequestId.Value = Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString();

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string ProjectId = Utility.DecryptQS(HDProjectId.Value);
            string ContractId = Utility.DecryptQS(HDContractId.Value);
            string PrjReId = Utility.DecryptQS(RequestId.Value);
            string PrePagemode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString()));

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(ContractId) || string.IsNullOrEmpty(PrjReId) || string.IsNullOrEmpty(PrePagemode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            FillProjectInfo(int.Parse(PrjReId));
            SetMode(PageMode);
            CheckWorkFlowPermission();

            ObjectDataSourceType.FilterExpression = "ProjectIngridientTypeId=" + ((int)TSP.DataManager.TSProjectIngridientType.Designer).ToString()
                + " OR ProjectIngridientTypeId=" + ((int)TSP.DataManager.TSProjectIngridientType.Implementer).ToString() + " OR ProjectIngridientTypeId="
                + ((int)TSP.DataManager.TSProjectIngridientType.Observer).ToString();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
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
        BtnNew.Enabled = true;
        BtnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        CheckAccess();

        ClearForm();
        SetEnable(true);

        Session["JudgmentGroupManager"] = null;
        Session["JudgmentGroupManager"] = MakeTblJudgmentGroup(); //CreateJudgmentGroupManager();
        FillGrid();

        ASPxRoundPanel2.HeaderText = "جدید";
        CheckWorkFlowPermissionForBtnNew();
    }

    private void SetEditModeKeys()
    {
        FillForm();
        BtnNew.Enabled = true;
        BtnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        CheckAccess();

        SetEnable(true);
        CmbMajor.Enabled = false;
        CmbType.Enabled = false;

        ASPxRoundPanel2.HeaderText = "ویرایش";
    }

    private void SetViewModeKeys()
    {
        BtnNew.Enabled = true;
        BtnNew2.Enabled = true;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        CheckAccess();

        SetEnable(false);

        //tbl1.Visible = false;

        if (ASPxRoundPanelJudgmentGroup.ClientVisible)
        {
            tbl1.Visible = false;
            CustomAspxDevGridView1.Columns[5].Visible = false;
        }

        ASPxRoundPanel2.HeaderText = "مشاهده";
        FillForm();
    }

    private void SetEnable(bool Enable)
    {
        CmbType.Enabled = Enable;
        CmbMajor.Enabled = Enable;
        //txtType.Enabled = Enable;
        //txtName.Enabled = Enable;
        txtDuration.Enabled = Enable;
        txtAmount.Enabled = Enable;
        txtWage.Enabled = Enable;
        txtContractDate.Enabled = Enable;
        ChbMaster.Enabled = Enable;
        txtParentId.Enabled = Enable;
        flpContract.ClientVisible = Enable;

        txtoAddress.Enabled = Enable;
        txtoBirthPlace.Enabled = Enable;
        txtoFatherName.Enabled = Enable;
        txtoFirstName.Enabled = Enable;
        txtoIdNo.Enabled = Enable;
        txtoLastName.Enabled = Enable;
        txtoMobileNo.Enabled = Enable;
        txtoSSN.Enabled = Enable;
        txtoTel.Enabled = Enable;
        txtParentId.Enabled = Enable;

        ASPxRoundPanelJudgmentGroup.Enabled = Enable;
        CustomAspxDevGridView1.Columns[5].Visible = Enable;
    }

    private void SetImpObsAndDesgn(int ProjectIngridientType)
    {
        bool Judgment = false;
        bool Major = false;
        bool Name = false;

        //  ASPxLabelObs.Text = "درصد دستمزد " + CmbType.Text;
        switch (ProjectIngridientType)
        {
            case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                Judgment = true;
                Name = true;
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                Name = true;
                Name = false;
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                Major = true;
                Name = false;
                break;
        }

        //CmbMajor.DataBind();
        //CmbMajor.SelectedIndex = -1;
        ASPxLabelMj.ClientVisible = Major;
        CmbMajor.ClientVisible = Major;
        ASPxRoundPanelJudgmentGroup.ClientVisible = Judgment;
        tbl1.Visible = Judgment;
        //lblName.Visible = txtName.Visible = lblType.Visible = txtType.Visible = Name;
    }

    private void SetNameAndType()
    {
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        if (string.IsNullOrEmpty(ProjectId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        int TypeValue = Convert.ToInt32(CmbType.Value);
        switch (TypeValue)
        {
            case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                TSP.DataManager.TechnicalServices.Project_ImplementerManager ImpManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
                ClearForm();
                SetImpObsAndDesgn((int)TSP.DataManager.TSProjectIngridientType.Implementer);
                ImpManager.FindImpMother(int.Parse(ProjectId));
                if (ImpManager.Count > 0)
                {
                    HDPrjImpObsDsgnId.Value = ImpManager[0]["PrjImpId"].ToString();
                    //txtName.Text = ImpManager[0]["Name"].ToString();
                    //txtType.Text = ImpManager[0]["MemberTypeTitle"].ToString();
                }
                else
                {
                    SetLabelWarning("برای پروژه مورد نظر مجری مادر تعریف نشده است");
                    return;
                }
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
                ClearForm();
                SetImpObsAndDesgn((int)TSP.DataManager.TSProjectIngridientType.Observer);
                ProjectObserversManager.FindObsMother(int.Parse(ProjectId));
                if (ProjectObserversManager.Count > 0)
                {
                    HDPrjImpObsDsgnId.Value = ProjectObserversManager[0]["ProjectObserversId"].ToString();
                    //txtName.Text = ProjectObserversManager[0]["Name"].ToString();
                    //txtType.Text = ProjectObserversManager[0]["MemberTypeTitle"].ToString();
                }
                else
                {
                    SetLabelWarning("برای پروژه مورد نظر نماینده ناظرین تعریف نشده است");
                    return;
                }

                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                TSP.DataManager.TechnicalServices.Project_DesignerManager DesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
                ClearForm();
                SetImpObsAndDesgn((int)TSP.DataManager.TSProjectIngridientType.Designer);
                //TSP.DataManager.TechnicalServices.Designer_PlansManager DesPlanManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
                //DesPlanManager.SelectDesignerByMajor(int.Parse(ProjectId),cm
                //HDPrjImpObsDsgnId.Value = ImpManager[0]["PrjImpId"].ToString();
                //txtName.Text = ImpManager[0]["Name"].ToString();
                break;
        }
    }

    //private void SetDesgnerNameAndType()
    //{
    //    string ProjectId = Utility.DecryptQS(HDProjectId.Value);
    //    if (string.IsNullOrEmpty(ProjectId))
    //    {
    //        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
    //        return;
    //    }

    //    if (CmbType.Value != null)
    //    {
    //        int TypeValue = int.Parse(CmbType.Value.ToString());
    //        switch (TypeValue)
    //        {
    //            case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
    //                break;

    //            case (int)TSP.DataManager.TSProjectIngridientType.Observer:
    //                break;

    //            case (int)TSP.DataManager.TSProjectIngridientType.Designer:
    //                //ClearForm();
    //                SetImpObsAndDesgn((int)TSP.DataManager.TSProjectIngridientType.Designer);

    //                TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
    //                DesignerPlansManager.FindMasterByLastVersion(Convert.ToInt32(ProjectId), GetPlansTypeId(Convert.ToInt32(CmbMajor.Value)));
    //                if (DesignerPlansManager.Count > 0)
    //                {
    //                    HDPrjImpObsDsgnId.Value = DesignerPlansManager[0]["PrjDesignerId"].ToString();
    //                    txtName.Text = DesignerPlansManager[0]["DesignerName"].ToString();
    //                    txtType.Text = DesignerPlansManager[0]["MeType"].ToString();
    //                }
    //                else
    //                {
    //                    SetLabelWarning("برای نوع طراح مورد نظر، نماینده طراحان تعریف نشده است");
    //                    return;
    //                }

    //                break;
    //        }

    //    }
    //    else
    //    {
    //        SetLabelWarning("نوع قرارداد را انتخاب نمایید");
    //        return;
    //    }

    //}

    private int GetPlansTypeId(int DesignerTypeId)
    {
        switch (DesignerTypeId)
        {
            case (int)TSP.DataManager.TSDesignerType.Memari:
                return (int)TSP.DataManager.TSPlansType.Memari;

            case (int)TSP.DataManager.TSDesignerType.Sazeh:
                return (int)TSP.DataManager.TSPlansType.Sazeh;

            case (int)TSP.DataManager.TSDesignerType.Shahrsazi:
                return (int)TSP.DataManager.TSPlansType.Shahrsazi;

            case (int)TSP.DataManager.TSDesignerType.TasisatBargh:
                return (int)TSP.DataManager.TSPlansType.TasisatBargh;

            case (int)TSP.DataManager.TSDesignerType.TasisatMechanic:
                return (int)TSP.DataManager.TSPlansType.TasisatMechanic;

            default:
                return -2;
        }
    }
    #endregion

    #region Fill & Clear
    private void FillForm()
    {
        int ContractId = Convert.ToInt32(Utility.DecryptQS(HDContractId.Value));

        try
        {
            TSP.DataManager.TechnicalServices.ContractManager ContractManager = new TSP.DataManager.TechnicalServices.ContractManager();

            ContractManager.FindByContractId(ContractId);
            if (ContractManager.Count > 0)
            {
                txtAmount.Text = Convert.ToDecimal(ContractManager[0]["Amount"]).ToString("0");
                txtContractDate.Text = ContractManager[0]["ContractDate"].ToString();
                txtDuration.Text = ContractManager[0]["Duration"].ToString();
                txtWage.Text = ContractManager[0]["PercentWage"].ToString();

                ChbMaster.Checked = Convert.ToBoolean(ContractManager[0]["HaveMaster"]);
                if (ChbMaster.Checked)
                {
                    ASPxLabelParent.ClientVisible = true;
                    txtParentId.ClientVisible = true;
                    txtParentId.Text = ContractManager[0]["ParentId"].ToString();
                }
                if (!Utility.IsDBNullOrNullValue(ContractManager[0]["FileUrl"]))
                {
                    HpContract.ClientVisible = true;
                    HpContract.NavigateUrl = ContractManager[0]["FileUrl"].ToString();
                    HDFlpContract["name"] = 1;
                }
                else
                    HDFlpContract["name"] = 0;
                //CmbMajor.DataBind();
                //CmbMajor.SelectedIndex = CmbMajor.Items.IndexOfValue(ContractManager[0][""]);

                if (!Utility.IsDBNullOrNullValue(ContractManager[0]["ProjectIngridientTypeId"]))
                {
                    CmbType.DataBind();
                    CmbType.SelectedIndex = CmbType.Items.IndexOfValue(ContractManager[0]["ProjectIngridientTypeId"].ToString());

                    // FillNameAndType(Convert.ToInt32(ContractManager[0]["ProjectIngridientTypeId"]), Convert.ToInt32(ContractManager[0]["PrjImpObsDsgnId"]));
                }
                if (!Utility.IsDBNullOrNullValue(ContractManager[0]["MjId"]))
                {
                    CmbMajor.DataBind();
                    CmbMajor.SelectedIndex = CmbMajor.Items.IndexOfValue(ContractManager[0]["MjId"].ToString());
                }
            }
            else
            {
                SetLabelWarning("اطلاعات توسط کاربر دیگری تغییر یافته است");
            }
        }
        catch (Exception)
        {
            SetLabelWarning("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }

    //private void FillNameAndType(int ProjectIngridientTypeId, int PrjImpObsDsgnId)
    //{
    //    switch (ProjectIngridientTypeId)
    //    {
    //        case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
    //            TSP.DataManager.TechnicalServices.Project_ImplementerManager ImplementerManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
    //            SetImpObsAndDesgn((int)TSP.DataManager.TSProjectIngridientType.Implementer);
    //            ImplementerManager.FindByPrjImpId(PrjImpObsDsgnId);
    //            if (ImplementerManager.Count > 0)
    //            {
    //                HDPrjImpObsDsgnId.Value = ImplementerManager[0]["PrjImpId"].ToString();
    //                txtName.Text = ImplementerManager[0]["Name"].ToString();
    //                txtType.Text = ImplementerManager[0]["MemberTypeTitle"].ToString();
    //            }
    //            ASPxRoundPanelJudgmentGroup.ClientVisible = true;
    //            FillJudgmentGroup();
    //            break;

    //        case (int)TSP.DataManager.TSProjectIngridientType.Observer:
    //            TSP.DataManager.TechnicalServices.Project_ObserversManager ProjectObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
    //            SetImpObsAndDesgn((int)TSP.DataManager.TSProjectIngridientType.Observer);
    //            ProjectObserversManager.FindByProjectObserversId(PrjImpObsDsgnId);
    //            if (ProjectObserversManager.Count > 0)
    //            {
    //                HDPrjImpObsDsgnId.Value = ProjectObserversManager[0]["ProjectObserversId"].ToString();
    //                txtName.Text = ProjectObserversManager[0]["Name"].ToString();
    //                txtType.Text = ProjectObserversManager[0]["MemberTypeTitle"].ToString();
    //            }
    //            ASPxRoundPanelJudgmentGroup.ClientVisible = false;
    //            break;

    //        case (int)TSP.DataManager.TSProjectIngridientType.Designer:
    //            TSP.DataManager.TechnicalServices.Project_DesignerManager DesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
    //            SetImpObsAndDesgn((int)TSP.DataManager.TSProjectIngridientType.Designer);
    //            DesignerManager.FindByDesignerId(PrjImpObsDsgnId);
    //            if (DesignerManager.Count > 0)
    //            {
    //                HDPrjImpObsDsgnId.Value = DesignerManager[0]["PrjDesignerId"].ToString();
    //                txtName.Text = DesignerManager[0]["DesignerName"].ToString();
    //                txtType.Text = DesignerManager[0]["MeTitle"].ToString();
    //            }
    //            ASPxRoundPanelJudgmentGroup.ClientVisible = false;
    //            break;
    //    }
    //}

    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
    }

    private void ClearForm()
    {
        //CmbType.DataBind();
        //CmbType.SelectedIndex = 0;
        //CmbMajor.DataBind();
        //CmbMajor.SelectedIndex = -1;
        //txtType.Text = "";
        //txtName.Text = "";
        txtDuration.Text = "";
        txtAmount.Text = "";
        txtWage.Text = "";
        txtContractDate.Text = "";
        ChbMaster.Checked = false;
        txtParentId.Text = "";

        HpContract.NavigateUrl = "";
        HpContract.ClientVisible = false;
        HDFlpContract.Set("name", 0);
        Session["AttachContractName"] = null;
        Session["AttachContract"] = null;
    }

    //private void FillGrid(int ContractId)
    //{
    //    TSP.DataManager.TechnicalServices.JudgmentGroupManager manager = (TSP.DataManager.TechnicalServices.JudgmentGroupManager)Session["JudgmentGroupManager"];
    //    manager.FindByContractId(ContractId);
    //    CustomAspxDevGridView1.DataSource = manager.DataTable;
    //    CustomAspxDevGridView1.KeyFieldName = "JudgmentGroupId";
    //    CustomAspxDevGridView1.DataBind();
    //}

    private void FillJudgmentGroup()
    {
        if (Session["JudgmentGroupManager"] == null) return;
        dtJudgmentGroup = (DataTable)Session["JudgmentGroupManager"];
        int ContractId = Convert.ToInt32(Utility.DecryptQS(HDContractId.Value));

        TSP.DataManager.TechnicalServices.JudgmentGroupManager JudgmentGroupManager = new TSP.DataManager.TechnicalServices.JudgmentGroupManager();
        JudgmentGroupManager.FindByContractId(ContractId);
        for (int i = 0; i < JudgmentGroupManager.Count; i++)
        {
            DataRow drJg = dtJudgmentGroup.NewRow();
            drJg["JudgmentGroupId"] = JudgmentGroupManager[i]["JudgmentGroupId"];
            drJg["FirstName"] = JudgmentGroupManager[i]["FirstName"];
            drJg["LastName"] = JudgmentGroupManager[i]["LastName"];
            drJg["FatherName"] = JudgmentGroupManager[i]["FatherName"];
            drJg["IdNo"] = JudgmentGroupManager[i]["IdNo"];
            drJg["SSN"] = JudgmentGroupManager[i]["SSN"];
            drJg["BirthPlace"] = JudgmentGroupManager[i]["BirthPlace"];
            drJg["Tel"] = JudgmentGroupManager[i]["Tel"];
            drJg["MobileNo"] = JudgmentGroupManager[i]["MobileNo"];
            drJg["Address"] = JudgmentGroupManager[i]["Address"];
            drJg["AgentType"] = JudgmentGroupManager[i]["AgentType"];
            drJg["AgentTypeName"] = JudgmentGroupManager[i]["AgentTypeName"];
            drJg["OtherPersonId"] = JudgmentGroupManager[i]["OtherPersonId"];
            dtJudgmentGroup.Rows.Add(drJg);
        }
        dtJudgmentGroup.AcceptChanges();
        Session["JudgmentGroupManager"] = dtJudgmentGroup;
        FillGrid();
    }

    private void FillGrid()
    {
        //string PageMode = Utility.DecryptQS(PgMode.Value);
        //if (string.IsNullOrEmpty(PageMode))
        //{
        //    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

        //    return;
        //}

        //TSP.DataManager.TechnicalServices.JudgmentGroupManager manager = (TSP.DataManager.TechnicalServices.JudgmentGroupManager)Session["JudgmentGroupManager"];
        //if (PageMode != "New")
        //{
        //    string ContractId = Utility.DecryptQS(HDContractId.Value);
        //    if (string.IsNullOrEmpty(ContractId))
        //    {
        //        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

        //        return;
        //    }
        //    manager.FindByContractId(Convert.ToInt32(ContractId));
        //}

        //CustomAspxDevGridView1.DataSource = manager.DataTable;
        //CustomAspxDevGridView1.KeyFieldName = "JudgmentGroupId";
        //CustomAspxDevGridView1.DataBind();
        if (Session["JudgmentGroupManager"] != null)
        {
            CustomAspxDevGridView1.DataSource = (DataTable)(Session["JudgmentGroupManager"]);
            CustomAspxDevGridView1.DataBind();
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

    private void ClearJudgmentGroup()
    {
        txtoAddress.Text = "";
        txtoBirthPlace.Text = "";
        txtoFatherName.Text = "";
        txtoFirstName.Text = "";
        txtoIdNo.Text = "";
        txtoLastName.Text = "";
        txtoMobileNo.Text = "";
        txtoTel.Text = "";
        txtoSSN.Text = "";
        cmbAgentType.DataBind();
        cmbAgentType.SelectedIndex = -1;
    }
    #endregion

    #region Insert&Update
    private void Insert()
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.ContractManager ContractManager = new TSP.DataManager.TechnicalServices.ContractManager();
        TSP.DataManager.TechnicalServices.JudgmentGroupManager JudgmentManager = new TSP.DataManager.TechnicalServices.JudgmentGroupManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();

        trans.Add(ContractManager);
        trans.Add(JudgmentManager);
        trans.Add(AttachManager);
        trans.Add(OtherPersonManager);

        try
        {
            //if (!CheckIfExsist(ContractManager))
            //    return;

            trans.BeginSave();

            InsertContract(ContractManager);

            if (Session["AttachContract"] == null || Session["AttachContractName"] == null)
            {
                trans.CancelSave();
                SetLabelWarning("فایل قرارداد را انتخاب نمایید");
                return;
            }

            if (CmbType.Value.ToString() == ((int)TSP.DataManager.TSProjectIngridientType.Implementer).ToString())
            {
                InsertOtherPersonAndJudgmentGroup(OtherPersonManager, JudgmentManager);
            }

            SetLabelWarning("ذخیره انجام شد");
            trans.EndSave();

            PgMode.Value = Utility.EncryptQS("Edit");
            SetEditModeKeys();
            Session["AttachContract"] = null;
            Session["AttachContractName"] = null;
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }

        SetImpObsAndDesgn(Convert.ToInt32(CmbType.Value));
    }

    private void InsertContract(TSP.DataManager.TechnicalServices.ContractManager ContractManager)
    {
        DataRow ContracRow = ContractManager.NewRow();
        if (!Utility.IsDBNullOrNullValue(HDPrjImpObsDsgnId.Value))
            ContracRow["PrjImpObsDsgnId"] = HDPrjImpObsDsgnId.Value;
        ContracRow["ProjectIngridientTypeId"] = int.Parse(CmbType.Value.ToString());
        ContracRow["PrjReId"] = Utility.DecryptQS(RequestId.Value);
        if (CmbMajor.Value != null)
            ContracRow["MjId"] = CmbMajor.Value;
        else
            ContracRow["MjId"] = DBNull.Value;
        ContracRow["Duration"] = txtDuration.Text;
        ContracRow["Amount"] = txtAmount.Text;
        ContracRow["PercentWage"] = txtWage.Text;
        ContracRow["ContractDate"] = txtContractDate.Text;
        ContracRow["HaveMaster"] = ChbMaster.Checked;
        if (ChbMaster.Checked)
            ContracRow["ParentId"] = txtParentId.Text;
        else
            ContracRow["ParentId"] = DBNull.Value;
        if (Session["AttachContract"] != null && Session["AttachContractName"] != null)
        {
            ContracRow["FileUrl"] = "~/Image/TechnicalServices/Contract/" + Path.GetFileName(Session["AttachContract"].ToString());
        }
        ContracRow["InActive"] = 0;
        ContracRow["CreateDate"] = Utility.GetDateOfToday();
        ContracRow["UserId"] = Utility.GetCurrentUser_UserId();
        ContracRow["ModifiedDate"] = DateTime.Now;

        ContractManager.AddRow(ContracRow);
        ContractManager.Save();

        ContractManager.DataTable.AcceptChanges();
        string ContractId = ContractManager[0]["ContractId"].ToString();
        HDContractId.Value = Utility.EncryptQS(ContractId);
    }

    private void InsertOtherPersonAndJudgmentGroup(TSP.DataManager.OtherPersonManager OtherPersonManager, TSP.DataManager.TechnicalServices.JudgmentGroupManager JudgmentGroupManager)
    {
        int ContractId = Convert.ToInt32(Utility.DecryptQS(HDContractId.Value));
        if (Session["JudgmentGroupManager"] == null) return;
        dtJudgmentGroup = (DataTable)Session["JudgmentGroupManager"];

        for (int i = 0; i < dtJudgmentGroup.Rows.Count; i++)
        {
            //----------other person----------------
            DataRow drOth = OtherPersonManager.NewRow();
            drOth["FirstName"] = dtJudgmentGroup.Rows[i]["FirstName"];
            drOth["LastName"] = dtJudgmentGroup.Rows[i]["LastName"];
            drOth["FatherName"] = dtJudgmentGroup.Rows[i]["FatherName"];
            drOth["IdNo"] = dtJudgmentGroup.Rows[i]["IdNo"];
            drOth["SSN"] = dtJudgmentGroup.Rows[i]["SSN"];
            drOth["BirthPlace"] = dtJudgmentGroup.Rows[i]["BirthPlace"];
            drOth["Tel"] = dtJudgmentGroup.Rows[i]["Tel"];
            drOth["MobileNo"] = dtJudgmentGroup.Rows[i]["MobileNo"];
            drOth["Address"] = dtJudgmentGroup.Rows[i]["Address"];
            drOth["UserId"] = Utility.GetCurrentUser_UserId();
            drOth["ModifiedDate"] = DateTime.Now;
            OtherPersonManager.AddRow(drOth);
            OtherPersonManager.Save();
            OtherPersonManager.DataTable.AcceptChanges();

            //----------JudgmentGroup-----------------
            DataRow rowJG = JudgmentGroupManager.NewRow();
            rowJG["ContractId"] = ContractId;
            rowJG["OtherPersonId"] = OtherPersonManager[i]["OtpId"];
            rowJG["AgentType"] = dtJudgmentGroup.Rows[i]["AgentType"];
            rowJG["UserId"] = Utility.GetCurrentUser_UserId();
            rowJG["ModifiedDate"] = DateTime.Now;
            JudgmentGroupManager.AddRow(rowJG);
            if (JudgmentGroupManager.Save() > 0)
            {
                JudgmentGroupManager.DataTable.AcceptChanges();
                dtJudgmentGroup.Rows[i].BeginEdit();
                dtJudgmentGroup.Rows[i]["JudgmentGroupId"] = JudgmentGroupManager[JudgmentGroupManager.Count - 1]["JudgmentGroupId"].ToString();
                dtJudgmentGroup.Rows[i].EndEdit();
            }
        }
    }

    private bool CheckIfExsist(TSP.DataManager.TechnicalServices.ContractManager ContractManager)
    {
        if (!ChbMaster.Checked)
        {
            ContractManager.FindByPrjImpObsDsgnId(Convert.ToInt32(HDPrjImpObsDsgnId.Value), Convert.ToInt32(CmbType.Value));
            if (ContractManager.Count > 0)
            {
                SetLabelWarning("برای شخص مورد نظر قرارداد ثبت شده است");
                return false;
            }
        }
        return true;
    }

    private void RowInserting()
    {
        if (Session["JudgmentGroupManager"] == null) return;
        dtJudgmentGroup = (DataTable)Session["JudgmentGroupManager"];
        if (!CheckType(dtJudgmentGroup))
            return;

        DataRow rowJg = dtJudgmentGroup.NewRow();
        rowJg["AgentType"] = cmbAgentType.Value.ToString();
        rowJg["AgentTypeName"] = cmbAgentType.SelectedItem.Text;
        rowJg["FirstName"] = txtoFirstName.Text;
        rowJg["LastName"] = txtoLastName.Text;
        rowJg["FatherName"] = txtoFatherName.Text;
        rowJg["SSN"] = txtoSSN.Text;
        rowJg["IdNo"] = txtoIdNo.Text;
        rowJg["BirthPlace"] = txtoBirthPlace.Text;
        rowJg["Tel"] = txtoTel.Text;
        rowJg["MobileNo"] = txtoMobileNo.Text;
        rowJg["Address"] = txtoAddress.Text;
        dtJudgmentGroup.Rows.Add(rowJg);
        Session["JudgmentGroupManager"] = dtJudgmentGroup;
        CustomAspxDevGridView1.CancelEdit();
        CustomAspxDevGridView1.DataSource = dtJudgmentGroup;
        CustomAspxDevGridView1.DataBind();
        ClearJudgmentGroup();
    }

    /******************************************************************** Update *****************************************************************/
    private void Update()
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();

        TSP.DataManager.TechnicalServices.ContractManager ContractManager = new TSP.DataManager.TechnicalServices.ContractManager();
        //  TSP.DataManager.TechnicalServices.JudgmentGroupManager JudgmentManager = (TSP.DataManager.TechnicalServices.JudgmentGroupManager)Session["JudgmentGroupManager"];
        TSP.DataManager.TechnicalServices.JudgmentGroupManager JudgmentManager = new TSP.DataManager.TechnicalServices.JudgmentGroupManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();
        TSP.DataManager.OtherPersonManager OtherPersonManager = new TSP.DataManager.OtherPersonManager();

        trans.Add(ContractManager);
        trans.Add(JudgmentManager);
        trans.Add(AttachManager);
        trans.Add(OtherPersonManager);

        try
        {
            trans.BeginSave();

            UpdateContract(ContractManager);

            if (CmbType.Value.ToString() == ((int)TSP.DataManager.TSProjectIngridientType.Implementer).ToString())
                if (!UpdateOtherPersonAndJudgmentGroup(OtherPersonManager, JudgmentManager))
                {
                    trans.CancelSave();
                    SetLabelWarning("خطایی در ذخیره انجام گرفته است");
                    return;
                }

            SetLabelWarning("ذخیره انجام شد");
            trans.EndSave();

            Session["AttachContract"] = null;
            Session["AttachContractName"] = null;

        }
        catch (Exception Err)
        {
            SetError(Err);
            trans.CancelSave();
        }

        SetImpObsAndDesgn(Convert.ToInt32(CmbType.Value));
    }

    private void UpdateContract(TSP.DataManager.TechnicalServices.ContractManager ContractManager)
    {
        int ContractId = Convert.ToInt32(Utility.DecryptQS(HDContractId.Value));

        ContractManager.FindByContractId(ContractId);
        if (ContractManager.Count > 0)
        {
            ContractManager[0]["PrjReId"] = Utility.DecryptQS(RequestId.Value);
            ContractManager[0]["Amount"] = txtAmount.Text;
            ContractManager[0]["ContractDate"] = txtContractDate.Text;
            ContractManager[0]["Duration"] = txtDuration.Text;
            ContractManager[0]["HaveMaster"] = ChbMaster.Checked;
            if (ChbMaster.Checked)
                ContractManager[0]["ParentId"] = txtParentId.Text;
            else
                ContractManager[0]["ParentId"] = DBNull.Value;
            if (Session["AttachContract"] != null)
                ContractManager[0]["FileUrl"] = "~/Image/TechnicalServices/Contract/" + Path.GetFileName(Session["AttachContract"].ToString());
            ContractManager[0]["PercentWage"] = txtWage.Text;
            ContractManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            ContractManager[0]["ModifiedDate"] = DateTime.Now;
        }
        ContractManager.Save();

        ContractManager.DataTable.AcceptChanges();
    }

    private bool UpdateOtherPersonAndJudgmentGroup(TSP.DataManager.OtherPersonManager OtherPersonManager, TSP.DataManager.TechnicalServices.JudgmentGroupManager JudgmentGroupManager)
    {
        if (Session["JudgmentGroupManager"] == null) return false;
        dtJudgmentGroup = (DataTable)Session["JudgmentGroupManager"];
        int ContractId = Convert.ToInt32(Utility.DecryptQS(HDContractId.Value));

        DataRow[] DelRows = dtJudgmentGroup.Select(null, null, DataViewRowState.Deleted);
        DataRow[] InsertRows = dtJudgmentGroup.Select(null, null, DataViewRowState.Added);
        if (DelRows != null)
            for (int i = 0; i < DelRows.Length; i++)
            {
                JudgmentGroupManager.FindByJudgmentGroupId(int.Parse(DelRows[i]["JudgmentGroupId", DataRowVersion.Original].ToString()));
                if (JudgmentGroupManager.Count == 1)
                {
                    JudgmentGroupManager[0].Delete();
                    JudgmentGroupManager.Save();
                    JudgmentGroupManager.DataTable.AcceptChanges();
                }

                OtherPersonManager.FindByCode(int.Parse(DelRows[i]["OtherPersonId", DataRowVersion.Original].ToString()));
                if (OtherPersonManager.Count == 1)
                {
                    OtherPersonManager[0].Delete();
                    OtherPersonManager.Save();
                    OtherPersonManager.DataTable.AcceptChanges();
                }
            }

        if (InsertRows != null)
        {
            for (int i = 0; i < InsertRows.Length; i++)
            {
                DataRow drOth = OtherPersonManager.NewRow();
                drOth.BeginEdit();
                drOth["FirstName"] = InsertRows[i]["FirstName"];
                drOth["LastName"] = InsertRows[i]["LastName"];
                drOth["FatherName"] = InsertRows[i]["FatherName"];
                drOth["IdNo"] = InsertRows[i]["IdNo"];
                drOth["SSN"] = InsertRows[i]["SSN"];
                drOth["BirthPlace"] = InsertRows[i]["BirthPlace"];
                drOth["Tel"] = InsertRows[i]["Tel"];
                drOth["MobileNo"] = InsertRows[i]["MobileNo"];
                drOth["Address"] = InsertRows[i]["Address"];
                drOth["UserId"] = Utility.GetCurrentUser_UserId();
                drOth["ModifiedDate"] = DateTime.Now;
                drOth.EndEdit();
                OtherPersonManager.AddRow(drOth);
                OtherPersonManager.Save();
                OtherPersonManager.DataTable.AcceptChanges();

                //----------JudgmentGroup-----------------
                DataRow rowJG = JudgmentGroupManager.NewRow();
                rowJG["ContractId"] = ContractId;
                rowJG["OtherPersonId"] = OtherPersonManager[i]["OtpId"];
                rowJG["AgentType"] = InsertRows[i]["AgentType"];
                rowJG["UserId"] = Utility.GetCurrentUser_UserId();
                rowJG["ModifiedDate"] = DateTime.Now;
                JudgmentGroupManager.AddRow(rowJG);
                if (JudgmentGroupManager.Save() > 0)
                {
                    JudgmentGroupManager.DataTable.AcceptChanges();
                    dtJudgmentGroup.DefaultView.RowFilter = "Id = " + InsertRows[i]["Id"].ToString();
                    dtJudgmentGroup.DefaultView[0].BeginEdit();
                    dtJudgmentGroup.DefaultView[0]["JudgmentGroupId"] = JudgmentGroupManager[JudgmentGroupManager.Count - 1]["JudgmentGroupId"].ToString();
                    dtJudgmentGroup.DefaultView[0].EndEdit();
                }
            }
        }

        return true;
    }

    //private void UpdateOtherPerson(TSP.DataManager.OtherPersonManager OtherPersonManager, DataRow JudgmentGroupRow)
    //{
    //    OtherPersonManager.FindByCode(Convert.ToInt32(JudgmentGroupRow["OtherPersonId"]));
    //    if (OtherPersonManager.Count == 1)
    //    {
    //        OtherPersonManager[0].BeginEdit();
    //        OtherPersonManager[0]["FirstName"] = JudgmentGroupRow["FirstName"];
    //        OtherPersonManager[0]["LastName"] = JudgmentGroupRow["LastName"];
    //        OtherPersonManager[0]["FatherName"] = JudgmentGroupRow["FatherName"];
    //        OtherPersonManager[0]["IdNo"] = JudgmentGroupRow["IdNo"];
    //        OtherPersonManager[0]["SSN"] = JudgmentGroupRow["SSN"];
    //        OtherPersonManager[0]["BirthPlace"] = JudgmentGroupRow["BirthPlace"];
    //        OtherPersonManager[0]["Tel"] = JudgmentGroupRow["Tel"];
    //        OtherPersonManager[0]["MobileNo"] = JudgmentGroupRow["MobileNo"];
    //        OtherPersonManager[0]["Address"] = JudgmentGroupRow["Address"];
    //        OtherPersonManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //        OtherPersonManager[0]["ModifiedDate"] = DateTime.Now;
    //        OtherPersonManager[0].EndEdit();
    //        OtherPersonManager.Save();
    //    }
    //}

    //private void InsertOtherPersonAndJudgmentGroupInUpdate(TSP.DataManager.OtherPersonManager OtherPersonManager, DataRow JudgmentGroupRow)
    //{
    //    DataRow drOth = OtherPersonManager.NewRow();

    //    drOth.BeginEdit();
    //    drOth["FirstName"] = JudgmentGroupRow["FirstName"];
    //    drOth["LastName"] = JudgmentGroupRow["LastName"];
    //    drOth["FatherName"] = JudgmentGroupRow["FatherName"];
    //    drOth["IdNo"] = JudgmentGroupRow["IdNo"];
    //    drOth["SSN"] = JudgmentGroupRow["SSN"];
    //    drOth["BirthPlace"] = JudgmentGroupRow["BirthPlace"];
    //    drOth["Tel"] = JudgmentGroupRow["Tel"];
    //    drOth["MobileNo"] = JudgmentGroupRow["MobileNo"];
    //    drOth["Address"] = JudgmentGroupRow["Address"];
    //    drOth["UserId"] = Utility.GetCurrentUser_UserId();
    //    drOth["ModifiedDate"] = DateTime.Now;
    //    drOth.EndEdit();

    //    OtherPersonManager.AddRow(drOth);
    //    OtherPersonManager.Save();

    //    int ContractId = Convert.ToInt32(Utility.DecryptQS(HDContractId.Value));
    //    int OtherPersonId = Convert.ToInt32(OtherPersonManager[OtherPersonManager.Count - 1]["OtpId"]);

    //    JudgmentGroupRow.BeginEdit();
    //    JudgmentGroupRow["ContractId"] = ContractId;
    //    JudgmentGroupRow["OtherPersonId"] = OtherPersonId;
    //    JudgmentGroupRow.EndEdit();
    //}

    //private void DeleteOtherPersonInUpdate(TSP.DataManager.OtherPersonManager OtherPersonManager, DataRow JudgmentGroupRow)
    //{
    //    OtherPersonManager.FindByCode(Convert.ToInt32(JudgmentGroupRow["OtherPersonId"]));
    //    if (OtherPersonManager.Count == 1)
    //    {
    //        OtherPersonManager[0].Delete();
    //        OtherPersonManager.Save();
    //    }
    //}
    #endregion

    #region Checks
    private void CheckParentId()
    {
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);

        ASPxLabelParent.ClientVisible = true;
        txtParentId.ClientVisible = true;

        if (!string.IsNullOrEmpty(ProjectId))
        {
            bool IsTrue = false;

            TSP.DataManager.TechnicalServices.ContractManager ContractManager = new TSP.DataManager.TechnicalServices.ContractManager();
            ContractManager.FindByProjectId(Convert.ToInt32(ProjectId));
            if (ContractManager.Count > 0)
            {
                for (int i = 0; i < ContractManager.Count; i++)
                    if (txtParentId.Text == ContractManager[i]["ContractId"].ToString())
                        return;

                txtParentId.Text = "";
                SetLabelWarning("کد قرارداد وارد شده مربوط به پروژه مورد نظر نمی باشد.مجدداً وارد نمایید");
            }
            else
            {
                SetLabelWarning("برای پروژه مورد نظر قراردادی وارد نشده است");
            }

        }
        else
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    private void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ContractManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        string PageMode = Utility.DecryptQS(PgMode.Value);
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        if (BtnNew.Enabled == true)
        {
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
        }

        if (btnEdit.Enabled == true)
        {
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
        }

        if (PageMode == "New" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;
        }
        if (PageMode == "Edit" && btnSave.Enabled == true)
        {
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    //private TSP.DataManager.TechnicalServices.JudgmentGroupManager CreateJudgmentGroupManager()
    //{
    //    TSP.DataManager.TechnicalServices.JudgmentGroupManager manager = new TSP.DataManager.TechnicalServices.JudgmentGroupManager();
    //    return manager;
    //}

    private bool CheckType(DataTable dtJudgmentGroup)
    {
        for (int i = 0; i < dtJudgmentGroup.Rows.Count; i++)
        {
            if (dtJudgmentGroup.Rows[i].RowState != DataRowState.Deleted)
            {
                if (dtJudgmentGroup.Rows[i]["AgentType"].ToString() == cmbAgentType.Value.ToString())
                {
                    SetLabelWarning("برای نوع انتخاب شده نماینده وارد شده است");
                    return false;
                }
            }
        }
        return true;
    }

    private string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                Session["AttachContractName"] = Path.GetFileName(uploadedFile.PostedFile.FileName);

                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/TechnicalServices/Contract/") + ret) == true);
            string tempFileName = MapPath("~/Image/TechnicalServices/Contract/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["AttachContract"] = tempFileName;

        }
        return ret;
    }

    public DataTable MakeTblJudgmentGroup()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Id");
        dt.Columns["Id"].AutoIncrement = true;
        dt.Columns["Id"].AutoIncrementSeed = 1;
        dt.Constraints.Add("Id", dt.Columns["Id"], true);
        dt.Columns.Add("JudgmentGroupId");
        dt.Columns.Add("AgentType");
        dt.Columns.Add("AgentTypeName");
        dt.Columns.Add("FirstName");
        dt.Columns.Add("LastName");
        dt.Columns.Add("FatherName");
        dt.Columns.Add("SSN");
        dt.Columns.Add("IdNo");
        dt.Columns.Add("BirthPlace");
        dt.Columns.Add("Tel");
        dt.Columns.Add("MobileNo");
        dt.Columns.Add("Address");
        dt.Columns.Add("OtherPersonId");
        return dt;
    }
    #endregion

    #region WorkFlow Methods

    private void CheckWorkFlowPermission()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        if (string.IsNullOrEmpty(PageMode.ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
        CheckWorkFlowPermissionForEdit(PageMode);
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        TSP.DataManager.WFPermission PerImp = CheckWorkFlowPermissionForImp(PageMode);
        TSP.DataManager.WFPermission PerObs = CheckWorkFlowPermissionForObs(PageMode);
        TSP.DataManager.WFPermission PerDes = CheckWorkFlowPermissionForDesign(PageMode);

        BtnNew.Enabled = PerImp.BtnNew || PerObs.BtnNew || PerDes.BtnNew;
        BtnNew2.Enabled = PerImp.BtnNew || PerObs.BtnNew || PerDes.BtnNew;
        btnEdit.Enabled = PerImp.BtnEdit || PerObs.BtnEdit || PerDes.BtnEdit;
        btnEdit2.Enabled = PerImp.BtnEdit || PerObs.BtnEdit || PerDes.BtnEdit;
        btnSave.Enabled = PerImp.BtnSave || PerObs.BtnSave || PerDes.BtnSave;
        btnSave2.Enabled = PerImp.BtnSave || PerObs.BtnSave || PerDes.BtnSave;

        if (PageMode == "New")
            SetCmbTypeByWorkFlowPermission(PerImp, PerObs, PerDes);

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    private TSP.DataManager.WFPermission CheckWorkFlowPermissionForImp(string PageMode)
    {
        //****TableId
        string PrjReId = Utility.DecryptQS(RequestId.Value);
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int ImpTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementerOfProject;

        TSP.DataManager.WFPermission WFImpPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(ImpTaskCode, WFCode, Convert.ToInt32(PrjReId), Utility.GetCurrentUser_UserId(), PageMode);

        return WFImpPer;
    }

    private TSP.DataManager.WFPermission CheckWorkFlowPermissionForObs(string PageMode)
    {
        //****TableId
        string PrjReId = Utility.DecryptQS(RequestId.Value);

        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;
        int ObsTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject;

        TSP.DataManager.WFPermission WFObsPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(ObsTaskCode, WFCode, Convert.ToInt32(PrjReId), Utility.GetCurrentUser_UserId(), PageMode);

        return WFObsPer;
    }

    private TSP.DataManager.WFPermission CheckWorkFlowPermissionForDesign(string PageMode)
    {
        //****TableId
        string PrjReId = Utility.DecryptQS(RequestId.Value);
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        //*******Editing Task Code
        int StructurePlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject;
        int ElectricalInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject;
        int MechanicInsPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject;
        int ArchitecturalPlanTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject;

        TSP.DataManager.WFPermission PerAtchitecturalPlanTask = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(ArchitecturalPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission PerStructurePlanTask = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(StructurePlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission PerElectricalInsPlanTask = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(ElectricalInsPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission PerMechanicInsPlanTask = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(MechanicInsPlanTaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId(), PageMode);

        TSP.DataManager.WFPermission WFPer = new TSP.DataManager.WFPermission();
        WFPer.BtnEdit = PerAtchitecturalPlanTask.BtnEdit || PerStructurePlanTask.BtnEdit || PerElectricalInsPlanTask.BtnEdit || PerMechanicInsPlanTask.BtnEdit;
        WFPer.BtnSave = PerAtchitecturalPlanTask.BtnSave || PerStructurePlanTask.BtnSave || PerElectricalInsPlanTask.BtnSave || PerMechanicInsPlanTask.BtnSave;
        WFPer.BtnNew = PerAtchitecturalPlanTask.BtnNew || PerStructurePlanTask.BtnNew || PerElectricalInsPlanTask.BtnNew || PerMechanicInsPlanTask.BtnNew;
        WFPer.BtnInactive = PerAtchitecturalPlanTask.BtnInactive || PerStructurePlanTask.BtnInactive || PerElectricalInsPlanTask.BtnInactive || PerMechanicInsPlanTask.BtnInactive;

        return WFPer;
    }

    private void SetCmbTypeByWorkFlowPermission(TSP.DataManager.WFPermission PerImp, TSP.DataManager.WFPermission PerObs, TSP.DataManager.WFPermission PerDes)
    {
        bool Imp = PerImp.BtnNew || PerImp.BtnSave || PerImp.BtnEdit;
        bool Obs = PerObs.BtnNew || PerObs.BtnSave || PerObs.BtnEdit;
        bool Des = PerDes.BtnNew || PerDes.BtnSave || PerDes.BtnEdit;

        if (Imp)
        {
            CmbType.Value = ((int)TSP.DataManager.TSProjectIngridientType.Implementer).ToString();
            SetImpObsAndDesgn((int)TSP.DataManager.TSProjectIngridientType.Implementer);
            SetNameAndType();
        }

        if (Obs)
        {
            CmbType.Value = ((int)TSP.DataManager.TSProjectIngridientType.Observer).ToString();
            SetImpObsAndDesgn((int)TSP.DataManager.TSProjectIngridientType.Observer);
            SetNameAndType();
        }

        if (Des)
        {
            CmbType.Value = ((int)TSP.DataManager.TSProjectIngridientType.Designer).ToString();
            SetImpObsAndDesgn((int)TSP.DataManager.TSProjectIngridientType.Designer);
        }
    }

    private void SetPermissionByCmbTypeAndWorkFlow(TSP.DataManager.WFPermission PerImp, TSP.DataManager.WFPermission PerObs, TSP.DataManager.WFPermission PerArtchitecturalPlan, TSP.DataManager.WFPermission PerOtherPlans, TSP.DataManager.WFPermission PerPlanChanginging)
    {
        bool Imp = PerImp.BtnNew || PerImp.BtnSave || PerImp.BtnEdit;
        bool Obs = PerObs.BtnNew || PerObs.BtnSave || PerObs.BtnEdit;
        bool ArtchitecturalPlan = PerArtchitecturalPlan.BtnNew || PerArtchitecturalPlan.BtnSave || PerArtchitecturalPlan.BtnEdit;
        bool OtherPlans = PerOtherPlans.BtnNew || PerOtherPlans.BtnSave || PerOtherPlans.BtnEdit;
        bool PlanChanginging = PerPlanChanginging.BtnNew || PerPlanChanginging.BtnSave || PerPlanChanginging.BtnEdit;

        int ProjectIngridientType = Convert.ToInt32(CmbType.Value);

        switch (ProjectIngridientType)
        {
            case (int)TSP.DataManager.TSProjectIngridientType.Implementer:
                if (!Imp)
                    SetBtnSaveAndEditEnable(false);
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Observer:
                if (!Obs)
                    SetBtnSaveAndEditEnable(false);
                break;

            case (int)TSP.DataManager.TSProjectIngridientType.Designer:
                if (!ArtchitecturalPlan && !OtherPlans && !PlanChanginging)
                    SetBtnSaveAndEditEnable(false);
                break;
        }
    }

    private void SetBtnSaveAndEditEnable(bool Enable)
    {
        btnEdit.Enabled = Enable;
        btnEdit2.Enabled = Enable;
        btnSave.Enabled = Enable;
        btnSave2.Enabled = Enable;
    }

    private int GetPlansId(int ProjectId, int PlansTypeId)
    {
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        PlansManager.SelectMaxVersion(ProjectId, 0, PlansTypeId);
        if (PlansManager.Count > 0)
            return Convert.ToInt32(PlansManager[0]["PlansId"]);
        return -1;
    }

    private void CheckWorkFlowPermissionForBtnNew()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        if (PageMode == "New")
        {
            TSP.DataManager.WFPermission PerImp = CheckWorkFlowPermissionForImp(PageMode);
            TSP.DataManager.WFPermission PerObs = CheckWorkFlowPermissionForObs(PageMode);
            TSP.DataManager.WFPermission PerDes = CheckWorkFlowPermissionForDesign(PageMode);

            SetCmbTypeByWorkFlowPermission(PerImp, PerObs, PerDes);
            SetNameAndType();
        }
    }

    //private void CheckWorkFlowPermissionForCmbMajor_SelectedIndexChanged()
    //{
    //    string PageMode = Utility.DecryptQS(PgMode.Value);

    //   TSP.DataManager.WFPermission PerOtherPlans = CheckWorkFlowPermissionForOtherPlans(PageMode);

    //    bool OtherPlans = PerOtherPlans.BtnNew || PerOtherPlans.BtnSave || PerOtherPlans.BtnEdit;

    //    if (OtherPlans && Convert.ToInt32(CmbMajor.Value) == (int)TSP.DataManager.TSDesignerType.Memari)
    //    {
    //        CmbMajor.SelectedIndex = -1;
    //        HDPrjImpObsDsgnId.Value = "-2";
    //        txtName.Text = "";
    //        txtType.Text = "";
    //        SetLabelWarning("در این مرحله از گردش کار نمی توانید برای این نوع طراح قرارداد ثبت کنید.");
    //    }
    //}
    #endregion
}
