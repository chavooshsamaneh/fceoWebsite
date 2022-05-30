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
using System.Globalization;

public partial class Employee_ImplementDoc_AddImplementDoc : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Request.QueryString["MFId"] == null || Request.QueryString["PgMd"] == null)
            {
                Response.Redirect("ImplementDoc.aspx");
            }
            TblTransfer.Visible = false;
            TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionImp(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            SetKeys();
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnSave"] = btnSave.Enabled;


        }
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
    }

    protected void txtMeId_TextChanged(object sender, EventArgs e)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        if (Utility.IsDBNullOrNullValue(txtMeId.Text.Trim()))
        {
            ShowMessage("لطفا کد عضویت را وارد نمائید.");
            return;
        }
        try
        {
            int MeId = int.Parse(txtMeId.Text.Trim());
            ClearForm();
            FillMember(MeId);
            CheckConditions(MeId);

            cmbIsTemporary.SelectedIndex = 0;
            string Today = Utility.GetDateOfToday();
            txtLastRegDateImp.Text = Today;

            PersianCalendar FC = new PersianCalendar();
            DateTime DtAddYear = FC.AddYears(DateTime.Now.Date, 1);
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            string ExpireDate = PDate.GetYear(DtAddYear) + "/" + PDate.GetMonth(DtAddYear).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DtAddYear).ToString().PadLeft(2, '0');
            txtExpDateImp.Text = ExpireDate;

            DataTable dtImpDoc = DocMemberFileManager.SelectImpDocLastVersion(MeId, -1, -1, -1);
            if (dtImpDoc.Rows.Count > 0)
            {
                ShowMessage("بیش از این برای این عضو مجوز اجرا تعریف شده است.");
                HiddenFieldDocMemberFile["MFId"] = Utility.EncryptQS(dtImpDoc.Rows[0]["MfId"].ToString());
                SetEditModeKeys();
            }
            TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
            DataTable dtworkReq = ObserverWorkRequestManager.SelectTSObserverWorkRequestByMember(MeId, TSP.DataManager.TSObserverWorkRequestStatus.Confirm);
            if (dtworkReq.Rows.Count > 0)
            {
                ShowMessage("امکان ثبت مجوز اجرا وجود ندارد.عضو انتخابی دارای درخواست آماده به کاری تایید شده می باشد.");
                txtMeId.ReadOnly = false;
                return;
            }

            //DataTable dtObsDoc = DocMemberFileManager.SelectObsDocLastVersionByMeId(MeId, 1);
            //if (dtObsDoc.Rows.Count > 0)
            //{
            //    ShowMessage("بیش از این برای این عضو مجوز نظارت تعریف شده است.");
            //    txtMeId.ReadOnly = false;
            //    return;
            //}
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازخوانی اطلاعات عضو انجام گرفته است");
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        SetNewModeKeys();
        HiddenFieldDocMemberFile["MeId"] = "";
        HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("New");
        HiddenFieldDocMemberFile["MFId"] = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
        if (string.IsNullOrEmpty(MFId) && PageMode != "New")
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        if (PageMode == "New")
        {
            InsertDocMemberFileImp();
        }
        else if (PageMode == "Edit")
        {
            Edit(int.Parse(MFId));
        }
        else if (PageMode == "ReDuplicate")
        {
            InsertNewRequest(TSP.DataManager.DocumentOfMemberRequestType.ReDuplicate);
        }
        else if (PageMode == "Revival")
        {
            InsertNewRequest(TSP.DataManager.DocumentOfMemberRequestType.Revival);
        }
        else if (PageMode == "Change")
        {
            InsertNewRequest(TSP.DataManager.DocumentOfMemberRequestType.Change);
        }
        else if (PageMode == "InValid")
        {
            InsertNewRequest(TSP.DataManager.DocumentOfMemberRequestType.InActive);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(Request.QueryString["PostId"])
            && !string.IsNullOrEmpty(Request.QueryString["SrchFlt"]))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            string PostId = Server.HtmlDecode(Request.QueryString["PostId"]);
            string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"]);
            Response.Redirect("ImplementDoc.aspx?PostId=" + PostId + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("ImplementDoc.aspx");
        }
    }

    protected void MenuMemberFile_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "JobHistory":
                Response.Redirect("~/Employee/Document/MemberJobHistory.aspx?MFId=" + HiddenFieldDocMemberFile["MFId"].ToString() + "&PgMd=" + HiddenFieldDocMemberFile["PageMode"].ToString() + "&DocType=" + Utility.EncryptQS("1"));
                break;
            case "Financial":
                Response.Redirect("FinancialStatus.aspx?MFId=" + HiddenFieldDocMemberFile["MFId"].ToString() + "&PgMd=" + HiddenFieldDocMemberFile["PageMode"].ToString());
                break;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (HiddenFieldDocMemberFile["MFId"] != null || !string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString()))
        {
            string MfId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
            if (CheckPermitionForEdit(int.Parse(MfId)))
            {
                SetEditModeKeys();
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار وجود ندارد.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان ویرایش اطلاعات وجود ندارد.";
        }
    }

    protected void CallbackPanelDoRegDate_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (string.IsNullOrEmpty(e.Parameter))
        {
            return;
        }
        SetImpDocDefualtExpireDate(Convert.ToInt32(e.Parameter));
    }
    #endregion

    #region Methods
    #region SetModes
    private void SetKeys()
    {
        HiddenFieldDocMemberFile["MFId"] = Request.QueryString["MFId"].ToString();
        HiddenFieldDocMemberFile["PageMode"] = Request.QueryString["PgMd"];
        int ImpDocId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.ClearBeforeFill = true;

        string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());

        if (PageMode != "New")
        {
            DocMemberFileManager.SelectImplementDoc(-1, ImpDocId);
            if (DocMemberFileManager.Count == 1)
            {
                HiddenFieldDocMemberFile["MemberFileId"] = Utility.EncryptQS(DocMemberFileManager[0]["MeId"].ToString());
                int MfId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MemberFileId"].ToString()));

                DocMemberFileManager.FindByCode(MfId, 0);
                if (DocMemberFileManager.Count == 1)
                {
                    HiddenFieldDocMemberFile["MeId"] = Utility.EncryptQS(DocMemberFileManager[0]["MeId"].ToString());
                }
            }
            // string MeId = Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString());
        }

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode(PageMode);
        CheckWorkFlowPermission();
    }

    private void SetMode(string PageMode)
    {
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        string MFId = Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString());
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        DocMemberFileManager.SelectImplementDoc(-1, int.Parse(MFId));
        if (DocMemberFileManager.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["TaskName"]))
                lblWorkFlowState.Text = "وضعیت درخواست: " + DocMemberFileManager[0]["TaskName"].ToString();
            else
                lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        }
        else
        {
            lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        }
        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "New":
                SetNewModeKeys();
                lblWorkFlowState.Visible = false;
                break;

            case "Edit":
                SetEditModeKeys();
                break;

            case "ReDuplicate":
                SetRequestMode("ReDuplicate");
                lblWorkFlowState.Visible = false;
                break;

            case "Revival":
                SetRequestMode("Revival");
                lblWorkFlowState.Visible = false;
                break;

            case "Change":
                SetRequestMode("Change");
                lblWorkFlowState.Visible = false;
                break;

            case "InValid":
                SetRequestMode("InValid");
                lblWorkFlowState.Visible = false;
                break;
            case "Active":
                SetRequestMode("Active");
                lblWorkFlowState.Visible = false;
                break;
        }
    }

    private void SetViewModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionImp(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Buttom's Enabled        
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        if (per.CanNew)
        {
            BtnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
        }

        MenuMemberFile.Enabled = true;
        TblTransfer.Visible = false;
        RoundPanelImpDoc.Enabled = false;

        if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
        {
            Response.Redirect("MemberFiles.aspx");
            return;
        }
        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        FillForm(MFId);

        RoundPanelMemberFile.HeaderText = "مشاهده";
        RoundPanelMemberFile.Enabled = false;
        if (!CheckPermitionForEdit(MFId))
        {
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
        }
        InsertWorkFlowStateView(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp), MFId);
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void SetNewModeKeys()
    {
        //Set Button's Enable
        RoundPanelMemberFile.Enabled = true;
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        MenuMemberFile.Enabled = false;
        ClearForm();
        txtMeId.ReadOnly = false;
        RoundPanelMemberFile.HeaderText = "جدید";
        RoundPanelImpDoc.Enabled = true;
        cmbIsTemporary.SelectedIndex = 0;
    }

    private void SetEditModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionImp(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        if (HiddenFieldDocMemberFile["MFId"] == null || string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("Edit");
        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        MenuMemberFile.Enabled = true;
        txtMeId.ReadOnly = true;
        FillForm(MFId);
        InsertWorkFlowStateView(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp), MFId);
        RoundPanelMemberFile.Enabled = true;
        RoundPanelMemberFile.HeaderText = "ویرایش";
    }

    //private void SetReDuplicateModeKeys()
    //{
    //    //Check UserPermission
    //    TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionImp(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
    //    btnSave.Enabled = per.CanEdit;
    //    btnSave2.Enabled = per.CanEdit;
    //    if (per.CanNew)
    //    {
    //        BtnNew.Enabled = true;
    //        btnNew2.Enabled = true;
    //    }
    //    btnEdit.Enabled = false;
    //    btnEdit2.Enabled = false;

    //    MenuMemberFile.Enabled = false;

    //    if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
    //    {
    //        Response.Redirect("MemberFiles.aspx");
    //        return;
    //    }
    //    int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
    //    FillForm(MFId);

    //    txtFollowCode.Text = "";
    //    lblFollowCode.Visible = false;
    //    txtFollowCode.Visible = false;

    //    RoundPanelMemberFile.HeaderText = "مشاهده-درخواست صدور المثنی";
    //    RoundPanelMemberFile.Enabled = true;
    //    txtMeId.ReadOnly = true;
    //    this.ViewState["BtnEdit"] = btnEdit.Enabled;
    //    this.ViewState["BtnNew"] = BtnNew.Enabled;
    //    this.ViewState["BtnSave"] = btnSave.Enabled;
    //    RoundPanelImpDoc.Enabled = true;
    //}

    //private void SetRevivalModeKeys()
    //{
    //    //Check UserPermission
    //    TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionImp(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
    //    //ُSet Buttom's Enabled        
    //    btnSave.Enabled = false;
    //    btnSave2.Enabled = false;
    //    if (per.CanNew)
    //    {
    //        BtnNew.Enabled = true;
    //        btnNew2.Enabled = true;
    //    }
    //    if (per.CanEdit)
    //    {
    //        btnEdit.Enabled = true;
    //        btnEdit2.Enabled = true;
    //    }

    //    MenuMemberFile.Enabled = false;
    //    if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
    //    {
    //        Response.Redirect("MemberFiles.aspx");
    //        return;
    //    }
    //    int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
    //    FillForm(MFId);

    //    txtFollowCode.Text = "";
    //    lblFollowCode.Visible = false;
    //    txtFollowCode.Visible = false;

    //    RoundPanelMemberFile.HeaderText = "مشاهده-درخواست تمدید";
    //    RoundPanelMemberFile.Enabled = true;
    //    txtMeId.ReadOnly = true;
    //    RoundPanelImpDoc.Enabled = true;
    //    this.ViewState["BtnEdit"] = btnEdit.Enabled;
    //    this.ViewState["BtnNew"] = BtnNew.Enabled;
    //    this.ViewState["BtnSave"] = btnSave.Enabled;
    //}

    //private void SetChangeModeKeys()
    //{
    //    //Check UserPermission
    //    TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionImp(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
    //    //ُSet Buttom's Enabled        
    //    btnSave.Enabled = false;
    //    btnSave2.Enabled = false;
    //    BtnNew.Enabled = per.CanNew;
    //    btnNew2.Enabled = per.CanNew;
    //    btnEdit.Enabled = per.CanEdit;
    //    btnEdit2.Enabled = per.CanEdit;

    //    MenuMemberFile.Enabled = false;

    //    if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
    //    {
    //        Response.Redirect("MemberFiles.aspx");
    //        return;
    //    }
    //    int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
    //    FillForm(MFId);

    //    txtFollowCode.Text = "";
    //    lblFollowCode.Visible = false;
    //    txtFollowCode.Visible = false;

    //    RoundPanelMemberFile.HeaderText = "مشاهدهدرخواست تغییرات-درخواست تغییرات";
    //    RoundPanelMemberFile.Enabled = true;
    //    RoundPanelImpDoc.Enabled = true;
    //    txtMeId.ReadOnly = true;
    //    this.ViewState["BtnEdit"] = btnEdit.Enabled;
    //    this.ViewState["BtnNew"] = BtnNew.Enabled;
    //    this.ViewState["BtnSave"] = btnSave.Enabled;
    //}

    //private void SetInValidModeKeys()
    //{
    //    //Check UserPermission
    //    TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionImp(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
    //    //ُSet Buttom's Enabled        
    //    btnSave.Enabled = false;
    //    btnSave2.Enabled = false;
    //    BtnNew.Enabled = per.CanNew;
    //    btnNew2.Enabled = per.CanNew;
    //    btnEdit.Enabled = per.CanEdit;
    //    btnEdit2.Enabled = per.CanEdit;

    //    MenuMemberFile.Enabled = false;

    //    if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
    //    {
    //        Response.Redirect("MemberFiles.aspx");
    //        return;
    //    }
    //    int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
    //    FillForm(MFId);

    //    txtFollowCode.Text = "";
    //    lblFollowCode.Visible = false;
    //    txtFollowCode.Visible = false;

    //    RoundPanelMemberFile.HeaderText = "مشاهده-درخواست ابطال";
    //    RoundPanelMemberFile.Enabled = true;
    //    RoundPanelImpDoc.Enabled = false;
    //    txtMeId.ReadOnly = true;
    //    this.ViewState["BtnEdit"] = btnEdit.Enabled;
    //    this.ViewState["BtnNew"] = BtnNew.Enabled;
    //    this.ViewState["BtnSave"] = btnSave.Enabled;
    //}

    private void SetRequestMode(string RequestType)
    {
        TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionImp(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        BtnNew.Enabled = per.CanNew;
        btnNew2.Enabled = per.CanNew;
        btnEdit.Enabled = per.CanEdit;
        btnEdit2.Enabled = per.CanEdit;

        MenuMemberFile.Enabled = false;

        if (HiddenFieldDocMemberFile["MFId"] == null || (string.IsNullOrEmpty(HiddenFieldDocMemberFile["MFId"].ToString())))
        {
            Response.Redirect("MemberFiles.aspx");
            return;
        }
        int MFId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MFId"].ToString()));
        FillForm(MFId);

        txtFollowCode.Text = "";
        lblFollowCode.Visible = false;
        txtFollowCode.Visible = false;
        RoundPanelMemberFile.HeaderText = "درخواست جدید";
        RoundPanelMemberFile.Enabled = true;
        switch (RequestType)
        {
            case "InValid":
                RoundPanelImpDoc.Enabled = false;
                break;
        }
        txtMeId.ReadOnly = true;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        SetRequestSetting(RequestType);
    }

    private void SetRequestSetting(string RequestType)
    {
        switch (RequestType)
        {
            case "ReDuplicate":
                RoundPanelMemberFile.HeaderText += "-درخواست صدور المثنی";
                break;
            case "Revival":
                RoundPanelMemberFile.HeaderText += "-درخواست تمدید";
                break;
            case "Change":
                RoundPanelMemberFile.HeaderText += "-درخواست تغییرات";
                break;
            case "InValid":
                RoundPanelMemberFile.HeaderText += "-درخواست ابطال";
                break;
            case "Active":
                RoundPanelMemberFile.HeaderText += "-درخواست احیاء";
                break;

        }
    }
    #endregion

    #region Inset Update
    private void InsertDocMemberFileImp()
    {
        TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TransactionManager.Add(DocMemberFileManager);
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(TransferMemberManager);

        int MeId = -2;
        Boolean IsTransfer = false;
        string PreMFNo = "";
        string PrCode = "";
        try
        {
            if (cmbGradeImp.SelectedItem == null)
            {
                ShowMessage("پایه مجوز را انتخاب نمایید.");
                return;
            }
            MeId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString()));
            if (!CheckConditions(MeId)) return;

            DataTable dtImpDoc = DocMemberFileManager.SelectImpDocLastVersion(MeId, -1, -1, -1);
            if (dtImpDoc.Rows.Count > 0)
            {
                ShowMessage("پیش از این برای این عضو مجوز اجرا تعریف شده است.");
                SetEditModeKeys();
                txtMeId.ReadOnly = false;
                return;
            }
            DataTable dtworkReq= ObserverWorkRequestManager.SelectTSObserverWorkRequestByMember(MeId, TSP.DataManager.TSObserverWorkRequestStatus.Confirm);            
            if (dtworkReq.Rows.Count > 0)
            {
                ShowMessage("امکان ثبت مجوز اجرا وجود ندارد.عضو انتخابی دارای درخواست آماده به کاری تایید شده می باشد.");
                return;
            }
            PrCode = Utility.GetCurrentProvinceNezamCodeFromTblProvince().ToString();
            if (string.IsNullOrEmpty(PrCode))
            {
                ShowMessage("کد نظام مهندسی استان جاری مشخص نمی باشد");
                return;
            }

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
        try
        {
            #region InsertImp

            TransactionManager.BeginSave();
            MemberManager.FindByCode(MeId);
            if (MemberManager.Count != 1)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            DataRow MemberFileRow = DocMemberFileManager.NewRow();
            MemberFileRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.ImplementDocument);
            MemberFileRow["MailNo"] = "";
            MemberFileRow["MailDate"] = "";
            MemberFileRow["MeId"] = Utility.DecryptQS(HiddenFieldDocMemberFile["LastMemberFileId"].ToString());//****پروانه اشتغال به کار عضو Id
            MemberFileRow["DocType"] = 1;//****مجوز مجری حقیقی
            if (!Utility.IsDBNullOrNullValue(txtSerialNoImp.Text))
                MemberFileRow["SerialNo"] = txtSerialNoImp.Text;
            MemberFileRow["RegDate"] = txtLastRegDateImp.Text;
            MemberFileRow["ExpireDate"] = txtExpDateImp.Text;
            MemberFileRow["IsTemporary"] = cmbIsTemporary.SelectedIndex;
            MemberFileRow["Description"] = txtDescription.Text;
            MemberFileRow["PrId"] = Utility.GetCurrentProvinceId();
            MemberFileRow["Type"] = (int)TSP.DataManager.DocumentOfMemberRequestType.New; //*****صدور
            MemberFileRow["IsConfirm"] = 0;
            MemberFileRow["IsTemporary"] = 0;
            MemberFileRow["InActive"] = 0;
            MemberFileRow["GrdId"] = cmbGradeImp.SelectedItem.Value;
            MemberFileRow["UserId"] = Utility.GetCurrentUser_UserId();
            MemberFileRow["ModifiedDate"] = DateTime.Now;
            MemberFileRow["CreateDate"] = Utility.GetDateOfToday();

            //TransferMemberManager.FindByMemberId(MeId, TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
            //if (TransferMemberManager.Count > 0 && !Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
            //{
            //    #region TransferMember
            //    TransferMemberManager[0].BeginEdit();

            //    if (ComboDocPr.SelectedItem != null && ComboDocPr.SelectedItem.Value != null)
            //        TransferMemberManager[0]["DocPrId"] = ComboDocPr.SelectedItem.Value;

            //    TransferMemberManager[0].EndEdit();
            //    if (TransferMemberManager.Save() <= 0)
            //    {
            //        TransactionManager.CancelSave();
            //        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            //        return;
            //    }
            //    if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["DocPrId"]))
            //        MemberFileRow["PrIdOrigin"] = Convert.ToInt32(TransferMemberManager[0]["DocPrId"]);
            //    MemberFileRow["MFNoOrigin"] = TransferMemberManager[0]["FileNo"].ToString();
            //    PreMFNo = TransferMemberManager[0]["FileNo"].ToString();
            //    PrCode = TransferMemberManager[0]["FileNo"].ToString().Remove(2);
            //    //if (cmbTransferType.Value != null)
            //    //    MemberFileRow["Type"] = Convert.ToInt32(cmbTransferType.Value);// TSP.DataManager.DocumentOfMemberRequestType.Transfer;//*****انتقالی
            //    IsTransfer = true;
            //    #endregion
            //}
            //else
            //{
            //    //*****صدور
            //    MemberFileRow["Type"] = (int)TSP.DataManager.DocumentOfMemberRequestType.New;
            //}

            DocMemberFileManager.AddRow(MemberFileRow);
            int cn = DocMemberFileManager.Save();
            DocMemberFileManager.DataTable.AcceptChanges();
            if (cn <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }
            #region Create MfNo
            string MfCode = "";
            string ImpDocCode = "20";

            string MfSerialNo = DocMemberFileManager[DocMemberFileManager.Count - 1]["MFSerialNo"].ToString();
            int SerialLen = MfSerialNo.Length;
            int t = 5 - SerialLen;
            for (int i = 0; i < t; i++)
            {
                MfSerialNo = "0" + MfSerialNo;
            }

            MfCode = ImpDocCode + "-" + PrCode + "-" + MfSerialNo;

            DocMemberFileManager[DocMemberFileManager.Count - 1].BeginEdit();
            if (!IsTransfer)
            {
                DocMemberFileManager[DocMemberFileManager.Count - 1]["MFNo"] = MfCode;
                DocMemberFileManager[DocMemberFileManager.Count - 1]["MFSerialNo"] = MfSerialNo;
            }
            else
                DocMemberFileManager[DocMemberFileManager.Count - 1]["MFNo"] = MfCode;
            DocMemberFileManager[DocMemberFileManager.Count - 1].EndEdit();
            if (DocMemberFileManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            #endregion

            #region WorkFlow
            int TaskId = -1;
            int TableId = int.Parse(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"].ToString());
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }
            TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            int CurrentNmcId = FindNmcId(TaskId);
            if (CurrentNmcId == -1)
            {
                TransactionManager.CancelSave();
                return;
            }
            int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);
            if (WfStart <= 0)
            {
                TransactionManager.CancelSave();
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                return;
            }

            TransactionManager.EndSave();
            ShowMessage("ذخیره انجام شد.");
            HiddenFieldDocMemberFile["MFId"] = Utility.EncryptQS(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"].ToString());
            TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionImp(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = false;
            btnEdit2.Enabled = false;
            btnSave.Enabled = per.CanEdit;
            btnSave2.Enabled = per.CanEdit;
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("Edit");
            MenuMemberFile.Enabled = true;
            txtMeId.ReadOnly = true;
            RoundPanelMemberFile.Enabled = true;
            RoundPanelMemberFile.HeaderText = "ویرایش";

            #endregion
            #endregion

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            TransactionManager.CancelSave();
            SetError(err);
        }
    }

    private void Edit(int MfId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        try
        {
            if (cmbGradeImp.SelectedItem == null)
            {
                ShowMessage("پایه مجوز را انتخاب نمایید.");
                return;
            }
            // DocMemberFileManager.FindByCode(MfId, 1);
            DocMemberFileManager.SelectImplementDoc(-1, MfId);
            if (DocMemberFileManager.Count == 1)
            {
                DocMemberFileManager[0].BeginEdit();
                DocMemberFileManager[0]["MailNo"] = "";
                DocMemberFileManager[0]["MailDate"] = "";
                if (!Utility.IsDBNullOrNullValue(txtSerialNoImp.Text))
                    DocMemberFileManager[0]["SerialNo"] = txtSerialNoImp.Text;
                DocMemberFileManager[0]["IsTemporary"] = cmbIsTemporary.SelectedIndex;
                //  DocMemberFileManager[0]["MFNo"] = 
                DocMemberFileManager[0]["RegDate"] = txtLastRegDateImp.Text;
                DocMemberFileManager[0]["ExpireDate"] = txtExpDateImp.Text;
                DocMemberFileManager[0]["Description"] = txtDescription.Text;
                DocMemberFileManager[0]["GrdId"] = cmbGradeImp.SelectedItem.Value;
                DocMemberFileManager[0].EndEdit();
                int cn = DocMemberFileManager.Save();
                if (cn > 0)
                {
                    ShowMessage("ذخیره انجام شد.");
                }
                else
                {
                    ShowMessage("خطایی در ذخیره انجام گرفته است.");
                    return;
                }
            }
            else
            {
                ShowMessage("اطلاعات توسط کاربر دیگری تغییر یافته است.");
                return;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetError(err);
        }
    }

    private void InsertNewRequest(TSP.DataManager.DocumentOfMemberRequestType DocumentOfMemberRequestType)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(DocMemberFileManager);
        try
        {
            if (cmbGradeImp.SelectedItem == null && DocumentOfMemberRequestType != TSP.DataManager.DocumentOfMemberRequestType.InActive)
            {
                ShowMessage("پایه مجوز را انتخاب نمایید.");
                return;
            }
            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString()));
            if (Utility.IsDBNullOrNullValue(MeId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            DocMemberFileManager.SelectImpDocLastVersion(MeId, -1, 1, -1);
            if (DocMemberFileManager.Count != 1)
            {
                ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                return;
            }
            #region Insert Request
            TransactionManager.BeginSave();
            DataRow MeFileRow = DocMemberFileManager.NewRow();
            if (DocumentOfMemberRequestType == TSP.DataManager.DocumentOfMemberRequestType.Revival)
            {
                string CrtEndDate = DocMemberFileManager[0]["ExpireDate"].ToString();
                Utility.Date objDate = new Utility.Date(CrtEndDate);
                string LastMonth = objDate.AddMonths(-1);
                string Today = Utility.GetDateOfToday();
                int IsDocExp = string.Compare(Today, LastMonth);
                if (IsDocExp <= 0)
                {
                    TransactionManager.CancelSave();
                    ShowMessage("تاریخ اعتبار مجوز انتخاب شده به پایان نرسیده است.");
                    return;
                }
            }

            MeFileRow["RegDate"] = txtLastRegDateImp.Text;
            MeFileRow["ExpireDate"] = txtExpDateImp.Text;
            MeFileRow["Description"] = txtDescription.Text;

            MeFileRow["CreateDate"] = Utility.GetDateOfToday();
            MeFileRow["FollowCode"] = Utility.GenFollowCode(Utility.FollowType.ImplementDocument);
            MeFileRow["MailNo"] = "";
            MeFileRow["MailDate"] = "";
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MeId"]))
                MeFileRow["MeId"] = DocMemberFileManager[0]["MeId"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFSerialNo"]))
                MeFileRow["MFSerialNo"] = DocMemberFileManager[0]["MFSerialNo"].ToString();
            MeFileRow["Type"] = (int)DocumentOfMemberRequestType;
            MeFileRow["DocType"] = 1;
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PrId"]))
                MeFileRow["PrId"] = DocMemberFileManager[0]["PrId"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PrIdOrigin"]))
                MeFileRow["PrIdOrigin"] = DocMemberFileManager[0]["PrIdOrigin"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                MeFileRow["MFNo"] = DocMemberFileManager[0]["MFNo"].ToString();
            if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNoOrigin"]))
                MeFileRow["MFNoOrigin"] = DocMemberFileManager[0]["MFNoOrigin"].ToString();
            MeFileRow["IsConfirm"] = 0;
            MeFileRow["InActive"] = 0;
            if (cmbGradeImp.SelectedItem != null)
                MeFileRow["GrdId"] = cmbGradeImp.SelectedItem.Value;
            MeFileRow["UserId"] = Utility.GetCurrentUser_UserId();
            MeFileRow["ModifiedDate"] = DateTime.Now;
            DocMemberFileManager.AddRow(MeFileRow);
            int cn = DocMemberFileManager.Save();
            #endregion
            if (cn > 0)
            {
                #region  WF
                int TableId = Convert.ToInt32(DocMemberFileManager[DocMemberFileManager.Count - 1]["MfId"]);
                int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
                WorkFlowTaskManager.FindByTaskCode(SaveInfoTaskCode);
                if (WorkFlowTaskManager.Count != 1)
                {
                    TransactionManager.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است.");
                    return;
                }
                int SaveInfoTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

                int CurrentNmcId = FindNmcId(SaveInfoTaskId);
                if (CurrentNmcId == -1)
                {
                    TransactionManager.CancelSave();
                    return;
                }
                string Description = "شروع جریان کار درخواست جدید در مجوز مجری حقیقی";
                int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, SaveInfoTaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), (int)TSP.DataManager.WorkFlowStateNmcIdType.NmcId, Description);
                if (WfStart <= 0)
                {
                    TransactionManager.CancelSave();
                    ShowMessage("خطایی در ذخیره انجام گرفته است");
                    return;
                }
                #endregion

                TransactionManager.EndSave();
                ShowMessage("ذخیره انجام شد.");
                TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileManager.GetUserPermissionImp(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                this.ViewState["BtnSave"] = btnSave.Enabled;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;
                HiddenFieldDocMemberFile["PageMode"] = Utility.EncryptQS("Edit");
                MenuMemberFile.Enabled = true;
                txtMeId.ReadOnly = true;
                RoundPanelMemberFile.Enabled = true;
                RoundPanelMemberFile.HeaderText = "ویرایش";
            }
            else
            {
                TransactionManager.CancelSave();
                DivReport.Visible = true;
                LabelWarning.Text = "خطایی در ذخیره انجام شد.";
            }

        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetError(err);
        }
    }
    #endregion

    #region Fills
    private void FillForm(int MfId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
        TSP.DataManager.ProvinceManager ProvinceManager = new TSP.DataManager.ProvinceManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();

        try
        {
            int MeId = int.Parse(Utility.DecryptQS(HiddenFieldDocMemberFile["MeId"].ToString()));
            FillMember(MeId);

            DocMemberFileManager.SelectImplementDoc(-1, MfId);
            if (DocMemberFileManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ExpireDate"]))
                    txtExpDateImp.Text = DocMemberFileManager[0]["ExpireDate"].ToString();

                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["RegDate"]))
                    txtLastRegDateImp.Text = DocMemberFileManager[0]["RegDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                    txtMfNoImp.Text = DocMemberFileManager[0]["MFNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["IsTemporary"]))
                    cmbIsTemporary.SelectedIndex = int.Parse(DocMemberFileManager[0]["IsTemporary"].ToString());
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["SerialNo"]))
                    txtSerialNoImp.Text = DocMemberFileManager[0]["SerialNo"].ToString();

                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["FollowCode"]))
                    txtFollowCode.Text = DocMemberFileManager[0]["FollowCode"].ToString();
                txtDescription.Text = DocMemberFileManager[0]["Description"].ToString();

                lblWorkFlowState.Visible = true;
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["TaskName"]))
                    lblWorkFlowState.Text = "وضعیت درخواست: " + DocMemberFileManager[0]["TaskName"].ToString();
                else
                    lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";


                int DocMemberFileId = int.Parse(DocMemberFileManager[0]["MeId"].ToString());
                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["GrdId"]))
                {
                    cmbGradeImp.DataBind();
                    cmbGradeImp.SelectedIndex = cmbGradeImp.Items.FindByValue(DocMemberFileManager[0]["GrdId"].ToString()).Index;
                }
                //DataTable dtDocMeDetail = DocMemberFileDetailManager.FindByResponsibility(DocMemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Implement);
                //if (dtDocMeDetail.Rows.Count > 0)
                //{
                //    DataTable dtMFMajor = DocMemberFileMajorManager.SelectMemberFileById(DocMemberFileId, MeId, 0, 1);
                //    if (dtMFMajor.Rows.Count > 0)
                //    {
                //        int MasterMjId = (int)dtMFMajor.Rows[0]["FMjId"];
                //        if (dtDocMeDetail.Rows[0]["MjId"].ToString() == dtMFMajor.Rows[0]["FMjId"].ToString())
                //        {
                //            txtGradeImp.Text = dtDocMeDetail.Rows[0]["GrdName"].ToString();
                //        }
                //    }
                //}

                if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["PrId"]))
                {
                    ProvinceManager.FindByCode(int.Parse(DocMemberFileManager[0]["PrId"].ToString()));
                    if (ProvinceManager.Count > 0)
                    {
                        txtProvinceNameImp.Text = ProvinceManager[0]["PrName"].ToString();
                    }
                }

                //DataTable dtImpDoc = DocMemberFileManager.SelectImplementDoc(MeId, MfId);
                //if (dtImpDoc.Rows.Count > 0)
                //{
                //    txtRegDateImp.Text = dtImpDoc.Rows[0]["RegDate"].ToString();
                //}

                DocMemberFileManager.FindByCode(DocMemberFileId, 0);
                if (DocMemberFileManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["MFNo"]))
                        lblMFNo.Text = DocMemberFileManager[0]["MFNo"].ToString();
                }
            }
            else
            {
                ShowMessage("اطلاعات توسط کاربر دیگری تغییر یافته است");
                return;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در بازخوانی اطلاعات رخ داده است");
        }
    }

    private bool FillMember(int MeId)
    {
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();
        MemberManager.FindByCode(MeId);
        HiddenFieldDocMemberFile["MeId"] = Utility.EncryptQS(MeId.ToString());
        if (MemberManager.Count == 1)
        {
            txtMeId.Text = MemberManager[0]["MeId"].ToString();
            lblMeName.Text = MemberManager[0]["FirstName"].ToString();
            lblMeLastName.Text = MemberManager[0]["LastName"].ToString();
            txtImpName.Text = MemberManager[0]["ImpGrdName"].ToString();
            txtDesName.Text = MemberManager[0]["DesGrdName"].ToString();
            txtObsName.Text = MemberManager[0]["ObsGrdName"].ToString();
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
            {
                ImgMember.ImageUrl = MemberManager[0]["ImageUrl"].ToString();
            }
            else
            {
                ImgMember.ImageUrl = "../../Images/Person.png";
            }

            DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
            if (DocMemberFileMajorManager.Count == 1)
            {
                txtMemberFileMajor.Text = DocMemberFileMajorManager[0]["MjName"].ToString();
            }

            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
            if (dtMeFile.Rows.Count == 1)
            {
                txtExpireDateMember.Text = DocMemberFileManager[0]["ExpireDate"].ToString();
                lblMFNo.Text = DocMemberFileManager[0]["MFNo"].ToString();
            }

            TransferMemberManager.FindByMemberId(MeId, TSP.DataManager.TransferMemberType.TransferFromOtherProvince);
            if (TransferMemberManager.Count > 0)
            {
                TblTransfer.Visible = true;
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
                    lblFileNo.Text = TransferMemberManager[0]["FileNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["MeNo"]))
                    lblPreMeNo.Text = TransferMemberManager[0]["MeNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["TransferDate"]))
                    lblTransferDate.Text = TransferMemberManager[0]["TransferDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["PrName"]))
                    lblPreProvince.Text = TransferMemberManager[0]["PrName"].ToString();
                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["DocPrId"]))
                {
                    ComboDocPr.DataBind();
                    ComboDocPr.SelectedIndex = ComboDocPr.Items.FindByValue(TransferMemberManager[0]["DocPrId"].ToString()).Index;
                }

                if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["ImageUrl"]))
                    HyperLinkTransfer.NavigateUrl = TransferMemberManager[0]["ImageUrl"].ToString();
            }
            else
            {
                TblTransfer.Visible = false;
            }

            return true;
        }
        else return false;
    }

    private void ClearForm()
    {
        txtMeId.Text = "";
        txtExpireDateMember.Text = "";
        lblMeLastName.Text = "";
        lblMeName.Text = "";
        lblMFNo.Text = "";
        txtExpDateImp.Text = "";
        cmbGradeImp.SelectedIndex = -1;
        txtLastRegDateImp.Text = "";
        txtMfNoImp.Text = "";
        txtProvinceNameImp.Text = "";
        //  txtRegDateImp.Text = "";
        txtFollowCode.Text = "";
        txtDesName.Text = "";
        txtObsName.Text = "";
        txtImpName.Text = "";
        txtMemberFileMajor.Text = "";
        txtSerialNoImp.Text = "";
        txtDescription.Text = "";
        cmbIsTemporary.SelectedIndex = -1;
        ImgMember.ImageUrl = "../../Images/Person.png";
        lblWorkFlowState.Text = "وضعیت درخواست: " + "نامشخص";
        TblTransfer.Visible = false;
    }

    void ShowMessage(string str)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = str;
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    bool CheckConditions(int MeId)
    {
        TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.AccountingDocBalanceDetailManager AccManager = new TSP.DataManager.AccountingDocBalanceDetailManager();
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OfficeRequestManager OfficeRequestManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();

        MemberManager.FindByCode(MeId);
        HiddenFieldDocMemberFile["MeId"] = Utility.EncryptQS(MeId.ToString());
        if (MemberManager.Count == 1)
        {
            int MRsId = int.Parse(MemberManager[0]["MrsId"].ToString());
            if (MRsId != (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.";
                return false;
            }

            #region Check OfficeMember
            OfMeManager.FindEngOfficeMemberByPersonId(MeId, (int)TSP.DataManager.EngOfficeConfirmationType.Confirmed);
            if (OfMeManager.Count > 0)
            {
                int EngOfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
                DataTable dtEngOffReq = OfMeManager.SelectLastRequestEngOfficeMember(EngOfIdMember, 0, MeId);
                if (dtEngOffReq.Rows.Count > 0)
                {
                    string str = "امکان ثبت مجوز اجرا برای این عضو وجود ندارد.عضو مورد نظر در دفتر ";
                    str += Utility.IsDBNullOrNullValue(dtEngOffReq.Rows[0]["EngOffName"]) ? "دیگری " : "<" + dtEngOffReq.Rows[0]["EngOffName"].ToString() + ">";
                    str += " مشغول به کار می باشد";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = (str);
                    return false;
                }
            }
            OfMeManager.FindOffMemberByPersonId(MeId, 2, (int)TSP.DataManager.MembershipRegistrationStatus.Confirmed, 1, (int)TSP.DataManager.OfficeDocumentStatus.Confirmed);
            if (OfMeManager.Count > 0)
            {
                int OfIdMember = Convert.ToInt32(OfMeManager[0]["OfId"]);
                DataTable dtOffReq = OfMeManager.SelectLastRequestOfficeMember(OfIdMember, 0, MeId);
                if (dtOffReq.Rows.Count > 0)
                {
                    string str = "امکان ثبت مجوز اجرا برای این عضو وجود ندارد.عضو مورد نظر در شرکت ";
                    str += Utility.IsDBNullOrNullValue(dtOffReq.Rows[0]["OfName"]) ? "دیگری " : "<" + dtOffReq.Rows[0]["OfName"].ToString() + ">";
                    str += " مشغول به کار می باشد";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = (str);
                    return false;
                }
            }
            #endregion

            #region CheckMemberFile
            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0);
            if (dtMeFile.Rows.Count <= 0)
            {
                ShowMessage("امکان ثبت عضو مورد نظر وجود ندارد.عضو انتخاب شده دارای پروانه اشتغال به کار تایید شده نمی باشد.");
                return false;
            }
            HiddenFieldDocMemberFile["LastMemberFileId"] = Utility.EncryptQS(DocMemberFileManager[0]["MfId"].ToString());
            int MemberFileId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());

            DataTable dtMeDetail = DocMemberFileDetailManager.FindByResponsibility(MemberFileId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Implement);
            if (dtMeDetail.Rows.Count <= 0)
            {
                ShowMessage("بدلیل نداشتن صلاحیت اجرا امکان درخواست مجوز اجرا برای عضو انتخاب شده وجود ندارد.");
                return false;
            }

            string ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();
            if (!string.IsNullOrEmpty(ExpireDate))
            {
                if (ExpireDate.CompareTo(Utility.GetDateOfToday()) <= 0)
                {
                    ShowMessage("بدلیل اتمام مدت اعتبار پروانه اشتغال امکان درخواست مجوز اجرا برای عضو انتخاب شده وجود ندارد.");
                    return false;
                }
            }

            if (!Utility.IsDBNullOrNullValue(dtMeFile.Rows[0]["IsConfirm"]) && dtMeFile.Rows[0]["IsConfirm"].ToString() == "1")
            {
                DocMemberFileMajorManager.SelectMemberMasterMajor(MeId);
                if (DocMemberFileMajorManager.Count <= 0)
                {
                    ShowMessage("رشته موضوع پروانه شخص انتخاب شده نامشخص است");
                    return false;
                }
            }
            else
            {
                ShowMessage("امکان ثبت عضو مورد نظر وجود ندارد.وضعیت پروانه اشتغال عضو نامشخص می باشد.");
                return false;
            }
            #endregion
        }
        else
        {
            ShowMessage("کد عضویت وارد شده معتبر نمی باشد.");
            return false;
        }
        return true;
    }
    #endregion

    #region WF
    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;

        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp);
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int TaskCodeDocUnit = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingImplementDoc;
            int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;

            if (CurrentTaskCode == DocMeFileSaveInfoTaskCode || CurrentTaskCode == TaskCodeDocUnit)
            {
                DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                if (dtWorkFlowState.Rows.Count > 0)
                {
                    int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, DocMeFileSaveInfoTaskCode, Utility.GetCurrentUser_UserId());
                    int PermissionTaskCodeDocUnit = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCodeDocUnit, Utility.GetCurrentUser_UserId());
                    if (Permission > 0 || PermissionTaskCodeDocUnit > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }


    }

    private void CheckWorkFlowPermission()
    {
        string PageMode = Utility.DecryptQS(HiddenFieldDocMemberFile["PageMode"].ToString());
        CheckWorkFlowPermissionForSave(PageMode);
        if (PageMode != "New" && PageMode != "ReDuplicate" && PageMode != "Revival" && PageMode != "Change" && PageMode != "InValid")
            CheckWorkFlowPermissionForEdit(PageMode);
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {
        int SaveTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        int TaskCodeDocUnit = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingImplementDoc;
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFileImp);

        TSP.DataManager.WFPermission WFPermission = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew(SaveTaskCode, TableType, Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPermissionTaskCodeDocUnit = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForSaveNew(TaskCodeDocUnit, TableType, Utility.GetCurrentUser_UserId(), PageMode);
        BtnNew.Enabled = btnNew2.Enabled = WFPermission.BtnNew || WFPermissionTaskCodeDocUnit.BtnNew;
        if (PageMode == "New")
            btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnSave || WFPermissionTaskCodeDocUnit.BtnSave;
        else if (PageMode == "ReDuplicate" || PageMode == "Revival" || PageMode == "Change" || PageMode == "InValid")
        {
            btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnNewRequest || WFPermissionTaskCodeDocUnit.BtnNewRequest;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        string TabelId = Utility.DecryptQS(Request.QueryString["MFId"].ToString());
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
        int TaskCodeDocUnit = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingImplementDoc;
        int WFCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;

        TSP.DataManager.WFPermission WFPermission = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, Convert.ToInt32(TabelId), Utility.GetCurrentUser_UserId(), PageMode);
        TSP.DataManager.WFPermission WFPermissionTaskCodeDocUnit = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCodeDocUnit, WFCode, Convert.ToInt32(TabelId), Utility.GetCurrentUser_UserId(), PageMode);

        btnSave.Enabled = btnSave2.Enabled = WFPermission.BtnSave|| WFPermissionTaskCodeDocUnit.BtnSave;
        btnEdit2.Enabled = btnEdit.Enabled = WFPermission.BtnEdit || WFPermissionTaskCodeDocUnit.BtnEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    private void InsertWorkFlowStateView(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "مشاهده", Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در مشاهده اطلاعات انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }

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
            ShowMessage("کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.");
            return (-1);
        }
    }
    #endregion

    private void SetImpDocDefualtExpireDate(int DocumentSetExpireDateType)
    {
        Utility.Date Date;
        if (string.IsNullOrEmpty(txtLastRegDateImp.Text))
        {
            txtLastRegDateImp.Text = Utility.GetDateOfToday();
            Date = new Utility.Date();
        }
        else
            Date = new Utility.Date(txtLastRegDateImp.Text);

        int MonthCount = 12;
        switch (DocumentSetExpireDateType)
        {
            case (int)TSP.DataManager.DocumentSetExpireDateType.Temporary:
                MonthCount = 12 * Utility.GetDefaultTemporaryImplementerDocExpireDate();
                txtExpDateImp.Text = Date.AddMonths(MonthCount);
                break;
            case (int)TSP.DataManager.DocumentSetExpireDateType.Permanent:

                MonthCount = 12 * Utility.GetDefaultPermanentImplementerDocExpireDate();
                txtExpDateImp.Text = Date.AddMonths(MonthCount);
                break;
        }
    }
    #endregion
}
