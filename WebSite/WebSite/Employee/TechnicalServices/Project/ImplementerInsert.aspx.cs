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

public partial class Employee_TechnicalServices_Project_ImplementerInsert : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    #region Event
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        txtFileNo.Attributes["onkeyup"] = "return ltr_override(event)";
        txtFileDate.Attributes["onkeyup"] = "return ltr_override(event)";

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
            Session["AttachCommitName"] = null;
            Session["AttachCommit"] = null;

            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Project.aspx");
            }

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.Project_ImplementerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            if (Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName] != null)
            {
                //String QueryValue = Utility.DecryptQS(Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName]);
                String QueryValue = Request.QueryString[TSP.DataManager.Automation.AttachPageToLetter.QueryName];
                if (TSP.DataManager.Automation.AttachPageToLetter.CheckPageParameterValue(QueryValue) == false)
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            }

            if ((string.IsNullOrEmpty(Request.QueryString["PageMode2"])) || (string.IsNullOrEmpty(Request.QueryString["PrjImpId"])) || (!per.CanView && Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode2"].ToString())) != "New"))
            {
                Response.Redirect("Implementer.aspx?ProjectId=" + Server.HtmlDecode(Request.QueryString["ProjectId"]) + "&PageMode=" + Request.QueryString["PageMode"] + "&PrjReId=" + Server.HtmlDecode(Request.QueryString["PrjReId"]));
                return;
            }

            //Session["AccountingManager"] = null;
            //Session["AccountingManager"] = CreateAccountingManager();

            SetKeys();

            //TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
            //ProjectManager.FindByProjectId(int.Parse(ProjectId));
            //if (ProjectManager.Count > 0)
            //{
            //    if (int.Parse(ProjectManager[0]["CitId"].ToString()) == Utility.GetCurrentCitId())
            //    {
            //        CmbType.Items.RemoveAt(2);                    
            //    }
            //}

            //FillAccountingLabels(int.Parse(ProjectId));
            //FillGrid();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnLetter"] = btnShowPpcAttachPageToAutomationLetter.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnLetter"] != null)
            this.btnShowPpcAttachPageToAutomationLetter.Enabled = this.btnShowPpcAttachPageToAutomationLetter2.Enabled = (bool)this.ViewState["BtnLetter"];

    }

    #region btn Click
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        HDImpId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        HDFlpCommit["name"] = 0;
        SetNewMode();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("Edit");
        SetEditMode();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string PrjImpId = Utility.DecryptQS(HDImpId.Value);
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);

        if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(ProjectId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        switch (PageMode)
        {
            case "New":
                Insert(int.Parse(ProjectId));
                break;
            case "Edit":
                if (string.IsNullOrEmpty(PrjImpId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                Edit(int.Parse(PrjImpId), int.Parse(ProjectId));
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect("Implementer.aspx?ProjectId=" + HDProjectId.Value
            + "&PageMode=" + Request.QueryString["PageMode"]
            + "&PrjReId=" + RequestId.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
    }
    #endregion

    protected void flpCommit_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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

    protected void MenuImp_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "ProjectId=" + HDProjectId.Value + "&PrjReId=" + RequestId.Value + "&PageMode=" + Request.QueryString["PageMode"]
            + "&PrjImpId=" + HDImpId.Value + "&PageMode2=" + PgMode.Value + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("JobHistory.aspx?" + QS);
                break;

            case "Entezami":
                break;

            case "Control":
                Response.Redirect("ProjectQC.aspx?" + QS + "&MemberTypeId=" + Utility.EncryptQS(CmbType.Value.ToString()));
                break;

            case "Imp":
                Response.Redirect("ImplementerInsert.aspx?" + QS);
                break;

        }

    }

    protected void txtID_TextChanged(object sender, EventArgs e)
    {
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();

        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OtherPersonManager OthpManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocOffMemberAcceptedGradeManager MemberAcceptedGradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();

        int MeOfOthId = -1;
        if (string.IsNullOrEmpty(txtID.Text))
        {
            SetLabelWarning("کد عضویت را وارد نمایید");
            return;
        }
        MeOfOthId = int.Parse(txtID.Text);
        if (CmbType.Value == null)
        {
            SetLabelWarning("نوع مجری را انتخاب نمایید");
            return;
        }
        string TypeValue = CmbType.Value.ToString();
        switch (TypeValue)
        {
            case "1"://Member
                #region Member
                MeManager.FindByCode(MeOfOthId);
                if (MeManager.Count != 1)
                {
                    ClearForm();
                    SetMember();
                    SetLabelWarning("چنین کد عضویتی وجود ندارد.مجدداً وارد نمایید");
                    return;
                }
                if (Convert.ToInt32(MeManager[0]["MrsId"]) != 1)
                {
                    ClearForm();
                    SetMember();
                    SetLabelWarning("وضعیت عضو مورد عضویت نظر تایید شده نمی باشد");
                    return;
                }
                if (Convert.ToBoolean(MeManager[0]["InActive"]))
                {
                    ClearForm();
                    SetMember();
                    SetLabelWarning("عضو مورد نظر غیر فعال می باشد");
                    return;
                }
                txtFatherName.Text = MeManager[0]["FatherName"].ToString();
                txtFirstName.Text = MeManager[0]["FirstName"].ToString();
                txtLastName.Text = MeManager[0]["LastName"].ToString();
                txtSSN.Text = MeManager[0]["SSN"].ToString();
                txtFileDate.Text = MeManager[0]["FileDate"].ToString();
                txtFileNo.Text = MeManager[0]["FileNo"].ToString();
                txtImpId.Text = MeManager[0]["ImpGrdName"].ToString();
                txtArchitectorCode.Text = MeManager[0]["ArchitectorCode"].ToString();
                FillProjectIngridientCapacityInfo(TSP.DataManager.TSMemberType.Member, MeOfOthId);
                SetMember();
                #endregion
                break;
            case "2"://Office
                #region Office
                OffManager.FindByCode(MeOfOthId);
                if (OffManager.Count == 1)
                {
                    if (Convert.ToInt32(OffManager[0]["MrsId"]) != 1)
                    {
                        ClearForm();
                        SetOffice();
                        SetLabelWarning("وضعیت عضو مورد نظر تایید شده نمی باشد");
                        return;
                    }
                    txtOrgName.Text = OffManager[0]["OfName"].ToString();
                    txtManager.Text = OffManager[0]["MName"].ToString();
                    txtFileDate.Text = OffManager[0]["FileDate"].ToString();
                    txtFileNo.Text = OffManager[0]["FileNo"].ToString();
                    txtImpId.Text = OfficeMemberManager.FindOffImpGradeName(MeOfOthId);
                    FillProjectIngridientCapacityInfo(TSP.DataManager.TSMemberType.Office, MeOfOthId);
                    SetOffice();
                }
                else
                {
                    ClearForm();
                    SetOffice();
                    SetLabelWarning("چنین کد عضویتی وجود ندارد.مجدداً وارد نمایید");
                    return;
                }
                #endregion
                break;
            case "3"://Kardan
                #region Kardan
                OthpManager.FindKardanAndMemarByOtpCode(MeOfOthId.ToString(), (int)TSP.DataManager.OtherPersonType.Kardan);
                if (OthpManager.Count == 1)
                {
                    if (Convert.ToBoolean(OthpManager[0]["InActive"]))
                    {
                        ClearForm();
                        SetKardan();
                        SetLabelWarning("عضو مورد نظر غیر فعال می باشد");
                        return;
                    }
                    txtFatherName.Text = OthpManager[0]["FatherName"].ToString();
                    txtFirstName.Text = OthpManager[0]["FirstName"].ToString();
                    txtLastName.Text = OthpManager[0]["LastName"].ToString();
                    txtSSN.Text = OthpManager[0]["SSN"].ToString();
                    txtFileDate.Text = OthpManager[0]["FileNoDate"].ToString();
                    txtFileNo.Text = OthpManager[0]["FileNo"].ToString();

                    MemberAcceptedGradeManager.FindByOtpIdAndResId(Convert.ToInt32(OthpManager[0]["OtpId"]), (int)TSP.DataManager.DocumentResponsibilityType.Implement, 0);
                    if (MemberAcceptedGradeManager.Count > 0)
                        txtImpId.Text = MemberAcceptedGradeManager[0]["GrdName"].ToString();

                    SetKardan();

                }
                else
                {
                    ClearForm();
                    SetKardan();
                    SetLabelWarning("چنین کد عضویتی وجود ندارد.مجدداً وارد نمایید");
                    return;
                }
                #endregion
                break;
            case "4"://Memar
                #region Memar
                OthpManager.FindKardanAndMemarByOtpCode(MeOfOthId.ToString(), (int)TSP.DataManager.OtherPersonType.Memar);
                if (OthpManager.Count == 1)
                {
                    if (Convert.ToBoolean(OthpManager[0]["InActive"]))
                    {
                        ClearForm();
                        SetMemar();
                        SetLabelWarning("عضو مورد نظر غیر فعال می باشد");
                        return;
                    }
                    txtFatherName.Text = OthpManager[0]["FatherName"].ToString();
                    txtFirstName.Text = OthpManager[0]["FirstName"].ToString();
                    txtLastName.Text = OthpManager[0]["LastName"].ToString();
                    txtSSN.Text = OthpManager[0]["SSN"].ToString();
                    txtFileDate.Text = OthpManager[0]["FileNoDate"].ToString();
                    txtFileNo.Text = OthpManager[0]["FileNo"].ToString();

                    MemberAcceptedGradeManager.FindByOtpIdAndResId(Convert.ToInt32(OthpManager[0]["OtpId"]), (int)TSP.DataManager.DocumentResponsibilityType.Implement, 0);
                    if (MemberAcceptedGradeManager.Count > 0)
                        txtImpId.Text = MemberAcceptedGradeManager[0]["GrdName"].ToString();

                    SetMemar();

                }
                else
                {
                    ClearForm();
                    SetMemar();
                    SetLabelWarning("چنین کد عضویتی وجود ندارد.مجدداً وارد نمایید");
                    return;
                }
                #endregion
                break;
        }
    }

    protected void CallbackAttachPageToAutomationLetter_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (String.IsNullOrEmpty(txtLetterNumber_AttachPageToAutomationLetter.Text.Trim()))
        {
            lblErrorInputAttachPageToAutomationLetter.ClientVisible = true;
            lblErrorInputAttachPageToAutomationLetter.Text = "شماره سند وارد نشده است";
            return;
        }

        String PageAddress = "~/Employee/TechnicalServices/Project/ImplementerInsert.aspx";
        String QuerySting = "?ProjectId=" + HDProjectId.Value + "&PrjImpId=" + HDImpId.Value + "&PrjReId=" + RequestId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&PageMode2=" + Utility.EncryptQS("View");

        TSP.DataManager.Automation.AttachPageToLetter objAttachPageToLetter = new TSP.DataManager.Automation.AttachPageToLetter();
        objAttachPageToLetter.AttachPage(txtLetterNumber_AttachPageToAutomationLetter.Text, PageAddress, QuerySting, txtLinkName_AttachPageToAutomationLetter.Text,
            int.Parse(txtTimeOut_AttachPageToAutomationLetter.Text), Utility.GetCurrentUser_UserId());
        if (objAttachPageToLetter.SaveState == true)
        {
            PanelAttachPageToAutomationLetterInputData.ClientVisible = false;
            PanelAttachPageToAutomationLetterFinish.ClientVisible = true;
            lblMessageAttachPageToAutomationLetter.Text = objAttachPageToLetter.Message;
        }
        else
        {
            lblErrorInputAttachPageToAutomationLetter.ClientVisible = true;
            lblErrorInputAttachPageToAutomationLetter.Text = objAttachPageToLetter.Message;
        }
    }
    #endregion

    #region Methods

    #region Set Key-Modes
    private void SetKeys()
    {
        try
        {
            PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode2"].ToString());
            HDProjectId.Value = Server.HtmlDecode(Request.QueryString["ProjectId"]).ToString();
            HDImpId.Value = Server.HtmlDecode(Request.QueryString["PrjImpId"]).ToString();
            RequestId.Value = Server.HtmlDecode(Request.QueryString["PrjReId"]).ToString();

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string ProjectId = Utility.DecryptQS(HDProjectId.Value);
            string PrjImpId = Utility.DecryptQS(HDImpId.Value);
            string PrjReId = Utility.DecryptQS(RequestId.Value);
            string MPageMode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString()));

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(ProjectId) || string.IsNullOrEmpty(PrjImpId) || string.IsNullOrEmpty(PrjReId) || string.IsNullOrEmpty(MPageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            SetMode();
            CheckWorkFlowPermission();
            FillProjectInfo(int.Parse(PrjReId));
            FillCapacityInfo();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    private void SetMode()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        switch (PageMode)
        {
            case "View":
                SetViewMode();
                break;

            case "New":
                SetNewMode();
                break;

            case "Edit":
                SetEditMode();
                break;
        }
    }

    private void SetViewMode()
    {
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        string PrjImpId = Utility.DecryptQS(HDImpId.Value);

        if (string.IsNullOrEmpty(PrjImpId) || string.IsNullOrEmpty(ProjectId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        BtnNew.Enabled = true;
        BtnNew2.Enabled = true;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        //btnDelete.Enabled = false;
        //btnDelete2.Enabled = false;
        CheckAccess();

        CmbType.Enabled = false;
        txtFatherName.Enabled = false;
        txtFileDate.Enabled = false;
        txtFileNo.Enabled = false;
        txtFirstName.Enabled = false;
        txtLastName.Enabled = false;
        txtOrgName.Enabled = false;
        txtSSN.Enabled = false;
        txtManager.Enabled = false;
        ChbMother.Enabled = false;
        ChbOwner.Enabled = false;
        txtArchitectorCode.Enabled = false;
        flpCommit.ClientVisible = false;
        txtImpId.Enabled = false;
        txtID.Enabled = false;
        //txtcCapacityDecrement.Enabled = false;
        //txtcWage.Enabled = false;

        FillForm(int.Parse(PrjImpId), int.Parse(ProjectId));
        SetCapacityVisible(!IsDecreased());
        SetMenuImpEnabled();

        ASPxRoundPanel2.HeaderText = "مشاهده";

    }

    private void SetNewMode()
    {
        BtnNew.Enabled = true;
        BtnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        CheckAccess();

        txtID.Enabled = true;
        CmbType.Enabled = true;
        txtFatherName.Enabled = true;
        txtFileDate.Enabled = true;
        txtFileNo.Enabled = true;
        txtFirstName.Enabled = true;
        txtLastName.Enabled = true;
        txtOrgName.Enabled = true;
        txtSSN.Enabled = true;
        txtManager.Enabled = true;
        ChbMother.Enabled = true;
        ChbOwner.Enabled = true;
        txtArchitectorCode.Enabled = true;
        flpCommit.ClientVisible = true;
        txtImpId.Enabled = true;
        //txtcCapacityDecrement.Enabled = true;
        //txtcWage.Enabled = true;

        btnShowPpcAttachPageToAutomationLetter.Enabled = false;
        btnShowPpcAttachPageToAutomationLetter2.Enabled = false;

        SetCapacityVisible(true);
        ClearForm();
        CapacityUserControl.ClearControlsIngridienCapacityInfo();
        SetMenuImpEnabled();

        ASPxRoundPanel2.HeaderText = "جدید";

    }

    private void SetEditMode()
    {
        string ProjectId = Utility.DecryptQS(HDProjectId.Value);
        string PrjImpId = Utility.DecryptQS(HDImpId.Value);

        if (string.IsNullOrEmpty(PrjImpId) || string.IsNullOrEmpty(ProjectId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        BtnNew.Enabled = true;
        BtnNew2.Enabled = true;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        btnEdit.Enabled = true;
        btnEdit2.Enabled = true;
        CheckAccess();

        txtID.Enabled = false;
        CmbType.Enabled = false;
        txtFatherName.Enabled = false;
        txtFileDate.Enabled = false;
        txtFileNo.Enabled = false;
        txtFirstName.Enabled = false;
        txtLastName.Enabled = false;
        txtOrgName.Enabled = false;
        txtSSN.Enabled = false;
        txtManager.Enabled = false;
        txtArchitectorCode.Enabled = false;
        ChbOwner.Enabled = true;
        ChbMother.Enabled = true;
        flpCommit.ClientVisible = true;
        txtImpId.Enabled = false;

        FillForm(int.Parse(PrjImpId), int.Parse(ProjectId));
        SetCapacityVisible(!IsDecreased());
        SetMenuImpEnabled();

        ASPxRoundPanel2.HeaderText = "ویرایش";
    }
    #endregion

    private void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.Project_ImplementerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    private void SetCapacityVisible(bool Visibility)
    {
        CapacityUserControl.Visible = Visibility;
    }

    private bool IsDecreased()
    {
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        int PrjImpId = Convert.ToInt32(Utility.DecryptQS(HDImpId.Value));

        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager CapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        //CapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(ProjectId, PrjImpId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
        if (CapacityDecrementManager.Count > 0)
            return Convert.ToBoolean(CapacityDecrementManager[0]["IsDecreased"]);
        return false;
    }

    //private string GetCapacityDecrement()
    //{
    //    return CapacityUserControl.CapacityDecrement;
    //}

    //private string GetCapacityWage()
    //{
    //    return CapacityUserControl.CapacityWage;
    //}

    #region Fill
    private void FillForm(int PrjImpId, int ProjectId)
    {
        TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImpManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager CapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();

        try
        {
            ProjectImpManager.FindByPrjImpId(PrjImpId);
            if (ProjectImpManager.Count > 0)
            {
                int MeOfficeId = Convert.ToInt32(ProjectImpManager[0]["MeOfficeId"]);
                string TypeValue = ProjectImpManager[0]["MemberTypeId"].ToString();
                CmbType.DataBind();
                CmbType.SelectedIndex = CmbType.Items.IndexOfValue(TypeValue);
                txtID.Text = MeOfficeId.ToString();
                txtFatherName.Text = ProjectImpManager[0]["FatherName"].ToString();
                txtFileDate.Text = ProjectImpManager[0]["FileDate"].ToString();
                txtFileNo.Text = ProjectImpManager[0]["No"].ToString();
                txtFirstName.Text = ProjectImpManager[0]["FirstName"].ToString();
                txtLastName.Text = ProjectImpManager[0]["LastName"].ToString();
                txtManager.Text = ProjectImpManager[0]["Manager"].ToString();
                txtOrgName.Text = ProjectImpManager[0]["Name"].ToString();
                txtSSN.Text = ProjectImpManager[0]["SSN"].ToString();
                ChbMother.Checked = Convert.ToBoolean(ProjectImpManager[0]["IsMother"]);
                ChbOwner.Checked = Convert.ToBoolean(ProjectImpManager[0]["IsOwner"]);
                //string[] split = ProjectImpManager[0]["CreateDate"].ToString().Split(new Char[] { '/' });
                //string Year = split[0];

                switch (TypeValue)
                {
                    case "1":
                        SetMember();
                        txtImpId.Text = ProjectImpManager[0]["ImpGrdName"].ToString();
                        FillProjectIngridientCapacityInfo(TSP.DataManager.TSMemberType.Member, MeOfficeId);
                        break;
                    case "2":
                        SetOffice();
                        txtImpId.Text = OfficeMemberManager.FindOffImpGradeName(MeOfficeId);
                        FillProjectIngridientCapacityInfo(TSP.DataManager.TSMemberType.Office, MeOfficeId);
                        break;
                    case "3":
                        SetKardan();
                        txtImpId.Text = ProjectImpManager[0]["ImpGrdName"].ToString();
                        break;
                    case "4":
                        SetMemar();
                        txtImpId.Text = ProjectImpManager[0]["ImpGrdName"].ToString();
                        break;
                }

                AttachManager.FindByTableTypeId(PrjImpId, (int)TSP.DataManager.TableCodes.TSProject_Implementer, (int)TSP.DataManager.TSAttachType.Commitment);
                if (AttachManager.Count > 0)
                {
                    HpCommit.ClientVisible = true;
                    HpCommit.NavigateUrl = AttachManager[0]["FilePath"].ToString();
                    HDFlpCommit["name"] = 1;


                }

                CapacityDecrementManager.FindByPrjImpObsDsgnIdAndIngridientTypeId(PrjImpId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                if (CapacityDecrementManager.Count > 0)
                {
                    CapacityUserControl.CapacityDecrement = CapacityDecrementManager[0]["CapacityDecrement"].ToString();
                    CapacityUserControl.CapacityWage = CapacityDecrementManager[0]["Wage"].ToString();
                }
            }
            else
            {
                SetLabelWarning("امکان مشاهده اطلاعات وجود ندارد.اطلاعات توسط کاربر دیگری تغییر یافته است");
            }
        }
        catch (Exception)
        {
            SetLabelWarning("خطایی در مشاهده اطلاعات رخ داده است");
        }
    }

    private void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
        //txtcDecrementPercent.Text = prjInfo.DecrementPercent.ToString();
        //txtcWagePercent.Text = prjInfo.WagePercent.ToString();
        //txtcFoundation.Text = prjInfo.Foundation.ToString();

        HDCitId.Value = prjInfo.CitId.ToString();
        if (prjInfo.CitId == Utility.GetCurrentCitId())
        {
            CmbType.Items.RemoveAt(2);
        }
    }

    // private void FillCapacityForFillForm(int MemberTypeId, int MeId)
    // {
    // ArrayList[0]: TotalCapacity(double), ArrayList[1]:UsedCapacity(double) , ArrayList[2]: RemainCapacity(double), ArrayList[3]:ReservedCapacity(double) , ArrayList[4]: ProjectNum(int)

    //Capacity capacity = new Capacity();
    //ArrayList arr = capacity.GetCapacityInformationInYear((int)TSP.DataManager.TSProjectIngridientType.Implementer, MemberTypeId, MeId, Year);
    //???????????????
    //ArrayList arr = capacity.GetCapacityInformationPerStage((int)TSP.DataManager.TSProjectIngridientType.Implementer, MemberTypeId, MeId);
    //txtcTotalCapacity.Text = arr[0].ToString();
    //txtcTotalFunction.Text = arr[1].ToString();
    //txtcRemainCapacity.Text = arr[2].ToString();
    //txtcReserve.Text = arr[3].ToString();
    //txtcProjectCount.Text = arr[4].ToString();
    //???????????????
    //}

    private void FillProjectIngridientCapacityInfo(TSP.DataManager.TSMemberType TSMemberTypeId, int ProjectIngridientId)
    {
        FillCapacityInfo();
        CapacityUserControl.FillProjectIngridienCapacityInfo(TSMemberTypeId, ProjectIngridientId);
    }

    private void FillCapacityInfo()
    {
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));
        int PrjReId = Convert.ToInt32(Utility.DecryptQS(RequestId.Value));
        CapacityUserControl.ProjectId = ProjectId;
        CapacityUserControl.PrjReqId = PrjReId;
        CapacityUserControl.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Implementer;
    }
    #endregion

    #region Clear Form
    private void ClearForm()
    {
        txtID.Text = "";
        CmbType.DataBind();
        CmbType.SelectedIndex = 0;
        SetMember();
        txtFatherName.Text = "";
        txtFileDate.Text = "";
        txtFileNo.Text = "";
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtOrgName.Text = "";
        txtSSN.Text = "";
        txtManager.Text = "";
        ChbMother.Checked = false;
        ChbOwner.Checked = false;
        HpCommit.NavigateUrl = "";
        HpCommit.ClientVisible = false;
        txtImpId.Text = "";
        txtArchitectorCode.Text = "";

    }

    private void ClearCapacity()
    {
        CapacityUserControl.ClearControlsIngridienCapacityInfo();
        CapacityUserControl.ClearControlsProjectInfo();
        //SetCapacityDecrement("");
        //SetCapacityWage("");
    }
    #endregion

    #region Set Control By Imp Type
    private void SetMember()
    {
        ASPxLabelFatherName.ClientVisible = true;
        ASPxLabelFirstName.ClientVisible = true;
        ASPxLabelLastName.ClientVisible = true;
        ASPxLabelSSN.ClientVisible = true;
        txtFatherName.ClientVisible = true;
        txtFirstName.ClientVisible = true;
        txtLastName.ClientVisible = true;
        txtSSN.ClientVisible = true;
        txtArchitectorCode.ClientVisible = true;
        txtManager.ClientVisible = false;
        txtOrgName.ClientVisible = false;
        ASPxLabelManager.ClientVisible = false;
        ASPxLabelOrgName.ClientVisible = false;

        CmbType.SelectedIndex = 0;

    }

    private void SetKardan()
    {
        ASPxLabelFatherName.ClientVisible = true;
        ASPxLabelFirstName.ClientVisible = true;
        ASPxLabelLastName.ClientVisible = true;
        ASPxLabelSSN.ClientVisible = true;
        txtFatherName.ClientVisible = true;
        txtFirstName.ClientVisible = true;
        txtLastName.ClientVisible = true;
        txtSSN.ClientVisible = true;

        txtManager.ClientVisible = false;
        txtOrgName.ClientVisible = false;
        ASPxLabelManager.ClientVisible = false;
        ASPxLabelOrgName.ClientVisible = false;

        CmbType.SelectedIndex = 2;

    }

    private void SetMemar()
    {
        ASPxLabelFatherName.ClientVisible = true;
        ASPxLabelFirstName.ClientVisible = true;
        ASPxLabelLastName.ClientVisible = true;
        ASPxLabelSSN.ClientVisible = true;
        txtFatherName.ClientVisible = true;
        txtFirstName.ClientVisible = true;
        txtLastName.ClientVisible = true;
        txtSSN.ClientVisible = true;

        txtManager.ClientVisible = false;
        txtOrgName.ClientVisible = false;
        ASPxLabelManager.ClientVisible = false;
        ASPxLabelOrgName.ClientVisible = false;

        CmbType.SelectedIndex = 3;

    }

    private void SetOffice()
    {
        ASPxLabelFatherName.ClientVisible = false;
        ASPxLabelFirstName.ClientVisible = false;
        ASPxLabelLastName.ClientVisible = false;
        ASPxLabelSSN.ClientVisible = false;
        txtFatherName.ClientVisible = false;
        txtFirstName.ClientVisible = false;
        txtLastName.ClientVisible = false;
        txtSSN.ClientVisible = false;

        txtManager.ClientVisible = true;
        txtOrgName.ClientVisible = true;
        ASPxLabelManager.ClientVisible = true;
        ASPxLabelOrgName.ClientVisible = true;

        CmbType.SelectedIndex = 1;
    }
    #endregion

    #region Set Error Methods
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
    #endregion

    #region Insert-Update
    private void Insert(int ProjectId)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();
        TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImpManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager CapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        Capacity Capacity = new Capacity();
        //  TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = (TSP.DataManager.TechnicalServices.AccountingManager)Session["AccountingManager"];

        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager(trans);
        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OtherPersonManager OthpManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.DocOffMemberAcceptedGradeManager MemberGradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
        TSP.DataManager.TechniciansActivityAreasManager TechniciansActivityAreasManager = new TSP.DataManager.TechniciansActivityAreasManager();

        trans.Add(AttachManager);
        trans.Add(ProjectImpManager);
        trans.Add(CapacityDecrementManager);
        //trans.Add(OffManager);
        //trans.Add(OthpManager);
        //trans.Add(MeManager);
        //trans.Add(DocMemberFileManager);
        //trans.Add(DocMemberFileDetailManager);
        //    trans.Add(AccountingManager);

        bool IsAttach = false;

        try
        {
            int AccId = -1;
            decimal Balance = 0;
            string LockName = "";

            string TypeValue = CmbType.Value.ToString();
            int MeOfOthId = int.Parse(txtID.Text);
            int OtpId = -1;

            if (!string.IsNullOrEmpty(TypeValue) || !string.IsNullOrEmpty(MeOfOthId.ToString()))
            {

                //#region IsExist
                //ImpManager.FindByMemberIdTypeId(MeOfOthId, int.Parse(TypeValue));
                //if (ImpManager.Count > 0)
                //{
                //    if (Convert.ToBoolean(ImpManager[0]["InActive"]))
                //    {
                //        IsExist = true;
                //        HDImpId.Value = Utility.EncryptQS(ImpManager[0]["ImplementerId"].ToString());
                //    }
                //}

                //#endregion

                switch (TypeValue)
                {
                    case "1"://Member

                        MeManager.FindByCode(MeOfOthId);

                        #region CheckLock
                        if (Convert.ToBoolean(MeManager[0]["IsLock"]))
                        {
                            LockName = FindLockers(MeOfOthId, (int)TSP.DataManager.LockMemberType.Member, 1);

                            SetLabelWarning("امکان ثبت عضو مورد نظر وجود ندارد.عضو مورد نظر توسط " + LockName + " قفل می باشد ");
                            return;
                        }
                        #endregion

                        #region CheckRepeat
                        ProjectImpManager.FindByMemberIdTypeId(ProjectId, MeOfOthId, (int)TSP.DataManager.TSMemberType.Member);
                        if (ProjectImpManager.Count > 0)
                        {
                            SetLabelWarning("اطلاعات عضو برای این پروژه تکراری می باشد");
                            return;
                        }
                        #endregion

                        if (!CheckFileNo(MeOfOthId))
                            return;

                        if (!CheckCapacity(Capacity, (int)TSP.DataManager.TSMemberType.Member, MeOfOthId, int.Parse(GetCapacityDecrement())))
                            return;


                        #region CheckAccounting
                        if (Utility.CreateAccount())
                        {
                            if (Utility.IsDBNullOrNullValue(MeManager[0]["AccId"]))
                            {
                                SetLabelWarning("حساب عضو انتخاب شده نامشخص می باشد.");
                                return;
                            }
                            AccId = int.Parse(MeManager[0]["AccId"].ToString());
                            Balance = GetAccountBalance(AccId);
                            if (Balance != 0)
                            {
                                SetLabelWarning("امکان ثبت عضو مورد نظر به عنوان مجری وجود ندارد.مانده حساب عضو صفر نمی باشد");
                                return;
                            }
                        }

                        #endregion
                     
                        break;

                    case "2"://Office
                        OffManager.FindByCode(MeOfOthId);
                        if (OffManager.Count == 0)
                        {
                            SetLabelWarning("اطلاعات عضو توسط کاربر دیگری تغییر یافته است");
                            return;
                        }
                        #region CheckRepeat
                        ProjectImpManager.FindByMemberIdTypeId(ProjectId, MeOfOthId, (int)TSP.DataManager.TSMemberType.Office);
                        if (ProjectImpManager.Count > 0)
                        {
                            SetLabelWarning("اطلاعات عضو برای این پروژه تکراری می باشد");
                            return;
                        }
                        #endregion
                        #region CheckLock
                        if (Convert.ToBoolean(OffManager[0]["IsLock"]))
                        {
                            LockName = FindLockers(MeOfOthId, (int)TSP.DataManager.LockMemberType.Office, 1);

                            SetLabelWarning("امکان ثبت عضو مورد نظر وجود ندارد.عضو مورد نظر توسط " + LockName + " قفل می باشد ");
                            return;
                        }
                        #endregion
                        #region CheckFileNo
                        if (Convert.ToInt32(OffManager[0]["MFType"]) != (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement && Convert.ToInt32(OffManager[0]["MFType"].ToString()) != (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesignAndImplement)
                        {
                            SetLabelWarning("امکان ثبت شرکت مورد نظر به عنوان مجری وجود ندارد.شرکت مورد نظر دارای پروانه مجری گری نمی باشد");
                            return;

                        }
                        if (!string.IsNullOrEmpty(OffManager[0]["FileDate"].ToString()))
                        {
                            if (OffManager[0]["FileDate"].ToString().CompareTo(Utility.GetDateOfToday()) <= 0)
                            {
                                SetLabelWarning("امکان ثبت شرکت مورد نظر به عنوان مجری وجود ندارد.مدت زمان اعتبار پروانه اشتغال شرکت به پایان رسیده است.");
                                return;
                            }
                        }
                        #endregion
                        #region CheckAccounting
                        if (Utility.CreateAccount())
                        {
                            if (Utility.IsDBNullOrNullValue(MeManager[0]["AccId"]))
                            {
                                SetLabelWarning("حساب عضو انتخاب شده نامشخص می باشد.");
                                return;
                            }
                            AccId = int.Parse(OffManager[0]["AccId"].ToString());
                            Balance = GetAccountBalance(AccId);
                            if (Balance != 0)
                            {
                                SetLabelWarning("امکان ثبت عضو مورد نظر به عنوان ناظر وجود ندارد.مانده حساب عضو صفر نمی باشد");
                                return;
                            }
                        }
                        #endregion
                        if (!CheckCapacity(Capacity, (int)TSP.DataManager.TSMemberType.Office, MeOfOthId, int.Parse(GetCapacityDecrement())))
                            return;
                        break;

                    case "3"://Kardan                   
                        OthpManager.FindKardanAndMemarByOtpCode(MeOfOthId.ToString(), (int)TSP.DataManager.OtherPersonType.Kardan);
                        OtpId = Convert.ToInt32(OthpManager[0]["OtpId"]);

                        #region CheckLock

                        if (Convert.ToBoolean(OthpManager[0]["IsLock"]))
                        {
                            LockName = FindLockers(OtpId, (int)TSP.DataManager.LockMemberType.Kardan, 1);

                            SetLabelWarning("امکان ثبت عضو مورد نظر وجود ندارد.عضو مورد نظر توسط " + LockName + " قفل می باشد ");
                            return;
                        }


                        #endregion

                        #region CheckFileNo
                        if (Utility.IsDBNullOrNullValue(OthpManager[0]["FileNo"]))
                        {
                            SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان مجری وجود ندارد.شخص انتخاب شده دارای پروانه اشتغال به کار نمی باشد.");
                            return;
                        }
                        MemberGradeManager.FindByOtpIdAndResId(OtpId, (int)TSP.DataManager.DocumentResponsibilityType.Implement, 0);
                        if (MemberGradeManager.Count == 0)
                        {
                            SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان مجری وجود ندارد.شخص انتخاب شده دارای صلاحیت اجرا نمی باشد.");
                            return;
                        }
                        #endregion

                        TechniciansActivityAreasManager.FindByOtpIdResIdCitId(OtpId, (int)TSP.DataManager.DocumentResponsibilityType.Implement, int.Parse(HDCitId.Value));
                        if (TechniciansActivityAreasManager.Count == 0)
                        {
                            SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان مجری وجود ندارد.این پروژه در حوزه فعالیت شخص نمی باشد");
                            return;
                        }
                        break;

                    case "4"://Memar
                        OthpManager.FindKardanAndMemarByOtpCode(MeOfOthId.ToString(), (int)TSP.DataManager.OtherPersonType.Memar);
                        OtpId = Convert.ToInt32(OthpManager[0]["OtpId"]);

                        #region CheckLock

                        if (Convert.ToBoolean(OthpManager[0]["IsLock"]))
                        {
                            LockName = FindLockers(OtpId, (int)TSP.DataManager.LockMemberType.Memar, 1);

                            SetLabelWarning("امکان ثبت عضو مورد نظر وجود ندارد.عضو مورد نظر توسط " + LockName + " قفل می باشد ");
                            return;
                        }


                        #endregion

                        #region CheckFileNo
                        if (Utility.IsDBNullOrNullValue(OthpManager[0]["FileNo"]))
                        {
                            SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان مجری وجود ندارد.شخص انتخاب شده دارای پروانه اشتغال به کار نمی باشد.");
                            return;
                        }
                        MemberGradeManager.FindByOtpIdAndResId(OtpId, (int)TSP.DataManager.DocumentResponsibilityType.Implement, 0);
                        if (MemberGradeManager.Count == 0)
                        {
                            SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان مجری وجود ندارد.شخص انتخاب شده دارای صلاحیت اجرا نمی باشد.");
                            return;
                        }
                        #endregion

                        TechniciansActivityAreasManager.FindByOtpIdResIdCitId(OtpId, (int)TSP.DataManager.DocumentResponsibilityType.Implement, int.Parse(HDCitId.Value));
                        if (TechniciansActivityAreasManager.Count == 0)
                        {
                            SetLabelWarning("امکان ثبت شخص مورد نظر به عنوان مجری وجود ندارد.این پروژه در حوزه فعالیت شخص نمی باشد");
                            return;
                        }
                        break;

                }

            }
            else
            {

                SetLabelWarning("نوع مجری را انتخاب نمایید");
                return;
            }

            //if (!IsExist)
            //{
            //    DataRow drImp = ImpManager.NewRow();
            //    drImp["MemberTypeId"] = TypeValue;
            //    drImp["MeOfficeId"] = MeOfOthId;
            //    drImp["CreateDate"] = Utility.GetDateOfToday();
            //    drImp["UserId"] = Utility.GetCurrentUser_UserId();
            //    drImp["ModifiedDate"] = DateTime.Now;
            //    ImpManager.AddRow(drImp);
            //    ImpManager.Save();
            //    HDImpId.Value = Utility.EncryptQS(ImpManager[0]["ImplementerId"].ToString());

            //}

            #region CheckIsMother
            ProjectImpManager.FindImpMother(ProjectId);
            if (ChbMother.Checked)
            {
                if (ProjectImpManager.Count > 0)
                {
                    SetLabelWarning("مجری مادر قبلاً انتخاب شده است");
                    return;
                }

                //ProjectImpManager.FindByProjectId(ProjectId);
                //for (int i = 0; i < ProjectImpManager.Count; i++)
                //{
                //    if (Convert.ToBoolean(ProjectImpManager[i]["IsMother"]))
                //    {
                //        if (Convert.ToBoolean(ProjectImpManager[i]["InActive"]))
                //        {
                //            SetLabelWarning("مجری مادر قبلاً انتخاب شده است");
                //            return;
                //            //ProjectImpManager[i].BeginEdit();
                //            //ProjectImpManager[i]["IsMother"] = 0;
                //            //ProjectImpManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                //            //ProjectImpManager[i].EndEdit();
                //            //ProjectImpManager.Save();
                //        }
                //    }
                //}
            }
            #endregion

            trans.BeginSave();

            DataRow drPrjImp = ProjectImpManager.NewRow();
            drPrjImp["ProjectId"] = ProjectId;
            drPrjImp["PrjReId"] = int.Parse(Utility.DecryptQS(RequestId.Value));

            switch (TypeValue)
            {
                case "1":
                    drPrjImp["MeOfficeId"] = MeOfOthId;
                    drPrjImp["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.Member;
                    break;
                case "2":
                    drPrjImp["MeOfficeId"] = MeOfOthId;
                    drPrjImp["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.Office;
                    break;
                case "3":
                    drPrjImp["MeOfficeId"] = OtpId;
                    drPrjImp["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.OtherPerson;
                    break;
                case "4":
                    drPrjImp["MeOfficeId"] = OtpId;
                    drPrjImp["MemberTypeId"] = (int)TSP.DataManager.TSMemberType.ExpArchitect;
                    break;
            }

            drPrjImp["IsOwner"] = ChbOwner.Checked;
            drPrjImp["IsMother"] = ChbMother.Checked;
            drPrjImp["CreateDate"] = Utility.GetDateOfToday();
            drPrjImp["UserId"] = Utility.GetCurrentUser_UserId();
            drPrjImp["ModifiedDate"] = DateTime.Now;
            ProjectImpManager.AddRow(drPrjImp);
            if (ProjectImpManager.Save() > 0)
            {
                int PrjImpId = Convert.ToInt32(ProjectImpManager[ProjectImpManager.Count - 1]["PrjImpId"]);
                HDImpId.Value = Utility.EncryptQS(PrjImpId.ToString());

                if (Session["AttachCommit"] != null && Session["AttachCommitName"] != null)
                {
                    DataRow drAtt = AttachManager.NewRow();
                    drAtt["TableTypeId"] = PrjImpId;
                    drAtt["TableType"] = (int)TSP.DataManager.TableCodes.TSProject_Implementer;
                    drAtt["AttachTypeId"] = (int)TSP.DataManager.TSAttachType.Commitment;
                    drAtt["FilePath"] = "~/Image/TechnicalServices/Implementer/" + Path.GetFileName(Session["AttachCommit"].ToString());
                    drAtt["FileName"] = Session["AttachCommitName"];
                    drAtt["UserId"] = Utility.GetCurrentUser_UserId();
                    drAtt["ModifiedDate"] = DateTime.Now;
                    AttachManager.AddRow(drAtt);
                    if (AttachManager.Save() > 0)
                        IsAttach = true;
                }
                else
                {
                    trans.CancelSave();
                    SetLabelWarning("فایل را انتخاب نمایید");
                    return;
                }
                //Capacity.InsertProjectCapacityDecrement(CapacityDecrementManager, GetCapacityDecrement(), GetCapacityWage(), (int)TSP.DataManager.TSProjectIngridientType.Implementer, PrjImpId, null, Utility.GetCurrentUser_UserId());


                trans.EndSave();
                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
                SetEditMode();
                btnShowPpcAttachPageToAutomationLetter.Enabled = true;
                btnShowPpcAttachPageToAutomationLetter2.Enabled = true;
                this.ViewState["BtnLetter"] = btnShowPpcAttachPageToAutomationLetter.Enabled;

                SetLabelWarning("ذخیره انجام شد");
            }

            else
            {
                trans.CancelSave();
                SetLabelWarning("خطایی در ذخیره اطلاعات انجام گرفته است");
            }


        }
        catch (Exception err)
        {
            trans.CancelSave();
            SetError(err, 'I');
        }

        if (CmbType.Value.ToString() == "1")
            SetMember();
        else if (CmbType.Value.ToString() == "2")
            SetOffice();
        else if (CmbType.Value.ToString() == "3")
            SetKardan();
        else
            SetMemar();

        if (IsAttach)
        {
            try
            {
                string ImgSoource = Session["AttachCommit"].ToString();
                string ImgTarget = Server.MapPath("~/Image/TechnicalServices/Implementer/") + Path.GetFileName(Session["AttachCommit"].ToString());
                File.Copy(ImgSoource, ImgTarget, true);
                HpCommit.ClientVisible = true;
                HpCommit.NavigateUrl = ImgTarget; //ImgSoource;

                Session["AttachCommit"] = null;
                Session["AttachCommitName"] = null;

            }
            catch (Exception)
            {
            }
        }

    }

    private void Edit(int PrjImpId, int ProjectId)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.AttachmentsManager AttachManager = new TSP.DataManager.TechnicalServices.AttachmentsManager();
        TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImpManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager CapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();

        trans.Add(AttachManager);
        trans.Add(ProjectImpManager);
        trans.Add(CapacityDecrementManager);


        Capacity Capacity = new Capacity();

        bool IsAttach = false;

        try
        {


            ProjectImpManager.FindImpMother(ProjectId);
            if (ChbMother.Checked)
            {
                if (ProjectImpManager.Count > 0 && Convert.ToInt32(ProjectImpManager[0]["PrjImpId"]) != PrjImpId)
                {
                    SetLabelWarning("مجری مادر قبلاً انتخاب شده است");
                    return;
                }
                

            }
            else if (ProjectImpManager.Count > 0 && Convert.ToInt32(ProjectImpManager[0]["PrjImpId"]) == PrjImpId)
            {
                if (Check2In1000Fiche(PrjImpId))
                {
                    SetLabelWarning("به علت وجود فیش 2 در 1000 برای این مجری، باید مجری مادر باشد");
                    return;
                }
            }

            trans.BeginSave();
            //ProjectImpManager.FindByProjectAndImpId(ProjectId, ImpId);
            ProjectImpManager.FindByPrjImpId(PrjImpId);
            if (ProjectImpManager.Count == 1)
            {
                ProjectImpManager[0].BeginEdit();
                ProjectImpManager[0]["IsOwner"] = ChbOwner.Checked;
                ProjectImpManager[0]["IsMother"] = ChbMother.Checked;
                ProjectImpManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ProjectImpManager[0]["ModifiedDate"] = DateTime.Now;
                ProjectImpManager[0].EndEdit();

                if (ProjectImpManager.Save() > 0)
                {
                    if (Session["AttachCommit"] != null && Session["AttachCommitName"] != null)
                    {
                        AttachManager.FindByTableTypeId(PrjImpId, (int)TSP.DataManager.TableCodes.TSProject_Implementer, (int)TSP.DataManager.TSAttachType.Commitment);
                        if (AttachManager.Count > 0)
                        {
                            AttachManager[0].BeginEdit();
                            if ((!string.IsNullOrEmpty(AttachManager[0]["FilePath"].ToString())) && (File.Exists(Server.MapPath(AttachManager[0]["FilePath"].ToString()))))
                            {
                                File.Delete(Server.MapPath(AttachManager[0]["FilePath"].ToString()));

                                //HpCommit.NavigateUrl = Session["AttachCommit"].ToString();

                                AttachManager[0]["FilePath"] = "~/Image/TechnicalServices/Implementer/" + Path.GetFileName(Session["AttachCommit"].ToString());
                                AttachManager[0]["FileName"] = Session["AttachCommitName"];

                            }
                            else
                            {
                                //HpCommit.NavigateUrl = Session["AttachCommit"].ToString();
                                AttachManager[0]["FilePath"] = "~/Image/TechnicalServices/Implementer/" + Path.GetFileName(Session["AttachCommit"].ToString());
                                AttachManager[0]["FileName"] = Session["AttachCommitName"];

                            }
                            AttachManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            AttachManager[0].EndEdit();
                            if (AttachManager.Save() > 0)
                                IsAttach = true;

                        }
                        else
                        {
                            DataRow drAtt = AttachManager.NewRow();
                            drAtt["TableTypeId"] = ProjectImpManager[0]["PrjImpId"];
                            //drAtt["TableType"] = (int)TSP.DataManager.TableCodes.TSProject_Implementer;
                            drAtt["AttachTypeId"] = (int)TSP.DataManager.TSAttachType.Commitment;
                            drAtt["FilePath"] = "~/Image/TechnicalServices/Implementer/" + Path.GetFileName(Session["AttachCommit"].ToString());
                            drAtt["FileName"] = Session["AttachCommitName"];
                            drAtt["UserId"] = Utility.GetCurrentUser_UserId();
                            drAtt["ModifiedDate"] = DateTime.Now;
                            AttachManager.AddRow(drAtt);
                            if (AttachManager.Save() > 0)
                                IsAttach = true;
                        }

                    }


                    CapacityDecrementManager.FindByPrjImpObsDsgnIdAndIngridientTypeId(PrjImpId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                    //Capacity.UpdateProjectCapacityDecrement(CapacityDecrementManager, GetCapacityDecrement(), GetCapacityWage(), null, Utility.GetCurrentUser_UserId());


                    SetLabelWarning("ذخیره انجام شد");
                    trans.EndSave();

                }
                else
                {
                    trans.CancelSave();
                    SetLabelWarning("خطایی در ذخیره انجام گرفته است");
                }


            }

            else
            {
                trans.CancelSave();
                SetLabelWarning("اطلاعات توسط کاربر دیگری تغییر یافته است");
            }
        }
        catch (Exception err)
        {
            trans.CancelSave();
            SetError(err, 'U');
        }

        if (CmbType.Value.ToString() == "1")
            SetMember();
        else if (CmbType.Value.ToString() == "2")
            SetOffice();
        else if (CmbType.Value.ToString() == "3")
            SetKardan();
        else
            SetMemar();

        if (IsAttach)
        {
            try
            {
                string ImgSoource = Session["AttachCommit"].ToString();
                string ImgTarget = Server.MapPath("~/Image/TechnicalServices/Implementer/") + Path.GetFileName(Session["AttachCommit"].ToString());
                File.Copy(ImgSoource, ImgTarget, true);

                HpCommit.ClientVisible = true;
                HpCommit.NavigateUrl = ImgTarget;//ImgSoource;
                Session["AttachCommit"] = null;
                Session["AttachCommitName"] = null;
            }
            catch (Exception)
            {
            }
        }
    }
    #endregion

    #region Check Conditions
    private bool CheckCapacity(Capacity Capacity, int MemberTypeId, int MeOfId, int CapacityValue)
    {
        string Err = Capacity.CheckCapacityAndJobCount((int)TSP.DataManager.TSProjectIngridientType.Observer, MemberTypeId, MeOfId, CapacityValue);
        if (Err != "")
        {
            SetLabelWarning(Err);
            return false;
        }
        return true;

    }

    private bool CheckFileNo(int MeId)
    {
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OtherPersonManager OthpManager = new TSP.DataManager.OtherPersonManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        DocMemberFileManager.ClearBeforeFill = true;

        DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
        if (dtMeFile.Rows.Count > 0)
        {
            int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
            string ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();

            if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
            {
                DataTable dtMeDetail = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Implement);
                if (dtMeDetail.Rows.Count == 0)
                {

                    SetLabelWarning("امکان ثبت عضو مورد نظر وجود ندارد.پروانه اشتغال عضو مورد نظر صلاحیت اجرا ندارد.");
                    return false;
                }


            }
            else
            {

                SetLabelWarning("امکان ثبت عضو مورد نظر به عنوان مجری وجود ندارد.وضعیت پروانه اشتغال عضو نا مشخص می باشد.");
                return false;

            }

            if (!string.IsNullOrEmpty(ExpireDate))
            {
                if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
                {
                    SetLabelWarning("امکان ثبت عضو مورد نظر عنوان مجری وجود ندارد.مدت زمان اعتبار پروانه اشتغال عضو به پایان رسیده است.");
                    return false;
                }
            }

        }
        else
        {

            SetLabelWarning("امکان ثبت عضو مورد نظر به عنوان مجری وجود ندارد.عضو انتخاب شده دارای پروانه اشتغال به کار نمی باشد.");
            return false;

        }
        DocMemberFileManager.SelectImpDocLastVersionByMeId(MeId);
        if (DocMemberFileManager.Count == 0)
        {
            SetLabelWarning("امکان ثبت عضو مورد نظر به عنوان مجری وجود ندارد.عضو انتخاب شده دارای مجوز اجرا نمی باشد.");
            return false;

            //OfMeManager.FindOffMemberByPersonId(MeId, 2);
            //if (OfMeManager.Count > 0)
            //{
            //    int OfId = int.Parse(OfMeManager[0]["OfId"].ToString());
            //    OffManager.FindByCode(OfId);
            //    if (OffManager.Count > 0)
            //    {
            //        if (Convert.ToInt32(OffManager[0]["MFType"]) != (int)TSP.DataManager.DocumentOfficeResponsibilityType.Implement && Convert.ToInt32( OffManager[0]["MFType"].ToString()) != (int)TSP.DataManager.DocumentOfficeResponsibilityType.ObservationAndDesignAndImplement)
            //        {
            //            SetLabelWarning( "امکان ثبت عضو مورد نظر به عنوان مجری وجود ندارد.عضو انتخاب شده دارای مجوز اجرا نمی باشد");
            //            return false;
            //        }
            //    }
            //    else
            //    {
            //        SetLabelWarning("امکان ثبت عضو مورد نظر به عنوان مجری وجود ندارد.عضو انتخاب شده دارای مجوز اجرا نمی باشد");
            //        return false;
            //    }
            //}
            //else
            //{
            //    SetLabelWarning("امکان ثبت عضو مورد نظر به عنوان مجری وجود ندارد.عضو انتخاب شده دارای مجوز اجرا نمی باشد");
            //    return false;
            //}

            //if (DocMemberFileManager.Count == 0)
            //{
            //    SetLabelWarning("امکان ثبت عضو مورد نظر به عنوان مجری وجود ندارد.عضو انتخاب شده دارای مجوز اجرا نمی باشد.");
            //    return;
            //}
        }
        return true;
    }

    private bool CheckIsMother()
    {
        int PrjImpId = Convert.ToInt32(Utility.DecryptQS(HDImpId.Value));
        int ProjectId = Convert.ToInt32(Utility.DecryptQS(HDProjectId.Value));

        TSP.DataManager.TechnicalServices.Project_ImplementerManager ProjectImpManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();
        ProjectImpManager.FindImpMother(ProjectId);
        if (ProjectImpManager.Count > 0 && Convert.ToInt32(ProjectImpManager[0]["PrjImpId"]) == PrjImpId)
            return true;
        return false;
    }

    private bool CheckContract()
    {
        int PrjImpId = Convert.ToInt32(Utility.DecryptQS(HDImpId.Value));

        TSP.DataManager.TechnicalServices.ContractManager ContractManager = new TSP.DataManager.TechnicalServices.ContractManager();
        ContractManager.FindByPrjImpObsDsgnId(PrjImpId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
        if (ContractManager.Count > 0)
            return true;
        else
            return false;
    }

    private decimal GetAccountBalance(int AccId)
    {
        TSP.DataManager.AccountingDocBalanceDetailManager AccManager = new TSP.DataManager.AccountingDocBalanceDetailManager();
        return AccManager.GetAccountBalance(AccId, Utility.GetDateOfToday());

    }

    private string FindLockers(int Id, int MemberTypeId, int IsLock)
    {
        TSP.DataManager.LockHistoryManager LockHistoryManager = new TSP.DataManager.LockHistoryManager();
        return LockHistoryManager.FindLockers(Id, MemberTypeId, IsLock);

    }

    private bool Check2In1000Fiche(int PrjImpId)
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.FindByTableTypeId(PrjImpId, (int)TSP.DataManager.TableCodes.TSProject_Implementer);
        if (AccountingManager.Count > 0)
            return true;
        else
            return false;
    }
    #endregion

    private void SetMenuImpEnabled()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        MenuImp.Items[3].Enabled = false;
        switch (PageMode)
        {
            case "New":
                MenuImp.Items[0].Enabled = false;
                MenuImp.Items[1].Enabled = false;
                MenuImp.Items[2].Enabled = false;
                MenuImp.Items[3].Enabled = false;
                break;

            case "View":
                MenuImp.Items[0].Enabled = true;
                MenuImp.Items[1].Enabled = true;
                MenuImp.Items[2].Enabled = true;
                if (CheckIsMother() && CheckContract())
                {
                    MenuImp.Items[3].Enabled = true;
                }
                break;

            case "Edit":
                MenuImp.Items[0].Enabled = true;
                MenuImp.Items[1].Enabled = true;
                MenuImp.Items[2].Enabled = true;
                if (CheckIsMother())
                {
                    MenuImp.Items[3].Enabled = true;
                }
                break;
        }

    }

    private string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                Session["AttachCommitName"] = Path.GetFileName(uploadedFile.PostedFile.FileName);

                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/TechnicalServices/Implementer/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["AttachCommit"] = tempFileName;
            //Session["FileOfArm2"] = ret;

        }
        return ret;
    }

    #region WF
    private void CheckWorkFlowPermission()
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        CheckWorkFlowPermissionForEdit(PageMode);
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        //*****TableId
        string PrjReId = Utility.DecryptQS(RequestId.Value);
        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementerOfProject;
        int WFCode = (int)TSP.DataManager.WorkFlows.TSProjectConfirming;

        int ChangeWFCode = (int)TSP.DataManager.WorkFlows.TSChangeImplementerConfirming;
        int ChangeTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveChangeProjectImplementerRequestInfo;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission ChangeWFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(ChangeTaskCode, ChangeWFCode, int.Parse(PrjReId), Utility.GetCurrentUser_UserId(), PageMode);

        btnEdit.Enabled = WFPer.BtnEdit || ChangeWFPer.BtnEdit;
        btnEdit2.Enabled = WFPer.BtnEdit || ChangeWFPer.BtnEdit;
        btnSave.Enabled = WFPer.BtnSave || ChangeWFPer.BtnSave;
        btnSave2.Enabled = WFPer.BtnSave || ChangeWFPer.BtnSave;
        BtnNew.Enabled = WFPer.BtnNew || ChangeWFPer.BtnNew;
        BtnNew2.Enabled = WFPer.BtnNew || ChangeWFPer.BtnNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }
    #endregion

    #region بدست آوردن و یا تنظیم مقادیر متراژ کسر ظرفیت ناظر/متراژ دستمزد ناظر وارد شده توسط کاربر
    private void SetCapacityWage(string CapacityWage)
    {
        CapacityUserControl.CapacityWage = CapacityWage;
    }

    private string GetCapacityWage()
    {
        return CapacityUserControl.CapacityWage;
    }

    private void SetCapacityDecrement(string CapacityDecrement)
    {
        CapacityUserControl.CapacityDecrement = CapacityDecrement;
    }

    private string GetCapacityDecrement()
    {
        return CapacityUserControl.CapacityDecrement;
    }

    private void SetCapcityUserControlEnable(Boolean Enable)
    {
        CapacityUserControl.CapacityDecrementEnable = Enable;
        CapacityUserControl.CapacityWageEnable = Enable;
    }
    #endregion
    #endregion
}

